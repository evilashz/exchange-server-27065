using System;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000383 RID: 899
	internal class LiveIdInstanceQueryProcessor : RbacQuery.RbacQueryProcessor
	{
		// Token: 0x06003061 RID: 12385 RVA: 0x0009374F File Offset: 0x0009194F
		internal LiveIdInstanceQueryProcessor(Func<DomainCacheValue, bool> predicate)
		{
			this.predicate = predicate;
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x00093760 File Offset: 0x00091960
		public override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
		{
			SmtpAddress smtpAddress = new SmtpAddress(HttpContextExtensions.CurrentUserLiveID());
			if (smtpAddress.IsValidAddress)
			{
				DomainCacheValue domainCacheValue = DomainCache.Singleton.Get(new SmtpDomainWithSubdomains(smtpAddress.Domain), RbacPrincipal.Current.RbacConfiguration.ExecutingUserOrganizationId);
				return new bool?(domainCacheValue != null && this.predicate(domainCacheValue));
			}
			return new bool?(false);
		}

		// Token: 0x04002363 RID: 9059
		internal const string BusinessLiveIdRoleName = "BusinessLiveId";

		// Token: 0x04002364 RID: 9060
		internal const string ConsumerLiveIdRoleName = "ConsumerLiveId";

		// Token: 0x04002365 RID: 9061
		internal static readonly RbacQuery.RbacQueryProcessor BusinessLiveId = new LiveIdInstanceQueryProcessor((DomainCacheValue x) => x.LiveIdInstanceType == LiveIdInstanceType.Business);

		// Token: 0x04002366 RID: 9062
		internal static readonly RbacQuery.RbacQueryProcessor ConsumerLiveId = new LiveIdInstanceQueryProcessor((DomainCacheValue x) => x.LiveIdInstanceType == LiveIdInstanceType.Consumer);

		// Token: 0x04002367 RID: 9063
		private Func<DomainCacheValue, bool> predicate;
	}
}
