using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientPermission
{
	// Token: 0x0200068F RID: 1679
	internal static class RecipientPermissionTaskHelper
	{
		// Token: 0x06003B73 RID: 15219 RVA: 0x000FD024 File Offset: 0x000FB224
		internal static string GetFriendlyNameOfSecurityIdentifier(SecurityIdentifier sid, IRecipientSession session, Task.TaskErrorLoggingDelegate errorLogger, Task.TaskVerboseLoggingDelegate verboseLogger)
		{
			if (!RecipientPermissionTaskHelper.sidToName.ContainsKey(sid))
			{
				ADRecipient adrecipient = (ADRecipient)SecurityPrincipalIdParameter.GetSecurityPrincipal(session, new SecurityPrincipalIdParameter(sid), errorLogger, verboseLogger);
				if (adrecipient != null)
				{
					if (adrecipient.Id != null)
					{
						RecipientPermissionTaskHelper.sidToName[sid] = adrecipient.Id.ToString();
					}
					else
					{
						RecipientPermissionTaskHelper.sidToName[sid] = SecurityPrincipalIdParameter.GetFriendlyUserName(sid, verboseLogger);
					}
				}
			}
			return RecipientPermissionTaskHelper.sidToName[sid];
		}

		// Token: 0x040026C2 RID: 9922
		private static Dictionary<SecurityIdentifier, string> sidToName = new Dictionary<SecurityIdentifier, string>();
	}
}
