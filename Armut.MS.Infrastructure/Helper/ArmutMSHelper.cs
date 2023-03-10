using System;
using MongoDB.Bson;
using System.Linq;

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
        foreach (var _ in from users in list where users.Contains(username) select new { })
        {
            return true;
        }

        return false;
    }

    public static ObjectId BsonParserId(string Id) => MongoDB.Bson.ObjectId.Parse(Id);
}

