using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDwarfSimulator
{
    class Duergar : Dwarf
    {
        public override void Noise()
        {
            MakeNoise("Lurks in the shadows.");
        }
    }
}
