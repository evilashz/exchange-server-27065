using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000DB RID: 219
	[Serializable]
	public sealed class EsentLogDisabledDueToRecoveryFailureException : EsentFatalException
	{
		// Token: 0x060007C9 RID: 1993 RVA: 0x00011886 File Offset: 0x0000FA86
		public EsentLogDisabledDueToRecoveryFailureException() : base("Try to log something after recovery faild", JET_err.LogDisabledDueToRecoveryFailure)
		{
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00011898 File Offset: 0x0000FA98
		private EsentLogDisabledDueToRecoveryFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
