using System;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000012 RID: 18
	internal class PstProviderAnchorMailbox : UserBasedAnchorMailbox
	{
		// Token: 0x06000085 RID: 133 RVA: 0x000042EA File Offset: 0x000024EA
		public PstProviderAnchorMailbox(string pstFilePath, IRequestContext requestContext) : base(AnchorSource.GenericAnchorHint, pstFilePath, requestContext)
		{
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004304 File Offset: 0x00002504
		protected override ADRawEntry LoadADRawEntry()
		{
			ADRawEntry ret = DirectoryHelper.InvokeAccountForest(base.RequestContext.LatencyTracker, () => HttpProxyBackEndHelper.GetOrganizationMailboxInClosestSite(OrganizationId.ForestWideOrgId, OrganizationCapability.PstProvider));
			return base.CheckForNullAndThrowIfApplicable<ADRawEntry>(ret);
		}
	}
}
