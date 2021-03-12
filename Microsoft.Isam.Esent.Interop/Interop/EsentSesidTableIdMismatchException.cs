using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200017C RID: 380
	[Serializable]
	public sealed class EsentSesidTableIdMismatchException : EsentUsageException
	{
		// Token: 0x0600090B RID: 2315 RVA: 0x00012A22 File Offset: 0x00010C22
		public EsentSesidTableIdMismatchException() : base("This session handle can't be used with this table id", JET_err.SesidTableIdMismatch)
		{
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00012A34 File Offset: 0x00010C34
		private EsentSesidTableIdMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
