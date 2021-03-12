using System;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200085B RID: 2139
	internal class ClientAsyncReplyTerminatorSink : IMessageSink
	{
		// Token: 0x06005B83 RID: 23427 RVA: 0x00140413 File Offset: 0x0013E613
		internal ClientAsyncReplyTerminatorSink(IMessageSink nextSink)
		{
			this._nextSink = nextSink;
		}

		// Token: 0x06005B84 RID: 23428 RVA: 0x00140424 File Offset: 0x0013E624
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage replyMsg)
		{
			Guid id = Guid.Empty;
			if (RemotingServices.CORProfilerTrackRemotingCookie())
			{
				object obj = replyMsg.Properties["CORProfilerCookie"];
				if (obj != null)
				{
					id = (Guid)obj;
				}
			}
			RemotingServices.CORProfilerRemotingClientReceivingReply(id, true);
			return this._nextSink.SyncProcessMessage(replyMsg);
		}

		// Token: 0x06005B85 RID: 23429 RVA: 0x0014046C File Offset: 0x0013E66C
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage replyMsg, IMessageSink replySink)
		{
			return null;
		}

		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06005B86 RID: 23430 RVA: 0x0014046F File Offset: 0x0013E66F
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x04002917 RID: 10519
		internal IMessageSink _nextSink;
	}
}
