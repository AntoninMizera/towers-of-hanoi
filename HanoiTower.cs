using System.Text;

namespace HanoiTowers
{
    public class HanoiTower
    {
        private const int SOURCE = 0;
        private const int DESTINATION = 2;

        private Stack<int>[] stacks = new Stack<int>[3];
        private int discAmount;

        public HanoiTower(int discs = 5)
        {
            discAmount = discs > 0 ? discs : 1;

            for (int i = 0; i < stacks.Length; i++)
            {
                stacks[i] = new(); // init stacks
            }

            while (discs > 0) {
                stacks[SOURCE].Push(discs--);
            }
        }

        public void Move()
        {
            // ke spravnemu posunu je treba 2^n - 1 kroku
            int stepAmount = (1 << discAmount) - 1;

            for (int i = 0; i < stepAmount; i++)
            {
                /*
                    Pro lichy pocet disku je posloupnost
                        A <-> B
                        A <-> C
                        B <-> C

                    Pro sudy pocet disku je posloupnost
                        A <-> C
                        A <-> B
                        B <-> C

                    Vzdy se mezi dvema disky provadi jen tah, ktery je platny,
                    tj. 

                    viz https://en.wikipedia.org/wiki/Tower_of_Hanoi#Simpler_statement_of_iterative_solution
                */
                if ((discAmount & 1) == 1)
                {
                    switch (i % 3)
                    {
                        case 0:
                            {
                                Swap(0, 2);
                                break;
                            }
                        case 1:
                            {
                                Swap(0, 1);
                                break;
                            }

                        case 2:
                            {
                                Swap(1, 2);
                                break;
                            }
                    }
                }
                else
                {
                    switch (i % 3)
                    {
                        case 0:
                            {
                                Swap(0, 1);
                                break;
                            }
                        case 1:
                            {
                                Swap(0, 2);
                                break;
                            }
                        case 2:
                            {
                                Swap(1, 2);
                                break;
                            }
                    }
                }
            }
        }

        private void Swap(int s1Idx, int s2Idx)
        {
            int valS1 = 0, valS2 = 0;

            stacks[s1Idx].TryPeek(out valS1);
            stacks[s2Idx].TryPeek(out valS2);

            // System.Console.WriteLine($"{s1Idx}: {valS1}, {s2Idx}: {valS2}");

            // je-li S1 prazdna, tak jediny platny tah je S2 -> S1
            if (valS1 == 0) {
                MoveToPosition(s2Idx, s1Idx);
                return;
            }

            // je-li S2 prazdna, tak jediny platny tah je S1 -> S2
            if (valS2 == 0) {
                MoveToPosition(s1Idx, s2Idx);
                return;
            }

            // je-li S1 vetsi nez S2, tak jediny platny tah je S2 -> S1
            if (valS1 > valS2) MoveToPosition(s2Idx, s1Idx);
            // je-li S1 mensi nebo rovna S2, tak jediny platny tah je S1 -> S2
            else MoveToPosition(s1Idx, s2Idx);
        }

        private void MoveToPosition(int from, int to)
        {
            int valFrom = 0, valTo = 0;

            stacks[to].TryPeek(out valTo);

            if (!stacks[from].TryPeek(out valFrom)) return;

            if (valTo != 0 && valFrom > valTo)
            {
                throw new Exception($"Cannot put a larger disc on top of a smaller one ({valFrom} from stack {from} on top of {valTo} to stack {to})");
            }

            System.Console.WriteLine($"{from} -> {to} (valFrom: {valFrom}, valTo: {valTo})");
            stacks[to].Push(stacks[from].Pop());
        }

        public override string ToString()
        {
            int maxLength = stacks.Select(x => x.Count).Max();

            StringBuilder sb = new();
            foreach (var stk in stacks)
            {
                StringBuilder sb2 = new();
                foreach (var num in stk)
                {
                    sb2.Append(num);
                }

                sb.Append($"STK: {sb2.ToString().PadLeft(maxLength)}\n");
            }

            return sb.ToString();
        }
    }
}