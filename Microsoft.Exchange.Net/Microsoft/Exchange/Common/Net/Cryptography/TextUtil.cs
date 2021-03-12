using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Microsoft.Exchange.Common.Net.Cryptography
{
	// Token: 0x020006BB RID: 1723
	public class TextUtil
	{
		// Token: 0x06002010 RID: 8208 RVA: 0x0003E228 File Offset: 0x0003C428
		public static string Truncate(string str, Encoding encoding, int maxByteCount, bool appendDotDotDot)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (appendDotDotDot)
			{
				maxByteCount -= TextUtil.dotDotDot.Length;
			}
			if (maxByteCount <= 0)
			{
				return string.Empty;
			}
			bool flag = false;
			if (str.Length > maxByteCount)
			{
				str = str.Substring(0, maxByteCount);
				flag = true;
			}
			while (encoding.GetByteCount(str) > maxByteCount)
			{
				str = str.Remove(str.Length - 1, 1);
				flag = true;
			}
			if (flag && appendDotDotDot)
			{
				return str + TextUtil.dotDotDot;
			}
			return str;
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x0003E2B8 File Offset: 0x0003C4B8
		public static string ConvertToUnsecureString(SecureString secureString)
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
				Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
			}
			return result;
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x0003E304 File Offset: 0x0003C504
		public static SecureString ConvertToSecureString(string unsecureString)
		{
			SecureString secureString = new SecureString();
			foreach (char c in unsecureString.ToCharArray())
			{
				secureString.AppendChar(c);
			}
			SecureString result;
			try
			{
				secureString.MakeReadOnly();
				result = secureString;
			}
			finally
			{
				unsecureString = null;
			}
			return result;
		}

		// Token: 0x04001EFC RID: 7932
		private static string dotDotDot = "...";
	}
}
