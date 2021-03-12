using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Contacts.ChangeLogger
{
	// Token: 0x0200055B RID: 1371
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ContactEmailChangeLogger : IContactChangeTracker
	{
		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x060039CF RID: 14799 RVA: 0x000ECBE5 File Offset: 0x000EADE5
		public string Name
		{
			get
			{
				return "EmailCorruptionTracker";
			}
		}

		// Token: 0x060039D0 RID: 14800 RVA: 0x000ECC08 File Offset: 0x000EAE08
		public bool ShouldLoadPropertiesForFurtherCheck(COWTriggerAction operation, string itemClass, StoreObjectId itemId, CoreItem item)
		{
			if (!ObjectClass.IsContact(itemClass))
			{
				return false;
			}
			switch (operation)
			{
			case COWTriggerAction.Create:
			case COWTriggerAction.Update:
			case COWTriggerAction.Copy:
				return Array.Exists<StorePropertyDefinition>(ContactEmailChangeLogger.TriggerProperties, (StorePropertyDefinition property) => item.PropertyBag.IsPropertyDirty(property));
			}
			return false;
		}

		// Token: 0x060039D1 RID: 14801 RVA: 0x000ECC6A File Offset: 0x000EAE6A
		public StorePropertyDefinition[] GetProperties(StoreObjectId itemId, CoreItem item)
		{
			return ContactEmailChangeLogger.TriggerProperties;
		}

		// Token: 0x060039D2 RID: 14802 RVA: 0x000ECC71 File Offset: 0x000EAE71
		public bool ShouldLogContact(StoreObjectId itemId, CoreItem item)
		{
			return ContactEmailChangeLogger.DetectEmailCorruption(item);
		}

		// Token: 0x060039D3 RID: 14803 RVA: 0x000ECC7E File Offset: 0x000EAE7E
		public bool ShouldLogGroupOperation(COWTriggerAction operation, StoreSession sourceSession, StoreObjectId sourceFolderId, StoreSession destinationSession, StoreObjectId destinationFolderId, ICollection<StoreObjectId> itemIds)
		{
			return false;
		}

		// Token: 0x060039D4 RID: 14804 RVA: 0x000ECC84 File Offset: 0x000EAE84
		private static bool DetectEmailCorruption(CoreItem item)
		{
			HashSet<string> hashSet = new HashSet<string>();
			foreach (StorePropertyDefinition propertyDefinition in ContactEmailChangeLogger.TriggerProperties)
			{
				string valueOrDefault = item.PropertyBag.GetValueOrDefault<string>(propertyDefinition, string.Empty);
				if (valueOrDefault.EndsWith("microsoft.com", StringComparison.OrdinalIgnoreCase))
				{
					int num = valueOrDefault.IndexOf('@');
					if (num != -1)
					{
						string text = valueOrDefault.Substring(0, num);
						text = text.Trim().ToLowerInvariant();
						hashSet.Add(text);
					}
				}
			}
			return hashSet.Count > 2;
		}

		// Token: 0x04001EE8 RID: 7912
		private static readonly StorePropertyDefinition[] TriggerProperties = new StorePropertyDefinition[]
		{
			ContactSchema.Email1EmailAddress,
			ContactSchema.Email2EmailAddress,
			ContactSchema.Email3EmailAddress,
			ContactSchema.IMAddress,
			ContactSchema.IMAddress2,
			ContactSchema.IMAddress3
		};
	}
}
