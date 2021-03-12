using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000100 RID: 256
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionTypeNoSupport : MapiPermanentException
	{
		// Token: 0x060004DF RID: 1247 RVA: 0x000132F0 File Offset: 0x000114F0
		internal MapiExceptionTypeNoSupport(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionTypeNoSupport", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00013304 File Offset: 0x00011504
		private MapiExceptionTypeNoSupport(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
