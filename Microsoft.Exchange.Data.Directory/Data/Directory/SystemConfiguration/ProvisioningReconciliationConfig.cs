using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000542 RID: 1346
	[Serializable]
	public sealed class ProvisioningReconciliationConfig : ADConfigurationObject
	{
		// Token: 0x17001336 RID: 4918
		// (get) Token: 0x06003C49 RID: 15433 RVA: 0x000E687C File Offset: 0x000E4A7C
		internal override ADObjectSchema Schema
		{
			get
			{
				return ProvisioningReconciliationConfig.schema;
			}
		}

		// Token: 0x17001337 RID: 4919
		// (get) Token: 0x06003C4A RID: 15434 RVA: 0x000E6883 File Offset: 0x000E4A83
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ProvisioningReconciliationConfig.mostDerivedClass;
			}
		}

		// Token: 0x17001338 RID: 4920
		// (get) Token: 0x06003C4B RID: 15435 RVA: 0x000E688A File Offset: 0x000E4A8A
		internal override ADObjectId ParentPath
		{
			get
			{
				return ProvisioningReconciliationConfig.parentPath;
			}
		}

		// Token: 0x17001339 RID: 4921
		// (get) Token: 0x06003C4C RID: 15436 RVA: 0x000E6891 File Offset: 0x000E4A91
		// (set) Token: 0x06003C4D RID: 15437 RVA: 0x000E68A3 File Offset: 0x000E4AA3
		public MultiValuedProperty<ReconciliationCookie> ReconciliationCookies
		{
			get
			{
				return (MultiValuedProperty<ReconciliationCookie>)this[ProvisioningReconciliationConfigSchema.ReconciliationCookies];
			}
			set
			{
				this[ProvisioningReconciliationConfigSchema.ReconciliationCookies] = value;
			}
		}

		// Token: 0x1700133A RID: 4922
		// (get) Token: 0x06003C4E RID: 15438 RVA: 0x000E68B1 File Offset: 0x000E4AB1
		// (set) Token: 0x06003C4F RID: 15439 RVA: 0x000E68C3 File Offset: 0x000E4AC3
		public MultiValuedProperty<ReconciliationCookie> ReconciliationCookiesForNextCycle
		{
			get
			{
				return (MultiValuedProperty<ReconciliationCookie>)this[ProvisioningReconciliationConfigSchema.ReconciliationCookiesForNextCycle];
			}
			internal set
			{
				this[ProvisioningReconciliationConfigSchema.ReconciliationCookiesForNextCycle] = value;
			}
		}

		// Token: 0x1700133B RID: 4923
		// (get) Token: 0x06003C50 RID: 15440 RVA: 0x000E68D1 File Offset: 0x000E4AD1
		// (set) Token: 0x06003C51 RID: 15441 RVA: 0x000E68E3 File Offset: 0x000E4AE3
		public ReconciliationCookie ReconciliationCookieForCurrentCycle
		{
			get
			{
				return (ReconciliationCookie)this[ProvisioningReconciliationConfigSchema.ReconciliationCookieForCurrentCycle];
			}
			internal set
			{
				this[ProvisioningReconciliationConfigSchema.ReconciliationCookieForCurrentCycle] = value;
			}
		}

		// Token: 0x040028CB RID: 10443
		private static ProvisioningReconciliationConfigSchema schema = ObjectSchema.GetInstance<ProvisioningReconciliationConfigSchema>();

		// Token: 0x040028CC RID: 10444
		private static string mostDerivedClass = "msExchReconciliationConfig";

		// Token: 0x040028CD RID: 10445
		private static ADObjectId parentPath = new ADObjectId("CN=Global Settings");

		// Token: 0x040028CE RID: 10446
		public static readonly string CanonicalName = "ProvisioningReconciliationConfig";
	}
}
