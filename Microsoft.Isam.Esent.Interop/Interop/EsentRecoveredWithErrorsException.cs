using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000E9 RID: 233
	[Serializable]
	public sealed class EsentRecoveredWithErrorsException : EsentStateException
	{
		// Token: 0x060007E5 RID: 2021 RVA: 0x00011A0E File Offset: 0x0000FC0E
		public EsentRecoveredWithErrorsException() : base("Restored with errors", JET_err.RecoveredWithErrors)
		{
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00011A20 File Offset: 0x0000FC20
		private EsentRecoveredWithErrorsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
