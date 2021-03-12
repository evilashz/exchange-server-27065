using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001C5 RID: 453
	public partial class CustomProxyAddressDialog : CustomAddressDialog
	{
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x0004D282 File Offset: 0x0004B482
		protected override ProxyAddressBaseDataHandler DataHandler
		{
			get
			{
				if (this.dataHandler == null)
				{
					this.dataHandler = new ProxyAddressDataHandler();
				}
				return this.dataHandler;
			}
		}

		// Token: 0x04000717 RID: 1815
		private ProxyAddressDataHandler dataHandler;
	}
}
