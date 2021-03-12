using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007B8 RID: 1976
	internal static class ExtensionMethods
	{
		// Token: 0x06002850 RID: 10320 RVA: 0x00055B9C File Offset: 0x00053D9C
		public static ScenarioException GetScenarioException(this Exception e)
		{
			for (Exception ex = e; ex != null; ex = ex.InnerException)
			{
				if (ex is ScenarioException)
				{
					return ex as ScenarioException;
				}
			}
			return null;
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x00055BC8 File Offset: 0x00053DC8
		public static byte[] ConvertToByteArray(this SecureString secureString)
		{
			IntPtr intPtr = IntPtr.Zero;
			byte[] result;
			try
			{
				int num = secureString.Length * 2;
				byte[] array = new byte[num];
				intPtr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
				Marshal.Copy(intPtr, array, 0, num);
				result = array;
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeGlobalAllocAnsi(intPtr);
				}
			}
			return result;
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x00055C24 File Offset: 0x00053E24
		public static string ConvertToUnsecureString(this SecureString secureString)
		{
			if (secureString == null)
			{
				throw new ArgumentNullException("secureString");
			}
			IntPtr intPtr = IntPtr.Zero;
			string result;
			try
			{
				intPtr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
				result = Marshal.PtrToStringUni(intPtr);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
				}
			}
			return result;
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x00055C7C File Offset: 0x00053E7C
		public unsafe static SecureString ConvertToSecureString(this string stringToConvert)
		{
			if (stringToConvert == null)
			{
				throw new ArgumentNullException("stringToConvert");
			}
			IntPtr intPtr2;
			IntPtr intPtr = intPtr2 = stringToConvert;
			if (intPtr != 0)
			{
				intPtr2 = (IntPtr)((int)intPtr + RuntimeHelpers.OffsetToStringData);
			}
			char* value = intPtr2;
			SecureString secureString = new SecureString(value, stringToConvert.Length);
			secureString.MakeReadOnly();
			return secureString;
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x00055CC0 File Offset: 0x00053EC0
		public static void Shuffle<T>(this IList<T> list)
		{
			Random random = new Random();
			for (int i = list.Count - 1; i > 0; i--)
			{
				int index = random.Next(i + 1);
				T value = list[i];
				list[i] = list[index];
				list[index] = value;
			}
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x00055D10 File Offset: 0x00053F10
		public static string GetCanonicalHostAddress(this Uri uri)
		{
			IPAddress ipaddress;
			if (IPAddress.TryParse(uri.DnsSafeHost, out ipaddress))
			{
				return ipaddress.ToString();
			}
			return uri.DnsSafeHost;
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x00055D3C File Offset: 0x00053F3C
		public static bool ContainsMatchingSuffix(this IEnumerable<string> list, string searchString)
		{
			foreach (string value in list)
			{
				if (searchString.EndsWith(value, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x00055D90 File Offset: 0x00053F90
		public static bool ContainsMatchingSubstring(this IEnumerable<string> list, string searchString)
		{
			foreach (string value in list)
			{
				if (searchString.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}
