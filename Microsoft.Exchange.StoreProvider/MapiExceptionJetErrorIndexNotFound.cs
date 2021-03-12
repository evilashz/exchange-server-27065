using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000177 RID: 375
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorIndexNotFound : MapiPermanentException
	{
		// Token: 0x060005CD RID: 1485 RVA: 0x000140DC File Offset: 0x000122DC
		internal MapiExceptionJetErrorIndexNotFound(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorIndexNotFound", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000140F0 File Offset: 0x000122F0
		private MapiExceptionJetErrorIndexNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
