using System;
using DeOlho.SeedWork.Domain;

namespace DeOlho.Politico.Domain
{
    public class Eleicao : Entity
    {
        public int Ano { get; set; }
        public string Descricao { get; set; }
        public long TipoId { get; set; }
        public TipoEleicao Tipo { get; set; }
        public DateTime Data { get; set; }
    }
}