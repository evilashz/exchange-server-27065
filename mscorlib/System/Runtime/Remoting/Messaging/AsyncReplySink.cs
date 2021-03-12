using System;
using System.Runtime.Remoting.Contexts;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000857 RID: 2135
	internal class AsyncReplySink : IMessageSink
	{
		// Token: 0x06005B6F RID: 23407 RVA: 0x00140092 File Offset: 0x0013E292
		internal AsyncReplySink(IMessageSink replySink, Context cliCtx)
		{
			this._replySink = replySink;
			this._cliCtx = cliCtx;
		}

		// Token: 0x06005B70 RID: 23408 RVA: 0x001400A8 File Offset: 0x0013E2A8
		[SecurityCritical]
		internal static object SyncProcessMessageCallback(object[] args)
		{
			IMessage msg = (IMessage)args[0];
			IMessageSink messageSink = (IMessageSink)args[1];
			Thread.CurrentContext.NotifyDynamicSinks(msg, true, false, true, true);
			return messageSink.SyncProcessMessage(msg);
		}

		// Token: 0x06005B71 RID: 23409 RVA: 0x001400E0 File Offset: 0x0013E2E0
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			IMessage result = null;
			if (this._replySink != null)
			{
				object[] args = new object[]
				{
					reqMsg,
					this._replySink
				};
				InternalCrossContextDelegate ftnToCall = new InternalCrossContextDelegate(AsyncReplySink.SyncProcessMessageCallback);
				result = (IMessage)Thread.CurrentThread.InternalCrossContextCallback(this._cliCtx, ftnToCall, args);
			}
			return result;
		}

		// Token: 0x06005B72 RID: 23410 RVA: 0x00140131 File Offset: 0x0013E331
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x06005B73 RID: 23411 RVA: 0x00140138 File Offset: 0x0013E338
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return this._replySink;
			}
		}

		// Token: 0x04002910 RID: 10512
		private IMessageSink _replySink;

		// Token: 0x04002911 RID: 10513
		private Context _cliCtx;
	}
}
