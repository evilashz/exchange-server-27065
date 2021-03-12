using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000CA RID: 202
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionCorruptMidsetDeleted : MapiPermanentException
	{
		// Token: 0x06000472 RID: 1138 RVA: 0x00012C8B File Offset: 0x00010E8B
		internal MapiExceptionCorruptMidsetDeleted(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionCorruptMidsetDeleted", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00012C9F File Offset: 0x00010E9F
		private MapiExceptionCorruptMidsetDeleted(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
