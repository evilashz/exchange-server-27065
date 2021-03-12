using System;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020007ED RID: 2029
	internal class SynchronizedServerContextSink : InternalSink, IMessageSink
	{
		// Token: 0x060057DD RID: 22493 RVA: 0x00134DAC File Offset: 0x00132FAC
		[SecurityCritical]
		internal SynchronizedServerContextSink(SynchronizationAttribute prop, IMessageSink nextSink)
		{
			this._property = prop;
			this._nextSink = nextSink;
		}

		// Token: 0x060057DE RID: 22494 RVA: 0x00134DC4 File Offset: 0x00132FC4
		[SecuritySafeCritical]
		~SynchronizedServerContextSink()
		{
			this._property.Dispose();
		}

		// Token: 0x060057DF RID: 22495 RVA: 0x00134DF8 File Offset: 0x00132FF8
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			WorkItem workItem = new WorkItem(reqMsg, this._nextSink, null);
			this._property.HandleWorkRequest(workItem);
			return workItem.ReplyMessage;
		}

		// Token: 0x060057E0 RID: 22496 RVA: 0x00134E28 File Offset: 0x00133028
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			WorkItem workItem = new WorkItem(reqMsg, this._nextSink, replySink);
			workItem.SetAsync();
			this._property.HandleWorkRequest(workItem);
			return null;
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x060057E1 RID: 22497 RVA: 0x00134E56 File Offset: 0x00133056
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x040027CF RID: 10191
		internal IMessageSink _nextSink;

		// Token: 0x040027D0 RID: 10192
		[SecurityCritical]
		internal SynchronizationAttribute _property;
	}
}
