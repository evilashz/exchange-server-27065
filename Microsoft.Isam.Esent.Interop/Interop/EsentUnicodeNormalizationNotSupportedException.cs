using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200011E RID: 286
	[Serializable]
	public sealed class EsentUnicodeNormalizationNotSupportedException : EsentUsageException
	{
		// Token: 0x0600084F RID: 2127 RVA: 0x00011FDA File Offset: 0x000101DA
		public EsentUnicodeNormalizationNotSupportedException() : base("OS does not provide support for Unicode normalisation (and no normalisation callback was specified)", JET_err.UnicodeNormalizationNotSupported)
		{
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00011FEC File Offset: 0x000101EC
		private EsentUnicodeNormalizationNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
