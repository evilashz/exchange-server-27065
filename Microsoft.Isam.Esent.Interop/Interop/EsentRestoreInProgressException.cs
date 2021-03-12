using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000D8 RID: 216
	[Serializable]
	public sealed class EsentRestoreInProgressException : EsentStateException
	{
		// Token: 0x060007C3 RID: 1987 RVA: 0x00011832 File Offset: 0x0000FA32
		public EsentRestoreInProgressException() : base("Restore in progress", JET_err.RestoreInProgress)
		{
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00011844 File Offset: 0x0000FA44
		private EsentRestoreInProgressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
