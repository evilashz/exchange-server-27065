using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000124 RID: 292
	[Serializable]
	public sealed class EsentRestoreOfNonBackupDatabaseException : EsentUsageException
	{
		// Token: 0x0600085B RID: 2139 RVA: 0x00012082 File Offset: 0x00010282
		public EsentRestoreOfNonBackupDatabaseException() : base("hard recovery attempted on a database that wasn't a backup database", JET_err.RestoreOfNonBackupDatabase)
		{
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00012094 File Offset: 0x00010294
		private EsentRestoreOfNonBackupDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
