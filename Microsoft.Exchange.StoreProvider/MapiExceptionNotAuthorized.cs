using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000126 RID: 294
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNotAuthorized : MapiPermanentException
	{
		// Token: 0x0600052B RID: 1323 RVA: 0x00013764 File Offset: 0x00011964
		internal MapiExceptionNotAuthorized(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNotAuthorized", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00013778 File Offset: 0x00011978
		private MapiExceptionNotAuthorized(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
