using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ActivityLog
{
	// Token: 0x02000342 RID: 834
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ActivitySchema : Schema
	{
		// Token: 0x060024FA RID: 9466 RVA: 0x00095340 File Offset: 0x00093540
		static ActivitySchema()
		{
			Dictionary<string, PropertyDefinition> dictionary = new Dictionary<string, PropertyDefinition>(ActivitySchema.hookablePropertyCollection.Value.Count);
			foreach (PropertyDefinition propertyDefinition in ActivitySchema.hookablePropertyCollection.Value)
			{
				dictionary.Add(propertyDefinition.Name, propertyDefinition);
			}
			ActivitySchema.hookablePropertyNameToPropertyDefinitionMapping = Hookable<ReadOnlyDictionary<string, PropertyDefinition>>.Create(true, new ReadOnlyDictionary<string, PropertyDefinition>(dictionary));
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x060024FB RID: 9467 RVA: 0x000954B0 File Offset: 0x000936B0
		public new static ActivitySchema Instance
		{
			get
			{
				return ActivitySchema.instance;
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x060024FC RID: 9468 RVA: 0x000954B7 File Offset: 0x000936B7
		public static Hookable<ReadOnlyCollection<PropertyDefinition>> HookablePropertyCollection
		{
			get
			{
				return ActivitySchema.hookablePropertyCollection;
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x060024FD RID: 9469 RVA: 0x000954BE File Offset: 0x000936BE
		public static Hookable<ReadOnlyDictionary<string, PropertyDefinition>> HookablePropertyNameToPropertyDefinitionMapping
		{
			get
			{
				return ActivitySchema.hookablePropertyNameToPropertyDefinitionMapping;
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x060024FE RID: 9470 RVA: 0x000954C5 File Offset: 0x000936C5
		public static ReadOnlyCollection<PropertyDefinition> PropertyCollection
		{
			get
			{
				return ActivitySchema.hookablePropertyCollection.Value;
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x060024FF RID: 9471 RVA: 0x000954D1 File Offset: 0x000936D1
		public static ReadOnlyDictionary<string, PropertyDefinition> PropertyNameToPropertyDefinitionMapping
		{
			get
			{
				return ActivitySchema.hookablePropertyNameToPropertyDefinitionMapping.Value;
			}
		}

		// Token: 0x04001666 RID: 5734
		public static readonly StorePropertyDefinition ActivityId = InternalSchema.ActivityId;

		// Token: 0x04001667 RID: 5735
		public static readonly StorePropertyDefinition ClientId = InternalSchema.ActivityClientId;

		// Token: 0x04001668 RID: 5736
		public static readonly StorePropertyDefinition ItemId = InternalSchema.ActivityItemId;

		// Token: 0x04001669 RID: 5737
		public static readonly StorePropertyDefinition TimeStamp = InternalSchema.ActivityTimeStamp;

		// Token: 0x0400166A RID: 5738
		public static readonly StorePropertyDefinition SessionId = InternalSchema.ActivitySessionId;

		// Token: 0x0400166B RID: 5739
		public static readonly StorePropertyDefinition LocaleId = InternalSchema.ActivityLocaleId;

		// Token: 0x0400166C RID: 5740
		public static readonly StorePropertyDefinition PreviousItemId = InternalSchema.ActivityGlobalObjectIdBytes;

		// Token: 0x0400166D RID: 5741
		private static readonly StorePropertyDefinition DeleteType = InternalSchema.ActivityDeleteType;

		// Token: 0x0400166E RID: 5742
		private static readonly StorePropertyDefinition WindowId = InternalSchema.ActivityWindowId;

		// Token: 0x0400166F RID: 5743
		private static readonly StorePropertyDefinition FolderId = InternalSchema.ActivityFolderId;

		// Token: 0x04001670 RID: 5744
		private static readonly StorePropertyDefinition OofEnabled = InternalSchema.ActivityOofEnabled;

		// Token: 0x04001671 RID: 5745
		private static readonly StorePropertyDefinition Browser = InternalSchema.ActivityBrowser;

		// Token: 0x04001672 RID: 5746
		private static readonly StorePropertyDefinition Location = InternalSchema.ActivityLocation;

		// Token: 0x04001673 RID: 5747
		private static readonly StorePropertyDefinition ConversationId = InternalSchema.ActivityConversationId;

		// Token: 0x04001674 RID: 5748
		private static readonly StorePropertyDefinition IpAddress = InternalSchema.ActivityIpAddress;

		// Token: 0x04001675 RID: 5749
		private static readonly StorePropertyDefinition TimeZone = InternalSchema.ActivityTimeZone;

		// Token: 0x04001676 RID: 5750
		private static readonly StorePropertyDefinition Category = InternalSchema.ActivityCategory;

		// Token: 0x04001677 RID: 5751
		public static readonly StorePropertyDefinition CustomProperties = InternalSchema.ActivityAttachmentIdBytes;

		// Token: 0x04001678 RID: 5752
		private static readonly StorePropertyDefinition ModuleSelected = InternalSchema.ActivityModuleSelected;

		// Token: 0x04001679 RID: 5753
		private static readonly StorePropertyDefinition LayoutType = InternalSchema.ActivityLayoutType;

		// Token: 0x0400167A RID: 5754
		private static ActivitySchema instance = new ActivitySchema();

		// Token: 0x0400167B RID: 5755
		private static readonly Hookable<ReadOnlyCollection<PropertyDefinition>> hookablePropertyCollection = Hookable<ReadOnlyCollection<PropertyDefinition>>.Create(true, new ReadOnlyCollection<PropertyDefinition>(ActivitySchema.instance.AllProperties.ToList<PropertyDefinition>()));

		// Token: 0x0400167C RID: 5756
		private static readonly Hookable<ReadOnlyDictionary<string, PropertyDefinition>> hookablePropertyNameToPropertyDefinitionMapping;
	}
}
