using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000808 RID: 2056
	internal class AsyncWorkItem : IMessageSink
	{
		// Token: 0x060058AC RID: 22700 RVA: 0x00137F3D File Offset: 0x0013613D
		[SecurityCritical]
		internal AsyncWorkItem(IMessageSink replySink, Context oldCtx) : this(null, replySink, oldCtx, null)
		{
		}

		// Token: 0x060058AD RID: 22701 RVA: 0x00137F49 File Offset: 0x00136149
		[SecurityCritical]
		internal AsyncWorkItem(IMessage reqMsg, IMessageSink replySink, Context oldCtx, ServerIdentity srvID)
		{
			this._reqMsg = reqMsg;
			this._replySink = replySink;
			this._oldCtx = oldCtx;
			this._callCtx = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
			this._srvID = srvID;
		}

		// Token: 0x060058AE RID: 22702 RVA: 0x00137F84 File Offset: 0x00136184
		[SecurityCritical]
		internal static object SyncProcessMessageCallback(object[] args)
		{
			IMessageSink messageSink = (IMessageSink)args[0];
			IMessage msg = (IMessage)args[1];
			return messageSink.SyncProcessMessage(msg);
		}

		// Token: 0x060058AF RID: 22703 RVA: 0x00137FAC File Offset: 0x001361AC
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage msg)
		{
			IMessage result = null;
			if (this._replySink != null)
			{
				Thread.CurrentContext.NotifyDynamicSinks(msg, false, false, true, true);
				object[] args = new object[]
				{
					this._replySink,
					msg
				};
				InternalCrossContextDelegate ftnToCall = new InternalCrossContextDelegate(AsyncWorkItem.SyncProcessMessageCallback);
				result = (IMessage)Thread.CurrentThread.InternalCrossContextCallback(this._oldCtx, ftnToCall, args);
			}
			return result;
		}

		// Token: 0x060058B0 RID: 22704 RVA: 0x0013800D File Offset: 0x0013620D
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x060058B1 RID: 22705 RVA: 0x0013801E File Offset: 0x0013621E
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return this._replySink;
			}
		}

		// Token: 0x060058B2 RID: 22706 RVA: 0x00138028 File Offset: 0x00136228
		[SecurityCritical]
		internal static object FinishAsyncWorkCallback(object[] args)
		{
			AsyncWorkItem asyncWorkItem = (AsyncWorkItem)args[0];
			Context serverContext = asyncWorkItem._srvID.ServerContext;
			LogicalCallContext logicalCallContext = CallContext.SetLogicalCallContext(asyncWorkItem._callCtx);
			serverContext.NotifyDynamicSinks(asyncWorkItem._reqMsg, false, true, true, true);
			IMessageCtrl messageCtrl = serverContext.GetServerContextChain().AsyncProcessMessage(asyncWorkItem._reqMsg, asyncWorkItem);
			CallContext.SetLogicalCallContext(logicalCallContext);
			return null;
		}

		// Token: 0x060058B3 RID: 22707 RVA: 0x00138084 File Offset: 0x00136284
		[SecurityCritical]
		internal virtual void FinishAsyncWork(object stateIgnored)
		{
			InternalCrossContextDelegate ftnToCall = new InternalCrossContextDelegate(AsyncWorkItem.FinishAsyncWorkCallback);
			object[] args = new object[]
			{
				this
			};
			Thread.CurrentThread.InternalCrossContextCallback(this._srvID.ServerContext, ftnToCall, args);
		}

		// Token: 0x04002825 RID: 10277
		private IMessageSink _replySink;

		// Token: 0x04002826 RID: 10278
		private ServerIdentity _srvID;

		// Token: 0x04002827 RID: 10279
		private Context _oldCtx;

		// Token: 0x04002828 RID: 10280
		[SecurityCritical]
		private LogicalCallContext _callCtx;

		// Token: 0x04002829 RID: 10281
		private IMessage _reqMsg;
	}
}
