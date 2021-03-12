using System;
using Microsoft.Exchange.Data.ConfigurationSettings;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000272 RID: 626
	internal abstract class MailboxResource : ResourceBase
	{
		// Token: 0x06001F4F RID: 8015 RVA: 0x00041A08 File Offset: 0x0003FC08
		public MailboxResource(Guid mailboxGuid)
		{
			this.MailboxGuid = mailboxGuid;
			base.ConfigContext = new MailboxSettingsContext(mailboxGuid, base.ConfigContext);
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06001F50 RID: 8016 RVA: 0x00041A29 File Offset: 0x0003FC29
		// (set) Token: 0x06001F51 RID: 8017 RVA: 0x00041A31 File Offset: 0x0003FC31
		public Guid MailboxGuid { get; private set; }

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06001F52 RID: 8018 RVA: 0x00041A3C File Offset: 0x0003FC3C
		public override string ResourceName
		{
			get
			{
				return this.MailboxGuid.ToString();
			}
		}
	}
}
