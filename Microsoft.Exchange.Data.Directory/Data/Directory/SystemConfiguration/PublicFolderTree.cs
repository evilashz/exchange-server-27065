using System;
using System.Security.AccessControl;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000548 RID: 1352
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class PublicFolderTree : ADConfigurationObject
	{
		// Token: 0x1700134D RID: 4941
		// (get) Token: 0x06003C80 RID: 15488 RVA: 0x000E75BC File Offset: 0x000E57BC
		internal override ADObjectSchema Schema
		{
			get
			{
				return PublicFolderTree.schema;
			}
		}

		// Token: 0x1700134E RID: 4942
		// (get) Token: 0x06003C81 RID: 15489 RVA: 0x000E75C3 File Offset: 0x000E57C3
		internal override string MostDerivedObjectClass
		{
			get
			{
				return PublicFolderTree.MostDerivedClass;
			}
		}

		// Token: 0x1700134F RID: 4943
		// (get) Token: 0x06003C82 RID: 15490 RVA: 0x000E75CA File Offset: 0x000E57CA
		// (set) Token: 0x06003C83 RID: 15491 RVA: 0x000E75DC File Offset: 0x000E57DC
		public PublicFolderTreeType PublicFolderTreeType
		{
			get
			{
				return (PublicFolderTreeType)this[PublicFolderTreeSchema.PublicFolderTreeType];
			}
			internal set
			{
				this[PublicFolderTreeSchema.PublicFolderTreeType] = value;
			}
		}

		// Token: 0x17001350 RID: 4944
		// (get) Token: 0x06003C84 RID: 15492 RVA: 0x000E75EF File Offset: 0x000E57EF
		public MultiValuedProperty<ADObjectId> PublicDatabases
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[PublicFolderTreeSchema.PublicDatabases];
			}
		}

		// Token: 0x06003C85 RID: 15493 RVA: 0x000E7601 File Offset: 0x000E5801
		internal void SetPublicFolderDefaultAdminAcl(RawSecurityDescriptor rawSecurityDescriptor)
		{
			this[PublicFolderTreeSchema.PublicFolderDefaultAdminAcl] = rawSecurityDescriptor;
		}

		// Token: 0x06003C86 RID: 15494 RVA: 0x000E7610 File Offset: 0x000E5810
		internal override void StampPersistableDefaultValues()
		{
			if (!base.IsModified(ADConfigurationObjectSchema.AdminDisplayName))
			{
				base.AdminDisplayName = PublicFolderTree.DefaultName;
			}
			if (!base.IsModified(PublicFolderTreeSchema.PublicFolderTreeType))
			{
				this.PublicFolderTreeType = PublicFolderTreeType.Mapi;
			}
			if (!base.IsModified(ADConfigurationObjectSchema.SystemFlags))
			{
				this[ADConfigurationObjectSchema.SystemFlags] = (SystemFlagsEnum.Movable | SystemFlagsEnum.Renamable);
			}
			base.StampPersistableDefaultValues();
		}

		// Token: 0x040028EB RID: 10475
		public static readonly string DefaultName = "Public Folders";

		// Token: 0x040028EC RID: 10476
		private static PublicFolderTreeSchema schema = ObjectSchema.GetInstance<PublicFolderTreeSchema>();

		// Token: 0x040028ED RID: 10477
		internal static readonly string MostDerivedClass = "msExchPFTree";
	}
}
