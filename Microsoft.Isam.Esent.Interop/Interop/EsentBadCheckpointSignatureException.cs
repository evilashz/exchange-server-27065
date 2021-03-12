using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000EE RID: 238
	[Serializable]
	public sealed class EsentBadCheckpointSignatureException : EsentInconsistentException
	{
		// Token: 0x060007EF RID: 2031 RVA: 0x00011A9A File Offset: 0x0000FC9A
		public EsentBadCheckpointSignatureException() : base("Bad signature for a checkpoint file", JET_err.BadCheckpointSignature)
		{
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00011AAC File Offset: 0x0000FCAC
		private EsentBadCheckpointSignatureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
