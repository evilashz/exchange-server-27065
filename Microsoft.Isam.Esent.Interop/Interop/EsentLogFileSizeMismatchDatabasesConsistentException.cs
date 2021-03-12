using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000FA RID: 250
	[Serializable]
	public sealed class EsentLogFileSizeMismatchDatabasesConsistentException : EsentStateException
	{
		// Token: 0x06000807 RID: 2055 RVA: 0x00011BEA File Offset: 0x0000FDEA
		public EsentLogFileSizeMismatchDatabasesConsistentException() : base("databases have been recovered, but the log file size used during recovery does not match JET_paramLogFileSize", JET_err.LogFileSizeMismatchDatabasesConsistent)
		{
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00011BFC File Offset: 0x0000FDFC
		private EsentLogFileSizeMismatchDatabasesConsistentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
