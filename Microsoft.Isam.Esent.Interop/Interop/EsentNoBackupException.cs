using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000E3 RID: 227
	[Serializable]
	public sealed class EsentNoBackupException : EsentStateException
	{
		// Token: 0x060007D9 RID: 2009 RVA: 0x00011966 File Offset: 0x0000FB66
		public EsentNoBackupException() : base("No backup in progress", JET_err.NoBackup)
		{
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00011978 File Offset: 0x0000FB78
		private EsentNoBackupException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
