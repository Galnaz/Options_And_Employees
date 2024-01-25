using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5.Options
{
    

    public class Option
    {

        private readonly object _value;

        public Option() { }
        public Option(object value) 
        {
            _value = value;
        }

        public bool HasValue => _value != null;
        public object Value => HasValue ? _value : throw new InvalidOperationException();

        public static Option<T> Empty<T>() => new Option<T>(default);

        public static Option<T> FromValue<T>(T value) => new Option<T> (value);

        public override bool Equals(object obj)
        {
            return obj is Option;
        }

        public override int GetHashCode()
        {
            return Value?.GetHashCode() ?? 0;
        }

        internal object Select(Func<object, object> p)
        {
            return HasValue ? p(Value) : Empty<object>();
        }

        internal object ValueOr(Func<object> p)
        {
            return Value ?? p();
        }
    }

    public struct Option<T> 
    {
        private readonly T _value;

        public Option() 
        {
            _value = default;
        }

        public Option(T value)
        {
            _value = value;
        }

        public bool HasValue => _value != null;

        public T Value => HasValue ? _value : throw new InvalidOperationException() ;

        public override bool Equals(object obj)
        {
            return obj is Option<T> other 
                && Equals(_value, other._value);
        }

        public override int GetHashCode()
        {
            return _value?.GetHashCode() ?? 0;
        }

        public static bool operator ==(Option<T> left, Option<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Option<T> left, Option<T> right)
        {
            return !left.Equals(right);
        }

        public Option<TNew> Select<TNew>(Func<T, TNew> selector)
        {
            var newVal = HasValue ? selector(_value) : default;
            return new Option<TNew>(newVal);
        }

        public T ValueOr(Func<T> defaultValueProvider)
        {
            return _value ?? defaultValueProvider();
        }
    }
}
