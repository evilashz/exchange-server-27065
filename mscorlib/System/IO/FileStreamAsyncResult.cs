using System;
using System.Diagnostics.Tracing;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.IO
{
	// Token: 0x0200018B RID: 395
	internal sealed class FileStreamAsyncResult : IAsyncResult
	{
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x0600182E RID: 6190 RVA: 0x0004D968 File Offset: 0x0004BB68
		internal unsafe NativeOverlapped* OverLapped
		{
			[SecurityCritical]
			get
			{
				return this._overlapped;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x0600182F RID: 6191 RVA: 0x0004D970 File Offset: 0x0004BB70
		internal bool IsAsync
		{
			[SecuritySafeCritical]
			get
			{
				return this._overlapped != null;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06001830 RID: 6192 RVA: 0x0004D97F File Offset: 0x0004BB7F
		internal int NumBytes
		{
			get
			{
				return this._numBytes;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x0004D987 File Offset: 0x0004BB87
		internal int ErrorCode
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x0004D98F File Offset: 0x0004BB8F
		internal int NumBufferedBytes
		{
			get
			{
				return this._numBufferedBytes;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06001833 RID: 6195 RVA: 0x0004D997 File Offset: 0x0004BB97
		internal int NumBytesRead
		{
			get
			{
				return this._numBytes + this._numBufferedBytes;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06001834 RID: 6196 RVA: 0x0004D9A6 File Offset: 0x0004BBA6
		internal bool IsWrite
		{
			get
			{
				return this._isWrite;
			}
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x0004D9B0 File Offset: 0x0004BBB0
		[SecuritySafeCritical]
		internal FileStreamAsyncResult(int numBufferedBytes, byte[] bytes, SafeFileHandle handle, AsyncCallback userCallback, object userStateObject, bool isWrite)
		{
			this._userCallback = userCallback;
			this._userStateObject = userStateObject;
			this._isWrite = isWrite;
			this._numBufferedBytes = numBufferedBytes;
			this._handle = handle;
			ManualResetEvent waitHandle = new ManualResetEvent(false);
			this._waitHandle = waitHandle;
			Overlapped overlapped = new Overlapped(0, 0, IntPtr.Zero, this);
			if (userCallback != null)
			{
				IOCompletionCallback iocompletionCallback = FileStreamAsyncResult.s_IOCallback;
				if (iocompletionCallback == null)
				{
					iocompletionCallback = (FileStreamAsyncResult.s_IOCallback = new IOCompletionCallback(FileStreamAsyncResult.AsyncFSCallback));
				}
				this._overlapped = overlapped.Pack(iocompletionCallback, bytes);
				return;
			}
			this._overlapped = overlapped.UnsafePack(null, bytes);
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x0004DA44 File Offset: 0x0004BC44
		internal static FileStreamAsyncResult CreateBufferedReadResult(int numBufferedBytes, AsyncCallback userCallback, object userStateObject, bool isWrite)
		{
			FileStreamAsyncResult fileStreamAsyncResult = new FileStreamAsyncResult(numBufferedBytes, userCallback, userStateObject, isWrite);
			fileStreamAsyncResult.CallUserCallback();
			return fileStreamAsyncResult;
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x0004DA62 File Offset: 0x0004BC62
		private FileStreamAsyncResult(int numBufferedBytes, AsyncCallback userCallback, object userStateObject, bool isWrite)
		{
			this._userCallback = userCallback;
			this._userStateObject = userStateObject;
			this._isWrite = isWrite;
			this._numBufferedBytes = numBufferedBytes;
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06001838 RID: 6200 RVA: 0x0004DA87 File Offset: 0x0004BC87
		public object AsyncState
		{
			get
			{
				return this._userStateObject;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x0004DA8F File Offset: 0x0004BC8F
		public bool IsCompleted
		{
			get
			{
				return this._isComplete;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x0004DA98 File Offset: 0x0004BC98
		public unsafe WaitHandle AsyncWaitHandle
		{
			[SecuritySafeCritical]
			get
			{
				if (this._waitHandle == null)
				{
					ManualResetEvent manualResetEvent = new ManualResetEvent(false);
					if (this._overlapped != null && this._overlapped->EventHandle != IntPtr.Zero)
					{
						manualResetEvent.SafeWaitHandle = new SafeWaitHandle(this._overlapped->EventHandle, true);
					}
					if (Interlocked.CompareExchange<ManualResetEvent>(ref this._waitHandle, manualResetEvent, null) == null)
					{
						if (this._isComplete)
						{
							this._waitHandle.Set();
						}
					}
					else
					{
						manualResetEvent.Close();
					}
				}
				return this._waitHandle;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x0004DB1D File Offset: 0x0004BD1D
		public bool CompletedSynchronously
		{
			get
			{
				return this._completedSynchronously;
			}
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x0004DB25 File Offset: 0x0004BD25
		private void CallUserCallbackWorker()
		{
			this._isComplete = true;
			Thread.MemoryBarrier();
			if (this._waitHandle != null)
			{
				this._waitHandle.Set();
			}
			this._userCallback(this);
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x0004DB54 File Offset: 0x0004BD54
		internal void CallUserCallback()
		{
			if (this._userCallback != null)
			{
				this._completedSynchronously = false;
				ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					((FileStreamAsyncResult)state).CallUserCallbackWorker();
				}, this);
				return;
			}
			this._isComplete = true;
			Thread.MemoryBarrier();
			if (this._waitHandle != null)
			{
				this._waitHandle.Set();
			}
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x0004DBB7 File Offset: 0x0004BDB7
		[SecurityCritical]
		internal void ReleaseNativeResource()
		{
			if (this._overlapped != null)
			{
				Overlapped.Free(this._overlapped);
			}
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x0004DBD0 File Offset: 0x0004BDD0
		internal void Wait()
		{
			if (this._waitHandle != null)
			{
				try
				{
					this._waitHandle.WaitOne();
				}
				finally
				{
					this._waitHandle.Close();
				}
			}
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x0004DC10 File Offset: 0x0004BE10
		[SecurityCritical]
		private unsafe static void AsyncFSCallback(uint errorCode, uint numBytes, NativeOverlapped* pOverlapped)
		{
			Overlapped overlapped = Overlapped.Unpack(pOverlapped);
			FileStreamAsyncResult fileStreamAsyncResult = (FileStreamAsyncResult)overlapped.AsyncResult;
			fileStreamAsyncResult._numBytes = (int)numBytes;
			if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords)16L))
			{
				FrameworkEventSource.Log.ThreadTransferReceive(fileStreamAsyncResult.OverLapped, 2, string.Empty);
			}
			if (errorCode == 109U || errorCode == 232U)
			{
				errorCode = 0U;
			}
			fileStreamAsyncResult._errorCode = (int)errorCode;
			fileStreamAsyncResult._completedSynchronously = false;
			fileStreamAsyncResult._isComplete = true;
			Thread.MemoryBarrier();
			ManualResetEvent waitHandle = fileStreamAsyncResult._waitHandle;
			if (waitHandle != null && !waitHandle.Set())
			{
				__Error.WinIOError();
			}
			AsyncCallback userCallback = fileStreamAsyncResult._userCallback;
			if (userCallback != null)
			{
				userCallback(fileStreamAsyncResult);
			}
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x0004DCBC File Offset: 0x0004BEBC
		[SecuritySafeCritical]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		internal void Cancel()
		{
			if (this.IsCompleted)
			{
				return;
			}
			if (this._handle.IsInvalid)
			{
				return;
			}
			if (!Win32Native.CancelIoEx(this._handle, this._overlapped))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != 1168)
				{
					__Error.WinIOError(lastWin32Error, string.Empty);
				}
			}
		}

		// Token: 0x04000856 RID: 2134
		private AsyncCallback _userCallback;

		// Token: 0x04000857 RID: 2135
		private object _userStateObject;

		// Token: 0x04000858 RID: 2136
		private ManualResetEvent _waitHandle;

		// Token: 0x04000859 RID: 2137
		[SecurityCritical]
		private SafeFileHandle _handle;

		// Token: 0x0400085A RID: 2138
		[SecurityCritical]
		private unsafe NativeOverlapped* _overlapped;

		// Token: 0x0400085B RID: 2139
		internal int _EndXxxCalled;

		// Token: 0x0400085C RID: 2140
		private int _numBytes;

		// Token: 0x0400085D RID: 2141
		private int _errorCode;

		// Token: 0x0400085E RID: 2142
		private int _numBufferedBytes;

		// Token: 0x0400085F RID: 2143
		private bool _isWrite;

		// Token: 0x04000860 RID: 2144
		private bool _isComplete;

		// Token: 0x04000861 RID: 2145
		private bool _completedSynchronously;

		// Token: 0x04000862 RID: 2146
		[SecurityCritical]
		private static IOCompletionCallback s_IOCallback;
	}
}
