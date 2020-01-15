/*
 * Crée par SharpDevelop.
 * Utilisateur: Jac
 * Date: 25/04/2016
 * Heure: 18:20
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
namespace CartoGPS
{
	partial class DlgCoord
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.buttonPositionner = new System.Windows.Forms.Button();
			this.buttonAnnuler = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxLon = new System.Windows.Forms.TextBox();
			this.textBoxLat = new System.Windows.Forms.TextBox();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
			this.buttonSupprimer = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonPositionner
			// 
			this.buttonPositionner.CausesValidation = false;
			this.buttonPositionner.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonPositionner.Location = new System.Drawing.Point(12, 68);
			this.buttonPositionner.Name = "buttonPositionner";
			this.buttonPositionner.Size = new System.Drawing.Size(75, 23);
			this.buttonPositionner.TabIndex = 3;
			this.buttonPositionner.Text = "Positionner";
			this.buttonPositionner.UseVisualStyleBackColor = true;
			// 
			// buttonAnnuler
			// 
			this.buttonAnnuler.CausesValidation = false;
			this.buttonAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonAnnuler.Location = new System.Drawing.Point(174, 68);
			this.buttonAnnuler.Name = "buttonAnnuler";
			this.buttonAnnuler.Size = new System.Drawing.Size(75, 23);
			this.buttonAnnuler.TabIndex = 4;
			this.buttonAnnuler.Text = "Annuler";
			this.buttonAnnuler.UseVisualStyleBackColor = true;
			this.buttonAnnuler.Click += new System.EventHandler(this.ButtonAnnulerClick);
			// 
			// label1
			// 
			this.label1.CausesValidation = false;
			this.label1.Location = new System.Drawing.Point(12, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(85, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Longitude (d°) :";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.CausesValidation = false;
			this.label2.Location = new System.Drawing.Point(12, 42);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(85, 23);
			this.label2.TabIndex = 0;
			this.label2.Text = "Latitude (d°) :";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textBoxLon
			// 
			this.textBoxLon.Location = new System.Drawing.Point(103, 16);
			this.textBoxLon.Name = "textBoxLon";
			this.textBoxLon.Size = new System.Drawing.Size(100, 20);
			this.textBoxLon.TabIndex = 1;
			this.textBoxLon.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxLonValidating);
			// 
			// textBoxLat
			// 
			this.textBoxLat.Location = new System.Drawing.Point(103, 39);
			this.textBoxLat.Name = "textBoxLat";
			this.textBoxLat.Size = new System.Drawing.Size(100, 20);
			this.textBoxLat.TabIndex = 2;
			this.textBoxLat.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxLatValidating);
			// 
			// errorProvider1
			// 
			this.errorProvider1.BlinkRate = 100;
			this.errorProvider1.ContainerControl = this;
			// 
			// buttonSupprimer
			// 
			this.buttonSupprimer.CausesValidation = false;
			this.buttonSupprimer.DialogResult = System.Windows.Forms.DialogResult.No;
			this.buttonSupprimer.Location = new System.Drawing.Point(93, 68);
			this.buttonSupprimer.Name = "buttonSupprimer";
			this.buttonSupprimer.Size = new System.Drawing.Size(75, 23);
			this.buttonSupprimer.TabIndex = 6;
			this.buttonSupprimer.Text = "Supprimer";
			this.buttonSupprimer.UseVisualStyleBackColor = true;
			this.buttonSupprimer.Click += new System.EventHandler(this.ButtonSupprimerClick);
			// 
			// DlgCoord
			// 
			this.AcceptButton = this.buttonPositionner;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonAnnuler;
			this.ClientSize = new System.Drawing.Size(260, 103);
			this.ControlBox = false;
			this.Controls.Add(this.buttonSupprimer);
			this.Controls.Add(this.textBoxLat);
			this.Controls.Add(this.textBoxLon);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonAnnuler);
			this.Controls.Add(this.buttonPositionner);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DlgCoord";
			this.Text = "Coordonnées GPS";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DialogCoordFormClosing);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		private System.Windows.Forms.Button buttonPositionner;
		private System.Windows.Forms.Button buttonAnnuler;
		private System.Windows.Forms.TextBox textBoxLon;
		private System.Windows.Forms.TextBox textBoxLat;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonSupprimer;
	}
}
