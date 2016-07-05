namespace _Entidades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comboes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Relacion = c.String(nullable: false, maxLength: 50),
                        Valor = c.Int(nullable: false),
                        Descripcion = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Contratoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        EmpresaId = c.Int(nullable: false),
                        PlanId = c.Int(nullable: false),
                        fecInicia = c.DateTime(nullable: false),
                        fecTermina = c.DateTime(nullable: false),
                        MinPlan = c.Int(nullable: false),
                        obsContrato = c.String(maxLength: 500),
                        UUID = c.String(maxLength: 128),
                        EstReg = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Empresas", t => t.EmpresaId, cascadeDelete: true)
                .ForeignKey("dbo.Plans", t => t.PlanId, cascadeDelete: true)
                .Index(t => t.EmpresaId)
                .Index(t => t.PlanId);
            
            CreateTable(
                "dbo.Empresas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        EmpRuc = c.String(nullable: false, maxLength: 13),
                        EmpNom = c.String(nullable: false, maxLength: 100),
                        Direccion = c.String(nullable: false, maxLength: 150),
                        Telefono = c.String(nullable: false, maxLength: 50),
                        UUID = c.String(),
                        EstReg = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 70),
                        Email = c.String(nullable: false, maxLength: 80),
                        Pwd = c.String(nullable: false, maxLength: 40),
                        Horario = c.String(maxLength: 70),
                        Tipo = c.Int(nullable: false),
                        EmpresaId = c.Int(nullable: false),
                        UUID = c.String(nullable: false, maxLength: 150),
                        EstReg = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Empresas", t => t.EmpresaId, cascadeDelete: true)
                .Index(t => t.EmpresaId);
            
            CreateTable(
                "dbo.Plans",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 50),
                        Minutos = c.Int(nullable: false),
                        UUID = c.String(),
                        EstReg = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ContratoId = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                        fechaINI = c.DateTime(nullable: false),
                        TecnicoId = c.Int(nullable: false),
                        fechaFIN = c.DateTime(nullable: false),
                        cmbPrioridadId = c.Int(nullable: false),
                        cmbEstadoId = c.Int(nullable: false),
                        TicketCategoriaId = c.Int(nullable: false),
                        NumDetalle = c.Int(nullable: false),
                        UUID = c.String(nullable: false, maxLength: 150),
                        EstReg = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Contratoes", t => t.ContratoId, cascadeDelete: true)
                .ForeignKey("dbo.Tecnicoes", t => t.TecnicoId, cascadeDelete: true)
                .ForeignKey("dbo.TicketsCategorias", t => t.TicketCategoriaId, cascadeDelete: true)
                .Index(t => t.ContratoId)
                .Index(t => t.TecnicoId)
                .Index(t => t.TicketCategoriaId);
            
            CreateTable(
                "dbo.Tecnicoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nombreTecnico = c.String(nullable: false, maxLength: 80),
                        EstReg = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.TicketsCategorias",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Categoria = c.String(nullable: false, maxLength: 50),
                        isDescarga = c.Boolean(nullable: false),
                        EstReg = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.TicketsDetalles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        SecRespta = c.Int(nullable: false),
                        TicketId = c.Int(nullable: false),
                        fechaING = c.DateTime(nullable: false),
                        teamViewer = c.String(nullable: false, maxLength: 12),
                        Minutos = c.Int(nullable: false),
                        Mensaje = c.String(nullable: false, maxLength: 460),
                        observacion = c.String(nullable: false, maxLength: 2000),
                        UsuarioId = c.Int(nullable: false),
                        File1 = c.String(),
                        File2 = c.String(),
                        File3 = c.String(),
                        isReaded = c.Boolean(nullable: false),
                        whoSend = c.String(nullable: false, maxLength: 7),
                        EstReg = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Tickets", t => t.TicketId, cascadeDelete: true)
                .Index(t => t.TicketId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketsDetalles", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.Tickets", "TicketCategoriaId", "dbo.TicketsCategorias");
            DropForeignKey("dbo.Tickets", "TecnicoId", "dbo.Tecnicoes");
            DropForeignKey("dbo.Tickets", "ContratoId", "dbo.Contratoes");
            DropForeignKey("dbo.Contratoes", "PlanId", "dbo.Plans");
            DropForeignKey("dbo.Usuarios", "EmpresaId", "dbo.Empresas");
            DropForeignKey("dbo.Contratoes", "EmpresaId", "dbo.Empresas");
            DropIndex("dbo.TicketsDetalles", new[] { "TicketId" });
            DropIndex("dbo.Tickets", new[] { "TicketCategoriaId" });
            DropIndex("dbo.Tickets", new[] { "TecnicoId" });
            DropIndex("dbo.Tickets", new[] { "ContratoId" });
            DropIndex("dbo.Usuarios", new[] { "EmpresaId" });
            DropIndex("dbo.Contratoes", new[] { "PlanId" });
            DropIndex("dbo.Contratoes", new[] { "EmpresaId" });
            DropTable("dbo.TicketsDetalles");
            DropTable("dbo.TicketsCategorias");
            DropTable("dbo.Tecnicoes");
            DropTable("dbo.Tickets");
            DropTable("dbo.Plans");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Empresas");
            DropTable("dbo.Contratoes");
            DropTable("dbo.Comboes");
        }
    }
}
