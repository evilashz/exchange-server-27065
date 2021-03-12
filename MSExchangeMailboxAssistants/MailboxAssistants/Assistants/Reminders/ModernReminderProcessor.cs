using System;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Reminders;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Reminders
{
	// Token: 0x02000244 RID: 580
	internal abstract class ModernReminderProcessor : ReminderProcessorBase
	{
		// Token: 0x060015CC RID: 5580 RVA: 0x0007AC1D File Offset: 0x00078E1D
		protected ModernReminderProcessor(IReminderMessageManager reminderMessageManager, IRemindersAssistantLog log) : base(reminderMessageManager, log)
		{
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060015CD RID: 5581
		protected abstract bool IsCreateInteresting { get; }

		// Token: 0x060015CE RID: 5582 RVA: 0x0007AC28 File Offset: 0x00078E28
		public override bool IsEventInteresting(IMapiEvent mapiEvent)
		{
			bool flag = mapiEvent.ItemType == ObjectType.MAPI_MESSAGE;
			bool flag2 = !base.IsNonIPMFolder(mapiEvent.ExtendedEventFlags);
			bool flag3 = base.IsModernRemindersModified(mapiEvent.ExtendedEventFlags);
			bool flag4 = this.IsTypeInteresting(mapiEvent.ObjectClass);
			bool flag5 = Globals.IsEventOfType(mapiEvent, MapiEventTypeFlags.ObjectModified) && flag3 && flag4;
			bool flag6 = Globals.IsEventOfType(mapiEvent, MapiEventTypeFlags.ObjectMoved) && flag3 && flag4;
			bool flag7 = Globals.IsEventOfType(mapiEvent, MapiEventTypeFlags.ObjectDeleted) && flag3 && flag4;
			bool flag8 = Globals.IsEventOfType(mapiEvent, MapiEventTypeFlags.ObjectCreated) && flag4 && this.IsCreateInteresting;
			bool flag9 = flag5 || flag6 || flag7 || flag8;
			ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "ModernReminderProcessor.IsEventInteresting - relevantPropertyChange={0}, modified={1}, moved={2}, deleted={3}, created={4}, relevantOperation={5}, relevantItemType={6}, relevantFolder={7}", new object[]
			{
				flag3,
				flag5,
				flag6,
				flag7,
				flag8,
				flag9,
				flag,
				flag2
			});
			return flag && flag2 && flag9;
		}

		// Token: 0x060015CF RID: 5583
		protected abstract bool IsTypeInteresting(string objectClass);
	}
}
