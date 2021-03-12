using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200080F RID: 2063
	internal class DispatchChannelSink : IServerChannelSink, IChannelSinkBase
	{
		// Token: 0x060058E3 RID: 22755 RVA: 0x001389E1 File Offset: 0x00136BE1
		internal DispatchChannelSink()
		{
		}

		// Token: 0x060058E4 RID: 22756 RVA: 0x001389E9 File Offset: 0x00136BE9
		[SecurityCritical]
		public ServerProcessing ProcessMessage(IServerChannelSinkStack sinkStack, IMessage requestMsg, ITransportHeaders requestHeaders, Stream requestStream, out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			if (requestMsg == null)
			{
				throw new ArgumentNullException("requestMsg", Environment.GetResourceString("Remoting_Channel_DispatchSinkMessageMissing"));
			}
			if (requestStream != null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_DispatchSinkWantsNullRequestStream"));
			}
			responseHeaders = null;
			responseStream = null;
			return ChannelServices.DispatchMessage(sinkStack, requestMsg, out responseMsg);
		}

		// Token: 0x060058E5 RID: 22757 RVA: 0x00138A28 File Offset: 0x00136C28
		[SecurityCritical]
		public void AsyncProcessResponse(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers, Stream stream)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060058E6 RID: 22758 RVA: 0x00138A2F File Offset: 0x00136C2F
		[SecurityCritical]
		public Stream GetResponseStream(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x060058E7 RID: 22759 RVA: 0x00138A36 File Offset: 0x00136C36
		public IServerChannelSink NextChannelSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x060058E8 RID: 22760 RVA: 0x00138A39 File Offset: 0x00136C39
		public IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}
	}
}
