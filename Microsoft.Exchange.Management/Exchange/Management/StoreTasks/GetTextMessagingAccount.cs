using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007B8 RID: 1976
	[Cmdlet("Get", "TextMessagingAccount", DefaultParameterSetName = "Identity")]
	public sealed class GetTextMessagingAccount : GetXsoObjectWithIdentityTaskBase<TextMessagingAccount, ADUser>
	{
		// Token: 0x06004583 RID: 17795 RVA: 0x0011D807 File Offset: 0x0011BA07
		internal override IConfigDataProvider CreateXsoMailboxDataProvider(ExchangePrincipal principal, ISecurityAccessToken userToken)
		{
			return new VersionedXmlDataProvider(principal, "Get-TextMessagingAccount");
		}
	}
}
