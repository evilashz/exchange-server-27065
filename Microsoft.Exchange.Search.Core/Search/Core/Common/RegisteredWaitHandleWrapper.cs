using System;
using System.Threading;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x0200007C RID: 124
	internal class RegisteredWaitHandleWrapper
	{
		// Token: 0x0600031E RID: 798 RVA: 0x0000A82E File Offset: 0x00008A2E
		public static void RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callback, object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			RegisteredWaitHandleWrapper.RegisterWaitForSingleObject(waitObject, callback, state, TimeSpan.FromMilliseconds(millisecondsTimeOutInterval), executeOnlyOnce);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000A844 File Offset: 0x00008A44
		public static void RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callback, object state, TimeSpan timeout, bool executeOnlyOnce)
		{
			RegisteredWaitHandleWrapper registeredWaitHandleWrapper = new RegisteredWaitHandleWrapper();
			registeredWaitHandleWrapper.userCallback = callback;
			registeredWaitHandleWrapper.userExecuteOnlyOnce = executeOnlyOnce;
			lock (registeredWaitHandleWrapper.lockObject)
			{
				registeredWaitHandleWrapper.handle = ThreadPool.RegisterWaitForSingleObject(waitObject, CallbackWrapper.WaitOrTimerCallback(new WaitOrTimerCallback(registeredWaitHandleWrapper.UnregisterHandle)), state, timeout, executeOnlyOnce);
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000A8B4 File Offset: 0x00008AB4
		private void UnregisterHandle(object state, bool timedOut)
		{
			if (this.userExecuteOnlyOnce || !timedOut)
			{
				lock (this.lockObject)
				{
					this.handle.Unregister(null);
				}
			}
			this.userCallback(state, timedOut);
		}

		// Token: 0x04000164 RID: 356
		private WaitOrTimerCallback userCallback;

		// Token: 0x04000165 RID: 357
		private RegisteredWaitHandle handle;

		// Token: 0x04000166 RID: 358
		private bool userExecuteOnlyOnce;

		// Token: 0x04000167 RID: 359
		private object lockObject = new object();
	}
}
