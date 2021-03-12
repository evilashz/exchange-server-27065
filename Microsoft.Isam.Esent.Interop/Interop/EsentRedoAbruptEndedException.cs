using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000F2 RID: 242
	[Serializable]
	public sealed class EsentRedoAbruptEndedException : EsentCorruptionException
	{
		// Token: 0x060007F7 RID: 2039 RVA: 0x00011B0A File Offset: 0x0000FD0A
		public EsentRedoAbruptEndedException() : base("Redo abruptly ended due to sudden failure in reading logs from log file", JET_err.RedoAbruptEnded)
		{
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00011B1C File Offset: 0x0000FD1C
		private EsentRedoAbruptEndedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
