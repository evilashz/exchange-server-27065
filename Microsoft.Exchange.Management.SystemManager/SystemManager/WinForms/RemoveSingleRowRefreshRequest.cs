using System;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200015B RID: 347
	public sealed class RemoveSingleRowRefreshRequest : SingleRowRefreshRequest
	{
		// Token: 0x06000E10 RID: 3600 RVA: 0x00035537 File Offset: 0x00033737
		public RemoveSingleRowRefreshRequest(object refreshCategory, object identity) : base(refreshCategory, identity)
		{
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00035544 File Offset: 0x00033744
		public override void DoRefresh(IRefreshable refreshableDataSource, IProgress progress)
		{
			ISupportFastRefresh supportFastRefresh = refreshableDataSource as ISupportFastRefresh;
			if (supportFastRefresh == null)
			{
				throw new InvalidOperationException();
			}
			supportFastRefresh.Remove(base.ObjectIdentity);
			if (progress != null)
			{
				progress.ReportProgress(100, 100, "");
			}
		}
	}
}
