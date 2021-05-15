using System;

namespace NET5Academy.Shared.Domain
{
    public abstract class OkEntity
    {
        private int? _requestedHashCode;
        private int _Id;

        public virtual int Id
        {
            get => _Id;
            set => _Id = value;
        }

        public bool IsTransient()
        {
            return this.Id == default(Int32);
        }

        public override int GetHashCode()
        {
            if (IsTransient())
                return base.GetHashCode();

            if (!_requestedHashCode.HasValue)
                _requestedHashCode = this.Id.GetHashCode() ^ 31; // XOR for random distribution

            return _requestedHashCode.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is OkEntity))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            OkEntity item = (OkEntity)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id == this.Id;
        }

        public static bool operator ==(OkEntity left, OkEntity right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(OkEntity left, OkEntity right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }
    }
}
