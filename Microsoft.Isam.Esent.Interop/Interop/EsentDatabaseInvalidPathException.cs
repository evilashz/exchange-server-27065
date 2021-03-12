using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200019C RID: 412
	[Serializable]
	public sealed class EsentDatabaseInvalidPathException : EsentUsageException
	{
		// Token: 0x0600094B RID: 2379 RVA: 0x00012DA2 File Offset: 0x00010FA2
		public EsentDatabaseInvalidPathException() : base("Specified path to database file is illegal", JET_err.DatabaseInvalidPath)
		{
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00012DB4 File Offset: 0x00010FB4
		private EsentDatabaseInvalidPathException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
