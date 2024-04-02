using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeWPF_MVVM.Model;

namespace TicTacToeWPF_MVVM.ViewModel
{
    using BaseClass;
    using System.Windows.Controls;
    using System.Windows.Input;
    public class MainViewModel : ViewModel
    {

     


        private Game _ngame;

        private bool _gameInProgress = false;

        public string[] _table = new string[] { "", "", "", "", "", "", "", "", "" };

        public string[] Table
        {
            get { return _table; }
            set
            {
                _table = value;
                onPropertyChange(nameof(Table));
            }
        }

        private string _whoseRound;

        public string WhoseRound
        {
            get
            {
                return _whoseRound;
            }
            set
            {
                _whoseRound = value;
                onPropertyChange(nameof(WhoseRound));
            }
        }


        public MainViewModel()
        {
        }

        private string _playerName1;

        public string PlayerName1
        {
            get { return _playerName1; }
            set
            {
                _playerName1 = value;
                onPropertyChange(nameof(PlayerName1));
            }
        }

        private string _playerName2;

        public string PlayerName2
        {
            get { return _playerName2; }
            set
            {
                _playerName2 = value;
                onPropertyChange(nameof(PlayerName2));
            }
        }

        private ICommand _onClick;

        public ICommand OnClick
        {
            get
            {
                return _onClick ?? (_onClick = new RelayCommand(OnClickFunc, CanOnClick));
            }
        }

        private void OnClickFunc(object obj)
        {
            int field = int.Parse((string)obj);

            Table[field] = _ngame.CurrentMark;

            Table = Table;

            _ngame.ChangeTurns(Table);
            WhoseRound = _ngame.WhoseRound();

            if (_ngame.IsFinished)
            {
                Table = new string[] { "", "", "", "", "", "", "", "", "" };
                PlayerName1 = "";
                PlayerName2 = "";
                _gameInProgress = false;
                WhoseRound = "";
            }
        }

        private bool CanOnClick(object param)
        {
            int field = int.Parse((string)param);

            if (!_gameInProgress)
            {
                return false;
            }

            for (int i = 0; i < Table.Length; i++)
            {
                if (Table[field] != "")
                    return false;
            }

            return true;
        }

        private ICommand _start;

        public ICommand Start
        {
            get
            {
                return _start ?? (_start = new RelayCommand(StartGame, CanStartGame));
            }
        }

        private void StartGame(object param)
        {
            var player1 = new Player(_playerName1);
            var player2 = new Player(_playerName2);
            _ngame = new Game(player1, player2);
            _gameInProgress = true;

            _ngame.ChangeTurns(Table);

            WhoseRound = _ngame.WhoseRound();
        }

        private bool CanStartGame(object param)
        {
            var textBoxEmpty = !(String.IsNullOrEmpty(_playerName1) || String.IsNullOrEmpty(_playerName2));

            return (textBoxEmpty && !_gameInProgress);
        }
    }
}
