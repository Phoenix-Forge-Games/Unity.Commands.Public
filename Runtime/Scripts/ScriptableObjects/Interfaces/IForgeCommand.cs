using UnityEngine;

namespace PhoenixForgeGames.Commands.Public.Commands.Interfaces
{
    public interface IForgeCommand
    {
        public void Execute();
        public void Execute(MonoBehaviour caller);
    }
}