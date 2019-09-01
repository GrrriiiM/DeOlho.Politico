using System;
using System.Collections.Generic;
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
        {
            _deOlhoDbContext = deOlhoDbContext;
        }
        public async override Task<Unit> Handle(PoliticoChangedMessage message, CancellationToken cancellationToken)
        {
            var listaSexo = await _deOlhoDbContext.Set<Sexo>().ToListAsync(cancellationToken);
            var listaGrauInstrucao = await _deOlhoDbContext.Set<GrauInstrucao>().ToListAsync(cancellationToken);
            var listaOcupacao = await _deOlhoDbContext.Set<Ocupacao>().ToListAsync(cancellationToken);
            var listaTipoEleicao = await _deOlhoDbContext.Set<TipoEleicao>().ToListAsync(cancellationToken);
            var listaEleicao = await _deOlhoDbContext.Set<Eleicao>().ToListAsync(cancellationToken);
            var listaMandatoSituacao = await _deOlhoDbContext.Set<MandatoSituacao>().ToListAsync(cancellationToken);
            var listaPartido = await _deOlhoDbContext.Set<Partido>().ToListAsync(cancellationToken);
            var listaCargo = await _deOlhoDbContext.Set<Cargo>().ToListAsync(cancellationToken);

            var politicoRepository = _deOlhoDbContext.Set<Domain.Politico>();

            foreach(var messagePolitico in message.Politicos)
            {

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
                    .Include(_ => _.Mandatos)
                    .ThenInclude(_ => _.Situacao)
                    .SingleOrDefaultAsync(_ => 
                        _.CPF == messagePolitico.NR_CPF_CANDIDATO,
                        cancellationToken)
                    ?? new Domain.Politico();

                var sexo = listaSexo.FirstOrDefault(_ => _.Id == messagePolitico.CD_GENERO);
                if (sexo == null)
                {
                    sexo = new  Sexo { Id = messagePolitico.CD_GENERO, Descricao = messagePolitico.DS_GENERO }; 
                    listaSexo.Add(sexo);
                }

                var grauInstrucao = listaGrauInstrucao.FirstOrDefault(_ => _.Id == messagePolitico.CD_GRAU_INSTRUCAO);
                if (grauInstrucao == null)
                {
                    grauInstrucao = new GrauInstrucao { Id = messagePolitico.CD_GRAU_INSTRUCAO, Descricao = messagePolitico.DS_GRAU_INSTRUCAO }; 
                    listaGrauInstrucao.Add(grauInstrucao);
                }
                    

                var ocupacao = listaOcupacao.FirstOrDefault(_ => _.Id == messagePolitico.CD_OCUPACAO);
                if (ocupacao == null)
                {
                    ocupacao = new Ocupacao { Id = messagePolitico.CD_OCUPACAO, Descricao = messagePolitico.DS_OCUPACAO }; 
                    listaOcupacao.Add(ocupacao);
                }
                    

                var tipoEleicao = listaTipoEleicao.FirstOrDefault(_ => _.Id == messagePolitico.CD_TIPO_ELEICAO);
                if (tipoEleicao == null)
                {
                    tipoEleicao = new TipoEleicao { Id = messagePolitico.CD_TIPO_ELEICAO, Descricao = messagePolitico.NM_TIPO_ELEICAO }; 
                    listaTipoEleicao.Add(tipoEleicao);
                }
                    

                var eleicao = listaEleicao.FirstOrDefault(_ => _.Id == messagePolitico.CD_ELEICAO);
                if (eleicao == null)
                {
                    eleicao = new Eleicao {  
                        Id = messagePolitico.CD_ELEICAO,
                        Ano = messagePolitico.ANO_ELEICAO,
                        Descricao = messagePolitico.DS_ELEICAO,
                        Data = messagePolitico.DT_ELEICAO,
                        Tipo = tipoEleicao,
                    };
                    listaEleicao.Add(eleicao);
                }

                var situacao = listaMandatoSituacao.FirstOrDefault(_ => _.Id == messagePolitico.CD_SIT_TOT_TURNO);
                if (situacao == null)
                {
                    situacao = new MandatoSituacao { Id = messagePolitico.CD_SIT_TOT_TURNO, Descricao = messagePolitico.DS_SIT_TOT_TURNO }; 
                    listaMandatoSituacao.Add(situacao);
                }
                    

                var partido = listaPartido.FirstOrDefault(_ => _.Id == messagePolitico.NR_PARTIDO);
                if (partido == null)
                {
                    partido = new Partido { Id = messagePolitico.NR_PARTIDO, Sigla = messagePolitico.SG_PARTIDO, Nome = messagePolitico.NM_PARTIDO }; 
                    listaPartido.Add(partido);
                }
                    

                var cargo = listaCargo.FirstOrDefault(_ => _.Id == messagePolitico.CD_CARGO);
                if (cargo == null)
                {
                    cargo = new Cargo { Id = messagePolitico.CD_CARGO, Nome = messagePolitico.DS_CARGO }; 
                    listaCargo.Add(cargo);
                }

                politico.CPF = messagePolitico.NR_CPF_CANDIDATO;
                politico.Nome = messagePolitico.NM_CANDIDATO;
                politico.Apelido = messagePolitico.NM_URNA_CANDIDATO;
                politico.DataNascimento = messagePolitico.DT_NASCIMENTO;
                politico.CidadeNascimento = messagePolitico.NM_MUNICIPIO_NASCIMENTO;
                politico.UFNascimento = messagePolitico.SG_UF_NASCIMENTO;
                politico.Sexo = sexo;
                politico.GrauInstrucao = grauInstrucao;
                politico.Ocupacao = ocupacao;
                politico.DataInformacao = new DateTime(messagePolitico.ANO_ELEICAO, 1 ,1);
                
                var mandato = politico.Mandatos.SingleOrDefault(_ => _.Eleicao.Ano == messagePolitico.ANO_ELEICAO) ?? new Mandato();;
            
                mandato.Eleicao = eleicao;
                mandato.Cargo = cargo;
                mandato.Partido = partido;
                mandato.Situacao = situacao;
                mandato.Abrangencia = messagePolitico.NM_UE;

                if (mandato.Id == 0)
                    politico.Mandatos.Add(mandato);

                politico.TermoPesquisa = politico.BuildTermoPesquisa();

                if (politico.Id == 0)
                    await politicoRepository.AddAsync(politico, cancellationToken);
                
            }

            await _deOlhoDbContext.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}