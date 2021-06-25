#nullable enable
using System;

namespace Hangman.Models
{
  public class Guess : IEquatable<Guess>
  {
    private readonly char _guessed;

    public Guess(char guessed)
    {
      _guessed = char.ToUpper(guessed);
    }

    public bool IsValid()
    {
      return char.IsLetter(_guessed);
    }

    public char GetChar()
    {
      return _guessed;
    }

    public bool Equals(char c)
    {
      return _guessed == char.ToUpper(c);
    }

    public bool Equals(Guess? other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return _guessed == other._guessed;
    }

    public override bool Equals(object? obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((Guess) obj);
    }

    public override int GetHashCode()
    {
      return _guessed.GetHashCode();
    }

    public override string ToString()
    {
      return _guessed.ToString();
    }
  }
}