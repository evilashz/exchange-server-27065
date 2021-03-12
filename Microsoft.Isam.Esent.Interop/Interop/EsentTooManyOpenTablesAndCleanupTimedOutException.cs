using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001B1 RID: 433
	[Serializable]
	public sealed class EsentTooManyOpenTablesAndCleanupTimedOutException : EsentUsageException
	{
		// Token: 0x06000975 RID: 2421 RVA: 0x00012FEE File Offset: 0x000111EE
		public EsentTooManyOpenTablesAndCleanupTimedOutException() : base("Cannot open any more tables (cleanup attempt failed to complete)", JET_err.TooManyOpenTablesAndCleanupTimedOut)
		{
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00013000 File Offset: 0x00011200
		private EsentTooManyOpenTablesAndCleanupTimedOutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
