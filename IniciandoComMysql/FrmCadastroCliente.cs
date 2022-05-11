using IniciandoComMysql.Dao;
using IniciandoComMysql.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IniciandoComMysql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AlimentaTabela();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente cliente = new Cliente
                {
                    Nome = textNome.Text,
                    Email = textEmail.Text,
                    Telefone = textTelefone.Text,
                    Endereco = textEndereco.Text
                };

                DaoCliente daoCliente = new DaoCliente();
                daoCliente.Salvar(cliente);

                MessageBox.Show($"Cadastro realizado com sucesso!", "", 0, MessageBoxIcon.Information);
                textNome.Text = "";
                textEmail.Text = "";
                textTelefone.Text = "";
                textEndereco.Text = "";
                AlimentaTabela();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Erro:", 0, MessageBoxIcon.Error);
            }
        }
        
        private void AlimentaTabela(DaoCliente clientes = null, string text = "")
        {
            dataGridView1.Rows.Clear();
            clientes = new DaoCliente();

            if (clientes.Pesquisa(text).Count == 0)
            {
                MessageBox.Show("Cadastro não encontrado!");
                textPesquisa.Text = "";
                AlimentaTabela();
            }
            clientes.Pesquisa(text).ForEach(cliente => {
                dataGridView1.Rows.Add(
                    cliente.Id,
                    cliente.Nome,
                    cliente.Email,
                    cliente.Telefone,
                    cliente.Endereco
                );
            });
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                DaoCliente clientes = new DaoCliente();
                AlimentaTabela(clientes, textPesquisa.Text);
            } catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
    }
}
