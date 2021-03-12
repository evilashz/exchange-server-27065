using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200019F RID: 415
	[Serializable]
	public sealed class EsentCatalogCorruptedException : EsentCorruptionException
	{
		// Token: 0x06000951 RID: 2385 RVA: 0x00012DF6 File Offset: 0x00010FF6
		public EsentCatalogCorruptedException() : base("Corruption detected in catalog", JET_err.CatalogCorrupted)
		{
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00012E08 File Offset: 0x00011008
		private EsentCatalogCorruptedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
