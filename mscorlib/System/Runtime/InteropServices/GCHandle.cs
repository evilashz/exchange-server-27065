using System;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200091B RID: 2331
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public struct GCHandle
	{
		// Token: 0x06005F63 RID: 24419 RVA: 0x001477AA File Offset: 0x001459AA
		[SecuritySafeCritical]
		static GCHandle()
		{
			if (GCHandle.s_probeIsActive)
			{
				GCHandle.s_cookieTable = new GCHandleCookieTable();
			}
		}

		// Token: 0x06005F64 RID: 24420 RVA: 0x001477CD File Offset: 0x001459CD
		[SecurityCritical]
		internal GCHandle(object value, GCHandleType type)
		{
			if (type > GCHandleType.Pinned)
			{
				throw new ArgumentOutOfRangeException("type", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
			}
			this.m_handle = GCHandle.InternalAlloc(value, type);
			if (type == GCHandleType.Pinned)
			{
				this.SetIsPinned();
			}
		}

		// Token: 0x06005F65 RID: 24421 RVA: 0x001477FF File Offset: 0x001459FF
		[SecurityCritical]
		internal GCHandle(IntPtr handle)
		{
			GCHandle.InternalCheckDomain(handle);
			this.m_handle = handle;
		}

		// Token: 0x06005F66 RID: 24422 RVA: 0x0014780E File Offset: 0x00145A0E
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static GCHandle Alloc(object value)
		{
			return new GCHandle(value, GCHandleType.Normal);
		}

		// Token: 0x06005F67 RID: 24423 RVA: 0x00147817 File Offset: 0x00145A17
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static GCHandle Alloc(object value, GCHandleType type)
		{
			return new GCHandle(value, type);
		}

		// Token: 0x06005F68 RID: 24424 RVA: 0x00147820 File Offset: 0x00145A20
		[SecurityCritical]
		[__DynamicallyInvokable]
		public void Free()
		{
			IntPtr handle = this.m_handle;
			if (handle != IntPtr.Zero && Interlocked.CompareExchange(ref this.m_handle, IntPtr.Zero, handle) == handle)
			{
				if (GCHandle.s_probeIsActive)
				{
					GCHandle.s_cookieTable.RemoveHandleIfPresent(handle);
				}
				GCHandle.InternalFree((IntPtr)((long)handle & -2L));
				return;
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
		}

		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x06005F69 RID: 24425 RVA: 0x00147894 File Offset: 0x00145A94
		// (set) Token: 0x06005F6A RID: 24426 RVA: 0x001478C3 File Offset: 0x00145AC3
		[__DynamicallyInvokable]
		public object Target
		{
			[SecurityCritical]
			[__DynamicallyInvokable]
			get
			{
				if (this.m_handle == IntPtr.Zero)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
				}
				return GCHandle.InternalGet(this.GetHandleValue());
			}
			[SecurityCritical]
			[__DynamicallyInvokable]
			set
			{
				if (this.m_handle == IntPtr.Zero)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
				}
				GCHandle.InternalSet(this.GetHandleValue(), value, this.IsPinned());
			}
		}

		// Token: 0x06005F6B RID: 24427 RVA: 0x001478FC File Offset: 0x00145AFC
		[SecurityCritical]
		public IntPtr AddrOfPinnedObject()
		{
			if (this.IsPinned())
			{
				return GCHandle.InternalAddrOfPinnedObject(this.GetHandleValue());
			}
			if (this.m_handle == IntPtr.Zero)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotPinned"));
		}

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x06005F6C RID: 24428 RVA: 0x0014794E File Offset: 0x00145B4E
		[__DynamicallyInvokable]
		public bool IsAllocated
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_handle != IntPtr.Zero;
			}
		}

		// Token: 0x06005F6D RID: 24429 RVA: 0x00147960 File Offset: 0x00145B60
		[SecurityCritical]
		public static explicit operator GCHandle(IntPtr value)
		{
			return GCHandle.FromIntPtr(value);
		}

		// Token: 0x06005F6E RID: 24430 RVA: 0x00147968 File Offset: 0x00145B68
		[SecurityCritical]
		public static GCHandle FromIntPtr(IntPtr value)
		{
			if (value == IntPtr.Zero)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
			}
			IntPtr intPtr = value;
			if (GCHandle.s_probeIsActive)
			{
				intPtr = GCHandle.s_cookieTable.GetHandle(value);
				if (IntPtr.Zero == intPtr)
				{
					Mda.FireInvalidGCHandleCookieProbe(value);
					return new GCHandle(IntPtr.Zero);
				}
			}
			return new GCHandle(intPtr);
		}

		// Token: 0x06005F6F RID: 24431 RVA: 0x001479CF File Offset: 0x00145BCF
		public static explicit operator IntPtr(GCHandle value)
		{
			return GCHandle.ToIntPtr(value);
		}

		// Token: 0x06005F70 RID: 24432 RVA: 0x001479D7 File Offset: 0x00145BD7
		public static IntPtr ToIntPtr(GCHandle value)
		{
			if (GCHandle.s_probeIsActive)
			{
				return GCHandle.s_cookieTable.FindOrAddHandle(value.m_handle);
			}
			return value.m_handle;
		}

		// Token: 0x06005F71 RID: 24433 RVA: 0x001479FB File Offset: 0x00145BFB
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.m_handle.GetHashCode();
		}

		// Token: 0x06005F72 RID: 24434 RVA: 0x00147A08 File Offset: 0x00145C08
		[__DynamicallyInvokable]
		public override bool Equals(object o)
		{
			if (o == null || !(o is GCHandle))
			{
				return false;
			}
			GCHandle gchandle = (GCHandle)o;
			return this.m_handle == gchandle.m_handle;
		}

		// Token: 0x06005F73 RID: 24435 RVA: 0x00147A3A File Offset: 0x00145C3A
		[__DynamicallyInvokable]
		public static bool operator ==(GCHandle a, GCHandle b)
		{
			return a.m_handle == b.m_handle;
		}

		// Token: 0x06005F74 RID: 24436 RVA: 0x00147A4D File Offset: 0x00145C4D
		[__DynamicallyInvokable]
		public static bool operator !=(GCHandle a, GCHandle b)
		{
			return a.m_handle != b.m_handle;
		}

		// Token: 0x06005F75 RID: 24437 RVA: 0x00147A60 File Offset: 0x00145C60
		internal IntPtr GetHandleValue()
		{
			return new IntPtr((long)this.m_handle & -2L);
		}

		// Token: 0x06005F76 RID: 24438 RVA: 0x00147A76 File Offset: 0x00145C76
		internal bool IsPinned()
		{
			return ((long)this.m_handle & 1L) != 0L;
		}

		// Token: 0x06005F77 RID: 24439 RVA: 0x00147A8A File Offset: 0x00145C8A
		internal void SetIsPinned()
		{
			this.m_handle = new IntPtr((long)this.m_handle | 1L);
		}

		// Token: 0x06005F78 RID: 24440
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr InternalAlloc(object value, GCHandleType type);

		// Token: 0x06005F79 RID: 24441
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalFree(IntPtr handle);

		// Token: 0x06005F7A RID: 24442
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object InternalGet(IntPtr handle);

		// Token: 0x06005F7B RID: 24443
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalSet(IntPtr handle, object value, bool isPinned);

		// Token: 0x06005F7C RID: 24444
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object InternalCompareExchange(IntPtr handle, object value, object oldValue, bool isPinned);

		// Token: 0x06005F7D RID: 24445
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr InternalAddrOfPinnedObject(IntPtr handle);

		// Token: 0x06005F7E RID: 24446
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalCheckDomain(IntPtr handle);

		// Token: 0x06005F7F RID: 24447
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern GCHandleType InternalGetHandleType(IntPtr handle);

		// Token: 0x04002A8A RID: 10890
		private const GCHandleType MaxHandleType = GCHandleType.Pinned;

		// Token: 0x04002A8B RID: 10891
		private IntPtr m_handle;

		// Token: 0x04002A8C RID: 10892
		private static volatile GCHandleCookieTable s_cookieTable;

		// Token: 0x04002A8D RID: 10893
		private static volatile bool s_probeIsActive = Mda.IsInvalidGCHandleCookieProbeEnabled();
	}
}
