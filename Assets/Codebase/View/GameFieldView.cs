using System.Collections.Generic;
using Logic;
using MyBox;
using UnityEngine;
using Utils;

namespace Codebase.View
{
    public class GameFieldView : MonoBehaviour
    {
        [SerializeField] private List<CellView> marbleViews;

        public void Initialize() => marbleViews.ForEach(marbleView => marbleView.Initialize());

        public void Show(Marble[,] fieldArrayRepresentation) => 
            marbleViews.ForEach((marble, i) => marble.Show(fieldArrayRepresentation.ByListIndex(i)));
    }
}