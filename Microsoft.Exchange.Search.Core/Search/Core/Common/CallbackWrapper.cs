using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000056 RID: 86
	internal class CallbackWrapper
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x000033F6 File Offset: 0x000015F6
		protected CallbackWrapper()
		{
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000033FE File Offset: 0x000015FE
		internal static Hookable<CallbackWrapper> HookableCallbackWrapper
		{
			get
			{
				return CallbackWrapper.hookableCallbackWrapper;
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00003405 File Offset: 0x00001605
		public static WaitCallback WaitCallback(WaitCallback callback)
		{
			return CallbackWrapper.HookableCallbackWrapper.Value.WrapWaitCallback(callback);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00003417 File Offset: 0x00001617
		public static WaitOrTimerCallback WaitOrTimerCallback(WaitOrTimerCallback callback)
		{
			return CallbackWrapper.HookableCallbackWrapper.Value.WrapWaitOrTimerCallback(callback);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00003429 File Offset: 0x00001629
		protected virtual WaitCallback WrapWaitCallback(WaitCallback callback)
		{
			return callback;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000342C File Offset: 0x0000162C
		protected virtual WaitOrTimerCallback WrapWaitOrTimerCallback(WaitOrTimerCallback callback)
		{
			return callback;
		}

		// Token: 0x040000A0 RID: 160
		private static readonly Hookable<CallbackWrapper> hookableCallbackWrapper = Hookable<CallbackWrapper>.Create(true, new CallbackWrapper());
	}
}
