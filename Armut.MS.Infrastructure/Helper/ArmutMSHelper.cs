using System;
using MongoDB.Bson;

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
        bool result = false;

        list.ToList().ForEach(x =>
        {
           if (x.Contains(username))
              {
                 result = true;
              }
        });

        return result;
    }

    public static ObjectId BsonParserId(string Id) => MongoDB.Bson.ObjectId.Parse(Id);
}

