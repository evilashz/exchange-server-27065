using System;
using System.Collections.Generic;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Diagnostics.Components.SchemaConverter;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x0200016F RID: 367
	[Serializable]
	internal class FlexibleSchemaStrategy : IAirSyncMissingPropertyStrategy
	{
		// Token: 0x06001044 RID: 4164 RVA: 0x0005BC4F File Offset: 0x00059E4F
		public FlexibleSchemaStrategy(Dictionary<string, bool> schemaTags)
		{
			this.schemaTags = schemaTags;
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0005BC60 File Offset: 0x00059E60
		public void ExecuteCopyProperty(IProperty srcProperty, AirSyncProperty dstAirSyncProperty)
		{
			if (srcProperty == null)
			{
				throw new ArgumentNullException("srcProperty");
			}
			if (dstAirSyncProperty == null)
			{
				throw new ArgumentNullException("dstAirSyncProperty");
			}
			if (dstAirSyncProperty.AirSyncTagNames == null)
			{
				throw new ArgumentNullException("dstAirSyncProperty.AirSyncTagNames");
			}
			if (PropertyState.Modified == srcProperty.State && (this.schemaTags == null || this.schemaTags.ContainsKey(dstAirSyncProperty.Namespace + dstAirSyncProperty.AirSyncTagNames[0])))
			{
				dstAirSyncProperty.CopyFrom(srcProperty);
			}
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0005BCD4 File Offset: 0x00059ED4
		public void PostProcessPropertyBag(AirSyncDataObject airSyncDataObject)
		{
			if (airSyncDataObject == null)
			{
				throw new ArgumentNullException("airSyncDataObject");
			}
			foreach (IProperty property in airSyncDataObject.Children)
			{
				AirSyncProperty airSyncProperty = (AirSyncProperty)property;
				if (airSyncProperty.State == PropertyState.Uninitialized)
				{
					airSyncProperty.State = PropertyState.Unmodified;
				}
				else if (airSyncProperty.IsBoundToEmptyTag())
				{
					airSyncProperty.State = PropertyState.SetToDefault;
				}
				AirSyncDiagnostics.TraceInfo<AirSyncProperty, PropertyState>(ExTraceGlobals.AirSyncTracer, this, "Property={0} State={1}", airSyncProperty, airSyncProperty.State);
			}
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0005BD64 File Offset: 0x00059F64
		public void Validate(AirSyncDataObject airSyncDataObject)
		{
		}

		// Token: 0x04000A88 RID: 2696
		private Dictionary<string, bool> schemaTags;
	}
}
