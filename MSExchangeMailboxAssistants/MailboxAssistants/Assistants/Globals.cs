using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x02000019 RID: 25
	internal static class Globals
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x00005318 File Offset: 0x00003518
		internal static bool IsItemInDumpster(MailboxSession mailboxSession, IStoreObject storeObject)
		{
			if (storeObject != null && storeObject.ParentId != null)
			{
				using (COWSession cowsession = COWSession.Create(mailboxSession))
				{
					return cowsession.IsDumpsterFolder(mailboxSession, storeObject.ParentId);
				}
				return false;
			}
			return false;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005364 File Offset: 0x00003564
		internal static bool IsDeletedItemsFolder(IMailboxSession mailboxSession, byte[] parentId)
		{
			StoreObjectId id = StoreObjectId.FromProviderSpecificId(parentId, StoreObjectType.Folder);
			return mailboxSession.GetDefaultFolderId(DefaultFolderType.DeletedItems).Equals(id);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005386 File Offset: 0x00003586
		internal static bool IsEventOfType(IMapiEvent mapiEvent, MapiEventTypeFlags eventType)
		{
			return (mapiEvent.EventMask & eventType) == eventType;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005393 File Offset: 0x00003593
		internal static bool IsStoreObjectDeleted(IMapiEvent mapiEvent, MailboxSession mailboxSession, IStoreObject storeObject)
		{
			return storeObject == null || Globals.IsStoreObjectDeleted(mapiEvent, mailboxSession, Globals.IsItemInDumpster(mailboxSession, storeObject));
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000053A8 File Offset: 0x000035A8
		internal static bool IsStoreObjectDeleted(IMapiEvent mapiEvent, IMailboxSession mailboxSession, bool isStoreObjectInDumpster)
		{
			return isStoreObjectInDumpster || Globals.IsEventOfType(mapiEvent, MapiEventTypeFlags.ObjectDeleted) || (Globals.IsEventOfType(mapiEvent, MapiEventTypeFlags.ObjectMoved) && Globals.IsDeletedItemsFolder(mailboxSession, mapiEvent.ParentEntryId));
		}

		// Token: 0x040000F0 RID: 240
		public const string EventSource = "MSExchangeMailboxAssistants";

		// Token: 0x040000F1 RID: 241
		public const string ProvisioningAssistantEventSource = "MSExchange Provisioning MailboxAssistant";

		// Token: 0x040000F2 RID: 242
		public const string ServiceRegistryKey = "MSExchangeMailboxAssistants";

		// Token: 0x040000F3 RID: 243
		public const string ServiceRegistryKeyPath = "System\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants";

		// Token: 0x040000F4 RID: 244
		public const string ParameterRegistryKeyPath = "System\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters";

		// Token: 0x040000F5 RID: 245
		public const string ELCAssistantCalendarTaskRetentionProcessingTimeInMinutes = "ELCAssistantCalendarTaskRetentionProcessingTimeInMinutes";

		// Token: 0x040000F6 RID: 246
		public const string ELCAssistantCalendarTaskRetentionEnabled = "ELCAssistantCalendarTaskRetentionEnabled";

		// Token: 0x040000F7 RID: 247
		public const string ELCAssistantInheritedTagDepthLimit = "ELCAssistantInheritedTagDepthLimit";

		// Token: 0x040000F8 RID: 248
		public const int DefaultELCAssistantCalendarTaskRetentionProcessingTimeInMinutes = 120;

		// Token: 0x040000F9 RID: 249
		public const int MinELCAssistantCalendarTaskRetentionProcessingTimeInMinutes = 30;

		// Token: 0x040000FA RID: 250
		public const int MaxELCAssistantCalendarTaskRetentionProcessingTimeInMinutes = 10080;

		// Token: 0x040000FB RID: 251
		public const int DefaultMaxELCAssistantInheritedTagDepth = 20;

		// Token: 0x040000FC RID: 252
		public static ExEventLog Logger = new ExEventLog(ExTraceGlobals.AssistantBaseTracer.Category, "MSExchangeMailboxAssistants");

		// Token: 0x040000FD RID: 253
		public static ExEventLog ProvisioningAssistantLogger = new ExEventLog(ExTraceGlobals.ProvisioningAssistantTracer.Category, "MSExchange Provisioning MailboxAssistant");
	}
}
