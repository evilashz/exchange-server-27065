using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200017F RID: 383
	[Serializable]
	public sealed class EsentReadPgnoVerifyFailureException : EsentCorruptionException
	{
		// Token: 0x06000911 RID: 2321 RVA: 0x00012A76 File Offset: 0x00010C76
		public EsentReadPgnoVerifyFailureException() : base("The database page read from disk had the wrong page number.", JET_err.ReadPgnoVerifyFailure)
		{
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00012A88 File Offset: 0x00010C88
		private EsentReadPgnoVerifyFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
