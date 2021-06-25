using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangman.Models
{
  public class Game : PersistableModel
  {
    private const int MaxLives = 7;
    private readonly string _answer;
    private readonly HashSet<Guess> _guesses;

    public Game(string answer, HashSet<Guess> previousGuesses, string gameStateFilePath) 
      : base(gameStateFilePath)
    {
      _answer = answer.ToUpper();
      _guesses = previousGuesses;
    }

    public string GetAnswer()
    {
      return _answer;
    }
    
    public bool IsWon()
    {
      return _answer.All(character => _guesses.Any(g => g.Equals(character)));
    }

    public bool IsLost()
    {
      return !IsWon() && GetLivesRemaining() == 0;
    }

    public bool IsInPlay()
    {
      return !IsWon() && !IsLost();
    }

    public bool IsDuplicateGuess(Guess guess)
    {
      return _guesses.Contains(guess);
    }

    public void SubmitGuess(Guess guess)
    {
      _guesses.Add(guess);
      
      Persist();
    }

    public List<Guess> GetIncorrectGuesses()
    {
      return _guesses.Where(g => !_answer.Contains(g.GetChar())).ToList();
    }

    public int GetLivesRemaining()
    {
      return MaxLives - GetIncorrectGuesses().Count;
    }

    public HashSet<Guess> GetGuesses()
    {
      return _guesses;
    }

    protected override string GetState()
    {
      var stringBuilder = new StringBuilder();

      stringBuilder.AppendLine(_answer);

      stringBuilder.AppendJoin(',', _guesses);

      return stringBuilder.ToString();
    }
  }
}