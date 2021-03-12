using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200011D RID: 285
	[Serializable]
	public sealed class EsentUnicodeTranslationFailException : EsentOperationException
	{
		// Token: 0x0600084D RID: 2125 RVA: 0x00011FBE File Offset: 0x000101BE
		public EsentUnicodeTranslationFailException() : base("Unicode normalization failed", JET_err.UnicodeTranslationFail)
		{
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00011FD0 File Offset: 0x000101D0
		private EsentUnicodeTranslationFailException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
