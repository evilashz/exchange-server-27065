using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.OutlookProtection;

namespace Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration
{
	// Token: 0x02000155 RID: 341
	internal sealed class PerTenantProtectionRulesCollection : PerTenantConfigurationLoader<IEnumerable<OutlookProtectionRule>>
	{
		// Token: 0x06000967 RID: 2407 RVA: 0x00028580 File Offset: 0x00026780
		public PerTenantProtectionRulesCollection(OrganizationId organizationId) : base(organizationId)
		{
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x00028589 File Offset: 0x00026789
		public IEnumerable<OutlookProtectionRule> ProtectionRules
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00028591 File Offset: 0x00026791
		public override void Initialize()
		{
			base.Initialize(PerTenantProtectionRulesCollection.NotificationLock);
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0002859E File Offset: 0x0002679E
		protected override ADNotificationRequestCookie Register(IConfigurationSession session)
		{
			return ADNotificationAdapter.RegisterChangeNotification<TransportRule>(PerTenantProtectionRulesCollection.GetProtectionRuleContainerId(session), new ADNotificationCallback(base.ChangeCallback), session);
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x000285B8 File Offset: 0x000267B8
		protected override IEnumerable<OutlookProtectionRule> Read(IConfigurationSession session)
		{
			ADPagedReader<TransportRule> adpagedReader = session.FindPaged<TransportRule>(PerTenantProtectionRulesCollection.GetProtectionRuleContainerId(session), QueryScope.SubTree, null, PerTenantProtectionRulesCollection.PriorityOrder, 0);
			TransportRule[] array = adpagedReader.ReadAllPages();
			List<OutlookProtectionRule> list = new List<OutlookProtectionRule>(array.Length);
			OutlookProtectionRuleParser instance = OutlookProtectionRuleParser.Instance;
			foreach (TransportRule transportRule in array)
			{
				try
				{
					list.Add((OutlookProtectionRule)instance.GetRule(transportRule.Xml));
				}
				catch (ParserException ex)
				{
					PerTenantProtectionRulesCollection.Tracer.TraceError<ADObjectId, Exception>((long)this.GetHashCode(), "Rule with identity {0} is corrupted and will not be returned to clients.  Details: {1}", transportRule.Id, ex);
					CachedOrganizationConfiguration.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_CorruptOutlookProtectionRule, transportRule.Id.ToString(), new object[]
					{
						transportRule.Id,
						ex
					});
				}
			}
			return list;
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00028698 File Offset: 0x00026898
		private static ADObjectId GetProtectionRuleContainerId(IConfigurationSession session)
		{
			ADObjectId orgContainerId = session.GetOrgContainerId();
			return orgContainerId.GetDescendantId("Transport Settings", "Rules", new string[]
			{
				"OutlookProtectionRules"
			});
		}

		// Token: 0x04000746 RID: 1862
		private const string EventSource = "MSExchange OutlookProtectionRules";

		// Token: 0x04000747 RID: 1863
		private static readonly Trace Tracer = ExTraceGlobals.OutlookProtectionRulesTracer;

		// Token: 0x04000748 RID: 1864
		private static readonly ExEventLog Logger = new ExEventLog(ExTraceGlobals.OutlookProtectionRulesTracer.Category, "MSExchange OutlookProtectionRules");

		// Token: 0x04000749 RID: 1865
		private static readonly object NotificationLock = new object();

		// Token: 0x0400074A RID: 1866
		private static readonly SortBy PriorityOrder = new SortBy(TransportRuleSchema.Priority, SortOrder.Ascending);
	}
}
