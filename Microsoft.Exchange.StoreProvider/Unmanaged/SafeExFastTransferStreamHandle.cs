using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002B8 RID: 696
	[ComVisible(false)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SafeExFastTransferStreamHandle : SafeExInterfaceHandle
	{
		// Token: 0x06000CEA RID: 3306 RVA: 0x0003444A File Offset: 0x0003264A
		protected SafeExFastTransferStreamHandle()
		{
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00034452 File Offset: 0x00032652
		internal SafeExFastTransferStreamHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0003445B File Offset: 0x0003265B
		internal SafeExFastTransferStreamHandle(SafeExInterfaceHandle innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00034464 File Offset: 0x00032664
		internal unsafe int Configure(int cValues, SPropValue* lpPropArray)
		{
			return SafeExFastTransferStreamHandle.IFastTransferStream_Configure(this.handle, cValues, lpPropArray);
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00034473 File Offset: 0x00032673
		internal int Download(out int cbBuffer, out SafeExMemoryHandle buffer)
		{
			return SafeExFastTransferStreamHandle.IFastTransferStream_Download(this.handle, out cbBuffer, out buffer);
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00034484 File Offset: 0x00032684
		internal unsafe int Upload(ArraySegment<byte> buffer)
		{
			fixed (byte* array = buffer.Array)
			{
				return SafeExFastTransferStreamHandle.IFastTransferStream_Upload(this.handle, buffer.Count, array + buffer.Offset);
			}
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x000344CE File Offset: 0x000326CE
		internal int Flush()
		{
			return SafeExFastTransferStreamHandle.IFastTransferStream_Flush(this.handle);
		}

		// Token: 0x06000CF1 RID: 3313
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IFastTransferStream_Configure(IntPtr pIFastTransferStream, int cValues, SPropValue* lpPropArray);

		// Token: 0x06000CF2 RID: 3314
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IFastTransferStream_Download(IntPtr pIFastTransferStream, out int cbBuffer, out SafeExMemoryHandle buffer);

		// Token: 0x06000CF3 RID: 3315
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IFastTransferStream_Upload(IntPtr pIFastTransferStream, int cb, byte* lpb);

		// Token: 0x06000CF4 RID: 3316
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IFastTransferStream_Flush(IntPtr pIFastTransferStream);
	}
}
