using VincentBattleNetApiServer.Core.DTOs.Base;

namespace VincentBattleNetApiServer.Core.DTOs.Res.AccountProfileAPI;

public record ResAccountProfileSummary : AbsApi
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("wow_accounts")]
    public InnerWowAccounts[]? WowAccounts { get; set; }

    [JsonPropertyName("collections")]
    public LinkObj? Collections { get; set; }

    public record InnerWowAccounts
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("characters")]
        public InnerCharacter[]? Characters { get; set; }

        public record InnerCharacter
        {
            [JsonPropertyName("character")]
            public LinkObj? Character { get; set; }

            [JsonPropertyName("protected_character")]
            public LinkObj? ProtectedCharacter { get; set; }

            [JsonPropertyName("name")]
            public string? Name { get; set; }

            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("realm")]
            public BaseSlugValue? Realm { get; set; }

            [JsonPropertyName("playable_class")]
            public BaseValue? PlayableClass { get; set; }

            [JsonPropertyName("playable_race")]
            public BaseValue? PlayableRace { get; set; }

            [JsonPropertyName("gender")]
            public InnerTypeName? Gender { get; set; }

            [JsonPropertyName("faction")]
            public InnerTypeName? Faction { get; set; }

            [JsonPropertyName("level")]
            public int Level { get; set; }

            public record InnerTypeName
            {
                [JsonPropertyName("type")]
                public string? Type { get; set; }

                [JsonPropertyName("name")]
                public string? Name { get; set; }
            }
        }
    }
}
