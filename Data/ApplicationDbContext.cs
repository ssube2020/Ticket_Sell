using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ticket_Sell.Models;

namespace Ticket_Sell.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }

        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<SoldTicket> SoldTickets { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<UserTicketHistory> UserTicketHistorys { get; set; }
        public DbSet<UserType> UserTypes { get; set; }

    }
}

