using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.BackSync;

namespace Microsoft.Exchange.Management.BackSync.Processors
{
	// Token: 0x020000C0 RID: 192
	internal class TenantRelocationProcessor : PipelineProcessor
	{
		// Token: 0x0600060D RID: 1549 RVA: 0x0001A416 File Offset: 0x00018616
		public TenantRelocationProcessor(IDataProcessor next, IPropertyLookup organizationPropertyLookup, ExcludedObjectReporter reporter, GetTenantRelocationStateDelegate getTenantRelocationState, Guid invocationId, bool isIncrementalSync) : base(next)
		{
			this.organizationPropertyLookup = organizationPropertyLookup;
			this.reporter = reporter;
			this.getTenantRelocationState = getTenantRelocationState;
			this.invocationId = invocationId;
			this.isIncrementalSync = isIncrementalSync;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0001A448 File Offset: 0x00018648
		protected override bool ProcessInternal(PropertyBag propertyBag)
		{
			ADObjectId tenantOU = ProcessorHelper.GetTenantOU(propertyBag);
			ADRawEntry properties = this.organizationPropertyLookup.GetProperties(tenantOU);
			if (!TenantRelocationProcessor.IsDeletedOrg(properties, tenantOU) && TenantRelocationProcessor.HasBeenInvolvedInRelocation(properties))
			{
				bool isSourceOfRelocation;
				TenantRelocationState tenantRelocationState = this.getTenantRelocationState(tenantOU, out isSourceOfRelocation, false);
				bool flag = tenantRelocationState.SourceForestState == TenantRelocationStatus.Retired;
				if (!flag || tenantRelocationState.SourceForestState == TenantRelocationStatus.Lockdown)
				{
					tenantRelocationState = this.getTenantRelocationState(tenantOU, out isSourceOfRelocation, true);
					flag = (tenantRelocationState.SourceForestState == TenantRelocationStatus.Retired);
				}
				return this.HandleRelocationState(flag, isSourceOfRelocation, propertyBag, properties);
			}
			return true;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0001A4D6 File Offset: 0x000186D6
		private static bool HasBeenInvolvedInRelocation(ADRawEntry org)
		{
			return org != null && (!string.IsNullOrEmpty((string)org[ExchangeConfigurationUnitSchema.TargetForest]) || !string.IsNullOrEmpty((string)org[ExchangeConfigurationUnitSchema.RelocationSourceForestRaw]));
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001A50E File Offset: 0x0001870E
		private static bool IsDeletedOrg(ADRawEntry org, ADObjectId ouId)
		{
			return (org != null && org.Id.IsDeleted) || (ouId != null && ouId.IsDeleted);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0001A530 File Offset: 0x00018730
		private static WatermarkMap GetVectorToFilterRelocationData(ADRawEntry org)
		{
			WatermarkMap watermarkMap = WatermarkMap.Empty;
			byte[] array = (byte[])org[TenantRelocationRequestSchema.TenantRelocationCompletionTargetVector];
			if (array != null)
			{
				try
				{
					watermarkMap = WatermarkMap.Parse(array);
				}
				catch (FormatException arg)
				{
					ExTraceGlobals.BackSyncTracer.TraceError<ADObjectId, FormatException>((long)SyncConfiguration.TraceId, "TenantRelocationProcessor::GetVectorToFilterRelocationData - Error parsing relocation completion vector tenant {0}. Error {1}.", org.Id, arg);
				}
			}
			ExTraceGlobals.BackSyncTracer.TraceDebug<ADObjectId, string>((long)SyncConfiguration.TraceId, "TenantRelocationProcessor::GetVectorToFilterRelocationData - Relocation completion vector found for tenant {0} is:\\n{1}.", org.Id, watermarkMap.SerializeToString());
			return watermarkMap;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0001A5B4 File Offset: 0x000187B4
		private bool HandleRelocationState(bool isRelocationCompleted, bool isSourceOfRelocation, PropertyBag propertyBag, ADRawEntry org)
		{
			if (!isRelocationCompleted)
			{
				if (!isSourceOfRelocation)
				{
					this.reporter.ReportExcludedObject(propertyBag, DirectoryObjectErrorCode.ObjectOutOfScope, ProcessingStage.RelocationStageFilter);
				}
				return isSourceOfRelocation;
			}
			if (!isSourceOfRelocation)
			{
				if (this.isIncrementalSync)
				{
					WatermarkMap vectorToFilterRelocationData = TenantRelocationProcessor.GetVectorToFilterRelocationData(org);
					if (vectorToFilterRelocationData.ContainsKey(this.invocationId))
					{
						long num = vectorToFilterRelocationData[this.invocationId];
						if ((long)(propertyBag[ADRecipientSchema.UsnChanged] ?? 9223372036854775807L) < num)
						{
							this.reporter.ReportExcludedObject(propertyBag, DirectoryObjectErrorCode.ObjectOutOfScope, ProcessingStage.RelocationPartOfRelocationSyncFilter);
							return false;
						}
					}
				}
				return true;
			}
			if (ProcessorHelper.IsObjectOrganizationUnit(propertyBag))
			{
				this.reporter.ReportExcludedObject(propertyBag, DirectoryObjectErrorCode.ObjectOutOfScope, ProcessingStage.RelocationStageFilter);
				return false;
			}
			ServiceInstanceId value = new ServiceInstanceId(string.Format("exchange/{0}", org[ExchangeConfigurationUnitSchema.TargetForest]));
			propertyBag.SetField(SyncObjectSchema.FaultInServiceInstance, value);
			return true;
		}

		// Token: 0x040002EE RID: 750
		private readonly IPropertyLookup organizationPropertyLookup;

		// Token: 0x040002EF RID: 751
		private readonly ExcludedObjectReporter reporter;

		// Token: 0x040002F0 RID: 752
		private readonly GetTenantRelocationStateDelegate getTenantRelocationState;

		// Token: 0x040002F1 RID: 753
		private readonly Guid invocationId;

		// Token: 0x040002F2 RID: 754
		private readonly bool isIncrementalSync;
	}
}
