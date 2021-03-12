using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000115 RID: 277
	[Serializable]
	public sealed class EsentRecoveredWithoutUndoException : EsentStateException
	{
		// Token: 0x0600083D RID: 2109 RVA: 0x00011EDE File Offset: 0x000100DE
		public EsentRecoveredWithoutUndoException() : base("Soft recovery successfully replayed all operations, but the Undo phase of recovery was skipped", JET_err.RecoveredWithoutUndo)
		{
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00011EF0 File Offset: 0x000100F0
		private EsentRecoveredWithoutUndoException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
