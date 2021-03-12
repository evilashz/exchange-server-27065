using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200012D RID: 301
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionWrongMailbox : MapiPermanentException
	{
		// Token: 0x06000539 RID: 1337 RVA: 0x00013836 File Offset: 0x00011A36
		internal MapiExceptionWrongMailbox(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionWrongMailbox", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0001384A File Offset: 0x00011A4A
		private MapiExceptionWrongMailbox(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
