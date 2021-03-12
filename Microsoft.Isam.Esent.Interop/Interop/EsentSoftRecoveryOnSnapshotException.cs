using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000117 RID: 279
	[Serializable]
	public sealed class EsentSoftRecoveryOnSnapshotException : EsentObsoleteException
	{
		// Token: 0x06000841 RID: 2113 RVA: 0x00011F16 File Offset: 0x00010116
		public EsentSoftRecoveryOnSnapshotException() : base("Soft recovery on a database from a shadow copy backup set", JET_err.SoftRecoveryOnSnapshot)
		{
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00011F28 File Offset: 0x00010128
		private EsentSoftRecoveryOnSnapshotException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
