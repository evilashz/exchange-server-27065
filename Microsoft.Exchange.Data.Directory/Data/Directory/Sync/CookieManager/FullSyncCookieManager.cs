using System;

namespace Microsoft.Exchange.Data.Directory.Sync.CookieManager
{
	// Token: 0x020007CF RID: 1999
	internal abstract class FullSyncCookieManager : CookieManager
	{
		// Token: 0x1700234D RID: 9037
		// (get) Token: 0x06006354 RID: 25428 RVA: 0x00158ADE File Offset: 0x00156CDE
		// (set) Token: 0x06006355 RID: 25429 RVA: 0x00158AE6 File Offset: 0x00156CE6
		public MsoFullSyncCookie LastCookie { get; protected set; }

		// Token: 0x06006356 RID: 25430 RVA: 0x00158AF0 File Offset: 0x00156CF0
		protected FullSyncCookieManager(Guid contextId)
		{
			if (Guid.Empty.Equals(contextId))
			{
				throw new ArgumentException("contextId is Guid.Empty.");
			}
			this.ContextId = contextId;
		}

		// Token: 0x1700234E RID: 9038
		// (get) Token: 0x06006357 RID: 25431 RVA: 0x00158B25 File Offset: 0x00156D25
		// (set) Token: 0x06006358 RID: 25432 RVA: 0x00158B2D File Offset: 0x00156D2D
		public Guid ContextId { get; private set; }

		// Token: 0x06006359 RID: 25433
		public abstract void ClearCookie();
	}
}
