using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.VersionedXml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Xml.Serialization.CalendarNotificationSettingsVersion1Point0
{
	// Token: 0x02000EFA RID: 3834
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class XmlSerializationWriterCalendarNotificationSettingsVersion1Point0 : XmlSerializationWriter
	{
		// Token: 0x06008425 RID: 33829 RVA: 0x0023DEEA File Offset: 0x0023C0EA
		public void Write16_CalendarNotificationSettings(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteNullTagLiteral("CalendarNotificationSettings", "");
				return;
			}
			base.TopLevelElement();
			this.Write15_Item("CalendarNotificationSettings", "", (CalendarNotificationSettingsVersion1Point0)o, true, false);
		}

		// Token: 0x06008426 RID: 33830 RVA: 0x0023DF24 File Offset: 0x0023C124
		private void Write15_Item(string n, string ns, CalendarNotificationSettingsVersion1Point0 o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(CalendarNotificationSettingsVersion1Point0)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("CalendarNotificationSettingsVersion1Point0", "");
			}
			base.WriteAttribute("Version", "", o.Version);
			this.Write7_TimeSlotMonitoringSettings("UpdateSettings", "", o.UpdateSettings, false, false);
			this.Write7_TimeSlotMonitoringSettings("ReminderSettings", "", o.ReminderSettings, false, false);
			this.Write12_TimePointScaningSettings("SummarySettings", "", o.SummarySettings, false, false);
			List<Emitter> emitters = o.Emitters;
			if (emitters != null)
			{
				for (int i = 0; i < ((ICollection)emitters).Count; i++)
				{
					this.Write14_Emitter("Emitter", "", emitters[i], false, false);
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x06008427 RID: 33831 RVA: 0x0023E020 File Offset: 0x0023C220
		private void Write14_Emitter(string n, string ns, Emitter o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(Emitter)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("Emitter", "");
			}
			base.WriteElementString("Type", "", this.Write13_EmitterType(o.Type));
			base.WriteElementStringRaw("Priority", "", XmlConvert.ToString(o.Priority));
			base.WriteElementStringRaw("Exclusive", "", XmlConvert.ToString(o.Exclusive));
			List<E164Number> phoneNumbers = o.PhoneNumbers;
			if (phoneNumbers != null)
			{
				for (int i = 0; i < ((ICollection)phoneNumbers).Count; i++)
				{
					base.WriteSerializable(phoneNumbers[i], "PhoneNumber", "", false, true);
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x06008428 RID: 33832 RVA: 0x0023E110 File Offset: 0x0023C310
		private string Write13_EmitterType(EmitterType v)
		{
			string result;
			switch (v)
			{
			case EmitterType.Unknown:
				result = "Unknown";
				break;
			case EmitterType.TextMessaging:
				result = "TextMessaging";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "Microsoft.Exchange.Data.Storage.VersionedXml.EmitterType");
			}
			return result;
		}

		// Token: 0x06008429 RID: 33833 RVA: 0x0023E160 File Offset: 0x0023C360
		private void Write12_TimePointScaningSettings(string n, string ns, TimePointScaningSettings o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(TimePointScaningSettings)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("TimePointScaningSettings", "");
			}
			base.WriteElementStringRaw("Enabled", "", XmlConvert.ToString(o.Enabled));
			base.WriteElementStringRaw("NotifyingTimeInDay", "", XmlSerializationWriter.FromDateTime(o.NotifyingTimeInDay));
			this.Write6_Duration("Duration", "", o.Duration, false, false);
			this.Write11_Recurrence("Recurrence", "", o.Recurrence, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x0600842A RID: 33834 RVA: 0x0023E230 File Offset: 0x0023C430
		private void Write11_Recurrence(string n, string ns, Recurrence o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(Recurrence)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("Recurrence", "");
			}
			base.WriteElementString("Type", "", this.Write8_RecurrenceType(o.Type));
			base.WriteElementStringRaw("Interval", "", XmlConvert.ToString(o.Interval));
			base.WriteElementStringRaw("NthDayInMonth", "", XmlConvert.ToString(o.NthDayInMonth));
			base.WriteElementString("DaysOfWeek", "", this.Write9_DaysOfWeek(o.DaysOfWeek));
			base.WriteElementString("WeekOrderInMonth", "", this.Write10_WeekOrderInMonth(o.WeekOrderInMonth));
			base.WriteElementStringRaw("MonthOrder", "", XmlConvert.ToString(o.MonthOrder));
			base.WriteEndElement(o);
		}

		// Token: 0x0600842B RID: 33835 RVA: 0x0023E340 File Offset: 0x0023C540
		private string Write10_WeekOrderInMonth(WeekOrderInMonth v)
		{
			string result;
			switch (v)
			{
			case WeekOrderInMonth.Last:
				result = "Last";
				break;
			case WeekOrderInMonth.None:
				result = "None";
				break;
			case WeekOrderInMonth.First:
				result = "First";
				break;
			case WeekOrderInMonth.Second:
				result = "Second";
				break;
			case WeekOrderInMonth.Third:
				result = "Third";
				break;
			case WeekOrderInMonth.Fourth:
				result = "Fourth";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "Microsoft.Exchange.Data.Storage.VersionedXml.WeekOrderInMonth");
			}
			return result;
		}

		// Token: 0x0600842C RID: 33836 RVA: 0x0023E418 File Offset: 0x0023C618
		private string Write9_DaysOfWeek(DaysOfWeek v)
		{
			if (v <= DaysOfWeek.Thursday)
			{
				switch (v)
				{
				case DaysOfWeek.None:
					return "None";
				case DaysOfWeek.Sunday:
					return "Sunday";
				case DaysOfWeek.Monday:
					return "Monday";
				case DaysOfWeek.Sunday | DaysOfWeek.Monday:
				case DaysOfWeek.Sunday | DaysOfWeek.Tuesday:
				case DaysOfWeek.Monday | DaysOfWeek.Tuesday:
				case DaysOfWeek.Sunday | DaysOfWeek.Monday | DaysOfWeek.Tuesday:
					break;
				case DaysOfWeek.Tuesday:
					return "Tuesday";
				case DaysOfWeek.Wednesday:
					return "Wednesday";
				default:
					if (v == DaysOfWeek.Thursday)
					{
						return "Thursday";
					}
					break;
				}
			}
			else
			{
				if (v == DaysOfWeek.Friday)
				{
					return "Friday";
				}
				switch (v)
				{
				case DaysOfWeek.Weekdays:
					return "Weekdays";
				case DaysOfWeek.Sunday | DaysOfWeek.Monday | DaysOfWeek.Tuesday | DaysOfWeek.Wednesday | DaysOfWeek.Thursday | DaysOfWeek.Friday:
					break;
				case DaysOfWeek.Saturday:
					return "Saturday";
				case DaysOfWeek.WeekendDays:
					return "WeekendDays";
				default:
					if (v == DaysOfWeek.AllDays)
					{
						return "AllDays";
					}
					break;
				}
			}
			return XmlSerializationWriter.FromEnum((long)v, new string[]
			{
				"None",
				"Sunday",
				"Monday",
				"Tuesday",
				"Wednesday",
				"Thursday",
				"Friday",
				"Saturday",
				"Weekdays",
				"WeekendDays",
				"AllDays"
			}, new long[]
			{
				0L,
				1L,
				2L,
				4L,
				8L,
				16L,
				32L,
				64L,
				62L,
				65L,
				127L
			}, "Microsoft.Exchange.Data.Storage.VersionedXml.DaysOfWeek");
		}

		// Token: 0x0600842D RID: 33837 RVA: 0x0023E584 File Offset: 0x0023C784
		private string Write8_RecurrenceType(RecurrenceType v)
		{
			string result;
			switch (v)
			{
			case RecurrenceType.Unknown:
				result = "Unknown";
				break;
			case RecurrenceType.Daily:
				result = "Daily";
				break;
			case RecurrenceType.Weekly:
				result = "Weekly";
				break;
			case RecurrenceType.Monthly:
				result = "Monthly";
				break;
			case RecurrenceType.MonthlyTh:
				result = "MonthlyTh";
				break;
			case RecurrenceType.Yearly:
				result = "Yearly";
				break;
			case RecurrenceType.YearlyTh:
				result = "YearlyTh";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "Microsoft.Exchange.Data.Storage.VersionedXml.RecurrenceType");
			}
			return result;
		}

		// Token: 0x0600842E RID: 33838 RVA: 0x0023E610 File Offset: 0x0023C810
		private void Write6_Duration(string n, string ns, Duration o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(Duration)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("Duration", "");
			}
			base.WriteElementString("Type", "", this.Write5_DurationType(o.Type));
			base.WriteElementStringRaw("Interval", "", XmlConvert.ToString(o.Interval));
			base.WriteElementStringRaw("UseWorkHoursTimeSlot", "", XmlConvert.ToString(o.UseWorkHoursTimeSlot));
			base.WriteElementStringRaw("StartTimeInDay", "", XmlSerializationWriter.FromDateTime(o.StartTimeInDay));
			base.WriteElementStringRaw("EndTimeInDay", "", XmlSerializationWriter.FromDateTime(o.EndTimeInDay));
			base.WriteElementStringRaw("NonWorkHoursExcluded", "", XmlConvert.ToString(o.NonWorkHoursExcluded));
			base.WriteEndElement(o);
		}

		// Token: 0x0600842F RID: 33839 RVA: 0x0023E71C File Offset: 0x0023C91C
		private string Write5_DurationType(DurationType v)
		{
			string result;
			switch (v)
			{
			case DurationType.Unknown:
				result = "Unknown";
				break;
			case DurationType.Days:
				result = "Days";
				break;
			case DurationType.Weeks:
				result = "Weeks";
				break;
			case DurationType.Months:
				result = "Months";
				break;
			case DurationType.Years:
				result = "Years";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "Microsoft.Exchange.Data.Storage.VersionedXml.DurationType");
			}
			return result;
		}

		// Token: 0x06008430 RID: 33840 RVA: 0x0023E790 File Offset: 0x0023C990
		private void Write7_TimeSlotMonitoringSettings(string n, string ns, TimeSlotMonitoringSettings o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(TimeSlotMonitoringSettings)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("TimeSlotMonitoringSettings", "");
			}
			base.WriteElementStringRaw("Enabled", "", XmlConvert.ToString(o.Enabled));
			base.WriteElementStringRaw("NotifyInWorkHoursTimeSlot", "", XmlConvert.ToString(o.NotifyInWorkHoursTimeSlot));
			base.WriteElementStringRaw("NotifyingStartTimeInDay", "", XmlSerializationWriter.FromDateTime(o.NotifyingStartTimeInDay));
			base.WriteElementStringRaw("NotifyingEndTimeInDay", "", XmlSerializationWriter.FromDateTime(o.NotifyingEndTimeInDay));
			this.Write6_Duration("Duration", "", o.Duration, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x06008431 RID: 33841 RVA: 0x0023E87C File Offset: 0x0023CA7C
		protected override void InitCallbacks()
		{
		}
	}
}
