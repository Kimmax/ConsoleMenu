using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nuernberger.ConsoleMenu
{
    public class LayerManager
    {
        public List<Block> Layers { get; private set; }

        public LayerManager()
        {
            this.Layers = new List<Block>();
        }

        public void AddLayer(Block layer)
        {
            this.Layers.Add(layer);
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
    }
}
