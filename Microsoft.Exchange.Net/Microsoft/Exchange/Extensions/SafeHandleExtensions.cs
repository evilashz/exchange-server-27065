using System;
using System.Security;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Extensions
{
	// Token: 0x02000B3F RID: 2879
	internal static class SafeHandleExtensions
	{
		// Token: 0x06003E0C RID: 15884 RVA: 0x000A22B5 File Offset: 0x000A04B5
		public static SafeSecureHGlobalHandle ConvertToUnsecureHGlobal(this SecureString securePassword)
		{
			if (securePassword == null)
			{
				throw new ArgumentNullException("securePassword");
			}
			return SafeSecureHGlobalHandle.DecryptAndAllocHGlobal(securePassword);
		}
	}
}
