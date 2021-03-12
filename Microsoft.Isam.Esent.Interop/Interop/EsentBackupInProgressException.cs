using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000D7 RID: 215
	[Serializable]
	public sealed class EsentBackupInProgressException : EsentStateException
	{
		// Token: 0x060007C1 RID: 1985 RVA: 0x00011816 File Offset: 0x0000FA16
		public EsentBackupInProgressException() : base("Backup is active already", JET_err.BackupInProgress)
		{
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00011828 File Offset: 0x0000FA28
		private EsentBackupInProgressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
