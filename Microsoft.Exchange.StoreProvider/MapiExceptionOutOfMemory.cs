using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200018C RID: 396
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionOutOfMemory : MapiPermanentException
	{
		// Token: 0x060005F7 RID: 1527 RVA: 0x0001432A File Offset: 0x0001252A
		internal MapiExceptionOutOfMemory(string message) : base("MapiExceptionOutOfMemory", message)
		{
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00014338 File Offset: 0x00012538
		private MapiExceptionOutOfMemory(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
