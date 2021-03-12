using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001CA RID: 458
	[Serializable]
	public sealed class EsentSecondaryIndexCorruptedException : EsentCorruptionException
	{
		// Token: 0x060009A7 RID: 2471 RVA: 0x000132AA File Offset: 0x000114AA
		public EsentSecondaryIndexCorruptedException() : base("Secondary index is corrupt. The database must be defragmented or the affected index must be deleted. If the corrupt index is over Unicode text, a likely cause a sort-order change.", JET_err.SecondaryIndexCorrupted)
		{
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x000132BC File Offset: 0x000114BC
		private EsentSecondaryIndexCorruptedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
