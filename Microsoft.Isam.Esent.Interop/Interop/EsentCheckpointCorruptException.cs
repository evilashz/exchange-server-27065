using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000EF RID: 239
	[Serializable]
	public sealed class EsentCheckpointCorruptException : EsentCorruptionException
	{
		// Token: 0x060007F1 RID: 2033 RVA: 0x00011AB6 File Offset: 0x0000FCB6
		public EsentCheckpointCorruptException() : base("Checkpoint file not found or corrupt", JET_err.CheckpointCorrupt)
		{
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00011AC8 File Offset: 0x0000FCC8
		private EsentCheckpointCorruptException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
