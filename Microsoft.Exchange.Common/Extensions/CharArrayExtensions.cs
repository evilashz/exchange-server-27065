using System;
using System.Security;

namespace Microsoft.Exchange.Extensions
{
	// Token: 0x02000068 RID: 104
	public static class CharArrayExtensions
	{
		// Token: 0x0600022D RID: 557 RVA: 0x00009D34 File Offset: 0x00007F34
		public unsafe static SecureString ConvertToSecureString(this char[] password)
		{
			if (password == null || password.Length == 0)
			{
				return new SecureString();
			}
			fixed (char* ptr = password)
			{
				SecureString secureString = new SecureString(ptr, password.Length);
				secureString.MakeReadOnly();
				return secureString;
			}
		}
	}
}
