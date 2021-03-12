using System;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200000D RID: 13
	internal class DomainAnchorMailbox : UserBasedAnchorMailbox
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00003B25 File Offset: 0x00001D25
		public DomainAnchorMailbox(string domain, IRequestContext requestContext) : this(AnchorSource.Domain, domain, requestContext)
		{
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003B30 File Offset: 0x00001D30
		protected DomainAnchorMailbox(AnchorSource anchorSource, object sourceObject, IRequestContext requestContext) : base(AnchorSource.Domain, sourceObject, requestContext)
		{
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003B3B File Offset: 0x00001D3B
		public virtual string Domain
		{
			get
			{
				return (string)base.SourceObject;
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003B48 File Offset: 0x00001D48
		public override string GetOrganizationNameForLogging()
		{
			string organizationNameForLogging = base.GetOrganizationNameForLogging();
			if (string.IsNullOrEmpty(organizationNameForLogging))
			{
				return this.Domain;
			}
			return organizationNameForLogging;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003B8C File Offset: 0x00001D8C
		protected override ADRawEntry LoadADRawEntry()
		{
			IRecipientSession session = this.GetDomainRecipientSession();
			ADRawEntry ret = DirectoryHelper.InvokeAccountForest(base.RequestContext.LatencyTracker, () => HttpProxyBackEndHelper.GetDefaultOrganizationMailbox(session, this.ToCookieKey()));
			return base.CheckForNullAndThrowIfApplicable<ADRawEntry>(ret);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003BD8 File Offset: 0x00001DD8
		protected IRecipientSession GetDomainRecipientSession()
		{
			IRecipientSession result = null;
			try
			{
				Guid externalOrgId = new Guid(this.Domain);
				result = DirectoryHelper.GetRecipientSessionFromExternalDirectoryOrganizationId(base.RequestContext.LatencyTracker, externalOrgId);
			}
			catch (FormatException)
			{
				result = DirectoryHelper.GetRecipientSessionFromDomain(base.RequestContext.LatencyTracker, this.Domain, false);
			}
			return result;
		}
	}
}
