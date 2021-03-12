using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.ContentTypes.iCalendar;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200082F RID: 2095
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class VAlarm : CalendarComponentBase
	{
		// Token: 0x06004E06 RID: 19974 RVA: 0x001463CE File Offset: 0x001445CE
		internal VAlarm(CalendarComponentBase root) : base(root)
		{
		}

		// Token: 0x17001622 RID: 5666
		// (get) Token: 0x06004E07 RID: 19975 RVA: 0x001463DE File Offset: 0x001445DE
		internal TimeSpan TimeSpan
		{
			get
			{
				if (this.valueType != CalendarValueType.Duration)
				{
					throw new InvalidOperationException();
				}
				return (TimeSpan)this.value;
			}
		}

		// Token: 0x17001623 RID: 5667
		// (get) Token: 0x06004E08 RID: 19976 RVA: 0x001463FB File Offset: 0x001445FB
		internal ExDateTime ExDateTime
		{
			get
			{
				if (this.valueType != CalendarValueType.DateTime)
				{
					throw new InvalidOperationException();
				}
				return (ExDateTime)((DateTime)this.value);
			}
		}

		// Token: 0x17001624 RID: 5668
		// (get) Token: 0x06004E09 RID: 19977 RVA: 0x0014641D File Offset: 0x0014461D
		internal CalendarValueType ValueType
		{
			get
			{
				return this.valueType;
			}
		}

		// Token: 0x17001625 RID: 5669
		// (get) Token: 0x06004E0A RID: 19978 RVA: 0x00146425 File Offset: 0x00144625
		internal TriggerRelationship TriggerRelationship
		{
			get
			{
				return this.triggerRelationship;
			}
		}

		// Token: 0x17001626 RID: 5670
		// (get) Token: 0x06004E0B RID: 19979 RVA: 0x0014642D File Offset: 0x0014462D
		internal VAlarmAction Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x17001627 RID: 5671
		// (get) Token: 0x06004E0C RID: 19980 RVA: 0x00146435 File Offset: 0x00144635
		internal string Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x06004E0D RID: 19981 RVA: 0x00146440 File Offset: 0x00144640
		internal static void Demote(CalendarWriter calendarWriter, TimeSpan minutes, string description, string recipientAddress)
		{
			calendarWriter.StartComponent(ComponentId.VAlarm);
			calendarWriter.WriteProperty(PropertyId.Description, description);
			calendarWriter.StartProperty(PropertyId.Trigger);
			calendarWriter.WriteParameter("RELATED", "START");
			calendarWriter.WritePropertyValue(minutes);
			if (recipientAddress == null)
			{
				calendarWriter.WriteProperty(PropertyId.Action, "DISPLAY");
			}
			else
			{
				calendarWriter.WriteProperty(PropertyId.Action, "EMAIL");
				calendarWriter.WriteProperty(PropertyId.Summary, "Reminder");
				calendarWriter.WriteProperty(PropertyId.Attendee, recipientAddress);
			}
			calendarWriter.EndComponent();
		}

		// Token: 0x06004E0E RID: 19982 RVA: 0x001464BC File Offset: 0x001446BC
		public static int CalculateReminderMinutesBeforeStart(VAlarm valarm, ExDateTime startTime, ExDateTime endTime)
		{
			int result;
			if (valarm.ValueType == CalendarValueType.Duration)
			{
				if (valarm.TriggerRelationship == TriggerRelationship.Start || valarm.TriggerRelationship == TriggerRelationship.None)
				{
					result = ((valarm.TimeSpan != TimeSpan.MinValue) ? ((int)valarm.TimeSpan.Negate().TotalMinutes) : 15);
				}
				else if (valarm.TriggerRelationship == TriggerRelationship.End)
				{
					int num = (int)valarm.TimeSpan.TotalMinutes;
					ExDateTime dt = endTime.AddMinutes((double)num);
					result = (int)(startTime - dt).TotalMinutes;
				}
				else
				{
					result = 15;
				}
			}
			else
			{
				result = (int)(startTime - valarm.ExDateTime).TotalMinutes;
			}
			return result;
		}

		// Token: 0x06004E0F RID: 19983 RVA: 0x0014656C File Offset: 0x0014476C
		public static void PromoteEmailReminders(Item item, List<VAlarm> emailVAlarms, ExDateTime startTime, ExDateTime endTime, bool isOccurrence)
		{
			if (item != null && emailVAlarms != null && emailVAlarms.Count > 0)
			{
				Reminders<EventTimeBasedInboxReminder> reminders = new Reminders<EventTimeBasedInboxReminder>();
				foreach (VAlarm valarm in emailVAlarms)
				{
					int reminderOffset = VAlarm.CalculateReminderMinutesBeforeStart(valarm, startTime, endTime);
					EventTimeBasedInboxReminder eventTimeBasedInboxReminder = new EventTimeBasedInboxReminder();
					eventTimeBasedInboxReminder.CustomMessage = valarm.Message;
					eventTimeBasedInboxReminder.ReminderOffset = reminderOffset;
					eventTimeBasedInboxReminder.OccurrenceChange = (isOccurrence ? EmailReminderChangeType.Added : EmailReminderChangeType.None);
					reminders.ReminderList.Add(eventTimeBasedInboxReminder);
				}
				Reminders<EventTimeBasedInboxReminder>.Set(item, InternalSchema.EventTimeBasedInboxReminders, reminders);
			}
		}

		// Token: 0x06004E10 RID: 19984 RVA: 0x00146618 File Offset: 0x00144818
		protected override void ProcessProperty(CalendarPropertyBase calendarProperty)
		{
			PropertyId propertyId = calendarProperty.CalendarPropertyId.PropertyId;
			if (propertyId != PropertyId.Description)
			{
				switch (propertyId)
				{
				case PropertyId.Action:
					if (string.Compare((string)calendarProperty.Value, "DISPLAY", StringComparison.CurrentCultureIgnoreCase) == 0)
					{
						this.action = VAlarmAction.Display;
						return;
					}
					if (string.Compare((string)calendarProperty.Value, "EMAIL", StringComparison.CurrentCultureIgnoreCase) == 0)
					{
						this.action = VAlarmAction.Email;
						return;
					}
					if (string.Compare((string)calendarProperty.Value, "AUDIO", StringComparison.CurrentCultureIgnoreCase) == 0)
					{
						this.action = VAlarmAction.Display;
						return;
					}
					this.action = VAlarmAction.Unknown;
					return;
				case PropertyId.Repeat:
					break;
				case PropertyId.Trigger:
					this.CheckTriggerArguments(calendarProperty);
					this.value = calendarProperty.Value;
					if (this.value != null && typeof(TimeSpan).GetTypeInfo().IsAssignableFrom(this.value.GetType().GetTypeInfo()))
					{
						this.valueType = CalendarValueType.Duration;
						return;
					}
					if (this.value != null && typeof(DateTime).GetTypeInfo().IsAssignableFrom(this.value.GetType().GetTypeInfo()))
					{
						this.valueType = CalendarValueType.DateTime;
						return;
					}
					this.valueType = calendarProperty.ValueType;
					return;
				default:
					return;
				}
			}
			else
			{
				this.message = (string)calendarProperty.Value;
			}
		}

		// Token: 0x06004E11 RID: 19985 RVA: 0x00146756 File Offset: 0x00144956
		protected override bool ValidateProperties()
		{
			return this.value != null && this.action != VAlarmAction.Unknown;
		}

		// Token: 0x06004E12 RID: 19986 RVA: 0x00146770 File Offset: 0x00144970
		private void CheckTriggerArguments(CalendarPropertyBase property)
		{
			this.triggerRelationship = TriggerRelationship.None;
			foreach (CalendarParameter calendarParameter in property.Parameters)
			{
				if (calendarParameter.ParameterId == ParameterId.TriggerRelationship)
				{
					if (string.Compare((string)calendarParameter.Value, "START", StringComparison.CurrentCultureIgnoreCase) == 0)
					{
						this.triggerRelationship = TriggerRelationship.Start;
						break;
					}
					if (string.Compare((string)calendarParameter.Value, "END", StringComparison.CurrentCultureIgnoreCase) == 0)
					{
						this.triggerRelationship = TriggerRelationship.End;
						break;
					}
					this.triggerRelationship = TriggerRelationship.Unknown;
					break;
				}
			}
		}

		// Token: 0x04002A9A RID: 10906
		private CalendarValueType valueType;

		// Token: 0x04002A9B RID: 10907
		private object value;

		// Token: 0x04002A9C RID: 10908
		private TriggerRelationship triggerRelationship;

		// Token: 0x04002A9D RID: 10909
		private VAlarmAction action = VAlarmAction.Unknown;

		// Token: 0x04002A9E RID: 10910
		private string message;
	}
}
