using System;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Proxies
{
	// Token: 0x020007D6 RID: 2006
	internal class AgileAsyncWorkerItem
	{
		// Token: 0x0600574C RID: 22348 RVA: 0x00133343 File Offset: 0x00131543
		[SecurityCritical]
		public AgileAsyncWorkerItem(IMethodCallMessage message, AsyncResult ar, object target)
		{
			this._message = new MethodCall(message);
			this._ar = ar;
			this._target = target;
		}

		// Token: 0x0600574D RID: 22349 RVA: 0x00133365 File Offset: 0x00131565
		[SecurityCritical]
		public static void ThreadPoolCallBack(object o)
		{
			((AgileAsyncWorkerItem)o).DoAsyncCall();
		}

		// Token: 0x0600574E RID: 22350 RVA: 0x00133372 File Offset: 0x00131572
		[SecurityCritical]
		public void DoAsyncCall()
		{
			new StackBuilderSink(this._target).AsyncProcessMessage(this._message, this._ar);
		}

		// Token: 0x04002793 RID: 10131
		private IMethodCallMessage _message;

		// Token: 0x04002794 RID: 10132
		private AsyncResult _ar;

		// Token: 0x04002795 RID: 10133
		private object _target;
	}
}
