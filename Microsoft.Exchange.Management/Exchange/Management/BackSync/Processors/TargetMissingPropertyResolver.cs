using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.DirSync;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Diagnostics.Components.BackSync;

namespace Microsoft.Exchange.Management.BackSync.Processors
{
	// Token: 0x020000AE RID: 174
	internal abstract class TargetMissingPropertyResolver<TTargetProperty> : PipelineProcessor
	{
		// Token: 0x060005B7 RID: 1463 RVA: 0x00018959 File Offset: 0x00016B59
		public TargetMissingPropertyResolver(IDataProcessor next, IPropertyLookup targetPropertyLookup) : base(next)
		{
			this.targetPropertyLookup = targetPropertyLookup;
		}

		// Token: 0x060005B8 RID: 1464
		protected abstract List<PropertyDefinition> GetResolvingProperties();

		// Token: 0x060005B9 RID: 1465
		protected abstract ADObjectId GetTargetADObjectId(TTargetProperty targetPropertyValue);

		// Token: 0x060005BA RID: 1466
		protected abstract bool UpdateTargetMissingProperty(string sourceId, ADPropertyDefinition propertyDefinition, TTargetProperty targetPropertyValue, ADRawEntry target);

		// Token: 0x060005BB RID: 1467 RVA: 0x0001896C File Offset: 0x00016B6C
		protected override bool ProcessInternal(PropertyBag propertyBag)
		{
			IDictionary<ADPropertyDefinition, object> changedProperties = ADDirSyncHelper.GetChangedProperties(this.GetResolvingProperties(), propertyBag);
			foreach (KeyValuePair<ADPropertyDefinition, object> keyValuePair in changedProperties)
			{
				if (keyValuePair.Value != null)
				{
					List<TTargetProperty> list = new List<TTargetProperty>();
					MultiValuedProperty<TTargetProperty> multiValuedProperty = (MultiValuedProperty<TTargetProperty>)keyValuePair.Value;
					foreach (TTargetProperty ttargetProperty in multiValuedProperty)
					{
						string text = (string)propertyBag[SyncObjectSchema.ObjectId];
						ADObjectId targetADObjectId = this.GetTargetADObjectId(ttargetProperty);
						ADRawEntry properties = this.targetPropertyLookup.GetProperties(targetADObjectId);
						if (properties == null)
						{
							ExTraceGlobals.BackSyncTracer.TraceDebug<string, ADObjectId>((long)SyncConfiguration.TraceId, "TargetMissingPropertyResolver:: - Skipping target {0} -> {1}. Target object does not exist any more.", text, targetADObjectId);
						}
						else
						{
							string text2 = (string)properties[SyncObjectSchema.ObjectId];
							if (string.IsNullOrEmpty(text2))
							{
								ExTraceGlobals.BackSyncTracer.TraceDebug<string, ADObjectId>((long)SyncConfiguration.TraceId, "TargetMissingPropertyResolver:: - Skipping target {0} -> {1}. Target without ExternalDirectoryObjectId.", text, targetADObjectId);
							}
							else if (this.UpdateTargetMissingProperty(text, keyValuePair.Key, ttargetProperty, properties))
							{
								ExTraceGlobals.BackSyncTracer.TraceDebug<string, ADObjectId, string>((long)SyncConfiguration.TraceId, "TargetMissingPropertyResolver:: - Updating target {0} -> {1} with targetId: {2}.", text, targetADObjectId, text2);
								list.Add(ttargetProperty);
							}
						}
					}
					if (multiValuedProperty.Count != list.Count)
					{
						ExTraceGlobals.BackSyncTracer.TraceDebug<string, int, int>((long)SyncConfiguration.TraceId, "TargetMissingPropertyResolver:: - {0} targets were removed. Replacing original target mvp (size {1}) with filtered collection (size {2}).", keyValuePair.Key.Name, multiValuedProperty.Count, list.Count);
					}
					propertyBag.SetField(keyValuePair.Key, new MultiValuedProperty<TTargetProperty>(list));
				}
			}
			return true;
		}

		// Token: 0x040002CF RID: 719
		private readonly IPropertyLookup targetPropertyLookup;
	}
}
