using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000190 RID: 400
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionEventError : MapiPermanentException
	{
		// Token: 0x060005FF RID: 1535 RVA: 0x00014396 File Offset: 0x00012596
		internal MapiExceptionEventError(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionEventError", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x000143AA File Offset: 0x000125AA
		private MapiExceptionEventError(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
