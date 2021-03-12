using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000120 RID: 288
	[Serializable]
	public sealed class EsentExistingLogFileHasBadSignatureException : EsentInconsistentException
	{
		// Token: 0x06000853 RID: 2131 RVA: 0x00012012 File Offset: 0x00010212
		public EsentExistingLogFileHasBadSignatureException() : base("Existing log file has bad signature", JET_err.ExistingLogFileHasBadSignature)
		{
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00012024 File Offset: 0x00010224
		private EsentExistingLogFileHasBadSignatureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
