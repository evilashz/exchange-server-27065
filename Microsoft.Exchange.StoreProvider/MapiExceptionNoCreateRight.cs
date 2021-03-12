using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200012A RID: 298
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNoCreateRight : MapiPermanentException
	{
		// Token: 0x06000533 RID: 1331 RVA: 0x000137DC File Offset: 0x000119DC
		internal MapiExceptionNoCreateRight(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNoCreateRight", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000137F0 File Offset: 0x000119F0
		private MapiExceptionNoCreateRight(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
