using System;
using System.Security;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x0200011B RID: 283
	public static class MailboxTaskUtilities
	{
		// Token: 0x060009A6 RID: 2470 RVA: 0x0002AC2E File Offset: 0x00028E2E
		public static SecureString GetRandomPassword(string name, string samAccountName)
		{
			return MailboxTaskUtilities.GetRandomPassword(name, samAccountName, 128);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0002AC3C File Offset: 0x00028E3C
		public static SecureString GetRandomPassword(string name, string samAccountName, int length)
		{
			string randomPassword = PasswordHelper.GetRandomPassword(name, samAccountName, length);
			return randomPassword.ConvertToSecureString();
		}
	}
}
