using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000144 RID: 324
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionPartialItems : MapiPermanentException
	{
		// Token: 0x06000567 RID: 1383 RVA: 0x00013AE2 File Offset: 0x00011CE2
		internal MapiExceptionPartialItems(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionPartialItems", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00013AF6 File Offset: 0x00011CF6
		private MapiExceptionPartialItems(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
