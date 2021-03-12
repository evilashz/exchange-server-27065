using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000812 RID: 2066
	[ComVisible(true)]
	public interface IChannelReceiver : IChannel
	{
		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x060058ED RID: 22765
		object ChannelData { [SecurityCritical] get; }

		// Token: 0x060058EE RID: 22766
		[SecurityCritical]
		string[] GetUrlsForUri(string objectURI);

		// Token: 0x060058EF RID: 22767
		[SecurityCritical]
		void StartListening(object data);

		// Token: 0x060058F0 RID: 22768
		[SecurityCritical]
		void StopListening(object data);
	}
}
