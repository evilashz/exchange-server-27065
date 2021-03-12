using System;
using System.Globalization;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000569 RID: 1385
	[ObjectScope(ConfigScopes.Global)]
	[Serializable]
	public class RootDse : ADNonExchangeObject
	{
		// Token: 0x170013ED RID: 5101
		// (get) Token: 0x06003E18 RID: 15896 RVA: 0x000EC069 File Offset: 0x000EA269
		internal override ADObjectSchema Schema
		{
			get
			{
				return RootDse.schema;
			}
		}

		// Token: 0x170013EE RID: 5102
		// (get) Token: 0x06003E19 RID: 15897 RVA: 0x000EC070 File Offset: 0x000EA270
		internal override string MostDerivedObjectClass
		{
			get
			{
				return RootDse.mostDerivedClass;
			}
		}

		// Token: 0x170013EF RID: 5103
		// (get) Token: 0x06003E1A RID: 15898 RVA: 0x000EC077 File Offset: 0x000EA277
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, "*");
			}
		}

		// Token: 0x170013F0 RID: 5104
		// (get) Token: 0x06003E1C RID: 15900 RVA: 0x000EC091 File Offset: 0x000EA291
		// (set) Token: 0x06003E1D RID: 15901 RVA: 0x000EC0A3 File Offset: 0x000EA2A3
		public ADObjectId ConfigurationNamingContext
		{
			get
			{
				return (ADObjectId)this[RootDseSchema.ConfigurationNamingContext];
			}
			set
			{
				this[RootDseSchema.ConfigurationNamingContext] = value;
			}
		}

		// Token: 0x170013F1 RID: 5105
		// (get) Token: 0x06003E1E RID: 15902 RVA: 0x000EC0B1 File Offset: 0x000EA2B1
		// (set) Token: 0x06003E1F RID: 15903 RVA: 0x000EC0C3 File Offset: 0x000EA2C3
		public ADObjectId DefaultNamingContext
		{
			get
			{
				return (ADObjectId)this[RootDseSchema.DefaultNamingContext];
			}
			set
			{
				this[RootDseSchema.DefaultNamingContext] = value;
			}
		}

		// Token: 0x170013F2 RID: 5106
		// (get) Token: 0x06003E20 RID: 15904 RVA: 0x000EC0D1 File Offset: 0x000EA2D1
		// (set) Token: 0x06003E21 RID: 15905 RVA: 0x000EC0E3 File Offset: 0x000EA2E3
		public string Fqdn
		{
			get
			{
				return (string)this[RootDseSchema.Fqdn];
			}
			set
			{
				this[RootDseSchema.Fqdn] = value;
			}
		}

		// Token: 0x170013F3 RID: 5107
		// (get) Token: 0x06003E22 RID: 15906 RVA: 0x000EC0F1 File Offset: 0x000EA2F1
		// (set) Token: 0x06003E23 RID: 15907 RVA: 0x000EC103 File Offset: 0x000EA303
		public long HighestCommittedUSN
		{
			get
			{
				return (long)this[RootDseSchema.HighestCommittedUSN];
			}
			set
			{
				this[RootDseSchema.HighestCommittedUSN] = value;
			}
		}

		// Token: 0x170013F4 RID: 5108
		// (get) Token: 0x06003E24 RID: 15908 RVA: 0x000EC116 File Offset: 0x000EA316
		public ExDateTime CurrentTime
		{
			get
			{
				return (ExDateTime)this[RootDseSchema.CurrentTime];
			}
		}

		// Token: 0x170013F5 RID: 5109
		// (get) Token: 0x06003E25 RID: 15909 RVA: 0x000EC128 File Offset: 0x000EA328
		// (set) Token: 0x06003E26 RID: 15910 RVA: 0x000EC13A File Offset: 0x000EA33A
		public bool IsSynchronized
		{
			get
			{
				return (bool)this[RootDseSchema.IsSynchronized];
			}
			set
			{
				this[RootDseSchema.IsSynchronized] = value;
			}
		}

		// Token: 0x170013F6 RID: 5110
		// (get) Token: 0x06003E27 RID: 15911 RVA: 0x000EC14D File Offset: 0x000EA34D
		public ADObjectId NtDsDsa
		{
			get
			{
				return (ADObjectId)this[RootDseSchema.NtDsDsa];
			}
		}

		// Token: 0x06003E28 RID: 15912 RVA: 0x000EC160 File Offset: 0x000EA360
		internal static object SiteGetter(IPropertyBag propertyBag)
		{
			ADObjectId adobjectId = (ADObjectId)propertyBag[RootDseSchema.NtDsDsa];
			return adobjectId.AncestorDN(3);
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x000EC188 File Offset: 0x000EA388
		internal static object CurrentTimeGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[RootDseSchema.CurrentTimeRaw];
			ExDateTime exDateTime;
			if (!string.IsNullOrEmpty(text) && ExDateTime.TryParseExact(text, "yyyyMMddHHmmss.fZ", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out exDateTime))
			{
				return exDateTime;
			}
			return ExDateTime.MinValue;
		}

		// Token: 0x170013F7 RID: 5111
		// (get) Token: 0x06003E2A RID: 15914 RVA: 0x000EC1D5 File Offset: 0x000EA3D5
		public ADObjectId Site
		{
			get
			{
				return (ADObjectId)this[RootDseSchema.Site];
			}
		}

		// Token: 0x04002A1C RID: 10780
		private static RootDseSchema schema = ObjectSchema.GetInstance<RootDseSchema>();

		// Token: 0x04002A1D RID: 10781
		private static string mostDerivedClass = "*";
	}
}
