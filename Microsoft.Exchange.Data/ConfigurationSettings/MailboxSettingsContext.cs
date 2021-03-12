using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ConfigurationSettings
{
	// Token: 0x02000203 RID: 515
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxSettingsContext : SettingsContextBase
	{
		// Token: 0x060011FA RID: 4602 RVA: 0x000361AE File Offset: 0x000343AE
		public MailboxSettingsContext(Guid mailboxGuid, SettingsContextBase nextContext = null) : base(nextContext)
		{
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x000361BE File Offset: 0x000343BE
		public override Guid? MailboxGuid
		{
			get
			{
				return new Guid?(this.mailboxGuid);
			}
		}

		// Token: 0x04000ADA RID: 2778
		private readonly Guid mailboxGuid;
	}
}
