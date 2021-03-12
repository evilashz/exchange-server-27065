using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002D4 RID: 724
	[Serializable]
	public abstract class ADLegacyVersionableObject : ADConfigurationObject
	{
		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06002089 RID: 8329 RVA: 0x00092F71 File Offset: 0x00091171
		// (set) Token: 0x0600208A RID: 8330 RVA: 0x00092F83 File Offset: 0x00091183
		internal int? MinAdminVersion
		{
			get
			{
				return (int?)this[ADLegacyVersionableObjectSchema.MinAdminVersion];
			}
			set
			{
				this[ADLegacyVersionableObjectSchema.MinAdminVersion] = value;
			}
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x00092F98 File Offset: 0x00091198
		internal void StampDefaultMinAdminVersion()
		{
			object obj = null;
			if (!this.propertyBag.TryGetField(ADLegacyVersionableObjectSchema.MinAdminVersion, ref obj))
			{
				this.MinAdminVersion = new int?(this.MaximumSupportedExchangeObjectVersion.ExchangeBuild.ToExchange2003FormatInt32());
			}
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x00092FDC File Offset: 0x000911DC
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (base.IsChanged(ADLegacyVersionableObjectSchema.MinAdminVersion) || base.IsChanged(ADObjectSchema.ExchangeVersion))
			{
				if (this.MinAdminVersion == null)
				{
					if (!base.ExchangeVersion.Equals(ExchangeObjectVersion.Exchange2003))
					{
						errors.Add(new PropertyValidationError(DirectoryStrings.ErrorMinAdminVersionNull(base.ExchangeVersion), ADLegacyVersionableObjectSchema.MinAdminVersion, this.MinAdminVersion));
						return;
					}
				}
				else if (base.ExchangeVersion.ExchangeBuild.ToExchange2003FormatInt32() != this.MinAdminVersion.Value)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.ErrorMinAdminVersionOutOfSync(this.MinAdminVersion.Value, base.ExchangeVersion, base.ExchangeVersion.ExchangeBuild.ToExchange2003FormatInt32()), ADLegacyVersionableObjectSchema.MinAdminVersion, this.MinAdminVersion));
				}
			}
		}
	}
}
