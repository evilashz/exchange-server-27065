using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000194 RID: 404
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorInvalidLanguageId : MapiPermanentException
	{
		// Token: 0x06000607 RID: 1543 RVA: 0x0001440E File Offset: 0x0001260E
		internal MapiExceptionJetErrorInvalidLanguageId(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorInvalidLanguageId", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00014422 File Offset: 0x00012622
		private MapiExceptionJetErrorInvalidLanguageId(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
