using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200010D RID: 269
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNoRecipients : MapiPermanentException
	{
		// Token: 0x060004F9 RID: 1273 RVA: 0x00013476 File Offset: 0x00011676
		internal MapiExceptionNoRecipients(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNoRecipients", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001348A File Offset: 0x0001168A
		private MapiExceptionNoRecipients(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
