using System;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000011 RID: 17
	internal class OrganizationAnchorMailbox : UserBasedAnchorMailbox
	{
		// Token: 0x0600007E RID: 126 RVA: 0x000041D8 File Offset: 0x000023D8
		public OrganizationAnchorMailbox(OrganizationId orgId, IRequestContext requestContext) : base(AnchorSource.OrganizationId, orgId, requestContext)
		{
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000041E3 File Offset: 0x000023E3
		public OrganizationId OrganizationId
		{
			get
			{
				return (OrganizationId)base.SourceObject;
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000041F0 File Offset: 0x000023F0
		public override string ToString()
		{
			return string.Format("{0}~{1}", base.AnchorSource, this.ToCookieKey());
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004210 File Offset: 0x00002410
		public override string ToCookieKey()
		{
			string arg = string.Empty;
			if (this.OrganizationId.ConfigurationUnit != null)
			{
				arg = this.OrganizationId.ConfigurationUnit.Parent.Name;
			}
			return string.Format("{0}@{1}", OrganizationAnchorMailbox.OrganizationAnchor, arg);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004256 File Offset: 0x00002456
		public override string GetOrganizationNameForLogging()
		{
			return this.OrganizationId.GetFriendlyName();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004284 File Offset: 0x00002484
		protected override ADRawEntry LoadADRawEntry()
		{
			IRecipientSession session = DirectoryHelper.GetRecipientSessionFromOrganizationId(base.RequestContext.LatencyTracker, this.OrganizationId);
			ADRawEntry ret = DirectoryHelper.InvokeAccountForest(base.RequestContext.LatencyTracker, () => HttpProxyBackEndHelper.GetDefaultOrganizationMailbox(session, this.ToCookieKey()));
			return base.CheckForNullAndThrowIfApplicable<ADRawEntry>(ret);
		}

		// Token: 0x0400002F RID: 47
		private static readonly string OrganizationAnchor = "OrganizationAnchor";
	}
}
