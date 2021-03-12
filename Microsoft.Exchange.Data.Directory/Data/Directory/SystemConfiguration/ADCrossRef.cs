using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200032C RID: 812
	[ObjectScope(ConfigScopes.Global)]
	[Serializable]
	public class ADCrossRef : ADNonExchangeObject
	{
		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x060025CA RID: 9674 RVA: 0x000A0175 File Offset: 0x0009E375
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADCrossRef.schema;
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x060025CB RID: 9675 RVA: 0x000A017C File Offset: 0x0009E37C
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADCrossRef.mostDerivedClass;
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x060025CD RID: 9677 RVA: 0x000A018B File Offset: 0x0009E38B
		public ADObjectId NCName
		{
			get
			{
				return (ADObjectId)this.propertyBag[ADCrossRefSchema.NCName];
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x060025CE RID: 9678 RVA: 0x000A01A2 File Offset: 0x0009E3A2
		public MultiValuedProperty<string> DnsRoot
		{
			get
			{
				return (MultiValuedProperty<string>)this.propertyBag[ADCrossRefSchema.DnsRoot];
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x060025CF RID: 9679 RVA: 0x000A01B9 File Offset: 0x0009E3B9
		public string NetBiosName
		{
			get
			{
				return (string)this.propertyBag[ADCrossRefSchema.NetBiosName];
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x060025D0 RID: 9680 RVA: 0x000A01D0 File Offset: 0x0009E3D0
		public ADObjectId TrustParent
		{
			get
			{
				return (ADObjectId)this.propertyBag[ADCrossRefSchema.TrustParent];
			}
		}

		// Token: 0x04001709 RID: 5897
		private static ADCrossRefSchema schema = ObjectSchema.GetInstance<ADCrossRefSchema>();

		// Token: 0x0400170A RID: 5898
		private static string mostDerivedClass = "crossRef";
	}
}
