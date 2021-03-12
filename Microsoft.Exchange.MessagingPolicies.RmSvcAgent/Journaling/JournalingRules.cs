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
	// Token: 0x02000004 RID: 4
	internal sealed class JournalingRules
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002458 File Offset: 0x00000658
		public TransportRuleCollection Rules
		{
			get
			{
				return this.journalingRules;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002460 File Offset: 0x00000660
		static JournalingRules()
		{
			ExTraceGlobals.FaultInjectionTracer.RegisterExceptionInjectionCallback(new ExceptionInjectionCallback(JournalingRules.FaultInjectionCallback));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000249A File Offset: 0x0000069A
		private JournalingRules(TransportRuleCollection rules)
		{
			this.journalingRules = rules;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000024AC File Offset: 0x000006AC
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

		// Token: 0x0600000D RID: 13 RVA: 0x00002560 File Offset: 0x00000760
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

		// Token: 0x0600000E RID: 14 RVA: 0x00002634 File Offset: 0x00000834
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

		// Token: 0x0600000F RID: 15 RVA: 0x000026E8 File Offset: 0x000008E8
		private JournalingRules()
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000026F0 File Offset: 0x000008F0
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

		// Token: 0x06000011 RID: 17 RVA: 0x0000286C File Offset: 0x00000A6C
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

		// Token: 0x06000012 RID: 18 RVA: 0x00002A78 File Offset: 0x00000C78
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

		// Token: 0x06000013 RID: 19 RVA: 0x00002B20 File Offset: 0x00000D20
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

		// Token: 0x06000014 RID: 20 RVA: 0x00002BBC File Offset: 0x00000DBC
		private static void Configure(ADNotificationEventArgs args)
		{
			lock (JournalingRules.staticLock)
			{
				JournalingRules.current = JournalingRules.Load(OrganizationId.ForestWideOrgId);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002C04 File Offset: 0x00000E04
		private static void ConfigureGcc(ADNotificationEventArgs args)
		{
			lock (JournalingRules.staticLock)
			{
				JournalingRules.currentGcc = JournalingRules.LoadGccRules();
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002C48 File Offset: 0x00000E48
		private static Exception FaultInjectionCallback(string exceptionType)
		{
			LocalizedString localizedString = new LocalizedString("Fault injection.");
			if (exceptionType.Equals("Microsoft.Exchange.MessagingPolicies.Rules.ParserException", StringComparison.OrdinalIgnoreCase))
			{
				return new ParserException(localizedString);
			}
			return new Exception(localizedString);
		}

		// Token: 0x04000009 RID: 9
		private static object staticLock = new object();

		// Token: 0x0400000A RID: 10
		private static JournalingRules current = null;

		// Token: 0x0400000B RID: 11
		private static List<GccRuleEntry> currentGcc = null;

		// Token: 0x0400000C RID: 12
		private static bool registeredForDynamicNotification = false;

		// Token: 0x0400000D RID: 13
		private static bool registeredForDynamicNotificationForGcc = false;

		// Token: 0x0400000E RID: 14
		private TransportRuleCollection journalingRules;
	}
}
