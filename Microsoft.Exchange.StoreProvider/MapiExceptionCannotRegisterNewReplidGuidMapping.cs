using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200013C RID: 316
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionCannotRegisterNewReplidGuidMapping : MapiPermanentException
	{
		// Token: 0x06000557 RID: 1367 RVA: 0x000139F2 File Offset: 0x00011BF2
		internal MapiExceptionCannotRegisterNewReplidGuidMapping(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionCannotRegisterNewReplidGuidMapping", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00013A06 File Offset: 0x00011C06
		private MapiExceptionCannotRegisterNewReplidGuidMapping(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
