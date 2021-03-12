using System;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000B5 RID: 181
	internal sealed class SharedContext : MailboxContextBase
	{
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x00015C4D File Offset: 0x00013E4D
		public override AuthZClientInfo CallerClientInfo
		{
			get
			{
				return CallContextUtilities.GetCallerClientInfo();
			}
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00015C54 File Offset: 0x00013E54
		internal SharedContext(UserContextKey key, string userAgent) : base(key, userAgent)
		{
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00015C5E File Offset: 0x00013E5E
		public override void ValidateLogonPermissionIfNecessary()
		{
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00015C60 File Offset: 0x00013E60
		protected override MailboxSession CreateMailboxSession()
		{
			if (base.ExchangePrincipal == null)
			{
				throw new OwaInvalidOperationException("SharedContext::CreateMailboxSession cannot open mailbox session when ExchangePrincipal is null");
			}
			MailboxSession result;
			try
			{
				MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(base.ExchangePrincipal, CultureInfo.InvariantCulture, "Client=OWA");
				if (mailboxSession == null)
				{
					throw new OwaInvalidOperationException("SharedContext::CreateMailboxSession cannot create a mailbox session");
				}
				result = mailboxSession;
			}
			catch (AccessDeniedException innerException)
			{
				throw new OwaExplicitLogonException("user has no access rights to the mailbox", "errorexplicitlogonaccessdenied", innerException);
			}
			return result;
		}
	}
}
