using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AF8 RID: 2808
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PropertyValueTrackingData
	{
		// Token: 0x060065F3 RID: 26099 RVA: 0x001B0F49 File Offset: 0x001AF149
		public PropertyValueTrackingData(PropertyTrackingInformation changeType, object originalValue)
		{
			EnumValidator.ThrowIfInvalid<PropertyTrackingInformation>(changeType, "changeType");
			this.propertyValueState = changeType;
			this.originalPropertyValue = originalValue;
		}

		// Token: 0x17001C22 RID: 7202
		// (get) Token: 0x060065F4 RID: 26100 RVA: 0x001B0F6A File Offset: 0x001AF16A
		internal PropertyTrackingInformation PropertyValueState
		{
			get
			{
				return this.propertyValueState;
			}
		}

		// Token: 0x17001C23 RID: 7203
		// (get) Token: 0x060065F5 RID: 26101 RVA: 0x001B0F72 File Offset: 0x001AF172
		internal object OriginalPropertyValue
		{
			get
			{
				return this.originalPropertyValue;
			}
		}

		// Token: 0x040039F7 RID: 14839
		public static readonly PropertyValueTrackingData PropertyValueTrackDataNotTracked = new PropertyValueTrackingData(PropertyTrackingInformation.NotTracked, null);

		// Token: 0x040039F8 RID: 14840
		public static readonly PropertyValueTrackingData PropertyValueTrackDataUnchanged = new PropertyValueTrackingData(PropertyTrackingInformation.Unchanged, null);

		// Token: 0x040039F9 RID: 14841
		private readonly PropertyTrackingInformation propertyValueState;

		// Token: 0x040039FA RID: 14842
		private readonly object originalPropertyValue;
	}
}
