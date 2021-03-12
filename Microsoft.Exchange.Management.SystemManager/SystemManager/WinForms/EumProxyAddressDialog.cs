using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001E8 RID: 488
	public partial class EumProxyAddressDialog : ProxyAddressDialog
	{
		// Token: 0x060015DE RID: 5598 RVA: 0x0005A0D4 File Offset: 0x000582D4
		public EumProxyAddressDialog()
		{
			this.InitializeComponent();
			this.Text = Strings.EumProxyAddress;
			this.addressLabel.Text = Strings.AddressOrExtension;
			this.dialplanLabel.Text = Strings.DialPlanPhoneContext;
			base.Prefix = ProxyAddressPrefix.UM.PrimaryPrefix;
			base.ContentPage.BindingSource.DataSource = typeof(EumProxyAddressDataHandler);
			this.addressTextBox.DataBindings.Add("Text", base.ContentPage.BindingSource, "Extension");
			this.addressTextBox.TextChanged += delegate(object param0, EventArgs param1)
			{
				base.UpdateError();
			};
			this.dialplanPickerLauncherTextBox.Picker = new AutomatedObjectPicker("DialPlanConfigurable");
			this.dialplanPickerLauncherTextBox.ValueMember = "PhoneContext";
			this.dialplanPickerLauncherTextBox.DataBindings.Add("SelectedValue", base.ContentPage.BindingSource, "PhoneContext", true, DataSourceUpdateMode.OnPropertyChanged);
			this.dialplanPickerLauncherTextBox.SelectedValueChanged += delegate(object param0, EventArgs param1)
			{
				base.UpdateError();
			};
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x0005A200 File Offset: 0x00058400
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			base.ContentPage.InputValidationProvider.SetUIValidationEnabled(base.ContentPage, true);
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x0005A220 File Offset: 0x00058420
		protected override UIValidationError[] GetValidationErrors()
		{
			List<UIValidationError> list = new List<UIValidationError>(UIValidationError.None);
			if (this.addressTextBox.TextLength + this.dialplanPickerLauncherTextBox.SelectedValue.ToString().Length > ProxyAddressBase.MaxLength - EumProxyAddressDataHandlerSchema.FixedLength)
			{
				list.Add(new UIValidationError(Strings.ExceedMaxLimit, this.addressTextBox));
			}
			if (string.IsNullOrEmpty(this.dialplanPickerLauncherTextBox.Text))
			{
				list.Add(new UIValidationError(Strings.SelectValueErrorMessage, this.dialplanPickerLauncherTextBox));
			}
			return list.ToArray();
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060015E1 RID: 5601 RVA: 0x0005A2AA File Offset: 0x000584AA
		protected override ProxyAddressBaseDataHandler DataHandler
		{
			get
			{
				if (this.dataHandler == null)
				{
					this.dataHandler = new EumProxyAddressDataHandler();
				}
				return this.dataHandler;
			}
		}

		// Token: 0x040007F0 RID: 2032
		private ProxyAddressDataHandler dataHandler;
	}
}
