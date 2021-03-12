using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000274 RID: 628
	internal class MailboxMergeSourceResource : MailboxMergeResource
	{
		// Token: 0x06001F54 RID: 8020 RVA: 0x00041A66 File Offset: 0x0003FC66
		private MailboxMergeSourceResource(Guid mailboxGuid) : base(mailboxGuid)
		{
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06001F55 RID: 8021 RVA: 0x00041A70 File Offset: 0x0003FC70
		public override int StaticCapacity
		{
			get
			{
				int config;
				using (base.ConfigContext.Activate())
				{
					config = ConfigBase<MRSConfigSchema>.GetConfig<int>("MaxActiveJobsPerSourceMailbox");
				}
				return config;
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06001F56 RID: 8022 RVA: 0x00041AB4 File Offset: 0x0003FCB4
		public override string ResourceType
		{
			get
			{
				return "MailboxMergeSource";
			}
		}

		// Token: 0x04000CA4 RID: 3236
		public static readonly ResourceCache<MailboxMergeSourceResource> Cache = new ResourceCache<MailboxMergeSourceResource>((Guid id) => new MailboxMergeSourceResource(id));
	}
}
