using DeOlho.SeedWork.Domain;

namespace DeOlho.Politico.Domain
{
    public class Mandato : Entity
    {
        public long PoliticoId { get; set; }
        public Politico Politico { get; set; }
        public long EleicaoId { get; set; }
        public Eleicao Eleicao { get; set; }
        public long PartidoId { get; set; }
        public Partido Partido { get; set; }
        public long CargoId { get; set; }
        public Cargo Cargo { get; set; }
        public bool Suplente { get; set; }
    }
}