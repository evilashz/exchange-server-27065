using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200035D RID: 861
	[Serializable]
	public class ADPasswordSettings : ADNonExchangeObject
	{
		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x060027B5 RID: 10165 RVA: 0x000A72A5 File Offset: 0x000A54A5
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADPasswordSettings.schema;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x060027B6 RID: 10166 RVA: 0x000A72AC File Offset: 0x000A54AC
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADPasswordSettings.mostDerivedClass;
			}
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x000A72B3 File Offset: 0x000A54B3
		public ADPasswordSettings()
		{
			this.propertyBag.Remove(ADObjectSchema.ExchangeVersion);
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x060027B8 RID: 10168 RVA: 0x000A72CB File Offset: 0x000A54CB
		// (set) Token: 0x060027B9 RID: 10169 RVA: 0x000A72E2 File Offset: 0x000A54E2
		internal MultiValuedProperty<ADObjectId> AppliesTo
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this.propertyBag[ADPasswordSettingsSchema.AppliesTo];
			}
			set
			{
				this.propertyBag[ADPasswordSettingsSchema.AppliesTo] = value;
			}
		}

		// Token: 0x04001833 RID: 6195
		private static ADPasswordSettingsSchema schema = ObjectSchema.GetInstance<ADPasswordSettingsSchema>();

		// Token: 0x04001834 RID: 6196
		private static string mostDerivedClass = "msds-PasswordSettings";
	}
}
