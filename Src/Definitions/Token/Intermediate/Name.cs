namespace MathEval
{
    class Name : Token
    {
        public string _value;

        public Name(string value, int position) : base(position, value.Length) => _value = value;
    }

}