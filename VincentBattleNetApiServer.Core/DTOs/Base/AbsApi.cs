namespace VincentBattleNetApiServer.Core.DTOs.Base;

// 추상 클래스나 추상 레코드는 Json 객체로 변환할 수 없기 때문에 abstract를 사용하면 안된다.
public record AbsApi
{
    [JsonPropertyName("_links")]
    public InnerLinks? Links { get; set; }

    public record InnerLinks
    {
        [JsonPropertyName("self")]
        public LinkObj? Self { get; set; }

        [JsonPropertyName("user")]
        public LinkObj? User { get; set; }

        [JsonPropertyName("profile")]
        public LinkObj? Profile { get; set; }
    }
}
