using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000E1 RID: 225
	internal sealed class InboxRemindersProperty : ComplexPropertyBase, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectCommand, ISetCommand, ISetUpdateCommand, IDeleteUpdateCommand, IUpdateCommand, IPropertyCommand
	{
		// Token: 0x06000626 RID: 1574 RVA: 0x00020716 File Offset: 0x0001E916
		private InboxRemindersProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0002071F File Offset: 0x0001E91F
		public static InboxRemindersProperty CreateCommand(CommandContext commandContext)
		{
			return new InboxRemindersProperty(commandContext);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00020727 File Offset: 0x0001E927
		public void ToXml()
		{
			throw new InvalidOperationException("InboxRemindersProperty.ToXml should not be called.");
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00020733 File Offset: 0x0001E933
		public void ToXmlForPropertyBag()
		{
			throw new InvalidOperationException("InboxRemindersProperty.ToXmlForPropertyBag should not be called.");
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00020740 File Offset: 0x0001E940
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			ServiceObject serviceObject = commandSettings.ServiceObject;
			StoreObject storeObject = commandSettings.StoreObject;
			InboxReminderType[] array = null;
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2012))
			{
				using (CalendarItemBase calendarItemBase = storeObject as CalendarItemBase)
				{
					if (calendarItemBase == null)
					{
						return;
					}
					calendarItemBase.Load(new PropertyDefinition[]
					{
						CalendarItemBaseSchema.EventTimeBasedInboxReminders
					});
					Reminders<EventTimeBasedInboxReminder> eventTimeBasedInboxReminders = calendarItemBase.EventTimeBasedInboxReminders;
					if (eventTimeBasedInboxReminders != null && eventTimeBasedInboxReminders.ReminderList.Count > 0)
					{
						array = new InboxReminderType[eventTimeBasedInboxReminders.ReminderList.Count];
						for (int i = 0; i < eventTimeBasedInboxReminders.ReminderList.Count; i++)
						{
							EventTimeBasedInboxReminder eventTimeBasedInboxReminder = eventTimeBasedInboxReminders.ReminderList[i];
							array[i] = new InboxReminderType
							{
								Id = eventTimeBasedInboxReminder.Identifier,
								ReminderOffset = eventTimeBasedInboxReminder.ReminderOffset,
								Message = eventTimeBasedInboxReminder.CustomMessage,
								IsOrganizerReminder = eventTimeBasedInboxReminder.IsOrganizerReminder,
								OccurrenceChange = (EmailReminderChangeType)eventTimeBasedInboxReminder.OccurrenceChange
							};
						}
					}
				}
			}
			serviceObject[CalendarItemSchema.InboxReminders] = array;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00020880 File Offset: 0x0001EA80
		public void Set()
		{
			SetCommandSettings commandSettings = base.GetCommandSettings<SetCommandSettings>();
			CalendarItemBase calendarItem = (CalendarItemBase)commandSettings.StoreObject;
			InboxRemindersProperty.SetProperty(commandSettings.ServiceObject, calendarItem);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x000208AC File Offset: 0x0001EAAC
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			CalendarItemBase calendarItem = (CalendarItemBase)updateCommandSettings.StoreObject;
			ServiceObject serviceObject = setPropertyUpdate.ServiceObject;
			InboxRemindersProperty.SetProperty(serviceObject, calendarItem);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x000208D4 File Offset: 0x0001EAD4
		public override void DeleteUpdate(DeletePropertyUpdate deletePropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			CalendarItemBase calendarItemBase = updateCommandSettings.StoreObject as CalendarItemBase;
			if (calendarItemBase == null)
			{
				throw new InvalidPropertyDeleteException(deletePropertyUpdate.PropertyPath);
			}
			InboxRemindersProperty.DeleteProperty(calendarItemBase);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00020904 File Offset: 0x0001EB04
		internal static void SetProperty(ServiceObject serviceObject, CalendarItemBase calendarItem)
		{
			InboxReminderType[] array = (InboxReminderType[])serviceObject.PropertyBag[CalendarItemSchema.InboxReminders];
			if (array != null)
			{
				calendarItem.Load(new PropertyDefinition[]
				{
					CalendarItemBaseSchema.EventTimeBasedInboxReminders
				});
				Reminders<EventTimeBasedInboxReminder> eventTimeBasedInboxReminders = calendarItem.EventTimeBasedInboxReminders;
				Reminders<EventTimeBasedInboxReminder> reminders = new Reminders<EventTimeBasedInboxReminder>();
				foreach (InboxReminderType inboxReminderType in array)
				{
					Guid seriesReminderId = Guid.Empty;
					if (eventTimeBasedInboxReminders != null)
					{
						EventTimeBasedInboxReminder eventTimeBasedInboxReminder = (EventTimeBasedInboxReminder)eventTimeBasedInboxReminders.GetReminder(inboxReminderType.Id);
						if (eventTimeBasedInboxReminder != null)
						{
							seriesReminderId = eventTimeBasedInboxReminder.SeriesReminderId;
						}
					}
					EventTimeBasedInboxReminder item = new EventTimeBasedInboxReminder
					{
						Identifier = inboxReminderType.Id,
						ReminderOffset = inboxReminderType.ReminderOffset,
						CustomMessage = inboxReminderType.Message,
						IsOrganizerReminder = inboxReminderType.IsOrganizerReminder,
						OccurrenceChange = (EmailReminderChangeType)inboxReminderType.OccurrenceChange,
						SeriesReminderId = seriesReminderId
					};
					reminders.ReminderList.Add(item);
				}
				calendarItem.EventTimeBasedInboxReminders = reminders;
			}
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00020A08 File Offset: 0x0001EC08
		internal static void DeleteProperty(CalendarItemBase calendarItem)
		{
			calendarItem.Load(new PropertyDefinition[]
			{
				CalendarItemBaseSchema.EventTimeBasedInboxReminders
			});
			calendarItem.EventTimeBasedInboxReminders = null;
		}
	}
}
