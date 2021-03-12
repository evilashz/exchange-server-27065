using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001C6 RID: 454
	public partial class CustomProxyAddressTemplateDialog : CustomAddressDialog
	{
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x0004D2A5 File Offset: 0x0004B4A5
		protected override ProxyAddressBaseDataHandler DataHandler
		{
			get
			{
				if (this.dataHandler == null)
				{
					this.dataHandler = new ProxyAddressTemplateDataHandler();
				}
				return this.dataHandler;
			}
		}

		// Token: 0x04000718 RID: 1816
		private ProxyAddressTemplateDataHandler dataHandler;
	}
}
