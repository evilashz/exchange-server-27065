using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200054A RID: 1354
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class PublicFolderTreeContainer : ADLegacyVersionableObject
	{
		// Token: 0x17001351 RID: 4945
		// (get) Token: 0x06003C8A RID: 15498 RVA: 0x000E76A1 File Offset: 0x000E58A1
		internal override ADObjectSchema Schema
		{
			get
			{
				return PublicFolderTreeContainer.schema;
			}
		}

		// Token: 0x17001352 RID: 4946
		// (get) Token: 0x06003C8B RID: 15499 RVA: 0x000E76A8 File Offset: 0x000E58A8
		internal override string MostDerivedObjectClass
		{
			get
			{
				return PublicFolderTreeContainer.mostDerivedClass;
			}
		}

		// Token: 0x06003C8D RID: 15501 RVA: 0x000E76B7 File Offset: 0x000E58B7
		internal override void StampPersistableDefaultValues()
		{
			if (!base.IsModified(ADConfigurationObjectSchema.AdminDisplayName))
			{
				base.AdminDisplayName = PublicFolderTreeContainer.DefaultName;
			}
			if (!base.IsModified(ADConfigurationObjectSchema.SystemFlags))
			{
				this[ADConfigurationObjectSchema.SystemFlags] = SystemFlagsEnum.None;
			}
			base.StampPersistableDefaultValues();
		}

		// Token: 0x040028EE RID: 10478
		public static readonly string DefaultName = "Folder Hierarchies";

		// Token: 0x040028EF RID: 10479
		private static PublicFolderTreeContainerSchema schema = ObjectSchema.GetInstance<PublicFolderTreeContainerSchema>();

		// Token: 0x040028F0 RID: 10480
		private static string mostDerivedClass = "msExchContainer";
	}
}
