using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000095 RID: 149
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionObjectDisposed : ExObjectDisposedException
	{
		// Token: 0x06000402 RID: 1026 RVA: 0x000124FB File Offset: 0x000106FB
		internal MapiExceptionObjectDisposed(string message) : base("MapiExceptionObjectDisposed: " + message)
		{
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001250E File Offset: 0x0001070E
		private MapiExceptionObjectDisposed(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
