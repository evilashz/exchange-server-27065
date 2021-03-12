using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000E8 RID: 232
	[Serializable]
	public sealed class EsentInvalidBackupException : EsentUsageException
	{
		// Token: 0x060007E3 RID: 2019 RVA: 0x000119F2 File Offset: 0x0000FBF2
		public EsentInvalidBackupException() : base("Cannot perform incremental backup when circular logging enabled", JET_err.InvalidBackup)
		{
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00011A04 File Offset: 0x0000FC04
		private EsentInvalidBackupException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
