using System;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000158 RID: 344
	public abstract class PartialRefreshRequest
	{
		// Token: 0x06000E06 RID: 3590 RVA: 0x0003543E File Offset: 0x0003363E
		public PartialRefreshRequest(object refreshCategory)
		{
			this.refreshCategory = refreshCategory;
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x0003544D File Offset: 0x0003364D
		public object RefreshCategory
		{
			get
			{
				return this.refreshCategory;
			}
		}

		// Token: 0x06000E08 RID: 3592
		public abstract void DoRefresh(IRefreshable refreshableDataSource, IProgress progress);

		// Token: 0x04000595 RID: 1429
		private readonly object refreshCategory;
	}
}
