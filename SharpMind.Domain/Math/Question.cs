using System;

namespace SharpMind.Domain.Math
{
    public partial class GameMath
    {
        [Serializable]
        public class Question
        {
            private Question(int leftOperand, int rightOperand, MathOperation operation)
            {
                switch (operation)
                {
                    case MathOperation.Add:
                        CorrectAnswer = leftOperand + rightOperand;
                        QuestionText = $"{leftOperand} + {rightOperand}";
                        break;
                    case MathOperation.Subtract:
                        if (leftOperand < rightOperand)
                        {
                            var temp = leftOperand;
                            leftOperand = rightOperand;
                            rightOperand = temp;
                        }
                        CorrectAnswer = leftOperand - rightOperand;
                        QuestionText = $"{leftOperand} - {rightOperand}";
                        break;
                    case MathOperation.Multiply:
                        CorrectAnswer = leftOperand * rightOperand;
                        QuestionText = $"{leftOperand} x {rightOperand}";
                        break;
                        //case MathOperation.Divide:
                        //    CorrectAnswer = leftOperand + rightOperand;
                        //    QuestionText = $"{leftOperand} / {rightOperand}";
                        //    break;
                }
            }

            public int CorrectAnswer { get; }

            public string QuestionText { get; }

            public int? Answer { get; set; }

            public static class QuestionGenerator
            {
                private static readonly Random Rand = new Random((int)DateTime.UtcNow.Ticks);

                public static Question CreateQuestion()
                {
                    Array operations = Enum.GetValues(typeof(MathOperation));
                    MathOperation operation = (MathOperation)operations.GetValue(Rand.Next(operations.Length));

                    int maxOperandValue = operation != MathOperation.Multiply ? 99 : 35;
                    int minOperandValue = 2;
                    var leftOperand = Rand.Next(minOperandValue, maxOperandValue);
                    var rightOperand = Rand.Next(minOperandValue, maxOperandValue);

                    return new Question(leftOperand, rightOperand, operation);
                }
            }

            public enum MathOperation
            {
                Add = 1,

                Subtract = 2,

                Multiply = 3,

                //Divide = 4
            }
        }
    }
}
