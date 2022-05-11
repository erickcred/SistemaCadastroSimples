using IniciandoComMysql.Entities.AbsEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IniciandoComMysql.Entities
{
    public class Cliente : Pessoa
    {
        public Cliente() { }
        public Cliente(int id, string nome, string email, string telefone, string endereco) : base(id, nome, email, telefone, endereco) { }
    }
}
