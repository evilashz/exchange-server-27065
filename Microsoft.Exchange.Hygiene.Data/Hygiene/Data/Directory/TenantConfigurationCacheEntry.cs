using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x0200010B RID: 267
	internal class TenantConfigurationCacheEntry : ADObject
	{
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x0001F15F File Offset: 0x0001D35F
		// (set) Token: 0x06000A57 RID: 2647 RVA: 0x0001F178 File Offset: 0x0001D378
		public Guid TenantId
		{
			get
			{
				return Guid.Parse(this[ADObjectSchema.RawName] as string);
			}
			set
			{
				string text = value.ToString();
				this[ADObjectSchema.RawName] = text;
				base.SetId(new ADObjectId(DalHelper.GetTenantDistinguishedName(text), value));
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x0001F1B1 File Offset: 0x0001D3B1
		// (set) Token: 0x06000A59 RID: 2649 RVA: 0x0001F1C3 File Offset: 0x0001D3C3
		public TenantConfigurationCacheEntryReason Reason
		{
			get
			{
				return (TenantConfigurationCacheEntryReason)this[TenantConfigurationCacheEntrySchema.Reason];
			}
			set
			{
				this[TenantConfigurationCacheEntrySchema.Reason] = value;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x0001F1D6 File Offset: 0x0001D3D6
		internal override ADObjectSchema Schema
		{
			get
			{
				return TenantConfigurationCacheEntry.schema;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0001F1DD File Offset: 0x0001D3DD
		internal override string MostDerivedObjectClass
		{
			get
			{
				return TenantConfigurationCacheEntry.mostDerivedClass;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x0001F1E4 File Offset: 0x0001D3E4
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x0400055D RID: 1373
		private static readonly string mostDerivedClass = "TenantConfigurationCacheEntry";

		// Token: 0x0400055E RID: 1374
		private static readonly TenantConfigurationCacheEntrySchema schema = ObjectSchema.GetInstance<TenantConfigurationCacheEntrySchema>();
	}
}
