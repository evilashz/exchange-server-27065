using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004D8 RID: 1240
	internal sealed class OverlappedData
	{
		// Token: 0x06003B70 RID: 15216 RVA: 0x000DFF44 File Offset: 0x000DE144
		[SecurityCritical]
		internal void ReInitialize()
		{
			this.m_asyncResult = null;
			this.m_iocb = null;
			this.m_iocbHelper = null;
			this.m_overlapped = null;
			this.m_userObject = null;
			this.m_pinSelf = (IntPtr)0;
			this.m_userObjectInternal = (IntPtr)0;
			this.m_AppDomainId = 0;
			this.m_nativeOverlapped.EventHandle = (IntPtr)0;
			this.m_isArray = 0;
			this.m_nativeOverlapped.InternalLow = (IntPtr)0;
			this.m_nativeOverlapped.InternalHigh = (IntPtr)0;
		}

		// Token: 0x06003B71 RID: 15217 RVA: 0x000DFFD0 File Offset: 0x000DE1D0
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal unsafe NativeOverlapped* Pack(IOCompletionCallback iocb, object userData)
		{
			if (!this.m_pinSelf.IsNull())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_Overlapped_Pack"));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			if (iocb != null)
			{
				this.m_iocbHelper = new _IOCompletionCallback(iocb, ref stackCrawlMark);
				this.m_iocb = iocb;
			}
			else
			{
				this.m_iocbHelper = null;
				this.m_iocb = null;
			}
			this.m_userObject = userData;
			if (this.m_userObject != null)
			{
				if (this.m_userObject.GetType() == typeof(object[]))
				{
					this.m_isArray = 1;
				}
				else
				{
					this.m_isArray = 0;
				}
			}
			return this.AllocateNativeOverlapped();
		}

		// Token: 0x06003B72 RID: 15218 RVA: 0x000E0068 File Offset: 0x000DE268
		[SecurityCritical]
		internal unsafe NativeOverlapped* UnsafePack(IOCompletionCallback iocb, object userData)
		{
			if (!this.m_pinSelf.IsNull())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_Overlapped_Pack"));
			}
			this.m_userObject = userData;
			if (this.m_userObject != null)
			{
				if (this.m_userObject.GetType() == typeof(object[]))
				{
					this.m_isArray = 1;
				}
				else
				{
					this.m_isArray = 0;
				}
			}
			this.m_iocb = iocb;
			this.m_iocbHelper = null;
			return this.AllocateNativeOverlapped();
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06003B73 RID: 15219 RVA: 0x000E00E1 File Offset: 0x000DE2E1
		// (set) Token: 0x06003B74 RID: 15220 RVA: 0x000E00EE File Offset: 0x000DE2EE
		[ComVisible(false)]
		internal IntPtr UserHandle
		{
			get
			{
				return this.m_nativeOverlapped.EventHandle;
			}
			set
			{
				this.m_nativeOverlapped.EventHandle = value;
			}
		}

		// Token: 0x06003B75 RID: 15221
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern NativeOverlapped* AllocateNativeOverlapped();

		// Token: 0x06003B76 RID: 15222
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void FreeNativeOverlapped(NativeOverlapped* nativeOverlappedPtr);

		// Token: 0x06003B77 RID: 15223
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern OverlappedData GetOverlappedFromNative(NativeOverlapped* nativeOverlappedPtr);

		// Token: 0x06003B78 RID: 15224
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void CheckVMForIOPacket(out NativeOverlapped* pOVERLAP, out uint errorCode, out uint numBytes);

		// Token: 0x040018F4 RID: 6388
		internal IAsyncResult m_asyncResult;

		// Token: 0x040018F5 RID: 6389
		[SecurityCritical]
		internal IOCompletionCallback m_iocb;

		// Token: 0x040018F6 RID: 6390
		internal _IOCompletionCallback m_iocbHelper;

		// Token: 0x040018F7 RID: 6391
		internal Overlapped m_overlapped;

		// Token: 0x040018F8 RID: 6392
		private object m_userObject;

		// Token: 0x040018F9 RID: 6393
		private IntPtr m_pinSelf;

		// Token: 0x040018FA RID: 6394
		private IntPtr m_userObjectInternal;

		// Token: 0x040018FB RID: 6395
		private int m_AppDomainId;

		// Token: 0x040018FC RID: 6396
		private byte m_isArray;

		// Token: 0x040018FD RID: 6397
		private byte m_toBeCleaned;

		// Token: 0x040018FE RID: 6398
		internal NativeOverlapped m_nativeOverlapped;
	}
}
