using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000173 RID: 371
	internal abstract class ModernRemindersPropertyBase : ComplexPropertyBase, ISetUpdateCommand, IUpdateCommand, IPropertyCommand
	{
		// Token: 0x06000AAB RID: 2731 RVA: 0x00033C11 File Offset: 0x00031E11
		public ModernRemindersPropertyBase(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00033C1C File Offset: 0x00031E1C
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			Item messageItem = (Item)updateCommandSettings.StoreObject;
			ServiceObject serviceObject = setPropertyUpdate.ServiceObject;
			this.SetProperty(serviceObject, messageItem);
		}

		// Token: 0x06000AAD RID: 2733
		internal abstract void SetProperty(ServiceObject serviceObject, Item messageItem);

		// Token: 0x06000AAE RID: 2734 RVA: 0x00033C44 File Offset: 0x00031E44
		protected static void GetModernReminderSettings(ModernReminderType[] modernReminderTypes, Reminders<ModernReminder> reminders)
		{
			foreach (ModernReminderType modernReminderType in modernReminderTypes)
			{
				ReminderTimeHint reminderTimeHint;
				Hours hours;
				Priority priority;
				if (ModernRemindersPropertyBase.TryGetStorageModernReminderSettings(modernReminderType, out reminderTimeHint, out hours, out priority))
				{
					ModernReminder item = new ModernReminder
					{
						Identifier = modernReminderType.Id,
						ReminderTimeHint = reminderTimeHint,
						Hours = hours,
						Priority = priority,
						Duration = modernReminderType.Duration,
						ReferenceTime = ExDateTimeConverter.Parse(modernReminderType.ReferenceTimeString),
						CustomReminderTime = ExDateTimeConverter.Parse(modernReminderType.CustomReminderTimeString),
						DueDate = ExDateTimeConverter.Parse(modernReminderType.DueDateString)
					};
					reminders.ReminderList.Add(item);
				}
			}
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00033D04 File Offset: 0x00031F04
		private static string ConvertExdateTimeToString(ExDateTime exDateTime)
		{
			return ExDateTimeConverter.ToOffsetXsdDateTime(exDateTime, exDateTime.TimeZone);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00033D13 File Offset: 0x00031F13
		private static bool TryGetStorageModernReminderSettings(ModernReminderType modernReminderType, out ReminderTimeHint reminderTimeHint, out Hours hours, out Priority priority)
		{
			reminderTimeHint = ReminderTimeHint.LaterToday;
			hours = Hours.Any;
			priority = Priority.Normal;
			return ModernRemindersPropertyBase.TryGetStorageReminderTimeHint(modernReminderType, out reminderTimeHint) && ModernRemindersPropertyBase.TryGetStorageHours(modernReminderType, out hours) && ModernRemindersPropertyBase.TryGetStoragePriority(modernReminderType, out priority);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00033D3C File Offset: 0x00031F3C
		private static bool TryGetStoragePriority(ModernReminderType modernReminderType, out Priority priority)
		{
			priority = Priority.Normal;
			if (modernReminderType == null)
			{
				return false;
			}
			switch (modernReminderType.Priority)
			{
			case Priority.Low:
				priority = Priority.Low;
				break;
			case Priority.Normal:
				priority = Priority.Normal;
				break;
			case Priority.High:
				priority = Priority.High;
				break;
			default:
				return false;
			}
			return true;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00033D80 File Offset: 0x00031F80
		private static bool TryGetStorageHours(ModernReminderType modernReminderType, out Hours hours)
		{
			hours = Hours.Any;
			if (modernReminderType == null)
			{
				return false;
			}
			switch (modernReminderType.Hours)
			{
			case Hours.Personal:
				hours = Hours.Personal;
				break;
			case Hours.Working:
				hours = Hours.Working;
				break;
			case Hours.Any:
				hours = Hours.Any;
				break;
			default:
				return false;
			}
			return true;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00033DC4 File Offset: 0x00031FC4
		private static bool TryGetStorageReminderTimeHint(ModernReminderType modernReminderType, out ReminderTimeHint reminderTimeHint)
		{
			reminderTimeHint = ReminderTimeHint.LaterToday;
			if (modernReminderType == null)
			{
				return false;
			}
			switch (modernReminderType.ReminderTimeHint)
			{
			case ReminderTimeHint.LaterToday:
				reminderTimeHint = ReminderTimeHint.LaterToday;
				break;
			case ReminderTimeHint.Tomorrow:
				reminderTimeHint = ReminderTimeHint.Tomorrow;
				break;
			case ReminderTimeHint.TomorrowMorning:
				reminderTimeHint = ReminderTimeHint.TomorrowMorning;
				break;
			case ReminderTimeHint.TomorrowAfternoon:
				reminderTimeHint = ReminderTimeHint.TomorrowAfternoon;
				break;
			case ReminderTimeHint.TomorrowEvening:
				reminderTimeHint = ReminderTimeHint.TomorrowEvening;
				break;
			case ReminderTimeHint.ThisWeekend:
				reminderTimeHint = ReminderTimeHint.ThisWeekend;
				break;
			case ReminderTimeHint.NextWeek:
				reminderTimeHint = ReminderTimeHint.NextWeek;
				break;
			case ReminderTimeHint.NextMonth:
				reminderTimeHint = ReminderTimeHint.NextMonth;
				break;
			case ReminderTimeHint.Someday:
				reminderTimeHint = ReminderTimeHint.Someday;
				break;
			case ReminderTimeHint.Custom:
				reminderTimeHint = ReminderTimeHint.Custom;
				break;
			case ReminderTimeHint.Now:
				reminderTimeHint = ReminderTimeHint.Now;
				break;
			case ReminderTimeHint.InTwoDays:
				reminderTimeHint = ReminderTimeHint.InTwoDays;
				break;
			default:
				return false;
			}
			return true;
		}
	}
}
