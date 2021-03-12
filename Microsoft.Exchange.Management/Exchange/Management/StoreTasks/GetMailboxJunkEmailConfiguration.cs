using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007CD RID: 1997
	[Cmdlet("Get", "MailboxJunkEmailConfiguration")]
	public sealed class GetMailboxJunkEmailConfiguration : GetXsoObjectWithIdentityTaskBase<MailboxJunkEmailConfiguration, ADUser>
	{
		// Token: 0x06004613 RID: 17939 RVA: 0x0011FFB3 File Offset: 0x0011E1B3
		internal override IConfigDataProvider CreateXsoMailboxDataProvider(ExchangePrincipal principal, ISecurityAccessToken userToken)
		{
			return new MailboxJunkEmailConfigurationDataProvider(principal, base.TenantGlobalCatalogSession, "Get-MailboxJunkEmailConfiguration");
		}
	}
}
