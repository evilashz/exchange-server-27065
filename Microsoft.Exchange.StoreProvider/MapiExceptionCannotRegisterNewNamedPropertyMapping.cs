using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200013D RID: 317
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionCannotRegisterNewNamedPropertyMapping : MapiPermanentException
	{
		// Token: 0x06000559 RID: 1369 RVA: 0x00013A10 File Offset: 0x00011C10
		internal MapiExceptionCannotRegisterNewNamedPropertyMapping(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionCannotRegisterNewNamedPropertyMapping", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00013A24 File Offset: 0x00011C24
		private MapiExceptionCannotRegisterNewNamedPropertyMapping(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
