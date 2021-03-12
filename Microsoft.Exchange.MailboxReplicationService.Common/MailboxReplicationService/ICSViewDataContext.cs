using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000FC RID: 252
	internal class ICSViewDataContext : DataContext
	{
		// Token: 0x06000934 RID: 2356 RVA: 0x00012787 File Offset: 0x00010987
		public ICSViewDataContext(ICSViewData icsViewData)
		{
			this.icsViewData = icsViewData;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00012796 File Offset: 0x00010996
		public override string ToString()
		{
			return string.Format("ICSView: {0}", (this.icsViewData != null) ? this.icsViewData.ToString() : "(null)");
		}

		// Token: 0x04000562 RID: 1378
		private ICSViewData icsViewData;
	}
}
