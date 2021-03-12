using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.Rus
{
	// Token: 0x020001CE RID: 462
	internal class RusConfig : ADObject
	{
		// Token: 0x06001368 RID: 4968 RVA: 0x0003ABDF File Offset: 0x00038DDF
		public RusConfig()
		{
			base.SetId(new ADObjectId(DalHelper.GetTenantDistinguishedName("RUS_Default"), CombGuidGenerator.NewGuid()));
			base.Name = "RUS_Default";
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x0003AC0C File Offset: 0x00038E0C
		// (set) Token: 0x0600136A RID: 4970 RVA: 0x0003AC1E File Offset: 0x00038E1E
		public string UniversalManifestVersion
		{
			get
			{
				return (string)this[RusConfigSchema.UniversalManifestVersion];
			}
			set
			{
				this[RusConfigSchema.UniversalManifestVersion] = value;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x0003AC2C File Offset: 0x00038E2C
		// (set) Token: 0x0600136C RID: 4972 RVA: 0x0003AC3E File Offset: 0x00038E3E
		public string UniversalManifestVersionV2
		{
			get
			{
				return (string)this[RusConfigSchema.UniversalManifestVersionV2];
			}
			set
			{
				this[RusConfigSchema.UniversalManifestVersionV2] = value;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x0600136D RID: 4973 RVA: 0x0003AC4C File Offset: 0x00038E4C
		internal override ADObjectSchema Schema
		{
			get
			{
				return RusConfig.schema;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x0003AC53 File Offset: 0x00038E53
		internal override string MostDerivedObjectClass
		{
			get
			{
				return RusConfig.mostDerivedClass;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x0600136F RID: 4975 RVA: 0x0003AC5A File Offset: 0x00038E5A
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04000957 RID: 2391
		public const string RusDistinguishedName = "RUS_Default";

		// Token: 0x04000958 RID: 2392
		private static readonly string mostDerivedClass = "RusConfig";

		// Token: 0x04000959 RID: 2393
		private static readonly RusConfigSchema schema = ObjectSchema.GetInstance<RusConfigSchema>();
	}
}
