using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.MailboxAssistants.Assistants.FilteredTracing;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x02000016 RID: 22
	internal abstract class EventBasedAssistant : AssistantBase
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00004FF4 File Offset: 0x000031F4
		public EventBasedAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005000 File Offset: 0x00003200
		public void HandleEvent(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item)
		{
			using (new GuidTraceFilter(base.DatabaseInfo.Guid, mapiEvent.MailboxGuid))
			{
				string value = (item != null) ? item.ClassName : mapiEvent.ObjectClass;
				if (!string.IsNullOrEmpty(value) || mapiEvent.ItemType == ObjectType.MAPI_STORE || mapiEvent.ItemType == ObjectType.MAPI_FOLDER)
				{
					EventBasedAssistant.Tracer.TraceDebug<EventBasedAssistant, long, IExchangePrincipal>((long)this.GetHashCode(), "{0}: Started handling event {1} for mailbox {2}.", this, mapiEvent.EventCounter, itemStore.MailboxOwner);
					List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
					IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
					Guid activityId = Guid.Empty;
					if (currentActivityScope != null)
					{
						activityId = currentActivityScope.ActivityId;
					}
					AssistantsLog.LogStartProcessingMailboxEvent(activityId, this, mapiEvent, itemStore.MailboxGuid);
					TraceContext.Set(itemStore);
					try
					{
						this.HandleEventInternal(mapiEvent, itemStore, item, list);
						goto IL_103;
					}
					finally
					{
						TraceContext.Reset();
						AssistantsLog.LogEndProcessingMailboxEvent(activityId, this, list, itemStore.MailboxGuid, string.Empty, null);
						EventBasedAssistant.Tracer.TraceDebug<EventBasedAssistant, long, IExchangePrincipal>((long)this.GetHashCode(), "{0}: Ended handling event {1} for mailbox {2}.", this, mapiEvent.EventCounter, itemStore.MailboxOwner);
					}
				}
				EventBasedAssistant.Tracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "{0}: Event not processed because we are unable get the ObjectClass on the item.", itemStore.MailboxOwner);
				IL_103:;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000513C File Offset: 0x0000333C
		public void OnStart(EventBasedStartInfo startInfo)
		{
			EventBasedAssistant.Tracer.TraceDebug<EventBasedAssistant>((long)this.GetHashCode(), "{0}: OnStart started", this);
			this.OnStartInternal(startInfo);
			EventBasedAssistant.Tracer.TraceDebug<EventBasedAssistant>((long)this.GetHashCode(), "{0}: OnStart completed", this);
			EventBasedAssistant.TracerPfd.TracePfd<int, EventBasedAssistant>((long)this.GetHashCode(), "PFD IWS{0} {1}: Started", 27415, this);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000519A File Offset: 0x0000339A
		protected virtual void OnStartInternal(EventBasedStartInfo startInfo)
		{
		}

		// Token: 0x060000C2 RID: 194
		protected abstract void HandleEventInternal(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item, List<KeyValuePair<string, object>> customDataToLog);

		// Token: 0x040000EC RID: 236
		private static readonly Trace Tracer = ExTraceGlobals.AssistantBaseTracer;

		// Token: 0x040000ED RID: 237
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;
	}
}
