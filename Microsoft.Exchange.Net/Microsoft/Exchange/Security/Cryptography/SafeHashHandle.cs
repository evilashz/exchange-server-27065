using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Security.Cryptography
{
	// Token: 0x02000ADA RID: 2778
	internal sealed class SafeHashHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003BA7 RID: 15271 RVA: 0x0009932B File Offset: 0x0009752B
		internal SafeHashHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06003BA8 RID: 15272 RVA: 0x0009933B File Offset: 0x0009753B
		private SafeHashHandle() : base(true)
		{
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06003BA9 RID: 15273 RVA: 0x00099344 File Offset: 0x00097544
		public static SafeHashHandle InvalidHandle
		{
			get
			{
				return new SafeHashHandle(IntPtr.Zero);
			}
		}

		// Token: 0x06003BAA RID: 15274 RVA: 0x00099350 File Offset: 0x00097550
		public static SafeHashHandle Create(SafeCryptProvHandle provider, CapiNativeMethods.AlgorithmId hashType)
		{
			SafeHashHandle invalidHandle = SafeHashHandle.InvalidHandle;
			if (!CapiNativeMethods.CryptCreateHash(provider.DangerousGetHandle(), hashType, IntPtr.Zero, 0, ref invalidHandle))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return invalidHandle;
		}

		// Token: 0x06003BAB RID: 15275 RVA: 0x00099388 File Offset: 0x00097588
		public unsafe void HashData(byte[] bytes, int offset, int size)
		{
			if (offset < 0 || size < 0 || size > bytes.Length || offset > bytes.Length - size)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (bytes.Length == 0)
			{
				return;
			}
			fixed (byte* ptr = &bytes[offset])
			{
				if (!CapiNativeMethods.CryptHashData(this, ptr, (uint)size, 0))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
			}
		}

		// Token: 0x06003BAC RID: 15276 RVA: 0x000993DC File Offset: 0x000975DC
		public byte[] HashFinal()
		{
			byte[] array = null;
			uint num = 0U;
			if (!CapiNativeMethods.CryptGetHashParam(this, CapiNativeMethods.HashParameter.HashValue, array, ref num, 0U))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			array = new byte[num];
			if (!CapiNativeMethods.CryptGetHashParam(this, CapiNativeMethods.HashParameter.HashValue, array, ref num, 0U))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return array;
		}

		// Token: 0x06003BAD RID: 15277 RVA: 0x00099426 File Offset: 0x00097626
		protected override bool ReleaseHandle()
		{
			return CapiNativeMethods.CryptDestroyHash(this.handle);
		}
	}
}
