using System;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000B4 RID: 180
	internal interface IDataObjectComponent
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000622 RID: 1570
		bool PendingDatabaseUpdates { get; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000623 RID: 1571
		int PendingDatabaseUpdateCount { get; }

		// Token: 0x06000624 RID: 1572
		void MinimizeMemory();

		// Token: 0x06000625 RID: 1573
		void CloneFrom(IDataObjectComponent other);
	}
}
