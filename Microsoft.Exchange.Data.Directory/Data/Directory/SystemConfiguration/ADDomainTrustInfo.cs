using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000335 RID: 821
	[Serializable]
	public class ADDomainTrustInfo : ADNonExchangeObject
	{
		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x060025EA RID: 9706 RVA: 0x000A083B File Offset: 0x0009EA3B
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADDomainTrustInfo.schema;
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x060025EB RID: 9707 RVA: 0x000A0842 File Offset: 0x0009EA42
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADDomainTrustInfo.mostDerivedClass;
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x060025ED RID: 9709 RVA: 0x000A0851 File Offset: 0x0009EA51
		public string TargetName
		{
			get
			{
				return (string)this[ADDomainTrustInfoSchema.TargetName];
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x060025EE RID: 9710 RVA: 0x000A0863 File Offset: 0x0009EA63
		public TrustAttributeFlag TrustAttribute
		{
			get
			{
				return (TrustAttributeFlag)this[ADDomainTrustInfoSchema.TrustAttributes];
			}
		}

		// Token: 0x0400172E RID: 5934
		private static ADDomainTrustInfoSchema schema = ObjectSchema.GetInstance<ADDomainTrustInfoSchema>();

		// Token: 0x0400172F RID: 5935
		private static string mostDerivedClass = "trustedDomain";
	}
}
