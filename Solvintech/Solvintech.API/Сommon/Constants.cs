namespace Solvintech.API.Сommon
{
    public static class Constants
    {
        public static class Configuration
        {
            public const string SecretKey = "SecretKey";
            public const string Authorization = "Authorization";
            public const string Bearer = "Bearer";
            public const string JWT = "JWT";
        }

        public static class Xml
        {
            public const string Valute = "//Valute";
        }

        public static class ConfigurationErrors
        {
            public const string SecretKetNotFound = "SecretKey isn't found";
            public const string ConnectionStringNotFound = $"Connection string '{ConnectionString.Name}' isn't found.";
        }

        public static class ConnectionString
        {
            public const string Name = "MsSqlServerConnection";
        }

        public static class Account
        {
            public const string SignUpFailed = "User signup failed.";
            public const string SignUpSuccess = "User sign up sucessfull.";
            public const string UserExists = "User is already exists.";
            public const string SignInFailed = "User sign in failed.";
            public const string SignInSuccess = "User sign in successfull.";
            public const string InvalidEmail = "User email is invalid.";
            public const string InvalidPassword = "User password is invalid.";
            public const string LogoutFailed = "User log out failed.";
            public const string LogoutSuccess = "User log out successfull.";
        }

        public static class Token
        {
            public const string InvalidHeaderAuthorization = "Invalid header 'Authorization'.";
            public const string InvalidAccessToken = "Invalid access token.";
            public const string UpdateSuccess = "Access token updated successfull.";
        }

        public static class Quotation
        {
            public static string GetQuotationsUrl(DateTime date)
            {
                return $"http://www.cbr.ru/scripts/XML_daily.asp?date_req={date.ToString("MM/dd/yyyy")}";
            }
        }

        public static class String
        {
            public static string IsEmpty(string input)
            {
                return $"{nameof(input)} cannot be empty";
            }
        }
    }
}
