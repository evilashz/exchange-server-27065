using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000F5 RID: 245
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionUnknownLcid : MapiPermanentException
	{
		// Token: 0x060004C9 RID: 1225 RVA: 0x000131A6 File Offset: 0x000113A6
		internal MapiExceptionUnknownLcid(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionUnknownLcid", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x000131BA File Offset: 0x000113BA
		private MapiExceptionUnknownLcid(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
