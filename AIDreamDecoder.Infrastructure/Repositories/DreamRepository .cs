﻿using AIDreamDecoder.Domain.Entities;
using AIDreamDecoder.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDreamDecoder.Infrastructure.Repositories
{
    public class DreamRepository : IDreamRepository
    {
        private readonly AIDreamDecoderDbContext _context;

        public DreamRepository(AIDreamDecoderDbContext context)
        {
            _context = context;
        }

        public async Task<List<Dream>> GetAllDreamsAsync()
        {
            return await _context.Dreams.ToListAsync(); // Rüyaları getir
        }

        public async Task<Dream> GetDreamByIdAsync(Guid id)
        {
            return await _context.Dreams.FindAsync(id); // ID'ye göre rüya getir
        }

        public async Task AddDreamAsync(Dream dream)
        {
            _context.Dreams.Add(dream); // Rüya ekle
            await _context.SaveChangesAsync(); // Değişiklikleri kaydet
        }

        public async Task<bool> DeleteDreamAsync(Guid id)
        {
            var dream = await _context.Dreams.FindAsync(id);
            if (dream == null) return false;
            _context.Dreams.Remove(dream); // Rüyayı sil
            await _context.SaveChangesAsync(); // Değişiklikleri kaydet
            return true;
        }

        public async Task<Dream> AddAsync(Dream dream)
        {
            if (dream == null) throw new ArgumentNullException(nameof(dream)); // Null kontrolü yap
            await _context.Dreams.AddAsync(dream); // Rüyayı ekle
            await _context.SaveChangesAsync(); // Veritabanına değişiklikleri kaydet
            return dream; // Eklenen rüyayı geri döndür
        }

        public async Task UpdateAsync(Dream dream)
        {
            _context.Dreams.Update(dream); // Rüya kaydını güncelle
            await _context.SaveChangesAsync(); // Değişiklikleri kaydet
        }
    }
}

