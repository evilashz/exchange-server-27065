using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200019B RID: 411
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionDuplicateObject : MapiPermanentException
	{
		// Token: 0x06000615 RID: 1557 RVA: 0x000144E0 File Offset: 0x000126E0
		internal MapiExceptionDuplicateObject(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionDuplicateObject", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x000144F4 File Offset: 0x000126F4
		private MapiExceptionDuplicateObject(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
