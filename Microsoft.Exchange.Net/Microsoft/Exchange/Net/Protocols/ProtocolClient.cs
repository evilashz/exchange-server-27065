using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Protocols
{
	// Token: 0x02000828 RID: 2088
	internal abstract class ProtocolClient : DisposeTrackableBase
	{
		// Token: 0x06002C4C RID: 11340 RVA: 0x000649C4 File Offset: 0x00062BC4
		internal ProtocolClient()
		{
		}

		// Token: 0x06002C4D RID: 11341
		internal abstract bool TryCancel();
	}
}
