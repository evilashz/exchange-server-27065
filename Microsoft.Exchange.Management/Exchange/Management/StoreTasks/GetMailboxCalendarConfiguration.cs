using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007C2 RID: 1986
	[Cmdlet("Get", "MailboxCalendarConfiguration")]
	public sealed class GetMailboxCalendarConfiguration : GetMailboxConfigurationTaskBase<MailboxCalendarConfiguration>
	{
	}
}
