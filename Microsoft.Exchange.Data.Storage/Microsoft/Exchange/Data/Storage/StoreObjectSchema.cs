using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000934 RID: 2356
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class StoreObjectSchema : Schema
	{
		// Token: 0x060057D6 RID: 22486 RVA: 0x001692A8 File Offset: 0x001674A8
		protected StoreObjectSchema()
		{
			base.AddDependencies(new Schema[]
			{
				CoreObjectSchema.Instance
			});
		}

		// Token: 0x1700185F RID: 6239
		// (get) Token: 0x060057D7 RID: 22487 RVA: 0x001692D1 File Offset: 0x001674D1
		public new static StoreObjectSchema Instance
		{
			get
			{
				if (StoreObjectSchema.instance == null)
				{
					StoreObjectSchema.instance = new StoreObjectSchema();
				}
				return StoreObjectSchema.instance;
			}
		}

		// Token: 0x04002EEE RID: 12014
		[Autoload]
		public static readonly StorePropertyDefinition ChangeKey = CoreObjectSchema.ChangeKey;

		// Token: 0x04002EEF RID: 12015
		[Autoload]
		public static readonly StorePropertyDefinition ContainerClass = InternalSchema.ContainerClass;

		// Token: 0x04002EF0 RID: 12016
		[Autoload]
		public static readonly StorePropertyDefinition CreationTime = CoreObjectSchema.CreationTime;

		// Token: 0x04002EF1 RID: 12017
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition DisplayName = InternalSchema.DisplayName;

		// Token: 0x04002EF2 RID: 12018
		[Autoload]
		public static readonly StorePropertyDefinition EntryId = CoreObjectSchema.EntryId;

		// Token: 0x04002EF3 RID: 12019
		[Autoload]
		public static readonly StorePropertyDefinition ItemClass = CoreItemSchema.ItemClass;

		// Token: 0x04002EF4 RID: 12020
		[Autoload]
		public static readonly StorePropertyDefinition ContentClass = CoreObjectSchema.ContentClass;

		// Token: 0x04002EF5 RID: 12021
		[Autoload]
		public static readonly StorePropertyDefinition IsRestricted = InternalSchema.IsRestricted;

		// Token: 0x04002EF6 RID: 12022
		[Autoload]
		public static readonly StorePropertyDefinition LastModifiedTime = InternalSchema.LastModifiedTime;

		// Token: 0x04002EF7 RID: 12023
		[Autoload]
		public static readonly StorePropertyDefinition ParentEntryId = CoreObjectSchema.ParentEntryId;

		// Token: 0x04002EF8 RID: 12024
		[Autoload]
		public static readonly StorePropertyDefinition SearchKey = InternalSchema.SearchKey;

		// Token: 0x04002EF9 RID: 12025
		[Autoload]
		public static readonly StorePropertyDefinition ParentItemId = InternalSchema.ParentItemId;

		// Token: 0x04002EFA RID: 12026
		[Autoload]
		public static readonly StorePropertyDefinition RecordKey = InternalSchema.RecordKey;

		// Token: 0x04002EFB RID: 12027
		public static readonly StorePropertyDefinition MapiStoreEntryId = InternalSchema.StoreEntryId;

		// Token: 0x04002EFC RID: 12028
		internal static readonly StorePropertyDefinition SourceKey = CoreObjectSchema.SourceKey;

		// Token: 0x04002EFD RID: 12029
		internal static readonly StorePropertyDefinition ParentSourceKey = CoreObjectSchema.ParentSourceKey;

		// Token: 0x04002EFE RID: 12030
		internal static readonly StorePropertyDefinition PredecessorChangeList = CoreObjectSchema.PredecessorChangeList;

		// Token: 0x04002EFF RID: 12031
		public static readonly StorePropertyDefinition[] ContentConversionProperties = CoreObjectSchema.AllPropertiesOnStore;

		// Token: 0x04002F00 RID: 12032
		public static readonly StorePropertyDefinition EffectiveRights = InternalSchema.EffectiveRights;

		// Token: 0x04002F01 RID: 12033
		public static readonly StorePropertyDefinition DeletedOnTime = CoreObjectSchema.DeletedOnTime;

		// Token: 0x04002F02 RID: 12034
		public static readonly StorePropertyDefinition IsSoftDeleted = InternalSchema.IsSoftDeleted;

		// Token: 0x04002F03 RID: 12035
		public static readonly StorePropertyDefinition PolicyTag = InternalSchema.PolicyTag;

		// Token: 0x04002F04 RID: 12036
		public static readonly StorePropertyDefinition ExplicitPolicyTag = InternalSchema.ExplicitPolicyTag;

		// Token: 0x04002F05 RID: 12037
		public static readonly StorePropertyDefinition ArchiveTag = InternalSchema.ArchiveTag;

		// Token: 0x04002F06 RID: 12038
		public static readonly StorePropertyDefinition ExplicitArchiveTag = InternalSchema.ExplicitArchiveTag;

		// Token: 0x04002F07 RID: 12039
		public static readonly StorePropertyDefinition RetentionPeriod = InternalSchema.RetentionPeriod;

		// Token: 0x04002F08 RID: 12040
		public static readonly StorePropertyDefinition ArchivePeriod = InternalSchema.ArchivePeriod;

		// Token: 0x04002F09 RID: 12041
		public static readonly StorePropertyDefinition PhoneticDisplayName = InternalSchema.PhoneticDisplayName;

		// Token: 0x04002F0A RID: 12042
		[Autoload]
		public static readonly StorePropertyDefinition RetentionFlags = InternalSchema.RetentionFlags;

		// Token: 0x04002F0B RID: 12043
		private static StoreObjectSchema instance = null;
	}
}
