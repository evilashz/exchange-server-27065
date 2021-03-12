using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004D7 RID: 1239
	internal class _IOCompletionCallback
	{
		// Token: 0x06003B6D RID: 15213 RVA: 0x000DFE4A File Offset: 0x000DE04A
		[SecurityCritical]
		internal _IOCompletionCallback(IOCompletionCallback ioCompletionCallback, ref StackCrawlMark stackMark)
		{
			this._ioCompletionCallback = ioCompletionCallback;
			this._executionContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
		}

		// Token: 0x06003B6E RID: 15214 RVA: 0x000DFE68 File Offset: 0x000DE068
		[SecurityCritical]
		internal static void IOCompletionCallback_Context(object state)
		{
			_IOCompletionCallback iocompletionCallback = (_IOCompletionCallback)state;
			iocompletionCallback._ioCompletionCallback(iocompletionCallback._errorCode, iocompletionCallback._numBytes, iocompletionCallback._pOVERLAP);
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x000DFE9C File Offset: 0x000DE09C
		[SecurityCritical]
		internal unsafe static void PerformIOCompletionCallback(uint errorCode, uint numBytes, NativeOverlapped* pOVERLAP)
		{
			do
			{
				Overlapped overlapped = OverlappedData.GetOverlappedFromNative(pOVERLAP).m_overlapped;
				_IOCompletionCallback iocbHelper = overlapped.iocbHelper;
				if (iocbHelper == null || iocbHelper._executionContext == null || iocbHelper._executionContext.IsDefaultFTContext(true))
				{
					IOCompletionCallback userCallback = overlapped.UserCallback;
					userCallback(errorCode, numBytes, pOVERLAP);
				}
				else
				{
					iocbHelper._errorCode = errorCode;
					iocbHelper._numBytes = numBytes;
					iocbHelper._pOVERLAP = pOVERLAP;
					using (ExecutionContext executionContext = iocbHelper._executionContext.CreateCopy())
					{
						ExecutionContext.Run(executionContext, _IOCompletionCallback._ccb, iocbHelper, true);
					}
				}
				OverlappedData.CheckVMForIOPacket(out pOVERLAP, out errorCode, out numBytes);
			}
			while (pOVERLAP != null);
		}

		// Token: 0x040018EE RID: 6382
		[SecurityCritical]
		private IOCompletionCallback _ioCompletionCallback;

		// Token: 0x040018EF RID: 6383
		private ExecutionContext _executionContext;

		// Token: 0x040018F0 RID: 6384
		private uint _errorCode;

		// Token: 0x040018F1 RID: 6385
		private uint _numBytes;

		// Token: 0x040018F2 RID: 6386
		[SecurityCritical]
		private unsafe NativeOverlapped* _pOVERLAP;

		// Token: 0x040018F3 RID: 6387
		internal static ContextCallback _ccb = new ContextCallback(_IOCompletionCallback.IOCompletionCallback_Context);
	}
}
