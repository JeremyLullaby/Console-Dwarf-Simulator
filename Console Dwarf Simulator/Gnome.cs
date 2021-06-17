using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDwarfSimulator
{
    class Gnome : Humanoid
    {
        public override void Noise()
        {
            MakeNoise("Studies their spells.");
        }
    }
}
