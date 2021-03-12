using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001FD RID: 509
	[Serializable]
	public sealed class EsentDiskFullException : EsentDiskException
	{
		// Token: 0x06000A0D RID: 2573 RVA: 0x0001383E File Offset: 0x00011A3E
		public EsentDiskFullException() : base("No space left on disk", JET_err.DiskFull)
		{
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00013850 File Offset: 0x00011A50
		private EsentDiskFullException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
