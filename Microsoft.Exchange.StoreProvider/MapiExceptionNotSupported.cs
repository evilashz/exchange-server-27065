using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000189 RID: 393
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNotSupported : MapiPermanentException
	{
		// Token: 0x060005F1 RID: 1521 RVA: 0x000142E2 File Offset: 0x000124E2
		internal MapiExceptionNotSupported(string message) : base("MapiExceptionNotSupported", message)
		{
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x000142F0 File Offset: 0x000124F0
		private MapiExceptionNotSupported(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
