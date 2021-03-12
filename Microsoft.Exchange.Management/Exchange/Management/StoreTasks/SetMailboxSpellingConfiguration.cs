using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007D0 RID: 2000
	[Cmdlet("Set", "MailboxSpellingConfiguration", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMailboxSpellingConfiguration : SetMailboxConfigurationTaskBase<MailboxSpellingConfiguration>
	{
		// Token: 0x1700152D RID: 5421
		// (get) Token: 0x0600461A RID: 17946 RVA: 0x0012002F File Offset: 0x0011E22F
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageMailboxSpellingConfiguration(this.Identity.ToString());
			}
		}
	}
}
