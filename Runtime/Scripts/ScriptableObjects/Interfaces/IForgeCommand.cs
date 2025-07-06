using UnityEngine;

namespace PhoenixForgeGames.Commands.Public.Scripts.ScriptableObjects.Interfaces
{
    public interface IForgeCommand
    {
        public void Execute();
        public void Execute(MonoBehaviour caller);
    }
}