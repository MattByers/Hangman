using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hangman.Models;

namespace Hangman.Views
{
  public class View
  {
    private readonly Game _game;

    public View(Game game)
    {
      _game = game;
    }

    public void DisplayWelcomeMessage()
    {
      Console.WriteLine("Welcome to Hangman!");
    }

    public void DisplayInstructions()
    {
      Console.WriteLine("Try to guess the word! You start with 7 lives. Guess a character each turn, incorrect guesses lose you a life!");
    }

    public void DisplayGameState()
    {
      int guessesRemaining = _game.GetLivesRemaining();
      string answer = _game.GetAnswer();
      HashSet<Guess> guessesSoFar = _game.GetGuesses();
      List<Guess> incorrectGuesses = _game.GetIncorrectGuesses();

      var stringBuilder = new StringBuilder();

      foreach (var character in answer)
      {
        stringBuilder.Append(guessesSoFar.Any(g => g.Equals(character)) ? character : '_');
      }

      stringBuilder.Append($" | You have {guessesRemaining} lives left");

      if (incorrectGuesses.Any())
      {
        var incorrectGuessesString = string.Join(", ", incorrectGuesses.Select(g => g.GetChar()));
        
        stringBuilder.Append($" | Incorrect guesses so far: {incorrectGuessesString}");
      }

      Console.WriteLine(stringBuilder.ToString());
    }

    public Guess AskForGuess()
    {
      Console.WriteLine("Guess a character: ");
      
      return new Guess(Console.ReadKey(true).KeyChar);
    }

    public void DisplayInvalidGuess()
    {
      Console.WriteLine("That guess is invalid. Please enter a single letter");
    }

    public void DisplayGameOutcome()
    {
      if (_game.IsWon())
      {
        Console.WriteLine("You won!");
      }

      if (_game.IsLost())
      {
        Console.WriteLine("You lost :(");
        Console.WriteLine($"The word was {_game.GetAnswer()}");
      }
    }

    public void DisplayDuplicateGuess()
    {
      Console.WriteLine("You've guessed that already!");
    }

    public void DisplayExitMessage()
    {
      Console.WriteLine("Press any key to exit...");

      Console.ReadKey();
      
      Environment.Exit(0);
    }
  }
}