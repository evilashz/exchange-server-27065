using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000814 RID: 2068
	[ComVisible(true)]
	public interface IClientChannelSinkProvider
	{
		// Token: 0x060058F5 RID: 22773
		[SecurityCritical]
		IClientChannelSink CreateSink(IChannelSender channel, string url, object remoteChannelData);

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x060058F6 RID: 22774
		// (set) Token: 0x060058F7 RID: 22775
		IClientChannelSinkProvider Next { [SecurityCritical] get; [SecurityCritical] set; }
	}
}
