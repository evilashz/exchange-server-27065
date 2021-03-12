using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000811 RID: 2065
	[ComVisible(true)]
	public interface IChannelSender : IChannel
	{
		// Token: 0x060058EC RID: 22764
		[SecurityCritical]
		IMessageSink CreateMessageSink(string url, object remoteChannelData, out string objectURI);
	}
}
