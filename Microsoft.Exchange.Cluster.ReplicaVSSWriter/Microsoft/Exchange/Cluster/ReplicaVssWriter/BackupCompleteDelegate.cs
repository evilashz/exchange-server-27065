using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Cluster.ReplicaVssWriter
{
	// Token: 0x02000139 RID: 313
	// (Invoke) Token: 0x060000B1 RID: 177
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe delegate bool BackupCompleteDelegate(IVssWriterComponents* pComponents);
}
