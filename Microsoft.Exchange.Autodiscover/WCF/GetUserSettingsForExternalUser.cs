using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x0200007C RID: 124
	internal sealed class GetUserSettingsForExternalUser : GetUserSettingsCommandBase
	{
		// Token: 0x06000352 RID: 850 RVA: 0x000153C5 File Offset: 0x000135C5
		internal GetUserSettingsForExternalUser(ExternalIdentity callerExternalIdentity, CallContext callContext) : base(callContext)
		{
			this.callerExternalIdentity = callerExternalIdentity;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x000153D8 File Offset: 0x000135D8
		protected override IStandardBudget AcquireBudget()
		{
			return StandardBudget.AcquireFallback(this.callerExternalIdentity.EmailAddress.ToString(), BudgetType.Ews);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00015404 File Offset: 0x00013604
		protected override void AddToQueryList(UserResultMapping userResultMapping, IBudget budget)
		{
			FaultInjection.GenerateFault((FaultInjection.LIDs)2745576765U);
			OrganizationId organizationId;
			if (base.TryGetOrganizationId(userResultMapping, out organizationId))
			{
				base.AddToADQueryList(userResultMapping, organizationId, null, budget);
				return;
			}
			this.AddToMServeQueryList(userResultMapping);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00015438 File Offset: 0x00013638
		private void AddToMServeQueryList(UserResultMapping userResultMapping)
		{
			ExTraceGlobals.FrameworkTracer.TraceDebug<string>((long)this.GetHashCode(), "AddToMServeQueryList() called for '{0}'.", userResultMapping.Mailbox);
			if (this.mServeDomainQueryList == null)
			{
				this.mServeDomainQueryList = new MServeDomainQueryList();
				this.queryLists.Add(this.mServeDomainQueryList);
			}
			this.mServeDomainQueryList.Add(userResultMapping);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00015494 File Offset: 0x00013694
		protected override bool IsPostAdQueryAuthorized(UserResultMapping userResultMapping)
		{
			MServeQueryResult mserveQueryResult = userResultMapping.Result as MServeQueryResult;
			if (mserveQueryResult != null)
			{
				if (mserveQueryResult.RedirectServer == null)
				{
					ExTraceGlobals.FrameworkTracer.TraceDebug<string>((long)this.GetHashCode(), "MServe provided NO redirect for '{0}'", userResultMapping.Mailbox);
					return false;
				}
				ExTraceGlobals.FrameworkTracer.TraceDebug<string, string>((long)this.GetHashCode(), "MServe provided redirect for '{0}' to {1}.", userResultMapping.Mailbox, mserveQueryResult.RedirectServer);
				return true;
			}
			else
			{
				ADQueryResult adqueryResult = userResultMapping.Result as ADQueryResult;
				if (adqueryResult == null)
				{
					return false;
				}
				if (adqueryResult.Result.Data == null)
				{
					return false;
				}
				if (this.HasOrganizationRelationship(adqueryResult.Result.Data.OrganizationId))
				{
					ExTraceGlobals.FrameworkTracer.TraceDebug<string>((long)this.GetHashCode(), "Organization relationship for '{0}'.", userResultMapping.Mailbox);
					return true;
				}
				if (this.HasPersonalRelationship(adqueryResult.Result.Data as ADUser))
				{
					ExTraceGlobals.FrameworkTracer.TraceDebug<string>((long)this.GetHashCode(), "Personal relationship for '{0}'.", userResultMapping.Mailbox);
					return true;
				}
				ExTraceGlobals.FrameworkTracer.TraceDebug<string>((long)this.GetHashCode(), "No organization relationship for '{0}'.", userResultMapping.Mailbox);
				return false;
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x000155B0 File Offset: 0x000137B0
		private bool HasOrganizationRelationship(OrganizationId organizationId)
		{
			OrganizationIdCacheValue organizationIdCacheValue = OrganizationIdCache.Singleton.Get(organizationId);
			OrganizationRelationship organizationRelationship = organizationIdCacheValue.GetOrganizationRelationship(this.callerExternalIdentity.EmailAddress.Domain);
			return organizationRelationship != null && organizationRelationship.Enabled && organizationRelationship.DomainNames.Contains(new SmtpDomain(this.callerExternalIdentity.EmailAddress.Domain));
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0001561C File Offset: 0x0001381C
		private bool HasPersonalRelationship(ADUser adUser)
		{
			if (adUser == null)
			{
				return false;
			}
			SharingPartnerIdentityCollection sharingPartnerIdentities = adUser.SharingPartnerIdentities;
			return sharingPartnerIdentities != null && sharingPartnerIdentities.Contains(this.callerExternalIdentity.ExternalId.ToString());
		}

		// Token: 0x04000307 RID: 775
		private ExternalIdentity callerExternalIdentity;

		// Token: 0x04000308 RID: 776
		private MServeDomainQueryList mServeDomainQueryList;
	}
}
