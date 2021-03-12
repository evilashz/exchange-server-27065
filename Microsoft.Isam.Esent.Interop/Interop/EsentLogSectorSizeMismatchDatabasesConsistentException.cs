using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000FC RID: 252
	[Serializable]
	public sealed class EsentLogSectorSizeMismatchDatabasesConsistentException : EsentStateException
	{
		// Token: 0x0600080B RID: 2059 RVA: 0x00011C22 File Offset: 0x0000FE22
		public EsentLogSectorSizeMismatchDatabasesConsistentException() : base("databases have been recovered, but the log file sector size (used during recovery) does not match the current volume's sector size", JET_err.LogSectorSizeMismatchDatabasesConsistent)
		{
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00011C34 File Offset: 0x0000FE34
		private EsentLogSectorSizeMismatchDatabasesConsistentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
