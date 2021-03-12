using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200013F RID: 319
	[Serializable]
	public sealed class EsentRecordTooBigException : EsentStateException
	{
		// Token: 0x06000891 RID: 2193 RVA: 0x00012376 File Offset: 0x00010576
		public EsentRecordTooBigException() : base("Record larger than maximum size", JET_err.RecordTooBig)
		{
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00012388 File Offset: 0x00010588
		private EsentRecordTooBigException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
