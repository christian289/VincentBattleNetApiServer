using VincentBattleNetApiServer.Core.DTOs.Base;

namespace VincentBattleNetApiServer.Core.DTOs.Res.CharacterAchievementsAPI;

public record ResCharacterAchievementsSummary : AbsApi
{
    [JsonPropertyName("total_quantity")]
    public int TotalQuantity { get; set; }

    [JsonPropertyName("total_points")]
    public int TotalPoints { get; set; }

    [JsonPropertyName("achievements")]
    public InnerAchievement[]? Achievements { get; set; }

    [JsonPropertyName("category_progress")]
    public InnerCategoryProgress[]? CategoryProgress { get; set; }

    [JsonPropertyName("recent_events")]
    public InnerRecentEvents[]? RecentEvents { get; set; }

    [JsonPropertyName("character")]
    public InnerCharacter? Character { get; set; }

    [JsonPropertyName("statistics")]
    public LinkObj? Statistics { get; set; }

    public record InnerAchievement
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("achievement")]
        public BaseValue? Achievement { get; set; }

        [JsonPropertyName("criteria")]
        public InnerCriteria? Criteria { get; set; }

        [JsonPropertyName("completed_timestamp")]
        public long CompletedTimestamp { get; set; }

        public record InnerCriteria
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("is_completed")]
            public bool IsCompleted { get; set; }

            [JsonPropertyName("child_criteria")]
            public InnerCriteria[]? ChildCriteria { get; set; }

            [JsonPropertyName("amount")]
            public long? Amount { get; set; } // 가끔씩 int 값을 초과하는 범위의 값이 들어있다.
        }
    }

    public record InnerCategoryProgress
    {
        [JsonPropertyName("category")]
        public BaseValue? Category { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("points")]
        public int Points { get; set; }
    }
    
    public record InnerRecentEvents
    {
        [JsonPropertyName("achievement")]
        public BaseValue? Achievement { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    }

    public record InnerCharacter : BaseValue
    {
        [JsonPropertyName("realm")]
        public BaseSlugValue? Realm { get; set; }
    }
}
