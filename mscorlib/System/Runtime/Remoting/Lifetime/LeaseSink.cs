using System;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x020007F4 RID: 2036
	internal class LeaseSink : IMessageSink
	{
		// Token: 0x06005828 RID: 22568 RVA: 0x00135C41 File Offset: 0x00133E41
		public LeaseSink(Lease lease, IMessageSink nextSink)
		{
			this.lease = lease;
			this.nextSink = nextSink;
		}

		// Token: 0x06005829 RID: 22569 RVA: 0x00135C57 File Offset: 0x00133E57
		[SecurityCritical]
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this.lease.RenewOnCall();
			return this.nextSink.SyncProcessMessage(msg);
		}

		// Token: 0x0600582A RID: 22570 RVA: 0x00135C70 File Offset: 0x00133E70
		[SecurityCritical]
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			this.lease.RenewOnCall();
			return this.nextSink.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x0600582B RID: 22571 RVA: 0x00135C8A File Offset: 0x00133E8A
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return this.nextSink;
			}
		}

		// Token: 0x040027EC RID: 10220
		private Lease lease;

		// Token: 0x040027ED RID: 10221
		private IMessageSink nextSink;
	}
}
