using System;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009C5 RID: 2501
	internal class LiveIdInstanceQueryProcessor : RbacQuery.RbacQueryProcessor
	{
		// Token: 0x060046D8 RID: 18136 RVA: 0x000FC17E File Offset: 0x000FA37E
		internal LiveIdInstanceQueryProcessor(LiveIdInstanceType liveIdInstanceType)
		{
			this.liveIdInstanceType = liveIdInstanceType;
		}

		// Token: 0x060046D9 RID: 18137 RVA: 0x000FC190 File Offset: 0x000FA390
		public override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
		{
			SmtpAddress smtpAddress;
			if (rbacConfiguration.TryGetExecutingWindowsLiveId(out smtpAddress) && smtpAddress.IsValidAddress)
			{
				DomainCacheValue domainCacheValue = DomainCache.Singleton.Get(new SmtpDomainWithSubdomains(smtpAddress.Domain), rbacConfiguration.ExecutingUserOrganizationId);
				return new bool?(domainCacheValue != null && domainCacheValue.LiveIdInstanceType == this.liveIdInstanceType);
			}
			return null;
		}

		// Token: 0x040028B2 RID: 10418
		internal const string BusinessLiveIdRoleName = "BusinessLiveId";

		// Token: 0x040028B3 RID: 10419
		internal const string ConsumerLiveIdRoleName = "ConsumerLiveId";

		// Token: 0x040028B4 RID: 10420
		private readonly LiveIdInstanceType liveIdInstanceType;
	}
}
