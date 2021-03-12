using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200042D RID: 1069
	internal class MapiFolderConfigurationSchema : ObjectSchema
	{
		// Token: 0x04001D52 RID: 7506
		public static readonly SimpleProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2003, typeof(MailboxFolderId), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D53 RID: 7507
		public static readonly SimpleProviderPropertyDefinition ObjectState = new SimpleProviderPropertyDefinition("ObjectState", ExchangeObjectVersion.Exchange2003, typeof(ObjectState), PropertyDefinitionFlags.None, Microsoft.Exchange.Data.ObjectState.New, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D54 RID: 7508
		public static readonly SimpleProviderPropertyDefinition ExchangeVersion = new SimpleProviderPropertyDefinition("ExchangeVersion", ExchangeObjectVersion.Exchange2003, typeof(ExchangeObjectVersion), PropertyDefinitionFlags.None, ExchangeObjectVersion.Exchange2010, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D55 RID: 7509
		public static readonly SimpleProviderPropertyDefinition Date = new SimpleProviderPropertyDefinition("Date", ExchangeObjectVersion.Exchange2003, typeof(DateTime?), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D56 RID: 7510
		public static readonly SimpleProviderPropertyDefinition Name = new SimpleProviderPropertyDefinition("Name", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D57 RID: 7511
		public static readonly SimpleProviderPropertyDefinition FolderPath = new SimpleProviderPropertyDefinition("FolderPath", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D58 RID: 7512
		public static readonly SimpleProviderPropertyDefinition FolderId = new SimpleProviderPropertyDefinition("FolderId", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D59 RID: 7513
		public static readonly SimpleProviderPropertyDefinition FolderType = new SimpleProviderPropertyDefinition("FolderType", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D5A RID: 7514
		public static readonly SimpleProviderPropertyDefinition ItemsInFolder = new SimpleProviderPropertyDefinition("ItemsInFolder", ExchangeObjectVersion.Exchange2003, typeof(int?), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D5B RID: 7515
		public static readonly SimpleProviderPropertyDefinition DeletedItemsInFolder = new SimpleProviderPropertyDefinition("DeleteItemsInFolder", ExchangeObjectVersion.Exchange2003, typeof(int?), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D5C RID: 7516
		public static readonly SimpleProviderPropertyDefinition FolderSize = new SimpleProviderPropertyDefinition("FolderSize", ExchangeObjectVersion.Exchange2003, typeof(ByteQuantifiedSize?), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D5D RID: 7517
		public static readonly SimpleProviderPropertyDefinition ItemsInFolderAndSubfolders = new SimpleProviderPropertyDefinition("ItemsInFolderAndSubfolders", ExchangeObjectVersion.Exchange2003, typeof(int?), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D5E RID: 7518
		public static readonly SimpleProviderPropertyDefinition DeletedItemsInFolderAndSubfolders = new SimpleProviderPropertyDefinition("DeletedItemsInFolderAndSubfolders", ExchangeObjectVersion.Exchange2003, typeof(int?), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D5F RID: 7519
		public static readonly SimpleProviderPropertyDefinition FolderAndSubfolderSize = new SimpleProviderPropertyDefinition("FolderAndSubfolderSize", ExchangeObjectVersion.Exchange2003, typeof(ByteQuantifiedSize?), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D60 RID: 7520
		public static readonly SimpleProviderPropertyDefinition OldestItemReceivedDate = new SimpleProviderPropertyDefinition("OldestItemReceivedDate", ExchangeObjectVersion.Exchange2003, typeof(DateTime?), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D61 RID: 7521
		public static readonly SimpleProviderPropertyDefinition NewestItemReceivedDate = new SimpleProviderPropertyDefinition("NewestItemReceivedDate", ExchangeObjectVersion.Exchange2003, typeof(DateTime?), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D62 RID: 7522
		public static readonly SimpleProviderPropertyDefinition OldestDeletedItemReceivedDate = new SimpleProviderPropertyDefinition("OldestDeletedItemReceivedDate", ExchangeObjectVersion.Exchange2003, typeof(DateTime?), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D63 RID: 7523
		public static readonly SimpleProviderPropertyDefinition NewestDeletedItemReceivedDate = new SimpleProviderPropertyDefinition("NewestDeletedItemReceivedDate", ExchangeObjectVersion.Exchange2003, typeof(DateTime?), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D64 RID: 7524
		public static readonly SimpleProviderPropertyDefinition OldestItemLastModifiedDate = new SimpleProviderPropertyDefinition("OldestItemLastModifiedDate", ExchangeObjectVersion.Exchange2003, typeof(DateTime?), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D65 RID: 7525
		public static readonly SimpleProviderPropertyDefinition NewestItemLastModifiedDate = new SimpleProviderPropertyDefinition("NewestItemLastModifiedDate", ExchangeObjectVersion.Exchange2003, typeof(DateTime?), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D66 RID: 7526
		public static readonly SimpleProviderPropertyDefinition OldestDeletedItemLastModifiedDate = new SimpleProviderPropertyDefinition("OldestDeletedItemLastModifiedDate", ExchangeObjectVersion.Exchange2003, typeof(DateTime?), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D67 RID: 7527
		public static readonly SimpleProviderPropertyDefinition NewestDeletedItemLastModifiedDate = new SimpleProviderPropertyDefinition("NewestDeletedItemLastModifiedDate", ExchangeObjectVersion.Exchange2003, typeof(DateTime?), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D68 RID: 7528
		public static readonly SimpleProviderPropertyDefinition ManagedFolder = new SimpleProviderPropertyDefinition("ManagedFolder", ExchangeObjectVersion.Exchange2003, typeof(ELCFolderIdParameter), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D69 RID: 7529
		public static readonly SimpleProviderPropertyDefinition DeletePolicy = new SimpleProviderPropertyDefinition("DeletePolicy", ExchangeObjectVersion.Exchange2003, typeof(RetentionPolicyTagIdParameter), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D6A RID: 7530
		public static readonly SimpleProviderPropertyDefinition ArchivePolicy = new SimpleProviderPropertyDefinition("ArchivePolicy", ExchangeObjectVersion.Exchange2003, typeof(RetentionPolicyTagIdParameter), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D6B RID: 7531
		public static readonly SimpleProviderPropertyDefinition TopSubject = new SimpleProviderPropertyDefinition("TopSubject", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D6C RID: 7532
		public static readonly SimpleProviderPropertyDefinition TopSubjectSize = new SimpleProviderPropertyDefinition("TopSubjectSize", ExchangeObjectVersion.Exchange2003, typeof(ByteQuantifiedSize?), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D6D RID: 7533
		public static readonly SimpleProviderPropertyDefinition TopSubjectCount = new SimpleProviderPropertyDefinition("TopSubjectCount", ExchangeObjectVersion.Exchange2003, typeof(int?), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D6E RID: 7534
		public static readonly SimpleProviderPropertyDefinition TopSubjectClass = new SimpleProviderPropertyDefinition("TopSubjectClass", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D6F RID: 7535
		public static readonly SimpleProviderPropertyDefinition TopSubjectPath = new SimpleProviderPropertyDefinition("TopSubjectPath", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D70 RID: 7536
		public static readonly SimpleProviderPropertyDefinition TopSubjectReceivedTime = new SimpleProviderPropertyDefinition("TopSubjectReceivedTime", ExchangeObjectVersion.Exchange2003, typeof(DateTime?), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D71 RID: 7537
		public static readonly SimpleProviderPropertyDefinition TopSubjectFrom = new SimpleProviderPropertyDefinition("TopSubjectFrom", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D72 RID: 7538
		public static readonly SimpleProviderPropertyDefinition TopClientInfoForSubject = new SimpleProviderPropertyDefinition("TopClientInfoForSubject", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D73 RID: 7539
		public static readonly SimpleProviderPropertyDefinition TopClientInfoCountForSubject = new SimpleProviderPropertyDefinition("TopClientInfoCountForSubject", ExchangeObjectVersion.Exchange2003, typeof(int?), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001D74 RID: 7540
		public static readonly SimpleProviderPropertyDefinition SearchFolders = new SimpleProviderPropertyDefinition("SearchFolders", ExchangeObjectVersion.Exchange2003, typeof(string[]), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
