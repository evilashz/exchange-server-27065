using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000183 RID: 387
	internal interface IPlayOnPhoneStateProvider : IDisposable
	{
		// Token: 0x06000DFE RID: 3582
		UMCallState GetCallState(string callId);
	}
}
