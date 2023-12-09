using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicShop.Models;

namespace MusicShop.Data
{
    public class MusicShopContext : DbContext
    {
        public MusicShopContext (DbContextOptions<MusicShopContext> options)
            : base(options)
        {
        }

        public DbSet<MusicShop.Models.MusicLibrary> MusicLibrary { get; set; } = default!;
    }
}
