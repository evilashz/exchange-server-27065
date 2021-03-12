using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020001DA RID: 474
	public class SafeRpcMemoryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060009E8 RID: 2536 RVA: 0x000561F0 File Offset: 0x000555F0
		public SafeRpcMemoryHandle(int size) : base(true)
		{
			try
			{
				this.Allocate(size);
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00056024 File Offset: 0x00055424
		public SafeRpcMemoryHandle(IntPtr handle) : base(true)
		{
			try
			{
				base.SetHandle(handle);
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00056010 File Offset: 0x00055410
		public SafeRpcMemoryHandle() : base(true)
		{
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x000561CC File Offset: 0x000555CC
		public void Allocate(int size)
		{
			if (size < 0)
			{
				throw new ArgumentException("size");
			}
			this.Allocate((ulong)((long)size));
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00056174 File Offset: 0x00055574
		public void Allocate(ulong size)
		{
			this.ReleaseHandle();
			IntPtr handle = new IntPtr(<Module>.MIDL_user_allocate(size));
			this.handle = handle;
			if (this.handle == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			initblk(this.handle.ToPointer(), 0, size);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00056068 File Offset: 0x00055468
		public void AddAssociatedHandle(SafeRpcMemoryHandle midlHandle)
		{
			if (this.associatedHandles == null)
			{
				this.associatedHandles = new List<SafeRpcMemoryHandle>(10);
			}
			this.associatedHandles.Add(midlHandle);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00056098 File Offset: 0x00055498
		public IntPtr Detach()
		{
			List<SafeRpcMemoryHandle> list = this.associatedHandles;
			if (list != null)
			{
				List<SafeRpcMemoryHandle>.Enumerator enumerator = list.GetEnumerator();
				if (enumerator.MoveNext())
				{
					do
					{
						SafeRpcMemoryHandle safeRpcMemoryHandle = enumerator.Current;
						safeRpcMemoryHandle.Detach();
						if (safeRpcMemoryHandle != null)
						{
							((IDisposable)safeRpcMemoryHandle).Dispose();
						}
					}
					while (enumerator.MoveNext());
				}
				this.associatedHandles.Clear();
				this.associatedHandles = null;
			}
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			return handle;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00056108 File Offset: 0x00055508
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		protected override bool ReleaseHandle()
		{
			if (!this.IsInvalid)
			{
				List<SafeRpcMemoryHandle> list = this.associatedHandles;
				if (list != null)
				{
					List<SafeRpcMemoryHandle>.Enumerator enumerator = list.GetEnumerator();
					if (enumerator.MoveNext())
					{
						do
						{
							IDisposable disposable = enumerator.Current;
							if (disposable != null)
							{
								disposable.Dispose();
							}
						}
						while (enumerator.MoveNext());
					}
					this.associatedHandles.Clear();
					this.associatedHandles = null;
				}
				<Module>.MIDL_user_free(this.handle.ToPointer());
			}
			return true;
		}

		// Token: 0x04000B88 RID: 2952
		private List<SafeRpcMemoryHandle> associatedHandles;
	}
}
