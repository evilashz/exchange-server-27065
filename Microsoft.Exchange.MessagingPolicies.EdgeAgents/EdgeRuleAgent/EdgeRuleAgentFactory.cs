using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MessagingPolicies.EdgeRuleAgent
{
	// Token: 0x02000002 RID: 2
	public class EdgeRuleAgentFactory : SmtpReceiveAgentFactory
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public EdgeRuleAgentFactory()
		{
			if (VariantConfiguration.InvariantNoFlightingSnapshot.CompliancePolicy.RuleConfigurationAdChangeNotifications.Enabled)
			{
				ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(new ADOperation(this.RegisterConfigurationChangeHandlers));
				if (!adoperationResult.Succeeded)
				{
					ExTraceGlobals.TransportRulesEngineTracer.TraceError(0L, "Unable to register for AD Change notification");
					throw new ExchangeConfigurationException(TransportRulesStrings.FailedToRegisterForConfigChangeNotification(EdgeRuleAgentFactory.RuleAgentName), adoperationResult.Exception);
				}
			}
			EdgeRuleAgentFactory.Configure(null);
			if (EdgeRuleAgentFactory.shouldDefer)
			{
				throw new ExchangeConfigurationException(TransportRulesStrings.FailedToLoadRuleCollection(EdgeRuleAgentFactory.RuleAgentName));
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000215A File Offset: 0x0000035A
		public override SmtpReceiveAgent CreateAgent(SmtpServer server)
		{
			return new EdgeRuleAgent(server, EdgeRuleAgentFactory.rules, EdgeRuleAgentFactory.shouldDefer);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002174 File Offset: 0x00000374
		private static void Configure(ADNotificationEventArgs args)
		{
			bool flag = false;
			try
			{
				ADNotificationAdapter.RunADOperation(delegate()
				{
					EdgeRuleAgentFactory.LoadRules();
				}, 5);
				flag = !EdgeRuleAgentFactory.shouldDefer;
			}
			catch (TransientException)
			{
				TransportAction.Logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_RuleCollectionLoadingTransientError, null, new object[]
				{
					EdgeRuleAgentFactory.RuleCollectionName
				});
			}
			finally
			{
				if (!flag)
				{
					RuleAuditProvider.LogFailure(EdgeRuleAgentFactory.RuleCollectionName);
				}
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002204 File Offset: 0x00000404
		private static void LoadRules()
		{
			lock (EdgeRuleAgentFactory.lockVar)
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 145, "LoadRules", "f:\\15.00.1497\\sources\\dev\\MessagingPolicies\\src\\EdgeRuleAgent\\EdgeRuleAgentFactory.cs");
				ADRuleStorageManager adruleStorageManager = null;
				Exception ex = null;
				try
				{
					adruleStorageManager = new ADRuleStorageManager(EdgeRuleAgentFactory.RuleCollectionName, tenantOrTopologyConfigurationSession);
					adruleStorageManager.LoadRuleCollection();
				}
				catch (ParserException ex2)
				{
					ex = ex2;
				}
				catch (DataSourceOperationException ex3)
				{
					ex = ex3;
				}
				catch (RuleCollectionNotInAdException ex4)
				{
					ex = ex4;
				}
				catch (ExchangeConfigurationException ex5)
				{
					ex = ex5;
				}
				if (ex != null)
				{
					EdgeRuleAgentFactory.rules = null;
					EdgeRuleAgentFactory.shouldDefer = true;
					TransportAction.Logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_RuleCollectionLoadingError, null, new object[]
					{
						EdgeRuleAgentFactory.RuleCollectionName,
						ex.ToString()
					});
				}
				else
				{
					bool flag2 = false;
					string hashOfRuleCollection = adruleStorageManager.GetHashOfRuleCollection();
					if (hashOfRuleCollection != EdgeRuleAgentFactory.cachedRuleCollectionHash)
					{
						flag2 = true;
						EdgeRuleAgentFactory.cachedRuleCollectionHash = hashOfRuleCollection;
					}
					if (flag2)
					{
						TransportRuleCollection transportRuleCollection = adruleStorageManager.GetRuleCollection();
						if (transportRuleCollection.CountAllNotDisabled > 0)
						{
							transportRuleCollection.CreatePerformanceCounters();
						}
						else
						{
							transportRuleCollection = null;
						}
						EdgeRuleAgentFactory.rules = transportRuleCollection;
						EdgeRuleAgentFactory.shouldDefer = false;
						TransportAction.Logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_RuleCollectionLoaded, null, new object[]
						{
							EdgeRuleAgentFactory.RuleCollectionName
						});
						RuleAuditProvider.LogSuccess(EdgeRuleAgentFactory.RuleCollectionName);
					}
				}
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000023C0 File Offset: 0x000005C0
		private void RegisterConfigurationChangeHandlers()
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 235, "RegisterConfigurationChangeHandlers", "f:\\15.00.1497\\sources\\dev\\MessagingPolicies\\src\\EdgeRuleAgent\\EdgeRuleAgentFactory.cs");
			ADRuleStorageManager adruleStorageManager = new ADRuleStorageManager(EdgeRuleAgentFactory.RuleCollectionName, tenantOrTopologyConfigurationSession);
			ADObjectId ruleCollectionId = adruleStorageManager.RuleCollectionId;
			ADNotificationAdapter.RegisterChangeNotification<TransportRule>(ruleCollectionId, new ADNotificationCallback(EdgeRuleAgentFactory.Configure));
		}

		// Token: 0x04000001 RID: 1
		private static readonly string RuleCollectionName = "Edge";

		// Token: 0x04000002 RID: 2
		private static readonly string RuleAgentName = "Edge Rule";

		// Token: 0x04000003 RID: 3
		private static object lockVar = new object();

		// Token: 0x04000004 RID: 4
		private static TransportRuleCollection rules = null;

		// Token: 0x04000005 RID: 5
		private static bool shouldDefer = true;

		// Token: 0x04000006 RID: 6
		private static string cachedRuleCollectionHash = string.Empty;
	}
}
