using System;
using System.Collections.Generic;

namespace RPGFramework
{
    public class CLIFrontend : GameFrontend
    {
        public Move selectMove(Character c)
        {
            return selectMoveRecur(c, c._mset, new Stack<MoveSet>());
        }

        private Move selectMoveRecur(Character c, MoveSet m, Stack<MoveSet> prev)
        {
            Dictionary<int, MoveTreeNode> objectMapping;
            string input;
            int entered;
            MoveTreeNode selected;
            while (true)
            {
                Console.WriteLine("What should {0} do?", c.Name);
                objectMapping = printMoves(m, prev);
                input = Console.ReadLine().Trim();
                if (Int32.TryParse(input, out entered))
                {
                    if (entered == 0)
                    {
                        MoveSet last = prev.Pop();
                        return selectMoveRecur(c, last, prev);
                    } else if (objectMapping.TryGetValue(entered, out selected))
                    {
                        if (selected is Move)
                            return (Move)selected;
                        else
                        {
                            prev.Push(m);
                            return selectMoveRecur(c, (MoveSet)selected, prev);
                        }
                    }
                }
                Console.WriteLine("incorrect entry, try again");
            }
        }

        private Dictionary<int, MoveTreeNode> printMoves(MoveSet mset, Stack<MoveSet> prev)
        {
            Dictionary<int, MoveTreeNode> mapping = new Dictionary<int, MoveTreeNode>();
            int opt = 1;
            string leadin = "";
            foreach(var prevset in prev)
            {
                Console.WriteLine("{0}{1}", leadin, prevset.Name);
                leadin += " ";
            }
            foreach(var item in mset.Children)
            {
                Console.WriteLine("{0}[{1}] {2}", leadin, opt, item.HasChildren() ? $">{item.Name}" : item.Name);
                mapping[opt] = item;
                opt++;
            }
            if (prev.Count != 0)
                Console.WriteLine("{0}[0] Back...", leadin);
            return mapping;
        }

        public Character selectCharacter(Character[] characters)
        {
            Dictionary<int, Character> opts;
            while (true)
            {
                opts = printCharacterOpt(characters);
                string input = Console.ReadLine().Trim();
                int entered;
                Character chosen;
                if (Int32.TryParse(input, out entered) && opts.TryGetValue(entered, out chosen))
                {
                    return chosen;
                }
                Console.WriteLine("Incorrect label, try again");
            }
        }

        Dictionary<int, Character> printCharacterOpt(Character[] characters)
        {
            Dictionary<int, Character> opts = new Dictionary<int, Character>();
            int mark = 1;
            Console.WriteLine("Select a Character:");
            foreach(var character in characters)
            {
                Console.WriteLine("[{0}] {1}", mark, character.Name);
                opts[mark] = character;
                mark++;
            }
            Console.WriteLine("[0] Back");
            opts[0] = null;
            return opts;
        }

        public void RequestEventDisplay(GameEvent m)
        {
            Console.WriteLine(m.Message);
        }

        public void RequestMoveEventDisplay(MoveEvent m, Character caster, Character target, bool didHit, int damage)
        {
            string completeMessage = m.Message.Replace("%c", caster.Name).Replace("%t", target == null ? "" : target.Name);
            string completeOnHit = m.OnHit.Replace("%c", caster.Name).Replace("%t", target == null ? "" : target.Name).Replace("%d", damage.ToString());
            string completeOnMiss = m.OnMiss.Replace("%c", caster.Name).Replace("%t", target == null ? "" : target.Name);
            Console.WriteLine(completeMessage);
            if (didHit)
                Console.WriteLine(completeOnHit);
            else
                Console.WriteLine(completeOnMiss);
        }
    }
}