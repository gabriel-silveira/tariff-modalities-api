namespace CTCEA_Tariff_Modalities.Entities;

public class ImmediateBilling : BaseEntity
{
    public required string MatriculaAeronave { get; set; }
    public required string CiaIcao { get; set; }
    public required string Nacionalidade { get; set; }
    public string ?IcaoLocalidade { get; set; }
    public required DateTime DataInclusao { get; set; }
    public DateTime ?DataSaida { get; set; }
}
