using System;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000CA RID: 202
	internal interface IMwiTarget : IRpcTarget
	{
		// Token: 0x060006B2 RID: 1714
		void SendMessageAsync(MwiMessage message);
	}
}
