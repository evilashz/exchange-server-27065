using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000275 RID: 629
	internal class MailboxMergeTargetResource : MailboxMergeResource
	{
		// Token: 0x06001F59 RID: 8025 RVA: 0x00041AEC File Offset: 0x0003FCEC
		private MailboxMergeTargetResource(Guid mailboxGuid) : base(mailboxGuid)
		{
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06001F5A RID: 8026 RVA: 0x00041AF8 File Offset: 0x0003FCF8
		public override int StaticCapacity
		{
			get
			{
				int config;
				using (base.ConfigContext.Activate())
				{
					config = ConfigBase<MRSConfigSchema>.GetConfig<int>("MaxActiveJobsPerTargetMailbox");
				}
				return config;
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06001F5B RID: 8027 RVA: 0x00041B3C File Offset: 0x0003FD3C
		public override string ResourceType
		{
			get
			{
				return "MailboxMergeTarget";
			}
		}

		// Token: 0x04000CA6 RID: 3238
		public static readonly ResourceCache<MailboxMergeTargetResource> Cache = new ResourceCache<MailboxMergeTargetResource>((Guid id) => new MailboxMergeTargetResource(id));
	}
}
