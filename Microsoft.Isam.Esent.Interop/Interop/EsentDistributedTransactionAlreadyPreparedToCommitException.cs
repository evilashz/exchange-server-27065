using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000185 RID: 389
	[Serializable]
	public sealed class EsentDistributedTransactionAlreadyPreparedToCommitException : EsentObsoleteException
	{
		// Token: 0x0600091D RID: 2333 RVA: 0x00012B1E File Offset: 0x00010D1E
		public EsentDistributedTransactionAlreadyPreparedToCommitException() : base("Attempted a write-operation after a distributed transaction has called PrepareToCommit", JET_err.DistributedTransactionAlreadyPreparedToCommit)
		{
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00012B30 File Offset: 0x00010D30
		private EsentDistributedTransactionAlreadyPreparedToCommitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
