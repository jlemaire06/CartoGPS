// FormCartoGPS.cs

using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace CartoGPS
{
 
	public partial class FormCartoGPS : Form
    {
		// Port série
		//private const string portName = "COM10";
		
		// Carte
		bool okC;													// vrai si carte valide
		string pathB;												// nom du fichier contenant le bitmap de la carte
		string pathC; 												// nom du fichier contenant les points de calage de la carte
		private Bitmap bm;        								    // bitmap de la carte à afficher
		private double a, b, c, d;							        // Coefficients de la transformation affine Pixel => Lieu
		private double A, B, C, D;							        // Coefficients de la transformation affine Lieu => Pixel
		double ech;													// Rapport d'échelle : nb pixels / m
			
		// Emplacement à situer
		private Pixel p;											// pixel dans carte initiale
		private bool okP;											// vrai s'il existe
		
		// Zoom
		private double zoom;										// zoom
		private double fZoom = 1.2;									// facteur se zoom
		private int wCarteZ, hCarteZ;								// dimensions de la carte zoomée
		private Pixel pZ;                                       	// Pixel de la carte zoomée affiché dans le coin supérieur-gauche de la zone client
		private int minColZ, maxColZ, minLigZ, maxLigZ; 			// Bornes des coordonnées de ce pixel		
			
		// Déplacement avec la souris dans la zone client
		private bool okDep = false;                                 // Indicateur de déplacement en cours
		private Point ptSouris;                                     // Position de la souris dans la zone client (coordonnées client)
	        
		// GPS
		//private Queue<Lieu> qGPS;
     	      	
        public FormCartoGPS()
        {
            InitializeComponent();	
            
            // Port série
            //port.PortName = portName;
			//qGPS = new Queue<Lieu>(10);
			port.PortName = Set.Default.portName;
			tsslIco.Image = Resource.ko;
			PortOpen();
	            
            // Fenêtre
            WindowState = FormWindowState.Maximized;
            
            // Carte
            okC = false;
            pathB = Set.Default.pathB;
            zoom = Set.Default.zoom;;	
            pZ = new Pixel(Set.Default.pZCol, Set.Default.pZLig);
            if (!File.Exists(pathB))
            {
            	if (!GetNewPathBitmap()) Environment.Exit(0);
            }
            if (!InitCarte()) Environment.Exit(0);
          	okC = true;
           	AfficherEchelle();
        }
        
        private bool InitCarte()
        {
        	try 
        	{
	          	// Bitmap
	            var _bm = new Bitmap(pathB);
	           	
	           	// Indicateurs de Zoom
	           	int _wCarteZ = Convert.ToInt32(_bm.Width * zoom);
	           	int _hCarteZ = Convert.ToInt32(_bm.Height * zoom);
	           	int _minColZ;
	           	_minColZ = 0;
	            int _maxColZ = Math.Max(_wCarteZ - ClientSize.Width, 0);
	            int _minLigZ;
				_minLigZ = 0;
	            int _maxLigZ = Math.Max(_hCarteZ - ClientSize.Height, 0);
	            
	            // Infos de calage et Titre
	            string _pathC = Path.ChangeExtension(pathB, ".xml");
	            var xmlDocCalage = new XmlDocument();
				xmlDocCalage.Load(_pathC);
				XmlNodeList xmlListPtsCalage = xmlDocCalage.GetElementsByTagName("Point");
				int n = xmlListPtsCalage.Count;
				var infCalage = new InfoCalage[n];
				int i = 0;
				foreach (XmlNode xmlPtCalage in xmlListPtsCalage)
				{
					infCalage[i].Lon  = double.Parse(xmlPtCalage.Attributes["Lon"].Value);
					infCalage[i].Lat  = double.Parse(xmlPtCalage.Attributes["Lat"].Value);
					infCalage[i].Col  = int.Parse(xmlPtCalage.Attributes["Col"].Value);
					infCalage[i].Lig  = int.Parse(xmlPtCalage.Attributes["Lig"].Value);
					i++;
				}
				XmlNodeList xmlListCarte= xmlDocCalage.GetElementsByTagName("Carte");
				string _titre = "";
				foreach (XmlNode xmlCarte in xmlListCarte)
				{
					_titre = xmlCarte.Attributes["Titre"].Value;
				}
				
				// Transformations affines
				var l1 = new Lieu(infCalage[0].Lon, infCalage[0].Lat);
				var l2 = new Lieu(infCalage[1].Lon, infCalage[1].Lat);
				var p1 = new Pixel(infCalage[0].Col, infCalage[0].Lig);
				var p2 = new Pixel(infCalage[1].Col, infCalage[1].Lig);
				double _a = (l2.Lon - l1.Lon)/(p2.Col - p1.Col);
				double _b = l1.Lon - (l2.Lon - l1.Lon)*p1.Col/(p2.Col - p1.Col);
				double _c = (l2.Lat - l1.Lat)/(p2.Lig - p1.Lig);
				double _d = l1.Lat - (l2.Lat - l1.Lat)*p1.Lig/(p2.Lig - p1.Lig);
				double _A = 1/_a;
				double _B = -_b/_a;
				double _C = 1/_c;
				double _D = -_d/_c;
				
				// Rapport d'échelle
				double _ech = Pixel.Distance(p1, p2)/Lieu.Distance(l1, l2);
				
				// Retour
				bm = _bm;
				pathC = _pathC;
				wCarteZ = _wCarteZ;
				hCarteZ = _hCarteZ;
				minColZ = _minColZ;
				maxColZ = _maxColZ;
				minLigZ = _minLigZ;
				maxLigZ = _maxLigZ;
				a = _a;
				b = _b;
				c = _c;
				d = _d;
				A = _A;
				B = _B;
				C = _C;
				D = _D;
				ech = _ech;
				
				// Pas de point à situer
				okP = false;
				
				// Titre de la fenêtre
				Text = "CartoGPS : " + _titre;
				
				return true;
        	}
        	catch
        	{
        		MessageBox.Show("Carte incorrecte");
        		return false;
        	}
        }
        
        private bool GetNewPathBitmap()
        {
        	if(openFileDialogBitmap.ShowDialog() == System.Windows.Forms.DialogResult.OK)
		    {
            	pathB = openFileDialogBitmap.FileName;
            	zoom = 1;
            	pZ = new Pixel(0, 0);
            	return true;
            }
            else
            {
            	return false;
            }
        }
               
        private Pixel PixelDeLieu(Lieu l)
        {
        	return PixelDeLieu(l.Lon, l.Lat);
        }
 
        private Pixel PixelDeLieu(double lon, double lat)
        {
         	return new Pixel(Convert.ToInt32(A*lon + B), Convert.ToInt32(C*lat + D));
        }
        
        private Lieu LieuDePixel(Pixel p)
        {
        	return LieuDePixel(p.Col, p.Lig);
        }       		

        private Lieu LieuDePixel(int col, int lig)
        {
        	return new Lieu(a*col + b, c*lig + d);
        }       		

        private double DLDeDPZ(int dpz)
        {
        	// distance entre lieux correspondant à une distance entre pixels zoomsé
        	return (dpz/zoom)/ech;
        }
        
        private int DPZDeDL(double dl)
        {
        	// distance entre pixels zoomés correspondant à une distance entre lieux
        	return Convert.ToInt32(dl*ech*zoom);
        }
        
        private void AfficherLieuSouris(MouseEventArgs e)
        {
         	var p = new Pixel(e.X + pZ.Col, e.Y + pZ.Lig); // Pixel dans carte zoomée
			if ((p.Col >= pZ.Col) && (p.Col < wCarteZ) && (p.Lig >= pZ.Lig) && (p.Lig < hCarteZ))
			{
				// Pixel dans carte initiale
				p.Col = Convert.ToInt32(p.Col/zoom);
				p.Lig = Convert.ToInt32(p.Lig/zoom);
				
				// Affichage informations
				tsslCol.Text = p.Col.ToString("Col : #");
				tsslLig.Text = p.Lig.ToString("Lig : #");
			}
			else
			{
				tsslCol.Text = string.Empty;				
				tsslLig.Text = string.Empty;
			}
        }
        
        private void AfficherEchelle()
        {
           	// Distance arrondie (m)
        	int dpz = 150;
        	double dl = DLDeDPZ(dpz);
        	if (dl < 1)
        	{
        		labelEch.Text = string.Empty;
         		return;
        	}
        	
        	int n = (int)(Math.Log10(dl));
        	dl = Convert.ToInt32(dl/Math.Pow(10, n)) * Math.Pow(10, n);
        	dpz = DPZDeDL(dl);
        	
        	// Affichage distance
        	if (dl >= 1000) 
        	{
        		dl /= 1000;
        		labelEch.Text = dl.ToString("#Km");
        	}
        	else 
        	{
        		labelEch.Text = dl.ToString("#m");
        	}
        	
        	// Affichage bitmap
        	labelEch.Width = dpz;
        }
    	
       
        void FormCartoGPSMouseDown(object sender, MouseEventArgs e)
		{
			switch (e.Button)
        	{
        		case (MouseButtons.Left):
        			okDep = true;
        			ptSouris =new Point(e.X, e.Y);
        		    Cursor.Clip = RectangleToScreen(ClientRectangle);
          			break;
        		
        		case (MouseButtons.Right):
					string _pathB = pathB;
					if (GetNewPathBitmap()) 
					{
						if (InitCarte()) 
						{
							Invalidate();
							AfficherLieuSouris(e);
            				AfficherEchelle();
						}
						else pathB = _pathB;
					}
        			break;         			
        	}
		}
        
        void FormCartoGPSMouseMove(object sender, MouseEventArgs e)
		{
        	AfficherLieuSouris(e);

			if (okDep)
			{
				bool inv = false;
				int deltaCol = -e.X + ptSouris.X;
				if (deltaCol != 0 && (pZ.Col + deltaCol) >= minColZ && (pZ.Col + deltaCol) <= maxColZ )
				{
					inv = true;
					pZ.Col = pZ.Col + deltaCol;
					ptSouris.X = e.X;
				}
				int deltaLig = -e.Y + ptSouris.Y;
				if (deltaLig != 0 && (pZ.Lig + deltaLig) >= minLigZ && (pZ.Lig + deltaLig) <= maxLigZ )
				{
					inv = true;
					pZ.Lig = pZ.Lig + deltaLig;
					ptSouris.Y = e.Y;
				}
				if (inv) Invalidate();
			}
		}
		
		void FormCartoGPSMouseUp(object sender, MouseEventArgs e)
		{
			okDep = false;
			Cursor.Clip = new Rectangle();
		}
		
		void FormCartoGPSMouseWheel(object sender, MouseEventArgs e)
        {
			// Pixel cliqué
			int c = Math.Min(pZ.Col + e.X, wCarteZ);
			int l = Math.Min(pZ.Lig + e.Y, hCarteZ);
			
			// Nouveau zoom
			double f = fZoom;
			if (e.Delta < 0) f = 1/f;
			zoom *= f;
			
			// Caractéristiques du bitmap zoomé
			wCarteZ = Convert.ToInt32(bm.Width*zoom);
			hCarteZ = Convert.ToInt32(bm.Height*zoom);
			pZ.Col = 0;
			pZ.Lig = 0;
            minColZ = 0;
            maxColZ = Math.Max(wCarteZ - ClientSize.Width, 0);
            minLigZ = 0;
            maxLigZ = Math.Max(hCarteZ - ClientSize.Height, 0);
            
            // Déplacement de l'image pour que le pixel cliqué ne change pas
            int x = Convert.ToInt32(c*f) - e.X;
        	if (x < minColZ) x = minColZ;
        	else if (x > maxColZ) x = maxColZ;
        	pZ.Col = x;       
            x = Convert.ToInt32(l*f) - e.Y;
        	if (x < minLigZ) x = minLigZ;
        	else if (x > maxLigZ) x = maxLigZ;
        	pZ.Lig = x;       	
            
            // Affichage
            Invalidate();
            AfficherLieuSouris(e);
            AfficherEchelle();
         }
		
		void FormCartoGPSResize(object sender, EventArgs e)
		{
			maxColZ = Math.Max(wCarteZ - ClientSize.Width, 0);
			if (ClientSize.Width > (wCarteZ - pZ.Col)) pZ.Col = Math.Max(wCarteZ - ClientSize.Width, 0);
			maxLigZ = Math.Max(hCarteZ - ClientSize.Height, 0);
			if (ClientSize.Height> (hCarteZ - pZ.Lig)) pZ.Lig = Math.Max(hCarteZ - ClientSize.Height, 0);
            Invalidate();
		}		
		
		void FormCartoGPSPaint(object sender, PaintEventArgs e)
		{
	    	var r = new Rectangle(pZ.Col, pZ.Lig, ClientSize.Width, ClientSize.Height);
	    	r.Intersect(new Rectangle(0, 0, wCarteZ, hCarteZ));
	    	var srcRect = new Rectangle(Convert.ToInt32(r.X / zoom), Convert.ToInt32(r.Y / zoom), Convert.ToInt32(r.Width /zoom), Convert.ToInt32(r.Height /zoom));
	    	var dstRect = new Rectangle(0, 0, r.Width, r.Height);
	    	e.Graphics.DrawImage(bm, dstRect, srcRect, GraphicsUnit.Pixel);
	    	if (okP)
	    	{
      			int rCircle = Convert.ToInt32(20*zoom);
      			int wPen = Convert.ToInt32(5*zoom);
//     	        e.Graphics.DrawEllipse(new Pen(Color.Yellow, wPen), new Rectangle(Convert.ToInt32(p.Col*zoom) - pZ.Col -rCircle, Convert.ToInt32(p.Lig*zoom) - pZ.Lig -rCircle , 2*rCircle, 2*rCircle));
                e.Graphics.DrawEllipse(new Pen(Color.Red, wPen), new Rectangle(Convert.ToInt32(p.Col * zoom) - pZ.Col - rCircle, Convert.ToInt32(p.Lig * zoom) - pZ.Lig - rCircle, 2 * rCircle, 2 * rCircle));
            }
        }
		
		void FormCartoGPSFormClosing(object sender, FormClosingEventArgs e)
		{
			if (okC)
			{
				Set.Default.pathB = pathB;
				Set.Default.zoom = zoom;
				Set.Default.pZCol = pZ.Col;
				Set.Default.pZLig = pZ.Lig;
				Set.Default.Save();
			}
		}
		
		void FormCartoGPSMouseLeave(object sender, EventArgs e)
		{
			tsslCol.Text = string.Empty;
			tsslLig.Text = string.Empty;
		}
		
		double Degre(string str)
		{
			double a = double.Parse(str.Replace('.', ','))/100;
			double e = Math.Truncate(a);
			return e + (a - e)/0.6;
		}
		/*
		Lieu MoyennesGPS()
		{
			var m = new Lieu(0, 0);
			Lieu[] t = qGPS.ToArray();
			for (int i=0; i<qGPS.Count; i++)
			{
				m.Lon += t[i].Lon;
				m.Lat += t[i].Lat;
			}
			m.Lon /= (double)qGPS.Count;
			m.Lat /= (double)qGPS.Count;
			return m;
		}
		*/
		void PortDataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			try
			{
				string str = port.ReadExisting();
				int deb = str.IndexOf("$GPGGA");
				if (deb >= 0)
				{
					int fin = str.IndexOf('\n', deb);
					if (fin > deb) 
					{
						str = str.Substring(deb, fin-deb+1);
						string[] tabStr = str.Split(',');
						double lon = Degre(tabStr[4]);
						double lat = Degre(tabStr[2]);
						float alt = float.Parse(tabStr[9].Replace('.', ','));
						int sat = int.Parse(tabStr[7]);
						float hdop = float.Parse(tabStr[8].Replace('.', ','));
						AfficherInfosGPS(lon, lat, alt, sat, hdop);
					}
				}
				Thread.Sleep(30);		
			}
			catch 
			{
			}
		}

		void AfficherInfosGPS(double lon, double lat, float alt, int sat, float hdop)
		{
			//qGPS.Enqueue(new Lieu(lon, lat));
			//if (qGPS.Count > 10) qGPS.Dequeue();
			//Lieu mGPS = MoyennesGPS();
			//tsslLon.Text = "Lon : " + mGPS.Lon.ToString("#.####0°");
			//tsslLat.Text = "Lat : " + mGPS.Lat.ToString("#.####0°");
			tsslLon.Text = "Lon : " + lon.ToString("#.####0°");
			tsslLat.Text = "Lat : " + lat.ToString("#.####0°");
			tsslAlt.Text = "Alt : " + alt.ToString("###m");									
			tsslSat.Text = "Sat : " + sat.ToString("#");
			tsslHDOP.Text = "HDOP : " + hdop.ToString("0.#0");
				
			// Récupération du point
    		//p = PixelDeLieu(mGPS.Lon, mGPS.Lat);
    		p = PixelDeLieu(lon, lat);
        				        		
    		// Déplacement de l'image pour placer le point au centre de la zone client
    		if (!okP)
    		{
    			okP = true;
    			CentrerCarte(p);
	    		Invalidate();
    		}        		
 		}
		
		void CentrerCarte(Pixel p)
		{
		    // Déplacement de la carte pour placer le point au centre de la zone client
    		int x = Convert.ToInt32(p.Col*zoom) - ClientSize.Width/2;
			if (x < minColZ) x = minColZ;
			else if (x > maxColZ) x = maxColZ;
			pZ.Col = x;       	
			x = Convert.ToInt32(p.Lig*zoom) - ClientSize.Height/2;
			if (x < minLigZ) x = minLigZ;
			else if (x > maxLigZ) x = maxLigZ;
			pZ.Lig = x;       	
		}
						
		void SstClick(object sender, EventArgs e)
		{
			// Affichage dialogue pour saisir les coordonnées
			var dlg = new DlgCoord();
			switch (dlg.ShowDialog(this))
			{
				case (DialogResult.OK) :
		      		// Récupération du point
		      		p = PixelDeLieu(new Lieu(dlg.Lon, dlg.Lat));
	         		okP = true;
	        		
	        		// Déplacement de la carte pour placer le point au centre de la zone client
	        		CentrerCarte(p);
			    	Invalidate();	
		        		
					// Affichage 				
					tsslLon.Text = "Lon : " + dlg.Lon.ToString("#.####0°");
					tsslLat.Text = "Lat : " + dlg.Lat.ToString("#.####0°");	
					tsslAlt.Text = "Alt";			
					tsslSat.Text = "Sat";
					tsslHDOP.Text = "HDOP";
					break;
	        		
	        	case (DialogResult.No) :
	        		okP = false;
			    	Invalidate();	
					tsslLon.Text = "Lon";
					tsslLat.Text = "Lat";	
					tsslAlt.Text = "Alt";			
					tsslSat.Text = "Sat";
					tsslHDOP.Text = "HDOP";
					break;
         	}
        	dlg.Dispose();	
		}
		
		private void PortOpen()
		{
			try
			{
				port.Open();
				tsslIco.Image = Resource.ok;
			} 
			catch
			{
			}
			
		}
		
		private void PortClose()
		{
			try
			{
				if (port.IsOpen) port.Close();
				tsslIco.Image = Resource.ko;
				if (!okP)
				{
					tsslLon.Text = "Lon";
					tsslLat.Text = "Lat";	
				}
				tsslAlt.Text = "Alt";			
				tsslSat.Text = "Sat";
				tsslHDOP.Text = "HDOP";
			} 
			catch
			{
			}
		}
		
		protected override void WndProc(ref Message m)
		{
			const int WM_DEVICECHANGE = 0x0219;
			const int DBT_DEVICEARRIVAL = 0x8000;
			const int DBT_DEVICEREMOVECOMPLETE = 0x8004;
			
			if (m.Msg == WM_DEVICECHANGE)
			{
				PortOpen();
				switch (m.WParam.ToInt32())
				{
					case DBT_DEVICEARRIVAL :
						PortOpen();
						break;
					case DBT_DEVICEREMOVECOMPLETE :
						PortClose();
						break;						
				}
			}
			base.WndProc(ref m);
		}
	}
	
	struct InfoCalage
	{
		public double Lon;
		public double Lat;
		public int Col;
		public int Lig;
	}
	
	class Pixel
	{
		public int Col;
		public int Lig;
		
		public Pixel(int col, int lig)
		{
			Col = col;
			Lig = lig;
		}
		
		public Pixel(Pixel px)
		{			
			Col = px.Col;
			Lig = px.Lig;
		}
		
		public static double Distance(Pixel p1, Pixel p2)
		{
 			return Math.Sqrt(Math.Pow(p1.Col - p2.Col, 2) + Math.Pow(p1.Lig - p2.Lig, 2));			
		}
	}
	
	
    public class Lieu
	{
		public double Lon;
		public double Lat;
		
		public Lieu(double lon, double lat)
		{
			Lon = lon;
			Lat = lat;
		}
		
		public Lieu(Lieu l)
		{
			Lon = l.Lon;
			Lat = l.Lat;
		}	
		
		public static double Distance(Lieu l1, Lieu l2)
		{
    		const double rad = 0.01745329251994329576923690768489; // pi/180
    		const double a = 6378137;					           // demi-grand axe de l'ellipsoïde WGS84
    		const double b = 6356752.3142;						   // demi-petit axe de l'ellipsoïde WGS84
    		double x =  Math.Cos(l1.Lat*rad)*Math.Cos(l2.Lat*rad)*Math.Cos((l2.Lon-l1.Lon)*rad);
    		x += Math.Sin(l1.Lat*rad)*Math.Sin(l2.Lat*rad);
    		double r = (2*a + b)/3;
     		return r*Math.Acos(x);
		}			
	}
    
    
}
