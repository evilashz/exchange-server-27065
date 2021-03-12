using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000128 RID: 296
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionInvalidRTF : MapiPermanentException
	{
		// Token: 0x0600052F RID: 1327 RVA: 0x000137A0 File Offset: 0x000119A0
		internal MapiExceptionInvalidRTF(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionInvalidRTF", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x000137B4 File Offset: 0x000119B4
		private MapiExceptionInvalidRTF(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
