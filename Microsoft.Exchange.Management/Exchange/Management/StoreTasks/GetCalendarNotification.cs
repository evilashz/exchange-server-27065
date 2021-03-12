using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x02000799 RID: 1945
	[Cmdlet("Get", "CalendarNotification", DefaultParameterSetName = "Identity")]
	public sealed class GetCalendarNotification : GetXsoObjectWithIdentityTaskBase<CalendarNotification, ADUser>
	{
		// Token: 0x06004489 RID: 17545 RVA: 0x001190B8 File Offset: 0x001172B8
		internal override IConfigDataProvider CreateXsoMailboxDataProvider(ExchangePrincipal principal, ISecurityAccessToken userToken)
		{
			return new VersionedXmlDataProvider(principal, "Get-CalendarNotification");
		}
	}
}
