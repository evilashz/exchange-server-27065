using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007BB RID: 1979
	[Cmdlet("Get", "MailboxAutoReplyConfiguration")]
	public sealed class GetMailboxAutoReplyConfiguration : GetXsoObjectWithIdentityTaskBase<MailboxAutoReplyConfiguration, ADUser>
	{
		// Token: 0x06004592 RID: 17810 RVA: 0x0011DE76 File Offset: 0x0011C076
		internal override IConfigDataProvider CreateXsoMailboxDataProvider(ExchangePrincipal principal, ISecurityAccessToken userToken)
		{
			return new MailboxAutoReplyConfigurationDataProvider(principal, "Get-MailboxAutoReplyConfiguration");
		}
	}
}
