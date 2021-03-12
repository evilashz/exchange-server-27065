using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000153 RID: 339
	[Serializable]
	public sealed class EsentNotInTransactionException : EsentUsageException
	{
		// Token: 0x060008B9 RID: 2233 RVA: 0x000125A6 File Offset: 0x000107A6
		public EsentNotInTransactionException() : base("Operation must be within a transaction", JET_err.NotInTransaction)
		{
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x000125B8 File Offset: 0x000107B8
		private EsentNotInTransactionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
