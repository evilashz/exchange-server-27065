using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000ED RID: 237
	[Serializable]
	public sealed class EsentBadDbSignatureException : EsentObsoleteException
	{
		// Token: 0x060007ED RID: 2029 RVA: 0x00011A7E File Offset: 0x0000FC7E
		public EsentBadDbSignatureException() : base("Bad signature for a db file", JET_err.BadDbSignature)
		{
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00011A90 File Offset: 0x0000FC90
		private EsentBadDbSignatureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
