using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000141 RID: 321
	[Serializable]
	public sealed class EsentInvalidDatabaseException : EsentUsageException
	{
		// Token: 0x06000895 RID: 2197 RVA: 0x000123AE File Offset: 0x000105AE
		public EsentInvalidDatabaseException() : base("Not a database file", JET_err.InvalidDatabase)
		{
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x000123C0 File Offset: 0x000105C0
		private EsentInvalidDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
