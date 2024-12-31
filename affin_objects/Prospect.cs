namespace affin_objects
{
    public class Prospect
    {
        public int ProspectoId { get; set; }
        public int TipoProspecto { get; set; }
        public string? RazonSocial { get; set; }
        public string? RFC { get; set; }
        public string? DireccionFiscal { get; set; }
        public string? TelefonoCelular { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? NumeroCuentaBancaria { get; set; }
        public string? AreasOperacion { get; set; }
        public string? CultivoPrincipal { get; set; }
        public string? UbicacionPlantios { get; set; }
        public int TieneSeguro { get; set; }
        public int StatusProspecto { get; set; }
        public string? UltimaActualizacion { get; set; }
    }
}
