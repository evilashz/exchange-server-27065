using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000F9 RID: 249
	[Serializable]
	public sealed class EsentSoftRecoveryOnBackupDatabaseException : EsentUsageException
	{
		// Token: 0x06000805 RID: 2053 RVA: 0x00011BCE File Offset: 0x0000FDCE
		public EsentSoftRecoveryOnBackupDatabaseException() : base("Soft recovery is intended on a backup database. Restore should be used instead", JET_err.SoftRecoveryOnBackupDatabase)
		{
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00011BE0 File Offset: 0x0000FDE0
		private EsentSoftRecoveryOnBackupDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
