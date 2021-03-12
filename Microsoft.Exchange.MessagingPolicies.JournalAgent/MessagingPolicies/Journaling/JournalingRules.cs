using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.Transport;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Diagnostics.FaultInjection;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Configuration;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x02000018 RID: 24
	internal sealed class JournalingRules
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000098E3 File Offset: 0x00007AE3
		public TransportRuleCollection Rules
		{
			get
			{
				return this.journalingRules;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000098EB File Offset: 0x00007AEB
		static JournalingRules()
		{
			ExTraceGlobals.FaultInjectionTracer.RegisterExceptionInjectionCallback(new ExceptionInjectionCallback(JournalingRules.FaultInjectionCallback));
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00009925 File Offset: 0x00007B25
		private JournalingRules(TransportRuleCollection rules)
		{
			this.journalingRules = rules;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00009934 File Offset: 0x00007B34
		public static List<GccRuleEntry> GetGccConfig()
		{
			if (JournalingRules.currentGcc == null)
			{
				lock (JournalingRules.staticLock)
				{
					if (JournalingRules.currentGcc == null)
					{
						JournalingRules.currentGcc = JournalingRules.LoadGccRules();
						if (!JournalingRules.registeredForDynamicNotificationForGcc)
						{
							IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(OrganizationId.ForestWideOrgId), 120, "GetGccConfig", "f:\\15.00.1497\\sources\\dev\\MessagingPolicies\\src\\Journaling\\Agent\\JournalingRules.cs");
							ADJournalRuleStorageManager adjournalRuleStorageManager = new ADJournalRuleStorageManager("JournalingVersioned", tenantOrTopologyConfigurationSession);
							TransportADNotificationAdapter.Instance.RegisterForJournalRuleNotifications(adjournalRuleStorageManager.RuleCollectionId, new ADNotificationCallback(JournalingRules.ConfigureGcc));
							JournalingRules.registeredForDynamicNotificationForGcc = true;
						}
					}
				}
			}
			return JournalingRules.currentGcc;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000099E8 File Offset: 0x00007BE8
		public static JournalingRules GetConfig(OrganizationId organizationId)
		{
			if (organizationId != OrganizationId.ForestWideOrgId)
			{
				return JournalingRules.Load(organizationId);
			}
			if (JournalingRules.current == null)
			{
				lock (JournalingRules.staticLock)
				{
					if (JournalingRules.current == null)
					{
						JournalingRules journalingRules = JournalingRules.Load(organizationId);
						if (journalingRules == null)
						{
							return null;
						}
						JournalingRules.current = journalingRules;
						if (!JournalingRules.registeredForDynamicNotification)
						{
							IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 176, "GetConfig", "f:\\15.00.1497\\sources\\dev\\MessagingPolicies\\src\\Journaling\\Agent\\JournalingRules.cs");
							ADJournalRuleStorageManager adjournalRuleStorageManager = new ADJournalRuleStorageManager("JournalingVersioned", tenantOrTopologyConfigurationSession);
							TransportADNotificationAdapter.Instance.RegisterForJournalRuleNotifications(adjournalRuleStorageManager.RuleCollectionId, new ADNotificationCallback(JournalingRules.Configure));
							JournalingRules.registeredForDynamicNotification = true;
						}
					}
				}
			}
			return JournalingRules.current;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00009ABC File Offset: 0x00007CBC
		public bool IsConfiguredJournalTargetAddress(string smtpAddress)
		{
			foreach (Rule rule in this.Rules)
			{
				JournalingRule journalingRule = (JournalingRule)rule;
				if (journalingRule.Enabled == RuleState.Enabled)
				{
					foreach (Microsoft.Exchange.MessagingPolicies.Rules.Action action in journalingRule.Actions)
					{
						Journal journal = (Journal)action;
						string targetAddress = journal.GetTargetAddress();
						if (string.Compare(targetAddress, smtpAddress, StringComparison.OrdinalIgnoreCase) == 0)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00009B70 File Offset: 0x00007D70
		private JournalingRules()
		{
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00009B78 File Offset: 0x00007D78
		private static JournalingRules Load(OrganizationId organizationId)
		{
			JournalingRules journalingRules = null;
			Exception ex = null;
			try
			{
				ADJournalRuleStorageManager adjournalRuleStorageManager;
				if (organizationId == OrganizationId.ForestWideOrgId)
				{
					IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 252, "Load", "f:\\15.00.1497\\sources\\dev\\MessagingPolicies\\src\\Journaling\\Agent\\JournalingRules.cs");
					adjournalRuleStorageManager = new ADJournalRuleStorageManager("JournalingVersioned", tenantOrTopologyConfigurationSession);
					adjournalRuleStorageManager.LoadRuleCollection();
				}
				else
				{
					JournalingRulesPerTenantSettings journalingRulesPerTenantSettings;
					if (!Components.Configuration.TryGetJournalingRules(organizationId, out journalingRulesPerTenantSettings))
					{
						ExTraceGlobals.JournalingTracer.TraceError<OrganizationId>(0L, "Failed to load journal rules for tenant {0}", organizationId);
						return null;
					}
					adjournalRuleStorageManager = new ADJournalRuleStorageManager("JournalingVersioned", journalingRulesPerTenantSettings.JournalRuleDataList);
					ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(3609603389U, organizationId.OrganizationalUnit.Name);
					adjournalRuleStorageManager.ParseRuleCollection();
				}
				TransportRuleCollection ruleCollection = adjournalRuleStorageManager.GetRuleCollection();
				if (ruleCollection == null)
				{
					ExTraceGlobals.JournalingTracer.TraceError<OrganizationId>(0L, "Failed to get journal rules for tenant {0}", organizationId);
					return null;
				}
				journalingRules = new JournalingRules(ruleCollection);
			}
			catch (TransientException ex2)
			{
				ex = ex2;
			}
			catch (DataValidationException ex3)
			{
				ex = ex3;
			}
			catch (ParserException ex4)
			{
				ex = ex4;
			}
			catch (ExchangeConfigurationException ex5)
			{
				ex = ex5;
			}
			finally
			{
				if (organizationId == OrganizationId.ForestWideOrgId)
				{
					if (journalingRules != null)
					{
						RuleAuditProvider.LogSuccess("JournalingVersioned");
					}
					else
					{
						RuleAuditProvider.LogFailure("JournalingVersioned");
					}
				}
			}
			if (ex != null)
			{
				ExTraceGlobals.JournalingTracer.TraceError<Exception>(0L, "Failed to load rules, Exception: {0}", ex);
			}
			return journalingRules;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00009CF4 File Offset: 0x00007EF4
		private static List<GccRuleEntry> LoadGccRules()
		{
			List<GccRuleEntry> list = null;
			Exception ex = null;
			try
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(OrganizationId.ForestWideOrgId), 356, "LoadGccRules", "f:\\15.00.1497\\sources\\dev\\MessagingPolicies\\src\\Journaling\\Agent\\JournalingRules.cs");
				ADJournalRuleStorageManager adjournalRuleStorageManager = new ADJournalRuleStorageManager("JournalingVersioned", tenantOrTopologyConfigurationSession);
				adjournalRuleStorageManager.LoadRuleCollection();
				TransportRuleCollection ruleCollection = adjournalRuleStorageManager.GetRuleCollection();
				if (ruleCollection == null)
				{
					ExTraceGlobals.JournalingTracer.TraceError(0L, "Failed to get GCC journal rules");
					return null;
				}
				list = new List<GccRuleEntry>(ruleCollection.Count);
				foreach (Rule rule in ruleCollection)
				{
					JournalingRule journalingRule = (JournalingRule)rule;
					if (!journalingRule.IsTooAdvancedToParse && journalingRule.GccRuleType != GccType.None && journalingRule.Enabled == RuleState.Enabled && (journalingRule.ExpiryDate == null || !(DateTime.UtcNow.Date > journalingRule.ExpiryDate.Value.Date)))
					{
						GccRuleEntry item = new GccRuleEntry(journalingRule.ImmutableId, journalingRule.Name, JournalingRules.ParseGccRecipient(journalingRule), journalingRule.GccRuleType == GccType.Full, journalingRule.ExpiryDate, JournalingRules.ParseGccJournalEmailAddress(journalingRule));
						list.Add(item);
					}
				}
			}
			catch (TransientException ex2)
			{
				ex = ex2;
			}
			catch (DataValidationException ex3)
			{
				ex = ex3;
			}
			catch (ParserException ex4)
			{
				ex = ex4;
			}
			catch (ExchangeConfigurationException ex5)
			{
				ex = ex5;
			}
			if (ex != null)
			{
				ExTraceGlobals.JournalingTracer.TraceError<Exception>(0L, "Exception hit while loading GCC rules, Details: {0}", ex);
				list = null;
			}
			return list;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00009F00 File Offset: 0x00008100
		private static SmtpAddress ParseGccRecipient(JournalingRule rule)
		{
			if (rule == null)
			{
				return SmtpAddress.Empty;
			}
			OrCondition orCondition = rule.Condition as OrCondition;
			if (orCondition == null)
			{
				return SmtpAddress.Empty;
			}
			List<Condition> subConditions = orCondition.SubConditions;
			if (subConditions == null || subConditions.Count == 0)
			{
				return SmtpAddress.Empty;
			}
			OrCondition orCondition2 = subConditions[0] as OrCondition;
			if (orCondition2 == null || orCondition2.SubConditions == null || orCondition2.SubConditions.Count == 0)
			{
				return SmtpAddress.Empty;
			}
			PredicateCondition predicateCondition = orCondition2.SubConditions[0] as PredicateCondition;
			if (predicateCondition == null || predicateCondition.Value == null)
			{
				return SmtpAddress.Empty;
			}
			return new SmtpAddress(predicateCondition.Value.RawValues[0]);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00009FA8 File Offset: 0x000081A8
		private static SmtpAddress ParseGccJournalEmailAddress(JournalingRule rule)
		{
			if (rule == null || rule.Actions == null || rule.Actions.Count == 0 || rule.Actions[0] == null || rule.Actions[0].Arguments.Count == 0)
			{
				return SmtpAddress.Empty;
			}
			Value value = (Value)rule.Actions[0].Arguments[0];
			if (value == null || !SmtpAddress.IsValidSmtpAddress(value.ParsedValue.ToString()))
			{
				return SmtpAddress.Empty;
			}
			return SmtpAddress.Parse(value.ParsedValue.ToString());
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000A044 File Offset: 0x00008244
		private static void Configure(ADNotificationEventArgs args)
		{
			lock (JournalingRules.staticLock)
			{
				JournalingRules.current = JournalingRules.Load(OrganizationId.ForestWideOrgId);
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000A08C File Offset: 0x0000828C
		private static void ConfigureGcc(ADNotificationEventArgs args)
		{
			lock (JournalingRules.staticLock)
			{
				JournalingRules.currentGcc = JournalingRules.LoadGccRules();
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000A0D0 File Offset: 0x000082D0
		private static Exception FaultInjectionCallback(string exceptionType)
		{
			LocalizedString localizedString = new LocalizedString("Fault injection.");
			if (exceptionType.Equals("Microsoft.Exchange.MessagingPolicies.Rules.ParserException", StringComparison.OrdinalIgnoreCase))
			{
				return new ParserException(localizedString);
			}
			return new Exception(localizedString);
		}

		// Token: 0x040000A2 RID: 162
		private static object staticLock = new object();

		// Token: 0x040000A3 RID: 163
		private static JournalingRules current = null;

		// Token: 0x040000A4 RID: 164
		private static List<GccRuleEntry> currentGcc = null;

		// Token: 0x040000A5 RID: 165
		private static bool registeredForDynamicNotification = false;

		// Token: 0x040000A6 RID: 166
		private static bool registeredForDynamicNotificationForGcc = false;

		// Token: 0x040000A7 RID: 167
		private TransportRuleCollection journalingRules;
	}
}
