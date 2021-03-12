using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000802 RID: 2050
	[SecurityCritical]
	[ComVisible(true)]
	public class ClientChannelSinkStack : IClientChannelSinkStack, IClientResponseChannelSinkStack
	{
		// Token: 0x06005887 RID: 22663 RVA: 0x00137585 File Offset: 0x00135785
		public ClientChannelSinkStack()
		{
		}

		// Token: 0x06005888 RID: 22664 RVA: 0x0013758D File Offset: 0x0013578D
		public ClientChannelSinkStack(IMessageSink replySink)
		{
			this._replySink = replySink;
		}

		// Token: 0x06005889 RID: 22665 RVA: 0x0013759C File Offset: 0x0013579C
		[SecurityCritical]
		public void Push(IClientChannelSink sink, object state)
		{
			this._stack = new ClientChannelSinkStack.SinkStack
			{
				PrevStack = this._stack,
				Sink = sink,
				State = state
			};
		}

		// Token: 0x0600588A RID: 22666 RVA: 0x001375D0 File Offset: 0x001357D0
		[SecurityCritical]
		public object Pop(IClientChannelSink sink)
		{
			if (this._stack == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_PopOnEmptySinkStack"));
			}
			while (this._stack.Sink != sink)
			{
				this._stack = this._stack.PrevStack;
				if (this._stack == null)
				{
					break;
				}
			}
			if (this._stack.Sink == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_PopFromSinkStackWithoutPush"));
			}
			object state = this._stack.State;
			this._stack = this._stack.PrevStack;
			return state;
		}

		// Token: 0x0600588B RID: 22667 RVA: 0x00137658 File Offset: 0x00135858
		[SecurityCritical]
		public void AsyncProcessResponse(ITransportHeaders headers, Stream stream)
		{
			if (this._replySink != null)
			{
				if (this._stack == null)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Channel_CantCallAPRWhenStackEmpty"));
				}
				IClientChannelSink sink = this._stack.Sink;
				object state = this._stack.State;
				this._stack = this._stack.PrevStack;
				sink.AsyncProcessResponse(this, state, headers, stream);
			}
		}

		// Token: 0x0600588C RID: 22668 RVA: 0x001376B8 File Offset: 0x001358B8
		[SecurityCritical]
		public void DispatchReplyMessage(IMessage msg)
		{
			if (this._replySink != null)
			{
				this._replySink.SyncProcessMessage(msg);
			}
		}

		// Token: 0x0600588D RID: 22669 RVA: 0x001376CF File Offset: 0x001358CF
		[SecurityCritical]
		public void DispatchException(Exception e)
		{
			this.DispatchReplyMessage(new ReturnMessage(e, null));
		}

		// Token: 0x04002818 RID: 10264
		private ClientChannelSinkStack.SinkStack _stack;

		// Token: 0x04002819 RID: 10265
		private IMessageSink _replySink;

		// Token: 0x02000C42 RID: 3138
		private class SinkStack
		{
			// Token: 0x0400371C RID: 14108
			public ClientChannelSinkStack.SinkStack PrevStack;

			// Token: 0x0400371D RID: 14109
			public IClientChannelSink Sink;

			// Token: 0x0400371E RID: 14110
			public object State;
		}
	}
}
