using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020003BA RID: 954
	internal class MarshalledString : IDisposable
	{
		// Token: 0x060010C3 RID: 4291 RVA: 0x0004FB44 File Offset: 0x0004EF44
		public MarshalledString(string stringToMarshal)
		{
			if (stringToMarshal != null)
			{
				IntPtr intPtr = Marshal.StringToHGlobalUni(stringToMarshal);
				IntPtr hglobal = intPtr;
				bool flag = false;
				try
				{
					this.nativeString = new SafeMarshalHGlobalHandle(intPtr);
					flag = true;
					return;
				}
				finally
				{
					if (!flag)
					{
						Marshal.FreeHGlobal(hglobal);
					}
				}
			}
			this.nativeString = new SafeMarshalHGlobalHandle();
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0004FBAC File Offset: 0x0004EFAC
		private void ~MarshalledString()
		{
			IDisposable disposable = this.nativeString;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0004FBCC File Offset: 0x0004EFCC
		public unsafe implicit operator ushort*()
		{
			return (ushort*)this.nativeString.DangerousGetHandle().ToPointer();
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060010C6 RID: 4294 RVA: 0x0004FBEC File Offset: 0x0004EFEC
		public unsafe int Length
		{
			get
			{
				ulong num;
				if (this.nativeString.IsInvalid)
				{
					num = 0UL;
				}
				else
				{
					void* ptr = this.nativeString.DangerousGetHandle().ToPointer();
					void* ptr2 = ptr;
					if (*(short*)ptr != 0)
					{
						do
						{
							ptr2 = (void*)((byte*)ptr2 + 2L);
						}
						while (*(short*)ptr2 != 0);
					}
					num = (ulong)(ptr2 - ptr >> 1);
				}
				return (int)num;
			}
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0004FC3C File Offset: 0x0004F03C
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.~MarshalledString();
			}
			else
			{
				base.Finalize();
			}
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x00050B50 File Offset: 0x0004FF50
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000FD3 RID: 4051
		private SafeMarshalHGlobalHandle nativeString;
	}
}
