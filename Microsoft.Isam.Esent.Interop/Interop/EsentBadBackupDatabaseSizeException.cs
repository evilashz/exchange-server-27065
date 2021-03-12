using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000108 RID: 264
	[Serializable]
	public sealed class EsentBadBackupDatabaseSizeException : EsentObsoleteException
	{
		// Token: 0x06000823 RID: 2083 RVA: 0x00011D72 File Offset: 0x0000FF72
		public EsentBadBackupDatabaseSizeException() : base("The backup database size is not in 4k", JET_err.BadBackupDatabaseSize)
		{
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00011D84 File Offset: 0x0000FF84
		private EsentBadBackupDatabaseSizeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
