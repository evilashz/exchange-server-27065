using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000C7 RID: 199
	[Serializable]
	public sealed class EsentSPAvailExtCorruptedException : EsentCorruptionException
	{
		// Token: 0x060007A1 RID: 1953 RVA: 0x00011656 File Offset: 0x0000F856
		public EsentSPAvailExtCorruptedException() : base("AvailExt space tree is corrupt", JET_err.SPAvailExtCorrupted)
		{
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00011668 File Offset: 0x0000F868
		private EsentSPAvailExtCorruptedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
