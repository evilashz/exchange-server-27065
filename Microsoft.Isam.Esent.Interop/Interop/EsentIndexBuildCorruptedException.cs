using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001C8 RID: 456
	[Serializable]
	public sealed class EsentIndexBuildCorruptedException : EsentCorruptionException
	{
		// Token: 0x060009A3 RID: 2467 RVA: 0x00013272 File Offset: 0x00011472
		public EsentIndexBuildCorruptedException() : base("Failed to build a secondary index that properly reflects primary index", JET_err.IndexBuildCorrupted)
		{
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00013284 File Offset: 0x00011484
		private EsentIndexBuildCorruptedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
