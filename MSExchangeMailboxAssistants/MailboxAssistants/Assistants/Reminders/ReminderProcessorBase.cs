using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Reminders
{
	// Token: 0x02000243 RID: 579
	internal abstract class ReminderProcessorBase : IEventProcessor
	{
		// Token: 0x060015BD RID: 5565 RVA: 0x0007AAE5 File Offset: 0x00078CE5
		protected ReminderProcessorBase(IReminderMessageManager reminderMessageManager, IRemindersAssistantLog log)
		{
			ArgumentValidator.ThrowIfNull("reminderMessageManager", reminderMessageManager);
			ArgumentValidator.ThrowIfNull("log", log);
			this.Name = base.GetType().Name;
			this.ReminderMessageManager = reminderMessageManager;
			this.Log = log;
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x0007AB22 File Offset: 0x00078D22
		// (set) Token: 0x060015BF RID: 5567 RVA: 0x0007AB2A File Offset: 0x00078D2A
		public string Name { get; private set; }

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x0007AB33 File Offset: 0x00078D33
		// (set) Token: 0x060015C1 RID: 5569 RVA: 0x0007AB3B File Offset: 0x00078D3B
		private protected IReminderMessageManager ReminderMessageManager { protected get; private set; }

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x0007AB44 File Offset: 0x00078D44
		// (set) Token: 0x060015C3 RID: 5571 RVA: 0x0007AB4C File Offset: 0x00078D4C
		private protected IRemindersAssistantLog Log { protected get; private set; }

		// Token: 0x060015C4 RID: 5572
		public abstract bool IsEventInteresting(IMapiEvent mapiEvent);

		// Token: 0x060015C5 RID: 5573 RVA: 0x0007AB58 File Offset: 0x00078D58
		public void HandleEvent(IMapiEvent mapiEvent, IMailboxSession itemStore, IStoreObject item, bool isItemInDumpster, List<KeyValuePair<string, object>> customDataToLog)
		{
			bool flag = Globals.IsEventOfType(mapiEvent, MapiEventTypeFlags.ObjectModified);
			bool flag2 = Globals.IsEventOfType(mapiEvent, MapiEventTypeFlags.ObjectMoved);
			bool flag3 = Globals.IsStoreObjectDeleted(mapiEvent, itemStore, isItemInDumpster);
			bool flag4 = Globals.IsEventOfType(mapiEvent, MapiEventTypeFlags.ObjectCreated);
			ExAssert.RetailAssert(flag || flag2 || flag3 || flag4, "HandleEvent called for uninteresting event, eventMask={0}", new object[]
			{
				mapiEvent.EventMask
			});
			if (flag3)
			{
				this.OnObjectDeleted(mapiEvent, itemStore, item, customDataToLog);
				return;
			}
			this.OnObjectModified(mapiEvent, itemStore, item, customDataToLog);
		}

		// Token: 0x060015C6 RID: 5574
		public abstract bool IsEnabled(VariantConfigurationSnapshot snapshot);

		// Token: 0x060015C7 RID: 5575
		protected abstract void OnObjectModified(IMapiEvent mapiEvent, IMailboxSession itemStore, IStoreObject item, List<KeyValuePair<string, object>> customDataToLog);

		// Token: 0x060015C8 RID: 5576
		protected abstract void OnObjectDeleted(IMapiEvent mapiEvent, IMailboxSession itemStore, IStoreObject item, List<KeyValuePair<string, object>> customDataToLog);

		// Token: 0x060015C9 RID: 5577 RVA: 0x0007ABD3 File Offset: 0x00078DD3
		protected bool IsNonIPMFolder(MapiExtendedEventFlags extendedEventFlags)
		{
			return (extendedEventFlags & MapiExtendedEventFlags.NonIPMFolder) != MapiExtendedEventFlags.None;
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x0007ABE4 File Offset: 0x00078DE4
		protected bool IsModernRemindersModified(MapiExtendedEventFlags extendedEventFlags)
		{
			return (extendedEventFlags & MapiExtendedEventFlags.ModernRemindersChanged) != MapiExtendedEventFlags.None;
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x0007ABF8 File Offset: 0x00078DF8
		protected long ExecuteAndMeasure(Action action)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			action();
			stopwatch.Stop();
			return stopwatch.ElapsedMilliseconds;
		}
	}
}
