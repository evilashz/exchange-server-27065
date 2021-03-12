using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000184 RID: 388
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionNoMoreConnections : MapiPermanentException
	{
		// Token: 0x060005E7 RID: 1511 RVA: 0x00014260 File Offset: 0x00012460
		internal MapiExceptionNoMoreConnections(string message) : base("MapiExceptionNoMoreConnections", message)
		{
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001426E File Offset: 0x0001246E
		private MapiExceptionNoMoreConnections(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
