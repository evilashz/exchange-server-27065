using System;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Extensions
{
	// Token: 0x0200006A RID: 106
	public static class SecureStringExtensions
	{
		// Token: 0x06000232 RID: 562 RVA: 0x00009D7C File Offset: 0x00007F7C
		public static string ConvertToUnsecureString(this SecureString securePassword)
		{
			if (securePassword == null)
			{
				throw new ArgumentNullException("securePassword");
			}
			IntPtr intPtr = IntPtr.Zero;
			string result;
			try
			{
				intPtr = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
				result = Marshal.PtrToStringUni(intPtr);
			}
			finally
			{
				Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
			}
			return result;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00009DC8 File Offset: 0x00007FC8
		public static SecureArray<char> ConvertToSecureCharArray(this SecureString securePassword)
		{
			if (securePassword == null)
			{
				throw new ArgumentNullException("securePassword");
			}
			SecureArray<char> secureArray = new SecureArray<char>(securePassword.Length);
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.SecureStringToBSTR(securePassword);
				Marshal.Copy(intPtr, secureArray.ArrayValue, 0, securePassword.Length);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeBSTR(intPtr);
				}
			}
			return secureArray;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00009E38 File Offset: 0x00008038
		public static string AsUnsecureString(this SecureString securePassword)
		{
			if (securePassword == null)
			{
				return null;
			}
			return securePassword.ConvertToUnsecureString();
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00009E48 File Offset: 0x00008048
		public static SecureArray<char> TransformToSecureCharArray(this SecureString securePassword, CharTransformDelegate transform)
		{
			if (securePassword == null)
			{
				throw new ArgumentNullException("securePassword");
			}
			if (transform == null)
			{
				throw new ArgumentNullException("transform");
			}
			SecureArray<char> secureArray = null;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				secureArray = securePassword.ConvertToSecureCharArray();
				disposeGuard.Add<SecureArray<char>>(secureArray);
				int num = 0;
				bool flag = false;
				foreach (char c in secureArray.ArrayValue)
				{
					char[] array = transform(c);
					num += ((array == null) ? 1 : array.Length);
					flag |= (array != null);
				}
				if (flag)
				{
					using (SecureArray<char> secureArray2 = secureArray)
					{
						secureArray = new SecureArray<char>(num);
						disposeGuard.Add<SecureArray<char>>(secureArray);
						int num2 = 0;
						foreach (char c2 in secureArray2.ArrayValue)
						{
							char[] array2 = transform(c2);
							if (array2 == null)
							{
								secureArray.ArrayValue[num2] = c2;
								num2++;
							}
							else
							{
								array2.CopyTo(secureArray.ArrayValue, num2);
								num2 += array2.Length;
							}
						}
					}
				}
				disposeGuard.Success();
			}
			return secureArray;
		}
	}
}
