using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Manager.Throttling
{
	// Token: 0x02000059 RID: 89
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class WorkTypeManager
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0001AAF4 File Offset: 0x00018CF4
		internal static WorkTypeManager Instance
		{
			get
			{
				return WorkTypeManager.workTypeManager;
			}
		}

		// Token: 0x17000123 RID: 291
		internal WorkTypeDefinition this[WorkType workType]
		{
			get
			{
				return this.workTypeDefinitions[workType];
			}
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0001AB0C File Offset: 0x00018D0C
		internal static WorkType ClassifyWorkTypeFromSubscriptionInformation(AggregationType aggregationType, SyncPhase syncPhase)
		{
			if (aggregationType != AggregationType.Aggregation)
			{
				if (aggregationType != AggregationType.Migration)
				{
					if (aggregationType != AggregationType.PeopleConnection)
					{
						throw new InvalidOperationException("unsupported aggregation type");
					}
					switch (syncPhase)
					{
					case SyncPhase.Initial:
						return WorkType.PeopleConnectionInitial;
					case SyncPhase.Incremental:
						return WorkType.PeopleConnectionIncremental;
					case SyncPhase.Delete:
						return WorkType.PolicyInducedDelete;
					}
					throw new InvalidOperationException("unsupported sync phase");
				}
				else
				{
					switch (syncPhase)
					{
					case SyncPhase.Initial:
						return WorkType.MigrationInitial;
					case SyncPhase.Incremental:
						return WorkType.MigrationIncremental;
					case SyncPhase.Finalization:
						return WorkType.MigrationFinalization;
					case SyncPhase.Completed:
						throw new NotSupportedException("Completed migration subscriptions are supposed to be disabled and not added to SQM");
					default:
						throw new InvalidOperationException("unsupported sync phase: " + syncPhase);
					}
				}
			}
			else
			{
				switch (syncPhase)
				{
				case SyncPhase.Initial:
					return WorkType.AggregationInitial;
				case SyncPhase.Incremental:
					return WorkType.AggregationIncremental;
				default:
					throw new InvalidOperationException("unsupported sync phase: " + syncPhase);
				}
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0001ABD8 File Offset: 0x00018DD8
		internal static bool IsOneOffWorkType(WorkType workType)
		{
			if (workType != WorkType.AggregationSubscriptionSaved)
			{
				switch (workType)
				{
				case WorkType.OwaLogonTriggeredSyncNow:
				case WorkType.OwaActivityTriggeredSyncNow:
				case WorkType.OwaRefreshButtonTriggeredSyncNow:
				case WorkType.PeopleConnectionTriggered:
					return true;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001AC0C File Offset: 0x00018E0C
		internal static bool IsLightWeightWorkType(WorkType workType)
		{
			switch (workType)
			{
			case WorkType.AggregationSubscriptionSaved:
			case WorkType.AggregationIncremental:
			case WorkType.MigrationIncremental:
			case WorkType.OwaLogonTriggeredSyncNow:
			case WorkType.OwaActivityTriggeredSyncNow:
			case WorkType.OwaRefreshButtonTriggeredSyncNow:
			case WorkType.PeopleConnectionTriggered:
			case WorkType.PeopleConnectionIncremental:
			case WorkType.PolicyInducedDelete:
				return true;
			}
			return false;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0001AC5C File Offset: 0x00018E5C
		internal void Initialize()
		{
			if (WorkTypeManager.initialized)
			{
				return;
			}
			this.Add(WorkType.AggregationSubscriptionSaved, new WorkTypeDefinition(WorkType.AggregationSubscriptionSaved, ContentAggregationConfig.SyncNowTime, ContentAggregationConfig.AggregationSubscriptionSavedSyncWeight, true));
			this.Add(WorkType.AggregationIncremental, new WorkTypeDefinition(WorkType.AggregationIncremental, ContentAggregationConfig.AggregationIncrementalSyncInterval, ContentAggregationConfig.AggregationIncrementalSyncWeight, false));
			this.Add(WorkType.AggregationInitial, new WorkTypeDefinition(WorkType.AggregationInitial, ContentAggregationConfig.AggregationInitialSyncInterval, ContentAggregationConfig.AggregationInitialSyncWeight, false));
			this.Add(WorkType.MigrationInitial, new WorkTypeDefinition(WorkType.MigrationInitial, ContentAggregationConfig.MigrationInitialSyncInterval, ContentAggregationConfig.MigrationInitialSyncWeight, false));
			this.Add(WorkType.MigrationIncremental, new WorkTypeDefinition(WorkType.MigrationIncremental, ContentAggregationConfig.MigrationIncrementalSyncInterval, ContentAggregationConfig.MigrationIncrementalSyncWeight, false));
			this.Add(WorkType.MigrationFinalization, new WorkTypeDefinition(WorkType.MigrationFinalization, ContentAggregationConfig.MigrationInitialSyncInterval, ContentAggregationConfig.MigrationFinalizationSyncWeight, true));
			this.Add(WorkType.OwaLogonTriggeredSyncNow, new WorkTypeDefinition(WorkType.OwaLogonTriggeredSyncNow, ContentAggregationConfig.OwaTriggeredSyncNowTime, ContentAggregationConfig.OwaLogonTriggeredSyncWeight, true));
			this.Add(WorkType.OwaRefreshButtonTriggeredSyncNow, new WorkTypeDefinition(WorkType.OwaRefreshButtonTriggeredSyncNow, ContentAggregationConfig.OwaTriggeredSyncNowTime, ContentAggregationConfig.OwaRefreshButtonTriggeredSyncWeight, true));
			this.Add(WorkType.OwaActivityTriggeredSyncNow, new WorkTypeDefinition(WorkType.OwaActivityTriggeredSyncNow, ContentAggregationConfig.OwaTriggeredSyncNowTime, ContentAggregationConfig.OwaSessionTriggeredSyncWeight, true));
			this.Add(WorkType.PeopleConnectionInitial, new WorkTypeDefinition(WorkType.PeopleConnectionInitial, ContentAggregationConfig.PeopleConnectionInitialSyncInterval, ContentAggregationConfig.PeopleConnectionInitialSyncWeight, false));
			this.Add(WorkType.PeopleConnectionTriggered, new WorkTypeDefinition(WorkType.PeopleConnectionTriggered, ContentAggregationConfig.PeopleConnectionTriggeredSyncInterval, ContentAggregationConfig.PeopleConnectionTriggeredSyncWeight, true));
			this.Add(WorkType.PeopleConnectionIncremental, new WorkTypeDefinition(WorkType.PeopleConnectionIncremental, ContentAggregationConfig.PeopleConnectionIncrementalSyncInterval, ContentAggregationConfig.PeopleConnectionIncrementalSyncWeight, false));
			this.Add(WorkType.PolicyInducedDelete, new WorkTypeDefinition(WorkType.PolicyInducedDelete, ContentAggregationConfig.OwaMailboxPolicyInducedDeleteInterval, ContentAggregationConfig.OwaMailboxPolicyInducedDeleteWeight, false));
			byte b = 0;
			foreach (WorkTypeDefinition workTypeDefinition in this.workTypeDefinitions.Values)
			{
				b += workTypeDefinition.Weight;
			}
			if (b != 100)
			{
				throw new NotSupportedException("Total weight of all work types must equal 100");
			}
			WorkTypeManager.initialized = true;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0001AE18 File Offset: 0x00019018
		internal virtual WorkTypeDefinition GetWorkTypeDefinition(WorkType workType)
		{
			return this.workTypeDefinitions[workType];
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001AE26 File Offset: 0x00019026
		internal void Add(WorkType workType, WorkTypeDefinition workTypeDefinition)
		{
			this.workTypeDefinitions.Add(workType, workTypeDefinition);
		}

		// Token: 0x04000261 RID: 609
		private readonly Dictionary<WorkType, WorkTypeDefinition> workTypeDefinitions = new Dictionary<WorkType, WorkTypeDefinition>();

		// Token: 0x04000262 RID: 610
		private static bool initialized = false;

		// Token: 0x04000263 RID: 611
		private static WorkTypeManager workTypeManager = new WorkTypeManager();
	}
}
