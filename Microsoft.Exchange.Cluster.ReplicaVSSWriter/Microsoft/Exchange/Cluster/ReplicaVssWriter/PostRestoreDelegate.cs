using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Cluster.ReplicaVssWriter
{
	// Token: 0x0200013C RID: 316
	// (Invoke) Token: 0x060000BD RID: 189
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe delegate bool PostRestoreDelegate(IVssWriterComponents* pComponents);
}
