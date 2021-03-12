using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000165 RID: 357
	[Serializable]
	internal class AirSyncSetToUnmodifiedStrategy : IAirSyncMissingPropertyStrategy
	{
		// Token: 0x06001023 RID: 4131 RVA: 0x0005B688 File Offset: 0x00059888
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
			if (PropertyState.SetToDefault == srcProperty.State)
			{
				dstAirSyncProperty.OutputEmptyNode();
				return;
			}
			if (PropertyState.Unmodified != srcProperty.State)
			{
				dstAirSyncProperty.CopyFrom(srcProperty);
			}
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0005B6C8 File Offset: 0x000598C8
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
			}
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0005B744 File Offset: 0x00059944
		public void Validate(AirSyncDataObject airSyncDataObject)
		{
		}
	}
}
