using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000188 RID: 392
	[Serializable]
	public sealed class EsentCannotNestDistributedTransactionsException : EsentObsoleteException
	{
		// Token: 0x06000923 RID: 2339 RVA: 0x00012B72 File Offset: 0x00010D72
		public EsentCannotNestDistributedTransactionsException() : base("Attempted to begin a distributed transaction when not at level 0", JET_err.CannotNestDistributedTransactions)
		{
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00012B84 File Offset: 0x00010D84
		private EsentCannotNestDistributedTransactionsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
