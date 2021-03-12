using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000D6 RID: 214
	[Serializable]
	public sealed class EsentBackupDirectoryNotEmptyException : EsentUsageException
	{
		// Token: 0x060007BF RID: 1983 RVA: 0x000117FA File Offset: 0x0000F9FA
		public EsentBackupDirectoryNotEmptyException() : base("The backup directory is not emtpy", JET_err.BackupDirectoryNotEmpty)
		{
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001180C File Offset: 0x0000FA0C
		private EsentBackupDirectoryNotEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
