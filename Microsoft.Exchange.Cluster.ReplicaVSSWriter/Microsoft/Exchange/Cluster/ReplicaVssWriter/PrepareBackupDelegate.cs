using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Cluster.ReplicaVssWriter
{
	// Token: 0x02000134 RID: 308
	// (Invoke) Token: 0x0600009D RID: 157
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe delegate bool PrepareBackupDelegate(IVssWriterComponents* pComponents);
}
