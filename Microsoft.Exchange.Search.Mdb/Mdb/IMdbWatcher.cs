using System;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x02000015 RID: 21
	internal interface IMdbWatcher : IDisposable
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000067 RID: 103
		// (remove) Token: 0x06000068 RID: 104
		event EventHandler Changed;

		// Token: 0x06000069 RID: 105
		IMdbCollection GetDatabases();

		// Token: 0x0600006A RID: 106
		bool Exists(Guid mdbGuid);
	}
}
