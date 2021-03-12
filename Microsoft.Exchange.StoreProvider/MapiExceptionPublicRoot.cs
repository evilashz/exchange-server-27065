using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200012F RID: 303
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionPublicRoot : MapiPermanentException
	{
		// Token: 0x0600053D RID: 1341 RVA: 0x00013872 File Offset: 0x00011A72
		internal MapiExceptionPublicRoot(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionPublicRoot", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00013886 File Offset: 0x00011A86
		private MapiExceptionPublicRoot(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
