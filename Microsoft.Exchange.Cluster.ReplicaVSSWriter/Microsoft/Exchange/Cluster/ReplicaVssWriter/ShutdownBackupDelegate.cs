using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Cluster.ReplicaVssWriter
{
	// Token: 0x0200013A RID: 314
	// (Invoke) Token: 0x060000B5 RID: 181
	[return: MarshalAs(UnmanagedType.U1)]
	internal delegate bool ShutdownBackupDelegate(_GUID snapshotSetId);
}
