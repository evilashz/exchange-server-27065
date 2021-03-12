using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200019A RID: 410
	[Serializable]
	public sealed class EsentDatabaseSharingViolationException : EsentUsageException
	{
		// Token: 0x06000947 RID: 2375 RVA: 0x00012D6A File Offset: 0x00010F6A
		public EsentDatabaseSharingViolationException() : base("A different database instance is using this database", JET_err.DatabaseSharingViolation)
		{
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00012D7C File Offset: 0x00010F7C
		private EsentDatabaseSharingViolationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
