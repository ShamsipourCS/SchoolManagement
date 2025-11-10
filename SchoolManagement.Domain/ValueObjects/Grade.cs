using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.ValueObjects;

public sealed class Grade : IEquatable<Grade>
{
    public decimal Value { get; }

    public Grade(decimal value)
    {
        if (value < 0 || value > 100)
            throw new ArgumentOutOfRangeException(nameof(value), "Grade must be between 0 and 100");
        Value = value;
    }

    public override bool Equals(object? obj) => Equals(obj as Grade);
    public bool Equals(Grade? other) => other != null && Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value.ToString();
}
