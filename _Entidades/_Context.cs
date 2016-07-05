using System;
using System.Data.Entity;
using System.Linq;
using _Entidades;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
namespace _Entidades
{
    public class _Context : DbContext
    {
        // El contexto se ha configurado para usar una cadena de conexión 'TicketContext' del archivo 
        // de configuración de la aplicación (App.config o Web.config). De forma predeterminada, 
        // esta cadena de conexión tiene como destino la base de datos 'DataTickets.TicketContext' de la instancia LocalDb. 
        // 
        // Si desea tener como destino una base de datos y/o un proveedor de base de datos diferente, 
        // modifique la cadena de conexión 'TicketContext'  en el archivo de configuración de la aplicación.
        public _Context()
            : base("data source=Servidor;initial catalog=TicketsMVC;integrated security=True;")
        {
        }

        // Agregue un DbSet para cada tipo de entidad que desee incluir en el modelo. Para obtener más información 
        // sobre cómo configurar y usar un modelo Code First, vea http://go.microsoft.com/fwlink/?LinkId=390109.


        public DbSet<Combo> Combo { get; set; }

        public  DbSet<Contrato> Contrato { get; set; }
        public  DbSet<Empresa> Empresa { get; set; }
        public  DbSet<Plan> Plan { get; set; }
        public  DbSet<Tecnico> Tecnico { get; set; }
        public  DbSet<Ticket> Ticket { get; set; }
        public DbSet<TicketsDetalle> TicketDetalle { get; set; }
        public DbSet<TicketsCategoria> TicketCategoria { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            _Table_Combo(modelBuilder);
            _Table_Contrato(modelBuilder);
            _Table_Empresa(modelBuilder);
            _Table_Plan(modelBuilder);
            _Table_Tecnico(modelBuilder);
            _Table_Ticket(modelBuilder);
            _Table_TicketCategoria(modelBuilder);
            _Table_TicketDetalle(modelBuilder);
        }

        private void _Table_Combo(DbModelBuilder modelo)
        {
            modelo.Entity<Combo>()
                .Property(x => x.Relacion)
                    .IsRequired();
            modelo.Entity<Combo>()
                .Property(x => x.Valor)
                    .IsRequired();
        }

        private void _Table_Contrato(DbModelBuilder modelo)
        {
            //Relacion con los planes
            //////modelo.Entity<Contrato>().HasRequired(p => p.e_Plan)
            //////    .WithMany(c => c.e_Contratos)
            //////    .HasForeignKey(c=> c.Plan_id);

            modelo.Entity<Contrato>()
                .Property(x => x.EmpresaId)
                    .IsRequired();
            modelo.Entity<Contrato>()
                .Property(x => x.PlanId)
                    .IsRequired();
            modelo.Entity<Contrato>()
                .Property(x => x.fecInicia)
                    .IsRequired();
            modelo.Entity<Contrato>()
                .Property(x => x.fecTermina)
                    .IsRequired();
            modelo.Entity<Contrato>()
                .Property(x => x.MinPlan)
                    .IsRequired();
            modelo.Entity<Contrato>()
                .Property(x => x.obsContrato)
                    .HasMaxLength(500);
            modelo.Entity<Contrato>()
                .Property(x => x.EstReg)
                    .IsRequired();
        }

        private void _Table_Empresa(DbModelBuilder modelo)
        {
            modelo.Entity<Empresa>()
                .Property(x => x.EmpRuc)
                    .IsRequired()
                    .HasMaxLength(13);
            modelo.Entity<Empresa>()
                .Property(x => x.EmpNom)
                    .IsRequired()
                    .HasMaxLength(100);
            modelo.Entity<Empresa>()
                .Property(x => x.Direccion)
                    .IsRequired()
                    .HasMaxLength(150);
            modelo.Entity<Empresa>()
                .Property(x => x.Telefono)
                    .IsRequired()
                    .HasMaxLength(50);
            modelo.Entity<Empresa>()
                .Property(x => x.EstReg)
                    .IsRequired();
        }

        private void _Table_Plan(DbModelBuilder modelo)
        {
            modelo.Entity<Plan>()
                .Property(x => x.Descripcion)
                    .HasMaxLength(50)
                    .IsRequired();
            modelo.Entity<Plan>()
                .Property(x => x.Minutos)
                    .IsRequired();
            modelo.Entity<Plan>()
                .Property(x => x.EstReg)
                    .IsRequired();
        }

        private void _Table_Tecnico(DbModelBuilder modelo)
        {
            modelo.Entity<Tecnico>()
                .Property(x => x.nombreTecnico)
                    .IsRequired()
                    .HasMaxLength(80);
            modelo.Entity<Tecnico>()
                .Property(x => x.EstReg)
                    .IsRequired();
        }

        private void _Table_Ticket(DbModelBuilder modelo)
        {
            modelo.Entity<Ticket>()
                .Property(x => x.ContratoId)
                    .IsRequired();
            modelo.Entity<Ticket>()
                .Property(x => x.UsuarioId)
                    .IsRequired();

            modelo.Entity<Ticket>()
                .Property(x => x.fechaINI)
                    .IsRequired();
            modelo.Entity<Ticket>()
                .Property(x => x.TecnicoId)
                    .IsRequired();
            modelo.Entity<Ticket>()
                .Property(x => x.cmbPrioridadId)
                    .IsRequired();
            modelo.Entity<Ticket>()
                .Property(x => x.TicketCategoriaId)
                    .IsRequired();
            modelo.Entity<Ticket>()
                .Property(x => x.cmbEstadoId)
                    .IsRequired();
            modelo.Entity<Ticket>()
                .Property(x => x.EstReg)
                    .IsRequired();

        }

        private void _Table_TicketCategoria(DbModelBuilder modelo)
        {
            modelo.Entity<TicketsCategoria>()
                .Property(x => x.Categoria)
                    .IsRequired()
                    .HasMaxLength(50);
            modelo.Entity<TicketsCategoria>()
                .Property(x => x.EstReg)
                    .IsRequired();
        }

        private void _Table_TicketDetalle(DbModelBuilder modelo)
        {
            modelo.Entity<TicketsDetalle>()
                .Property(x => x.TicketId)
                    .IsRequired();
            modelo.Entity<TicketsDetalle>()
                .Property(x => x.SecRespta)
                    .IsRequired();
            modelo.Entity<TicketsDetalle>()
                .Property(x => x.UsuarioId)
                    .IsRequired();
            modelo.Entity<TicketsDetalle>()
                .Property(x => x.fechaING)
                    .IsRequired();
            modelo.Entity<TicketsDetalle>()
                .Property(x => x.Mensaje)
                    .IsRequired()
                    .HasMaxLength(460);
            modelo.Entity<TicketsDetalle>()
                .Property(x => x.teamViewer)
                    .IsRequired()
                    .HasMaxLength(12);
            modelo.Entity<TicketsDetalle>()
                .Property(x => x.observacion)
                    .IsRequired()
                    .HasMaxLength(2000);
            modelo.Entity<TicketsDetalle>()
                .Property(x => x.EstReg)
                    .IsRequired();

        }

        private void _Table_Usuario(DbModelBuilder modelo)
        {
            modelo.Entity<Usuario>()
                .Property(x => x.Nombre)
                    .IsRequired()
                    .HasMaxLength(70);
            modelo.Entity<Usuario>()
                .Property(x => x.Email)
                    .IsRequired()
                    .HasMaxLength(80);
            modelo.Entity<Usuario>()
                .Property(x => x.Pwd)
                    .IsRequired()
                    .HasMaxLength(40);
            modelo.Entity<Usuario>()
                .Property(x => x.Horario)
                    .HasMaxLength(70);
            modelo.Entity<Usuario>()
                .Property(x => x.EstReg)
                    .IsRequired();
        }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}


   
}