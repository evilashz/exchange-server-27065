using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000155 RID: 341
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionConversationNotFound : MapiPermanentException
	{
		// Token: 0x06000589 RID: 1417 RVA: 0x00013CE0 File Offset: 0x00011EE0
		internal MapiExceptionConversationNotFound(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionConversationNotFound", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00013CF4 File Offset: 0x00011EF4
		private MapiExceptionConversationNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
