using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200019D RID: 413
	[Serializable]
	public sealed class EsentDatabaseIdInUseException : EsentObsoleteException
	{
		// Token: 0x0600094D RID: 2381 RVA: 0x00012DBE File Offset: 0x00010FBE
		public EsentDatabaseIdInUseException() : base("A database is being assigned an id already in use", JET_err.DatabaseIdInUse)
		{
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00012DD0 File Offset: 0x00010FD0
		private EsentDatabaseIdInUseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
