using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005D9 RID: 1497
	internal sealed class TenantRelocationRequestSchema : ADLegacyVersionableObjectSchema
	{
		// Token: 0x170016C5 RID: 5829
		// (get) Token: 0x06004509 RID: 17673 RVA: 0x00100E50 File Offset: 0x000FF050
		internal static IList<ADPropertyDefinition> RelocationSpecificProperties
		{
			get
			{
				if (TenantRelocationRequestSchema.relocationSpecificProperties == null)
				{
					TenantRelocationRequestSchema.relocationSpecificProperties = new List<ADPropertyDefinition>
					{
						TenantRelocationRequestSchema.RelocationSyncStartTime,
						TenantRelocationRequestSchema.LockdownStartTime,
						TenantRelocationRequestSchema.RetiredStartTime,
						TenantRelocationRequestSchema.TransitionCounterRaw,
						TenantRelocationRequestSchema.SafeLockdownSchedule,
						TenantRelocationRequestSchema.RelocationSourceForestRaw,
						TenantRelocationRequestSchema.TargetForest,
						TenantRelocationRequestSchema.TenantRelocationFlags,
						TenantRelocationRequestSchema.RelocationStateRequested,
						TenantRelocationRequestSchema.RelocationStatusDetailsRaw,
						TenantRelocationRequestSchema.TenantSyncCookie,
						TenantRelocationRequestSchema.TenantRelocationCompletionTargetVector
					};
				}
				return TenantRelocationRequestSchema.relocationSpecificProperties;
			}
		}

		// Token: 0x04002F72 RID: 12146
		private static IList<ADPropertyDefinition> relocationSpecificProperties;

		// Token: 0x04002F73 RID: 12147
		public static readonly ADPropertyDefinition RelocationSyncStartTime = new ADPropertyDefinition("RelocationSyncStartTime", ExchangeObjectVersion.Exchange2003, typeof(DateTime?), "msExchRelocateTenantStartSync", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002F74 RID: 12148
		public static readonly ADPropertyDefinition LockdownStartTime = new ADPropertyDefinition("LockdownStartTime", ExchangeObjectVersion.Exchange2003, typeof(DateTime?), "msExchRelocateTenantStartLockdown", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002F75 RID: 12149
		public static readonly ADPropertyDefinition RetiredStartTime = new ADPropertyDefinition("RetiredStartTime", ExchangeObjectVersion.Exchange2003, typeof(DateTime?), "msExchRelocateTenantStartRetired", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002F76 RID: 12150
		public static readonly ADPropertyDefinition GLSResolvedForest = new ADPropertyDefinition("GLSResolvedForest", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.TaskPopulated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002F77 RID: 12151
		public static readonly ADPropertyDefinition SourceForestRIDMaster = new ADPropertyDefinition("SourceForestRIDMaster", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.TaskPopulated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002F78 RID: 12152
		public static readonly ADPropertyDefinition TargetForestRIDMaster = new ADPropertyDefinition("TargetForestRIDMaster", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.TaskPopulated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002F79 RID: 12153
		public static readonly ADPropertyDefinition TransitionCounterRaw = new ADPropertyDefinition("TransitionCounterRaw", ExchangeObjectVersion.Exchange2003, typeof(int), "msExchRelocateTenantTransitionCounter", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002F7A RID: 12154
		public static readonly ADPropertyDefinition TransitionCounter = new ADPropertyDefinition("TransitionCounter", ExchangeObjectVersion.Exchange2003, typeof(TransitionCount), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			TenantRelocationRequestSchema.TransitionCounterRaw
		}, null, new GetterDelegate(TenantRelocationRequest.GetTransitionCounter), new SetterDelegate(TenantRelocationRequest.SetTransitionCounter), null, null);

		// Token: 0x04002F7B RID: 12155
		public static readonly ADPropertyDefinition TargetOrganizationId = new ADPropertyDefinition("TargetOrganizationId", ExchangeObjectVersion.Exchange2003, typeof(OrganizationId), null, ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002F7C RID: 12156
		public static readonly ADPropertyDefinition RelocationInProgress = new ADPropertyDefinition("RelocationInProgress", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002F7D RID: 12157
		public static readonly ADPropertyDefinition TenantSyncCookie = new ADPropertyDefinition("TenantSyncCookie", ExchangeObjectVersion.Exchange2003, typeof(byte[]), "msExchSyncCookie", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002F7E RID: 12158
		public static readonly ADPropertyDefinition TargetOriginatingServer = new ADPropertyDefinition("TargetOriginatingServer", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.TaskPopulated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04002F7F RID: 12159
		public static readonly ADPropertyDefinition SafeLockdownSchedule = ExchangeConfigurationUnitSchema.SafeLockdownSchedule;

		// Token: 0x04002F80 RID: 12160
		public static readonly ADPropertyDefinition RelocationSourceForestRaw = ExchangeConfigurationUnitSchema.RelocationSourceForestRaw;

		// Token: 0x04002F81 RID: 12161
		public static readonly ADPropertyDefinition TargetForest = ExchangeConfigurationUnitSchema.TargetForest;

		// Token: 0x04002F82 RID: 12162
		public static readonly ADPropertyDefinition SourceForest = ExchangeConfigurationUnitSchema.SourceForest;

		// Token: 0x04002F83 RID: 12163
		public static readonly ADPropertyDefinition TenantRelocationFlags = ExchangeConfigurationUnitSchema.TenantRelocationFlags;

		// Token: 0x04002F84 RID: 12164
		public static readonly ADPropertyDefinition RelocationStateRequested = ExchangeConfigurationUnitSchema.RelocationStateRequested;

		// Token: 0x04002F85 RID: 12165
		public static readonly ADPropertyDefinition Suspended = ExchangeConfigurationUnitSchema.Suspended;

		// Token: 0x04002F86 RID: 12166
		public static readonly ADPropertyDefinition RelocationLastError = ExchangeConfigurationUnitSchema.RelocationLastError;

		// Token: 0x04002F87 RID: 12167
		public static readonly ADPropertyDefinition AutoCompletionEnabled = ExchangeConfigurationUnitSchema.AutoCompletionEnabled;

		// Token: 0x04002F88 RID: 12168
		public static readonly ADPropertyDefinition LargeTenantModeEnabled = ExchangeConfigurationUnitSchema.LargeTenantModeEnabled;

		// Token: 0x04002F89 RID: 12169
		public static readonly ADPropertyDefinition RelocationStatusDetailsRaw = ExchangeConfigurationUnitSchema.RelocationStatusDetailsRaw;

		// Token: 0x04002F8A RID: 12170
		public static readonly ADPropertyDefinition RelocationStatus = ExchangeConfigurationUnitSchema.RelocationStatus;

		// Token: 0x04002F8B RID: 12171
		public static readonly ADPropertyDefinition RelocationStatusDetailsSource = ExchangeConfigurationUnitSchema.RelocationStatusDetailsSource;

		// Token: 0x04002F8C RID: 12172
		public static readonly ADPropertyDefinition RelocationStatusDetailsDestination = ExchangeConfigurationUnitSchema.RelocationStatusDetailsDestination;

		// Token: 0x04002F8D RID: 12173
		public static readonly ADPropertyDefinition LastSuccessfulRelocationSyncStart = ExchangeConfigurationUnitSchema.LastSuccessfulRelocationSyncStart;

		// Token: 0x04002F8E RID: 12174
		public static readonly ADPropertyDefinition TenantRelocationCompletionTargetVector = ExchangeConfigurationUnitSchema.TenantRelocationCompletionTargetVector;

		// Token: 0x04002F8F RID: 12175
		public static readonly ADPropertyDefinition ExternalDirectoryOrganizationId = ExchangeConfigurationUnitSchema.ExternalDirectoryOrganizationId;

		// Token: 0x04002F90 RID: 12176
		public static readonly ADPropertyDefinition ServicePlan = ExchangeConfigurationUnitSchema.ServicePlan;

		// Token: 0x04002F91 RID: 12177
		public static readonly ADPropertyDefinition OrganizationStatus = ExchangeConfigurationUnitSchema.OrganizationStatus;

		// Token: 0x04002F92 RID: 12178
		public static readonly ADPropertyDefinition TargetOrganizationStatus = ExchangeConfigurationUnitSchema.TargetOrganizationStatus;

		// Token: 0x04002F93 RID: 12179
		public static readonly ADPropertyDefinition AdminDisplayVersion = OrganizationSchema.AdminDisplayVersion;

		// Token: 0x04002F94 RID: 12180
		public static readonly ADPropertyDefinition ExchangeUpgradeBucket = ExchangeConfigurationUnitSchema.ExchangeUpgradeBucket;

		// Token: 0x04002F95 RID: 12181
		public static readonly ADPropertyDefinition OrganizationFlags = OrganizationSchema.OrganizationFlags;

		// Token: 0x04002F96 RID: 12182
		public static readonly ADPropertyDefinition ObjectVersion = OrganizationSchema.ObjectVersion;
	}
}
