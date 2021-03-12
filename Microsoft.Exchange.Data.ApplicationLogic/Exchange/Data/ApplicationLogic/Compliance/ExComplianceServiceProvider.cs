using System;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy;
using Microsoft.Office.CompliancePolicy.ComplianceData;
using Microsoft.Office.CompliancePolicy.ComplianceTask;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Exchange.Data.ApplicationLogic.Compliance
{
	// Token: 0x020000C6 RID: 198
	internal class ExComplianceServiceProvider : ComplianceServiceProvider
	{
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00021C86 File Offset: 0x0001FE86
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x00021C8E File Offset: 0x0001FE8E
		public string PreferredDomainController { get; set; }

		// Token: 0x0600085A RID: 2138 RVA: 0x00021C97 File Offset: 0x0001FE97
		internal ExComplianceServiceProvider()
		{
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00021C9F File Offset: 0x0001FE9F
		internal ExComplianceServiceProvider(string preferredDomainController, ExecutionLog logger)
		{
			this.PreferredDomainController = preferredDomainController;
			this.logger = logger;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00021CB5 File Offset: 0x0001FEB5
		public override Auditor GetAuditor()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00021CBC File Offset: 0x0001FEBC
		public override ExecutionLog GetExecutionLog()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00021CC3 File Offset: 0x0001FEC3
		public override ComplianceItemPagedReader GetPagedReader(ComplianceItemContainer container)
		{
			return ExComplianceServiceProvider.GetExComplianceContainer(container).ComplianceItemPagedReader;
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00021CD0 File Offset: 0x0001FED0
		public override PolicyConfigProvider GetPolicyStore(ComplianceItemContainer rootContainer)
		{
			MailboxSession session = ExComplianceServiceProvider.GetExComplianceContainer(rootContainer).Session;
			OrganizationId organizationId = session.OrganizationId;
			return this.GetPolicyStore(organizationId);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00021CF8 File Offset: 0x0001FEF8
		public override PolicyConfigProvider GetPolicyStore(string tenantId)
		{
			if (tenantId == null)
			{
				throw new ArgumentNullException("tenantId");
			}
			OrganizationId organizationId;
			if (!OrganizationId.TryCreateFromBytes(Convert.FromBase64String(tenantId), Encoding.UTF8, out organizationId))
			{
				throw new ArgumentException("Cannot create OrganizationId from such tenantId: " + tenantId);
			}
			return this.GetPolicyStore(organizationId);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00021D40 File Offset: 0x0001FF40
		public PolicyConfigProvider GetPolicyStore(OrganizationId organizationId)
		{
			if (organizationId == null)
			{
				throw new ArgumentNullException("organizationId");
			}
			ExPolicyConfigProvider exPolicyConfigProvider = (ExPolicyConfigProvider)PolicyConfigProviderManager<ExPolicyConfigProviderManager>.Instance.CreateForProcessingEngine(organizationId, this.logger, this.PreferredDomainController);
			this.PreferredDomainController = exPolicyConfigProvider.LastUsedDc;
			return exPolicyConfigProvider;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00021D8B File Offset: 0x0001FF8B
		public override RuleParser GetRuleParser()
		{
			if (this.ruleParser == null)
			{
				this.ruleParser = new RuleParser(new SimplePolicyParserFactory());
			}
			return this.ruleParser;
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00021DAC File Offset: 0x0001FFAC
		public override ComplianceItemContainer GetComplianceItemContainer(string tenantId, string scope)
		{
			if (tenantId == null)
			{
				throw new ArgumentNullException("Either this.session or tenantId should be not null");
			}
			OrganizationId scopingOrganizationId;
			if (!OrganizationId.TryCreateFromBytes(Convert.FromBase64String(tenantId), Encoding.UTF8, out scopingOrganizationId))
			{
				throw new ArgumentException("Cannot create OrganizationId from such tenantId: " + tenantId);
			}
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(scopingOrganizationId), 169, "GetComplianceItemContainer", "f:\\15.00.1497\\sources\\dev\\data\\src\\ApplicationLogic\\Compliance\\ExComplianceServiceProvider.cs");
			return new ExMailboxComplianceItemContainer(tenantOrRootOrgRecipientSession, scope);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00021E18 File Offset: 0x00020018
		private static ExComplianceItemContainer GetExComplianceContainer(ComplianceItemContainer container)
		{
			ExComplianceItemContainer exComplianceItemContainer = container as ExComplianceItemContainer;
			if (exComplianceItemContainer != null)
			{
				return exComplianceItemContainer;
			}
			throw new ArgumentException("Operation can be invoked only with an ExComplianceContainer");
		}

		// Token: 0x040003B7 RID: 951
		private readonly ExecutionLog logger;

		// Token: 0x040003B8 RID: 952
		private RuleParser ruleParser;
	}
}
