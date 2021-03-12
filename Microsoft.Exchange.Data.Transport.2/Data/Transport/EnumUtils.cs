using System;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x0200005D RID: 93
	public static class EnumUtils
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00006819 File Offset: 0x00004A19
		public static string[] DeliveryPriorityEnumNames
		{
			get
			{
				return EnumUtils.deliveryPriorityEnumNames;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00006820 File Offset: 0x00004A20
		public static DeliveryPriority[] DeliveryPriorityEnumValues
		{
			get
			{
				return EnumUtils.deliveryPriorityEnumValues;
			}
		}

		// Token: 0x04000179 RID: 377
		private static readonly string[] deliveryPriorityEnumNames = Enum.GetNames(typeof(DeliveryPriority));

		// Token: 0x0400017A RID: 378
		private static readonly DeliveryPriority[] deliveryPriorityEnumValues = (DeliveryPriority[])Enum.GetValues(typeof(DeliveryPriority));
	}
}
