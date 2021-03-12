using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Conversations;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Conversations
{
	// Token: 0x02000022 RID: 34
	internal sealed class ConversationsAssistant : EventBasedAssistant, IEventBasedAssistant, IAssistantBase
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00005BD4 File Offset: 0x00003DD4
		public ConversationsAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005BE0 File Offset: 0x00003DE0
		public bool IsEventInteresting(MapiEvent mapiEvent)
		{
			CachedState cachedState = AssistantsService.CachedObjectsList.GetCachedState(mapiEvent.MailboxGuid);
			MailboxData mailboxData = (MailboxData)cachedState.State[3];
			return BodyTagProcessor.IsEventInteresting(mapiEvent) || ActionsProcessor.IsEventInteresting(mapiEvent) || SentItemsProcessor.IsEventInteresting(mapiEvent, mailboxData) || LicensingProcessor.IsEventInteresting(mapiEvent, mailboxData) || InferenceProcessor.IsEventInteresting(mapiEvent, mailboxData);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005C38 File Offset: 0x00003E38
		protected override void HandleEventInternal(MapiEvent mapiEvent, MailboxSession session, StoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			CachedState cachedState = AssistantsService.CachedObjectsList.GetCachedState(mapiEvent.MailboxGuid);
			MailboxData mailboxData = (MailboxData)cachedState.State[3];
			if (mailboxData == null)
			{
				mailboxData = new MailboxData(session);
				Interlocked.Exchange(ref cachedState.State[3], mailboxData);
			}
			if (BodyTagProcessor.IsEventInteresting(mapiEvent))
			{
				BodyTagProcessor.HandleEventInternal(session, item);
				return;
			}
			if (ActionsProcessor.IsEventInteresting(mapiEvent))
			{
				ActionsProcessor.HandleEventInternal(mapiEvent, session, item, mailboxData);
				Interlocked.Exchange(ref cachedState.State[3], mailboxData);
				return;
			}
			if (SentItemsProcessor.IsEventInteresting(mapiEvent, mailboxData))
			{
				SentItemsProcessor.HandleEventInternal(mapiEvent, session, item, mailboxData);
				Interlocked.Exchange(ref cachedState.State[3], mailboxData);
				return;
			}
			if (LicensingProcessor.IsEventInteresting(mapiEvent, mailboxData))
			{
				LicensingProcessor.HandleEventInternal(session, item);
				return;
			}
			if (InferenceProcessor.IsEventInteresting(mapiEvent, mailboxData))
			{
				InferenceProcessor.HandleEventInternal(mapiEvent, session, item, mailboxData, customDataToLog);
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005D0D File Offset: 0x00003F0D
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005D15 File Offset: 0x00003F15
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005D1D File Offset: 0x00003F1D
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x0400010D RID: 269
		private static readonly Trace Tracer = ExTraceGlobals.GeneralTracer;
	}
}
