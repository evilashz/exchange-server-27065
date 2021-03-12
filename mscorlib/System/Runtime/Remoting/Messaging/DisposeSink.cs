using System;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000859 RID: 2137
	internal class DisposeSink : IMessageSink
	{
		// Token: 0x06005B7B RID: 23419 RVA: 0x001402CB File Offset: 0x0013E4CB
		internal DisposeSink(IDisposable iDis, IMessageSink replySink)
		{
			this._iDis = iDis;
			this._replySink = replySink;
		}

		// Token: 0x06005B7C RID: 23420 RVA: 0x001402E4 File Offset: 0x0013E4E4
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			IMessage result = null;
			try
			{
				if (this._replySink != null)
				{
					result = this._replySink.SyncProcessMessage(reqMsg);
				}
			}
			finally
			{
				this._iDis.Dispose();
			}
			return result;
		}

		// Token: 0x06005B7D RID: 23421 RVA: 0x00140328 File Offset: 0x0013E528
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x06005B7E RID: 23422 RVA: 0x0014032F File Offset: 0x0013E52F
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return this._replySink;
			}
		}

		// Token: 0x04002914 RID: 10516
		private IDisposable _iDis;

		// Token: 0x04002915 RID: 10517
		private IMessageSink _replySink;
	}
}
