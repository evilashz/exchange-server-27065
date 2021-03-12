using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Classification;

namespace Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration
{
	// Token: 0x02000150 RID: 336
	internal sealed class PerTenantClassificationDefinitionCollection : PerTenantConfigurationLoader<IEnumerable<ClassificationRulePackage>>
	{
		// Token: 0x0600093C RID: 2364 RVA: 0x00027AA4 File Offset: 0x00025CA4
		public PerTenantClassificationDefinitionCollection(OrganizationId organizationId) : base(organizationId)
		{
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x00027AB0 File Offset: 0x00025CB0
		public IEnumerable<ClassificationRulePackage> ClassificationDefinitions
		{
			get
			{
				if (!this.organizationId.Equals(OrganizationId.ForestWideOrgId))
				{
					return this.data.Concat(PerTenantClassificationDefinitionCollection.oobClassificationDefinitions.Value.ClassificationDefinitions);
				}
				return this.data;
			}
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00027AF2 File Offset: 0x00025CF2
		public override void Initialize()
		{
			base.Initialize(PerTenantClassificationDefinitionCollection.NotificationLock);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00027AFF File Offset: 0x00025CFF
		protected override ADNotificationRequestCookie Register(IConfigurationSession session)
		{
			return ADNotificationAdapter.RegisterChangeNotification<TransportRule>(PerTenantClassificationDefinitionCollection.GetClassificationDefinitionsContainerId(session), new ADNotificationCallback(base.ChangeCallback), session);
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00027B19 File Offset: 0x00025D19
		protected override bool RefreshOnChange
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00027C0C File Offset: 0x00025E0C
		protected override IEnumerable<ClassificationRulePackage> Read(IConfigurationSession session)
		{
			ADPagedReader<TransportRule> adpagedReader = session.FindPaged<TransportRule>(PerTenantClassificationDefinitionCollection.GetClassificationDefinitionsContainerId(session), QueryScope.SubTree, null, PerTenantClassificationDefinitionCollection.PriorityOrder, 0);
			TransportRule[] source = adpagedReader.ReadAllPages();
			ClassificationParser parser = ClassificationParser.Instance;
			return (from rule in source.Select(delegate(TransportRule adRule)
			{
				try
				{
					ClassificationRulePackage rulePackage = parser.GetRulePackage(adRule.ReplicationSignature);
					rulePackage.Version = (adRule.WhenChangedUTC ?? (adRule.WhenCreatedUTC ?? DateTime.MinValue));
					rulePackage.ID = adRule.Name;
					return rulePackage;
				}
				catch (ParserException ex)
				{
					PerTenantClassificationDefinitionCollection.Tracer.TraceError<ADObjectId, Exception>((long)this.GetHashCode(), "Rule with identity {0} is corrupted and will not be returned to clients.  Details: {1}", adRule.Id, ex);
					CachedOrganizationConfiguration.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_CorruptClassificationDefinition, adRule.Id.ToString(), new object[]
					{
						adRule.Id,
						ex
					});
				}
				return null;
			})
			where rule != null
			select rule).ToList<ClassificationRulePackage>();
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00027C88 File Offset: 0x00025E88
		private static ADObjectId GetClassificationDefinitionsContainerId(IConfigurationSession session)
		{
			ADObjectId orgContainerId = session.GetOrgContainerId();
			return orgContainerId.GetDescendantId("Transport Settings", "Rules", new string[]
			{
				"ClassificationDefinitions"
			});
		}

		// Token: 0x0400072B RID: 1835
		private const string EventSource = "MSExchange ClassificationDefinitions";

		// Token: 0x0400072C RID: 1836
		private static readonly Trace Tracer = ExTraceGlobals.ClassificationDefinitionsTracer;

		// Token: 0x0400072D RID: 1837
		private static readonly ExEventLog Logger = new ExEventLog(ExTraceGlobals.ClassificationDefinitionsTracer.Category, "MSExchange ClassificationDefinitions");

		// Token: 0x0400072E RID: 1838
		private static readonly object NotificationLock = new object();

		// Token: 0x0400072F RID: 1839
		private static readonly SortBy PriorityOrder = new SortBy(TransportRuleSchema.Priority, SortOrder.Ascending);

		// Token: 0x04000730 RID: 1840
		private static Lazy<PerTenantClassificationDefinitionCollection> oobClassificationDefinitions = new Lazy<PerTenantClassificationDefinitionCollection>(delegate()
		{
			PerTenantClassificationDefinitionCollection perTenantClassificationDefinitionCollection = new PerTenantClassificationDefinitionCollection(OrganizationId.ForestWideOrgId);
			perTenantClassificationDefinitionCollection.Initialize();
			return perTenantClassificationDefinitionCollection;
		}, LazyThreadSafetyMode.ExecutionAndPublication);
	}
}
