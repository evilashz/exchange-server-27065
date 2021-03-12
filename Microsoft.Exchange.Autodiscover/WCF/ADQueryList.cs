using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000076 RID: 118
	internal sealed class ADQueryList : QueryListBase<ADQueryResult>
	{
		// Token: 0x06000319 RID: 793 RVA: 0x0001420E File Offset: 0x0001240E
		internal ADQueryList(OrganizationId organizationId, ADObjectId searchRoot, IBudget budget)
		{
			ExTraceGlobals.FrameworkTracer.TraceDebug<OrganizationId, ADObjectId>((long)this.GetHashCode(), "Constructing ADQueryList for organizationId {0} searchRoot '{1}'.", organizationId, searchRoot);
			this.organizationId = organizationId;
			this.searchRoot = searchRoot;
			this.budget = budget;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00014243 File Offset: 0x00012443
		protected override ADQueryResult CreateResult(UserResultMapping userResultMapping)
		{
			return new ADQueryResult(userResultMapping);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x000142A0 File Offset: 0x000124A0
		public override void Execute()
		{
			ExTraceGlobals.FrameworkTracer.TraceDebug<ADQueryList, int>((long)this.GetHashCode(), "{0} Execute() called for {1} addresses.", this, this.resultDictionary.Values.Count);
			this.budget.CheckOverBudget();
			IRecipientSession adRecipientSession = this.GetRecipientSessionForOrganization(this.searchRoot, this.organizationId);
			Result<ADRecipient>[] adRecipientQueryResults = null;
			Guid[] archiveGuid;
			if (this.TryCreateExchangeGuidArray(out archiveGuid))
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.Current.TrackLatency(ServiceLatencyMetadata.RequestedUserADLatency, delegate()
				{
					adRecipientQueryResults = this.FindByExchangeGuidsIncludingArchive(adRecipientSession, archiveGuid);
				});
			}
			else
			{
				SmtpProxyAddress[] smtpProxyAddresses = this.CreateSmtpProxyAddressArray();
				RequestDetailsLoggerBase<RequestDetailsLogger>.Current.TrackLatency(ServiceLatencyMetadata.RequestedUserADLatency, delegate()
				{
					adRecipientQueryResults = adRecipientSession.FindByProxyAddresses(smtpProxyAddresses);
				});
			}
			this.SetQueryResults(adRecipientQueryResults);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00014384 File Offset: 0x00012584
		private bool TryCreateExchangeGuidArray(out Guid[] exchangeGuid)
		{
			bool result = true;
			exchangeGuid = null;
			int num = 0;
			foreach (ADQueryResult adqueryResult in this.resultDictionary.Values)
			{
				string smtpAddress = adqueryResult.UserResultMapping.SmtpProxyAddress.SmtpAddress;
				Guid guid;
				if (!SmtpProxyAddress.TryDeencapsulateExchangeGuid(smtpAddress, out guid))
				{
					result = false;
					exchangeGuid = null;
					break;
				}
				if (exchangeGuid == null)
				{
					exchangeGuid = new Guid[this.resultDictionary.Count];
				}
				exchangeGuid[num] = guid;
				num++;
			}
			return result;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0001442C File Offset: 0x0001262C
		private SmtpProxyAddress[] CreateSmtpProxyAddressArray()
		{
			SmtpProxyAddress[] array = new SmtpProxyAddress[this.resultDictionary.Count];
			int num = 0;
			foreach (ADQueryResult adqueryResult in this.resultDictionary.Values)
			{
				array[num] = adqueryResult.UserResultMapping.SmtpProxyAddress;
				num++;
			}
			return array;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000144A4 File Offset: 0x000126A4
		private Result<ADRecipient>[] FindByExchangeGuidsIncludingArchive(IRecipientSession adRecipientSession, Guid[] archiveGuids)
		{
			Result<ADRecipient>[] array = new Result<ADRecipient>[archiveGuids.Length];
			for (int i = 0; i < archiveGuids.Length; i++)
			{
				Result<ADRecipient>[] array2 = adRecipientSession.FindByExchangeGuidsIncludingArchive(new Guid[]
				{
					archiveGuids[i]
				});
				if (array2.Length > 0)
				{
					array[i] = array2[0];
				}
				else
				{
					array[i] = new Result<ADRecipient>(null, ProviderError.NotFound);
				}
			}
			return array;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00014528 File Offset: 0x00012728
		private void SetQueryResults(Result<ADRecipient>[] adRecipientQueryResults)
		{
			int num = 0;
			foreach (ADQueryResult adqueryResult in this.resultDictionary.Values)
			{
				adqueryResult.Result = adRecipientQueryResults[num];
				num++;
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00014594 File Offset: 0x00012794
		public override string ToString()
		{
			return string.Format(base.ToString() + "-OrganizationId:{0}-SearchRoot:{1}", this.organizationId, (this.searchRoot != null) ? this.searchRoot.ToString() : "<Null>");
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000145CC File Offset: 0x000127CC
		private IRecipientSession GetRecipientSessionForOrganization(ADObjectId searchRoot, OrganizationId organizationId)
		{
			ExTraceGlobals.FrameworkTracer.TraceDebug<OrganizationId, ADObjectId>((long)this.GetHashCode(), "GetRecipientSessionForOrganization() called for organizationId {0} searchRoot '{1}'.", organizationId, searchRoot);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, searchRoot, CultureInfo.CurrentCulture.LCID, true, ConsistencyMode.IgnoreInvalid, null, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 268, "GetRecipientSessionForOrganization", "f:\\15.00.1497\\sources\\dev\\autodisc\\src\\WCF\\Commands\\ADQueryList.cs");
			tenantOrRootOrgRecipientSession.ServerTimeout = new TimeSpan?(ADQueryList.RecipientSessionServerTimeout);
			return tenantOrRootOrgRecipientSession;
		}

		// Token: 0x040002EE RID: 750
		internal static TimeSpan RecipientSessionServerTimeout = TimeSpan.FromSeconds(30.0);

		// Token: 0x040002EF RID: 751
		private OrganizationId organizationId;

		// Token: 0x040002F0 RID: 752
		private ADObjectId searchRoot;

		// Token: 0x040002F1 RID: 753
		private IBudget budget;
	}
}
