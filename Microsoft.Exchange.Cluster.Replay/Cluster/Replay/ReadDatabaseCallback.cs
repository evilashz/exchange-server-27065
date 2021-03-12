using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000246 RID: 582
	// (Invoke) Token: 0x0600162E RID: 5678
	internal delegate int ReadDatabaseCallback(byte[] buffer, ulong fileReadOffset, int bytesToRead);
}
