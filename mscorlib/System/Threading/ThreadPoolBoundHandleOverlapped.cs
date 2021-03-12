using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004DB RID: 1243
	[SecurityCritical]
	internal sealed class ThreadPoolBoundHandleOverlapped : Overlapped
	{
		// Token: 0x06003B96 RID: 15254 RVA: 0x000E042C File Offset: 0x000DE62C
		public unsafe ThreadPoolBoundHandleOverlapped(IOCompletionCallback callback, object state, object pinData, PreAllocatedOverlapped preAllocated)
		{
			this._userCallback = callback;
			this._userState = state;
			this._preAllocated = preAllocated;
			this._nativeOverlapped = base.Pack(ThreadPoolBoundHandleOverlapped.s_completionCallback, pinData);
			this._nativeOverlapped->OffsetLow = 0;
			this._nativeOverlapped->OffsetHigh = 0;
		}

		// Token: 0x06003B97 RID: 15255 RVA: 0x000E0480 File Offset: 0x000DE680
		private unsafe static void CompletionCallback(uint errorCode, uint numBytes, NativeOverlapped* nativeOverlapped)
		{
			ThreadPoolBoundHandleOverlapped threadPoolBoundHandleOverlapped = (ThreadPoolBoundHandleOverlapped)Overlapped.Unpack(nativeOverlapped);
			if (threadPoolBoundHandleOverlapped._completed)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NativeOverlappedReused"));
			}
			threadPoolBoundHandleOverlapped._completed = true;
			if (threadPoolBoundHandleOverlapped._boundHandle == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Argument_NativeOverlappedAlreadyFree"));
			}
			threadPoolBoundHandleOverlapped._userCallback(errorCode, numBytes, nativeOverlapped);
		}

		// Token: 0x04001903 RID: 6403
		private static readonly IOCompletionCallback s_completionCallback = new IOCompletionCallback(ThreadPoolBoundHandleOverlapped.CompletionCallback);

		// Token: 0x04001904 RID: 6404
		private readonly IOCompletionCallback _userCallback;

		// Token: 0x04001905 RID: 6405
		internal readonly object _userState;

		// Token: 0x04001906 RID: 6406
		internal PreAllocatedOverlapped _preAllocated;

		// Token: 0x04001907 RID: 6407
		internal unsafe NativeOverlapped* _nativeOverlapped;

		// Token: 0x04001908 RID: 6408
		internal ThreadPoolBoundHandle _boundHandle;

		// Token: 0x04001909 RID: 6409
		internal bool _completed;
	}
}
