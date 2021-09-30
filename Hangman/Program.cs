using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;

namespace Hangman
{
    class Program
    {
        static List<char> correctLetter = new List<char>();
        static bool end = false;
        static StringBuilder incorrectLetter = new StringBuilder();
        static string secretWord = "";
        static char[] answer;
        static int GuessCount = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Hangman game");
            Console.WriteLine("---------------------------");
            secretWord = RandomWordGenerator();
            //Console.WriteLine(secretWord);
            answer = new char[secretWord.Length];
            GuessWord(secretWord);
            Console.ReadLine();
        }
        private static void GuessWord(string secretWord)
        {
            TransformToDash(secretWord, answer);
            while (GuessCount < 10 && !end)
            {
                char guessLetter =' ';
                string input = "";
                bool result = false;
                Console.WriteLine();
                try
                {
                    result = Char.TryParse(input = Console.ReadLine(), out guessLetter);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (OutOfMemoryException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (System.IO.IOException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                guessLetter = Char.ToUpper(guessLetter);
                if (result)
                {
                    GuessWordByLetter(secretWord, guessLetter);
                }
                else
                {
                    GuessWordByWord(secretWord, input);
                }
                Console.WriteLine();
                WinnerCheker(GuessCount, secretWord);
            }
        }
        private static void GuessWordByLetter(string secretWord, char guessLetter)
        {
            if (CheckLetterRepeated(secretWord, guessLetter))
            {
                Console.WriteLine("You have entered the letter before.Please guess new letter");
            }
            else
            {
                SolveWord(secretWord, guessLetter);
                GuessCount++;
            }
        }
        private static void GuessWordByWord(string secretWord, string input)
        {
            if (input.ToUpper() == secretWord)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    answer[i] = (input.ToUpper())[i];
                }
            }

            else
            {
                Console.WriteLine("You didn't get the correct word");
            }
            GuessCount++;
            IsWordAnswered();
        }
        private static bool CheckLetterRepeated(string secretWord, char guessLetter)
        {
            bool repeatedLetter = false;
            if (correctLetter.Contains(guessLetter) || incorrectLetter.ToString().Contains(guessLetter))
            {
                repeatedLetter = true;
            }
            return repeatedLetter;
        }
        private static bool LetterExistInWordChecker(string secretWord, char guessLetter)
        {
            bool exist = false;
            if (secretWord.Contains(guessLetter))
            {
                exist = true;
            }
            return exist;
        }
        private static void SolveWord(string secretWord, char guessLetter)
        {
            if (LetterExistInWordChecker(secretWord, guessLetter))
            {
                for (int i = 0; i < secretWord.Length; i++)
                {
                    if (secretWord[i] == guessLetter)
                        answer[i] = guessLetter;
                }
                correctLetter.Add(guessLetter);
                IsWordAnswered();
            }
            else
            {
                try
                {
                    incorrectLetter.Append(" " + guessLetter);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private static void IsWordAnswered()
        {
            for (int i = 0; i < answer.Length; i++)
            {
                if (answer[i] == '_')
                {
                    end = false;
                    break;
                }
                end = true;
            }
        }
        private static string RandomWordGenerator()
        {
            Random ran = new Random();
            string secretWord = "";
            string[] HangmanWords = {
                 "ADVOKAT","AFFÄR","BALKONG","BERÄTTELSE","BARNSKÖTARE","CITRON","DISKMASKIN","FLYGPLATS","FOTBOLLSPELARE",
                 "ÄPPLE","LÄGENHET","SOMMAR","SPEGEL","ÄPPLE","TREVLIG","MODIG","DATOR","UNDERBAR","KRAFTIG",
                 "ENSAM","DÅLIG","SPÄNNANDE","INTRESSANT"
                  };
            //string[] HangmanWords =(File.ReadAllText("../../../wordList.txt")).Split(",");
            secretWord = HangmanWords[ran.Next(0, HangmanWords.Length)];
            return secretWord;

        }
        private static void TransformToDash(string word, char[] answer)
        {
            for (int i = 0; i < word.Length; i++)
            {
                answer[i] = '_';
                Console.Write(answer[i] + " ");
            }
        }
        private static void display(char[] answer)
        {
            foreach (char item in answer)
            {
                Console.Write(item + " ");
            }
        }
        private static void WinnerCheker(int GuessCount, string secretWord)
        {
            if (end)
            {
                Console.WriteLine();
                Console.WriteLine("You win");
                Console.Write("The word is ");
                display(answer);

            }
            else
            {
                if (GuessCount == 10)
                {
                    Console.WriteLine();
                    Console.WriteLine("You lose");
                    Console.WriteLine("The word is " + secretWord);
                }
                else
                {
                    Console.WriteLine((10 - GuessCount) + " attempt is left.");
                    Console.WriteLine("Attempted incorrect letter: " + incorrectLetter);
                    display(answer);
                }

            }
        }

    }
}



