using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000128 RID: 296
	[Serializable]
	public sealed class EsentBackupAbortByServerException : EsentOperationException
	{
		// Token: 0x06000863 RID: 2147 RVA: 0x000120F2 File Offset: 0x000102F2
		public EsentBackupAbortByServerException() : base("Backup was aborted by server by calling JetTerm with JET_bitTermStopBackup or by calling JetStopBackup", JET_err.BackupAbortByServer)
		{
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00012104 File Offset: 0x00010304
		private EsentBackupAbortByServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
