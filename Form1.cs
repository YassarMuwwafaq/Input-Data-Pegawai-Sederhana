using MySql.Data.MySqlClient;
using Mysqlx.Prepare;
using System.Data;

namespace projectmysql
{
    public partial class Form1 : Form
    {
        int selectedId;//menyimpan id data yang dipilih
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonInput_Click(object sender, EventArgs e)
        {
            //buat objek mysqlconnection
            String conString = "server=localhost; database=db_simpeg; uid=root; pwd=\"\"";
            MySqlConnection cnn = new MySqlConnection(conString);
            MySqlCommand cmd = new MySqlCommand();
            cmd = cnn.CreateCommand();
            cmd.CommandText = "INSERT INTO tbpegawai (nip,nama,jabatan,gaji) VALUES (@nip,@nama,@jabatan, @gaji)";
            cmd.Parameters.AddWithValue("nip", textBoxNIP.Text);
            cmd.Parameters.AddWithValue("nama", textBoxNama.Text);
            cmd.Parameters.AddWithValue("jabatan", textBoxJabatan.Text);
            cmd.Parameters.AddWithValue("gaji", textBoxGaji.Text);
            try
            {
                //buka koneksi
                cnn.Open();
                //insert data
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil Diinput");
                tampilkanData();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("koneksi ke database gagal \n" + ex.Message);
            }
        }

        private void btnTesKoneksi_Click(object sender, EventArgs e)
        {
            //buat objek mysqlconnection
            String conString = "server=localhost; database=db_simpeg; uid=root; pwd=\"\"";
            MySqlConnection cnn = new MySqlConnection(conString);
            try
            {
                cnn.Open();
                MessageBox.Show("koneksi ke database berhasil");
                cnn.Close();
            }
           catch (Exception ex)
            {
                MessageBox.Show("koneksi ke database gagal");
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tampilkanData();
        }
        private void tampilkanData()
        {
            //pengaturan koneksi ke database
            String conString = "server=localhost; database=db_simpeg; uid=root; pwd=\"\"";
            //membuat objek koneksi
            MySqlConnection cnn = new MySqlConnection(conString);
            //membuat objek data adapter untuk menampung hasil query
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM tbpegawai",cnn);
            //menyiapkan objek dataset
            DataSet ds = new DataSet();
            //mengisi dataset
            da.Fill(ds,"pegawai");
            //menampilkan dataset di datagridview
            dataGridView1.DataSource = ds.Tables["pegawai"].DefaultView;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //int idData = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            //baca primary key 'id' dari row data yang di click
            selectedId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            MessageBox.Show("id:" + selectedId);
            //baca primary key'id' dari row data yang diclick
            textBoxNIP.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBoxNama.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBoxJabatan.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBoxGaji.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void buttonHapus_Click(object sender, EventArgs e)
        {
            //buat objek mysqlconnection
            String conString = "server=localhost; database=db_simpeg; uid=root; pwd=\"\"";
            MySqlConnection cnn = new MySqlConnection(conString);
            MySqlCommand cmd = new MySqlCommand();
            cmd = cnn.CreateCommand();
            cmd.CommandText = "DELETE FROM tbpegawai WHERE id=@id";
            cmd.Parameters.AddWithValue("id",selectedId );

            DialogResult dialogResult = MessageBox.Show("Anda yakin ingin menghapus data dengan id: " + selectedId + " ?",
                "Konfirmasi", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            
            try
            {
                //buka koneksi
                cnn.Open();
                //hapus data
                cmd.ExecuteNonQuery();
                MessageBox.Show("hapus data berhasil");
                tampilkanData();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("hapus data gagal \n" + ex.Message);
            }
        }

        private void buttonupdate_Click(object sender, EventArgs e)
        {
            //buat objek mysqlconnection
            String conString = "server=localhost; database=db_simpeg; uid=root; pwd=\"\"";
            MySqlConnection cnn = new MySqlConnection(conString);
            MySqlCommand cmd = new MySqlCommand();
            cmd = cnn.CreateCommand();
            cmd.CommandText = "UPDATE tbpegawai SET nip=@nip, nama=@nama, jabatan=@jabatan, gaji=@gaji WHERE id=@id";
            cmd.Parameters.AddWithValue("id", selectedId);
            cmd.Parameters.AddWithValue("nip", textBoxNIP.Text);
            cmd.Parameters.AddWithValue("nama", textBoxNama.Text);
            cmd.Parameters.AddWithValue("jabatan", textBoxJabatan.Text);
            cmd.Parameters.AddWithValue("gaji", textBoxGaji.Text);

            DialogResult dialogResult = MessageBox.Show("Anda yakin ingin mengubah data dengan id: " + selectedId + " ?",
                "Konfirmasi", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)

                try
                {
                    //buka koneksi
                    cnn.Open();
                    //hapus data
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("ubah data berhasil");
                    tampilkanData();
                    cnn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ubah data gagal \n" + ex.Message);
                }
        }
    }
}