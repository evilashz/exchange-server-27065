using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200032E RID: 814
	[Serializable]
	public class ADCrossRefContainer : ADNonExchangeObject
	{
		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x060025D4 RID: 9684 RVA: 0x000A0211 File Offset: 0x0009E411
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADCrossRefContainer.schema;
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x060025D5 RID: 9685 RVA: 0x000A0218 File Offset: 0x0009E418
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADCrossRefContainer.mostDerivedClass;
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x060025D7 RID: 9687 RVA: 0x000A0227 File Offset: 0x0009E427
		public MultiValuedProperty<string> UPNSuffixes
		{
			get
			{
				return (MultiValuedProperty<string>)this.propertyBag[ADCrossRefContainerSchema.UPNSuffixes];
			}
		}

		// Token: 0x0400170C RID: 5900
		private static ADCrossRefContainerSchema schema = ObjectSchema.GetInstance<ADCrossRefContainerSchema>();

		// Token: 0x0400170D RID: 5901
		private static string mostDerivedClass = "crossRefContainer";
	}
}
