using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001A6 RID: 422
	[Serializable]
	public sealed class EsentDatabaseFailedIncrementalReseedException : EsentStateException
	{
		// Token: 0x0600095F RID: 2399 RVA: 0x00012EBA File Offset: 0x000110BA
		public EsentDatabaseFailedIncrementalReseedException() : base("The incremental reseed being performed on the specified database cannot be completed due to a fatal error.  A full reseed is required to recover this database.", JET_err.DatabaseFailedIncrementalReseed)
		{
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00012ECC File Offset: 0x000110CC
		private EsentDatabaseFailedIncrementalReseedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
