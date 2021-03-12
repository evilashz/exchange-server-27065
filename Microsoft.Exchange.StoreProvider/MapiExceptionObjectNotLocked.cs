using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000187 RID: 391
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionObjectNotLocked : BaseException
	{
		// Token: 0x060005ED RID: 1517 RVA: 0x000142AD File Offset: 0x000124AD
		internal MapiExceptionObjectNotLocked(string message) : base("MapiExceptionObjectNotLocked: " + message)
		{
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x000142C0 File Offset: 0x000124C0
		private MapiExceptionObjectNotLocked(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
