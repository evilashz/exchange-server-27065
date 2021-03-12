using System;
using System.Net;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.Routing;
using Microsoft.Exchange.HttpProxy.Routing.RoutingKeys;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200000C RID: 12
	internal class ExternalDirectoryObjectIdAnchorMailbox : UserBasedAnchorMailbox
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00003A3C File Offset: 0x00001C3C
		public ExternalDirectoryObjectIdAnchorMailbox(string externalDirectoryObjectId, OrganizationId organizationId, IRequestContext requestContext) : base(AnchorSource.ExternalDirectoryObjectId, externalDirectoryObjectId, requestContext)
		{
			this.externalDirectoryObjectId = externalDirectoryObjectId;
			this.organizationId = organizationId;
			base.NotFoundExceptionCreator = delegate()
			{
				string message = string.Format("Cannot find mailbox by ExternalDirectoryObjectId={0} in organizationId={1}.", this.externalDirectoryObjectId, this.organizationId);
				return new HttpProxyException(HttpStatusCode.NotFound, HttpProxySubErrorCode.MailboxExternalDirectoryObjectIdNotFound, message);
			};
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003A9C File Offset: 0x00001C9C
		protected override ADRawEntry LoadADRawEntry()
		{
			IRecipientSession recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.organizationId), 62, "LoadADRawEntry", "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\AnchorMailbox\\ExternalDirectoryObjectIdAnchorMailbox.cs");
			ADRawEntry ret = DirectoryHelper.InvokeAccountForest(base.RequestContext.LatencyTracker, () => recipientSession.FindADUserByExternalDirectoryObjectId(this.externalDirectoryObjectId));
			return base.CheckForNullAndThrowIfApplicable<ADRawEntry>(ret);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003B03 File Offset: 0x00001D03
		protected override IRoutingKey GetRoutingKey()
		{
			return new ExternalDirectoryObjectIdRoutingKey(Guid.Parse(this.externalDirectoryObjectId), Guid.Parse(this.organizationId.ToExternalDirectoryOrganizationId()));
		}

		// Token: 0x04000028 RID: 40
		private readonly string externalDirectoryObjectId;

		// Token: 0x04000029 RID: 41
		private readonly OrganizationId organizationId;
	}
}
