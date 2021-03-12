using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200081A RID: 2074
	[ComVisible(true)]
	public interface IServerChannelSink : IChannelSinkBase
	{
		// Token: 0x06005901 RID: 22785
		[SecurityCritical]
		ServerProcessing ProcessMessage(IServerChannelSinkStack sinkStack, IMessage requestMsg, ITransportHeaders requestHeaders, Stream requestStream, out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream);

		// Token: 0x06005902 RID: 22786
		[SecurityCritical]
		void AsyncProcessResponse(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers, Stream stream);

		// Token: 0x06005903 RID: 22787
		[SecurityCritical]
		Stream GetResponseStream(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers);

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06005904 RID: 22788
		IServerChannelSink NextChannelSink { [SecurityCritical] get; }
	}
}
