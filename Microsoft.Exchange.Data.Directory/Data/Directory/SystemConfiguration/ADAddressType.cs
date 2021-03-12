using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200031E RID: 798
	[Serializable]
	public class ADAddressType : ADConfigurationObject
	{
		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06002513 RID: 9491 RVA: 0x0009DBD8 File Offset: 0x0009BDD8
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADAddressType.schema;
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06002514 RID: 9492 RVA: 0x0009DBDF File Offset: 0x0009BDDF
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADAddressType.mostDerivedClass;
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06002516 RID: 9494 RVA: 0x0009DBEE File Offset: 0x0009BDEE
		public Version FileVersion
		{
			get
			{
				return (Version)this[ADAddressTypeSchema.FileVersion];
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06002517 RID: 9495 RVA: 0x0009DC00 File Offset: 0x0009BE00
		public string ProxyGeneratorDll
		{
			get
			{
				return (string)this[ADAddressTypeSchema.ProxyGeneratorDll];
			}
		}

		// Token: 0x040016D0 RID: 5840
		private static ADAddressTypeSchema schema = ObjectSchema.GetInstance<ADAddressTypeSchema>();

		// Token: 0x040016D1 RID: 5841
		private static string mostDerivedClass = "AddrType";

		// Token: 0x040016D2 RID: 5842
		internal static readonly ADObjectId ContainerId = new ADObjectId("CN=Address-Types,CN=Addressing");
	}
}
