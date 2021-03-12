using System;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020007FF RID: 2047
	internal class ServerAsyncReplyTerminatorSink : IMessageSink
	{
		// Token: 0x0600587E RID: 22654 RVA: 0x0013752C File Offset: 0x0013572C
		internal ServerAsyncReplyTerminatorSink(IMessageSink nextSink)
		{
			this._nextSink = nextSink;
		}

		// Token: 0x0600587F RID: 22655 RVA: 0x0013753C File Offset: 0x0013573C
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage replyMsg)
		{
			Guid guid;
			RemotingServices.CORProfilerRemotingServerSendingReply(out guid, true);
			if (RemotingServices.CORProfilerTrackRemotingCookie())
			{
				replyMsg.Properties["CORProfilerCookie"] = guid;
			}
			return this._nextSink.SyncProcessMessage(replyMsg);
		}

		// Token: 0x06005880 RID: 22656 RVA: 0x0013757A File Offset: 0x0013577A
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage replyMsg, IMessageSink replySink)
		{
			return null;
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06005881 RID: 22657 RVA: 0x0013757D File Offset: 0x0013577D
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x04002817 RID: 10263
		internal IMessageSink _nextSink;
	}
}
