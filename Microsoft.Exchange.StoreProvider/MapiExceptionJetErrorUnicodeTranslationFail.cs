using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000198 RID: 408
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorUnicodeTranslationFail : MapiPermanentException
	{
		// Token: 0x0600060F RID: 1551 RVA: 0x00014486 File Offset: 0x00012686
		internal MapiExceptionJetErrorUnicodeTranslationFail(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorUnicodeTranslationFail", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001449A File Offset: 0x0001269A
		private MapiExceptionJetErrorUnicodeTranslationFail(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
