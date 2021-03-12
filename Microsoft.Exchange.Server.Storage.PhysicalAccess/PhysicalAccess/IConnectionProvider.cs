using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000017 RID: 23
	public interface IConnectionProvider
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000BE RID: 190
		Database Database { get; }

		// Token: 0x060000BF RID: 191
		Connection GetConnection();
	}
}
