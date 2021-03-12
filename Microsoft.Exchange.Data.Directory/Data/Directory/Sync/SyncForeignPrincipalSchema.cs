using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000828 RID: 2088
	internal class SyncForeignPrincipalSchema : SyncObjectSchema
	{
		// Token: 0x170024B0 RID: 9392
		// (get) Token: 0x060067B1 RID: 26545 RVA: 0x0016DD41 File Offset: 0x0016BF41
		public override DirectoryObjectClass DirectoryObjectClass
		{
			get
			{
				return DirectoryObjectClass.ForeignPrincipal;
			}
		}

		// Token: 0x04004448 RID: 17480
		public static SyncPropertyDefinition DisplayName = new SyncPropertyDefinition(ADRecipientSchema.DisplayName, "DisplayName", typeof(DirectoryPropertyStringSingleLength1To256), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004449 RID: 17481
		public static SyncPropertyDefinition Description = new SyncPropertyDefinition("Description", "Description", typeof(string), typeof(DirectoryPropertyStringSingleLength1To1024), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.FilteringOnly, SyncPropertyDefinition.SyncPropertySetVersion3, string.Empty);

		// Token: 0x0400444A RID: 17482
		public static SyncPropertyDefinition LinkedPartnerGroupId = new SyncPropertyDefinition(ADGroupSchema.LinkedPartnerGroupId, "ForeignPrincipalId", typeof(DirectoryPropertyGuidSingle), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400444B RID: 17483
		public static SyncPropertyDefinition LinkedPartnerOrganizationId = new SyncPropertyDefinition(ADGroupSchema.LinkedPartnerOrganizationId, "ForeignContextId", typeof(DirectoryPropertyGuidSingle), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);
	}
}
