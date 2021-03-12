using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage.Contacts.ChangeLogger
{
	// Token: 0x0200055D RID: 1373
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class UcsTracker : IContactChangeTracker
	{
		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x060039DF RID: 14815 RVA: 0x000ECFA2 File Offset: 0x000EB1A2
		public string Name
		{
			get
			{
				return "UCSTracker";
			}
		}

		// Token: 0x060039E0 RID: 14816 RVA: 0x000ECFC4 File Offset: 0x000EB1C4
		public bool ShouldLoadPropertiesForFurtherCheck(COWTriggerAction operation, string itemClass, StoreObjectId itemId, CoreItem item)
		{
			StorePropertyDefinition[] array;
			if (ObjectClass.IsContact(itemClass))
			{
				array = UcsTracker.ContactProperties;
			}
			else if (ObjectClass.IsDistributionList(itemClass))
			{
				array = UcsTracker.PdlProperties;
			}
			else
			{
				if (!ObjectClass.IsContactsFolder(itemClass))
				{
					UcsTracker.Tracer.TraceDebug<string, StoreObjectId>((long)this.GetHashCode(), "UcsTracker.ShouldLogContact: Skipping Item with Class - {0}, Id - {1}", itemClass, itemId);
					return false;
				}
				array = UcsTracker.FolderProperties;
			}
			switch (operation)
			{
			case COWTriggerAction.Create:
			case COWTriggerAction.Update:
			case COWTriggerAction.Copy:
			case COWTriggerAction.Move:
			case COWTriggerAction.MoveToDeletedItems:
			case COWTriggerAction.SoftDelete:
			case COWTriggerAction.HardDelete:
			{
				bool flag = Array.Exists<StorePropertyDefinition>(array, (StorePropertyDefinition property) => item.PropertyBag.IsPropertyDirty(property));
				UcsTracker.Tracer.TraceDebug((long)this.GetHashCode(), "UcsTracker.ShouldLogContact: Operation {0} for item with Class - {1}, Id - {2} - will be logged: {3}", new object[]
				{
					operation,
					itemClass,
					itemId,
					flag
				});
				return flag;
			}
			}
			UcsTracker.Tracer.TraceDebug<COWTriggerAction, string, StoreObjectId>((long)this.GetHashCode(), "UcsTracker.ShouldLogContact: Skipping Operation {0} for item with Class - {1}, Id - {2}", operation, itemClass, itemId);
			return false;
		}

		// Token: 0x060039E1 RID: 14817 RVA: 0x000ED0D0 File Offset: 0x000EB2D0
		public StorePropertyDefinition[] GetProperties(StoreObjectId itemId, CoreItem item)
		{
			string valueOrDefault = item.PropertyBag.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty);
			if (ObjectClass.IsContact(valueOrDefault))
			{
				return UcsTracker.AllContactPropertiesToBeLoaded;
			}
			if (ObjectClass.IsDistributionList(valueOrDefault))
			{
				return UcsTracker.AllPdlPropertiesToBeLoaded;
			}
			if (ObjectClass.IsContactsFolder(valueOrDefault))
			{
				if (this.IsUcsFolder(valueOrDefault))
				{
					return UcsTracker.FolderProperties;
				}
				if (!item.PropertyBag.IsPropertyDirty(StoreObjectSchema.ContainerClass))
				{
					UcsTracker.Tracer.TraceDebug((long)this.GetHashCode(), "UcsTracker.GetProperties: Container class property is not dirty.");
				}
				else
				{
					IValidatablePropertyBag validatablePropertyBag = item.PropertyBag as IValidatablePropertyBag;
					if (validatablePropertyBag == null)
					{
						UcsTracker.Tracer.TraceDebug((long)this.GetHashCode(), "UcsTracker.GetProperties: Skipping retrieval of old value as property bag doesn't track original values.");
					}
					else
					{
						PropertyValueTrackingData originalPropertyInformation = validatablePropertyBag.GetOriginalPropertyInformation(StoreObjectSchema.ContainerClass);
						string containerClass = originalPropertyInformation.OriginalPropertyValue as string;
						if (this.IsUcsFolder(containerClass))
						{
							return UcsTracker.FolderProperties;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060039E2 RID: 14818 RVA: 0x000ED1A8 File Offset: 0x000EB3A8
		public bool ShouldLogContact(StoreObjectId itemId, CoreItem item)
		{
			string text = (string)item.PropertyBag.TryGetProperty(StoreObjectSchema.ItemClass);
			if (!ObjectClass.IsContact(text))
			{
				if (ObjectClass.IsDistributionList(text))
				{
					StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId((byte[])item.PropertyBag.TryGetProperty(StoreObjectSchema.ParentEntryId));
					StoreObjectId defaultFolderId = ((MailboxSession)item.Session).GetDefaultFolderId(DefaultFolderType.ImContactList);
					if (object.Equals(storeObjectId, defaultFolderId))
					{
						return true;
					}
					UcsTracker.Tracer.TraceDebug((long)this.GetHashCode(), "UcsTracker.ShouldLogContact: Skipping PDL with Class - {0}, ParentFolderId - {1}, ImContactListFolderId - {2}, Id - {3}", new object[]
					{
						text,
						storeObjectId,
						defaultFolderId,
						itemId
					});
				}
				return true;
			}
			StoreObjectId storeObjectId2 = StoreObjectId.FromProviderSpecificId((byte[])item.PropertyBag.TryGetProperty(StoreObjectSchema.ParentEntryId));
			StoreObjectId defaultFolderId2 = ((MailboxSession)item.Session).GetDefaultFolderId(DefaultFolderType.QuickContacts);
			if (object.Equals(storeObjectId2, defaultFolderId2))
			{
				return true;
			}
			UcsTracker.Tracer.TraceDebug((long)this.GetHashCode(), "UcsTracker.ShouldLogContact: Skipping contact item with Class - {0}, ParentFolderId - {1}, QuickContactsFolderId - {2}, Id - {3}", new object[]
			{
				text,
				storeObjectId2,
				defaultFolderId2,
				itemId
			});
			return false;
		}

		// Token: 0x060039E3 RID: 14819 RVA: 0x000ED2BC File Offset: 0x000EB4BC
		public bool ShouldLogGroupOperation(COWTriggerAction operation, StoreSession sourceSession, StoreObjectId sourceFolderId, StoreSession destinationSession, StoreObjectId destinationFolderId, ICollection<StoreObjectId> itemIds)
		{
			UcsTracker.Tracer.TraceDebug<COWTriggerAction>((long)this.GetHashCode(), "UcsTracker.ShouldLogGroupOperation: Invoked for operation: {0}", operation);
			switch (operation)
			{
			case COWTriggerAction.Create:
			case COWTriggerAction.Update:
			case COWTriggerAction.Copy:
			case COWTriggerAction.Move:
			case COWTriggerAction.MoveToDeletedItems:
			case COWTriggerAction.SoftDelete:
			case COWTriggerAction.HardDelete:
				return true;
			}
			return false;
		}

		// Token: 0x060039E4 RID: 14820 RVA: 0x000ED314 File Offset: 0x000EB514
		private bool IsUcsFolder(string containerClass)
		{
			bool flag = containerClass == "IPF.Contact.MOC.ImContactList" || containerClass == "IPF.Contact.MOC.QuickContacts";
			UcsTracker.Tracer.TraceDebug<string, bool>((long)this.GetHashCode(), "UcsTracker.IsUcsFolder: Folder with ContainerClass - {0} is interesting: {1}", containerClass, flag);
			return flag;
		}

		// Token: 0x04001EEA RID: 7914
		private static readonly Trace Tracer = ExTraceGlobals.ContactChangeLoggingTracer;

		// Token: 0x04001EEB RID: 7915
		private static readonly StorePropertyDefinition[] ContactProperties = new StorePropertyDefinition[]
		{
			ContactSchema.PartnerNetworkId,
			ContactSchema.IMAddress,
			StoreObjectSchema.DisplayName,
			ContactSchema.TelUri,
			ContactSchema.ImContactSipUriAddress,
			ContactSchema.OtherTelephone
		};

		// Token: 0x04001EEC RID: 7916
		private static readonly StorePropertyDefinition[] PdlProperties = new StorePropertyDefinition[]
		{
			DistributionListSchema.Members,
			DistributionListSchema.OneOffMembers,
			DistributionListSchema.DLStream,
			DistributionListSchema.DLChecksum,
			StoreObjectSchema.DisplayName,
			StoreObjectSchema.ItemClass
		};

		// Token: 0x04001EED RID: 7917
		private static readonly StorePropertyDefinition[] FolderProperties = new StorePropertyDefinition[]
		{
			StoreObjectSchema.ContainerClass,
			FolderSchema.DisplayName,
			FolderSchema.IsHidden,
			FolderSchema.ExtendedFolderFlags
		};

		// Token: 0x04001EEE RID: 7918
		private static readonly StorePropertyDefinition[] AllContactPropertiesToBeLoaded = PropertyDefinitionCollection.Merge<StorePropertyDefinition>(UcsTracker.ContactProperties, new StorePropertyDefinition[]
		{
			StoreObjectSchema.ParentEntryId
		});

		// Token: 0x04001EEF RID: 7919
		private static readonly StorePropertyDefinition[] AllPdlPropertiesToBeLoaded = PropertyDefinitionCollection.Merge<StorePropertyDefinition>(UcsTracker.PdlProperties, new StorePropertyDefinition[]
		{
			StoreObjectSchema.ParentEntryId
		});
	}
}
