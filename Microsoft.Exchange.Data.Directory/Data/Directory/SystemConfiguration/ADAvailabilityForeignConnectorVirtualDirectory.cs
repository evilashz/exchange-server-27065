using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000327 RID: 807
	[Serializable]
	public sealed class ADAvailabilityForeignConnectorVirtualDirectory : ExchangeVirtualDirectory
	{
		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x0600256A RID: 9578 RVA: 0x0009EC9B File Offset: 0x0009CE9B
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADAvailabilityForeignConnectorVirtualDirectory.schema;
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x0600256B RID: 9579 RVA: 0x0009ECA2 File Offset: 0x0009CEA2
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADAvailabilityForeignConnectorVirtualDirectory.MostDerivedClass;
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x0600256C RID: 9580 RVA: 0x0009ECA9 File Offset: 0x0009CEA9
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x0600256D RID: 9581 RVA: 0x0009ECBC File Offset: 0x0009CEBC
		// (set) Token: 0x0600256E RID: 9582 RVA: 0x0009ECCE File Offset: 0x0009CECE
		public string AvailabilityForeignConnectorType
		{
			get
			{
				return (string)this[ADAvailabilityForeignConnectorVirtualDirectorySchema.AvailabilityForeignConnectorType];
			}
			internal set
			{
				this[ADAvailabilityForeignConnectorVirtualDirectorySchema.AvailabilityForeignConnectorType] = value;
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x0600256F RID: 9583 RVA: 0x0009ECDC File Offset: 0x0009CEDC
		// (set) Token: 0x06002570 RID: 9584 RVA: 0x0009ECEE File Offset: 0x0009CEEE
		public MultiValuedProperty<string> AvailabilityForeignConnectorDomains
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADAvailabilityForeignConnectorVirtualDirectorySchema.AvailabilityForeignConnectorDomains];
			}
			internal set
			{
				this[ADAvailabilityForeignConnectorVirtualDirectorySchema.AvailabilityForeignConnectorDomains] = value;
			}
		}

		// Token: 0x040016F4 RID: 5876
		private static readonly ADAvailabilityForeignConnectorVirtualDirectorySchema schema = ObjectSchema.GetInstance<ADAvailabilityForeignConnectorVirtualDirectorySchema>();

		// Token: 0x040016F5 RID: 5877
		public static readonly string MostDerivedClass = "msExchAvailabilityForeignConnectorVirtualDirectory";
	}
}
