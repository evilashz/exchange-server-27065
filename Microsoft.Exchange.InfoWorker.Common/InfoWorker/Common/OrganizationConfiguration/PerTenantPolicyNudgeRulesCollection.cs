using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.MessagingPolicies.Rules.PolicyNudges;

namespace Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration
{
	// Token: 0x02000152 RID: 338
	internal sealed class PerTenantPolicyNudgeRulesCollection : PerTenantConfigurationLoader<PerTenantPolicyNudgeRulesCollection.PolicyNudgeRules>
	{
		// Token: 0x0600094D RID: 2381 RVA: 0x00027DB2 File Offset: 0x00025FB2
		public PerTenantPolicyNudgeRulesCollection(OrganizationId organizationId) : base(organizationId)
		{
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x00027DBB File Offset: 0x00025FBB
		public PerTenantPolicyNudgeRulesCollection.PolicyNudgeRules Rules
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00027DC3 File Offset: 0x00025FC3
		public override void Initialize()
		{
			base.Initialize(PerTenantPolicyNudgeRulesCollection.NotificationLock);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00027DD0 File Offset: 0x00025FD0
		protected override ADNotificationRequestCookie Register(IConfigurationSession session)
		{
			ADNotificationRequestCookie request = ADNotificationAdapter.RegisterChangeNotification<PolicyTipMessageConfig>(PerTenantPolicyNudgeRulesCollection.GetPolicyTipMessageConfigsContainerId(session), new ADNotificationCallback(base.ChangeCallback), session);
			ADNotificationRequestCookie result;
			try
			{
				result = ADNotificationAdapter.RegisterChangeNotification<TransportRule>(PerTenantPolicyNudgeRulesCollection.GetPolicyNudgeRuleContainerId(session), new ADNotificationCallback(base.ChangeCallback), session);
			}
			catch (Exception ex)
			{
				ADNotificationAdapter.UnregisterChangeNotification(request);
				throw ex;
			}
			return result;
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x00027E2C File Offset: 0x0002602C
		protected override bool RefreshOnChange
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00027E9C File Offset: 0x0002609C
		protected override PerTenantPolicyNudgeRulesCollection.PolicyNudgeRules Read(IConfigurationSession session)
		{
			ADPagedReader<PolicyTipMessageConfig> adpagedReader = session.FindPaged<PolicyTipMessageConfig>(PerTenantPolicyNudgeRulesCollection.GetPolicyTipMessageConfigsContainerId(session), QueryScope.SubTree, null, null, 0);
			IEnumerable<PolicyTipMessageConfig> tenantPolicyTipMessages = adpagedReader.ReadAllPages();
			ADPagedReader<TransportRule> adpagedReader2 = session.FindPaged<TransportRule>(PerTenantPolicyNudgeRulesCollection.GetPolicyNudgeRuleContainerId(session), QueryScope.SubTree, null, PerTenantPolicyNudgeRulesCollection.PriorityOrder, 0);
			PolicyNudgeRuleParser parser = PolicyNudgeRuleParser.Instance;
			IEnumerable<PolicyNudgeRule> tenantPolicyNudgeRules = (from rule in adpagedReader2.ReadAllPages()
			select parser.GetRule(rule.Xml, rule.Name, rule.WhenChangedUTC ?? (rule.WhenCreatedUTC ?? DateTime.MinValue)) into rule
			where rule != null
			select rule).ToList<PolicyNudgeRule>();
			return new PerTenantPolicyNudgeRulesCollection.PolicyNudgeRules(tenantPolicyNudgeRules, new PerTenantPolicyNudgeRulesCollection.PolicyTipMessages(tenantPolicyTipMessages));
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00027F38 File Offset: 0x00026138
		private static ADObjectId GetPolicyNudgeRuleContainerId(IConfigurationSession session)
		{
			ADObjectId orgContainerId = session.GetOrgContainerId();
			return orgContainerId.GetDescendantId("Transport Settings", "Rules", new string[]
			{
				"TransportVersioned"
			});
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00027F6C File Offset: 0x0002616C
		private static ADObjectId GetPolicyTipMessageConfigsContainerId(IConfigurationSession session)
		{
			ADObjectId orgContainerId = session.GetOrgContainerId();
			return orgContainerId.GetDescendantId(PolicyTipMessageConfig.PolicyTipMessageConfigContainer);
		}

		// Token: 0x04000734 RID: 1844
		private const string EventSource = "MSExchange OutlookPolicyNudgeRules";

		// Token: 0x04000735 RID: 1845
		private static readonly Trace Tracer = ExTraceGlobals.OutlookPolicyNudgeRulesTracer;

		// Token: 0x04000736 RID: 1846
		private static readonly ExEventLog Logger = new ExEventLog(ExTraceGlobals.OutlookPolicyNudgeRulesTracer.Category, "MSExchange OutlookPolicyNudgeRules");

		// Token: 0x04000737 RID: 1847
		private static readonly object NotificationLock = new object();

		// Token: 0x04000738 RID: 1848
		private static readonly SortBy PriorityOrder = new SortBy(TransportRuleSchema.Priority, SortOrder.Ascending);

		// Token: 0x02000153 RID: 339
		internal class PolicyNudgeRules
		{
			// Token: 0x06000957 RID: 2391 RVA: 0x00027FCA File Offset: 0x000261CA
			internal PolicyNudgeRules(IEnumerable<PolicyNudgeRule> tenantPolicyNudgeRules, PerTenantPolicyNudgeRulesCollection.PolicyTipMessages policyTipMessages)
			{
				this.Rules = tenantPolicyNudgeRules;
				this.Messages = policyTipMessages;
			}

			// Token: 0x1700025B RID: 603
			// (get) Token: 0x06000958 RID: 2392 RVA: 0x00027FE0 File Offset: 0x000261E0
			// (set) Token: 0x06000959 RID: 2393 RVA: 0x00027FE8 File Offset: 0x000261E8
			internal IEnumerable<PolicyNudgeRule> Rules { get; private set; }

			// Token: 0x1700025C RID: 604
			// (get) Token: 0x0600095A RID: 2394 RVA: 0x00027FF1 File Offset: 0x000261F1
			// (set) Token: 0x0600095B RID: 2395 RVA: 0x00027FF9 File Offset: 0x000261F9
			internal PerTenantPolicyNudgeRulesCollection.PolicyTipMessages Messages { get; private set; }

			// Token: 0x0400073A RID: 1850
			internal static PerTenantPolicyNudgeRulesCollection.PolicyNudgeRules Empty = new PerTenantPolicyNudgeRulesCollection.PolicyNudgeRules(new PolicyNudgeRule[0], new PerTenantPolicyNudgeRulesCollection.PolicyTipMessages(new PolicyTipMessageConfig[0]));
		}

		// Token: 0x02000154 RID: 340
		internal class PolicyTipMessages
		{
			// Token: 0x0600095D RID: 2397 RVA: 0x0002801F File Offset: 0x0002621F
			internal PolicyTipMessages(IEnumerable<PolicyTipMessageConfig> tenantPolicyTipMessages)
			{
				this.tenantPolicyTipMessages = PerTenantPolicyNudgeRulesCollection.PolicyTipMessages.ToDictionary(tenantPolicyTipMessages);
			}

			// Token: 0x0600095E RID: 2398 RVA: 0x000283D4 File Offset: 0x000265D4
			private static IEnumerable<PolicyTipMessageConfig> GetBuiltIn()
			{
				ADObjectId GlobalScopeContainerId = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest().GetDescendantId(PolicyTipMessageConfig.PolicyTipMessageConfigContainer);
				IEnumerable<CultureInfo> supportedCultureInfos = from lcid in LanguagePackInfo.expectedCultureLcids
				select new CultureInfo(lcid);
				PolicyTipMessageConfig policyTipMessageConfig;
				foreach (CultureInfo exchangeCultureInfo in supportedCultureInfos)
				{
					foreach (Tuple<PolicyTipMessageConfigAction, LocalizedString> mapping in PerTenantPolicyNudgeRulesCollection.PolicyTipMessages.builtInActionStringsMapping)
					{
						policyTipMessageConfig = new PolicyTipMessageConfig
						{
							Action = mapping.Item1,
							Locale = exchangeCultureInfo.Name,
							Value = mapping.Item2.ToString(exchangeCultureInfo)
						};
						policyTipMessageConfig.SetId(GlobalScopeContainerId.GetChildId("BuiltIn\\" + exchangeCultureInfo.Name + "\\" + mapping.Item1.ToString()));
						yield return policyTipMessageConfig;
					}
				}
				policyTipMessageConfig = new PolicyTipMessageConfig
				{
					Action = PolicyTipMessageConfigAction.Url,
					Locale = string.Empty,
					Value = string.Empty
				};
				policyTipMessageConfig.SetId(GlobalScopeContainerId.GetChildId("BuiltIn\\" + PolicyTipMessageConfigAction.Url.ToString()));
				yield return policyTipMessageConfig;
				yield break;
			}

			// Token: 0x0600095F RID: 2399 RVA: 0x0002845C File Offset: 0x0002665C
			private static IDictionary<Tuple<string, PolicyTipMessageConfigAction>, PolicyTipMessage> ToDictionary(IEnumerable<PolicyTipMessageConfig> policyTipMessageConfigs)
			{
				return policyTipMessageConfigs.ToDictionary((PolicyTipMessageConfig m) => Tuple.Create<string, PolicyTipMessageConfigAction>(m.Locale, m.Action), (PolicyTipMessageConfig m) => new PolicyTipMessage(m.Value, m.Identity.ToString(), m.WhenChangedUTC ?? (m.WhenCreatedUTC ?? DateTime.MinValue)));
			}

			// Token: 0x06000960 RID: 2400 RVA: 0x000284A9 File Offset: 0x000266A9
			public bool TryGetValue(Tuple<string, PolicyTipMessageConfigAction> key, out PolicyTipMessage value)
			{
				return this.tenantPolicyTipMessages.TryGetValue(key, out value) || PerTenantPolicyNudgeRulesCollection.PolicyTipMessages.builtIn.Value.TryGetValue(key, out value);
			}

			// Token: 0x0400073D RID: 1853
			private IDictionary<Tuple<string, PolicyTipMessageConfigAction>, PolicyTipMessage> tenantPolicyTipMessages;

			// Token: 0x0400073E RID: 1854
			private static Lazy<IDictionary<Tuple<string, PolicyTipMessageConfigAction>, PolicyTipMessage>> builtIn = new Lazy<IDictionary<Tuple<string, PolicyTipMessageConfigAction>, PolicyTipMessage>>(() => PerTenantPolicyNudgeRulesCollection.PolicyTipMessages.ToDictionary(PerTenantPolicyNudgeRulesCollection.PolicyTipMessages.builtInConfigs.Value), LazyThreadSafetyMode.PublicationOnly);

			// Token: 0x0400073F RID: 1855
			internal static Lazy<IEnumerable<PolicyTipMessageConfig>> builtInConfigs = new Lazy<IEnumerable<PolicyTipMessageConfig>>(() => PerTenantPolicyNudgeRulesCollection.PolicyTipMessages.GetBuiltIn().ToList<PolicyTipMessageConfig>(), LazyThreadSafetyMode.PublicationOnly);

			// Token: 0x04000740 RID: 1856
			private static Tuple<PolicyTipMessageConfigAction, LocalizedString>[] builtInActionStringsMapping = new Tuple<PolicyTipMessageConfigAction, LocalizedString>[]
			{
				Tuple.Create<PolicyTipMessageConfigAction, LocalizedString>(PolicyTipMessageConfigAction.NotifyOnly, ClientStrings.PolicyTipDefaultMessageNotifyOnly),
				Tuple.Create<PolicyTipMessageConfigAction, LocalizedString>(PolicyTipMessageConfigAction.RejectOverride, ClientStrings.PolicyTipDefaultMessageRejectOverride),
				Tuple.Create<PolicyTipMessageConfigAction, LocalizedString>(PolicyTipMessageConfigAction.Reject, ClientStrings.PolicyTipDefaultMessageReject)
			};
		}
	}
}
