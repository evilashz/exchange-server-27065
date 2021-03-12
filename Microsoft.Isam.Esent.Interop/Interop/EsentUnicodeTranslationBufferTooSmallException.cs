using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200011C RID: 284
	[Serializable]
	public sealed class EsentUnicodeTranslationBufferTooSmallException : EsentObsoleteException
	{
		// Token: 0x0600084B RID: 2123 RVA: 0x00011FA2 File Offset: 0x000101A2
		public EsentUnicodeTranslationBufferTooSmallException() : base("Unicode translation buffer too small", JET_err.UnicodeTranslationBufferTooSmall)
		{
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00011FB4 File Offset: 0x000101B4
		private EsentUnicodeTranslationBufferTooSmallException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
