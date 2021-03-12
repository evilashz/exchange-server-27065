using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200020B RID: 523
	[Serializable]
	public sealed class EsentRecordFormatConversionFailedException : EsentCorruptionException
	{
		// Token: 0x06000A29 RID: 2601 RVA: 0x000139C6 File Offset: 0x00011BC6
		public EsentRecordFormatConversionFailedException() : base("Internal error during dynamic record format conversion", JET_err.RecordFormatConversionFailed)
		{
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x000139D8 File Offset: 0x00011BD8
		private EsentRecordFormatConversionFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
