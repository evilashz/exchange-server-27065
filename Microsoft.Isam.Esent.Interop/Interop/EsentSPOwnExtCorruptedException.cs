using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000C9 RID: 201
	[Serializable]
	public sealed class EsentSPOwnExtCorruptedException : EsentCorruptionException
	{
		// Token: 0x060007A5 RID: 1957 RVA: 0x0001168E File Offset: 0x0000F88E
		public EsentSPOwnExtCorruptedException() : base("OwnExt space tree is corrupt", JET_err.SPOwnExtCorrupted)
		{
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x000116A0 File Offset: 0x0000F8A0
		private EsentSPOwnExtCorruptedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
