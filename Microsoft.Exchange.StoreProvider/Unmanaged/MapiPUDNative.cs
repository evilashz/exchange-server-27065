using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200027E RID: 638
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct MapiPUDNative
	{
		// Token: 0x06000B60 RID: 2912 RVA: 0x00033B98 File Offset: 0x00031D98
		internal unsafe MapiPUDNative(NativePUD* ppud)
		{
			this.replGuid = ppud->replGuid;
			this.ltid = new MapiLtidNative(&ppud->ltid);
		}

		// Token: 0x040010FC RID: 4348
		internal MapiLtidNative ltid;

		// Token: 0x040010FD RID: 4349
		internal Guid replGuid;

		// Token: 0x040010FE RID: 4350
		internal static readonly int Size = 16 + MapiLtidNative.Size;
	}
}
