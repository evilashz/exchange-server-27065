using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000032 RID: 50
	public static class PSCredentialExtension
	{
		// Token: 0x06000213 RID: 531 RVA: 0x0000877A File Offset: 0x0000697A
		public static bool IsLiveId(this PSCredential cred)
		{
			if (cred == null)
			{
				throw new ArgumentNullException("cred");
			}
			return SmtpAddress.IsValidSmtpAddress(cred.UserName);
		}
	}
}
