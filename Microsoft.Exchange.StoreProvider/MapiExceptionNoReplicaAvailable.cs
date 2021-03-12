using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200011E RID: 286
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNoReplicaAvailable : MapiPermanentException
	{
		// Token: 0x0600051B RID: 1307 RVA: 0x00013674 File Offset: 0x00011874
		internal MapiExceptionNoReplicaAvailable(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionNoReplicaAvailable", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00013688 File Offset: 0x00011888
		private MapiExceptionNoReplicaAvailable(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
