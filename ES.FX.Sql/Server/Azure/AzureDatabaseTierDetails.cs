namespace ES.FX.Sql.Server.Azure
{
    public class AzureDatabaseTierDetails
    {
        public string Edition { get; set; } = "standard";
        public string ServiceObjective { get; set; } = "S0";
        public string ElasticPool { get; set; }
    }
}