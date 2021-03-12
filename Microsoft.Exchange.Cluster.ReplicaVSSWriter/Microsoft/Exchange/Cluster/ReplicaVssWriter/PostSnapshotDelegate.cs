using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Cluster.ReplicaVssWriter
{
	// Token: 0x02000138 RID: 312
	// (Invoke) Token: 0x060000AD RID: 173
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe delegate bool PostSnapshotDelegate(IVssWriterComponents* pComponents);
}
