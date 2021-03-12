using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200092E RID: 2350
	internal class NativeBuffer : IDisposable
	{
		// Token: 0x060060EA RID: 24810 RVA: 0x0014A9E3 File Offset: 0x00148BE3
		[SecuritySafeCritical]
		static NativeBuffer()
		{
			NativeBuffer.s_handleCache = new SafeHeapHandleCache(64UL, 2048UL, 0);
		}

		// Token: 0x060060EB RID: 24811 RVA: 0x0014AA03 File Offset: 0x00148C03
		public NativeBuffer(ulong initialMinCapacity = 0UL)
		{
			this.EnsureByteCapacity(initialMinCapacity);
		}

		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x060060EC RID: 24812 RVA: 0x0014AA14 File Offset: 0x00148C14
		protected unsafe void* VoidPointer
		{
			[SecurityCritical]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (this._handle != null)
				{
					return this._handle.DangerousGetHandle().ToPointer();
				}
				return null;
			}
		}

		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x060060ED RID: 24813 RVA: 0x0014AA3F File Offset: 0x00148C3F
		protected unsafe byte* BytePointer
		{
			[SecurityCritical]
			get
			{
				return (byte*)this.VoidPointer;
			}
		}

		// Token: 0x060060EE RID: 24814 RVA: 0x0014AA47 File Offset: 0x00148C47
		[SecuritySafeCritical]
		public SafeHandle GetHandle()
		{
			return this._handle ?? NativeBuffer.s_emptyHandle;
		}

		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x060060EF RID: 24815 RVA: 0x0014AA58 File Offset: 0x00148C58
		public ulong ByteCapacity
		{
			get
			{
				return this._capacity;
			}
		}

		// Token: 0x060060F0 RID: 24816 RVA: 0x0014AA60 File Offset: 0x00148C60
		[SecuritySafeCritical]
		public void EnsureByteCapacity(ulong minCapacity)
		{
			if (this._capacity < minCapacity)
			{
				this.Resize(minCapacity);
				this._capacity = minCapacity;
			}
		}

		// Token: 0x170010F8 RID: 4344
		public unsafe byte this[ulong index]
		{
			[SecuritySafeCritical]
			get
			{
				if (index >= this._capacity)
				{
					throw new ArgumentOutOfRangeException();
				}
				return this.BytePointer[index];
			}
			[SecuritySafeCritical]
			set
			{
				if (index >= this._capacity)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.BytePointer[index] = value;
			}
		}

		// Token: 0x060060F3 RID: 24819 RVA: 0x0014AAB0 File Offset: 0x00148CB0
		[SecuritySafeCritical]
		private void Resize(ulong byteLength)
		{
			if (byteLength == 0UL)
			{
				this.ReleaseHandle();
				return;
			}
			if (this._handle == null)
			{
				this._handle = NativeBuffer.s_handleCache.Acquire(byteLength);
				return;
			}
			this._handle.Resize(byteLength);
		}

		// Token: 0x060060F4 RID: 24820 RVA: 0x0014AAE2 File Offset: 0x00148CE2
		[SecuritySafeCritical]
		private void ReleaseHandle()
		{
			if (this._handle != null)
			{
				NativeBuffer.s_handleCache.Release(this._handle);
				this._capacity = 0UL;
				this._handle = null;
			}
		}

		// Token: 0x060060F5 RID: 24821 RVA: 0x0014AB0B File Offset: 0x00148D0B
		[SecuritySafeCritical]
		public virtual void Free()
		{
			this.ReleaseHandle();
		}

		// Token: 0x060060F6 RID: 24822 RVA: 0x0014AB13 File Offset: 0x00148D13
		[SecuritySafeCritical]
		public void Dispose()
		{
			this.Free();
		}

		// Token: 0x04002ACC RID: 10956
		private static readonly SafeHeapHandleCache s_handleCache;

		// Token: 0x04002ACD RID: 10957
		[SecurityCritical]
		private static readonly SafeHandle s_emptyHandle = new NativeBuffer.EmptySafeHandle();

		// Token: 0x04002ACE RID: 10958
		[SecurityCritical]
		private SafeHeapHandle _handle;

		// Token: 0x04002ACF RID: 10959
		private ulong _capacity;

		// Token: 0x02000C63 RID: 3171
		[SecurityCritical]
		private sealed class EmptySafeHandle : SafeHandle
		{
			// Token: 0x06006FEB RID: 28651 RVA: 0x00180736 File Offset: 0x0017E936
			public EmptySafeHandle() : base(IntPtr.Zero, true)
			{
			}

			// Token: 0x1700134A RID: 4938
			// (get) Token: 0x06006FEC RID: 28652 RVA: 0x00180744 File Offset: 0x0017E944
			public override bool IsInvalid
			{
				[SecurityCritical]
				get
				{
					return true;
				}
			}

			// Token: 0x06006FED RID: 28653 RVA: 0x00180747 File Offset: 0x0017E947
			[SecurityCritical]
			protected override bool ReleaseHandle()
			{
				return true;
			}
		}
	}
}
