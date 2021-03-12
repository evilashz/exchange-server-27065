using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000813 RID: 2067
	[ComVisible(true)]
	public interface IChannelReceiverHook
	{
		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x060058F1 RID: 22769
		string ChannelScheme { [SecurityCritical] get; }

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x060058F2 RID: 22770
		bool WantsToListen { [SecurityCritical] get; }

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x060058F3 RID: 22771
		IServerChannelSink ChannelSinkChain { [SecurityCritical] get; }

		// Token: 0x060058F4 RID: 22772
		[SecurityCritical]
		void AddHookChannelUri(string channelUri);
	}
}
