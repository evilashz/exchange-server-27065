using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ClientAccessRules;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000881 RID: 2177
	[Cmdlet("Test", "ClientAccessRule", SupportsShouldProcess = true)]
	public sealed class TestClientAccessRule : GetMultitenancySingletonSystemConfigurationObjectTask<ADClientAccessRule>
	{
		// Token: 0x1700166C RID: 5740
		// (get) Token: 0x06004B7A RID: 19322 RVA: 0x00138573 File Offset: 0x00136773
		// (set) Token: 0x06004B7B RID: 19323 RVA: 0x0013857B File Offset: 0x0013677B
		[Parameter(Mandatory = true)]
		public MailboxIdParameter User { get; set; }

		// Token: 0x1700166D RID: 5741
		// (get) Token: 0x06004B7C RID: 19324 RVA: 0x00138584 File Offset: 0x00136784
		// (set) Token: 0x06004B7D RID: 19325 RVA: 0x0013858C File Offset: 0x0013678C
		[Parameter(Mandatory = true)]
		public ClientAccessProtocol Protocol { get; set; }

		// Token: 0x1700166E RID: 5742
		// (get) Token: 0x06004B7E RID: 19326 RVA: 0x00138595 File Offset: 0x00136795
		// (set) Token: 0x06004B7F RID: 19327 RVA: 0x0013859D File Offset: 0x0013679D
		[Parameter(Mandatory = true)]
		public IPAddress RemoteAddress { get; set; }

		// Token: 0x1700166F RID: 5743
		// (get) Token: 0x06004B80 RID: 19328 RVA: 0x001385A6 File Offset: 0x001367A6
		// (set) Token: 0x06004B81 RID: 19329 RVA: 0x001385AE File Offset: 0x001367AE
		[Parameter(Mandatory = true)]
		public int RemotePort { get; set; }

		// Token: 0x17001670 RID: 5744
		// (get) Token: 0x06004B82 RID: 19330 RVA: 0x001385B7 File Offset: 0x001367B7
		// (set) Token: 0x06004B83 RID: 19331 RVA: 0x001385BF File Offset: 0x001367BF
		[Parameter(Mandatory = true)]
		public ClientAccessAuthenticationMethod AuthenticationType { get; set; }

		// Token: 0x06004B84 RID: 19332 RVA: 0x001385C8 File Offset: 0x001367C8
		protected override void InternalProcessRecord()
		{
			this.RunClientAccessRules();
		}

		// Token: 0x06004B85 RID: 19333 RVA: 0x0013861C File Offset: 0x0013681C
		private void RunClientAccessRules()
		{
			long ticks = DateTime.UtcNow.Ticks;
			ClientAccessRuleCollection clientAccessRuleCollection = this.FetchClientAccessRulesCollection();
			ADRawEntry adrawEntry = this.FetchADRawEntry(this.User);
			string usernameFromADRawEntry = ClientAccessRulesUtils.GetUsernameFromADRawEntry(adrawEntry);
			base.WriteVerbose(RulesTasksStrings.TestClientAccessRuleFoundUsername(usernameFromADRawEntry));
			ClientAccessRulesEvaluationContext context = new ClientAccessRulesEvaluationContext(clientAccessRuleCollection, usernameFromADRawEntry, new IPEndPoint(this.RemoteAddress, this.RemotePort), this.Protocol, this.AuthenticationType, adrawEntry, ObjectSchema.GetInstance<ClientAccessRulesRecipientFilterSchema>(), delegate(ClientAccessRulesEvaluationContext evaluationContext)
			{
			}, delegate(Rule rule, ClientAccessRulesAction action)
			{
				ObjectId identity = null;
				ClientAccessRule clientAccessRule = rule as ClientAccessRule;
				if (clientAccessRule != null)
				{
					identity = clientAccessRule.Identity;
				}
				this.WriteResult(new ClientAccessRulesEvaluationResult
				{
					Identity = identity,
					Name = rule.Name,
					Action = action
				});
			}, ticks);
			clientAccessRuleCollection.Run(context);
		}

		// Token: 0x06004B86 RID: 19334 RVA: 0x001386C0 File Offset: 0x001368C0
		private ADRawEntry FetchADRawEntry(MailboxIdParameter user)
		{
			OrganizationId organizationId = ((IConfigurationSession)base.DataSession).GetOrgContainer().OrganizationId;
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 105, "FetchADRawEntry", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\ClientAccessRules\\TestClientAccessRule.cs");
			tenantOrRootOrgRecipientSession.UseGlobalCatalog = true;
			List<ADUser> list = new List<ADUser>(base.GetDataObjects<ADUser>(user, tenantOrRootOrgRecipientSession, null));
			if (list.Count != 1)
			{
				base.WriteError(new RecipientTaskException(RulesTasksStrings.TestClientAccessRuleUserNotFoundOrMoreThanOne(user.ToString())), ErrorCategory.InvalidArgument, null);
			}
			return list[0];
		}

		// Token: 0x06004B87 RID: 19335 RVA: 0x00138740 File Offset: 0x00136940
		private ClientAccessRuleCollection FetchClientAccessRulesCollection()
		{
			ClientAccessRuleCollection clientAccessRuleCollection = new ClientAccessRuleCollection((base.Identity == null) ? OrganizationId.ForestWideOrgId.ToString() : base.Identity.ToString());
			OrganizationId organizationId = ((IConfigurationSession)base.DataSession).GetOrgContainer().OrganizationId;
			if (organizationId != null && !OrganizationId.ForestWideOrgId.Equals(organizationId))
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), OrganizationId.ForestWideOrgId, OrganizationId.ForestWideOrgId, false), 133, "FetchClientAccessRulesCollection", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\ClientAccessRules\\TestClientAccessRule.cs");
				clientAccessRuleCollection.AddClientAccessRuleCollection(this.FetchClientAccessRulesCollection(tenantOrTopologyConfigurationSession));
			}
			clientAccessRuleCollection.AddClientAccessRuleCollection(this.FetchClientAccessRulesCollection((IConfigurationSession)base.DataSession));
			return clientAccessRuleCollection;
		}

		// Token: 0x06004B88 RID: 19336 RVA: 0x001387F4 File Offset: 0x001369F4
		private ClientAccessRuleCollection FetchClientAccessRulesCollection(IConfigurationSession session)
		{
			ClientAccessRulesPriorityManager clientAccessRulesPriorityManager = new ClientAccessRulesPriorityManager(ClientAccessRulesStorageManager.GetClientAccessRules(session));
			ClientAccessRuleCollection clientAccessRuleCollection = new ClientAccessRuleCollection((base.Identity == null) ? OrganizationId.ForestWideOrgId.ToString() : base.Identity.ToString());
			foreach (ADClientAccessRule adclientAccessRule in clientAccessRulesPriorityManager.ADClientAccessRules)
			{
				ClientAccessRule clientAccessRule = adclientAccessRule.GetClientAccessRule();
				if (clientAccessRule.Enabled == RuleState.Disabled)
				{
					base.WriteVerbose(RulesTasksStrings.ClientAccessRuleWillBeConsideredEnabled(clientAccessRule.Name));
					clientAccessRule.Enabled = RuleState.Enabled;
				}
				base.WriteVerbose(RulesTasksStrings.ClientAccessRuleWillBeAddedToCollection(clientAccessRule.Name));
				clientAccessRuleCollection.Add(clientAccessRule);
			}
			return clientAccessRuleCollection;
		}
	}
}
