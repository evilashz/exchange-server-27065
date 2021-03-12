using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x020000A0 RID: 160
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNotEnoughDisk : MapiRetryableException
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x0001279F File Offset: 0x0001099F
		internal MapiExceptionNotEnoughDisk(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNotEnoughDisk", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x000127B3 File Offset: 0x000109B3
		private MapiExceptionNotEnoughDisk(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
