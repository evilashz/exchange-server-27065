using System;
using System.Collections.Generic;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000164 RID: 356
	[Serializable]
	internal class AirSyncSetToDefaultStrategy : IAirSyncMissingPropertyStrategy
	{
		// Token: 0x0600101F RID: 4127 RVA: 0x0005B4FA File Offset: 0x000596FA
		public AirSyncSetToDefaultStrategy(Dictionary<string, bool> supportedTags)
		{
			this.supportedTags = supportedTags;
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0005B509 File Offset: 0x00059709
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
			if (PropertyState.Modified == srcProperty.State || PropertyState.Stream == srcProperty.State || PropertyState.SetToDefault == srcProperty.State)
			{
				dstAirSyncProperty.CopyFrom(srcProperty);
			}
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0005B54C File Offset: 0x0005974C
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
					if (this.supportedTags != null && !this.supportedTags.ContainsKey(airSyncProperty.AirSyncTagNames[0]))
					{
						airSyncProperty.State = PropertyState.Unmodified;
					}
					else
					{
						airSyncProperty.State = (airSyncProperty.ClientChangeTracked ? PropertyState.Unmodified : PropertyState.SetToDefault);
					}
				}
			}
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0005B5E8 File Offset: 0x000597E8
		public void Validate(AirSyncDataObject airSyncDataObject)
		{
			if (this.supportedTags != null && airSyncDataObject == null)
			{
				throw new ArgumentNullException("airSyncDataObject");
			}
			if (this.supportedTags != null)
			{
				foreach (IProperty property in airSyncDataObject.Children)
				{
					AirSyncProperty airSyncProperty = (AirSyncProperty)property;
					if (airSyncProperty.RequiresClientSupport && !this.supportedTags.ContainsKey(airSyncProperty.AirSyncTagNames[0]))
					{
						throw new ConversionException("Client must support property: " + airSyncProperty.AirSyncTagNames[0]);
					}
				}
			}
		}

		// Token: 0x04000A81 RID: 2689
		private Dictionary<string, bool> supportedTags;
	}
}
