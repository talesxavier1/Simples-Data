namespace SingularChatAPIs.utils;
public static class AppSettings {

    public static IConfiguration appSetting { get; }

    static AppSettings() {
        appSetting = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
    }

}