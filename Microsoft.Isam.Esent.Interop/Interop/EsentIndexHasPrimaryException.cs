using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001C0 RID: 448
	[Serializable]
	public sealed class EsentIndexHasPrimaryException : EsentUsageException
	{
		// Token: 0x06000993 RID: 2451 RVA: 0x00013192 File Offset: 0x00011392
		public EsentIndexHasPrimaryException() : base("Primary index already defined", JET_err.IndexHasPrimary)
		{
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x000131A4 File Offset: 0x000113A4
		private EsentIndexHasPrimaryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
