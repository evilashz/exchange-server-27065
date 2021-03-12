using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000187 RID: 391
	[Serializable]
	public sealed class EsentDistributedTransactionNotYetPreparedToCommitException : EsentObsoleteException
	{
		// Token: 0x06000921 RID: 2337 RVA: 0x00012B56 File Offset: 0x00010D56
		public EsentDistributedTransactionNotYetPreparedToCommitException() : base("Attempted to commit a distributed transaction, but PrepareToCommit has not yet been called", JET_err.DistributedTransactionNotYetPreparedToCommit)
		{
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00012B68 File Offset: 0x00010D68
		private EsentDistributedTransactionNotYetPreparedToCommitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
