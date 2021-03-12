using System;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200015A RID: 346
	public class MultiRowRefreshRequest : PartialRefreshRequest
	{
		// Token: 0x06000E0E RID: 3598 RVA: 0x000354FA File Offset: 0x000336FA
		public MultiRowRefreshRequest(object refreshCategory, object[] identities) : base(refreshCategory)
		{
			this.identities = identities;
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0003550C File Offset: 0x0003370C
		public override void DoRefresh(IRefreshable refreshableDataSource, IProgress progress)
		{
			ISupportFastRefresh supportFastRefresh = refreshableDataSource as ISupportFastRefresh;
			if (supportFastRefresh == null)
			{
				throw new InvalidOperationException();
			}
			supportFastRefresh.Refresh(progress, this.identities, 0);
		}

		// Token: 0x04000597 RID: 1431
		private readonly object[] identities;
	}
}
