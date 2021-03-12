using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001A5 RID: 421
	[Serializable]
	public sealed class EsentDatabaseInvalidIncrementalReseedException : EsentUsageException
	{
		// Token: 0x0600095D RID: 2397 RVA: 0x00012E9E File Offset: 0x0001109E
		public EsentDatabaseInvalidIncrementalReseedException() : base("The database is not a valid state to perform an incremental reseed.", JET_err.DatabaseInvalidIncrementalReseed)
		{
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00012EB0 File Offset: 0x000110B0
		private EsentDatabaseInvalidIncrementalReseedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
