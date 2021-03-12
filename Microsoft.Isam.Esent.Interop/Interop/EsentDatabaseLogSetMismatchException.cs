using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000F4 RID: 244
	[Serializable]
	public sealed class EsentDatabaseLogSetMismatchException : EsentInconsistentException
	{
		// Token: 0x060007FB RID: 2043 RVA: 0x00011B42 File Offset: 0x0000FD42
		public EsentDatabaseLogSetMismatchException() : base("Database does not belong with the current set of log files", JET_err.DatabaseLogSetMismatch)
		{
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00011B54 File Offset: 0x0000FD54
		private EsentDatabaseLogSetMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
