using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000E1 RID: 225
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionCallFailed : MapiPermanentException
	{
		// Token: 0x060004A1 RID: 1185 RVA: 0x00012F4E File Offset: 0x0001114E
		internal MapiExceptionCallFailed(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionCallFailed", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00012F62 File Offset: 0x00011162
		private MapiExceptionCallFailed(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
