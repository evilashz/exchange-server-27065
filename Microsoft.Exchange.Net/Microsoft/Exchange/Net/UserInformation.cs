using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000CA0 RID: 3232
	internal static class UserInformation
	{
		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x06004739 RID: 18233 RVA: 0x000BFE6A File Offset: 0x000BE06A
		public static string UserPrincipalName
		{
			get
			{
				return UserInformation.GetUserNameEx(NativeMethods.ExtendedNameFormat.UserPrincipal);
			}
		}

		// Token: 0x0600473A RID: 18234 RVA: 0x000BFE74 File Offset: 0x000BE074
		private static string GetUserNameEx(NativeMethods.ExtendedNameFormat type)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			int capacity = stringBuilder.Capacity;
			if (!NativeMethods.GetUserNameEx(type, stringBuilder, ref capacity))
			{
				int hrforLastWin32Error = Marshal.GetHRForLastWin32Error();
				if (234 == hrforLastWin32Error)
				{
					stringBuilder.Capacity = capacity + 1;
					if (!NativeMethods.GetUserNameEx(NativeMethods.ExtendedNameFormat.UserPrincipal, stringBuilder, ref capacity))
					{
						hrforLastWin32Error = Marshal.GetHRForLastWin32Error();
					}
				}
				if (hrforLastWin32Error != 0)
				{
					Marshal.ThrowExceptionForHR(hrforLastWin32Error);
				}
			}
			return stringBuilder.ToString().Trim();
		}

		// Token: 0x02000CA1 RID: 3233
		internal static class WindowsErrorCode
		{
			// Token: 0x04003C47 RID: 15431
			internal const int SUCCESS = 0;

			// Token: 0x04003C48 RID: 15432
			internal const int MOREDATA = 234;
		}
	}
}
