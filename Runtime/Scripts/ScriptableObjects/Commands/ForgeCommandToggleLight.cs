using UnityEngine;

namespace PhoenixForgeGames.Commands.Public.Commands
{
    [CreateAssetMenu(fileName = "Command - Toggle Light - ", menuName = "Phoenix Forge Games/Commands/Public/Toggle Light", order = 0)]
    public class ForgeCommandToggleLight : ForgeCommand
    {
        public bool shouldBeOn;

        public override void Execute(MonoBehaviour caller)
        {
            var light = caller.GetComponent<Light>();
            if (!light) return;

            light.enabled = shouldBeOn;
            
            base.Execute(caller);
        }
    }
}