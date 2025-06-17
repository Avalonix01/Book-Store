namespace BookStore.API.Endpoints;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class V1
    {
        private const string Version = "v1";
        private const string Base = $"{ApiBase}/{Version}";

        public static class Authors
        {
            private const string Base = $"{V1.Base}/authors";
            public const string GetAll = Base;
            public const string GetById = $"{Base}/{{id:guid}}";
            public const string Create = Base;
            public const string Update = $"{Base}/{{id:guid}}";
            public const string Delete = $"{Base}/{{id:guid}}";
        }

        public static class Books
        {
            private const string Base = $"{V1.Base}/books";
            public const string GetAll = Base;
            public const string GetById = $"{Base}/{{id:guid}}";
            public const string Create = Base;
            public const string Update = $"{Base}/{{id:guid}}";
            public const string Delete = $"{Base}/{{id:guid}}";
        }

        public static class Auth
        {
            private const string Base = $"{V1.Base}/auth";
            public const string Login = $"{Base}/login";
            public const string Register = $"{Base}/register";
        }
    }
}