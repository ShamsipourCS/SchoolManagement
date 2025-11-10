using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.ValueObjects;

public sealed class Email : IEquatable<Email>
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email is required", nameof(value));
        if (!value.Contains("@"))
            throw new ArgumentException("Email is invalid", nameof(value));

        Value = value.Trim();
    }

    public override bool Equals(object? obj) => Equals(obj as Email);
    public bool Equals(Email? other) => other != null && Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value;
}
