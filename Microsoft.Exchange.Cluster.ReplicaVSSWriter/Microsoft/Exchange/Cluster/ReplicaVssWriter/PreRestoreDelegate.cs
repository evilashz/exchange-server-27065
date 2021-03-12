using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Cluster.ReplicaVssWriter
{
	// Token: 0x0200013B RID: 315
	// (Invoke) Token: 0x060000B9 RID: 185
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe delegate bool PreRestoreDelegate(IVssWriterComponents* pComponents);
}
