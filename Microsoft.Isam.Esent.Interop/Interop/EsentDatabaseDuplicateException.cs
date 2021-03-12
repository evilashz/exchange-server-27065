using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200018C RID: 396
	[Serializable]
	public sealed class EsentDatabaseDuplicateException : EsentUsageException
	{
		// Token: 0x0600092B RID: 2347 RVA: 0x00012BE2 File Offset: 0x00010DE2
		public EsentDatabaseDuplicateException() : base("Database already exists", JET_err.DatabaseDuplicate)
		{
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00012BF4 File Offset: 0x00010DF4
		private EsentDatabaseDuplicateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
