using PhoenixForgeGames.Commands.Public.Behaviors;
using PhoenixForgeGames.Commands.Public.Scripts.ScriptableObjects.Interfaces;
using UnityEngine;

namespace PhoenixForgeGames.Commands.Public.Scripts.ScriptableObjects
{
    public class ForgeCommand : ScriptableObject, IForgeCommand
    {
        protected ForgePublicCommandComponent ForgePublicCommandComponent;
        public string commandDescription;
        
        public virtual void Execute()
        {
        }

        public virtual void Execute(MonoBehaviour caller)
        {
            ForgePublicCommandComponent = caller.GetComponent<ForgePublicCommandComponent>();
            Execute();
        }
    }
}