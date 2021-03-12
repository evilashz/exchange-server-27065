using System;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200080C RID: 2060
	internal class ADAsyncWorkItem
	{
		// Token: 0x060058D4 RID: 22740 RVA: 0x001387B3 File Offset: 0x001369B3
		[SecurityCritical]
		internal ADAsyncWorkItem(IMessage reqMsg, IMessageSink nextSink, IMessageSink replySink)
		{
			this._reqMsg = reqMsg;
			this._nextSink = nextSink;
			this._replySink = replySink;
			this._callCtx = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
		}

		// Token: 0x060058D5 RID: 22741 RVA: 0x001387E8 File Offset: 0x001369E8
		[SecurityCritical]
		internal virtual void FinishAsyncWork(object stateIgnored)
		{
			LogicalCallContext logicalCallContext = CallContext.SetLogicalCallContext(this._callCtx);
			IMessage msg = this._nextSink.SyncProcessMessage(this._reqMsg);
			if (this._replySink != null)
			{
				this._replySink.SyncProcessMessage(msg);
			}
			CallContext.SetLogicalCallContext(logicalCallContext);
		}

		// Token: 0x04002838 RID: 10296
		private IMessageSink _replySink;

		// Token: 0x04002839 RID: 10297
		private IMessageSink _nextSink;

		// Token: 0x0400283A RID: 10298
		[SecurityCritical]
		private LogicalCallContext _callCtx;

		// Token: 0x0400283B RID: 10299
		private IMessage _reqMsg;
	}
}
