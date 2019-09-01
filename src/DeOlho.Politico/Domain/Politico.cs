using System;
using System.Collections.Generic;
using System.Linq;
using DeOlho.SeedWork.Domain;

namespace DeOlho.Politico.Domain
{
    public class Politico : Entity
    {
        public long CPF { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CidadeNascimento { get; set; }
        public string UFNascimento { get; set; }
        public long SexoId { get; set; }
        public Sexo Sexo { get; set; }
        public long GrauInstrucaoId { get; set; }
        public GrauInstrucao GrauInstrucao { get; set; }
        public long OcupacaoId { get; set; }
        public Ocupacao Ocupacao { get; set; }
        public DateTime DataInformacao { get; set; }
        public List<Mandato> Mandatos { get; set; } = new List<Mandato>();
        public string TermoPesquisa { get; set; }

        public string BuildTermoPesquisa()
        {
            return $"[ {Nome} {Apelido} ] {string.Join(' ', Mandatos.Select(_ => _.BuildTermoPesquisa()))}";
        }
    }
}