using System;
using TMPro;
using UnityCommunity.UnitySingleton;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BetterReality.Framework
{
    public class TestMan : PersistentMonoSingleton<TestMan>
    {
        [field: SerializeField] protected string StartScene { get; private set; }

        [SerializeField] protected string m_PlayerName;

        public string testField;
        [SerializeField] protected int testInt;

        [field: SerializeField] public string SubjectName { get; set; }


        [SerializeField] private int _health;

        [SerializeField] private string _hudText;

        // indicates whether a mock testman is in use on a gamepage or if it's included via _app
        public bool DevMode = false;

        // bind to tmpro instance for heads up display
        public TextMeshProUGUI hudTMPGui;
        
        
        public int Health
        {
            get => _health;
            set => _health = value;
        }

        public string HudText
        {
            get => _hudText;
            set
            {
                _hudText = value;
                hudTMPGui.text = _hudText;
            }
        }

        // time test started
        private double _startTime;
        protected virtual void Start()
        {
            if (StartScene != null && StartScene.Length > 0 && !DevMode)
            {
                SceneManager.LoadScene(StartScene);
            }
            _startTime = Math.Round(Time.time*1000);
            Debug.Log($"Initialised TestMan singleton @ {_startTime} by {name}");
            // persist accross scene changes
            DontDestroyOnLoad(this); 
        }

        public double Milliseconds
        {
            get { return Math.Round(Time.time * 1000 - _startTime); }
        }
        
        // used to load app during development - do we?
        // currently disabled but have not sure if it's needed for the TestMan object to persits
        /*
        public static void DevPreload()
        {
            GameObject check = GameObject.Find("__app");
            if (check == null)
            {
                Debug.Log("loading preloader for dev preview");
                SceneManager.LoadScene("_preload");
            }
        }
        */        
        
    }
}