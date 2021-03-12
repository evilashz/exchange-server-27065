using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200020D RID: 525
	[Serializable]
	public sealed class EsentRollbackErrorException : EsentFatalException
	{
		// Token: 0x06000A2D RID: 2605 RVA: 0x000139FE File Offset: 0x00011BFE
		public EsentRollbackErrorException() : base("error during rollback", JET_err.RollbackError)
		{
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00013A10 File Offset: 0x00011C10
		private EsentRollbackErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
