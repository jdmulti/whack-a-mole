using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>, IInit, ILoop
{
    /*################################################################################
        Variables
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        References
    --------------------------------------------------------------------------------*/
    [Header("References")]
    public ComponentSystem componentSystem;
    public CountdownTimer countdownTimer;
    public CountdownTimer countdownTimerRandomAppear;
    public HoleManager holeManager;
    public MoleManager moleManager;
    public InputManager inputManager;
    public ScoreManager scoreManager;
    public DifficultyManager difficultyManager;
    public AudioManager audioManager;
    public Hammer hammer;

    public UICountdownTimer uiCountdownTimer;
    public UIScore uiScore;
    public UIWave uiWave;

    /*--------------------------------------------------------------------------------
        References
    --------------------------------------------------------------------------------*/
    [Header("Settings")]
    public int gameDuration;

    /*--------------------------------------------------------------------------------
        private
    --------------------------------------------------------------------------------*/
    private DifficultySettings difficultySettings;

    /*################################################################################
        Functions: public
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        Init
    --------------------------------------------------------------------------------*/
    public void Init()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        componentSystem.Initialized += ComponentSystem_OnInitialized;
        StateMachine.OnStateChange += StateMachine_OnStateChange;
        countdownTimer.OnFinished += CountdownTimer_OnFinished;
        countdownTimerRandomAppear.OnFinished += CountdownTimerRandomAppear_OnFinished;
        inputManager.OnHit += InputManager_OnHit;
        difficultyManager.OnChange += DifficultyManager_OnChange;
        hammer.OnHitMole += Hammer_OnHitMole;

        uiCountdownTimer.SetTime(countdownTimer.CurrentTime);
    }
    /*--------------------------------------------------------------------------------
        Loop
    --------------------------------------------------------------------------------*/
    public void Loop()
    {
        UserInput();

        uiCountdownTimer.SetTime(countdownTimer.CurrentTime);
        uiScore.SetScore(scoreManager.Score);
        difficultyManager.CheckDifficulty(countdownTimer.MaxDuration, countdownTimer.CurrentTime);
    }

    /*################################################################################
        Functions: private
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        UserInput
    --------------------------------------------------------------------------------*/
    private void UserInput()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    /*--------------------------------------------------------------------------------
        ScreenMenu
    --------------------------------------------------------------------------------*/
    private void ScreenMenu()
    {
        audioManager.StopAllMusic();
        audioManager.PlayMusic(AudioManager.MusicType.MusicMenu);
    }
    /*--------------------------------------------------------------------------------
        ScreenScore
    --------------------------------------------------------------------------------*/
    private void ScreenScore()
    {
        audioManager.StopAllMusic();
        audioManager.PlayMusic(AudioManager.MusicType.MusicScore);
    }
    /*--------------------------------------------------------------------------------
        GameStart
    --------------------------------------------------------------------------------*/
    private void GameStart()
    {
        difficultyManager.Reset();
        scoreManager.Reset();
        countdownTimer.SetCountdown(gameDuration);
        countdownTimerRandomAppear.SetCountdown(Random.Range(difficultySettings.randomAppearTimingMin, difficultySettings.randomAppearTimingMax));
        moleManager.randomHideTimeMin = difficultySettings.randomDisappearTimingMin;
        moleManager.randomHideTimeMax = difficultySettings.randomDisappearTimingMax;

        countdownTimer.CountdownStart();
        countdownTimerRandomAppear.CountdownStart();

        audioManager.StopAllMusic();
        audioManager.PlayMusic(AudioManager.MusicType.MusicLevel1);
    }
    /*--------------------------------------------------------------------------------
        GameStop
    --------------------------------------------------------------------------------*/
    private void GameStop()
    {
        countdownTimerRandomAppear.CountdownStop();
        holeManager.ResetHoles();
        moleManager.ResetMolePositions();

        audioManager.StopAllMusic();
        audioManager.PlayMusic(AudioManager.MusicType.MusicClear, OnGameMusicLevelClear_Finished);
    }

    /*################################################################################
        Functions: on event
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        ComponentSystem_OnInitialized
    --------------------------------------------------------------------------------*/
    private void ComponentSystem_OnInitialized()
    {
        StateMachine.SetState(StateMachine.State.MainMenu);
    }
    /*--------------------------------------------------------------------------------
        StateMachine_OnStateChange
    --------------------------------------------------------------------------------*/
    private void StateMachine_OnStateChange(StateMachine.State currenState, StateMachine.State previousState)
    {
        switch(currenState)
        {
            case StateMachine.State.MainMenu:
                ScreenMenu();
                break;
            case StateMachine.State.Game:
                GameStart();
                break;
            case StateMachine.State.Score:
                ScreenScore();
                break;
            default:
                break;
        }
    }
    /*--------------------------------------------------------------------------------
        CountdownTimer_OnFinished
    --------------------------------------------------------------------------------*/
    private void CountdownTimer_OnFinished()
    {
        GameStop();
    }
    /*--------------------------------------------------------------------------------
        CountdownTimerRandomAppear_OnFinished
    --------------------------------------------------------------------------------*/
    private void CountdownTimerRandomAppear_OnFinished()
    {
        if(holeManager.IsAnyHoleEmpty())
        {
            Hole hole = holeManager.GetRandomHole();
            moleManager.SetMoleHole(hole);
            audioManager.PlayFx(AudioManager.AudioType.Appear);
        }
        countdownTimerRandomAppear.SetCountdown(Random.Range(difficultySettings.randomAppearTimingMin, difficultySettings.randomAppearTimingMax));
        countdownTimerRandomAppear.CountdownStart();
    }
    /*--------------------------------------------------------------------------------
        InputManager_OnHit
    --------------------------------------------------------------------------------*/
    private void InputManager_OnHit(Mole mole)
    {
        scoreManager.AddScorePoints(mole.scorePoints);
        hammer.HitMole(mole);  
    }
    /*--------------------------------------------------------------------------------
        Hammer_OnHitMole
    --------------------------------------------------------------------------------*/
    private void Hammer_OnHitMole(Mole mole)
    {
        mole.Hit();
        audioManager.PlayFx(AudioManager.AudioType.Hit, 0.15f);
        audioManager.PlayFx(AudioManager.AudioType.Point, 0.4f);
    }
    /*--------------------------------------------------------------------------------
        DifficultyManager_OnChange
    --------------------------------------------------------------------------------*/
    private void DifficultyManager_OnChange(DifficultySettings difficultySettings)
    {
        this.difficultySettings = difficultySettings;
        uiWave.SetWave(difficultySettings.level);
    }
    /*--------------------------------------------------------------------------------
        OnGameMusicLevelClear_Finished
    --------------------------------------------------------------------------------*/
    private void OnGameMusicLevelClear_Finished()
    {
        StateMachine.SetState(StateMachine.State.Score);
    }
}
