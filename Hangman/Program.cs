using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Hangman game");
            Console.WriteLine("---------------------------");
            string secretWord = RandomWordGenerator();
           // Console.WriteLine(secretWord);
            GuessWord(secretWord);
            Console.ReadLine();
        }

        private static void GuessWord(string secretWord)
        {
            bool end = false;
            int GuessCount = 0;
            char[] answer = new char[secretWord.Length];
            List<char> correctLetter = new List<char>();
            StringBuilder incorrectLetter = new StringBuilder();
            TransformToDash(secretWord, answer);
            while (GuessCount < 10 && !end)
            {
                char guessLetter=' ';
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
               guessLetter=Char.ToUpper(guessLetter);
                
                if (result)
                {
                    if ((guessLetter >= 'A' && guessLetter <= 'Z') || guessLetter=='Ä' || guessLetter == 'Å' || guessLetter == 'Ö')
                    {
                        if (!correctLetter.Contains(guessLetter) && !incorrectLetter.ToString().Contains(guessLetter))
                        {
                            if (secretWord.Contains(guessLetter))
                            {
                                for (int i = 0; i < secretWord.Length; i++)
                                {
                                    if (secretWord[i] == guessLetter)
                                    {
                                        answer[i] = guessLetter;
                                    }
                                }
                                correctLetter.Add(guessLetter);
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
                            else
                           {
                                try
                                {
                                    incorrectLetter.Append(" "+guessLetter);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            GuessCount++;

                        }
                        else
                        {
                            Console.WriteLine("You have entered the letter before.Please guess new letter");
                            // System.Threading.Thread.Sleep(2000);
                        }
                    }
                    else 
                    {
                        Console.WriteLine("Please enter a vaild letter.");
                        //System.Threading.Thread.Sleep(2000);
                    }
                }
                else
                {
                    if (input.ToUpper() == secretWord)
                    {
                        for (int i = 0; i < input.Length; i++)
                        {
                            answer[i] = input.ToUpper()[i];
                        }
                        end = true;
                    }

                    else
                    {
                        Console.WriteLine("You didn't get the correct word");
                       // System.Threading.Thread.Sleep(2000);
                    }

                    GuessCount++;

                }
               Console.WriteLine();
                //Console.Clear();
                WinnerCheker(end,incorrectLetter,GuessCount,secretWord,answer);
            }
        }

        private static string RandomWordGenerator()
        {
            Random ran = new Random();
            string secretWord = "";
            /* string[] HangmanWords = {
                 "ADVOKAT","AFFÄR","BALKONG","BERÄTTELSE","BARNSKÖTARE","CITRON","DISKMASKIN","FLYGPLATS","FOTBOLLSPELARE",
                 "JÄRNVÄGSSTATION","LÄGENHET","SOMMAR","SPEGEL","ÄPPLE","TREVLIG","MODIG","DATOR","UNDERBAR","KRAFTIG",
                 "ENSAM","DÅLIG","SPÄNNANDE","INTRESSANT"
                  };*/
            string[] HangmanWords =(File.ReadAllText("../../../wordList.txt")).Split(",");
            secretWord = HangmanWords[ran.Next(0, HangmanWords.Length)];
            //try
            //{
            //    WebClient wc = new WebClient();
            //    string wordList = wc.DownloadString("https://www.mit.edu/~ecprice/wordlist.10000");
            //   
            //} string[] words = wordList.Split('\n');
            //    word=words[ran.Next(0, words.Length)];
            //catch (WebException ex)
            //{
            //    Console.WriteLine("The website can't be loaded");
            //}
            //catch (ArgumentNullException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //catch(NotSupportedException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            
            return secretWord;

        }
        private static char[] TransformToDash(string word,char[] answer)
        {
            for (int i = 0; i < word.Length; i++)
            {
                answer[i] = '_';
                Console.Write(answer[i]+" ");
            }
            return answer;
        }
        private static void display(char[]answer)
        {
            foreach (char item in answer)
            {
                Console.Write(item+" ");
            }
        }
        private static void WinnerCheker(bool end,StringBuilder incorrectLetter,int GuessCount,string secretWord,char[] answer)
        {
            if(end)
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
                    Console.WriteLine((10-GuessCount) +" attempt is left.");
                    Console.WriteLine("Attempted incorrect letter: " + incorrectLetter);
                    display(answer);
                }

            }
        }
 
    }
}
    

