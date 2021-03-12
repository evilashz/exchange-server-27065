using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200011D RID: 285
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNoReplicaHere : MapiPermanentException
	{
		// Token: 0x06000519 RID: 1305 RVA: 0x00013656 File Offset: 0x00011856
		internal MapiExceptionNoReplicaHere(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNoReplicaHere", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001366A File Offset: 0x0001186A
		private MapiExceptionNoReplicaHere(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
