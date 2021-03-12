using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.BackSync;

namespace Microsoft.Exchange.Management.BackSync.Processors
{
	// Token: 0x020000B3 RID: 179
	internal class MissingPropertyResolver : PipelineProcessor, IMissingPropertyResolver
	{
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x000191E4 File Offset: 0x000173E4
		// (set) Token: 0x060005D3 RID: 1491 RVA: 0x000191EC File Offset: 0x000173EC
		public ADRawEntry LastProcessedEntry { get; private set; }

		// Token: 0x060005D4 RID: 1492 RVA: 0x000191F5 File Offset: 0x000173F5
		public MissingPropertyResolver(IDataProcessor next, IPropertyLookup objectPropertyLookup) : base(next)
		{
			this.objectPropertyLookup = objectPropertyLookup;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00019208 File Offset: 0x00017408
		protected override bool ProcessInternal(PropertyBag propertyBag)
		{
			ADObjectId id = (ADObjectId)propertyBag[ADObjectSchema.Id];
			ADRawEntry properties = this.objectPropertyLookup.GetProperties(id);
			if (properties == null)
			{
				ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "MissingPropertyResolver:: - Skipping object {0}. Cannot read missing properties. Object was removed. Next incremantal sync will pick up deletion.", new object[]
				{
					propertyBag[ADObjectSchema.Id]
				});
				return false;
			}
			foreach (object obj in properties.propertyBag.Keys)
			{
				ProviderPropertyDefinition providerPropertyDefinition = (ProviderPropertyDefinition)obj;
				if (!propertyBag.Contains(providerPropertyDefinition))
				{
					propertyBag.SetField(providerPropertyDefinition, properties[providerPropertyDefinition]);
				}
			}
			if (ProcessorHelper.IsObjectOrganizationUnit(propertyBag) && !propertyBag.Contains(SyncCompanySchema.DirSyncStatusAck) && propertyBag.Contains(ExtendedOrganizationalUnitSchema.DirSyncStatusAck) && propertyBag[ExtendedOrganizationalUnitSchema.DirSyncStatusAck] != null && ((MultiValuedProperty<string>)propertyBag[ExtendedOrganizationalUnitSchema.DirSyncStatusAck]).Count > 0)
			{
				propertyBag.SetField(SyncCompanySchema.DirSyncStatusAck, propertyBag[ExtendedOrganizationalUnitSchema.DirSyncStatusAck]);
			}
			this.LastProcessedEntry = properties;
			return true;
		}

		// Token: 0x040002D8 RID: 728
		private readonly IPropertyLookup objectPropertyLookup;
	}
}
