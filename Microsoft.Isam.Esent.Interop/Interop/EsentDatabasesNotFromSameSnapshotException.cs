using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000116 RID: 278
	[Serializable]
	public sealed class EsentDatabasesNotFromSameSnapshotException : EsentObsoleteException
	{
		// Token: 0x0600083F RID: 2111 RVA: 0x00011EFA File Offset: 0x000100FA
		public EsentDatabasesNotFromSameSnapshotException() : base("Databases to be restored are not from the same shadow copy backup", JET_err.DatabasesNotFromSameSnapshot)
		{
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00011F0C File Offset: 0x0001010C
		private EsentDatabasesNotFromSameSnapshotException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
