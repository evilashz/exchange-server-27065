using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004D9 RID: 1241
	[ComVisible(true)]
	public class Overlapped
	{
		// Token: 0x06003B7A RID: 15226 RVA: 0x000E0104 File Offset: 0x000DE304
		public Overlapped()
		{
			this.m_overlappedData = (OverlappedData)Overlapped.s_overlappedDataCache.Allocate();
			this.m_overlappedData.m_overlapped = this;
		}

		// Token: 0x06003B7B RID: 15227 RVA: 0x000E0130 File Offset: 0x000DE330
		public Overlapped(int offsetLo, int offsetHi, IntPtr hEvent, IAsyncResult ar)
		{
			this.m_overlappedData = (OverlappedData)Overlapped.s_overlappedDataCache.Allocate();
			this.m_overlappedData.m_overlapped = this;
			this.m_overlappedData.m_nativeOverlapped.OffsetLow = offsetLo;
			this.m_overlappedData.m_nativeOverlapped.OffsetHigh = offsetHi;
			this.m_overlappedData.UserHandle = hEvent;
			this.m_overlappedData.m_asyncResult = ar;
		}

		// Token: 0x06003B7C RID: 15228 RVA: 0x000E019F File Offset: 0x000DE39F
		[Obsolete("This constructor is not 64-bit compatible.  Use the constructor that takes an IntPtr for the event handle.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public Overlapped(int offsetLo, int offsetHi, int hEvent, IAsyncResult ar) : this(offsetLo, offsetHi, new IntPtr(hEvent), ar)
		{
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06003B7D RID: 15229 RVA: 0x000E01B1 File Offset: 0x000DE3B1
		// (set) Token: 0x06003B7E RID: 15230 RVA: 0x000E01BE File Offset: 0x000DE3BE
		public IAsyncResult AsyncResult
		{
			get
			{
				return this.m_overlappedData.m_asyncResult;
			}
			set
			{
				this.m_overlappedData.m_asyncResult = value;
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06003B7F RID: 15231 RVA: 0x000E01CC File Offset: 0x000DE3CC
		// (set) Token: 0x06003B80 RID: 15232 RVA: 0x000E01DE File Offset: 0x000DE3DE
		public int OffsetLow
		{
			get
			{
				return this.m_overlappedData.m_nativeOverlapped.OffsetLow;
			}
			set
			{
				this.m_overlappedData.m_nativeOverlapped.OffsetLow = value;
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06003B81 RID: 15233 RVA: 0x000E01F1 File Offset: 0x000DE3F1
		// (set) Token: 0x06003B82 RID: 15234 RVA: 0x000E0203 File Offset: 0x000DE403
		public int OffsetHigh
		{
			get
			{
				return this.m_overlappedData.m_nativeOverlapped.OffsetHigh;
			}
			set
			{
				this.m_overlappedData.m_nativeOverlapped.OffsetHigh = value;
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06003B83 RID: 15235 RVA: 0x000E0218 File Offset: 0x000DE418
		// (set) Token: 0x06003B84 RID: 15236 RVA: 0x000E0238 File Offset: 0x000DE438
		[Obsolete("This property is not 64-bit compatible.  Use EventHandleIntPtr instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public int EventHandle
		{
			get
			{
				return this.m_overlappedData.UserHandle.ToInt32();
			}
			set
			{
				this.m_overlappedData.UserHandle = new IntPtr(value);
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06003B85 RID: 15237 RVA: 0x000E024B File Offset: 0x000DE44B
		// (set) Token: 0x06003B86 RID: 15238 RVA: 0x000E0258 File Offset: 0x000DE458
		[ComVisible(false)]
		public IntPtr EventHandleIntPtr
		{
			get
			{
				return this.m_overlappedData.UserHandle;
			}
			set
			{
				this.m_overlappedData.UserHandle = value;
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06003B87 RID: 15239 RVA: 0x000E0266 File Offset: 0x000DE466
		internal _IOCompletionCallback iocbHelper
		{
			get
			{
				return this.m_overlappedData.m_iocbHelper;
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06003B88 RID: 15240 RVA: 0x000E0273 File Offset: 0x000DE473
		internal IOCompletionCallback UserCallback
		{
			[SecurityCritical]
			get
			{
				return this.m_overlappedData.m_iocb;
			}
		}

		// Token: 0x06003B89 RID: 15241 RVA: 0x000E0280 File Offset: 0x000DE480
		[SecurityCritical]
		[Obsolete("This method is not safe.  Use Pack (iocb, userData) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[CLSCompliant(false)]
		public unsafe NativeOverlapped* Pack(IOCompletionCallback iocb)
		{
			return this.Pack(iocb, null);
		}

		// Token: 0x06003B8A RID: 15242 RVA: 0x000E028A File Offset: 0x000DE48A
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe NativeOverlapped* Pack(IOCompletionCallback iocb, object userData)
		{
			return this.m_overlappedData.Pack(iocb, userData);
		}

		// Token: 0x06003B8B RID: 15243 RVA: 0x000E0299 File Offset: 0x000DE499
		[SecurityCritical]
		[Obsolete("This method is not safe.  Use UnsafePack (iocb, userData) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[CLSCompliant(false)]
		public unsafe NativeOverlapped* UnsafePack(IOCompletionCallback iocb)
		{
			return this.UnsafePack(iocb, null);
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x000E02A3 File Offset: 0x000DE4A3
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe NativeOverlapped* UnsafePack(IOCompletionCallback iocb, object userData)
		{
			return this.m_overlappedData.UnsafePack(iocb, userData);
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x000E02B4 File Offset: 0x000DE4B4
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe static Overlapped Unpack(NativeOverlapped* nativeOverlappedPtr)
		{
			if (nativeOverlappedPtr == null)
			{
				throw new ArgumentNullException("nativeOverlappedPtr");
			}
			return OverlappedData.GetOverlappedFromNative(nativeOverlappedPtr).m_overlapped;
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x000E02E0 File Offset: 0x000DE4E0
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe static void Free(NativeOverlapped* nativeOverlappedPtr)
		{
			if (nativeOverlappedPtr == null)
			{
				throw new ArgumentNullException("nativeOverlappedPtr");
			}
			Overlapped overlapped = OverlappedData.GetOverlappedFromNative(nativeOverlappedPtr).m_overlapped;
			OverlappedData.FreeNativeOverlapped(nativeOverlappedPtr);
			OverlappedData overlappedData = overlapped.m_overlappedData;
			overlapped.m_overlappedData = null;
			overlappedData.ReInitialize();
			Overlapped.s_overlappedDataCache.Free(overlappedData);
		}

		// Token: 0x040018FF RID: 6399
		private OverlappedData m_overlappedData;

		// Token: 0x04001900 RID: 6400
		private static PinnableBufferCache s_overlappedDataCache = new PinnableBufferCache("System.Threading.OverlappedData", () => new OverlappedData());
	}
}
