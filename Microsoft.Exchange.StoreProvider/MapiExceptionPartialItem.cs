using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000118 RID: 280
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionPartialItem : MapiPermanentException
	{
		// Token: 0x0600050F RID: 1295 RVA: 0x000135C0 File Offset: 0x000117C0
		internal MapiExceptionPartialItem(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionPartialItem", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000135D4 File Offset: 0x000117D4
		private MapiExceptionPartialItem(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
