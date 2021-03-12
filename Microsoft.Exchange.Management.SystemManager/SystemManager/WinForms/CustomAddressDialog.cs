using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001C3 RID: 451
	public abstract partial class CustomAddressDialog : ProxyAddressDialog
	{
		// Token: 0x060012FD RID: 4861 RVA: 0x0004CB64 File Offset: 0x0004AD64
		public CustomAddressDialog()
		{
			this.InitializeComponent();
			this.Text = Strings.CustomAddress;
			this.addressLabel.Text = Strings.ProxyAddressLabel;
			this.prefixLabel.Text = Strings.ProxyAddressPrefixLabel;
			this.addressTextBox.DataBindings.Add("Text", base.ContentPage.BindingSource, "Address");
			this.prefixTextBox.DataBindings.Add("Text", base.ContentPage.BindingSource, "Prefix");
			this.addressTextBox.TextChanged += delegate(object param0, EventArgs param1)
			{
				int val = ProxyAddressBase.MaxLength - 1 - ((this.addressTextBox.Text.Length == 0) ? 1 : this.addressTextBox.Text.Length);
				this.prefixTextBox.MaxLength = Math.Min(9, val);
			};
			this.prefixTextBox.TextChanged += delegate(object param0, EventArgs param1)
			{
				this.addressTextBox.MaxLength = ProxyAddressBase.MaxLength - 1 - ((this.prefixTextBox.Text.Length == 0) ? 1 : this.prefixTextBox.Text.Length);
			};
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0004CC3F File Offset: 0x0004AE3F
		protected override void OnShown(EventArgs e)
		{
			this.addressTextBox.SelectAll();
			base.OnShown(e);
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x0004D139 File Offset: 0x0004B339
		// (set) Token: 0x06001301 RID: 4865 RVA: 0x0004D146 File Offset: 0x0004B346
		[DefaultValue(false)]
		public bool IsPrefixTextBoxReadOnly
		{
			get
			{
				return this.prefixTextBox.ReadOnly;
			}
			set
			{
				this.prefixTextBox.ReadOnly = value;
			}
		}
	}
}
