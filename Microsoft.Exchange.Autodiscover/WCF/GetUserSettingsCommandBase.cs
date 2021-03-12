using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x0200007B RID: 123
	internal abstract class GetUserSettingsCommandBase
	{
		// Token: 0x06000349 RID: 841 RVA: 0x00014FF4 File Offset: 0x000131F4
		internal GetUserSettingsCommandBase(CallContext callContext)
		{
			this.response = callContext.Response;
			this.userResultMappingList = new List<UserResultMapping>(callContext.Users.Count);
			foreach (User user in callContext.Users)
			{
				UserResultMapping item = new UserResultMapping(user.Mailbox, callContext);
				this.userResultMappingList.Add(item);
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0001507C File Offset: 0x0001327C
		internal void Execute()
		{
			ExTraceGlobals.FrameworkTracer.TraceDebug<GetUserSettingsCommandBase>((long)this.GetHashCode(), "{0} Execute() called.", this);
			this.queryLists = new List<IQueryList>(this.userResultMappingList.Count);
			this.adQueryListDictionary = new Dictionary<OrganizationId, ADQueryList>(this.userResultMappingList.Count);
			using (IStandardBudget standardBudget = this.AcquireBudget())
			{
				HttpContext.Current.Items["StartBudget"] = standardBudget.ToString();
				GetUserSettingsCommandBase.InitializeBudget(standardBudget);
				foreach (UserResultMapping userResultMapping in this.userResultMappingList)
				{
					if (userResultMapping.IsValidSmtpAddress)
					{
						this.AddToQueryList(userResultMapping, standardBudget);
					}
					else
					{
						ExTraceGlobals.FrameworkTracer.TraceDebug<string>((long)this.GetHashCode(), "Mailbox not valid smtp address for '{0}'.", userResultMapping.Mailbox);
						this.SetInvalidSmtpAddressResult(userResultMapping);
					}
				}
				foreach (IQueryList queryList in this.queryLists)
				{
					queryList.Execute();
				}
				foreach (UserResultMapping userResultMapping2 in this.userResultMappingList)
				{
					if (!(userResultMapping2.Result is InvalidSmtpAddressResult) && !this.IsPostAdQueryAuthorized(userResultMapping2))
					{
						ExTraceGlobals.FrameworkTracer.TraceDebug<string>((long)this.GetHashCode(), "Mailbox not valid smtp address for '{0}' because caller was not authorized to perform query.", userResultMapping2.Mailbox);
						this.SetInvalidSmtpAddressResult(userResultMapping2);
					}
					this.response.UserResponses.Add(userResultMapping2.Result.CreateResponse(standardBudget));
				}
				HttpContext.Current.Items["EndBudget"] = standardBudget.ToString();
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x000152A0 File Offset: 0x000134A0
		protected virtual bool IsPostAdQueryAuthorized(UserResultMapping userResultMapping)
		{
			return true;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000152A3 File Offset: 0x000134A3
		protected void SetInvalidSmtpAddressResult(UserResultMapping userResultMapping)
		{
			ExTraceGlobals.FrameworkTracer.TraceDebug<string>((long)this.GetHashCode(), "SetInvalidSmtpAddressResult() called for '{0}'.", userResultMapping.Mailbox);
			userResultMapping.Result = new InvalidSmtpAddressResult(userResultMapping);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x000152D0 File Offset: 0x000134D0
		protected bool TryGetOrganizationId(UserResultMapping userResultMapping, out OrganizationId organizationId)
		{
			organizationId = DomainToOrganizationIdCache.Singleton.Get(new SmtpDomain(userResultMapping.SmtpAddress.Domain));
			bool flag = organizationId == OrganizationId.ForestWideOrgId || (organizationId != null && ADAccountPartitionLocator.IsKnownPartition(organizationId.PartitionId));
			if (!flag)
			{
				organizationId = null;
			}
			return flag;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00015330 File Offset: 0x00013530
		protected void AddToADQueryList(UserResultMapping userResultMapping, OrganizationId organizationId, ADObjectId searchRoot, IBudget budget)
		{
			ExTraceGlobals.FrameworkTracer.TraceDebug<string>((long)this.GetHashCode(), "AddToADQueryList() called for '{0}'.", userResultMapping.Mailbox);
			ADQueryList adqueryList;
			if (!this.adQueryListDictionary.TryGetValue(organizationId, out adqueryList))
			{
				adqueryList = new ADQueryList(organizationId, searchRoot, budget);
				this.adQueryListDictionary.Add(organizationId, adqueryList);
				this.queryLists.Add(adqueryList);
			}
			adqueryList.Add(userResultMapping);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00015394 File Offset: 0x00013594
		private static void InitializeBudget(IStandardBudget budget)
		{
			string callerInfo = "GetUserSettingsCommandBase.InitializeBudget";
			budget.CheckOverBudget();
			budget.StartConnection(callerInfo);
			budget.StartLocal(callerInfo, default(TimeSpan));
		}

		// Token: 0x06000350 RID: 848
		protected abstract IStandardBudget AcquireBudget();

		// Token: 0x06000351 RID: 849
		protected abstract void AddToQueryList(UserResultMapping userResultMapping, IBudget budget);

		// Token: 0x04000303 RID: 771
		protected GetUserSettingsResponse response;

		// Token: 0x04000304 RID: 772
		protected List<IQueryList> queryLists;

		// Token: 0x04000305 RID: 773
		protected Dictionary<OrganizationId, ADQueryList> adQueryListDictionary;

		// Token: 0x04000306 RID: 774
		private List<UserResultMapping> userResultMappingList;
	}
}
