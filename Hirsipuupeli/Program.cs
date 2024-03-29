﻿// To run this program, you need to add a reference to words\words_fi.txt
// dotnet run -- -f words\words_fi.txt

using System;
using System.IO;
using System.Linq;
using CommandLine;
using TurboReader;
using System.Collections.Generic;

namespace Hangman
{
    /// <summary>
    /// The main program class that runs the hangman game.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The entry point of the program.
        /// </summary>
        /// <param name="args"> Command line arguments for the program.</param>
        static void Main(string[] args)
        {
            // Parse the command line options and pass them to the RunOptions method
            Parser.Default.ParseArguments<Options>(args).WithParsed(RunOptions);
        }

        /// <summary>
        /// Runs the hangman game using the options provided.
        /// </summary>
        /// <param name="opts">The options for the hangman game.</param>

        private static void RunOptions(Options opts)
        {
            TurboReader.TurboReader tr = new TurboReader.TurboReader();
            // Get list of words from file
            string[] words;
            try
            {
                words = File.ReadAllLines(opts.FilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading word list file: " + ex.Message);
                return;
            }

            // Randomly select a word from the list
            string wordToGuess = words[new Random().Next(words.Length)];

            // Initialize variables
            string wordSoFar = new string('_', wordToGuess.Length);
            int lives = 5;
            bool wordGuessed = false;
            List<char> guessedLetters = new List<char>();
            while (!wordGuessed && lives > 0)
            {
                // Print current state of word
                Console.WriteLine(wordSoFar);

                // Get letter from user
                Console.Write("Guess a letter: ");
                char letter = Console.ReadLine()[0];
                letter = Char.ToLower(letter);
                // Check if letter is valid
                bool letterChecked = tr.CheckIfOnlyLetters(letter.ToString());
                if (letterChecked == false)
                {
                    Console.WriteLine("You can only use letters!");
                    continue;
                }
                
                if (guessedLetters.Contains(letter))
                {
                    Console.WriteLine("You have already guessed this letter!");
                    continue;
                }

                // Check if letter is in word
                bool letterInWord = false;
                for (int i = 0; i < wordToGuess.Length; i++)
                {
                    if (wordToGuess[i] == letter)
                    {
                        wordSoFar = wordSoFar.Remove(i, 1).Insert(i, letter.ToString());
                        letterInWord = true;
                    }
                }

                if (!letterInWord)
                {
                    lives--;
                    Console.WriteLine(
                        $"Sorry, that letter is not in the word. Lives remaining: {lives}"
                    );
                    guessedLetters.Add(letter);
                }

                // Print already guessed letters
                Console.Write("Letters already guessed: ");
                foreach (char c in guessedLetters)
                {
                    Console.Write(c + " ");
                }
                Console.WriteLine();

                // Check if word is guessed
                if (!wordSoFar.Contains("_"))
                {
                    wordGuessed = true;
                    Console.WriteLine("Congratulations! You guessed the word!");
                    Console.WriteLine("The word was: " + wordToGuess);
                }
            }

            if (!wordGuessed)
            {
                Console.WriteLine("Sorry, you ran out of lives. The word was: " + wordToGuess);
            }
        }
    }

    /// <summary>
    /// Class for storing the options for the program.
    /// </summary>
    class Options
    {
        /// <summary>
        /// File path option, required and contains the path to a file containing a list of words, one per line.
        /// </summary>
        [Option(
            'f',
            "file",
            Required = true,
            HelpText = "The path to a file containing a list of words, one per line."
        )]
        public string? FilePath { get; set; }
    }
}
