﻿using Dapper.Contrib.Extensions;
using QueBox.Models;
using QueBox.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace QueBox.Repository.Implements
{
    public class CapaRepository : ICaparepository
    {
        private readonly IDbConnection _db;

        public CapaRepository(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public async Task<int> Add(Capa C)
        {
            try
            {
                int ID_Capa = await _db.InsertAsync(C);
                return ID_Capa;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<int> Add(Capa C)
        {
            try
            {
                int ID_IMG = await _db.InsertAsync(C);
                return ID_IMG;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<int> Add(Capa C)
        {
            try
            {
                int Numero = await _db.InsertAsync(C);
                return Numero;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
