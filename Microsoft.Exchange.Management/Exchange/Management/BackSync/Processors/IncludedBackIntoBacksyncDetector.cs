using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Management.BackSync.Processors
{
	// Token: 0x020000AC RID: 172
	internal class IncludedBackIntoBacksyncDetector : PipelineProcessor
	{
		// Token: 0x060005B4 RID: 1460 RVA: 0x000188F7 File Offset: 0x00016AF7
		public IncludedBackIntoBacksyncDetector(IDataProcessor next, ServiceInstanceId serviceInstanceId) : base(next)
		{
			this.serviceInstanceId = serviceInstanceId;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00018908 File Offset: 0x00016B08
		protected override bool ProcessInternal(PropertyBag propertyBag)
		{
			if (propertyBag.Contains(SyncUserSchema.ServiceOriginatedResource))
			{
				MultiValuedProperty<Capability> multiValuedProperty = (MultiValuedProperty<Capability>)propertyBag[SyncUserSchema.ServiceOriginatedResource];
				if (!multiValuedProperty.Contains(Capability.ExcludedFromBackSync) && propertyBag.Count == 6)
				{
					propertyBag.SetField(SyncObjectSchema.FaultInServiceInstance, this.serviceInstanceId);
				}
			}
			return true;
		}

		// Token: 0x040002CE RID: 718
		private readonly ServiceInstanceId serviceInstanceId;
	}
}
