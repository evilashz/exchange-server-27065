using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Setup.Bootstrapper.Common;

namespace Microsoft.Exchange.Bootstrapper.Setup
{
	// Token: 0x02000005 RID: 5
	public partial class DialogBoxForm : Form
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00003200 File Offset: 0x00001400
		public DialogBoxForm(string localizedString)
		{
			this.InitializeComponent();
			this.okButton.Text = Strings.ButtonTexkOk;
			this.Text = Strings.MessageHeaderText;
			this.statusText.Text = localizedString;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000325E File Offset: 0x0000145E
		private void OkButton_Click(object sender, EventArgs e)
		{
			base.Close();
		}
	}
}
