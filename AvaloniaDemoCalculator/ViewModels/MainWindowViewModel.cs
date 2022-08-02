using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using AvaloniaDemoCalculator.Models;
using ReactiveUI;


namespace AvaloniaDemoCalculator.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private double _firstValue;
        private double _secondValue;
        private Operation _operation = Operation.Add;

        public double ShownValue
        {
            get => _secondValue;
            set => this.RaiseAndSetIfChanged(ref _secondValue, value);
        }

        public ReactiveCommand<int, Unit> AddNumberCommand { get; }
        public ReactiveCommand<Unit, Unit> RemoveLastNumberCommand { get; }
        public ReactiveCommand<Operation, Unit> ExecuteOperationCommand { get; }

        public ReactiveCommand<Unit, Unit> ExitCommand { get; }

        public MainWindowViewModel()
        {
            AddNumberCommand = ReactiveCommand
                .Create<int>(AddNumber);

            ExecuteOperationCommand = ReactiveCommand
                .Create<Operation>(ExecuteOperation);

            RemoveLastNumberCommand = ReactiveCommand
                .Create(RemoveLastNumber);

            ExitCommand = ReactiveCommand.Create( () => Environment.Exit(0));
        }

        private void AddNumber(int value)
        {
            ShownValue = ShownValue * 10 + value;
        }

        public void ClearScreen()
        {
            ShownValue = 0;
            _operation = Operation.Add;
            _firstValue = 0;
        }

        public void RemoveLastNumber()
        {
            ShownValue = (int)ShownValue / 10;
        }
        private void ExecuteOperation(Operation operation)
        {
            switch (_operation)
            {
                case Operation.Add:
                {
                    _firstValue += _secondValue;
                    ShownValue = 0;
                    break;
                }
                case Operation.Subtract:
                {
                    _firstValue -= _secondValue;
                    ShownValue = 0;
                    break;
                }
                case Operation.Multiply:
                {
                    _firstValue *= _secondValue;
                    ShownValue = 0;
                    break;
                }
                case Operation.Divide:
                {
                    _firstValue /= _secondValue;
                    ShownValue = 0;
                    break;
                }
            }

            if (operation == Operation.Result)
            {
                ShownValue = _firstValue;
                _operation = Operation.Add;
                _firstValue = 0;
            }
            else
            {
                _operation = operation;
            }
        }
    }
}
