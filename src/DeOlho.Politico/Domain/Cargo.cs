using DeOlho.SeedWork.Domain;

namespace DeOlho.Politico.Domain
{
    public class Cargo : Entity
    {
        public string Nome { get; set; }
        public long AbrangenciaId { get; set; }
        public string Abrangencia { get; set; }
    }
}