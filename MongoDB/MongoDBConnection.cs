using MongoDB.Driver;
using SingularChatAPIs.utils;
using System.Diagnostics;

namespace SingularChatAPIs.BD;
public static class MongoDBConnection {

    private static MongoClient mongoClient;
    private static IMongoDatabase database;

    static MongoDBConnection() {
        start();
    }
    public static IMongoDatabase getMongoDatabase() {
        return database;
    }

    public static void start() {
        if (mongoClient == null) {
            var stopwatch = Stopwatch.StartNew();
            Console.WriteLine("[MongoDBConnection:start] Init MongoConnection.");
            mongoClient = new MongoClient(AppSettings.appSetting["MongoDBSettings:ConnectionString"]);
            database = mongoClient.GetDatabase(AppSettings.appSetting["MongoDBSettings:Master_DatabaseName"]);
            stopwatch.Stop();
            Console.WriteLine($"[MongoDBConnection:start] Final MongoConnection. - {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}

