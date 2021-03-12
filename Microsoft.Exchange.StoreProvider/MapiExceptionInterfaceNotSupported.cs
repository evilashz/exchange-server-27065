using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000E3 RID: 227
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionInterfaceNotSupported : MapiPermanentException
	{
		// Token: 0x060004A5 RID: 1189 RVA: 0x00012F8A File Offset: 0x0001118A
		internal MapiExceptionInterfaceNotSupported(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionInterfaceNotSupported", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00012F9E File Offset: 0x0001119E
		private MapiExceptionInterfaceNotSupported(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
