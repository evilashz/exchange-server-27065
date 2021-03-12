using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200018D RID: 397
	[Serializable]
	public sealed class EsentDatabaseInUseException : EsentUsageException
	{
		// Token: 0x0600092D RID: 2349 RVA: 0x00012BFE File Offset: 0x00010DFE
		public EsentDatabaseInUseException() : base("Database in use", JET_err.DatabaseInUse)
		{
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00012C10 File Offset: 0x00010E10
		private EsentDatabaseInUseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
