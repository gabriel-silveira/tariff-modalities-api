namespace CTCEA_Modalidade_Tarifaria.Entities;

public class CobrancaAVista : BaseEntity
{
    public required string MatriculaAeronave { get; set; }
    public required string CiaIcao { get; set; }
    public required string Nacionalidade { get; set; }
    public string ?IcaoLocalidade { get; set; }
    public required DateTime DataInclusao { get; set; }
    public DateTime ?DataSaida { get; set; }
}
