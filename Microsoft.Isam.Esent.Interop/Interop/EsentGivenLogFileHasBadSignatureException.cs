using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000104 RID: 260
	[Serializable]
	public sealed class EsentGivenLogFileHasBadSignatureException : EsentInconsistentException
	{
		// Token: 0x0600081B RID: 2075 RVA: 0x00011D02 File Offset: 0x0000FF02
		public EsentGivenLogFileHasBadSignatureException() : base("Restore log file has bad signature", JET_err.GivenLogFileHasBadSignature)
		{
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00011D14 File Offset: 0x0000FF14
		private EsentGivenLogFileHasBadSignatureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
