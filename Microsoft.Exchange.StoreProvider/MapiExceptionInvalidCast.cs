using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000094 RID: 148
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionInvalidCast : ExInvalidCastException
	{
		// Token: 0x06000400 RID: 1024 RVA: 0x000124DE File Offset: 0x000106DE
		internal MapiExceptionInvalidCast(string message) : base("MapiExceptionInvalidCast: " + message)
		{
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000124F1 File Offset: 0x000106F1
		private MapiExceptionInvalidCast(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
