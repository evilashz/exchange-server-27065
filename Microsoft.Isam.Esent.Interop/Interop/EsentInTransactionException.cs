using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000176 RID: 374
	[Serializable]
	public sealed class EsentInTransactionException : EsentUsageException
	{
		// Token: 0x060008FF RID: 2303 RVA: 0x0001297A File Offset: 0x00010B7A
		public EsentInTransactionException() : base("Operation not allowed within a transaction", JET_err.InTransaction)
		{
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0001298C File Offset: 0x00010B8C
		private EsentInTransactionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
