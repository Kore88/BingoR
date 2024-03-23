using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BingoR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<BingoJatekos> jatekosok = new List<BingoJatekos>();
        string[,] jatekosSzamok;
        List<int> kihuzottszamok = new List<int>();
        List<int> nyertesek = new List<int>();
        

        public MainWindow()
        {
            InitializeComponent();
            Beolvas();
            
        }

        private void Beolvas()
        {
            try
            {
                using FileStream fs = File.OpenRead("nevek.text");
                using StreamReader sr = new StreamReader(fs);

                int index = 0;

                while (!sr.EndOfStream)
                {
                    jatekosok.Add(new BingoJatekos(sr.ReadLine()));
                    cbxJatekos.Items.Add(jatekosok[index].Nev);
                    nyertesek.Add(0);
                    index++;
                }
                fs.Close();
                sr.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void cbxJatekos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbNyeroSzamok.Text = "";

            jatekosSzamok = jatekosok[cbxJatekos.SelectedIndex].Szamok;
            //for (int i = 0; i < 5; i++)
            //{
            //    for(int j = 0; j < 5; j++)
            //    {
            //        TextBox tb = new TextBox();
            //        tb.Margin = new Thickness(j*30, i*30,0,0);
            //        tb.Width = 20;
                    
            //        if (i == 2&& j == 2)
            //        {
            //            tb.Text = "X";
            //            tb.Background = Brushes.Green;
            //        }
            //        else
            //        {
            //            tb.Text = jatekosSzamok[i, j];
            //        }
            //        cTarto.Children.Add(tb);
            //    }
            //}
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    TextBox tb = new TextBox();
                    tb.Margin = new Thickness(j * 30, i * 30, 0, 0);
                    tb.Width = 20;

                    if (i == 2 && j == 2)
                    {
                        tb.Text = "X";
                        tb.Background = Brushes.Green;
                    }
                    else if (jatekosok[cbxJatekos.SelectedIndex].Kihuzva[i, j])
                    {
                        tb.Text = jatekosSzamok[i, j];
                        tb.Background = Brushes.Green;

                    }
                    else
                    {
                        tb.Text = jatekosSzamok[i, j];
                    }
                    cTarto.Children.Add(tb);
                }
            }
        }

        private void btnHuzas_Click(object sender, RoutedEventArgs e)
        {
            if (cbxJatekos.SelectedIndex != 0)
            {
                Random rnd = new Random();
                int vszam;


                do
                {
                    vszam = rnd.Next(1, 76);
                } while (kihuzottszamok.Contains(vszam));

                kihuzottszamok.Add(vszam);
                txbNyeroSzamok.Text += (vszam.ToString() + ", ");

                if (kihuzottszamok.Count %10 ==0)
                {              
                    txbNyeroSzamok.AppendText("\n");
                    txbNyeroSzamok.Height += 23;
                }

                jatekosok[cbxJatekos.SelectedIndex].SoroltSzamotJelol(vszam);

                cTarto.Children.Clear();

                bool nyert = false;

                foreach (BingoJatekos jatekos in jatekosok)
                {
                    for (int i = 0; i < jatekosok.Count; i++)
                    {
                        if (nyertesek[i] == 0) 
                        {
                            jatekos.SoroltSzamotJelol(vszam);
                            if (jatekos.BingoEll())
                            {
                                nyertesek[i] = 1;
                                if (!nyert)
                                {
                                    nyert = true;
                                    MessageBox.Show($"{jatekos.Nev} Gratulálok, BINGÓ !!");
                                }
                                break;

                            }
                        }         
                    }                    
                }

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        TextBox tb = new TextBox();
                        tb.Margin = new Thickness(j * 30, i * 30, 0, 0);
                        tb.Width = 20;

                        if (i == 2 && j == 2)
                        {
                            tb.Text = "X";
                            tb.Background = Brushes.Green;
                        }
                        else if (jatekosok[cbxJatekos.SelectedIndex].Kihuzva[i, j])
                        {
                            tb.Text = jatekosSzamok[i, j];
                            tb.Background = Brushes.Green;

                        }
                        else
                        {
                            tb.Text = jatekosSzamok[i, j];
                        }
                        cTarto.Children.Add(tb);
                    }
                }

                //if (jatekosok[cbxJatekos.SelectedIndex].BingoEll())
                //{
                //    MessageBox.Show("BINGO !!!");
                //}
            }
           
        }      
    }
}