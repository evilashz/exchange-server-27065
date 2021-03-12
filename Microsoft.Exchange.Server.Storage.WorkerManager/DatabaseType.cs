using System;

namespace Microsoft.Exchange.Server.Storage.WorkerManager
{
	// Token: 0x02000003 RID: 3
	public enum DatabaseType : byte
	{
		// Token: 0x04000002 RID: 2
		None,
		// Token: 0x04000003 RID: 3
		Active,
		// Token: 0x04000004 RID: 4
		Passive,
		// Token: 0x04000005 RID: 5
		Recovery
	}
}
