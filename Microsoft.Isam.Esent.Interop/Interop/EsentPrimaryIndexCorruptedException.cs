using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001C9 RID: 457
	[Serializable]
	public sealed class EsentPrimaryIndexCorruptedException : EsentCorruptionException
	{
		// Token: 0x060009A5 RID: 2469 RVA: 0x0001328E File Offset: 0x0001148E
		public EsentPrimaryIndexCorruptedException() : base("Primary index is corrupt. The database must be defragmented or the table deleted.", JET_err.PrimaryIndexCorrupted)
		{
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x000132A0 File Offset: 0x000114A0
		private EsentPrimaryIndexCorruptedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
