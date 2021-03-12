using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000186 RID: 390
	[Serializable]
	public sealed class EsentNotInDistributedTransactionException : EsentObsoleteException
	{
		// Token: 0x0600091F RID: 2335 RVA: 0x00012B3A File Offset: 0x00010D3A
		public EsentNotInDistributedTransactionException() : base("Attempted to PrepareToCommit a non-distributed transaction", JET_err.NotInDistributedTransaction)
		{
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00012B4C File Offset: 0x00010D4C
		private EsentNotInDistributedTransactionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
