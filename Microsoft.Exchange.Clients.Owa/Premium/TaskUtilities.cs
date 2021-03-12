using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000505 RID: 1285
	internal static class TaskUtilities
	{
		// Token: 0x06003112 RID: 12562 RVA: 0x00121380 File Offset: 0x0011F580
		public static Work MinutesToWork(int minutes)
		{
			Work work = new Work((float)minutes, DurationUnit.Minutes);
			if (minutes > 0)
			{
				int[] array = new int[]
				{
					1,
					60,
					480,
					2400
				};
				int[] array2 = new int[]
				{
					10,
					4
				};
				int i;
				for (i = 0; i < array.Length - 1; i++)
				{
					if (minutes < array[i + 1])
					{
						break;
					}
				}
				while (i > 0)
				{
					for (int j = 0; j < array2.Length; j++)
					{
						for (int k = 1; k <= array2[j]; k++)
						{
							if (minutes % (array[i] * k / array2[j]) == 0)
							{
								work.WorkAmount = (float)minutes / (float)array[i];
								work.WorkUnit = (DurationUnit)i;
								return work;
							}
						}
					}
					i--;
				}
			}
			return work;
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x00121434 File Offset: 0x0011F634
		public static SanitizedHtmlString GetDueByString(ExDateTime? dueDate)
		{
			SanitizedHtmlString result = null;
			if (dueDate != null)
			{
				int days = (dueDate.Value.Date - DateTimeUtilities.GetLocalTime().Date).Days;
				if (days == 0)
				{
					result = SanitizedHtmlString.GetNonEncoded(2136131687);
				}
				else if (days == 1)
				{
					result = SanitizedHtmlString.GetNonEncoded(-1763467067);
				}
				else if (days > 1 && days < 15)
				{
					result = SanitizedHtmlString.Format(LocalizedStrings.GetNonEncoded(397195003), new object[]
					{
						days
					});
				}
				else if (days == -1)
				{
					result = SanitizedHtmlString.GetNonEncoded(-2069325904);
				}
				else if (days < -1 && days > -15)
				{
					result = SanitizedHtmlString.Format(LocalizedStrings.GetNonEncoded(2142112873), new object[]
					{
						days * -1
					});
				}
				else if (days < -14)
				{
					result = SanitizedHtmlString.GetNonEncoded(2027934943);
				}
			}
			return result;
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x00121523 File Offset: 0x0011F723
		public static Strings.IDs GetStatusString(TaskStatus enumValue)
		{
			return TaskUtilities.statusToStringMap[enumValue];
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x00121530 File Offset: 0x0011F730
		public static Strings.IDs GetPriorityString(Importance enumValue)
		{
			return TaskUtilities.priorityToStringMap[enumValue];
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x0012153D File Offset: 0x0011F73D
		public static Strings.IDs GetWorkDurationUnitString(DurationUnit enumValue)
		{
			return TaskUtilities.durationToStringMap[enumValue];
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x0012154A File Offset: 0x0011F74A
		public static string GetWhen(Task task)
		{
			return task.GenerateWhen();
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x00121554 File Offset: 0x0011F754
		public static string GenerateWhen(UserContext userContext, Recurrence recurrence)
		{
			string when;
			using (Task task = Task.Create(userContext.MailboxSession, userContext.TasksFolderId))
			{
				task.Recurrence = recurrence;
				when = TaskUtilities.GetWhen(task);
			}
			return when;
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x001215A0 File Offset: 0x0011F7A0
		public static TaskType GetTaskType(Task task)
		{
			return TaskUtilities.GetTaskType(task);
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x001215A8 File Offset: 0x0011F7A8
		public static TaskType GetTaskType(Item item)
		{
			if (item != null)
			{
				object obj = item.TryGetProperty(TaskSchema.TaskType);
				if (obj is int)
				{
					switch ((int)obj)
					{
					case 0:
						return TaskType.NoMatch;
					case 1:
						return TaskType.Undelegated;
					case 2:
						return TaskType.Delegated;
					case 3:
						return TaskType.DelegatedAccepted;
					case 4:
						return TaskType.DelegatedDeclined;
					case 5:
						return TaskType.Max;
					}
				}
			}
			return TaskType.NoMatch;
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x00121600 File Offset: 0x0011F800
		public static ExDateTime? GetAssignedTime(Task task)
		{
			if (task != null)
			{
				object obj = task.TryGetProperty(TaskSchema.AssignedTime);
				if (obj is ExDateTime)
				{
					ExDateTime value = (ExDateTime)obj;
					return new ExDateTime?(value);
				}
			}
			return null;
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x0012163C File Offset: 0x0011F83C
		public static string GetTaskDelegator(Task task)
		{
			if (task != null)
			{
				string text = task.TryGetProperty(TaskSchema.TaskDelegator) as string;
				if (text != null)
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x00121664 File Offset: 0x0011F864
		public static bool IsAssignedTask(Task task)
		{
			if (task != null)
			{
				TaskType taskType = TaskUtilities.GetTaskType(task);
				return TaskUtilities.IsAssignedTaskType(taskType);
			}
			return false;
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x00121683 File Offset: 0x0011F883
		public static bool IsAssignedTaskType(TaskType taskType)
		{
			return taskType != TaskType.NoMatch && taskType != TaskType.Undelegated;
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x00121691 File Offset: 0x0011F891
		public static bool IsRegeneratingRecurrenceType(RecurrenceType recurrenceType)
		{
			return recurrenceType == RecurrenceType.DailyRegenerating || recurrenceType == RecurrenceType.WeeklyRegenerating || recurrenceType == RecurrenceType.MonthlyRegenerating || recurrenceType == RecurrenceType.YearlyRegenerating;
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x001216AC File Offset: 0x0011F8AC
		public static bool IsTeamTask(Task task)
		{
			if (task != null)
			{
				object obj = task.TryGetProperty(TaskSchema.IsTeamTask);
				if (obj is bool)
				{
					return (bool)obj;
				}
			}
			return false;
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x001216D8 File Offset: 0x0011F8D8
		public static bool IsTaskAccepted(Task task)
		{
			if (task != null)
			{
				object obj = task.TryGetProperty(TaskSchema.TaskAccepted);
				return obj is bool && (bool)obj;
			}
			return false;
		}

		// Token: 0x06003122 RID: 12578 RVA: 0x00121708 File Offset: 0x0011F908
		public static bool IsValidTaskStatus(TaskStatus status)
		{
			switch (status)
			{
			case TaskStatus.NotStarted:
			case TaskStatus.InProgress:
			case TaskStatus.Completed:
			case TaskStatus.WaitingOnOthers:
			case TaskStatus.Deferred:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x00121738 File Offset: 0x0011F938
		public static bool IsValidTaskPriority(Importance priority)
		{
			switch (priority)
			{
			case Importance.Low:
			case Importance.Normal:
			case Importance.High:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x00121760 File Offset: 0x0011F960
		public static void RenderReminderDate(TextWriter output, Item item, bool isEnabled)
		{
			ExDateTime selectedDate = ExDateTime.MinValue;
			if (item != null)
			{
				object obj = item.TryGetProperty(ItemSchema.ReminderDueBy);
				if (obj is ExDateTime)
				{
					selectedDate = (ExDateTime)obj;
				}
			}
			DatePickerDropDownCombo.RenderDatePicker(output, "divRemindDate", selectedDate, DatePicker.Features.TodayButton | DatePicker.Features.DropDown, isEnabled);
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x001217A0 File Offset: 0x0011F9A0
		public static void RenderReminderTimeDropDownList(UserContext userContext, TextWriter output, Item item, bool isEnabled)
		{
			ExDateTime today = ExDateTime.Today;
			if (item == null)
			{
				TimeDropDownList.RenderTimePicker(output, today.AddMinutes((double)userContext.WorkingHours.WorkDayStartTimeInWorkingHoursTimeZone), "divRemindTime", isEnabled);
				return;
			}
			object obj = item.TryGetProperty(ItemSchema.ReminderDueBy);
			if (obj is ExDateTime)
			{
				TimeDropDownList.RenderTimePicker(output, (ExDateTime)obj, "divRemindTime", isEnabled);
				return;
			}
			TimeDropDownList.RenderTimePicker(output, today.AddMinutes((double)userContext.WorkingHours.WorkDayStartTimeInWorkingHoursTimeZone), "divRemindTime", isEnabled);
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x0012181C File Offset: 0x0011FA1C
		public static void RenderInfobarMessages(Task task, Infobar infobar)
		{
			if (task != null)
			{
				if (TaskUtilities.IsAssignedTask(task))
				{
					infobar.AddMessage(-2129520243, InfobarMessageType.Informational);
					if (TaskUtilities.IsTaskAccepted(task))
					{
						infobar.AddMessage(-1021948398, InfobarMessageType.Informational);
					}
				}
				if (task.Status != TaskStatus.Completed)
				{
					SanitizedHtmlString dueByString = TaskUtilities.GetDueByString(task.DueDate);
					if (dueByString != null)
					{
						infobar.AddMessage(dueByString, InfobarMessageType.Informational, "divDueDate");
						return;
					}
				}
				else if (task.CompleteDate != null)
				{
					infobar.AddMessage(SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(-12768704), new object[]
					{
						task.CompleteDate.Value.ToString("d")
					}), InfobarMessageType.Informational, "divDtCmplt");
				}
			}
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x001218D0 File Offset: 0x0011FAD0
		public static void SetIncomplete(Task task, TaskStatus taskStatus)
		{
			if (task == null)
			{
				throw new ArgumentNullException("task");
			}
			switch (taskStatus)
			{
			case TaskStatus.NotStarted:
				task.SetStatusNotStarted();
				break;
			case TaskStatus.InProgress:
				task.SetStatusInProgress();
				break;
			case TaskStatus.Completed:
				throw new ArgumentException("task status cannot be completed");
			case TaskStatus.WaitingOnOthers:
				task.SetStatusWaitingOnOthers();
				break;
			case TaskStatus.Deferred:
				task.SetStatusDeferred();
				break;
			}
			task.DeleteProperties(new PropertyDefinition[]
			{
				ItemSchema.FlagCompleteTime
			});
			task.DeleteProperties(new PropertyDefinition[]
			{
				ItemSchema.CompleteDate
			});
		}

		// Token: 0x040021F4 RID: 8692
		public const int MaxWorkMinutes = 1525252319;

		// Token: 0x040021F5 RID: 8693
		public const int WorkMinutesInDay = 480;

		// Token: 0x040021F6 RID: 8694
		public const int WorkMinutesInWeek = 2400;

		// Token: 0x040021F7 RID: 8695
		public static readonly PropertyDefinition[] TaskPrefetchProperties = new PropertyDefinition[]
		{
			BodySchema.Codepage,
			BodySchema.InternetCpid,
			TaskSchema.StatusDescription,
			TaskSchema.TotalWork,
			TaskSchema.ActualWork,
			TaskSchema.Mileage,
			TaskSchema.BillingInformation,
			TaskSchema.Companies,
			TaskSchema.Contacts,
			TaskSchema.AssignedTime,
			TaskSchema.TaskOwner,
			TaskSchema.LastUser,
			TaskSchema.TaskDelegator,
			TaskSchema.OwnershipState,
			TaskSchema.DelegationState,
			TaskSchema.IsAssignmentEditable,
			TaskSchema.TaskType,
			TaskSchema.TaskAccepted,
			TaskSchema.IsTeamTask,
			TaskSchema.TaskChangeCount,
			TaskSchema.LastUpdateType,
			StoreObjectSchema.EffectiveRights
		};

		// Token: 0x040021F8 RID: 8696
		public static readonly EnumInfo<TaskStatus>[] TaskStatusTable = new EnumInfo<TaskStatus>[]
		{
			new EnumInfo<TaskStatus>(-27287708, TaskStatus.NotStarted),
			new EnumInfo<TaskStatus>(558434074, TaskStatus.InProgress),
			new EnumInfo<TaskStatus>(604411353, TaskStatus.Completed),
			new EnumInfo<TaskStatus>(1796266637, TaskStatus.WaitingOnOthers),
			new EnumInfo<TaskStatus>(-341200625, TaskStatus.Deferred)
		};

		// Token: 0x040021F9 RID: 8697
		public static readonly EnumInfo<Importance>[] TaskPriorityTable = new EnumInfo<Importance>[]
		{
			new EnumInfo<Importance>(1502599728, Importance.Low),
			new EnumInfo<Importance>(1690472495, Importance.Normal),
			new EnumInfo<Importance>(-77932258, Importance.High)
		};

		// Token: 0x040021FA RID: 8698
		public static readonly EnumInfo<DurationUnit>[] TaskWorkDurationTable = new EnumInfo<DurationUnit>[]
		{
			new EnumInfo<DurationUnit>(-178797907, DurationUnit.Minutes),
			new EnumInfo<DurationUnit>(-1483270941, DurationUnit.Hours),
			new EnumInfo<DurationUnit>(-1872639189, DurationUnit.Days),
			new EnumInfo<DurationUnit>(-1893458757, DurationUnit.Weeks)
		};

		// Token: 0x040021FB RID: 8699
		private static Dictionary<TaskStatus, Strings.IDs> statusToStringMap = Utilities.CreateEnumLocalizedStringMap<TaskStatus>(TaskUtilities.TaskStatusTable);

		// Token: 0x040021FC RID: 8700
		private static Dictionary<Importance, Strings.IDs> priorityToStringMap = Utilities.CreateEnumLocalizedStringMap<Importance>(TaskUtilities.TaskPriorityTable);

		// Token: 0x040021FD RID: 8701
		private static Dictionary<DurationUnit, Strings.IDs> durationToStringMap = Utilities.CreateEnumLocalizedStringMap<DurationUnit>(TaskUtilities.TaskWorkDurationTable);
	}
}
