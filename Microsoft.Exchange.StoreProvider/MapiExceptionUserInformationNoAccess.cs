using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020001B4 RID: 436
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionUserInformationNoAccess : MapiPermanentException
	{
		// Token: 0x06000648 RID: 1608 RVA: 0x000147D2 File Offset: 0x000129D2
		internal MapiExceptionUserInformationNoAccess(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionUserInformationNoAccess", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x000147E6 File Offset: 0x000129E6
		private MapiExceptionUserInformationNoAccess(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
