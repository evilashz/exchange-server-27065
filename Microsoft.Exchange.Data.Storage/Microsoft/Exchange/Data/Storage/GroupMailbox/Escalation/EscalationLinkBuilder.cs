using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.GroupMailbox.Escalation
{
	// Token: 0x02000803 RID: 2051
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class EscalationLinkBuilder
	{
		// Token: 0x06004C84 RID: 19588 RVA: 0x0013CCC6 File Offset: 0x0013AEC6
		public EscalationLinkBuilder(IExchangePrincipal groupExchangePrincipal, IMailboxUrls mailboxUrls)
		{
			Util.ThrowOnNullArgument(groupExchangePrincipal, "groupExchangePrincipal");
			Util.ThrowOnNullArgument(mailboxUrls, "mailboxUrls");
			this.groupExchangePrincipal = groupExchangePrincipal;
			this.mailboxUrls = mailboxUrls;
		}

		// Token: 0x06004C85 RID: 19589 RVA: 0x0013CCF4 File Offset: 0x0013AEF4
		public virtual string GetEscalationLink(EscalationLinkType linkType)
		{
			string owaUrl = this.GetOwaUrl();
			if (owaUrl == null)
			{
				return null;
			}
			string domain = this.groupExchangePrincipal.MailboxInfo.PrimarySmtpAddress.Domain;
			string text = (linkType == EscalationLinkType.Subscribe) ? "subscribe" : "unsubscribe";
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}/groupsubscription.ashx?realm={2}&action={3}&exsvurl=1", new object[]
			{
				owaUrl,
				this.groupExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString(),
				domain,
				text
			});
		}

		// Token: 0x06004C86 RID: 19590 RVA: 0x0013CD84 File Offset: 0x0013AF84
		private string GetOwaUrl()
		{
			string text = this.mailboxUrls.OwaUrl;
			if (text == null)
			{
				throw new ServerNotFoundException("Unable to find OWA Service URL.");
			}
			if (!text.EndsWith("/"))
			{
				text += "/";
			}
			return text;
		}

		// Token: 0x040029BA RID: 10682
		public const string SubscribeActionName = "subscribe";

		// Token: 0x040029BB RID: 10683
		public const string UnsubscribeActionName = "unsubscribe";

		// Token: 0x040029BC RID: 10684
		private readonly IExchangePrincipal groupExchangePrincipal;

		// Token: 0x040029BD RID: 10685
		private IMailboxUrls mailboxUrls;
	}
}
