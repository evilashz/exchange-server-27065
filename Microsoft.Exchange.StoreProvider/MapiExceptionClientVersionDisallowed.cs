using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000145 RID: 325
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionClientVersionDisallowed : MapiPermanentException
	{
		// Token: 0x06000569 RID: 1385 RVA: 0x00013B00 File Offset: 0x00011D00
		internal MapiExceptionClientVersionDisallowed(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionClientVersionDisallowed", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00013B14 File Offset: 0x00011D14
		private MapiExceptionClientVersionDisallowed(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
