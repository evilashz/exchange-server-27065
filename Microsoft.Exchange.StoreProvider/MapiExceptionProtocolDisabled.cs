using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000146 RID: 326
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionProtocolDisabled : MapiPermanentException
	{
		// Token: 0x0600056B RID: 1387 RVA: 0x00013B1E File Offset: 0x00011D1E
		internal MapiExceptionProtocolDisabled(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionProtocolDisabled", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00013B32 File Offset: 0x00011D32
		private MapiExceptionProtocolDisabled(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
