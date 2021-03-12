using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000DA RID: 218
	[Serializable]
	public sealed class EsentLogWriteFailException : EsentIOException
	{
		// Token: 0x060007C7 RID: 1991 RVA: 0x0001186A File Offset: 0x0000FA6A
		public EsentLogWriteFailException() : base("Failure writing to log file", JET_err.LogWriteFail)
		{
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0001187C File Offset: 0x0000FA7C
		private EsentLogWriteFailException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
