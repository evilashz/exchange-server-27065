using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02000914 RID: 2324
	internal class GlobalPrereqTask : SessionTask
	{
		// Token: 0x06005286 RID: 21126 RVA: 0x00153770 File Offset: 0x00151970
		public GlobalPrereqTask() : base(HybridStrings.GlobalPrereqTaskName, 1)
		{
		}

		// Token: 0x06005287 RID: 21127 RVA: 0x00153790 File Offset: 0x00151990
		public override bool CheckPrereqs(ITaskContext taskContext)
		{
			if (!base.CheckPrereqs(taskContext))
			{
				return false;
			}
			if (taskContext.HybridConfigurationObject.ExchangeVersion.ExchangeBuild.Major != taskContext.HybridConfigurationObject.MaximumSupportedExchangeObjectVersion.ExchangeBuild.Major)
			{
				base.Logger.LogInformation(HybridStrings.HybridInfoTaskLogTemplate(base.Name, HybridStrings.ErrorHybridMustBeUpgraded));
				base.AddLocalizedStringError(HybridStrings.ErrorHybridMustBeUpgraded);
				return false;
			}
			if (Configuration.RequiresIntraOrganizationConnector(taskContext.HybridConfigurationObject.ServiceInstance) && Configuration.RestrictIOCToSP1OrGreater(taskContext.HybridConfigurationObject.ServiceInstance) && !(taskContext.OnPremisesSession.GetIntraOrganizationConfiguration().DeploymentIsCompleteIOCReady ?? false))
			{
				base.Logger.LogInformation(HybridStrings.HybridInfoTaskLogTemplate(base.Name, HybridStrings.ErrorIncompatibleServersDetected));
				base.AddLocalizedStringError(HybridStrings.ErrorIncompatibleServersDetected);
				return false;
			}
			if (taskContext.HybridConfigurationObject.Domains == null || taskContext.HybridConfigurationObject.Domains.Count == 0)
			{
				base.Logger.LogInformation(HybridStrings.HybridInfoTaskLogTemplate(base.Name, HybridStrings.ErrorNoHybridDomains));
				base.AddLocalizedStringError(HybridStrings.ErrorNoHybridDomains);
				return false;
			}
			IOrderedEnumerable<string> orderedEnumerable = from d in taskContext.HybridConfigurationObject.Domains
			select d.Domain into d
			orderby d
			select d;
			taskContext.Parameters.Set<IEnumerable<string>>("_hybridDomainList", orderedEnumerable);
			IEnumerable<IAcceptedDomain> acceptedDomain = base.TenantSession.GetAcceptedDomain();
			if (acceptedDomain.Count<IAcceptedDomain>() == 0)
			{
				base.Logger.LogInformation(HybridStrings.HybridInfoTaskLogTemplate(base.Name, HybridStrings.ErrorNoTenantAcceptedDomains));
				base.AddLocalizedStringError(HybridStrings.ErrorNoTenantAcceptedDomains);
				return false;
			}
			taskContext.Parameters.Set<IEnumerable<IAcceptedDomain>>("_tenantAcceptedDomains", acceptedDomain);
			string text = null;
			foreach (IAcceptedDomain acceptedDomain2 in acceptedDomain)
			{
				if (acceptedDomain2.IsCoexistenceDomain)
				{
					text = acceptedDomain2.DomainNameDomain;
					break;
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				base.Logger.LogInformation(HybridStrings.HybridInfoTaskLogTemplate(base.Name, HybridStrings.ErrorNoHybridDomain));
				base.AddLocalizedStringError(HybridStrings.ErrorNoHybridDomain);
				return false;
			}
			taskContext.Parameters.Set<string>("_hybridDomain", text);
			IEnumerable<IAcceptedDomain> acceptedDomain3 = base.OnPremisesSession.GetAcceptedDomain();
			if (acceptedDomain3.Count<IAcceptedDomain>() == 0)
			{
				base.Logger.LogInformation(HybridStrings.HybridInfoTaskLogTemplate(base.Name, HybridStrings.ErrorNoOnPremAcceptedDomains));
				base.AddLocalizedStringError(HybridStrings.ErrorNoOnPremAcceptedDomains);
				return false;
			}
			taskContext.Parameters.Set<IEnumerable<IAcceptedDomain>>("_onPremAcceptedDomains", acceptedDomain3);
			foreach (string domain in orderedEnumerable)
			{
				if (!GlobalPrereqTask.IsAcceptedDomain(domain, acceptedDomain))
				{
					base.Logger.LogInformation(HybridStrings.HybridInfoTaskLogTemplate(base.Name, HybridStrings.ErrorHybridDomainNotAcceptedOnTenant(domain)));
					base.AddLocalizedStringError(HybridStrings.ErrorHybridDomainNotAcceptedOnTenant(domain));
					return false;
				}
				if (!GlobalPrereqTask.IsAcceptedDomain(domain, acceptedDomain3))
				{
					base.Logger.LogInformation(HybridStrings.HybridInfoTaskLogTemplate(base.Name, HybridStrings.ErrorHybridDomainNotAcceptedOnPrem(domain)));
					base.AddLocalizedStringError(HybridStrings.ErrorHybridDomainNotAcceptedOnPrem(domain));
					return false;
				}
			}
			if (!GlobalPrereqTask.IsAcceptedDomain(text, acceptedDomain))
			{
				base.Logger.LogInformation(HybridStrings.HybridInfoTaskLogTemplate(base.Name, HybridStrings.ErrorCoexistenceDomainNotAcceptedOnTenant(text)));
				base.AddLocalizedStringError(HybridStrings.ErrorCoexistenceDomainNotAcceptedOnTenant(text));
				return false;
			}
			IOrganizationConfig organizationConfig = base.OnPremisesSession.GetOrganizationConfig();
			List<string> list = new List<string>();
			list.Add(text);
			OrganizationRelationship organizationRelationship = TaskCommon.GetOrganizationRelationship(taskContext.OnPremisesSession, TaskCommon.GetOnPremOrgRelationshipName(organizationConfig), list);
			taskContext.Parameters.Set<OrganizationRelationship>("_onPremOrgRel", organizationRelationship);
			organizationRelationship = TaskCommon.GetOrganizationRelationship(taskContext.TenantSession, TaskCommon.GetTenantOrgRelationshipName(organizationConfig), orderedEnumerable);
			taskContext.Parameters.Set<OrganizationRelationship>("_tenantOrgRel", organizationRelationship);
			foreach (ADObjectId adobjectId in taskContext.HybridConfigurationObject.ClientAccessServers)
			{
				IExchangeServer exchangeServer = base.OnPremisesSession.GetExchangeServer(adobjectId.Name);
				if (!this.HasCASRole(exchangeServer))
				{
					base.Logger.LogInformation(HybridStrings.HybridInfoTaskLogTemplate(base.Name, HybridStrings.ErrorCASRoleInvalid(exchangeServer.Name)));
					base.AddLocalizedStringError(HybridStrings.ErrorCASRoleInvalid(exchangeServer.Name));
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005288 RID: 21128 RVA: 0x00153CB8 File Offset: 0x00151EB8
		private static bool IsAcceptedDomain(string domain, IEnumerable<IAcceptedDomain> acceptedDomains)
		{
			return acceptedDomains.Any((IAcceptedDomain a) => a.DomainNameDomain.Equals(domain, StringComparison.InvariantCultureIgnoreCase));
		}

		// Token: 0x06005289 RID: 21129 RVA: 0x00153CE4 File Offset: 0x00151EE4
		private bool HasCASRole(IExchangeServer exchangeServer)
		{
			return exchangeServer.ServerRole.ToString().Contains("ClientAccess");
		}

		// Token: 0x04002FF4 RID: 12276
		private const string CASRole = "ClientAccess";

		// Token: 0x04002FF5 RID: 12277
		private const string isCoexistenceDomainKey = "IsCoexistenceDomain";
	}
}
