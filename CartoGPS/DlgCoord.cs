// DlgCoord.cs

using System;
using System.Drawing;
using System.Windows.Forms;

namespace CartoGPS
{
	public partial class DlgCoord : Form
	{
		private bool cancel;
		private bool no;
		private bool okLon;
		private bool okLat;
		
		public double Lon
		{
			get 
			{
				return double.Parse(textBoxLon.Text);	
				//return double.Parse("6,9458");	
			}
		}
		
		public double Lat
		{
			get
			{
				return double.Parse(textBoxLat.Text);
				//return double.Parse("43,8296");
			}
		}		
		
		public DlgCoord()
		{
			InitializeComponent();
			cancel = false;
			no = false;
			okLon = true;
			okLat = true;
		}
		
		void TextBoxLonValidating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			double x;
			if (double.TryParse(textBoxLon.Text, out x)) 
			{
				errorProvider1.SetError(textBoxLon, "");
				okLon = true;
			}
			else 
			{
				errorProvider1.SetError(textBoxLon, "Entrer une valeur numérique");
				okLon = false;
			}
		}
		
		void TextBoxLatValidating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			double x;
			if (double.TryParse(textBoxLat.Text, out x)) 
			{
				errorProvider1.SetError(textBoxLat, "");
				okLat = true;
			}
			else 
			{
				errorProvider1.SetError(textBoxLat, "Entrer une valeur numérique");
				okLat = false;
			}
		}
				
		void DialogCoordFormClosing(object sender, FormClosingEventArgs e)
		{
			if (cancel || no) return;
			ValidateChildren();
			if(!okLon || !okLat) e.Cancel = true; // bloque la fermeture
		}
		
		void ButtonAnnulerClick(object sender, EventArgs e)
		{
			cancel = true;
		}
		
		void ButtonSupprimerClick(object sender, EventArgs e)
		{
			no = true;
		}
	}
}
