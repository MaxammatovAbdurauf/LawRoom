namespace LawRoomApi.Helpers;

public static class GenerateRandomString
{
    public static string RandomString() => Guid.NewGuid().ToString("N");
}