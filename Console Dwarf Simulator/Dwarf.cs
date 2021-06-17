using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDwarfSimulator
{
    class Dwarf : Humanoid
    {
        public override void Noise()
        {
            MakeNoise("Takes a sip of Ale.");
        }

        public override Type DetermineSpawnType()
        {
            int n = random.Next(1, 3);
            if (n == 1)
            {
                return typeof(Duergar);
            }
            else
            {
                return base.DetermineSpawnType();
            }
        }
    }
}
