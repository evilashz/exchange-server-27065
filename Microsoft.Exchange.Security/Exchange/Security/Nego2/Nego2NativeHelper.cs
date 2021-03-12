using System;
using System.Globalization;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Exchange.Security.Nego2
{
	// Token: 0x02000028 RID: 40
	internal static class Nego2NativeHelper
	{
		// Token: 0x06000100 RID: 256 RVA: 0x000093EC File Offset: 0x000075EC
		internal static NetworkCredential GetCredential(IntPtr ppAuthIdentity)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder();
			int num = Nego2NativeHelper.SspiEncodeAuthIdentityAsStrings(ppAuthIdentity, ref stringBuilder, ref stringBuilder2, ref stringBuilder3);
			if (num != 0)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "SspiEncodeAuthIdentityAsStrings failed with error {0}", new object[]
				{
					num
				}));
			}
			return new NetworkCredential(stringBuilder.ToString(), stringBuilder3.ToString(), stringBuilder2.ToString());
		}

		// Token: 0x06000101 RID: 257
		[DllImport("sspicli.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int SspiEncodeAuthIdentityAsStrings(IntPtr pAuthIdentity, ref StringBuilder pszUserName, ref StringBuilder pszDomainName, ref StringBuilder pszPackedCredentialsString);

		// Token: 0x06000102 RID: 258
		[DllImport("Nego2NativeInterface.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int CreateLiveClientAuthBufferWithPlainPassword([MarshalAs(UnmanagedType.LPWStr)] string wszUserName, [MarshalAs(UnmanagedType.LPWStr)] string wszPassword, uint dwFlags, bool bIsBusinessInstance, out IntPtr ppAuthBuffer, out IntPtr pwdAuthBufferLen);

		// Token: 0x06000103 RID: 259
		[DllImport("Nego2NativeInterface.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int FreeAuthBuffer(IntPtr ppAuthBuffer);

		// Token: 0x02000029 RID: 41
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CREDUI_INFO
		{
			// Token: 0x04000180 RID: 384
			public int cbSize;

			// Token: 0x04000181 RID: 385
			public IntPtr hwndParent;

			// Token: 0x04000182 RID: 386
			public string pszMessageText;

			// Token: 0x04000183 RID: 387
			public string pszCaptionText;

			// Token: 0x04000184 RID: 388
			public IntPtr hbmBanner;
		}
	}
}
