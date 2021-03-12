using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200018F RID: 399
	[Serializable]
	public sealed class EsentDatabaseInvalidNameException : EsentUsageException
	{
		// Token: 0x06000931 RID: 2353 RVA: 0x00012C36 File Offset: 0x00010E36
		public EsentDatabaseInvalidNameException() : base("Invalid database name", JET_err.DatabaseInvalidName)
		{
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00012C48 File Offset: 0x00010E48
		private EsentDatabaseInvalidNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
