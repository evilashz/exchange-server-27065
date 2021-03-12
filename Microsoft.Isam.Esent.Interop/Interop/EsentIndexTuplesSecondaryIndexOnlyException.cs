using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001CC RID: 460
	[Serializable]
	public sealed class EsentIndexTuplesSecondaryIndexOnlyException : EsentUsageException
	{
		// Token: 0x060009AB RID: 2475 RVA: 0x000132E2 File Offset: 0x000114E2
		public EsentIndexTuplesSecondaryIndexOnlyException() : base("tuple index can only be on a secondary index", JET_err.IndexTuplesSecondaryIndexOnly)
		{
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x000132F4 File Offset: 0x000114F4
		private EsentIndexTuplesSecondaryIndexOnlyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
