using ChainResponsibility;
using System;

namespace ChainResponsibility
{
    public interface IHandler
    {
        public IHandler Successor { get; set; }
        public void Handle(int d);
        public void Cancel();
    }

    public class Receptionist : IHandler
    {
        public IHandler Successor { get; set; }

        public void Handle(int d)
        {
            if (d < 0)
            {
                Cancel();
                return;
            }
            if (Successor != null) Successor.Handle(d);
        }

        public void Cancel()
        {
            Console.WriteLine("Go away from Receptionst!!!");
        }
    }

    public abstract class Doctor : IHandler
    {
        public IHandler Successor { get; set; }
        public string Name { get; set; }
        public int[] disiases { get; set; }
        public abstract void Handle(int d);

        public void Cancel()
        {
            Console.WriteLine("I don't know");
        }
    }

    public class Surgery : Doctor
    {
        public Surgery()
        {
            Name = "Surgery";
            disiases = new int[] { 1, 2, 3 };
        }

        public override void Handle(int d)
        {
            bool success = Array.Exists(disiases, disiase => disiase == d);
            if (success)
            {
                Console.WriteLine("You are Handleed from {0}", Name);
                return;
            }
            if (Successor != null) Successor.Handle(d);
            else Cancel();
        }
    }

    public class Optometrist : Doctor
    {
        public Optometrist()
        {
            Name = "Optometrist";
            disiases = new int[] { 4, 5, 6 };
        }

        public override void Handle(int d)
        {
            bool success = Array.Exists(disiases, disiase => disiase == d);
            if (success)
            {
                Console.WriteLine("You are Handleed from {0}", Name);
                return;
            }
            if (Successor != null) Successor.Handle(d);
            else Cancel();
        }
    }

    public class Nevrologist : Doctor
    {
        public Nevrologist()
        {
            Name = "Nevrologist";
            disiases = new int[] { 7, 8, 9 };
        }

        public override void Handle(int d)
        {
            bool success = Array.Exists(disiases, disiase => disiase == d);
            if (success)
            {
                Console.WriteLine("You are Handleed from {0}", Name);
                return;
            }
            if (Successor != null) Successor.Handle(d);
            else Cancel();
        }
    }

    public class Patient
    {
        public int Disiase { get; set; }
        public Patient(int d)
        {
            this.Disiase = d;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Patient Sasha = new Patient(5);
            Surgery s = new Surgery();
            Optometrist o = new Optometrist();
            Nevrologist n = new Nevrologist();
            Receptionist r = new Receptionist();
            r.Successor = s;
            s.Successor = o;
            o.Successor = n;
            r.Handle(Sasha.Disiase);
        }
    }
}
