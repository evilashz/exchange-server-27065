using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000127 RID: 295
	[Serializable]
	public sealed class EsentTransactionTooLongException : EsentQuotaException
	{
		// Token: 0x06000861 RID: 2145 RVA: 0x000120D6 File Offset: 0x000102D6
		public EsentTransactionTooLongException() : base("Too many outstanding generations between JetBeginTransaction and current generation.", JET_err.TransactionTooLong)
		{
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000120E8 File Offset: 0x000102E8
		private EsentTransactionTooLongException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
