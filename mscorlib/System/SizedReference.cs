﻿using System;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System
{
	// Token: 0x020000EA RID: 234
	internal class SizedReference : IDisposable
	{
		// Token: 0x06000EC5 RID: 3781
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateSizedRef(object o);

		// Token: 0x06000EC6 RID: 3782
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FreeSizedRef(IntPtr h);

		// Token: 0x06000EC7 RID: 3783
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object GetTargetOfSizedRef(IntPtr h);

		// Token: 0x06000EC8 RID: 3784
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetApproximateSizeOfSizedRef(IntPtr h);

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0002D108 File Offset: 0x0002B308
		[SecuritySafeCritical]
		private void Free()
		{
			IntPtr handle = this._handle;
			if (handle != IntPtr.Zero && Interlocked.CompareExchange(ref this._handle, IntPtr.Zero, handle) == handle)
			{
				SizedReference.FreeSizedRef(handle);
			}
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0002D14C File Offset: 0x0002B34C
		[SecuritySafeCritical]
		public SizedReference(object target)
		{
			IntPtr handle = IntPtr.Zero;
			handle = SizedReference.CreateSizedRef(target);
			this._handle = handle;
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0002D178 File Offset: 0x0002B378
		~SizedReference()
		{
			this.Free();
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x0002D1A4 File Offset: 0x0002B3A4
		public object Target
		{
			[SecuritySafeCritical]
			get
			{
				IntPtr handle = this._handle;
				if (handle == IntPtr.Zero)
				{
					return null;
				}
				object targetOfSizedRef = SizedReference.GetTargetOfSizedRef(handle);
				if (!(this._handle == IntPtr.Zero))
				{
					return targetOfSizedRef;
				}
				return null;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x0002D1E8 File Offset: 0x0002B3E8
		public long ApproximateSize
		{
			[SecuritySafeCritical]
			get
			{
				IntPtr handle = this._handle;
				if (handle == IntPtr.Zero)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
				}
				long approximateSizeOfSizedRef = SizedReference.GetApproximateSizeOfSizedRef(handle);
				if (this._handle == IntPtr.Zero)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
				}
				return approximateSizeOfSizedRef;
			}
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0002D247 File Offset: 0x0002B447
		public void Dispose()
		{
			this.Free();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400058C RID: 1420
		internal volatile IntPtr _handle;
	}
}
