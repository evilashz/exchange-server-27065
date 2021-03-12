using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarNotification;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000DF RID: 223
	internal sealed class CalendarNotificationAssistant : EventBasedAssistant, IEventBasedAssistant, IAssistantBase
	{
		// Token: 0x06000961 RID: 2401 RVA: 0x0003F4C0 File Offset: 0x0003D6C0
		public static bool TryHandleException(long id, string action, string source, Exception ex)
		{
			ExTraceGlobals.AssistantTracer.TraceError<string, string, Exception>(id, "Exception is caught during {0} for {1}: {2}", action, source, ex);
			if (ex is StorageTransientException || ex is StoragePermanentException || ex is AITransientException || ex is MissingSystemMailboxException || ex is LocalServerException || ex is DataValidationException || ex is DataSourceTransientException || ex is DataSourceOperationException)
			{
				return true;
			}
			if (GrayException.IsGrayException(ex))
			{
				ExWatson.SendReport(ex, ReportOptions.None, null);
				return true;
			}
			return false;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0003F535 File Offset: 0x0003D735
		public CalendarNotificationAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
			this.settingProcessor = new SettingsChangeProcessor(databaseInfo);
			this.calendarProcessor = new CalendarChangeProcessor(databaseInfo);
			this.deliveryStatusProcessor = new TextMessageDeliveryStatusProcessor(databaseInfo);
			this.initiator = new CalendarNotificationInitiator();
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0003F56F File Offset: 0x0003D76F
		public bool IsEventInteresting(MapiEvent mapiEvent)
		{
			return this.calendarProcessor.IsEventInteresting(mapiEvent) || this.settingProcessor.IsEventInteresting(mapiEvent) || this.deliveryStatusProcessor.IsEventInteresting(mapiEvent);
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0003F59B File Offset: 0x0003D79B
		protected override void OnStartInternal(EventBasedStartInfo startInfo)
		{
			this.initiator.Initiate(base.DatabaseInfo);
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0003F5AE File Offset: 0x0003D7AE
		protected override void OnShutdownInternal()
		{
			this.initiator.Stop(base.DatabaseInfo);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0003F5C4 File Offset: 0x0003D7C4
		protected override void HandleEventInternal(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			try
			{
				bool flag = false;
				if (this.calendarProcessor.IsEventInteresting(mapiEvent))
				{
					this.calendarProcessor.HandleEvent(mapiEvent, itemStore, item);
					flag = true;
				}
				else if (this.settingProcessor.IsEventInteresting(mapiEvent))
				{
					this.settingProcessor.HandleEvent(mapiEvent, itemStore, item);
					flag = true;
				}
				else if (this.deliveryStatusProcessor.IsEventInteresting(mapiEvent))
				{
					this.deliveryStatusProcessor.HandleEvent(mapiEvent, itemStore, item);
					flag = true;
				}
				if (flag)
				{
					CalNotifsCounters.NumberOfInterestingMailboxEvents.Increment();
				}
			}
			catch (Exception ex)
			{
				if (!CalendarNotificationAssistant.TryHandleException((long)this.GetHashCode(), "HandleAssistantEvent", itemStore.MailboxOwner.ToString(), ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0003F678 File Offset: 0x0003D878
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0003F680 File Offset: 0x0003D880
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0003F688 File Offset: 0x0003D888
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000653 RID: 1619
		private SettingsChangeProcessor settingProcessor;

		// Token: 0x04000654 RID: 1620
		private CalendarChangeProcessor calendarProcessor;

		// Token: 0x04000655 RID: 1621
		private TextMessageDeliveryStatusProcessor deliveryStatusProcessor;

		// Token: 0x04000656 RID: 1622
		private CalendarNotificationInitiator initiator;
	}
}
