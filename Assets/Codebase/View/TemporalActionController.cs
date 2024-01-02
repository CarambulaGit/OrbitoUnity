using Logic;
using MyBox;
using UnityEngine;

namespace Codebase.View
{
    public class TemporalActionController : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private Vector2Int position;
        [SerializeField] private GameField.Direction direction;

        [ButtonMethod]
        public void PlaceMarble() => gameManager.Game.PlaceMarble(position.y, position.x);

        [ButtonMethod]
        public void MoveMarble() => gameManager.Game.MoveMarble(position.y, position.x, direction);

        [ButtonMethod]
        public void SkipMove() => gameManager.Game.SkipMove();
    }
}