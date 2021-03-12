using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200018D RID: 397
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionIncompatiblePropType : MapiPermanentException
	{
		// Token: 0x060005F9 RID: 1529 RVA: 0x00014342 File Offset: 0x00012542
		internal MapiExceptionIncompatiblePropType(string message) : base("MapiExceptionIncompatiblePropType", message)
		{
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00014350 File Offset: 0x00012550
		private MapiExceptionIncompatiblePropType(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
