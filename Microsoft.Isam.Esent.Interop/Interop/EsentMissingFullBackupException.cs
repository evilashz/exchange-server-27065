using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000107 RID: 263
	[Serializable]
	public sealed class EsentMissingFullBackupException : EsentStateException
	{
		// Token: 0x06000821 RID: 2081 RVA: 0x00011D56 File Offset: 0x0000FF56
		public EsentMissingFullBackupException() : base("The database missed a previous full backup before incremental backup", JET_err.MissingFullBackup)
		{
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00011D68 File Offset: 0x0000FF68
		private EsentMissingFullBackupException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
