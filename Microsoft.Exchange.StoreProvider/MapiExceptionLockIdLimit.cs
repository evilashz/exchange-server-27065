using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000117 RID: 279
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionLockIdLimit : MapiPermanentException
	{
		// Token: 0x0600050D RID: 1293 RVA: 0x000135A2 File Offset: 0x000117A2
		internal MapiExceptionLockIdLimit(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionLockIdLimit", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x000135B6 File Offset: 0x000117B6
		private MapiExceptionLockIdLimit(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
