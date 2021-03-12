using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000134 RID: 308
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionIllegalCrossServerConnection : MapiPermanentException
	{
		// Token: 0x06000547 RID: 1351 RVA: 0x00013908 File Offset: 0x00011B08
		internal MapiExceptionIllegalCrossServerConnection(string message) : base("MapiExceptionIllegalCrossServerConnection", message)
		{
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00013916 File Offset: 0x00011B16
		private MapiExceptionIllegalCrossServerConnection(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
