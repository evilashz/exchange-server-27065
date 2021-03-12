using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001E8 RID: 488
	[Serializable]
	public sealed class EsentLVCorruptedException : EsentCorruptionException
	{
		// Token: 0x060009E3 RID: 2531 RVA: 0x000135F2 File Offset: 0x000117F2
		public EsentLVCorruptedException() : base("Corruption encountered in long-value tree", JET_err.LVCorrupted)
		{
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00013604 File Offset: 0x00011804
		private EsentLVCorruptedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
