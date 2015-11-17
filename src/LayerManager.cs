using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Nuernberger.ConsoleMenu
{
    public class LayerManager
    {
        public ObservableCollection<Block> Layers { get; private set; }

        public LayerManager()
        {
            this.Layers = new ObservableCollection<Block>();
        }

        public void AddLayer(Block layer)
        {
            this.Layers.Add(layer);
            layer.zIndex = this.Layers.Count;
        }

        public void AddLayer(Block layer, int zIndex)
        {
            this.Layers.Insert(zIndex, layer);
        }

        public void Draw()
        {
            foreach (Block layer in this.Layers)
                layer.Draw();
        }

        public void MoveToZ(Block layer, int zIndex)
        {
            this.Layers.Move(layer.zIndex, zIndex);
        }
    }
}
