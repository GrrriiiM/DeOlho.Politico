using DeOlho.SeedWork.Domain;

namespace DeOlho.Politico.Domain
{
    public class Partido : Entity
    {
        public string Sigla { get; set; }
        public string Nome { get; set; }
    }
}