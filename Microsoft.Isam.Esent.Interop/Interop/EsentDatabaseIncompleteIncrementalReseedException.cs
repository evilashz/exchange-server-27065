using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001A4 RID: 420
	[Serializable]
	public sealed class EsentDatabaseIncompleteIncrementalReseedException : EsentInconsistentException
	{
		// Token: 0x0600095B RID: 2395 RVA: 0x00012E82 File Offset: 0x00011082
		public EsentDatabaseIncompleteIncrementalReseedException() : base("The database cannot be attached because it is currently being rebuilt as part of an incremental reseed.", JET_err.DatabaseIncompleteIncrementalReseed)
		{
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00012E94 File Offset: 0x00011094
		private EsentDatabaseIncompleteIncrementalReseedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
