using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Notifications.Broker;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000027 RID: 39
	internal sealed class MailboxChangeAssistantType : IEventBasedAssistantType, IAssistantType
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00009D65 File Offset: 0x00007F65
		public LocalizedString Name
		{
			get
			{
				return ServiceStrings.NotificationBrokerMailboxChangeAssistantName;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00009D6C File Offset: 0x00007F6C
		public string NonLocalizedName
		{
			get
			{
				return "NotificationBrokerMailboxChangeAssistant";
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00009D73 File Offset: 0x00007F73
		public MapiEventTypeFlags EventMask
		{
			get
			{
				return MapiEventTypeFlags.MailboxCreated | MapiEventTypeFlags.MailboxDeleted | MapiEventTypeFlags.MailboxDisconnected | MapiEventTypeFlags.MailboxReconnected | MapiEventTypeFlags.MailboxMoveStarted | MapiEventTypeFlags.MailboxMoveSucceeded | MapiEventTypeFlags.MailboxMoveFailed;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00009D7A File Offset: 0x00007F7A
		public bool NeedsMailboxSession
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00009D7D File Offset: 0x00007F7D
		public bool ProcessesPublicDatabases
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00009D80 File Offset: 0x00007F80
		public PropertyDefinition[] PreloadItemProperties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00009D83 File Offset: 0x00007F83
		public Guid Identity
		{
			get
			{
				return MailboxChangeAssistantType.ComponentGuid;
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00009D8A File Offset: 0x00007F8A
		public IEventBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			ExTraceGlobals.MailboxChangeTracer.TraceFunction<DatabaseInfo>((long)this.GetHashCode(), "Creating MailboxChangeAssistant for database: {0}", databaseInfo);
			return new MailboxChangeAssistant(BrokerSubscriptionStore.Instance, databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x040000B7 RID: 183
		private const string AssistantName = "NotificationBrokerMailboxChangeAssistant";

		// Token: 0x040000B8 RID: 184
		private const MapiEventTypeFlags InterestedMapiEventTypeFlags = MapiEventTypeFlags.MailboxCreated | MapiEventTypeFlags.MailboxDeleted | MapiEventTypeFlags.MailboxDisconnected | MapiEventTypeFlags.MailboxReconnected | MapiEventTypeFlags.MailboxMoveStarted | MapiEventTypeFlags.MailboxMoveSucceeded | MapiEventTypeFlags.MailboxMoveFailed;

		// Token: 0x040000B9 RID: 185
		private static readonly Guid ComponentGuid = new Guid("{9a08f4ec-acaa-4b79-892e-83e99ccb0675}");
	}
}
