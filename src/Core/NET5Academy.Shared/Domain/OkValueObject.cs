using System;
using System.Collections.Generic;
using System.Linq;

namespace NET5Academy.Shared.Domain
{
    public abstract class OkValueObject
    {
        protected static bool EqualOperator(OkValueObject left, OkValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
                return false;

            return ReferenceEquals(left, null) || left.Equals(right);
        }

        protected static bool NotEqualOperator(OkValueObject left, OkValueObject right)
        {
            return !(EqualOperator(left, right));
        }

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var other = (OkValueObject)obj;
            return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
             .Select(x => x != null ? x.GetHashCode() : 0)
             .Aggregate((x, y) => x ^ y);
        }

        public OkValueObject GetCopy()
        {
            return this.MemberwiseClone() as OkValueObject;
        }
    }
}
