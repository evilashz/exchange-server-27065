using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000F8 RID: 248
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionInvalidWorkstationAccount : MapiPermanentException
	{
		// Token: 0x060004CF RID: 1231 RVA: 0x00013200 File Offset: 0x00011400
		internal MapiExceptionInvalidWorkstationAccount(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionInvalidWorkstationAccount", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00013214 File Offset: 0x00011414
		private MapiExceptionInvalidWorkstationAccount(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
