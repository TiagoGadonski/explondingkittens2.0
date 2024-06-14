public class Card
{
    public string Type { get; set; }
    public string CatName { get; set; }

    public Card(string type, string catName = null)
    {
        Type = type;
        CatName = catName;
    }

    public string DisplayName
    {
        get
        {
            return Type switch
            {
                "Exploding Kitten" => "Exploding Kitten",
                "Defuse" => "Defuse",
                "Attack" => "Attack",
                "Skip" => "Skip",
                "Favor" => "Favor",
                "Shuffle" => "Shuffle",
                "See the Future" => "See the Future",
                "Nope" => "Nope",
                "Tacocat" => "Tacocat",
                "Hairy Potato Cat" => "Hairy Potato Cat",
                "Rainbow Ralphing Cat" => "Rainbow Ralphing Cat",
                "Beard Cat" => "Beard Cat",
                "Cattermelon" => "Cattermelon",
                _ => Type
            };
        }
    }
}
