using System;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020007EE RID: 2030
	internal class WorkItem
	{
		// Token: 0x060057E3 RID: 22499 RVA: 0x00134E74 File Offset: 0x00133074
		[SecurityCritical]
		internal WorkItem(IMessage reqMsg, IMessageSink nextSink, IMessageSink replySink)
		{
			this._reqMsg = reqMsg;
			this._replyMsg = null;
			this._nextSink = nextSink;
			this._replySink = replySink;
			this._ctx = Thread.CurrentContext;
			this._callCtx = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
		}

		// Token: 0x060057E4 RID: 22500 RVA: 0x00134EC3 File Offset: 0x001330C3
		internal virtual void SetWaiting()
		{
			this._flags |= 1;
		}

		// Token: 0x060057E5 RID: 22501 RVA: 0x00134ED3 File Offset: 0x001330D3
		internal virtual bool IsWaiting()
		{
			return (this._flags & 1) == 1;
		}

		// Token: 0x060057E6 RID: 22502 RVA: 0x00134EE0 File Offset: 0x001330E0
		internal virtual void SetSignaled()
		{
			this._flags |= 2;
		}

		// Token: 0x060057E7 RID: 22503 RVA: 0x00134EF0 File Offset: 0x001330F0
		internal virtual bool IsSignaled()
		{
			return (this._flags & 2) == 2;
		}

		// Token: 0x060057E8 RID: 22504 RVA: 0x00134EFD File Offset: 0x001330FD
		internal virtual void SetAsync()
		{
			this._flags |= 4;
		}

		// Token: 0x060057E9 RID: 22505 RVA: 0x00134F0D File Offset: 0x0013310D
		internal virtual bool IsAsync()
		{
			return (this._flags & 4) == 4;
		}

		// Token: 0x060057EA RID: 22506 RVA: 0x00134F1A File Offset: 0x0013311A
		internal virtual void SetDummy()
		{
			this._flags |= 8;
		}

		// Token: 0x060057EB RID: 22507 RVA: 0x00134F2A File Offset: 0x0013312A
		internal virtual bool IsDummy()
		{
			return (this._flags & 8) == 8;
		}

		// Token: 0x060057EC RID: 22508 RVA: 0x00134F38 File Offset: 0x00133138
		[SecurityCritical]
		internal static object ExecuteCallback(object[] args)
		{
			WorkItem workItem = (WorkItem)args[0];
			if (workItem.IsAsync())
			{
				workItem._nextSink.AsyncProcessMessage(workItem._reqMsg, workItem._replySink);
			}
			else if (workItem._nextSink != null)
			{
				workItem._replyMsg = workItem._nextSink.SyncProcessMessage(workItem._reqMsg);
			}
			return null;
		}

		// Token: 0x060057ED RID: 22509 RVA: 0x00134F90 File Offset: 0x00133190
		[SecurityCritical]
		internal virtual void Execute()
		{
			Thread.CurrentThread.InternalCrossContextCallback(this._ctx, WorkItem._xctxDel, new object[]
			{
				this
			});
		}

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x060057EE RID: 22510 RVA: 0x00134FB2 File Offset: 0x001331B2
		internal virtual IMessage ReplyMessage
		{
			get
			{
				return this._replyMsg;
			}
		}

		// Token: 0x040027D1 RID: 10193
		private const int FLG_WAITING = 1;

		// Token: 0x040027D2 RID: 10194
		private const int FLG_SIGNALED = 2;

		// Token: 0x040027D3 RID: 10195
		private const int FLG_ASYNC = 4;

		// Token: 0x040027D4 RID: 10196
		private const int FLG_DUMMY = 8;

		// Token: 0x040027D5 RID: 10197
		internal int _flags;

		// Token: 0x040027D6 RID: 10198
		internal IMessage _reqMsg;

		// Token: 0x040027D7 RID: 10199
		internal IMessageSink _nextSink;

		// Token: 0x040027D8 RID: 10200
		internal IMessageSink _replySink;

		// Token: 0x040027D9 RID: 10201
		internal IMessage _replyMsg;

		// Token: 0x040027DA RID: 10202
		internal Context _ctx;

		// Token: 0x040027DB RID: 10203
		[SecurityCritical]
		internal LogicalCallContext _callCtx;

		// Token: 0x040027DC RID: 10204
		internal static InternalCrossContextDelegate _xctxDel = new InternalCrossContextDelegate(WorkItem.ExecuteCallback);
	}
}
