using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200013B RID: 315
	[Serializable]
	public sealed class EsentDiskIOException : EsentIOException
	{
		// Token: 0x06000889 RID: 2185 RVA: 0x00012306 File Offset: 0x00010506
		public EsentDiskIOException() : base("Disk IO error", JET_err.DiskIO)
		{
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00012318 File Offset: 0x00010518
		private EsentDiskIOException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
