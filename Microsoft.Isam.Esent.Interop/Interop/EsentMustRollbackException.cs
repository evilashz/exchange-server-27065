using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000154 RID: 340
	[Serializable]
	public sealed class EsentMustRollbackException : EsentUsageException
	{
		// Token: 0x060008BB RID: 2235 RVA: 0x000125C2 File Offset: 0x000107C2
		public EsentMustRollbackException() : base("Transaction must rollback because failure of unversioned update", JET_err.MustRollback)
		{
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x000125D4 File Offset: 0x000107D4
		private EsentMustRollbackException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
