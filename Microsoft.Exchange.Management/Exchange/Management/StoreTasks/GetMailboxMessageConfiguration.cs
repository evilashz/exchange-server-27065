using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007C9 RID: 1993
	[Cmdlet("Get", "MailboxMessageConfiguration")]
	public sealed class GetMailboxMessageConfiguration : GetXsoObjectWithIdentityTaskBase<MailboxMessageConfiguration, ADUser>
	{
		// Token: 0x060045FE RID: 17918 RVA: 0x0011FC33 File Offset: 0x0011DE33
		internal override IConfigDataProvider CreateXsoMailboxDataProvider(ExchangePrincipal principal, ISecurityAccessToken userToken)
		{
			return new XsoDictionaryDataProvider(principal, "Get-MailboxMessageConfiguration");
		}
	}
}
