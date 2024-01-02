using Logic;
using UnityEngine;

namespace Codebase.View
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private MarbleView marbleView;
        private Marble _marble;

        [field: SerializeField] public Vector2Int Pos { get; private set; }
        
        public void Initialize() => marbleView.Initialize();

        public void Show(Marble marble)
        {
            _marble = marble;
            if (marble == null)
            {
                marbleView.gameObject.SetActive(false);
                return;
            }
        
            marbleView.gameObject.SetActive(true);
            marbleView.Show(marble);
        }
    }
}