using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace FinanceApp.Data.Service
{
    // small helper to store complex objects in session
    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? GetObject<T>(this ISession session)
        {
            var str = session.GetString(typeof(T).FullName!);
            if (string.IsNullOrEmpty(str)) return default;
            return JsonSerializer.Deserialize<T>(str);
        }

        public static T? GetObject<T>(this ISession session, string key)
        {
            var str = session.GetString(key);
            if (string.IsNullOrEmpty(str)) return default;
            return JsonSerializer.Deserialize<T>(str);
        }
    }
}