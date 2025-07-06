using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhoenixForgeGames.Commands.Public.Commands
{
    [CreateAssetMenu(fileName = "Command - Execute After Seconds - ", menuName = "Phoenix Forge Games/Commands/Public/Execute After Seconds", order = 0)]
    public class ForgeCommandExecuteCommandAfterSeconds : ForgeCommand
    {
        public bool debug;
        public List<ForgeCommand> delayedCommands;
        public float seconds;
        
        public override void Execute(MonoBehaviour caller)
        {
            base.Execute(caller);
            caller.StartCoroutine(ExecuteDelayed(caller));
        }

        private IEnumerator ExecuteDelayed(MonoBehaviour caller)
        {
            yield return new WaitForSeconds(seconds);
            foreach (var command in delayedCommands)
            {
                command.Execute(caller);
            }
        }
    }
}