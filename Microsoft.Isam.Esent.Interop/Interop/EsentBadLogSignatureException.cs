using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000EC RID: 236
	[Serializable]
	public sealed class EsentBadLogSignatureException : EsentInconsistentException
	{
		// Token: 0x060007EB RID: 2027 RVA: 0x00011A62 File Offset: 0x0000FC62
		public EsentBadLogSignatureException() : base("Bad signature for a log file", JET_err.BadLogSignature)
		{
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00011A74 File Offset: 0x0000FC74
		private EsentBadLogSignatureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
