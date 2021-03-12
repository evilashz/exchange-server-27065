using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000188 RID: 392
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionObjectLockCountOverflow : MapiPermanentException
	{
		// Token: 0x060005EF RID: 1519 RVA: 0x000142CA File Offset: 0x000124CA
		internal MapiExceptionObjectLockCountOverflow(string message) : base("MapiExceptionObjectLockCountOverflow", message)
		{
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x000142D8 File Offset: 0x000124D8
		private MapiExceptionObjectLockCountOverflow(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
