using System.Collections.Generic;
using Hangman.Models;
using Hangman.Services;
using Hangman.Views;

namespace Hangman.Controllers
{
  public class HangmanController
  {
    private const string WordsFilename = "words_alpha.txt";
    private const string GameStateFilePath = "game_state.txt";

    private View _view;
    private Game _game;

    public void Run()
    {
      SetupGame();
      
      _view.DisplayGameState();

      if (_game.IsInPlay())
      {
        Guess guess = _view.AskForGuess();

        if (!guess.IsValid())
        {
          _view.DisplayInvalidGuess();
        } 
        else if (_game.IsDuplicateGuess(guess))
        {
          _view.DisplayDuplicateGuess();
        }
        else
        {
          _game.SubmitGuess(guess);
        }

        _view.DisplayGameState();
      }

      if (!_game.IsInPlay())
      {
        _view.DisplayGameOutcome();
      }
      
      _view.DisplayExitMessage();
    }

    private void SetupGame()
    {
      var loadGameService = new LoadGame(GameStateFilePath);
      if (loadGameService.Run())
      {
        _game = loadGameService.Game;
        _view = new View(_game);
      }
      else
      {
        var generateWordService = new GenerateWordFromFile(WordsFilename);
        var answer = generateWordService.Run();
        
        _game = new Game(answer, new HashSet<Guess>(), GameStateFilePath);
        _view = new View(_game);
        
        _view.DisplayWelcomeMessage();
        _view.DisplayInstructions();
      }
    }
  }
}