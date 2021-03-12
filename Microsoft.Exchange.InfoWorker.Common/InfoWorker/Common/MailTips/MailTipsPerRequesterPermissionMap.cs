using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.InfoWorker.Common.Availability;

namespace Microsoft.Exchange.InfoWorker.Common.MailTips
{
	// Token: 0x02000122 RID: 290
	internal sealed class MailTipsPerRequesterPermissionMap
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x000243DF File Offset: 0x000225DF
		// (set) Token: 0x06000800 RID: 2048 RVA: 0x000243E7 File Offset: 0x000225E7
		internal ExternalClientContext ExternalClientContext { get; private set; }

		// Token: 0x06000801 RID: 2049 RVA: 0x000243F0 File Offset: 0x000225F0
		internal MailTipsPerRequesterPermissionMap(ClientContext clientContext, int recipientCount, Trace tracer, int traceId)
		{
			this.ExternalClientContext = (clientContext as ExternalClientContext);
			if (this.ExternalClientContext != null)
			{
				this.permissionMap = new Dictionary<OrganizationId, MailTipsPermission>(recipientCount);
			}
			this.tracer = tracer;
			this.traceId = traceId;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00024428 File Offset: 0x00022628
		internal MailTipsPermission Lookup(RecipientData recipientData)
		{
			if (this.ExternalClientContext == null)
			{
				this.tracer.TraceDebug<object, EmailAddress>((long)this.traceId, "{0}: InternalMailTipsPermission used for {1} because requester is not external", TraceContext.Get(), recipientData.EmailAddress);
				return MailTipsPermission.AllAccess;
			}
			if (recipientData.IsEmpty)
			{
				this.tracer.TraceDebug<object, EmailAddress>((long)this.traceId, "{0}: InternalMailTipsPermission used for {1} because recipient did not resolve in AD", TraceContext.Get(), recipientData.EmailAddress);
				return MailTipsPermission.AllAccess;
			}
			MailTipsPermission mailTipsPermission;
			if (this.permissionMap.TryGetValue(recipientData.OrganizationId, out mailTipsPermission))
			{
				return mailTipsPermission;
			}
			OrganizationRelationship organizationRelationship = FreeBusyPermission.GetOrganizationRelationship(recipientData.OrganizationId, this.ExternalClientContext.EmailAddress.Domain);
			if (organizationRelationship == null || !organizationRelationship.Enabled)
			{
				this.tracer.TraceDebug<object, OrganizationId, string>((long)this.traceId, "{0}: No organization relationship found in organization {1} for domain {2}", TraceContext.Get(), recipientData.OrganizationId, this.ExternalClientContext.EmailAddress.Domain);
				return MailTipsPermission.NoAccess;
			}
			bool requesterInAccessScope = false;
			if (organizationRelationship.MailTipsAccessScope == null)
			{
				requesterInAccessScope = true;
			}
			else if (organizationRelationship.MailTipsAccessEnabled && organizationRelationship.MailTipsAccessLevel != MailTipsAccessLevel.None)
			{
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(recipientData.OrganizationId), 127, "Lookup", "f:\\15.00.1497\\sources\\dev\\infoworker\\src\\common\\MailTips\\MailTipsPerRequesterPermissionMap.cs");
				ADGroup adgroup = tenantOrRootOrgRecipientSession.Read(organizationRelationship.MailTipsAccessScope) as ADGroup;
				if (adgroup == null)
				{
					this.tracer.TraceError<object, OrganizationId, ADObjectId>((long)this.traceId, "{0}: OrganizationRelationship for organization {1} has invalid MailTipsAccessScope {2} which cannot be resolved in ad as an ADGroup", TraceContext.Get(), recipientData.OrganizationId, organizationRelationship.MailTipsAccessScope);
				}
				else if (adgroup.ContainsMember(recipientData.Id, false))
				{
					this.tracer.TraceDebug((long)this.traceId, "{0}: {1} is a member of MailTipsAccessScope {2} for OrganizationRelationship of organization {3}", new object[]
					{
						TraceContext.Get(),
						recipientData.EmailAddress,
						organizationRelationship.MailTipsAccessScope,
						recipientData.OrganizationId
					});
					requesterInAccessScope = true;
				}
				else
				{
					this.tracer.TraceDebug((long)this.traceId, "{0}: {1} is not a member of MailTipsAccessScope {2} for OrganizationRelationship of organization {3}", new object[]
					{
						TraceContext.Get(),
						recipientData.EmailAddress,
						organizationRelationship.MailTipsAccessScope,
						recipientData.OrganizationId
					});
				}
			}
			mailTipsPermission = new MailTipsPermission(organizationRelationship.MailTipsAccessEnabled, organizationRelationship.MailTipsAccessLevel, requesterInAccessScope);
			this.permissionMap[recipientData.OrganizationId] = mailTipsPermission;
			return mailTipsPermission;
		}

		// Token: 0x040004BF RID: 1215
		private Dictionary<OrganizationId, MailTipsPermission> permissionMap;

		// Token: 0x040004C0 RID: 1216
		private Trace tracer;

		// Token: 0x040004C1 RID: 1217
		private int traceId;
	}
}
