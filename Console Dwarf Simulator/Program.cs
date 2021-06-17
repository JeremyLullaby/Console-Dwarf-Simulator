using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace ConsoleDwarfSimulator
{
    //https://www.youtube.com/watch?v=mZTuy9zHuyE

    class Program
    {
        static List<Humanoid> humanoids = new List<Humanoid>();
        static List<string> messages = new List<string>();

        static void NewHumanoid(Humanoid h)
        {
            lock (humanoids)
            {
                humanoids.Add(h);
            }

            h.Born += Dwarf_Born;
            h.Died += Dwarf_Died;
            h.NewMessage += H_NewMessage;
        }

        static void Main(string[] args)
        {
            NewHumanoid(new Dwarf { firstName = "Halgroli", SurName = "Treasuremantle" });
            NewHumanoid(new Dwarf { firstName = "Hereda", SurName = "Mudcloak" });
            NewHumanoid(new Dwarf { firstName = "Thoadaes", SurName = "Chaosbender" });
            NewHumanoid(new Gnome { firstName = "Heinrich", SurName = "Schlitt" });
            NewHumanoid(new Gnome { firstName = "Lorifi", SurName = "Derrend" });
            NewHumanoid(new Gnome { firstName = "Bilmop", SurName = "Darkhelm" });

            while (humanoids.Count > 0)
            {
                Console.Clear();
                List<Humanoid> copy;
                lock (humanoids)
                {
                    copy = new List<Humanoid>(humanoids);
                }
                foreach (Humanoid dwarf in copy)
                {
                    dwarf.Live();
                    Console.WriteLine(dwarf.ToString(true));
                }
                ShowMessages();
                Thread.Sleep(50);
            }
        }

        public static void ShowMessages()
        {
            Console.SetCursorPosition(0, 12);
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            List<string> msgs = new List<string>(messages);
            msgs.Reverse();
            msgs = msgs.Take(10).ToList();
            msgs.Reverse();
            foreach (string s in msgs)
            {
                Console.WriteLine(s);
            }
            Console.ForegroundColor = prevColor;
        }

        private static void Dwarf_Died(Humanoid sender)
        {
            lock (humanoids)
            {
                humanoids.Remove(sender);
            }
            messages.Add(sender.ToString() + " died.");
        }

        private static void Dwarf_Born(Humanoid sender, Humanoid offspring)
        {
            NewHumanoid(offspring);
            messages.Add(sender.ToString() + " born.");
        }

        private static void H_NewMessage(Humanoid sender, string msg)
        {
            messages.Add(sender.ToString() + ": " + msg);
        }

    }
}
