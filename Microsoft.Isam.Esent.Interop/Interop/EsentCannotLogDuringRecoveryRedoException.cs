using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000DC RID: 220
	[Serializable]
	public sealed class EsentCannotLogDuringRecoveryRedoException : EsentErrorException
	{
		// Token: 0x060007CB RID: 1995 RVA: 0x000118A2 File Offset: 0x0000FAA2
		public EsentCannotLogDuringRecoveryRedoException() : base("Try to log something during recovery redo", JET_err.CannotLogDuringRecoveryRedo)
		{
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x000118B4 File Offset: 0x0000FAB4
		private EsentCannotLogDuringRecoveryRedoException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
