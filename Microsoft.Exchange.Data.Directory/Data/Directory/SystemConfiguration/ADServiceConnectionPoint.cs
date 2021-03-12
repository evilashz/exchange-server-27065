using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200038B RID: 907
	[ObjectScope(ConfigScopes.Server)]
	[Serializable]
	public class ADServiceConnectionPoint : ADNonExchangeObject
	{
		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06002989 RID: 10633 RVA: 0x000AE9B2 File Offset: 0x000ACBB2
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADServiceConnectionPoint.schema;
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x0600298A RID: 10634 RVA: 0x000AE9B9 File Offset: 0x000ACBB9
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADServiceConnectionPoint.mostDerivedClass;
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x0600298B RID: 10635 RVA: 0x000AE9C0 File Offset: 0x000ACBC0
		// (set) Token: 0x0600298C RID: 10636 RVA: 0x000AE9D7 File Offset: 0x000ACBD7
		public MultiValuedProperty<string> Keywords
		{
			get
			{
				return (MultiValuedProperty<string>)this.propertyBag[ADServiceConnectionPointSchema.Keywords];
			}
			internal set
			{
				this.propertyBag[ADServiceConnectionPointSchema.Keywords] = value;
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x0600298D RID: 10637 RVA: 0x000AE9EA File Offset: 0x000ACBEA
		// (set) Token: 0x0600298E RID: 10638 RVA: 0x000AEA01 File Offset: 0x000ACC01
		public MultiValuedProperty<string> ServiceBindingInformation
		{
			get
			{
				return (MultiValuedProperty<string>)this.propertyBag[ADServiceConnectionPointSchema.ServiceBindingInformation];
			}
			internal set
			{
				this.propertyBag[ADServiceConnectionPointSchema.ServiceBindingInformation] = value;
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x0600298F RID: 10639 RVA: 0x000AEA14 File Offset: 0x000ACC14
		// (set) Token: 0x06002990 RID: 10640 RVA: 0x000AEA2B File Offset: 0x000ACC2B
		public string ServiceDnsName
		{
			get
			{
				return (string)this.propertyBag[ADServiceConnectionPointSchema.ServiceDnsName];
			}
			internal set
			{
				this.propertyBag[ADServiceConnectionPointSchema.ServiceDnsName] = value;
			}
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06002991 RID: 10641 RVA: 0x000AEA3E File Offset: 0x000ACC3E
		// (set) Token: 0x06002992 RID: 10642 RVA: 0x000AEA55 File Offset: 0x000ACC55
		public string ServiceClassName
		{
			get
			{
				return (string)this.propertyBag[ADServiceConnectionPointSchema.ServiceClassName];
			}
			internal set
			{
				this.propertyBag[ADServiceConnectionPointSchema.ServiceClassName] = value;
			}
		}

		// Token: 0x04001967 RID: 6503
		private static ADServiceConnectionPointSchema schema = ObjectSchema.GetInstance<ADServiceConnectionPointSchema>();

		// Token: 0x04001968 RID: 6504
		private static string mostDerivedClass = "ServiceConnectionPoint";
	}
}
