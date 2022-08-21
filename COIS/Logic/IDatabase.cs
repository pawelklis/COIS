using System;
using System.Collections.Generic;
using System.Text;


    public interface IDatabase
    {
        public int Id { get; set; }
        public void Save();
        public void Delete();
    }

