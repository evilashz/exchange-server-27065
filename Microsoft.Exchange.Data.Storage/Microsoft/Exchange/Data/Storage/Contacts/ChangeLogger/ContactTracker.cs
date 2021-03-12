using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage.Contacts.ChangeLogger
{
	// Token: 0x0200055C RID: 1372
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ContactTracker : IContactChangeTracker
	{
		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x060039D7 RID: 14807 RVA: 0x000ECD62 File Offset: 0x000EAF62
		public string Name
		{
			get
			{
				return "ContactTracker";
			}
		}

		// Token: 0x060039D8 RID: 14808 RVA: 0x000ECD69 File Offset: 0x000EAF69
		public bool ShouldLoadPropertiesForFurtherCheck(COWTriggerAction operation, string itemClass, StoreObjectId itemId, CoreItem item)
		{
			if (ObjectClass.IsContact(itemClass))
			{
				return true;
			}
			ContactTracker.Tracer.TraceDebug<string, StoreObjectId>((long)this.GetHashCode(), "ContactTracker.ShouldLoadPropertiesForFurtherCheck: Skipping Item with Class - {0}, Id - {1}", itemClass, itemId);
			return false;
		}

		// Token: 0x060039D9 RID: 14809 RVA: 0x000ECD90 File Offset: 0x000EAF90
		public StorePropertyDefinition[] GetProperties(StoreObjectId itemId, CoreItem item)
		{
			List<StorePropertyDefinition> list = new List<StorePropertyDefinition>();
			foreach (PropertyDefinition propertyDefinition in ContactSchema.Instance.InternalAllProperties)
			{
				if (!item.PropertyBag.IsPropertyDirty(propertyDefinition))
				{
					ContactTracker.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ContactTracker.ShouldLogContact: Skipping property as it is not dirty: {0}", propertyDefinition.Name);
				}
				else
				{
					StorePropertyDefinition storePropertyDefinition = propertyDefinition as StorePropertyDefinition;
					if (storePropertyDefinition == null)
					{
						ContactTracker.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ContactTracker.ShouldLogContact: Skipping property as it is not a StorePropertyDefinition: {0}", propertyDefinition.Name);
					}
					else
					{
						list.Add(storePropertyDefinition);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x060039DA RID: 14810 RVA: 0x000ECE44 File Offset: 0x000EB044
		public bool ShouldLogContact(StoreObjectId itemId, CoreItem item)
		{
			return true;
		}

		// Token: 0x060039DB RID: 14811 RVA: 0x000ECE48 File Offset: 0x000EB048
		public bool ShouldLogGroupOperation(COWTriggerAction operation, StoreSession sourceSession, StoreObjectId sourceFolderId, StoreSession destinationSession, StoreObjectId destinationFolderId, ICollection<StoreObjectId> itemIds)
		{
			if (sourceSession != null && sourceFolderId != null && (this.IsDefaultFolder(sourceSession, sourceFolderId, DefaultFolderType.Conflicts) || this.IsDefaultFolder(sourceSession, sourceFolderId, DefaultFolderType.SyncIssues)))
			{
				ContactTracker.Tracer.TraceDebug((long)this.GetHashCode(), "ContactTracker.ShouldLogGroupOperation: SourceFolder is a Conflicts/SyncIssues folder.");
				return false;
			}
			if (destinationSession != null && destinationFolderId != null && (this.IsDefaultFolder(destinationSession, destinationFolderId, DefaultFolderType.Conflicts) || this.IsDefaultFolder(destinationSession, destinationFolderId, DefaultFolderType.SyncIssues)))
			{
				ContactTracker.Tracer.TraceDebug((long)this.GetHashCode(), "ContactTracker.ShouldLogGroupOperation: DestinationFolder is a Conflicts/SyncIssues folder.");
				return false;
			}
			ContactTracker.Tracer.TraceDebug<COWTriggerAction>((long)this.GetHashCode(), "ContactTracker.ShouldLogGroupOperation: Invoked for operation: {0}", operation);
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

		// Token: 0x060039DC RID: 14812 RVA: 0x000ECF10 File Offset: 0x000EB110
		private bool IsDefaultFolder(StoreSession session, StoreObjectId folderId, DefaultFolderType defaultFolder)
		{
			MailboxSession mailboxSession = session as MailboxSession;
			if (mailboxSession == null)
			{
				ContactTracker.Tracer.TraceDebug<StoreSession>((long)this.GetHashCode(), "ContactTracker.IsDefaultFolder: Invoked for session that is not a mailbox session: {0}", session);
				return false;
			}
			StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(defaultFolder);
			bool flag = object.Equals(defaultFolder, folderId);
			ContactTracker.Tracer.TraceDebug((long)this.GetHashCode(), "ContactTracker.IsDefaultFolder: Given folderId {0}, DefaultFolderType {1}'s Id {2} are same: {3}", new object[]
			{
				folderId,
				defaultFolder,
				defaultFolderId,
				flag
			});
			return flag;
		}

		// Token: 0x04001EE9 RID: 7913
		private static readonly Trace Tracer = ExTraceGlobals.ContactChangeLoggingTracer;
	}
}
