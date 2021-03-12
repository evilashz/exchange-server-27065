using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Cluster.ReplicaVssWriter
{
	// Token: 0x02000135 RID: 309
	// (Invoke) Token: 0x060000A1 RID: 161
	[return: MarshalAs(UnmanagedType.U1)]
	internal delegate bool PrepareSnapshotDelegate();
}
