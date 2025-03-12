using System;
using BetterReality.Events;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BetterReality.Framework
{
    public class BetterPlayer : MonoBehaviour
    {
        public float headHeight=1.6f;
        public float handHeight=.8f;
        public float floorHeight = 0;
        public EndCalibration endCalibration = new EndCalibration();
        public Transform headTransform;
        public Transform handTransform;
        [SerializeField] InputActionReference setHeight;
        
        [SerializeField]
        public TextMeshPro HUD;
        private GameMan _gameMan;
        private bool _calibrating;
        [SerializeField] private float countDown = 5;
        private float remainingCountDown;

        // can't recalibrate after first game
        private bool _hasStarted;
        
        private void Awake()
        {
            setHeight.action.Enable(); 
            setHeight.action.performed += StartCalibration;
            _gameMan = FindObjectOfType<GameMan>();
        }
        
        private void Start()
        {
            //HUD.text = "Right grip for Height Calibration";
            //_calibrating = false;
            // or we could just start with this!
           StartCalibration();
           _gameMan.gameStartedEvent.AddListener( () => { _hasStarted = true; } );
        }

        private void StartCalibration()
        {
            _calibrating = true;
            remainingCountDown = countDown;
        }

        private void StartCalibration (InputAction.CallbackContext context) 
        {
            if (!_gameMan.Started)
            {
                StartCalibration();
            }
        }

        private void Update()
        {
            if (_calibrating && !_hasStarted)
            {
                remainingCountDown -= Time.deltaTime;
                if (remainingCountDown <= 0)
                {
                    CalibrateHeight();
                    HUD.text = $"Calibration\n\nComplete";
                    StartCoroutine(BetterUtils.WaitAndRun(2, () => { HUD.text = ""; }));
                    StartCoroutine(BetterUtils.WaitAndRun(3, () => { HUD.text = "Start Game: \nRight Trigger\n\nRecalibrate:\nLeft Trigger"; }));
                    _calibrating = false;
                }
                else 
                {
                    HUD.text = $"Calibrating\n{Math.Round(remainingCountDown, 1).ToString("0.0")}s\n\nLook ahead\narms by your side\n";
                }
            }
        }

        public void CalibrateHeight()
        {
            if (!_gameMan.Started)
            {
                headHeight=headTransform.position.y;
                handHeight=handTransform.position.y;
                endCalibration.Invoke(Vector3.zero);
                Debug.Log($"height calibration {floorHeight}->{handHeight}->{headHeight}");
            }
        }
    }
    
}