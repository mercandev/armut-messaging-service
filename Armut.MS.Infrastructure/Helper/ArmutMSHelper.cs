using System;
namespace Armut.MS.Infrastructure.Helper;

public static class ArmutMSHelper
{
    public static string[] Add(string[] list, string username)
    {
        IEnumerable<string> bannedList = list.Append<string>(username);
        return bannedList.ToArray();
    }

    public static bool CheckBannedUser(string[] list, string username)
    {
        foreach (var users in list)
        {
            if(users.Contains(username))
            {
                return true;
            }
        }

        return false;
    }
}

