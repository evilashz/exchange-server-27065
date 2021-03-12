using System;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200057C RID: 1404
	internal sealed class ActiveDirectoryServerSchema : ServerSchema
	{
		// Token: 0x06003F41 RID: 16193 RVA: 0x000F36C0 File Offset: 0x000F18C0
		internal static object IsServerStatesOnlineGetter(IPropertyBag propertyBag)
		{
			bool flag = false;
			if (propertyBag[ServerSchema.ComponentStates] != null)
			{
				MultiValuedProperty<string> componentStates = (MultiValuedProperty<string>)propertyBag[ServerSchema.ComponentStates];
				flag = ServerComponentStates.IsRemoteComponentOnlineAccordingToADInternal(componentStates, ServerComponentEnum.ServerWideOffline);
			}
			return flag;
		}

		// Token: 0x06003F42 RID: 16194 RVA: 0x000F36FC File Offset: 0x000F18FC
		internal static QueryFilter IsServerStatesOnlineFilterBuilder(SinglePropertyFilter filter)
		{
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			if (comparisonFilter == null || (comparisonFilter.ComparisonOperator != ComparisonOperator.Equal && comparisonFilter.ComparisonOperator != ComparisonOperator.NotEqual))
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
			}
			TextFilter textFilter = new TextFilter(ServerSchema.ComponentStates, "*:ServerWideOffline:*:0:*", MatchOptions.WildcardString, MatchFlags.IgnoreCase);
			TextFilter textFilter2 = new TextFilter(ServerSchema.ComponentStates, "*:ServerWideOffline:*:3:*", MatchOptions.WildcardString, MatchFlags.IgnoreCase);
			if ((comparisonFilter.ComparisonOperator == ComparisonOperator.Equal && (bool)comparisonFilter.PropertyValue) || (comparisonFilter.ComparisonOperator == ComparisonOperator.NotEqual && !(bool)comparisonFilter.PropertyValue))
			{
				return new NotFilter(new OrFilter(new QueryFilter[]
				{
					textFilter,
					textFilter2
				}));
			}
			return new OrFilter(new QueryFilter[]
			{
				textFilter,
				textFilter2
			});
		}

		// Token: 0x04002BE1 RID: 11233
		private const int MailTipsGenerateGroupMetricsShift = 0;

		// Token: 0x04002BE2 RID: 11234
		private const int MailTipsGenerationTimeSpecifiedShift = 1;

		// Token: 0x04002BE3 RID: 11235
		private const int MailTipsGenerationHourShift = 2;

		// Token: 0x04002BE4 RID: 11236
		private const int MailTipsGenerationHourLength = 5;

		// Token: 0x04002BE5 RID: 11237
		private const int MailTipsGenerationMinuteShift = 7;

		// Token: 0x04002BE6 RID: 11238
		private const int MailTipsGenerationMinuteLength = 6;

		// Token: 0x04002BE7 RID: 11239
		private const int ActivationConfig_AutoPolicy_Shift = 0;

		// Token: 0x04002BE8 RID: 11240
		private const int ActivationConfig_AutoPolicy_Length = 8;

		// Token: 0x04002BE9 RID: 11241
		public static readonly ADPropertyDefinition AssistantsThrottleWorkcycle = new ADPropertyDefinition("AssistantsThrottleWorkcycle", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchAssistantsThrottleWorkcycle", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002BEA RID: 11242
		public static readonly ADPropertyDefinition CalendarRepairWorkCycle = new ADPropertyDefinition("CalendarRepairWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.CalendarRepair), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.CalendarRepair), null, null);

		// Token: 0x04002BEB RID: 11243
		public static readonly ADPropertyDefinition CalendarRepairWorkCycleCheckpoint = new ADPropertyDefinition("CalendarRepairWorkCycleCheckpoint", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex.CalendarRepair), Server.AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex.CalendarRepair), null, null);

		// Token: 0x04002BEC RID: 11244
		public static readonly ADPropertyDefinition SharingPolicyWorkCycle = new ADPropertyDefinition("SharingPolicyWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.SharingPolicy), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.SharingPolicy), null, null);

		// Token: 0x04002BED RID: 11245
		public static readonly ADPropertyDefinition SharingPolicyWorkCycleCheckpoint = new ADPropertyDefinition("SharingPolicyWorkCycleCheckpoint", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex.SharingPolicy), Server.AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex.SharingPolicy), null, null);

		// Token: 0x04002BEE RID: 11246
		public static readonly ADPropertyDefinition PublicFolderWorkCycle = new ADPropertyDefinition("PublicFolderWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.PublicFolder), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.PublicFolder), null, null);

		// Token: 0x04002BEF RID: 11247
		public static readonly ADPropertyDefinition PublicFolderWorkCycleCheckpoint = new ADPropertyDefinition("PublicFolderWorkCycleCheckpoint", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex.PublicFolder), Server.AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex.PublicFolder), null, null);

		// Token: 0x04002BF0 RID: 11248
		public static readonly ADPropertyDefinition SiteMailboxWorkCycle = new ADPropertyDefinition("SiteMailboxWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.SiteMailbox), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.SiteMailbox), null, null);

		// Token: 0x04002BF1 RID: 11249
		public static readonly ADPropertyDefinition SiteMailboxWorkCycleCheckpoint = new ADPropertyDefinition("SiteMailboxWorkCycleCheckpoint", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex.SiteMailbox), Server.AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex.SiteMailbox), null, null);

		// Token: 0x04002BF2 RID: 11250
		public static readonly ADPropertyDefinition SharingSyncWorkCycle = new ADPropertyDefinition("SharingSyncWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.SharingSync), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.SharingSync), null, null);

		// Token: 0x04002BF3 RID: 11251
		public static readonly ADPropertyDefinition SharingSyncWorkCycleCheckpoint = new ADPropertyDefinition("SharingSyncWorkCycleCheckpoint", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex.SharingSync), Server.AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex.SharingSync), null, null);

		// Token: 0x04002BF4 RID: 11252
		public static readonly ADPropertyDefinition ManagedFolderWorkCycle = new ADPropertyDefinition("ManagedFolderWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.ManagedFolder), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.ManagedFolder), null, null);

		// Token: 0x04002BF5 RID: 11253
		public static readonly ADPropertyDefinition ManagedFolderWorkCycleCheckpoint = new ADPropertyDefinition("ManagedFolderWorkCycleCheckpoint", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex.ManagedFolder), Server.AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex.ManagedFolder), null, null);

		// Token: 0x04002BF6 RID: 11254
		public static readonly ADPropertyDefinition MailboxAssociationReplicationWorkCycle = new ADPropertyDefinition("MailboxAssociationReplicationWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.MailboxAssociationReplication), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.MailboxAssociationReplication), null, null);

		// Token: 0x04002BF7 RID: 11255
		public static readonly ADPropertyDefinition MailboxAssociationReplicationWorkCycleCheckpoint = new ADPropertyDefinition("MailboxAssociationReplicationWorkCycleCheckpoint", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex.MailboxAssociationReplication), Server.AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex.MailboxAssociationReplication), null, null);

		// Token: 0x04002BF8 RID: 11256
		public static readonly ADPropertyDefinition GroupMailboxWorkCycle = new ADPropertyDefinition("GroupMailboxWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.GroupMailbox), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.GroupMailbox), null, null);

		// Token: 0x04002BF9 RID: 11257
		public static readonly ADPropertyDefinition GroupMailboxWorkCycleCheckpoint = new ADPropertyDefinition("GroupMailboxWorkCycleCheckpoint", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex.GroupMailbox), Server.AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex.GroupMailbox), null, null);

		// Token: 0x04002BFA RID: 11258
		public static readonly ADPropertyDefinition TopNWorkCycle = new ADPropertyDefinition("TopNWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.TopN), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.TopN), null, null);

		// Token: 0x04002BFB RID: 11259
		public static readonly ADPropertyDefinition TopNWorkCycleCheckpoint = new ADPropertyDefinition("TopNWorkCycleCheckpoint", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex.TopN), Server.AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex.TopN), null, null);

		// Token: 0x04002BFC RID: 11260
		public static readonly ADPropertyDefinition UMReportingWorkCycle = new ADPropertyDefinition("UMReportingWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.UMReporting), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.UMReporting), null, null);

		// Token: 0x04002BFD RID: 11261
		public static readonly ADPropertyDefinition UMReportingWorkCycleCheckpoint = new ADPropertyDefinition("UMReportingWorkCycleCheckpoint", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex.UMReporting), Server.AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex.UMReporting), null, null);

		// Token: 0x04002BFE RID: 11262
		public static readonly ADPropertyDefinition InferenceTrainingWorkCycle = new ADPropertyDefinition("InferenceTrainingWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.InferenceTraining), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.InferenceTraining), null, null);

		// Token: 0x04002BFF RID: 11263
		public static readonly ADPropertyDefinition InferenceTrainingWorkCycleCheckpoint = new ADPropertyDefinition("InferenceTrainingWorkCycleCheckpoint", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex.InferenceTraining), Server.AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex.InferenceTraining), null, null);

		// Token: 0x04002C00 RID: 11264
		public static readonly ADPropertyDefinition DirectoryProcessorWorkCycle = new ADPropertyDefinition("DirectoryProcessorWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.DirectoryProcessor), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.DirectoryProcessor), null, null);

		// Token: 0x04002C01 RID: 11265
		public static readonly ADPropertyDefinition DirectoryProcessorWorkCycleCheckpoint = new ADPropertyDefinition("DirectoryProcessorWorkCycleCheckpoint", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex.DirectoryProcessor), Server.AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex.DirectoryProcessor), null, null);

		// Token: 0x04002C02 RID: 11266
		public static readonly ADPropertyDefinition OABGeneratorWorkCycle = new ADPropertyDefinition("OABGeneratorWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.OABGenerator), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.OABGenerator), null, null);

		// Token: 0x04002C03 RID: 11267
		public static readonly ADPropertyDefinition OABGeneratorWorkCycleCheckpoint = new ADPropertyDefinition("OABGeneratorWorkCycleCheckpoint", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex.OABGenerator), Server.AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex.OABGenerator), null, null);

		// Token: 0x04002C04 RID: 11268
		public static readonly ADPropertyDefinition InferenceDataCollectionWorkCycle = new ADPropertyDefinition("InferenceDataCollectionWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.InferenceDataCollection), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.InferenceDataCollection), null, null);

		// Token: 0x04002C05 RID: 11269
		public static readonly ADPropertyDefinition InferenceDataCollectionWorkCycleCheckpoint = new ADPropertyDefinition("InferenceDataCollectionWorkCycleCheckpoint", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex.InferenceDataCollection), Server.AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex.InferenceDataCollection), null, null);

		// Token: 0x04002C06 RID: 11270
		public static readonly ADPropertyDefinition PeopleRelevanceWorkCycle = new ADPropertyDefinition("PeopleRelevanceWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.PeopleRelevance), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.PeopleRelevance), null, null);

		// Token: 0x04002C07 RID: 11271
		public static readonly ADPropertyDefinition PeopleRelevanceWorkCycleCheckpoint = new ADPropertyDefinition("PeopleRelevanceWorkCycleCheckpoint", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex.PeopleRelevance), Server.AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex.PeopleRelevance), null, null);

		// Token: 0x04002C08 RID: 11272
		public static readonly ADPropertyDefinition SharePointSignalStoreWorkCycle = new ADPropertyDefinition("SharePointSignalStoreWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.SharePointSignalStore), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.SharePointSignalStore), null, null);

		// Token: 0x04002C09 RID: 11273
		public static readonly ADPropertyDefinition SharePointSignalStoreWorkCycleCheckpoint = new ADPropertyDefinition("SharePointSignalStoreWorkCycleCheckpoint", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex.SharePointSignalStore), Server.AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex.SharePointSignalStore), null, null);

		// Token: 0x04002C0A RID: 11274
		public static readonly ADPropertyDefinition PeopleCentricTriageWorkCycle = new ADPropertyDefinition("PeopleCentricTriageWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.PeopleCentricTriage), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.PeopleCentricTriage), null, null);

		// Token: 0x04002C0B RID: 11275
		public static readonly ADPropertyDefinition PeopleCentricTriageWorkCycleCheckpoint = new ADPropertyDefinition("PeopleCentricTriageWorkCycleCheckpoint", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex.PeopleCentricTriage), Server.AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex.PeopleCentricTriage), null, null);

		// Token: 0x04002C0C RID: 11276
		public static readonly ADPropertyDefinition MailboxProcessorWorkCycle = new ADPropertyDefinition("MailboxProcessorWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.MailboxProcessor), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.MailboxProcessor), null, null);

		// Token: 0x04002C0D RID: 11277
		public static readonly ADPropertyDefinition StoreDsMaintenanceWorkCycle = new ADPropertyDefinition("StoreDsMaintenanceWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.StoreDsMaintenance), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.StoreDsMaintenance), null, null);

		// Token: 0x04002C0E RID: 11278
		public static readonly ADPropertyDefinition StoreDsMaintenanceWorkCycleCheckpoint = new ADPropertyDefinition("StoreDsMaintenanceWorkCycleCheckpoint", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex.StoreDsMaintenance), Server.AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex.StoreDsMaintenance), null, null);

		// Token: 0x04002C0F RID: 11279
		public static readonly ADPropertyDefinition StoreIntegrityCheckWorkCycle = new ADPropertyDefinition("StoreIntegrityCheckWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.StoreIntegrityCheck), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.StoreIntegrityCheck), null, null);

		// Token: 0x04002C10 RID: 11280
		public static readonly ADPropertyDefinition StoreIntegrityCheckWorkCycleCheckpoint = new ADPropertyDefinition("StoreIntegrityCheckWorkCycleCheckpoint", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex.StoreIntegrityCheck), Server.AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex.StoreIntegrityCheck), null, null);

		// Token: 0x04002C11 RID: 11281
		public static readonly ADPropertyDefinition StoreMaintenanceWorkCycle = new ADPropertyDefinition("StoreMaintenanceWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.StoreMaintenance), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.StoreMaintenance), null, null);

		// Token: 0x04002C12 RID: 11282
		public static readonly ADPropertyDefinition StoreMaintenanceWorkCycleCheckpoint = new ADPropertyDefinition("StoreMaintenanceWorkCycleCheckpoint", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex.StoreMaintenance), Server.AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex.StoreMaintenance), null, null);

		// Token: 0x04002C13 RID: 11283
		public static readonly ADPropertyDefinition StoreScheduledIntegrityCheckWorkCycle = new ADPropertyDefinition("StoreScheduledIntegrityCheckWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.StoreScheduledIntegrityCheck), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.StoreScheduledIntegrityCheck), null, null);

		// Token: 0x04002C14 RID: 11284
		public static readonly ADPropertyDefinition StoreScheduledIntegrityCheckWorkCycleCheckpoint = new ADPropertyDefinition("StoreScheduledIntegrityCheckWorkCycleCheckpoint", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex.StoreScheduledIntegrityCheck), Server.AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex.StoreScheduledIntegrityCheck), null, null);

		// Token: 0x04002C15 RID: 11285
		public static readonly ADPropertyDefinition StoreUrgentMaintenanceWorkCycle = new ADPropertyDefinition("StoreUrgentMaintenanceWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.StoreUrgentMaintenance), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.StoreUrgentMaintenance), null, null);

		// Token: 0x04002C16 RID: 11286
		public static readonly ADPropertyDefinition StoreUrgentMaintenanceWorkCycleCheckpoint = new ADPropertyDefinition("StoreUrgentMaintenanceWorkCycleCheckpoint", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex.StoreUrgentMaintenance), Server.AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex.StoreUrgentMaintenance), null, null);

		// Token: 0x04002C17 RID: 11287
		public static readonly ADPropertyDefinition JunkEmailOptionsCommitterWorkCycle = new ADPropertyDefinition("JunkEmailOptionsCommitterWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.JunkEmailOptionsCommitter), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.JunkEmailOptionsCommitter), null, null);

		// Token: 0x04002C18 RID: 11288
		public static readonly ADPropertyDefinition ProbeTimeBasedAssistantWorkCycle = new ADPropertyDefinition("ProbeTimeBaseAssistantdWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.ProbeTimeBasedAssistant), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.ProbeTimeBasedAssistant), null, null);

		// Token: 0x04002C19 RID: 11289
		public static readonly ADPropertyDefinition ProbeTimeBasedAssistantWorkCycleCheckpoint = new ADPropertyDefinition("ProbeTimeBasedAssistantWorkCycleCheckpoint", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex.ProbeTimeBasedAssistant), Server.AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex.ProbeTimeBasedAssistant), null, null);

		// Token: 0x04002C1A RID: 11290
		public static readonly ADPropertyDefinition SearchIndexRepairTimeBasedAssistantWorkCycle = new ADPropertyDefinition("SearchIndexRepairTimeBasedAssistantWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.SearchIndexRepairTimeBasedAssistant), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.SearchIndexRepairTimeBasedAssistant), null, null);

		// Token: 0x04002C1B RID: 11291
		public static readonly ADPropertyDefinition SearchIndexRepairTimeBasedAssistantWorkCycleCheckpoint = new ADPropertyDefinition("SearchIndexRepairTimeBasedAssistantWorkCycleCheckpoint", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex.SearchIndexRepairTimeBasedAssistant), Server.AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex.SearchIndexRepairTimeBasedAssistant), null, null);

		// Token: 0x04002C1C RID: 11292
		public static readonly ADPropertyDefinition DarTaskStoreTimeBasedAssistantWorkCycle = new ADPropertyDefinition("DarTaskStoreTimeBasedAssistantWorkCycle", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex.DarTaskStoreTimeBasedAssistant), Server.AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex.DarTaskStoreTimeBasedAssistant), null, null);

		// Token: 0x04002C1D RID: 11293
		public static readonly ADPropertyDefinition DarTaskStoreTimeBasedAssistantWorkCycleCheckpoint = new ADPropertyDefinition("DarTaskStoreTimeBasedAssistantWorkCycleCheckpoint", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle
		}, null, Server.AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex.DarTaskStoreTimeBasedAssistant), Server.AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex.DarTaskStoreTimeBasedAssistant), null, null);

		// Token: 0x04002C1E RID: 11294
		public static readonly ADPropertyDefinition AssistantMaintenanceScheduleInternal = new ADPropertyDefinition("AssistantMaintenanceScheduleInternal", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchAssistantsMaintenanceSchedule", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C1F RID: 11295
		public static readonly ADPropertyDefinition SharingPolicySchedule = new ADPropertyDefinition("SharingPolicySchedule", ExchangeObjectVersion.Exchange2007, typeof(ScheduleInterval[]), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AssistantMaintenanceScheduleInternal
		}, null, Server.AssistantMaintenanceScheduleGetterDelegate(ActiveDirectoryServerSchema.AssistantMaintenanceScheduleInternal, ScheduledAssistant.SharingPolicy), Server.AssistantMaintenanceScheduleSetterDelegate(ActiveDirectoryServerSchema.AssistantMaintenanceScheduleInternal, ScheduledAssistant.SharingPolicy), null, null);

		// Token: 0x04002C20 RID: 11296
		public static readonly ADPropertyDefinition CalendarRepairFlagsInternal = new ADPropertyDefinition("CalendarRepairFlagsInternal", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchCalendarRepairFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 11, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C21 RID: 11297
		public static readonly ADPropertyDefinition CalendarRepairMissingItemFixDisabled = new ADPropertyDefinition("CalendarRepairMissingItemFixDisabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.CalendarRepairFlagsInternal
		}, null, ADObject.FlagGetterDelegate(ActiveDirectoryServerSchema.CalendarRepairFlagsInternal, 4), ADObject.FlagSetterDelegate(ActiveDirectoryServerSchema.CalendarRepairFlagsInternal, 4), null, null);

		// Token: 0x04002C22 RID: 11298
		public static readonly ADPropertyDefinition CalendarRepairLogEnabled = new ADPropertyDefinition("CalendarRepairLogEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.CalendarRepairFlagsInternal
		}, null, ADObject.FlagGetterDelegate(ActiveDirectoryServerSchema.CalendarRepairFlagsInternal, 1), ADObject.FlagSetterDelegate(ActiveDirectoryServerSchema.CalendarRepairFlagsInternal, 1), null, null);

		// Token: 0x04002C23 RID: 11299
		public static readonly ADPropertyDefinition CalendarRepairLogSubjectLoggingEnabled = new ADPropertyDefinition("CalendarRepairLogSubjectLoggingEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.CalendarRepairFlagsInternal
		}, null, ADObject.FlagGetterDelegate(ActiveDirectoryServerSchema.CalendarRepairFlagsInternal, 2), ADObject.FlagSetterDelegate(ActiveDirectoryServerSchema.CalendarRepairFlagsInternal, 2), null, null);

		// Token: 0x04002C24 RID: 11300
		public static readonly ADPropertyDefinition CalendarRepairLogPath = new ADPropertyDefinition("CalendarRepairLogPath", ExchangeObjectVersion.Exchange2007, typeof(LocalLongFullPath), "msExchCalendarRepairLogPath", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C25 RID: 11301
		public static readonly ADPropertyDefinition CalendarRepairIntervalEndWindow = new ADPropertyDefinition("CalendarRepairIntervalEndWindow", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchCalendarRepairIntervalEndWindow", ADPropertyDefinitionFlags.PersistDefaultValue, 30, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C26 RID: 11302
		public static readonly ADPropertyDefinition CalendarRepairLogFileAgeLimit = new ADPropertyDefinition("CalendarRepairLogFileAgeLimit", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchCalendarRepairLogFileAgeLimit", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(10.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneHour)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C27 RID: 11303
		public static readonly ADPropertyDefinition CalendarRepairLogDirectorySizeLimit = new ADPropertyDefinition("CalendarRepairLogDirectorySizeLimit", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), "msExchCalendarRepairLogFileSizeLimit", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C28 RID: 11304
		public static readonly ADPropertyDefinition CalendarRepairMode = new ADPropertyDefinition("CalendarRepairMode", ExchangeObjectVersion.Exchange2007, typeof(CalendarRepairType), null, ADPropertyDefinitionFlags.Calculated, CalendarRepairType.RepairAndValidate, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.CalendarRepairFlagsInternal
		}, null, delegate(IPropertyBag bag)
		{
			int num = (int)bag[ActiveDirectoryServerSchema.CalendarRepairFlagsInternal];
			return ((num & 8) > 0) ? CalendarRepairType.RepairAndValidate : CalendarRepairType.ValidateOnly;
		}, delegate(object value, IPropertyBag bag)
		{
			int num = 8;
			int num2 = (int)bag[ActiveDirectoryServerSchema.CalendarRepairFlagsInternal];
			bag[ActiveDirectoryServerSchema.CalendarRepairFlagsInternal] = (CalendarRepairType.RepairAndValidate.Equals(value) ? (num2 | num) : (num2 & ~num));
		}, null, null);

		// Token: 0x04002C29 RID: 11305
		public static readonly ADPropertyDefinition TransportSyncAccountsSuccessivePoisonItemThreshold = new ADPropertyDefinition("TransportSyncAccountsSuccessivePoisonItemThreshold", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSyncAccountsSuccessivePoisonItemsThreshold", ADPropertyDefinitionFlags.PersistDefaultValue, 3, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, int.MaxValue)
		}, null, null);

		// Token: 0x04002C2A RID: 11306
		public static readonly ADPropertyDefinition TransportSyncHubHealthLogEnabled = new ADPropertyDefinition("TransportSyncHubHealthLogEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportSyncServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportSyncServerFlags, 2), ADObject.FlagSetterDelegate(ServerSchema.TransportSyncServerFlags, 2), null, null);

		// Token: 0x04002C2B RID: 11307
		public static readonly ADPropertyDefinition TransportSyncHubHealthLogMaxFileSize = new ADPropertyDefinition("TransportSyncHubHealthLogMaxFileSize", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), ByteQuantifiedSize.KilobyteQuantifierProvider, "msExchSyncHubHealthLogPerFileSizeQuota", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromKB(10240UL), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C2C RID: 11308
		public static readonly ADPropertyDefinition TransportSyncHubHealthLogMaxAge = new ADPropertyDefinition("TransportSyncHubHealthLogMaxAge", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchSyncHubHealthLogAgeQuotaInHours", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(30.0), PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.MaxValue),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, null, null);

		// Token: 0x04002C2D RID: 11309
		public static readonly ADPropertyDefinition TransportSyncHubHealthLogMaxDirectorySize = new ADPropertyDefinition("TransportSyncHubHealthLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), ByteQuantifiedSize.KilobyteQuantifierProvider, "msExchSyncHubHealthLogDirectorySizeQuota", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromGB(10UL), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C2E RID: 11310
		public static readonly ADPropertyDefinition TransportSyncHubHealthLogFilePath = new ADPropertyDefinition("TransportSyncHubHealthLogFilePath", ExchangeObjectVersion.Exchange2007, typeof(LocalLongFullPath), "msExchSyncHubHealthLogFilePath", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint
		}, null, null);

		// Token: 0x04002C2F RID: 11311
		public static readonly ADPropertyDefinition TransportSyncMailboxHealthLogEnabled = new ADPropertyDefinition("TransportSyncMailboxHealthLogEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportSyncServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportSyncServerFlags, 4096), ADObject.FlagSetterDelegate(ServerSchema.TransportSyncServerFlags, 4096), null, null);

		// Token: 0x04002C30 RID: 11312
		public static readonly ADPropertyDefinition TransportSyncMailboxHealthLogMaxFileSize = new ADPropertyDefinition("TransportSyncMailboxHealthLogMaxFileSize", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), ByteQuantifiedSize.KilobyteQuantifierProvider, "msExchSyncMailboxHealthLogPerFileSizeQuota", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromKB(10240UL), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C31 RID: 11313
		public static readonly ADPropertyDefinition TransportSyncMailboxHealthLogMaxAge = new ADPropertyDefinition("TransportSyncMailboxHealthLogMaxAge", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchSyncMailboxHealthLogAgeQuotaInHours", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(30.0), PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.MaxValue),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, null, null);

		// Token: 0x04002C32 RID: 11314
		public static readonly ADPropertyDefinition TransportSyncMailboxHealthLogMaxDirectorySize = new ADPropertyDefinition("TransportSyncMailboxHealthLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), ByteQuantifiedSize.KilobyteQuantifierProvider, "msExchSyncMailboxHealthLogDirectorySizeQuota", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromGB(10UL), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C33 RID: 11315
		public static readonly ADPropertyDefinition TransportSyncMailboxHealthLogFilePath = new ADPropertyDefinition("TransportSyncMailboxHealthLogFilePath", ExchangeObjectVersion.Exchange2007, typeof(LocalLongFullPath), "msExchSyncMailboxHealthLogFilePath", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint
		}, null, null);

		// Token: 0x04002C34 RID: 11316
		public static readonly ADPropertyDefinition MigrationLogExtensionData = new ADPropertyDefinition("MigrationLogExtensionData", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchMigrationLogExtensionData", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C35 RID: 11317
		public static readonly ADPropertyDefinition MigrationLogFilePath = new ADPropertyDefinition("MigrationLogLogFilePath", ExchangeObjectVersion.Exchange2007, typeof(LocalLongFullPath), "msExchMigrationLogLogFilePath", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint
		}, null, null);

		// Token: 0x04002C36 RID: 11318
		public static readonly ADPropertyDefinition MigrationLogLoggingLevel = new ADPropertyDefinition("MigrationLogLoggingLevel", ExchangeObjectVersion.Exchange2007, typeof(MigrationEventType), "msExchMigrationLogLoggingLevel", ADPropertyDefinitionFlags.PersistDefaultValue, MigrationEventType.Information, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<MigrationEventType>(MigrationEventType.None, (MigrationEventType)2147483647)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C37 RID: 11319
		public static readonly ADPropertyDefinition MigrationLogMaxAge = new ADPropertyDefinition("MigrationLogMaxAge", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchMigrationLogAgeQuotaInHours", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(180.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.MaxValue),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C38 RID: 11320
		public static readonly ADPropertyDefinition MigrationLogMaxDirectorySize = new ADPropertyDefinition("MigrationLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), "msExchMigrationLogDirectorySizeQuotaLarge", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromGB(5UL), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C39 RID: 11321
		public static readonly ADPropertyDefinition MigrationLogMaxFileSize = new ADPropertyDefinition("MigrationLogMaxFileSize", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), "msExchMigrationLogPerFileSizeQuota", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromMB(100UL), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C3A RID: 11322
		public static readonly ADPropertyDefinition AutoDatabaseMountDialType = new ADPropertyDefinition("AutoDatabaseMountDialType", ExchangeObjectVersion.Exchange2007, typeof(AutoDatabaseMountDial), "msExchDataLossForAutoDatabaseMount", ADPropertyDefinitionFlags.PersistDefaultValue, AutoDatabaseMountDial.GoodAvailability, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C3B RID: 11323
		public static readonly ADPropertyDefinition ElcAuditLogPath = new ADPropertyDefinition("ElcAuditLogPath", ExchangeObjectVersion.Exchange2007, typeof(LocalLongFullPath), "msExchELCAuditLogPath", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint
		}, null, null);

		// Token: 0x04002C3C RID: 11324
		public static readonly ADPropertyDefinition ElcAuditLogFileAgeLimit = new ADPropertyDefinition("ElcAuditLogFileAgeLimit", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchELCAuditLogFileAgeLimit", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(0.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C3D RID: 11325
		public static readonly ADPropertyDefinition ElcAuditLogDirectorySizeLimit = new ADPropertyDefinition("ElcAuditLogDirectorySizeLimit", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), "msExchELCAuditLogDirectorySizeLimit", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C3E RID: 11326
		public static readonly ADPropertyDefinition ElcAuditLogFileSizeLimit = new ADPropertyDefinition("ElcAuditLogFileSizeLimit", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), "msExchELCAuditLogFileSizeLimit", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C3F RID: 11327
		public static readonly ADPropertyDefinition MailboxRoleFlags = new ADPropertyDefinition("MailboxRoleFlags", ExchangeObjectVersion.Exchange2007, typeof(MailboxServerRoleFlags), "msExchMailboxRoleFlags", ADPropertyDefinitionFlags.None, MailboxServerRoleFlags.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C40 RID: 11328
		public static readonly ADPropertyDefinition MAPIEncryptionRequired = new ADPropertyDefinition("MAPIEncryptionRequired", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.MailboxRoleFlags
		}, null, Server.MailboxRoleFlagsGetter(ActiveDirectoryServerSchema.MailboxRoleFlags, MailboxServerRoleFlags.MAPIEncryptionRequired), Server.MailboxRoleFlagsSetter(ActiveDirectoryServerSchema.MailboxRoleFlags, MailboxServerRoleFlags.MAPIEncryptionRequired), null, null);

		// Token: 0x04002C41 RID: 11329
		public static readonly ADPropertyDefinition ExpirationAuditLogEnabled = new ADPropertyDefinition("ExpirationAuditLogEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.MailboxRoleFlags
		}, null, Server.MailboxRoleFlagsGetter(ActiveDirectoryServerSchema.MailboxRoleFlags, MailboxServerRoleFlags.ExpirationAuditLogEnabled), Server.MailboxRoleFlagsSetter(ActiveDirectoryServerSchema.MailboxRoleFlags, MailboxServerRoleFlags.ExpirationAuditLogEnabled), null, null);

		// Token: 0x04002C42 RID: 11330
		public static readonly ADPropertyDefinition AutocopyAuditLogEnabled = new ADPropertyDefinition("AutocopyAuditLogEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.MailboxRoleFlags
		}, null, Server.MailboxRoleFlagsGetter(ActiveDirectoryServerSchema.MailboxRoleFlags, MailboxServerRoleFlags.AutocopyAuditLogEnabled), Server.MailboxRoleFlagsSetter(ActiveDirectoryServerSchema.MailboxRoleFlags, MailboxServerRoleFlags.AutocopyAuditLogEnabled), null, null);

		// Token: 0x04002C43 RID: 11331
		public static readonly ADPropertyDefinition FolderAuditLogEnabled = new ADPropertyDefinition("FolderAuditLogEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.MailboxRoleFlags
		}, null, Server.MailboxRoleFlagsGetter(ActiveDirectoryServerSchema.MailboxRoleFlags, MailboxServerRoleFlags.FolderAuditLogEnabled), Server.MailboxRoleFlagsSetter(ActiveDirectoryServerSchema.MailboxRoleFlags, MailboxServerRoleFlags.FolderAuditLogEnabled), null, null);

		// Token: 0x04002C44 RID: 11332
		public static readonly ADPropertyDefinition ElcSubjectLoggingEnabled = new ADPropertyDefinition("ElcSubjectLoggingEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.MailboxRoleFlags
		}, null, Server.MailboxRoleFlagsGetter(ActiveDirectoryServerSchema.MailboxRoleFlags, MailboxServerRoleFlags.ElcSubjectLoggingEnabled), Server.MailboxRoleFlagsSetter(ActiveDirectoryServerSchema.MailboxRoleFlags, MailboxServerRoleFlags.ElcSubjectLoggingEnabled), null, null);

		// Token: 0x04002C45 RID: 11333
		public static readonly ADPropertyDefinition EdgeSyncAdamSslPort = new ADPropertyDefinition("EdgeSyncAdamSslPort", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchEdgeSyncAdamSSLPort", ADPropertyDefinitionFlags.PersistDefaultValue, 50636, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 65535)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C46 RID: 11334
		public new static readonly ADPropertyDefinition SystemFlags = new ADPropertyDefinition("SystemFlags", ExchangeObjectVersion.Exchange2003, typeof(SystemFlagsEnum), "systemFlags", ADPropertyDefinitionFlags.PersistDefaultValue, SystemFlagsEnum.DeleteImmediately | SystemFlagsEnum.Renamable, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C47 RID: 11335
		public static readonly ADPropertyDefinition MaxConcurrentMailboxSubmissions = new ADPropertyDefinition("MaxConcurrentMailboxSubmissions", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchTransportMaxConcurrentMailboxSubmissions", ADPropertyDefinitionFlags.PersistDefaultValue, 20, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, 256)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C48 RID: 11336
		public static readonly ADPropertyDefinition MailTipsSettings = new ADPropertyDefinition("MailTipsSettings", ExchangeObjectVersion.Exchange2003, typeof(long), "msExchMailTipsSettings", ADPropertyDefinitionFlags.PersistDefaultValue, 0L, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C49 RID: 11337
		public static readonly ADPropertyDefinition ForceGroupMetricsGeneration = ADObject.BitfieldProperty("ForceGroupMetricsGeneration", 0, ActiveDirectoryServerSchema.MailTipsSettings);

		// Token: 0x04002C4A RID: 11338
		public static readonly ADPropertyDefinition SipTcpListeningPort = new ADPropertyDefinition("SipTcpListeningPort", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchUMTcpListeningPort", ADPropertyDefinitionFlags.PersistDefaultValue, 5062, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, 65535)
		}, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, 65535)
		}, null, null);

		// Token: 0x04002C4B RID: 11339
		public static readonly ADPropertyDefinition SipTlsListeningPort = new ADPropertyDefinition("SipTlsListeningPort", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchUMTlsListeningPort", ADPropertyDefinitionFlags.PersistDefaultValue, 5063, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, 65535)
		}, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, 65535)
		}, null, null);

		// Token: 0x04002C4C RID: 11340
		public static readonly ADPropertyDefinition ExternalServiceFqdn = new ADPropertyDefinition("ExternalServiceFqdn", ExchangeObjectVersion.Exchange2007, typeof(UMSmartHost), "msExchUMLoadBalancerFqdn", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C4D RID: 11341
		public static readonly ADPropertyDefinition UMServerSet = new ADPropertyDefinition("UMServerSet", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchUMEnabledFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 7, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C4E RID: 11342
		public static readonly ADPropertyDefinition IPAddressFamilyConfigurable = new ADPropertyDefinition("IPAddressFamilyConfigurable", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.UMServerSet
		}, null, ADObject.FlagGetterDelegate(1, ActiveDirectoryServerSchema.UMServerSet), ADObject.FlagSetterDelegate(1, ActiveDirectoryServerSchema.UMServerSet), null, null);

		// Token: 0x04002C4F RID: 11343
		public static readonly ADPropertyDefinition IPAddressFamily = new ADPropertyDefinition("IPAddressFamily", ExchangeObjectVersion.Exchange2007, typeof(IPAddressFamily), null, ADPropertyDefinitionFlags.Calculated, Microsoft.Exchange.Data.Directory.IPAddressFamily.IPv4Only, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.UMServerSet
		}, null, new GetterDelegate(Server.IPAddressFamilyGetter), new SetterDelegate(Server.IPAddressFamilySetter), null, null);

		// Token: 0x04002C50 RID: 11344
		public static readonly ADPropertyDefinition ProvisioningFlags = SharedPropertyDefinitions.ProvisioningFlags;

		// Token: 0x04002C51 RID: 11345
		public static readonly ADPropertyDefinition IsExcludedFromProvisioning = new ADPropertyDefinition("IsExcludedFromProvisioning", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.ProvisioningFlags
		}, new CustomFilterBuilderDelegate(MailboxDatabase.IsExcludedFromProvisioningFilterBuilder), ADObject.FlagGetterDelegate(ActiveDirectoryServerSchema.ProvisioningFlags, 2), ADObject.FlagSetterDelegate(ActiveDirectoryServerSchema.ProvisioningFlags, 2), null, null);

		// Token: 0x04002C52 RID: 11346
		public static readonly ADPropertyDefinition IsOutOfService = SharedPropertyDefinitions.IsOutOfService;

		// Token: 0x04002C53 RID: 11347
		public static readonly ADPropertyDefinition UseDowngradedExchangeServerAuth = new ADPropertyDefinition("UseDowngradedExchangeServerAuth", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportServerFlags, 2097152), ADObject.FlagSetterDelegate(ServerSchema.TransportServerFlags, 2097152), null, null);

		// Token: 0x04002C54 RID: 11348
		public static readonly ADPropertyDefinition AreServerStatesOnline = new ADPropertyDefinition("IsServerStatesOnline", ExchangeObjectVersion.Exchange2012, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.ComponentStates
		}, new CustomFilterBuilderDelegate(ActiveDirectoryServerSchema.IsServerStatesOnlineFilterBuilder), new GetterDelegate(ActiveDirectoryServerSchema.IsServerStatesOnlineGetter), null, null, null);

		// Token: 0x04002C55 RID: 11349
		public static readonly ADPropertyDefinition HostedDatabaseCopies = new ADPropertyDefinition("HostedDatabaseCopies", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchHostServerBL", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.DoNotProvisionalClone | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C56 RID: 11350
		public static readonly ADPropertyDefinition ContinuousReplicationMaxMemoryPerDatabase = new ADPropertyDefinition("ContinuousReplicationMaxMemoryPerDatabase", ExchangeObjectVersion.Exchange2007, typeof(long?), "msExchContinuousReplicationMaxMemoryPerMDB", ADPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C57 RID: 11351
		public static readonly ADPropertyDefinition AutoDagFlags = new ADPropertyDefinition("AutoDagFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchAutoDAGParamServerFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C58 RID: 11352
		public static readonly ADPropertyDefinition DatabaseCopyLocationAgilityDisabled = new ADPropertyDefinition("DatabaseCopyLocationAgilityDisabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AutoDagFlags
		}, null, ADObject.FlagGetterDelegate(ActiveDirectoryServerSchema.AutoDagFlags, 1), ADObject.FlagSetterDelegate(ActiveDirectoryServerSchema.AutoDagFlags, 1), null, null);

		// Token: 0x04002C59 RID: 11353
		public static readonly ADPropertyDefinition DatabaseCopyActivationDisabledAndMoveNow = new ADPropertyDefinition("DatabaseCopyActivationDisabledAndMoveNow", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AutoDagFlags
		}, null, ADObject.FlagGetterDelegate(ActiveDirectoryServerSchema.AutoDagFlags, 2), ADObject.FlagSetterDelegate(ActiveDirectoryServerSchema.AutoDagFlags, 2), null, null);

		// Token: 0x04002C5A RID: 11354
		public static readonly ADPropertyDefinition AutoDagServerConfigured = new ADPropertyDefinition("AutoDagServerConfigured", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ActiveDirectoryServerSchema.AutoDagFlags
		}, null, ADObject.FlagGetterDelegate(ActiveDirectoryServerSchema.AutoDagFlags, 4), ADObject.FlagSetterDelegate(ActiveDirectoryServerSchema.AutoDagFlags, 4), null, null);

		// Token: 0x04002C5B RID: 11355
		public static readonly ADPropertyDefinition ActivationConfig = new ADPropertyDefinition("ActivationConfig", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchActivationConfig", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002C5C RID: 11356
		public static readonly ADPropertyDefinition DatabaseCopyAutoActivationPolicy = ADObject.BitfieldProperty("DatabaseCopyAutoActivationPolicy", 0, 8, ActiveDirectoryServerSchema.ActivationConfig);

		// Token: 0x04002C5D RID: 11357
		public static readonly ADPropertyDefinition FaultZone = new ADPropertyDefinition("FaultZone", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchServerFaultZone", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new NoLeadingOrTrailingWhitespaceConstraint(),
			new RegexConstraint("^[^`~!@#&\\^\\(\\)\\+\\[\\]\\{\\}\\<\\>\\?=,:|./\\\\;]*$", DataStrings.NameValidationSpaceAllowedPatternDescription)
		}, null, null);

		// Token: 0x04002C5E RID: 11358
		public static readonly ADPropertyDefinition MailboxRelease = SharedPropertyDefinitions.MailboxRelease;

		// Token: 0x0200057D RID: 1405
		[Flags]
		private enum CalendarRepairOptions
		{
			// Token: 0x04002C62 RID: 11362
			None = 0,
			// Token: 0x04002C63 RID: 11363
			LogEnabled = 1,
			// Token: 0x04002C64 RID: 11364
			SubjectLoggingEnabled = 2,
			// Token: 0x04002C65 RID: 11365
			MissingItemFixDisabled = 4,
			// Token: 0x04002C66 RID: 11366
			CalendarRepairMode = 8
		}
	}
}
