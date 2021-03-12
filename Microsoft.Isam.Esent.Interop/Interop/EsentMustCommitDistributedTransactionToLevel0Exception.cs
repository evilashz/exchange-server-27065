using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000184 RID: 388
	[Serializable]
	public sealed class EsentMustCommitDistributedTransactionToLevel0Exception : EsentObsoleteException
	{
		// Token: 0x0600091B RID: 2331 RVA: 0x00012B02 File Offset: 0x00010D02
		public EsentMustCommitDistributedTransactionToLevel0Exception() : base("Attempted to PrepareToCommit a distributed transaction to non-zero level", JET_err.MustCommitDistributedTransactionToLevel0)
		{
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00012B14 File Offset: 0x00010D14
		private EsentMustCommitDistributedTransactionToLevel0Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
