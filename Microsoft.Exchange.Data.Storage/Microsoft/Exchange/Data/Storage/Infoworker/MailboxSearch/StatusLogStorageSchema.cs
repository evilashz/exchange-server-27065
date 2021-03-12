using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x02000D15 RID: 3349
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class StatusLogStorageSchema
	{
		// Token: 0x060073D8 RID: 29656 RVA: 0x00202252 File Offset: 0x00200452
		public static StoreObjectId FindChildFolderByName(Folder folder, string name)
		{
			return folder.FindChildFolderByName(name);
		}

		// Token: 0x040050E3 RID: 20707
		public const string StatusLogItemClass = "IPM.Configuration.MailboxDiscoverySearch.StatusLog";

		// Token: 0x040050E4 RID: 20708
		public static readonly StorePropertyDefinition NameProperty = InternalSchema.Subject;

		// Token: 0x040050E5 RID: 20709
		public static readonly StorePropertyDefinition ItemClassProperty = InternalSchema.ItemClass;

		// Token: 0x040050E6 RID: 20710
		public static readonly StorePropertyDefinition ItemIdProperty = InternalSchema.ItemId;

		// Token: 0x040050E7 RID: 20711
		public static readonly GuidNamePropertyDefinition OperationStatusProperty = GuidNamePropertyDefinition.InternalCreate("OperationStatus", typeof(int), PropType.Int, MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "OperationStatus", PropertyFlags.None, NativeStorePropertyDefinition.TypeCheckingFlag.AllowCompatibleType, false, PropertyDefinitionConstraint.None);

		// Token: 0x040050E8 RID: 20712
		public static readonly GuidNamePropertyDefinition ExportSettingsProperty = GuidNamePropertyDefinition.InternalCreate("ExportSettings", typeof(byte[]), PropType.Binary, MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "ExportSettings", PropertyFlags.None, NativeStorePropertyDefinition.TypeCheckingFlag.DisableTypeCheck, false, PropertyDefinitionConstraint.None);
	}
}
