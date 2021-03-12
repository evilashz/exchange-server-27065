using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace Microsoft.Exchange.Extensions
{
	// Token: 0x0200001D RID: 29
	public static class StringExtensions
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00003D81 File Offset: 0x00001F81
		public static bool IsNullOrBlank(this string value)
		{
			return string.IsNullOrEmpty(value) || value.Trim().Length == 0;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003D9B File Offset: 0x00001F9B
		public static bool Contains(this string value, string search, StringComparison sc)
		{
			return value.IndexOf(search, sc) >= 0;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003DAB File Offset: 0x00001FAB
		public static string FormatWith(this string template, params object[] args)
		{
			return string.Format(template, args);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003DB4 File Offset: 0x00001FB4
		public unsafe static SecureString ConvertToSecureString(this string password)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			IntPtr intPtr2;
			IntPtr intPtr = intPtr2 = password;
			if (intPtr != 0)
			{
				intPtr2 = (IntPtr)((int)intPtr + RuntimeHelpers.OffsetToStringData);
			}
			char* value = intPtr2;
			SecureString secureString = new SecureString(value, password.Length);
			secureString.MakeReadOnly();
			return secureString;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003DF5 File Offset: 0x00001FF5
		public static SecureString AsSecureString(this string password)
		{
			if (password == null)
			{
				return null;
			}
			return password.ConvertToSecureString();
		}
	}
}
