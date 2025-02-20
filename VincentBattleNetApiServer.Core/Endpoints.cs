namespace VincentBattleNetApiServer.Core;

public static class Endpoints
{
    #region Realm API
    public const string RealmsIndex = "/data/wow/realm/index";
    public const string Realm = "/data/wow/realm/{0}"; // 0: realmSlug
    public const string RealmSearch = "/data/wow/search/realm";
    #endregion

    #region Toy API
    public const string ToyIndex = "/data/wow/toy/index";
    public const string Toy = "/data/wow/toy/{0}"; // 0: toyId
    #endregion

    #region WoW Token API
    public const string WowTokenIndex = "/data/wow/token/index";
    #endregion

    #region Account Profile API
    public const string AccountProfileSummary = "/profile/user/wow";
    public const string AccountCollectionsIndex = "/profile/user/wow/collections";
    public const string AccountToysCollectionSummary = "/profile/user/wow/collections/toys";
    #endregion

    #region Character Achievements API
    public const string CharacterAchievementsSummary = "/profile/wow/character/{0}/{1}/achievements"; // 0: realmSlug, 1: characterName
    #endregion
}
