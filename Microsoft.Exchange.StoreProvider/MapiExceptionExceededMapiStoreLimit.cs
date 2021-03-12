using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000185 RID: 389
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionExceededMapiStoreLimit : MapiPermanentException
	{
		// Token: 0x060005E9 RID: 1513 RVA: 0x00014278 File Offset: 0x00012478
		internal MapiExceptionExceededMapiStoreLimit(string message) : base("MapiExceptionExceededMapiStoreLimit", message)
		{
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00014286 File Offset: 0x00012486
		private MapiExceptionExceededMapiStoreLimit(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
