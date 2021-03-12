using System;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.ProcessManager
{
	// Token: 0x020007F6 RID: 2038
	internal interface IComWorker<IComInterface>
	{
		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06002AC4 RID: 10948
		IComInterface Worker { get; }

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06002AC5 RID: 10949
		SafeProcessHandle SafeProcessHandle { get; }

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06002AC6 RID: 10950
		int ProcessId { get; }
	}
}
