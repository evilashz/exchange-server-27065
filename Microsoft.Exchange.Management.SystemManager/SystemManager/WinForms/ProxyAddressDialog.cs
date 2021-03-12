using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001C1 RID: 449
	public abstract partial class ProxyAddressDialog : ExchangeDialog
	{
		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x0004C90D File Offset: 0x0004AB0D
		public ExchangePage ContentPage
		{
			get
			{
				return this.exchangePage;
			}
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0004C918 File Offset: 0x0004AB18
		public ProxyAddressDialog()
		{
			this.InitializeComponent();
			this.AutoSize = true;
			base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.MinimumSize = new Size(443, 0);
			this.MaximumSize = new Size(443, 1024);
			this.exchangePage.BindingSource.DataSource = typeof(ProxyAddressBaseDataHandler);
			this.exchangePage.Context = new DataContext(this.DataHandler);
			this.exchangePage.OnSetActive();
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060012EF RID: 4847
		protected abstract ProxyAddressBaseDataHandler DataHandler { get; }

		// Token: 0x060012F0 RID: 4848 RVA: 0x0004CA01 File Offset: 0x0004AC01
		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			if (base.DialogResult == DialogResult.OK)
			{
				this.exchangePage.InputValidationProvider.WriteBindings();
				e.Cancel = !this.exchangePage.OnKillActive();
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x0004CA37 File Offset: 0x0004AC37
		// (set) Token: 0x060012F2 RID: 4850 RVA: 0x0004CA44 File Offset: 0x0004AC44
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public ProxyAddressBase ProxyAddressBase
		{
			get
			{
				return this.DataHandler.ProxyAddressBase;
			}
			set
			{
				this.DataHandler.ProxyAddressBase = value;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x0004CA52 File Offset: 0x0004AC52
		// (set) Token: 0x060012F4 RID: 4852 RVA: 0x0004CA5F File Offset: 0x0004AC5F
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(null)]
		public ProxyAddressBase OriginalProxyAddressBase
		{
			get
			{
				return this.DataHandler.OriginalProxyAddressBase;
			}
			set
			{
				this.DataHandler.OriginalProxyAddressBase = value;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060012F5 RID: 4853 RVA: 0x0004CA6D File Offset: 0x0004AC6D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public List<ProxyAddressBase> ProxyAddresses
		{
			get
			{
				return this.DataHandler.ProxyAddresses;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x0004CA7A File Offset: 0x0004AC7A
		// (set) Token: 0x060012F7 RID: 4855 RVA: 0x0004CA87 File Offset: 0x0004AC87
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string Prefix
		{
			get
			{
				return this.DataHandler.Prefix;
			}
			set
			{
				this.DataHandler.Prefix = value;
			}
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0004CA95 File Offset: 0x0004AC95
		protected virtual UIValidationError[] GetValidationErrors()
		{
			return UIValidationError.None;
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x0004CA9C File Offset: 0x0004AC9C
		protected void UpdateError()
		{
			this.exchangePage.UpdateError();
		}

		// Token: 0x020001C2 RID: 450
		private class ProxyAddressContentPage : ExchangePage
		{
			// Token: 0x060012FA RID: 4858 RVA: 0x0004CAA9 File Offset: 0x0004ACA9
			public ProxyAddressContentPage() : this(null)
			{
			}

			// Token: 0x060012FB RID: 4859 RVA: 0x0004CAB2 File Offset: 0x0004ACB2
			public ProxyAddressContentPage(ProxyAddressDialog dialog)
			{
				base.Name = "ProxyAddressContentPage";
				this.proxyAddressDialog = dialog;
			}

			// Token: 0x060012FC RID: 4860 RVA: 0x0004CACC File Offset: 0x0004ACCC
			protected override UIValidationError[] GetValidationErrors()
			{
				return this.proxyAddressDialog.GetValidationErrors();
			}

			// Token: 0x0400070F RID: 1807
			private ProxyAddressDialog proxyAddressDialog;
		}
	}
}
