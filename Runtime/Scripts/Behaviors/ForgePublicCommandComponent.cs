using System;
using System.Collections;
using System.Collections.Generic;
using PhoenixForgeGames.Commands.Public.Scripts.ScriptableObjects;
using UnityEngine;

namespace PhoenixForgeGames.Commands.Public.Behaviors
{
    public class ForgePublicCommandComponent : MonoBehaviour
    {
        [Header("Interval Command Settings")]
        public float initialDelay = 0f;
        public float interval = 1f;
        
        [Header("Delayed Command Settings")]
        [Header("Command Delay after triggered")]
        public float delay;
        [Header("Trigger Automatically")]
        public bool delayOnStart;
        
        [Header("Base Commands")]
        public List<ForgeCommand> awakeCommands;
        public List<ForgeCommand> startCommands;
        public List<ForgeCommand> updateCommands;
        public List<ForgeCommand> fixedUpdateCommands;
        public List<ForgeCommand> onEnabledCommands;
        public List<ForgeCommand> onDisabledCommands;
        public List<ForgeCommand> delayedCommands;
        public List<ForgeCommand> intervalCommands;
        public List<ForgeCommand> triggeredCommands;

        protected virtual void Awake()
        {
            ExecuteCommandList(awakeCommands);
        }

        protected virtual void Start()
        {
            ExecuteCommandList(startCommands);
            
            if (delayOnStart)
            {
                ExecuteDelayedCommands();
            }

            if (intervalCommands.Count > 0)
            {
                InvokeRepeating(nameof(ExecuteIntervalCommands), initialDelay, interval);
            }
        }

        protected void Update()
        {
            ExecuteCommandList(updateCommands);
        }

        protected void FixedUpdate()
        {
            ExecuteCommandList(fixedUpdateCommands);
        }

        protected void OnEnable()
        {
            ExecuteCommandList(onEnabledCommands);
        }

        protected void OnDisable()
        {
            ExecuteCommandList(onDisabledCommands);
        }

        public virtual void ExecuteDelayedCommands()
        {
            if (delayedCommands.Count > 0)
            {
                StartCoroutine(DelayThenExecuteCommands());
            }
        }

        public virtual void ExecuteTriggeredCommands()
        {
            ExecuteCommandList(triggeredCommands);
        }


        protected virtual void ExecuteIntervalCommands()
        {
            ExecuteCommandList(intervalCommands);
        }
        
        
        
        protected virtual IEnumerator DelayThenExecuteCommands()
        {
            var time = 0f;
            while (time < delay)
            {
                time += 1;
                yield return new WaitForSeconds(1);
            }
            ExecuteCommandList(delayedCommands);
        }

        public void ExecuteCommandList<T>(List<T> list) where T : ForgeCommand
        {
            foreach (var command in list)
            {
                command.Execute(this);
            }
        }
    }
}