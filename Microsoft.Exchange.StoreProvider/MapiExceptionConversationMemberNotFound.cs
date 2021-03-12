using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000156 RID: 342
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionConversationMemberNotFound : MapiPermanentException
	{
		// Token: 0x0600058B RID: 1419 RVA: 0x00013CFE File Offset: 0x00011EFE
		internal MapiExceptionConversationMemberNotFound(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionConversationMemberNotFound", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00013D12 File Offset: 0x00011F12
		private MapiExceptionConversationMemberNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
