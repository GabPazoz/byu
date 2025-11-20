using System;

class Program
{
    static void Main(string[] args)
    {
        // Create videos
        Video v1 = new Video("Learn C# in 10 Minutes", "Tech Academy", 600);
        Video v2 = new Video("The Book of Mormon Story", "Church Channel", 480);
        Video v3 = new Video("BYU-Idaho Campus Tour", "Rexburg Life", 300);

        // Add comments
        v1.AddComment(new Comment("John", "Great explanation!"));
        v1.AddComment(new Comment("Maria", "Helped me a lot, thanks!"));
        v1.AddComment(new Comment("Luke", "Please make more videos!"));

        v2.AddComment(new Comment("Ana", "Beautiful message."));
        v2.AddComment(new Comment("Peter", "Very spiritual."));
        v2.AddComment(new Comment("Sarah", "I loved it!"));

        v3.AddComment(new Comment("Jake", "Looks amazing!"));
        v3.AddComment(new Comment("Emily", "I want to study there!"));
        v3.AddComment(new Comment("Paul", "Great tour."));

        // Display
        v1.Display();
        v2.Display();
        v3.Display();
    }
}
