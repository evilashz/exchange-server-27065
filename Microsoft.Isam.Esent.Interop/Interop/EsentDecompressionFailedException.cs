using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001F7 RID: 503
	[Serializable]
	public sealed class EsentDecompressionFailedException : EsentCorruptionException
	{
		// Token: 0x06000A01 RID: 2561 RVA: 0x00013796 File Offset: 0x00011996
		public EsentDecompressionFailedException() : base("Internal error: data could not be decompressed", JET_err.DecompressionFailed)
		{
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x000137A8 File Offset: 0x000119A8
		private EsentDecompressionFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
