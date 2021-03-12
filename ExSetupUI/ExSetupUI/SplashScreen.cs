using System;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x02000028 RID: 40
	internal partial class SplashScreen : Form
	{
		// Token: 0x060001DD RID: 477 RVA: 0x0000AE81 File Offset: 0x00009081
		private SplashScreen()
		{
			this.InitializeComponent();
			this.statusLabel.Text = Strings.SplashText;
			base.Width = this.statusLabel.Width + 10;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000AEB8 File Offset: 0x000090B8
		public void ShowSplash()
		{
			base.FormBorderStyle = FormBorderStyle.None;
			base.StartPosition = FormStartPosition.CenterScreen;
			base.TopMost = true;
			base.Show();
			base.TopMost = false;
			Application.DoEvents();
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000AEE1 File Offset: 0x000090E1
		public void CloseSplash()
		{
			base.Close();
		}

		// Token: 0x04000107 RID: 263
		public static readonly SplashScreen SplashInstance = new SplashScreen();
	}
}
