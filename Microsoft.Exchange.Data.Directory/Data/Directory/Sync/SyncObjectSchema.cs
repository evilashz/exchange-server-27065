using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory.DirSync;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000814 RID: 2068
	internal abstract class SyncObjectSchema : ADObjectSchema
	{
		// Token: 0x06006638 RID: 26168 RVA: 0x001694AF File Offset: 0x001676AF
		public SyncObjectSchema()
		{
			SyncObjectSchema.InitializeSyncPropertyCollections(base.AllProperties, out this.allLinkedProperties, out this.allForwardSyncProperties, out this.allForwardSyncLinkedProperties, out this.allBackSyncProperties, out this.allShadowProperties);
		}

		// Token: 0x1700240E RID: 9230
		// (get) Token: 0x06006639 RID: 26169 RVA: 0x001694E0 File Offset: 0x001676E0
		public ReadOnlyCollection<SyncPropertyDefinition> AllLinkedProperties
		{
			get
			{
				return this.allLinkedProperties;
			}
		}

		// Token: 0x1700240F RID: 9231
		// (get) Token: 0x0600663A RID: 26170 RVA: 0x001694E8 File Offset: 0x001676E8
		public ReadOnlyCollection<SyncPropertyDefinition> AllForwardSyncProperties
		{
			get
			{
				return this.allForwardSyncProperties;
			}
		}

		// Token: 0x17002410 RID: 9232
		// (get) Token: 0x0600663B RID: 26171 RVA: 0x001694F0 File Offset: 0x001676F0
		public ReadOnlyCollection<SyncPropertyDefinition> AllForwardSyncLinkedProperties
		{
			get
			{
				return this.allForwardSyncLinkedProperties;
			}
		}

		// Token: 0x17002411 RID: 9233
		// (get) Token: 0x0600663C RID: 26172 RVA: 0x001694F8 File Offset: 0x001676F8
		public ReadOnlyCollection<SyncPropertyDefinition> AllBackSyncProperties
		{
			get
			{
				return this.allBackSyncProperties;
			}
		}

		// Token: 0x17002412 RID: 9234
		// (get) Token: 0x0600663D RID: 26173 RVA: 0x00169500 File Offset: 0x00167700
		public ReadOnlyCollection<SyncPropertyDefinition> AllShadowProperties
		{
			get
			{
				return this.allShadowProperties;
			}
		}

		// Token: 0x17002413 RID: 9235
		// (get) Token: 0x0600663E RID: 26174
		public abstract DirectoryObjectClass DirectoryObjectClass { get; }

		// Token: 0x0600663F RID: 26175 RVA: 0x00169508 File Offset: 0x00167708
		public static void InitializeSyncPropertyCollections(ICollection<PropertyDefinition> allProperties, out ReadOnlyCollection<SyncPropertyDefinition> allLinkedProperties, out ReadOnlyCollection<SyncPropertyDefinition> allForwardSyncProperties, out ReadOnlyCollection<SyncPropertyDefinition> allForwardSyncLinkedProperties, out ReadOnlyCollection<SyncPropertyDefinition> allBackSyncProperties, out ReadOnlyCollection<SyncPropertyDefinition> allShadowProperties)
		{
			List<SyncPropertyDefinition> list = new List<SyncPropertyDefinition>();
			List<SyncPropertyDefinition> list2 = new List<SyncPropertyDefinition>();
			List<SyncPropertyDefinition> list3 = new List<SyncPropertyDefinition>();
			List<SyncPropertyDefinition> list4 = new List<SyncPropertyDefinition>();
			List<SyncPropertyDefinition> list5 = new List<SyncPropertyDefinition>();
			foreach (PropertyDefinition propertyDefinition in allProperties)
			{
				ADPropertyDefinition adpropertyDefinition = (ADPropertyDefinition)propertyDefinition;
				SyncPropertyDefinition syncPropertyDefinition = adpropertyDefinition as SyncPropertyDefinition;
				if (syncPropertyDefinition != null)
				{
					if (syncPropertyDefinition.IsSyncLink)
					{
						list.Add(syncPropertyDefinition);
						if (syncPropertyDefinition.IsForwardSync)
						{
							list3.Add(syncPropertyDefinition);
						}
					}
					if (syncPropertyDefinition.IsForwardSync)
					{
						list2.Add(syncPropertyDefinition);
					}
					if (syncPropertyDefinition.IsBackSync)
					{
						list4.Add(syncPropertyDefinition);
					}
					if (syncPropertyDefinition.IsShadow)
					{
						list5.Add(syncPropertyDefinition);
					}
				}
			}
			allLinkedProperties = new ReadOnlyCollection<SyncPropertyDefinition>(list.ToArray());
			allForwardSyncProperties = new ReadOnlyCollection<SyncPropertyDefinition>(list2.ToArray());
			allForwardSyncLinkedProperties = new ReadOnlyCollection<SyncPropertyDefinition>(list3.ToArray());
			allBackSyncProperties = new ReadOnlyCollection<SyncPropertyDefinition>(list4.ToArray());
			allShadowProperties = new ReadOnlyCollection<SyncPropertyDefinition>(list5.ToArray());
		}

		// Token: 0x04004396 RID: 17302
		public static SyncPropertyDefinition All = new SyncPropertyDefinition("All", null, typeof(bool), typeof(bool), SyncPropertyDefinitionFlags.Ignore, SyncPropertyDefinition.InitialSyncPropertySetVersion, false);

		// Token: 0x04004397 RID: 17303
		public static SyncPropertyDefinition ContextId = new SyncPropertyDefinition("ContextId", null, typeof(string), typeof(string), SyncPropertyDefinitionFlags.Immutable, SyncPropertyDefinition.InitialSyncPropertySetVersion, string.Empty);

		// Token: 0x04004398 RID: 17304
		public static SyncPropertyDefinition Deleted = new SyncPropertyDefinition(ADDirSyncResultSchema.IsDeleted, null, typeof(bool), SyncPropertyDefinitionFlags.Ignore | SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.BackSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004399 RID: 17305
		public static SyncPropertyDefinition ObjectId = new SyncPropertyDefinition(ADRecipientSchema.ExternalDirectoryObjectId, null, typeof(string), SyncPropertyDefinitionFlags.Immutable, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400439A RID: 17306
		public static SyncPropertyDefinition SyncObjectId = new SyncPropertyDefinition("SyncObjectId", null, typeof(SyncObjectId), typeof(object), SyncPropertyDefinitionFlags.Immutable | SyncPropertyDefinitionFlags.TaskPopulated, SyncPropertyDefinition.InitialSyncPropertySetVersion, null);

		// Token: 0x0400439B RID: 17307
		public static SyncPropertyDefinition LastKnownParent = new SyncPropertyDefinition(DeletedObjectSchema.LastKnownParent, null, typeof(string), SyncPropertyDefinitionFlags.BackSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400439C RID: 17308
		public static SyncPropertyDefinition UsnChanged = new SyncPropertyDefinition(ADRecipientSchema.UsnChanged, null, typeof(long), SyncPropertyDefinitionFlags.Immutable, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400439D RID: 17309
		public static SyncPropertyDefinition PropertyValidationErrors = new SyncPropertyDefinition("PropertyValidationErrors", null, typeof(ValidationError), typeof(object), SyncPropertyDefinitionFlags.Ignore | SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.InitialSyncPropertySetVersion, null);

		// Token: 0x0400439E RID: 17310
		public static SyncPropertyDefinition FaultInServiceInstance = new SyncPropertyDefinition("FaultInServiceInstance", null, typeof(ServiceInstanceId), typeof(object), SyncPropertyDefinitionFlags.Ignore | SyncPropertyDefinitionFlags.TaskPopulated, SyncPropertyDefinition.InitialSyncPropertySetVersion, null);

		// Token: 0x0400439F RID: 17311
		private ReadOnlyCollection<SyncPropertyDefinition> allLinkedProperties;

		// Token: 0x040043A0 RID: 17312
		private ReadOnlyCollection<SyncPropertyDefinition> allForwardSyncProperties;

		// Token: 0x040043A1 RID: 17313
		private ReadOnlyCollection<SyncPropertyDefinition> allForwardSyncLinkedProperties;

		// Token: 0x040043A2 RID: 17314
		private ReadOnlyCollection<SyncPropertyDefinition> allBackSyncProperties;

		// Token: 0x040043A3 RID: 17315
		private ReadOnlyCollection<SyncPropertyDefinition> allShadowProperties;
	}
}
