namespace SchoolDatabaseManager.Classes
{
    public class Group
    {
        public int Id;
        public string Name;

        public Group(string name, int id)
        {
            Name = name;
            Id = id;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
