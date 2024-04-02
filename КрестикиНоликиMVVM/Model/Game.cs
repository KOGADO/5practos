using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TicTacToeWPF_MVVM.Model
{
    public class Game
    {
        public List<Player> Players { get; set; } = new List<Player>();
        public string CurrentMark { get; set; }
        private List<string> MarksToAssign { get; set; } = new List<string>();
        public string Winner { get; set; }
        public bool IsFinished { get; set; }

        public Game(Player p1, Player p2)
        {
            IsFinished = false;

            Players.Add(p1);
            Players.Add(p2);

            MarksToAssign.Add("X");
            MarksToAssign.Add("O");
            AssignMark();

            foreach (var player in Players)
            {
                Console.WriteLine($"PlayerInfo: {player.Name}, {player.Mark}, {player.IsTurn}");
            }

            AssignTurn();

            foreach (var player in Players)
            {
                Console.WriteLine($"PlayerInfo after turn assignment: {player.Name}, {player.Mark}, {player.IsTurn}");
            }
        }

        public string WhoseRound()
        {
            var player = Players.First(x => x.IsTurn == false);

            return $"Ход игрока: {player.Name}, его сторона {player.Mark}";
        }

        private void ChangeTurn(Player player)
        {
            player.IsTurn = !player.IsTurn;      
        }

        public void ChangeTurns(string[] field)
        {
            foreach (var player in Players)
            {
                if (CheckWin(field) == true)
                {
                    var winner = Players.FirstOrDefault(x => x.IsWinner == true);
                    MessageBox.Show($"Победитель - {winner.Name}, сторона игрока {winner.Mark}");
                    IsFinished = true;
                    break;
                }
                else if (IsTie(field))
                {
                    MessageBox.Show("Ничья!");
                    IsFinished = true;
                    break;
                }

                if (player.IsTurn)
                {
                    CurrentMark = player.Mark;
                }

                ChangeTurn(player);
            }
        }

        private void AssignTurn()
        {
            var player = Players.FirstOrDefault(x => x.Mark == "X");
            player.IsTurn = true;
        }

        private bool CheckWin(string[] table)
        {
            var conditionToWin1 = CheckWinHorizontal(table);
            var conditionToWin2 = CheckWinVertical(table);
            var conditionToWin3 = CheckWinDiagonal(table);

            return conditionToWin1 || conditionToWin2 || conditionToWin3;
        }

        private bool CheckWinDiagonal(string[] table)
        {
            if ((table[0] + table[4] + table[8]) == "XXX")
            {
                var winner = Players.FirstOrDefault(x => x.Mark == "X");
                winner.IsWinner = true;

                return true;
            }

            if ((table[0] + table[4] + table[8]) == "OOO")
            {
                var winner = Players.FirstOrDefault(x => x.Mark == "O");
                winner.IsWinner = true;

                return true;
            }

            if ((table[2] + table[4] + table[6]) == "XXX")
            {
                var winner = Players.FirstOrDefault(x => x.Mark == "X");
                winner.IsWinner = true;

                return true;
            }

            if ((table[2] + table[4] + table[6]) == "OOO")
            {
                var winner = Players.FirstOrDefault(x => x.Mark == "O");
                winner.IsWinner = true;

                return true;
            }

            return false;
        }

        private bool CheckWinVertical(string[] table)
        {
            for (int i = 0; i < 3; i++)
            {
                string txt = table[i] + table[i + 3] + table[i + 6];
                if (txt == "XXX")
                {
                    var winner = Players.FirstOrDefault(x => x.Mark == "X");
                    winner.IsWinner = true;
                    
                    return true;
                }

                if (txt == "OOO")
                {
                    var winner = Players.FirstOrDefault(x => x.Mark == "O");
                    winner.IsWinner = true;

                    return true;
                }
                    
            }

            return false;
        }

        private bool CheckWinHorizontal(string[] table)
        {
            for (int i = 0; i < 9; i += 3)
            {
                string txt = table[i] + table[i + 1] + table[i + 2];
                if (txt == "XXX")
                {
                    var winner = Players.FirstOrDefault(x => x.Mark == "X");
                    winner.IsWinner = true;

                    return true;
                }

                if (txt == "OOO")
                {
                    var winner = Players.FirstOrDefault(x => x.Mark == "O");
                    winner.IsWinner = true;

                    return true;
                }
            }

            return false;
        }

        private bool IsTie(string[] table)
        {
            var txt = "";

            foreach(var element in table)
            {
                txt += element;
            }

            if (txt.Length == 9 && !String.IsNullOrWhiteSpace(txt))
                return true;
            
            return false;
        }

        private void AssignMark()
        {
            var random = new Random();

            foreach (var player in Players)
            {
                int index = random.Next(MarksToAssign.Count);
                player.Mark = MarksToAssign.ElementAt(index);
                MarksToAssign.RemoveAt(index);
            }
        }
    }
}
