using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200018A RID: 394
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionInvalidOperation : MapiPermanentException
	{
		// Token: 0x060005F3 RID: 1523 RVA: 0x000142FA File Offset: 0x000124FA
		internal MapiExceptionInvalidOperation(string message) : base("MapiExceptionInvalidOperation", message)
		{
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00014308 File Offset: 0x00012508
		private MapiExceptionInvalidOperation(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
