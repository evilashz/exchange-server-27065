using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000190 RID: 400
	[Serializable]
	public sealed class EsentDatabaseInvalidPagesException : EsentUsageException
	{
		// Token: 0x06000933 RID: 2355 RVA: 0x00012C52 File Offset: 0x00010E52
		public EsentDatabaseInvalidPagesException() : base("Invalid number of pages", JET_err.DatabaseInvalidPages)
		{
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00012C64 File Offset: 0x00010E64
		private EsentDatabaseInvalidPagesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
