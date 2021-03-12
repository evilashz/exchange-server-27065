using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000E2 RID: 226
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionInvalidParameter : MapiPermanentException
	{
		// Token: 0x060004A3 RID: 1187 RVA: 0x00012F6C File Offset: 0x0001116C
		internal MapiExceptionInvalidParameter(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionInvalidParameter", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00012F80 File Offset: 0x00011180
		private MapiExceptionInvalidParameter(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
