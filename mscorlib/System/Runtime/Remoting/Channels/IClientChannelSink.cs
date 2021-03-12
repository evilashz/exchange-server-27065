using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000818 RID: 2072
	[ComVisible(true)]
	public interface IClientChannelSink : IChannelSinkBase
	{
		// Token: 0x060058FC RID: 22780
		[SecurityCritical]
		void ProcessMessage(IMessage msg, ITransportHeaders requestHeaders, Stream requestStream, out ITransportHeaders responseHeaders, out Stream responseStream);

		// Token: 0x060058FD RID: 22781
		[SecurityCritical]
		void AsyncProcessRequest(IClientChannelSinkStack sinkStack, IMessage msg, ITransportHeaders headers, Stream stream);

		// Token: 0x060058FE RID: 22782
		[SecurityCritical]
		void AsyncProcessResponse(IClientResponseChannelSinkStack sinkStack, object state, ITransportHeaders headers, Stream stream);

		// Token: 0x060058FF RID: 22783
		[SecurityCritical]
		Stream GetRequestStream(IMessage msg, ITransportHeaders headers);

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06005900 RID: 22784
		IClientChannelSink NextChannelSink { [SecurityCritical] get; }
	}
}
