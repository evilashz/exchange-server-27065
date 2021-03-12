using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Notifications.Broker;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000026 RID: 38
	internal class MailboxChangeAssistant : IEventBasedAssistant, IAssistantBase
	{
		// Token: 0x06000194 RID: 404 RVA: 0x00009B75 File Offset: 0x00007D75
		public MailboxChangeAssistant(IMailboxChangeHandler handler, DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName)
		{
			ArgumentValidator.ThrowIfNull("handler", handler);
			ArgumentValidator.ThrowIfNull("databaseInfo", databaseInfo);
			this.handler = handler;
			this.databaseInfo = databaseInfo;
			this.Name = name;
			this.NonLocalizedName = nonLocalizedName;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00009BB0 File Offset: 0x00007DB0
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00009BB8 File Offset: 0x00007DB8
		public LocalizedString Name { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00009BC1 File Offset: 0x00007DC1
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00009BC9 File Offset: 0x00007DC9
		public string NonLocalizedName { get; private set; }

		// Token: 0x06000199 RID: 409 RVA: 0x00009BD2 File Offset: 0x00007DD2
		public void OnStart(EventBasedStartInfo startInfo)
		{
			this.handler.HandleDatabaseStart(this.databaseInfo);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00009BE5 File Offset: 0x00007DE5
		public void OnShutdown()
		{
			this.handler.HandleDatabaseShutdown(this.databaseInfo);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00009BF8 File Offset: 0x00007DF8
		public bool IsEventInteresting(MapiEvent mapiEvent)
		{
			if (this.IsMailboxCreatedOrConnected(mapiEvent))
			{
				return true;
			}
			if (this.IsMailboxMoveSucceeded(mapiEvent))
			{
				return true;
			}
			if (this.IsMailboxDeletedOrDisconnected(mapiEvent))
			{
				return true;
			}
			ExTraceGlobals.MailboxChangeTracer.TraceDebug<MapiEvent>((long)this.GetHashCode(), "MailboxChangeAssistant about to ignore the event: {0}", mapiEvent);
			return false;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00009C34 File Offset: 0x00007E34
		public void HandleEvent(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item)
		{
			if (this.IsMailboxCreatedOrConnected(mapiEvent))
			{
				this.handler.HandleMailboxCreatedOrConnected(this.databaseInfo, mapiEvent.MailboxGuid);
				return;
			}
			if (this.IsMailboxMoveSucceeded(mapiEvent))
			{
				this.handler.HandleMailboxMoveSucceeded(this.databaseInfo, mapiEvent.MailboxGuid);
				return;
			}
			if (this.IsMailboxDeletedOrDisconnected(mapiEvent))
			{
				this.handler.HandleMailboxDeletedOrDisconnected(this.databaseInfo, mapiEvent.MailboxGuid);
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00009CA4 File Offset: 0x00007EA4
		private bool IsMailboxCreatedOrConnected(MapiEvent mapiEvent)
		{
			return (mapiEvent.EventMask & MapiEventTypeFlags.MailboxCreated) != (MapiEventTypeFlags)0 || (mapiEvent.EventMask & MapiEventTypeFlags.MailboxReconnected) != (MapiEventTypeFlags)0 || ((mapiEvent.EventMask & MapiEventTypeFlags.MailboxMoveFailed) != (MapiEventTypeFlags)0 && (mapiEvent.EventFlags & MapiEventFlags.Source) != MapiEventFlags.None);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00009CF1 File Offset: 0x00007EF1
		private bool IsMailboxMoveSucceeded(MapiEvent mapiEvent)
		{
			return (mapiEvent.EventMask & MapiEventTypeFlags.MailboxMoveSucceeded) != (MapiEventTypeFlags)0 && (mapiEvent.EventFlags & MapiEventFlags.Destination) != MapiEventFlags.None;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00009D18 File Offset: 0x00007F18
		private bool IsMailboxDeletedOrDisconnected(MapiEvent mapiEvent)
		{
			return (mapiEvent.EventMask & MapiEventTypeFlags.MailboxDeleted) != (MapiEventTypeFlags)0 || (mapiEvent.EventMask & MapiEventTypeFlags.MailboxDisconnected) != (MapiEventTypeFlags)0 || ((mapiEvent.EventMask & MapiEventTypeFlags.MailboxMoveStarted) != (MapiEventTypeFlags)0 && (mapiEvent.EventFlags & MapiEventFlags.Source) != MapiEventFlags.None);
		}

		// Token: 0x040000B3 RID: 179
		private readonly DatabaseInfo databaseInfo;

		// Token: 0x040000B4 RID: 180
		private readonly IMailboxChangeHandler handler;
	}
}
