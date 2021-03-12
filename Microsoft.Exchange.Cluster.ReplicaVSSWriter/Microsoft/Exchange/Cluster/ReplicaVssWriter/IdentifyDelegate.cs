using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Cluster.ReplicaVssWriter
{
	// Token: 0x02000133 RID: 307
	// (Invoke) Token: 0x06000099 RID: 153
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe delegate bool IdentifyDelegate(IVssCreateWriterMetadata* pMetadata);
}
