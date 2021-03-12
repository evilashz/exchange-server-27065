using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003E4 RID: 996
	[Serializable]
	public class DatabaseAvailabilityGroupConfiguration : ADConfigurationObject
	{
		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06002DC5 RID: 11717 RVA: 0x000BB0FC File Offset: 0x000B92FC
		internal override ADObjectSchema Schema
		{
			get
			{
				return DatabaseAvailabilityGroupConfiguration.schema;
			}
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06002DC6 RID: 11718 RVA: 0x000BB103 File Offset: 0x000B9303
		internal override string MostDerivedObjectClass
		{
			get
			{
				return DatabaseAvailabilityGroupConfiguration.mostDerivedClass;
			}
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06002DC7 RID: 11719 RVA: 0x000BB10A File Offset: 0x000B930A
		// (set) Token: 0x06002DC8 RID: 11720 RVA: 0x000BB11C File Offset: 0x000B931C
		public new string Name
		{
			get
			{
				return (string)this[DatabaseAvailabilityGroupConfigurationSchema.Name];
			}
			internal set
			{
				this[DatabaseAvailabilityGroupConfigurationSchema.Name] = value;
			}
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06002DC9 RID: 11721 RVA: 0x000BB12A File Offset: 0x000B932A
		// (set) Token: 0x06002DCA RID: 11722 RVA: 0x000BB13C File Offset: 0x000B933C
		public string ConfigurationXML
		{
			get
			{
				return (string)this[DatabaseAvailabilityGroupConfigurationSchema.ConfigurationXML];
			}
			internal set
			{
				this[DatabaseAvailabilityGroupConfigurationSchema.ConfigurationXML] = value;
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06002DCB RID: 11723 RVA: 0x000BB14A File Offset: 0x000B934A
		public MultiValuedProperty<ADObjectId> Dags
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[DatabaseAvailabilityGroupConfigurationSchema.Dags];
			}
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x000BB15C File Offset: 0x000B935C
		internal static object DagConfigNameGetter(IPropertyBag propertyBag)
		{
			return propertyBag[ADObjectSchema.RawName];
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x000BB169 File Offset: 0x000B9369
		internal static void DagConfigNameSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ADObjectSchema.RawName] = value;
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06002DCE RID: 11726 RVA: 0x000BB177 File Offset: 0x000B9377
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04001EB3 RID: 7859
		private static readonly DatabaseAvailabilityGroupConfigurationSchema schema = ObjectSchema.GetInstance<DatabaseAvailabilityGroupConfigurationSchema>();

		// Token: 0x04001EB4 RID: 7860
		private static string mostDerivedClass = "msExchMDBAvailabilityGroupConfiguration";
	}
}
