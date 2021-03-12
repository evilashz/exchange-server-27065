using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200011F RID: 287
	[Serializable]
	public sealed class EsentUnicodeLanguageValidationFailureException : EsentOperationException
	{
		// Token: 0x06000851 RID: 2129 RVA: 0x00011FF6 File Offset: 0x000101F6
		public EsentUnicodeLanguageValidationFailureException() : base("Can not validate the language", JET_err.UnicodeLanguageValidationFailure)
		{
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00012008 File Offset: 0x00010208
		private EsentUnicodeLanguageValidationFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
