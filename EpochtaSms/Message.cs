namespace EpochtaSms
{
    public class Message
    {
        public string Phone { get; }
        public string Text { get; }

        public Message(string phone, string text)
        {
            Phone = phone;
            Text = text;
        }
    }
}
