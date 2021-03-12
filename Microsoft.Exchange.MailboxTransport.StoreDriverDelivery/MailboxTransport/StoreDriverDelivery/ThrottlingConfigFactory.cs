using System;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200003D RID: 61
	internal class ThrottlingConfigFactory
	{
		// Token: 0x060002C7 RID: 711 RVA: 0x0000CD9E File Offset: 0x0000AF9E
		public static IThrottlingConfig Create()
		{
			if (ThrottlingConfigFactory.InstanceBuilder != null)
			{
				return ThrottlingConfigFactory.InstanceBuilder();
			}
			return new ThrottlingConfig();
		}

		// Token: 0x0400011C RID: 284
		internal static ThrottlingConfigFactory.ThrottlingConfigBuilder InstanceBuilder;

		// Token: 0x0200003E RID: 62
		// (Invoke) Token: 0x060002CA RID: 714
		public delegate IThrottlingConfig ThrottlingConfigBuilder();
	}
}
