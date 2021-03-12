using System;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x0200002D RID: 45
	public static class WTFLog
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000AECB File Offset: 0x000090CB
		public static WTFLogComponent Core
		{
			get
			{
				return WTFLog.coreLogger;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000AED2 File Offset: 0x000090D2
		public static WTFLogComponent DataAccess
		{
			get
			{
				return WTFLog.dataAccessLogger;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000AED9 File Offset: 0x000090D9
		public static WTFLogComponent WorkItem
		{
			get
			{
				return WTFLog.workItemLogger;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000AEE0 File Offset: 0x000090E0
		public static WTFLogComponent ManagedAvailability
		{
			get
			{
				return WTFLog.managedAvailabilityLogger;
			}
		}

		// Token: 0x04000114 RID: 276
		private static readonly Guid componentGuid = new Guid("EAF36C57-87B9-4D84-B551-3537A14A62B8");

		// Token: 0x04000115 RID: 277
		private static readonly WTFLogComponent coreLogger = new WTFLogComponent(WTFLog.componentGuid, 1, "Core", true);

		// Token: 0x04000116 RID: 278
		private static readonly WTFLogComponent dataAccessLogger = new WTFLogComponent(WTFLog.componentGuid, 2, "DataAccess", true);

		// Token: 0x04000117 RID: 279
		private static readonly WTFLogComponent workItemLogger = new WTFLogComponent(WTFLog.componentGuid, 3, "WorkItem", true);

		// Token: 0x04000118 RID: 280
		private static readonly WTFLogComponent managedAvailabilityLogger = new WTFLogComponent(WTFLog.componentGuid, 4, "ManagedAvailability", true);
	}
}
