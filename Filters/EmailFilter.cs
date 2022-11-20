namespace LawRoomApi.Filters;

public class EmailFilterAttribute : Attribute
{
    public bool IsValid ()
    {
        return true;
    }
}