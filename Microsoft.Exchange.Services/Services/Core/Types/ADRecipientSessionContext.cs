using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006A8 RID: 1704
	internal class ADRecipientSessionContext
	{
		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x0600349D RID: 13469 RVA: 0x000BDDC9 File Offset: 0x000BBFC9
		// (set) Token: 0x0600349E RID: 13470 RVA: 0x000BDDD1 File Offset: 0x000BBFD1
		public OrganizationId OrganizationId { get; private set; }

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x0600349F RID: 13471 RVA: 0x000BDDDA File Offset: 0x000BBFDA
		public string OrganizationPrefix
		{
			get
			{
				if (this.OrganizationId.OrganizationalUnit != null)
				{
					return this.OrganizationId.OrganizationalUnit.Name;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x060034A0 RID: 13472 RVA: 0x000BDDFF File Offset: 0x000BBFFF
		public bool IsRootOrganization
		{
			get
			{
				return this.OrganizationId.Equals(OrganizationId.ForestWideOrgId);
			}
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x000BDE11 File Offset: 0x000BC011
		private ADRecipientSessionContext(OrganizationId organizationId, ADRecipientSessionContext.GetADRecipientSessionCallback getADRecipientSession, ADRecipientSessionContext.GetGALScopedADRecipientSessionCallback getGALScopedADRecipientSession)
		{
			this.getADRecipientSession = getADRecipientSession;
			this.getGALScopedADRecipientSession = getGALScopedADRecipientSession;
			this.OrganizationId = organizationId;
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x000BDE3D File Offset: 0x000BC03D
		public static ADRecipientSessionContext CreateForRootOrganization()
		{
			return new ADRecipientSessionContext(OrganizationId.ForestWideOrgId, new ADRecipientSessionContext.GetADRecipientSessionCallback(Directory.CreateRootADRecipientSession), (ClientSecurityContext c) => Directory.CreateGALScopedADRecipientSessionForOrganization(null, 0, OrganizationId.ForestWideOrgId, c));
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x000BDE72 File Offset: 0x000BC072
		public static ADRecipientSessionContext CreateForMachine()
		{
			return ADRecipientSessionContext.CreateForRootOrganization();
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x000BDEA0 File Offset: 0x000BC0A0
		public static ADRecipientSessionContext CreateForOrganization(OrganizationId orgId)
		{
			return new ADRecipientSessionContext(orgId, () => Directory.CreateADRecipientSessionForOrganization(null, orgId), (ClientSecurityContext c) => Directory.CreateGALScopedADRecipientSessionForOrganization(null, 0, orgId, c));
		}

		// Token: 0x060034A5 RID: 13477 RVA: 0x000BDEDD File Offset: 0x000BC0DD
		public static ADRecipientSessionContext CreateForPartner(OrganizationId targetPartnerOrg)
		{
			return ADRecipientSessionContext.CreateForOrganization(targetPartnerOrg);
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x000BDEE8 File Offset: 0x000BC0E8
		public static ADRecipientSessionContext CreateFromSidInRootOrg(SecurityIdentifier sid)
		{
			ADIdentityInformation adidentityInformation = null;
			ADRecipientSessionContext adrecipientSessionContext = ADRecipientSessionContext.CreateForRootOrganization();
			if (!ADIdentityInformationCache.Singleton.TryGet(sid, adrecipientSessionContext, out adidentityInformation))
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<SecurityIdentifier>(0L, "ADRecipientSessionContext.CreateFromSid. Sid {0} was not found.", sid);
				return adrecipientSessionContext;
			}
			if (adidentityInformation is ContactIdentity && EWSSettings.IsMultiTenancyEnabled)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<SecurityIdentifier>(0L, "ADRecipientSessionContext.CreateFromSid. CreateFromSid {0} is an ADContact, which isn't supported for datacenter topologies.", sid);
				throw new ADConfigurationException();
			}
			return new ADRecipientSessionContext(adidentityInformation.OrganizationId, new ADRecipientSessionContext.GetADRecipientSessionCallback(adidentityInformation.GetADRecipientSession), new ADRecipientSessionContext.GetGALScopedADRecipientSessionCallback(adidentityInformation.GetGALScopedADRecipientSession));
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x000BDF70 File Offset: 0x000BC170
		public static ADRecipientSessionContext CreateFromSmtpAddress(string smtpAddress)
		{
			ADRecipientSessionContext adRecipientSessionContext;
			if (EWSSettings.IsMultiTenancyEnabled)
			{
				ADSessionSettings adsessionSettings = Directory.SessionSettingsFromAddress(smtpAddress);
				adRecipientSessionContext = ADRecipientSessionContext.CreateForOrganization(adsessionSettings.CurrentOrganizationId);
			}
			else
			{
				adRecipientSessionContext = ADRecipientSessionContext.CreateForRootOrganization();
			}
			RecipientIdentity adIdentity = null;
			if (!ADIdentityInformationCache.Singleton.TryGetRecipientIdentity(smtpAddress, adRecipientSessionContext, out adIdentity))
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>(0L, "ADRecipientSessionContext.CreateFromSmtpAddress. Smtp address {0} was not found.", smtpAddress);
				throw new ADConfigurationException();
			}
			return ADRecipientSessionContext.CreateFromADIdentityInformation(adIdentity);
		}

		// Token: 0x060034A8 RID: 13480 RVA: 0x000BDFCF File Offset: 0x000BC1CF
		public static ADRecipientSessionContext CreateFromADIdentityInformation(ADIdentityInformation adIdentity)
		{
			return new ADRecipientSessionContext(adIdentity.OrganizationId, new ADRecipientSessionContext.GetADRecipientSessionCallback(adIdentity.GetADRecipientSession), new ADRecipientSessionContext.GetGALScopedADRecipientSessionCallback(adIdentity.GetGALScopedADRecipientSession));
		}

		// Token: 0x060034A9 RID: 13481 RVA: 0x000BE00C File Offset: 0x000BC20C
		public IRecipientSession GetADRecipientSession()
		{
			if (this.adRecipientSession == null)
			{
				ADSessionSettingsFactory.RunWithInactiveMailboxVisibilityEnablerForDatacenter(delegate
				{
					this.adRecipientSession = this.getADRecipientSession();
				});
			}
			return this.adRecipientSession;
		}

		// Token: 0x060034AA RID: 13482 RVA: 0x000BE03F File Offset: 0x000BC23F
		public void ExcludeInactiveMailboxInADRecipientSession()
		{
			if (this.adRecipientSession == null)
			{
				this.adRecipientSession = this.getADRecipientSession();
				return;
			}
			this.adRecipientSession.SessionSettings.IncludeSoftDeletedObjects = false;
			this.adRecipientSession.SessionSettings.IncludeInactiveMailbox = false;
		}

		// Token: 0x060034AB RID: 13483 RVA: 0x000BE07D File Offset: 0x000BC27D
		public IRecipientSession GetGALScopedADRecipientSession(ClientSecurityContext clientSecurityContext)
		{
			if (this.adRecipientSessionGALScoped == null)
			{
				this.adRecipientSessionGALScoped = this.getGALScopedADRecipientSession(clientSecurityContext);
			}
			return this.adRecipientSessionGALScoped;
		}

		// Token: 0x04001DAA RID: 7594
		private IRecipientSession adRecipientSession;

		// Token: 0x04001DAB RID: 7595
		private IRecipientSession adRecipientSessionGALScoped;

		// Token: 0x04001DAC RID: 7596
		private ADRecipientSessionContext.GetADRecipientSessionCallback getADRecipientSession;

		// Token: 0x04001DAD RID: 7597
		private ADRecipientSessionContext.GetGALScopedADRecipientSessionCallback getGALScopedADRecipientSession;

		// Token: 0x020006A9 RID: 1705
		// (Invoke) Token: 0x060034AF RID: 13487
		internal delegate IRecipientSession GetADRecipientSessionCallback();

		// Token: 0x020006AA RID: 1706
		// (Invoke) Token: 0x060034B3 RID: 13491
		internal delegate IRecipientSession GetGALScopedADRecipientSessionCallback(ClientSecurityContext clientSecurityContext);
	}
}
