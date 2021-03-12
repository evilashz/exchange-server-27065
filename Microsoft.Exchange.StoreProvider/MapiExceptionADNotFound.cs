using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000CD RID: 205
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionADNotFound : MapiRetryableException
	{
		// Token: 0x06000478 RID: 1144 RVA: 0x00012CE5 File Offset: 0x00010EE5
		internal MapiExceptionADNotFound(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionADNotFound", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00012CF9 File Offset: 0x00010EF9
		private MapiExceptionADNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
