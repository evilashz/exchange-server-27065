using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000180 RID: 384
	[Serializable]
	public sealed class EsentReadLostFlushVerifyFailureException : EsentCorruptionException
	{
		// Token: 0x06000913 RID: 2323 RVA: 0x00012A92 File Offset: 0x00010C92
		public EsentReadLostFlushVerifyFailureException() : base("The database page read from disk had a previous write not represented on the page.", JET_err.ReadLostFlushVerifyFailure)
		{
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00012AA4 File Offset: 0x00010CA4
		private EsentReadLostFlushVerifyFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
