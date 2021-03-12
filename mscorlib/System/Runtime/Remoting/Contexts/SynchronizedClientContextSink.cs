using System;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020007EF RID: 2031
	internal class SynchronizedClientContextSink : InternalSink, IMessageSink
	{
		// Token: 0x060057EF RID: 22511 RVA: 0x00134FBA File Offset: 0x001331BA
		[SecurityCritical]
		internal SynchronizedClientContextSink(SynchronizationAttribute prop, IMessageSink nextSink)
		{
			this._property = prop;
			this._nextSink = nextSink;
		}

		// Token: 0x060057F0 RID: 22512 RVA: 0x00134FD0 File Offset: 0x001331D0
		[SecuritySafeCritical]
		~SynchronizedClientContextSink()
		{
			this._property.Dispose();
		}

		// Token: 0x060057F1 RID: 22513 RVA: 0x00135004 File Offset: 0x00133204
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			IMessage message;
			if (this._property.IsReEntrant)
			{
				this._property.HandleThreadExit();
				message = this._nextSink.SyncProcessMessage(reqMsg);
				this._property.HandleThreadReEntry();
			}
			else
			{
				LogicalCallContext logicalCallContext = (LogicalCallContext)reqMsg.Properties[Message.CallContextKey];
				string text = logicalCallContext.RemotingData.LogicalCallID;
				bool flag = false;
				if (text == null)
				{
					text = Identity.GetNewLogicalCallID();
					logicalCallContext.RemotingData.LogicalCallID = text;
					flag = true;
				}
				bool flag2 = false;
				if (this._property.SyncCallOutLCID == null)
				{
					this._property.SyncCallOutLCID = text;
					flag2 = true;
				}
				message = this._nextSink.SyncProcessMessage(reqMsg);
				if (flag2)
				{
					this._property.SyncCallOutLCID = null;
					if (flag)
					{
						LogicalCallContext logicalCallContext2 = (LogicalCallContext)message.Properties[Message.CallContextKey];
						logicalCallContext2.RemotingData.LogicalCallID = null;
					}
				}
			}
			return message;
		}

		// Token: 0x060057F2 RID: 22514 RVA: 0x001350E8 File Offset: 0x001332E8
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			if (!this._property.IsReEntrant)
			{
				LogicalCallContext logicalCallContext = (LogicalCallContext)reqMsg.Properties[Message.CallContextKey];
				string newLogicalCallID = Identity.GetNewLogicalCallID();
				logicalCallContext.RemotingData.LogicalCallID = newLogicalCallID;
				this._property.AsyncCallOutLCIDList.Add(newLogicalCallID);
			}
			SynchronizedClientContextSink.AsyncReplySink replySink2 = new SynchronizedClientContextSink.AsyncReplySink(replySink, this._property);
			return this._nextSink.AsyncProcessMessage(reqMsg, replySink2);
		}

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x060057F3 RID: 22515 RVA: 0x0013515A File Offset: 0x0013335A
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x040027DD RID: 10205
		internal IMessageSink _nextSink;

		// Token: 0x040027DE RID: 10206
		[SecurityCritical]
		internal SynchronizationAttribute _property;

		// Token: 0x02000C3D RID: 3133
		internal class AsyncReplySink : IMessageSink
		{
			// Token: 0x06006F82 RID: 28546 RVA: 0x0017FC61 File Offset: 0x0017DE61
			[SecurityCritical]
			internal AsyncReplySink(IMessageSink nextSink, SynchronizationAttribute prop)
			{
				this._nextSink = nextSink;
				this._property = prop;
			}

			// Token: 0x06006F83 RID: 28547 RVA: 0x0017FC78 File Offset: 0x0017DE78
			[SecurityCritical]
			public virtual IMessage SyncProcessMessage(IMessage reqMsg)
			{
				WorkItem workItem = new WorkItem(reqMsg, this._nextSink, null);
				this._property.HandleWorkRequest(workItem);
				if (!this._property.IsReEntrant)
				{
					this._property.AsyncCallOutLCIDList.Remove(((LogicalCallContext)reqMsg.Properties[Message.CallContextKey]).RemotingData.LogicalCallID);
				}
				return workItem.ReplyMessage;
			}

			// Token: 0x06006F84 RID: 28548 RVA: 0x0017FCE1 File Offset: 0x0017DEE1
			[SecurityCritical]
			public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
			{
				throw new NotSupportedException();
			}

			// Token: 0x17001337 RID: 4919
			// (get) Token: 0x06006F85 RID: 28549 RVA: 0x0017FCE8 File Offset: 0x0017DEE8
			public IMessageSink NextSink
			{
				[SecurityCritical]
				get
				{
					return this._nextSink;
				}
			}

			// Token: 0x04003711 RID: 14097
			internal IMessageSink _nextSink;

			// Token: 0x04003712 RID: 14098
			[SecurityCritical]
			internal SynchronizationAttribute _property;
		}
	}
}
