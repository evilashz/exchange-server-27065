using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000815 RID: 2069
	[ComVisible(true)]
	public interface IServerChannelSinkProvider
	{
		// Token: 0x060058F8 RID: 22776
		[SecurityCritical]
		void GetChannelData(IChannelDataStore channelData);

		// Token: 0x060058F9 RID: 22777
		[SecurityCritical]
		IServerChannelSink CreateSink(IChannelReceiver channel);

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x060058FA RID: 22778
		// (set) Token: 0x060058FB RID: 22779
		IServerChannelSinkProvider Next { [SecurityCritical] get; [SecurityCritical] set; }
	}
}
