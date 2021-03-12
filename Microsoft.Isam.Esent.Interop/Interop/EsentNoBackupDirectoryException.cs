using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000D5 RID: 213
	[Serializable]
	public sealed class EsentNoBackupDirectoryException : EsentUsageException
	{
		// Token: 0x060007BD RID: 1981 RVA: 0x000117DE File Offset: 0x0000F9DE
		public EsentNoBackupDirectoryException() : base("No backup directory given", JET_err.NoBackupDirectory)
		{
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x000117F0 File Offset: 0x0000F9F0
		private EsentNoBackupDirectoryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
