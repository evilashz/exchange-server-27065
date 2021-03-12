using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003ED RID: 1005
	[Serializable]
	public class StampGroupContainer : ADConfigurationObject
	{
		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06002E1F RID: 11807 RVA: 0x000BBD08 File Offset: 0x000B9F08
		internal override ADObjectSchema Schema
		{
			get
			{
				return StampGroupContainer.schema;
			}
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06002E20 RID: 11808 RVA: 0x000BBD0F File Offset: 0x000B9F0F
		internal override string MostDerivedObjectClass
		{
			get
			{
				return StampGroupContainer.mostDerivedClass;
			}
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06002E21 RID: 11809 RVA: 0x000BBD16 File Offset: 0x000B9F16
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04001EFB RID: 7931
		public static readonly string DefaultName = "Stamp Groups";

		// Token: 0x04001EFC RID: 7932
		private static StampGroupContainerSchema schema = ObjectSchema.GetInstance<StampGroupContainerSchema>();

		// Token: 0x04001EFD RID: 7933
		private static string mostDerivedClass = "msExchMDBAvailabilityGroupContainer";
	}
}
