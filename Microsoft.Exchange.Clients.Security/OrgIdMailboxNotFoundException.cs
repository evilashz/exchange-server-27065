using System;
using Microsoft.Exchange.Clients.Common;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x02000018 RID: 24
	public class OrgIdMailboxNotFoundException : OrgIdLogonException
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000032CF File Offset: 0x000014CF
		protected override string ErrorMessageFormatString
		{
			get
			{
				return Strings.MailboxNotFoundErrorMessage;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000032D6 File Offset: 0x000014D6
		public override Strings.IDs ErrorMessageStringId
		{
			get
			{
				return 1753500428;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000032E0 File Offset: 0x000014E0
		public virtual ErrorMode? ErrorMode
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000032F6 File Offset: 0x000014F6
		public OrgIdMailboxNotFoundException(string userName, string logoutUrl) : base(userName, logoutUrl)
		{
		}
	}
}
