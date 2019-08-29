using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DeOlho.ETL.tse_jus_br.Messages;
using DeOlho.EventBus.MediatR;
using DeOlho.Politico.Domain;
using DeOlho.SeedWork.Domain.Abstractions;
using DeOlho.SeedWork.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeOlho.Politico.Application.Events
{

    public class ChangePoliticoWhenPoliticoChangedIntegrationEventHandler : EventBusConsumerHandler<PoliticoChangedMessage>
    {
        readonly DeOlhoDbContext _deOlhoDbContext;
        //readonly IRepository<Domain.Politico> _politicoRepository;

        public ChangePoliticoWhenPoliticoChangedIntegrationEventHandler(
            DeOlhoDbContext deOlhoDbContext)
            //IRepository<Domain.Politico> politicoRepository)
        {
            _deOlhoDbContext = deOlhoDbContext;   
            //_politicoRepository = politicoRepository;
        }
        public async override Task<Unit> Handle(PoliticoChangedMessage message, CancellationToken cancellationToken)
        {
            var politicoRepository = _deOlhoDbContext.Set<Domain.Politico>();

            var politico = await politicoRepository
                .Include(_ => _.Sexo)
                .Include(_ => _.GrauInstrucao)
                .Include(_ => _.Ocupacao)
                .Include(_ => _.Mandatos)
                .ThenInclude(_ => _.Eleicao.Tipo)
                .Include(_ => _.Mandatos)
                .ThenInclude(_ => _.Partido)
                .Include(_ => _.Mandatos)
                .ThenInclude(_ => _.Cargo)
                .SingleOrDefaultAsync(_ => 
                    _.CPF == message.NR_CPF_CANDIDATO,
                    cancellationToken)
                ?? new Domain.Politico();

            var sexo = await _deOlhoDbContext.Set<Sexo>().SingleOrDefaultAsync(_ => _.Id == message.CD_GENERO)
                ?? new Sexo { Id = message.CD_GENERO, Descricao = message.DS_GENERO };

            var grauInstrucao = await _deOlhoDbContext.Set<GrauInstrucao>().SingleOrDefaultAsync(_ => _.Id == message.CD_GRAU_INSTRUCAO)
                ?? new GrauInstrucao { Id = message.CD_GRAU_INSTRUCAO, Descricao = message.DS_GRAU_INSTRUCAO };

            var ocupacao = await _deOlhoDbContext.Set<Ocupacao>().SingleOrDefaultAsync(_ => _.Id == message.CD_OCUPACAO)
                ?? new Ocupacao { Id = message.CD_OCUPACAO, Descricao = message.DS_OCUPACAO };

            var tipoEleicao = await _deOlhoDbContext.Set<TipoEleicao>().SingleOrDefaultAsync(_ => _.Id == message.CD_TIPO_ELEICAO)
                ?? new TipoEleicao { Id = message.CD_TIPO_ELEICAO, Descricao = message.NM_TIPO_ELEICAO };

            var eleicao = await _deOlhoDbContext.Set<Eleicao>().SingleOrDefaultAsync(_ => _.Id == message.CD_ELEICAO)
                ?? new Eleicao { 
                    Id = message.CD_ELEICAO,
                    Ano = message.ANO_ELEICAO,
                    Descricao = message.DS_ELEICAO,
                    Data = message.DT_ELEICAO,
                    Tipo = tipoEleicao
                };

            var partido = await _deOlhoDbContext.Set<Partido>().SingleOrDefaultAsync(_ => _.Id == message.NR_PARTIDO)
                ?? new Partido { Id = message.NR_PARTIDO, Sigla = message.SG_PARTIDO, Nome = message.NM_PARTIDO };

            var cargo = await _deOlhoDbContext.Set<Cargo>().SingleOrDefaultAsync(_ => _.Id == message.CD_CARGO)
                ?? new Cargo { Id = message.CD_CARGO, Nome = message.DS_CARGO };
            

            politico.CPF = message.NR_CPF_CANDIDATO;
            politico.Nome = message.NM_SOCIAL_CANDIDATO;
            politico.Apelido = message.NM_CANDIDATO;
            politico.DataNascimento = message.DT_NASCIMENTO;
            politico.CidadeNascimento = message.NM_MUNICIPIO_NASCIMENTO;
            politico.UFNascimento = message.SG_UF_NASCIMENTO;
            politico.Sexo = sexo;
            politico.GrauInstrucao = grauInstrucao;
            politico.Ocupacao = ocupacao;
            politico.DataInformacao = new DateTime(message.ANO_ELEICAO, 1 ,1);
            
            var mandato = politico.Mandatos.SingleOrDefault(_ => _.Eleicao.Ano == message.ANO_ELEICAO) ?? new Mandato();;
            
            mandato.Eleicao = eleicao;

            mandato.Cargo = cargo;

            mandato.Partido = partido;

            if (mandato.Id == 0)
                politico.Mandatos.Add(mandato);

            if (politico.Id == 0)
                await politicoRepository.AddAsync(politico, cancellationToken);
            
            await _deOlhoDbContext.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}