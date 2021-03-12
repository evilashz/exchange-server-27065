using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001E9 RID: 489
	[Serializable]
	public sealed class EsentMultiValuedDuplicateAfterTruncationException : EsentStateException
	{
		// Token: 0x060009E5 RID: 2533 RVA: 0x0001360E File Offset: 0x0001180E
		public EsentMultiValuedDuplicateAfterTruncationException() : base("Duplicate detected on a unique multi-valued column after data was normalized, and normalizing truncated the data before comparison", JET_err.MultiValuedDuplicateAfterTruncation)
		{
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00013620 File Offset: 0x00011820
		private EsentMultiValuedDuplicateAfterTruncationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
