using Microsoft.EntityFrameworkCore;

namespace DeOlho.Politico.Infrastucture.Data
{
    public class PoliticoEntityMapping : IEntityTypeConfiguration<Domain.Politico>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Politico> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.HasIndex(_ => _.CPF);
        }
    }

    // public class MandatoEntityMapping : IEntityTypeConfiguration<Domain.Mandato>
    // {
    //     public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Mandato> builder)
    //     {
    //         builder.HasKey(_ => _.Id);
    //     }
    // }

    // public class PartidoEntityMapping : IEntityTypeConfiguration<Domain.Partido>
    // {
    //     public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Partido> builder)
    //     {
    //         builder.HasKey(_ => _.Id);
    //     }
    // }

    // public class EleicaoEntityMapping : IEntityTypeConfiguration<Domain.Partido>
    // {
    //     public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Partido> builder)
    //     {
    //         builder.HasKey(_ => _.Id);
    //     }
    // }

}