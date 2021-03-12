using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001EF RID: 495
	[Serializable]
	public sealed class EsentNoCurrentRecordException : EsentStateException
	{
		// Token: 0x060009F1 RID: 2545 RVA: 0x000136B6 File Offset: 0x000118B6
		public EsentNoCurrentRecordException() : base("Currency not on a record", JET_err.NoCurrentRecord)
		{
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x000136C8 File Offset: 0x000118C8
		private EsentNoCurrentRecordException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
