using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000543 RID: 1347
	internal static class PSCredentialHelper
	{
		// Token: 0x06003C54 RID: 15444 RVA: 0x000E6928 File Offset: 0x000E4B28
		public static PSCredential GetCredentialFromUserPass(string userName, string password, bool passwordEncryptionEnabled)
		{
			if (passwordEncryptionEnabled)
			{
				throw new NotImplementedException("Password encryption not yet implemented");
			}
			if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
			{
				SecureString secureString = new SecureString();
				foreach (char c in password.ToCharArray())
				{
					secureString.AppendChar(c);
				}
				return new PSCredential(userName, secureString);
			}
			return null;
		}

		// Token: 0x06003C55 RID: 15445 RVA: 0x000E6984 File Offset: 0x000E4B84
		public static void GetUserPassFromCredential(PSCredential credential, out string userName, out string password, bool passwordEncryptionEnabled)
		{
			userName = null;
			password = null;
			if (passwordEncryptionEnabled)
			{
				throw new NotImplementedException("Password encryption not yet implemented");
			}
			if (credential == null)
			{
				return;
			}
			string text = string.Empty;
			if (credential.Password == null || credential.Password.Length == 0)
			{
				return;
			}
			text = credential.Password.ConvertToUnsecureString();
			userName = credential.UserName;
			password = text;
		}
	}
}
