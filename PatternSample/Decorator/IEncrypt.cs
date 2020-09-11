namespace PatternSample.Decorator
{
    public interface IEncrypt
    {
        string Encrypt(string raw);
    }

    public class NullEncrypt : IEncrypt
    {
        public string Encrypt(string raw)
        {
            return raw;
        }
    }

    public class Md5Encrypt : IEncrypt
    {
        private readonly IEncrypt _inner;

        public Md5Encrypt(IEncrypt inner)
        {
            _inner = inner;
        }

        public string Encrypt(string raw)
        {
            var text = _inner.Encrypt(raw);

            return Md5(text);
        }

        protected virtual string Md5(string text) => text + "-md5";
    }

    public class Sha256Encrypt : IEncrypt
    {
        private readonly IEncrypt _inner;

        public Sha256Encrypt(IEncrypt inner)
        {
            _inner = inner;
        }

        public string Encrypt(string raw)
        {
            var text = _inner.Encrypt(raw);

            return Sha256(text);
        }

        protected virtual string Sha256(string text) => text + "-Sha256";
    }
}