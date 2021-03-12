using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020002E6 RID: 742
	public struct WorkerTaskFrameworkTags
	{
		// Token: 0x04001379 RID: 4985
		public const int FaultInjection = 0;

		// Token: 0x0400137A RID: 4986
		public const int Core = 1;

		// Token: 0x0400137B RID: 4987
		public const int DataAccess = 2;

		// Token: 0x0400137C RID: 4988
		public const int WorkItem = 3;

		// Token: 0x0400137D RID: 4989
		public const int ManagedAvailability = 4;

		// Token: 0x0400137E RID: 4990
		public static Guid guid = new Guid("EAF36C57-87B9-4D84-B551-3537A14A62B8");
	}
}
