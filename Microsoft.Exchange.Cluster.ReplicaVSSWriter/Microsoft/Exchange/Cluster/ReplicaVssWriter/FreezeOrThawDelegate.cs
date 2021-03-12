using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Cluster.ReplicaVssWriter
{
	// Token: 0x02000136 RID: 310
	// (Invoke) Token: 0x060000A5 RID: 165
	internal delegate int FreezeOrThawDelegate([MarshalAs(UnmanagedType.U1)] bool fFreeze, [MarshalAs(UnmanagedType.U1)] bool fLock);
}
