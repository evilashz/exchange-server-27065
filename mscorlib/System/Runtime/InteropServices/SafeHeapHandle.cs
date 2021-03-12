using System;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200092B RID: 2347
	[SecurityCritical]
	internal sealed class SafeHeapHandle : SafeBuffer
	{
		// Token: 0x060060C8 RID: 24776 RVA: 0x0014A1CE File Offset: 0x001483CE
		public SafeHeapHandle(ulong byteLength) : base(true)
		{
			this.Resize(byteLength);
		}

		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x060060C9 RID: 24777 RVA: 0x0014A1DE File Offset: 0x001483DE
		public override bool IsInvalid
		{
			[SecurityCritical]
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x060060CA RID: 24778 RVA: 0x0014A1F0 File Offset: 0x001483F0
		public void Resize(ulong byteLength)
		{
			if (base.IsClosed)
			{
				throw new ObjectDisposedException("SafeHeapHandle");
			}
			ulong num = 0UL;
			if (this.handle == IntPtr.Zero)
			{
				this.handle = Marshal.AllocHGlobal((IntPtr)((long)byteLength));
			}
			else
			{
				num = base.ByteLength;
				this.handle = Marshal.ReAllocHGlobal(this.handle, (IntPtr)((long)byteLength));
			}
			if (this.handle == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			if (byteLength > num)
			{
				ulong num2 = byteLength - num;
				if (num2 > 9223372036854775807UL)
				{
					GC.AddMemoryPressure(long.MaxValue);
					GC.AddMemoryPressure((long)(num2 - 9223372036854775807UL));
				}
				else
				{
					GC.AddMemoryPressure((long)num2);
				}
			}
			else
			{
				this.RemoveMemoryPressure(num - byteLength);
			}
			base.Initialize(byteLength);
		}

		// Token: 0x060060CB RID: 24779 RVA: 0x0014A2BA File Offset: 0x001484BA
		private void RemoveMemoryPressure(ulong removedBytes)
		{
			if (removedBytes == 0UL)
			{
				return;
			}
			if (removedBytes > 9223372036854775807UL)
			{
				GC.RemoveMemoryPressure(long.MaxValue);
				GC.RemoveMemoryPressure((long)(removedBytes - 9223372036854775807UL));
				return;
			}
			GC.RemoveMemoryPressure((long)removedBytes);
		}

		// Token: 0x060060CC RID: 24780 RVA: 0x0014A2F4 File Offset: 0x001484F4
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			if (handle != IntPtr.Zero)
			{
				this.RemoveMemoryPressure(base.ByteLength);
				Marshal.FreeHGlobal(handle);
			}
			return true;
		}
	}
}
