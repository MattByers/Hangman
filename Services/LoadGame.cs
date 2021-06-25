using System;
using System.Collections.Generic;
using System.IO;
using Hangman.Models;

namespace Hangman.Services
{
  public class LoadGame
  {
    private readonly string _gameStateFilePath;
    
    public Game Game { get; private set; }

    public LoadGame(string gameStateFilePath)
    {
      _gameStateFilePath = gameStateFilePath;
    }
    
    public bool Run()
    {
      if (!File.Exists(_gameStateFilePath))
      {
        return false;
      }

      using StreamReader gameStateReader = new StreamReader(_gameStateFilePath);
      string gameAnswer = gameStateReader.ReadLine();
      string guessesString = gameStateReader.ReadLine();

      if (string.IsNullOrEmpty(gameAnswer))
      {
        return false;
      }

      var guesses = new HashSet<Guess>();
      
      if (!string.IsNullOrEmpty(guessesString))
      {
        foreach (var guessString in guessesString.Split(','))
        {
          if (char.TryParse(guessString, out char guessChar))
          {
            guesses.Add(new Guess(guessChar));
          }
        }
      }

      Game = new Game(gameAnswer, guesses, _gameStateFilePath);
      
      return true;
    }
  }
}