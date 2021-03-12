using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007CF RID: 1999
	[Cmdlet("Get", "MailboxSpellingConfiguration")]
	public sealed class GetMailboxSpellingConfiguration : GetMailboxConfigurationTaskBase<MailboxSpellingConfiguration>
	{
	}
}
