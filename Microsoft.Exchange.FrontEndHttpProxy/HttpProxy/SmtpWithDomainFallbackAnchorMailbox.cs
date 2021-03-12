using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.HttpProxy.Common;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000018 RID: 24
	internal class SmtpWithDomainFallbackAnchorMailbox : SmtpAnchorMailbox
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00004D92 File Offset: 0x00002F92
		public SmtpWithDomainFallbackAnchorMailbox(string smtp, IRequestContext requestContext) : base(smtp, requestContext)
		{
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004DE8 File Offset: 0x00002FE8
		protected override ADRawEntry LoadADRawEntry()
		{
			IRecipientSession session = DirectoryHelper.GetRecipientSessionFromSmtpOrLiveId(base.RequestContext.LatencyTracker, base.Smtp, false);
			ADRawEntry adrawEntry = DirectoryHelper.InvokeAccountForest(base.RequestContext.LatencyTracker, () => session.FindByProxyAddress(new SmtpProxyAddress(this.Smtp, true), this.PropertySet));
			if (adrawEntry == null)
			{
				PerfCounters.HttpProxyCountersInstance.RedirectByTenantMailboxCount.Increment();
				adrawEntry = DirectoryHelper.InvokeAccountForest(base.RequestContext.LatencyTracker, () => HttpProxyBackEndHelper.GetDefaultOrganizationMailbox(session, this.ToCookieKey()));
			}
			else
			{
				PerfCounters.HttpProxyCountersInstance.RedirectBySenderMailboxCount.Increment();
			}
			return base.CheckForNullAndThrowIfApplicable<ADRawEntry>(adrawEntry);
		}
	}
}
