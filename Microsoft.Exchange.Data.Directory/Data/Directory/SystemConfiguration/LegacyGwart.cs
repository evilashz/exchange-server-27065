using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000497 RID: 1175
	[Serializable]
	public class LegacyGwart : ADConfigurationObject
	{
		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x06003564 RID: 13668 RVA: 0x000D23AC File Offset: 0x000D05AC
		internal override ADObjectSchema Schema
		{
			get
			{
				return LegacyGwart.schema;
			}
		}

		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x06003565 RID: 13669 RVA: 0x000D23B3 File Offset: 0x000D05B3
		// (set) Token: 0x06003566 RID: 13670 RVA: 0x000D23C5 File Offset: 0x000D05C5
		public DateTime? GwartLastModified
		{
			get
			{
				return (DateTime?)this[LegacyGwartSchema.GwartLastModified];
			}
			internal set
			{
				this[LegacyGwartSchema.GwartLastModified] = value;
			}
		}

		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x06003567 RID: 13671 RVA: 0x000D23D8 File Offset: 0x000D05D8
		internal override string MostDerivedObjectClass
		{
			get
			{
				return LegacyGwart.mostDerivedClass;
			}
		}

		// Token: 0x04002418 RID: 9240
		private static LegacyGwartSchema schema = ObjectSchema.GetInstance<LegacyGwartSchema>();

		// Token: 0x04002419 RID: 9241
		private static string mostDerivedClass = "siteAddressing";

		// Token: 0x0400241A RID: 9242
		public static readonly string DefaultName = "Legacy GWART";
	}
}
