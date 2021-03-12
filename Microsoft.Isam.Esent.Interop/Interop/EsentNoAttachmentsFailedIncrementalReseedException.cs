using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001A7 RID: 423
	[Serializable]
	public sealed class EsentNoAttachmentsFailedIncrementalReseedException : EsentStateException
	{
		// Token: 0x06000961 RID: 2401 RVA: 0x00012ED6 File Offset: 0x000110D6
		public EsentNoAttachmentsFailedIncrementalReseedException() : base("The incremental reseed being performed on the specified database cannot be completed because the min required log contains no attachment info.  A full reseed is required to recover this database.", JET_err.NoAttachmentsFailedIncrementalReseed)
		{
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00012EE8 File Offset: 0x000110E8
		private EsentNoAttachmentsFailedIncrementalReseedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
