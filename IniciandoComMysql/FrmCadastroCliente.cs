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
    public partial class FrmCadastroCliente : Form
    {
        public FrmCadastroCliente()
        {
            InitializeComponent();
        }

        private void FrmCadastroCliente_Load(object sender, EventArgs e)
        {
            AlimentaTabela();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidaCampo())
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
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Erro:", 0, MessageBoxIcon.Error);
            }
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                DaoCliente clientes = new DaoCliente();
                AlimentaTabela(clientes, textPesquisa.Text);
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private bool ValidaCampo()
        {
            bool resultado = true;
            if (textNome.Text == String.Empty)
            {
                MessageBox.Show("O campo Nome dever ser preenchido!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textNome.BackColor = Color.PaleVioletRed;
                resultado = false;
            }
            if (textEmail.Text == String.Empty)
            {
                MessageBox.Show("O campo E-mail deve ser preebchido", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textEmail.BackColor = Color.PaleVioletRed;
                resultado = false;
            }
            if (textTelefone.Text == String.Empty)
            {
                MessageBox.Show("O campo Telefone deve ser preenchido", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textTelefone.BackColor = Color.PaleVioletRed;
                resultado = false;
            }
            if (textEndereco.Text == String.Empty)
            {
                MessageBox.Show("O campo Endereço deve ser preenchido", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textEndereco.BackColor = Color.PaleVioletRed;
                resultado = false;
            }
            return resultado;
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

        private void textNome_TextChanged(object sender, EventArgs e)
        {
            textNome.BackColor = Color.White;
        }

        private void textEmail_TextChanged(object sender, EventArgs e)
        {
            textEmail.BackColor = Color.White;
        }

        private void textTelefone_TextChanged(object sender, EventArgs e)
        {
            textTelefone.BackColor = Color.White;
            textTelefone.MaxLength = 12;
        }

        private void textEndereco_TextChanged(object sender, EventArgs e)
        {
            textEndereco.BackColor = Color.White;
        }
    }
}
