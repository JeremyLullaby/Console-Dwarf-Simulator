using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDwarfSimulator
{
    class Humanoid
    {
        #region vars and props

        private Humanoid parent;
        private int generation = 0;

        private int noiseTime = 2800;

        public int Life { get { return life; } }
        private int life;

        public bool alive { get { return (life > 0); } }

        public int Age { get { return age; } }
        private int age = 0;

        public string firstName;
        public string SurName
        {
            get { return surName; }
            set
            {
                surName = value;
                baseName = value;
            }
        }
        private string surName;
        private string baseName;

        #endregion

        #region events and delegates
        static protected Random random = new Random();

        public delegate void Birth(Humanoid sender, Humanoid offspring);
        public event Birth Born;
        public delegate void Death(Humanoid sender);
        public event Death Died;

        public delegate void Message(Humanoid sender, string msg);
        public event Message NewMessage;

        #endregion

        public Humanoid()
        {
            life = random.Next(6000, 6500);
        }

        #region overrides
        public override string ToString()
        {
            return string.IsNullOrEmpty(firstName + surName) ?
                "(" + GetType().Name + ")" : firstName + " " + surName;
        }

        public string ToString(bool showStatistics)
        {
            return ToString() + (showStatistics ?
                " [Type " + GetType().Name
                + " Age " + age.ToString()
                + ", Life " + life.ToString()
                + " Alive: " + alive
                + "]" : "");
        }
        #endregion

        public virtual Type DetermineSpawnType()
        {
            return this.GetType();
        }

        public virtual void Noise()
        {
            // base class does nothing. only used through inheritence
        }

        protected void MakeNoise(string noise)
        {
            if (NewMessage != null) NewMessage(this, noise);
        }

        public void Live()
        {
            if (alive)
            {
                age++;
                life -= 25;
                if (!alive)
                {
                    if (Died != null) Died(this);
                }
                else
                {
                    if (life % 3000 < 15)
                    {
                        Spawn();
                    }

                    if (life % noiseTime < 20)
                    {
                        Noise();
                    }
                }
            }
            else
            {
                life = 0;
            }
        }

        private void Spawn()
        {
            if (age < 50) return;

            Humanoid child = (Humanoid)Activator.CreateInstance(DetermineSpawnType());
            child.parent = this;
            child.baseName = baseName;
            child.generation = generation + 1;
            if (NewMessage != null) NewMessage(this, "Spawning a new " + GetType().Name);
            child.surName = baseName + " " + child.generation.ToString();
            if (Born != null) Born(this, child);
        }
    }
}
