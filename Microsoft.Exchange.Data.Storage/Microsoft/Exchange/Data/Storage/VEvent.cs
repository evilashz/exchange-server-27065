using System;
using System.Collections.Generic;
using Microsoft.Exchange.Calendar;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.ContentTypes.iCalendar;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000815 RID: 2069
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class VEvent : VItemBase
	{
		// Token: 0x06004D51 RID: 19793 RVA: 0x00141A4A File Offset: 0x0013FC4A
		static VEvent()
		{
			VEvent.CreateSchema();
		}

		// Token: 0x06004D52 RID: 19794 RVA: 0x00141A51 File Offset: 0x0013FC51
		internal static Dictionary<object, SchemaInfo> GetConversionSchema()
		{
			return VEvent.conversionSchema;
		}

		// Token: 0x06004D53 RID: 19795 RVA: 0x00141A58 File Offset: 0x0013FC58
		private static void AddSchemaInfo(CalendarPropertyId calendarPropertyId, object promotionMethod, object demotionMethod, CalendarMethod methods, IcalFlags flags)
		{
			VEvent.conversionSchema.Add(calendarPropertyId.Key, new SchemaInfo(calendarPropertyId, promotionMethod, demotionMethod, methods, flags));
		}

		// Token: 0x06004D54 RID: 19796 RVA: 0x00141A75 File Offset: 0x0013FC75
		private static void AddSchemaInfo(PropertyId propertyId, object promotionMethod, object demotionMethod)
		{
			VEvent.AddSchemaInfo(new CalendarPropertyId(propertyId), promotionMethod, demotionMethod, CalendarMethod.All, IcalFlags.None);
		}

		// Token: 0x06004D55 RID: 19797 RVA: 0x00141A8A File Offset: 0x0013FC8A
		private static void AddSchemaInfo(string propertyName, object promotionMethod, object demotionMethod)
		{
			VEvent.AddSchemaInfo(new CalendarPropertyId(propertyName), promotionMethod, demotionMethod, CalendarMethod.All, IcalFlags.None);
		}

		// Token: 0x06004D56 RID: 19798 RVA: 0x00141AA0 File Offset: 0x0013FCA0
		private static void CreateSchema()
		{
			VEvent.conversionSchema = new Dictionary<object, SchemaInfo>();
			VEvent.AddSchemaInfo(PropertyId.Summary, new PromotePropertyDelegate(VItemBase.PromoteSubject), new DemotePropertyDelegate(VEvent.DemoteSubject));
			VEvent.AddSchemaInfo(PropertyId.DateTimeStart, null, new DemotePropertyDelegate(VEvent.DemoteStartTime));
			VEvent.AddSchemaInfo(PropertyId.DateTimeEnd, null, new DemotePropertyDelegate(VEvent.DemoteEndTime));
			VEvent.AddSchemaInfo(PropertyId.Uid, null, new DemotePropertyDelegate(VEvent.DemoteUid));
			VEvent.AddSchemaInfo(PropertyId.Organizer, new PromotePropertyDelegate(VEvent.PromoteOrganizer), null);
			VEvent.AddSchemaInfo(PropertyId.Description, new PromotePropertyDelegate(VItemBase.PromoteDescription), null);
			VEvent.AddSchemaInfo(PropertyId.Comment, new PromotePropertyDelegate(VItemBase.PromoteComment), null);
			VEvent.AddSchemaInfo(PropertyId.RecurrenceId, null, new DemotePropertyDelegate(VEvent.DemoteRecurrenceId));
			VEvent.AddSchemaInfo(PropertyId.Class, new PromotePropertyDelegate(VItemBase.PromoteClass), new DemotePropertyDelegate(VItemBase.DemoteClass));
			VEvent.AddSchemaInfo(PropertyId.Priority, new PromotePropertyDelegate(VItemBase.PromotePriority), new DemotePropertyDelegate(VEvent.DemotePriority));
			VEvent.AddSchemaInfo(PropertyId.DateTimeStamp, new PromotePropertyDelegate(VEvent.PromoteDtStamp), new DemotePropertyDelegate(VEvent.DemoteDtStamp));
			VEvent.AddSchemaInfo(PropertyId.Transparency, new PromotePropertyDelegate(VEvent.PromoteTransp), new DemotePropertyDelegate(VEvent.DemoteTransp));
			VEvent.AddSchemaInfo(PropertyId.Status, new PromotePropertyDelegate(VEvent.PromoteStatus), new DemotePropertyDelegate(VEvent.DemoteStatus));
			VEvent.AddSchemaInfo(PropertyId.Sequence, new PromotePropertyDelegate(VEvent.PromoteSequence), new DemotePropertyDelegate(VEvent.DemoteSequence));
			VEvent.AddSchemaInfo(new CalendarPropertyId(PropertyId.Categories), new PromotePropertyDelegate(VEvent.PromoteCategories), new DemotePropertyDelegate(VEvent.DemoteCategories), CalendarMethod.All, IcalFlags.MultiValue);
			VEvent.AddSchemaInfo(new CalendarPropertyId(PropertyId.ExceptionDate), null, null, CalendarMethod.All, IcalFlags.MultiValue);
			VEvent.AddSchemaInfo(new CalendarPropertyId("X-MICROSOFT-EXDATE"), null, null, CalendarMethod.All, IcalFlags.MultiValue);
			VEvent.AddSchemaInfo(PropertyId.Location, new PromotePropertyDelegate(VEvent.PromoteLocation), new DemotePropertyDelegate(VEvent.DemoteLocation));
			VEvent.AddSchemaInfo(PropertyId.Contact, new PromotePropertyDelegate(VEvent.PromoteContact), new DemotePropertyDelegate(VEvent.DemoteContact));
			VEvent.AddSchemaInfo("X-MICROSOFT-CDO-APPT-SEQUENCE", new PromotePropertyDelegate(VEvent.PromoteXApptSequence), new DemotePropertyDelegate(VEvent.DemoteXApptSequence));
			VEvent.AddSchemaInfo("X-MICROSOFT-CDO-OWNERAPPTID", new PromotePropertyDelegate(VEvent.PromoteXOwnerApptId), new DemotePropertyDelegate(VEvent.DemoteXOwnerApptId));
			VEvent.AddSchemaInfo("X-MICROSOFT-CDO-BUSYSTATUS", new PromotePropertyDelegate(VEvent.PromoteXBusyStatus), new DemotePropertyDelegate(VEvent.DemoteXBusyStatus));
			VEvent.AddSchemaInfo("X-MICROSOFT-CDO-INTENDEDSTATUS", new PromotePropertyDelegate(VEvent.PromoteXIntendedStatus), new DemotePropertyDelegate(VEvent.DemoteXIntendedStatus));
			VEvent.AddSchemaInfo("X-MICROSOFT-CDO-ALLDAYEVENT", new PromotePropertyDelegate(VEvent.PromoteXAllDayEvent), new DemotePropertyDelegate(VEvent.DemoteXAllDayEvent));
			VEvent.AddSchemaInfo("X-MICROSOFT-CDO-IMPORTANCE", new PromotePropertyDelegate(VItemBase.PromoteXImportance), new DemotePropertyDelegate(VItemBase.DemoteXImportance));
			VEvent.AddSchemaInfo("X-MICROSOFT-CDO-INSTTYPE", new PromotePropertyDelegate(VEvent.PromoteXInstanceType), new DemotePropertyDelegate(VEvent.DemoteXInstanceType));
			VEvent.AddSchemaInfo("X-MICROSOFT-ISORGANIZER", new PromotePropertyDelegate(VEvent.PromoteXIsOrganizer), null);
			VEvent.AddSchemaInfo("X-MICROSOFT-CHARMID", new PromotePropertyDelegate(VEvent.PromoteXCharmId), null);
			VEvent.AddSchemaInfo("X-MICROSOFT-DISALLOW-COUNTER", new PromotePropertyDelegate(VEvent.PromoteXDisallowCounter), new DemotePropertyDelegate(VEvent.DemoteXDisallowCounter));
		}

		// Token: 0x06004D57 RID: 19799 RVA: 0x00141DEC File Offset: 0x0013FFEC
		private static bool PromoteOrganizer(VEvent vevent, CalendarPropertyBase property)
		{
			if (vevent.item == null)
			{
				return true;
			}
			CalendarAttendee calendarAttendee = (CalendarAttendee)property;
			if (CalendarUtil.IsReplyOrCounter(vevent.Context.Method))
			{
				Participant participant = InboundMimeHeadersParser.CreateParticipantFromMime(calendarAttendee.Name, calendarAttendee.Address, vevent.InboundContext.Options, true);
				if (participant == null)
				{
					return false;
				}
				vevent.addressCache.Participants[ConversionItemParticipants.ParticipantIndex.ReceivedRepresenting] = participant;
			}
			else
			{
				Participant participant2 = InboundMimeHeadersParser.CreateParticipantFromMime(calendarAttendee.Name, calendarAttendee.Address, vevent.InboundContext.Options, vevent.IsResolvingFromAndSender);
				Participant participant3 = string.IsNullOrEmpty(calendarAttendee.SentBy) ? participant2 : InboundMimeHeadersParser.CreateParticipantFromMime(null, calendarAttendee.SentBy, vevent.InboundContext.Options, vevent.IsResolvingFromAndSender);
				if (participant2 == null || participant3 == null)
				{
					return false;
				}
				vevent.addressCache.Participants[ConversionItemParticipants.ParticipantIndex.From] = participant2;
				vevent.addressCache.Participants[ConversionItemParticipants.ParticipantIndex.Sender] = participant3;
			}
			return true;
		}

		// Token: 0x06004D58 RID: 19800 RVA: 0x00141EE8 File Offset: 0x001400E8
		private static bool PromoteCategories(VEvent vevent, CalendarPropertyBase property)
		{
			List<object> list = property.Value as List<object>;
			if (list != null)
			{
				List<string> list2 = new List<string>();
				foreach (object obj in list)
				{
					string text = obj as string;
					if (!string.IsNullOrEmpty(text))
					{
						list2.Add(text);
					}
				}
				vevent.SetProperty(InternalSchema.Keywords, list2.ToArray());
			}
			return true;
		}

		// Token: 0x06004D59 RID: 19801 RVA: 0x00141F70 File Offset: 0x00140170
		private static bool PromoteDtStamp(VEvent vevent, CalendarPropertyBase property)
		{
			if (!CalendarUtil.IsReplyOrCounter(vevent.Context.Method))
			{
				vevent.SetProperty(InternalSchema.OwnerCriticalChangeTime, property.Value);
			}
			else
			{
				vevent.SetProperty(InternalSchema.AttendeeCriticalChangeTime, property.Value);
			}
			return true;
		}

		// Token: 0x06004D5A RID: 19802 RVA: 0x00141FAC File Offset: 0x001401AC
		private static bool PromoteXOwnerApptId(VEvent vevent, CalendarPropertyBase property)
		{
			int num;
			if (!int.TryParse((string)property.Value, out num))
			{
				string name = property.CalendarPropertyId.PropertyId.ToString();
				ExTraceGlobals.ICalTracer.TraceError<string, object>((long)vevent.GetHashCode(), "VEvent::PromoteXOwnerApptId. Invalid OAID. UID:'{0}'. Value:'{1}'", vevent.Uid, property.Value);
				vevent.Context.AddError(ServerStrings.InvalidICalElement(name));
			}
			else
			{
				vevent.SetProperty(InternalSchema.OwnerAppointmentID, num);
			}
			return true;
		}

		// Token: 0x06004D5B RID: 19803 RVA: 0x0014202C File Offset: 0x0014022C
		private static bool PromoteXBusyStatus(VEvent vevent, CalendarPropertyBase property)
		{
			BusyType busyType = CalendarUtil.BusyTypeFromStringOrDefault((string)property.Value, BusyType.Unknown);
			if (busyType != BusyType.Unknown)
			{
				vevent.busyStatus = busyType;
				vevent.BusyStatusPromotedBasedOnXBusyStatus = true;
			}
			return true;
		}

		// Token: 0x06004D5C RID: 19804 RVA: 0x0014205E File Offset: 0x0014025E
		private static bool PromoteXIntendedStatus(VEvent vevent, CalendarPropertyBase property)
		{
			vevent.SetProperty(InternalSchema.IntendedFreeBusyStatus, CalendarUtil.BusyTypeFromString((string)property.Value));
			vevent.HasExchangeIntendedStatus = true;
			return true;
		}

		// Token: 0x06004D5D RID: 19805 RVA: 0x00142088 File Offset: 0x00140288
		private static bool PromoteTransp(VEvent vevent, CalendarPropertyBase property)
		{
			vevent.SetProperty(InternalSchema.Transparent, property.Value);
			BusyType busyType = CalendarUtil.BusyTypeFromTranspStringOrDefault((string)property.Value, BusyType.Unknown);
			if (!vevent.BusyStatusPromotedBasedOnXBusyStatus && busyType != BusyType.Unknown)
			{
				vevent.busyStatus = busyType;
			}
			return true;
		}

		// Token: 0x06004D5E RID: 19806 RVA: 0x001420CC File Offset: 0x001402CC
		private static bool PromoteStatus(VEvent vevent, CalendarPropertyBase property)
		{
			if (vevent.busyStatus == BusyType.Unknown)
			{
				vevent.busyStatus = CalendarUtil.BusyTypeFromStringOrDefault((string)property.Value, BusyType.Unknown);
			}
			return true;
		}

		// Token: 0x06004D5F RID: 19807 RVA: 0x001420F0 File Offset: 0x001402F0
		private static bool PromoteXAllDayEvent(VEvent vevent, CalendarPropertyBase property)
		{
			bool? flag = CalendarUtil.BooleanFromString((string)property.Value);
			if (flag != null)
			{
				vevent.SetProperty(InternalSchema.MapiIsAllDayEvent, flag.Value);
				return true;
			}
			return false;
		}

		// Token: 0x06004D60 RID: 19808 RVA: 0x00142134 File Offset: 0x00140334
		private static bool PromoteXInstanceType(VEvent vevent, CalendarPropertyBase property)
		{
			if (!int.TryParse((string)property.Value, out vevent.instanceType))
			{
				ExTraceGlobals.ICalTracer.TraceError<LocalizedString>((long)vevent.GetHashCode(), "VEvent::PromoteXInstanceType. {0}.", ServerStrings.InvalidICalElement(property.CalendarPropertyId.PropertyId.ToString()));
			}
			return true;
		}

		// Token: 0x06004D61 RID: 19809 RVA: 0x0014218C File Offset: 0x0014038C
		private static bool PromoteXIsOrganizer(VEvent vevent, CalendarPropertyBase property)
		{
			if (vevent.item != null && vevent.item.Session.IsOlcMoveDestination)
			{
				bool? flag = CalendarUtil.BooleanFromString((string)property.Value);
				if (flag != null && !flag.Value)
				{
					vevent.AppendAppointmentStateFlags(AppointmentStateFlags.Received);
				}
			}
			return true;
		}

		// Token: 0x06004D62 RID: 19810 RVA: 0x001421E0 File Offset: 0x001403E0
		private static bool PromoteXCharmId(VEvent vevent, CalendarPropertyBase property)
		{
			string propertyValue = (string)property.Value;
			vevent.SetProperty(InternalSchema.CharmId, propertyValue);
			return true;
		}

		// Token: 0x06004D63 RID: 19811 RVA: 0x00142208 File Offset: 0x00140408
		private static bool PromoteXDisallowCounter(VEvent vevent, CalendarPropertyBase property)
		{
			bool? flag = CalendarUtil.BooleanFromString((string)property.Value);
			if (flag != null)
			{
				vevent.SetProperty(InternalSchema.DisallowNewTimeProposal, flag.Value);
				return true;
			}
			return false;
		}

		// Token: 0x06004D64 RID: 19812 RVA: 0x0014224C File Offset: 0x0014044C
		private static bool PromoteXApptSequence(VEvent vevent, CalendarPropertyBase property)
		{
			if (!int.TryParse((string)property.Value, out vevent.sequence))
			{
				ExTraceGlobals.ICalTracer.TraceError<LocalizedString>((long)vevent.GetHashCode(), "VEvent::PromoteXApptSequence. {0}.", ServerStrings.InvalidICalElement(property.CalendarPropertyId.PropertyId.ToString()));
			}
			return true;
		}

		// Token: 0x06004D65 RID: 19813 RVA: 0x001422A4 File Offset: 0x001404A4
		private static bool PromoteSequence(VEvent vevent, CalendarPropertyBase property)
		{
			int num = (int)property.Value;
			if (vevent.sequence == -1 && num != 0)
			{
				vevent.sequence = num;
			}
			return true;
		}

		// Token: 0x06004D66 RID: 19814 RVA: 0x001422D4 File Offset: 0x001404D4
		private static bool PromoteLocation(VEvent vevent, CalendarPropertyBase property)
		{
			vevent.SetProperty(InternalSchema.Location, property.Value);
			foreach (CalendarParameter calendarParameter in property.Parameters)
			{
				if (calendarParameter.Name == "ALTREP")
				{
					vevent.SetProperty(InternalSchema.LocationURL, calendarParameter.Value);
					break;
				}
			}
			return true;
		}

		// Token: 0x06004D67 RID: 19815 RVA: 0x00142358 File Offset: 0x00140558
		private static bool PromoteContact(VEvent vevent, CalendarPropertyBase property)
		{
			vevent.SetProperty(InternalSchema.Contact, property.Value);
			foreach (CalendarParameter calendarParameter in property.Parameters)
			{
				if (calendarParameter.Name == "ALTREP")
				{
					vevent.SetProperty(InternalSchema.ContactURL, calendarParameter.Value);
					break;
				}
			}
			return true;
		}

		// Token: 0x06004D68 RID: 19816 RVA: 0x001423DC File Offset: 0x001405DC
		private static void DemoteDtStamp(VEvent vevent)
		{
			ExDateTime? valueAsNullable;
			if (!CalendarUtil.IsReplyOrCounter(vevent.Context.Method))
			{
				valueAsNullable = vevent.GetValueAsNullable<ExDateTime>(InternalSchema.OwnerCriticalChangeTime);
			}
			else
			{
				valueAsNullable = vevent.GetValueAsNullable<ExDateTime>(InternalSchema.AttendeeCriticalChangeTime);
			}
			if (valueAsNullable == null)
			{
				valueAsNullable = vevent.GetValueAsNullable<ExDateTime>(InternalSchema.LastModifiedTime);
			}
			if (valueAsNullable != null)
			{
				valueAsNullable = new ExDateTime?(ExTimeZone.UtcTimeZone.ConvertDateTime(valueAsNullable.Value));
			}
			else
			{
				valueAsNullable = new ExDateTime?(ExDateTime.UtcNow);
			}
			vevent.calendarWriter.StartProperty(PropertyId.DateTimeStamp);
			vevent.calendarWriter.WritePropertyValue((DateTime)valueAsNullable.Value.ToUtc());
		}

		// Token: 0x06004D69 RID: 19817 RVA: 0x00142488 File Offset: 0x00140688
		private static void DemoteSequence(VEvent vevent)
		{
			int value = Math.Max(vevent.GetValueOrDefault<int>(InternalSchema.AppointmentSequenceNumber), vevent.GetValueOrDefault<int>(InternalSchema.AppointmentLastSequenceNumber));
			vevent.calendarWriter.StartProperty(PropertyId.Sequence);
			vevent.calendarWriter.WritePropertyValue(value);
		}

		// Token: 0x06004D6A RID: 19818 RVA: 0x001424CC File Offset: 0x001406CC
		private static void DemoteUid(VEvent vevent)
		{
			if (vevent.master != null)
			{
				vevent.uid = vevent.master.uid;
			}
			else
			{
				if (vevent.item == null)
				{
					throw new InvalidOperationException("VEvent.item and VEvent.master can't be both null");
				}
				string text = null;
				try
				{
					text = new GlobalObjectId(vevent.item)
					{
						Date = ExDateTime.MinValue
					}.Uid;
				}
				catch (CorruptDataException ex)
				{
					ExTraceGlobals.ICalTracer.TraceError<CorruptDataException>((long)vevent.GetHashCode(), "VEvent::DemoteUid. Invalid GOID. Found exception:'{0}'", ex);
					if (vevent.Context.Method != CalendarMethod.Publish)
					{
						throw new ConversionFailedException(ConversionFailureReason.CorruptContent, ex);
					}
				}
				vevent.uid = (text ?? Guid.NewGuid().ToString());
			}
			vevent.calendarWriter.WriteProperty(PropertyId.Uid, vevent.uid);
		}

		// Token: 0x06004D6B RID: 19819 RVA: 0x001425A0 File Offset: 0x001407A0
		private static void DemoteCategories(VEvent vevent)
		{
			string[] array = vevent.TryGetProperty(InternalSchema.Keywords) as string[];
			if (array != null && array.Length > 0)
			{
				vevent.calendarWriter.StartProperty(PropertyId.Categories);
				for (int i = 0; i < array.Length; i++)
				{
					vevent.calendarWriter.WritePropertyValue(array[i]);
				}
			}
		}

		// Token: 0x06004D6C RID: 19820 RVA: 0x001425F0 File Offset: 0x001407F0
		private static void DemotePriority(VEvent vevent)
		{
			int value;
			switch (vevent.GetValueOrDefault<Importance>(InternalSchema.Importance, Importance.Normal))
			{
			case Importance.Low:
				value = 9;
				goto IL_2E;
			case Importance.High:
				value = 1;
				goto IL_2E;
			}
			value = 5;
			IL_2E:
			vevent.calendarWriter.StartProperty(PropertyId.Priority);
			vevent.calendarWriter.WritePropertyValue(value);
		}

		// Token: 0x06004D6D RID: 19821 RVA: 0x00142644 File Offset: 0x00140844
		private static void DemoteRecurrenceId(VEvent vevent)
		{
			ExDateTime exDateTime;
			if (vevent.exceptionInfo == null)
			{
				if (vevent.item != null)
				{
					try
					{
						GlobalObjectId globalObjectId = new GlobalObjectId(vevent.item);
						exDateTime = vevent.timeZone.Assign(globalObjectId.Date);
						if (exDateTime != ExDateTime.MinValue)
						{
							exDateTime += Util.ConvertSCDTimeToTimeSpan(vevent.GetValueOrDefault<int>(InternalSchema.StartRecurTime));
						}
						goto IL_AE;
					}
					catch (CorruptDataException ex)
					{
						if (vevent.Context.Method != CalendarMethod.Publish)
						{
							throw new ConversionFailedException(ConversionFailureReason.CorruptContent, ex);
						}
						ExTraceGlobals.ICalTracer.TraceDebug<CorruptDataException>((long)vevent.GetHashCode(), "VEvent::DemoteRecurrenceId. Found exception:'{0}'.", ex);
						exDateTime = ExDateTime.MinValue;
						goto IL_AE;
					}
				}
				throw new InvalidOperationException("VEvent.item and VEvent.exceptionInfo can't be both null");
			}
			exDateTime = vevent.exceptionInfo.OriginalStartTime.Date;
			IL_AE:
			if (exDateTime != ExDateTime.MinValue)
			{
				VEvent.DemoteDateTime(vevent, PropertyId.RecurrenceId, exDateTime);
			}
		}

		// Token: 0x06004D6E RID: 19822 RVA: 0x00142728 File Offset: 0x00140928
		private static void DemoteDateTime(VEvent vevent, PropertyId propertyId, ExDateTime dateTime)
		{
			vevent.calendarWriter.StartProperty(propertyId);
			VItemBase.DemoteDateTimeValue(vevent, dateTime);
		}

		// Token: 0x06004D6F RID: 19823 RVA: 0x0014273D File Offset: 0x0014093D
		private static void DemoteDateTime(VEvent vevent, string propertyName, ExDateTime dateTime)
		{
			vevent.calendarWriter.StartProperty(propertyName);
			VItemBase.DemoteDateTimeValue(vevent, dateTime);
		}

		// Token: 0x06004D70 RID: 19824 RVA: 0x00142754 File Offset: 0x00140954
		private static void DemoteStartTime(VEvent vevent)
		{
			object obj = vevent.TryGetProperty(InternalSchema.StartTime);
			if (obj != null && !(obj is PropertyError))
			{
				VEvent.DemoteStartOrEndTime(vevent, PropertyId.DateTimeStart, obj);
			}
		}

		// Token: 0x06004D71 RID: 19825 RVA: 0x00142784 File Offset: 0x00140984
		private static void DemoteEndTime(VEvent vevent)
		{
			object obj = vevent.TryGetProperty(InternalSchema.EndTime);
			if (obj != null && !(obj is PropertyError))
			{
				VEvent.DemoteStartOrEndTime(vevent, PropertyId.DateTimeEnd, obj);
			}
		}

		// Token: 0x06004D72 RID: 19826 RVA: 0x001427B4 File Offset: 0x001409B4
		private static void DemoteStartOrEndTime(VEvent vevent, PropertyId propertyId, object dateTime)
		{
			vevent.calendarWriter.StartProperty(propertyId);
			if (!(dateTime is ExDateTime))
			{
				vevent.calendarWriter.WritePropertyValue(dateTime);
				return;
			}
			bool valueOrDefault = vevent.GetValueOrDefault<bool>(InternalSchema.IsAllDayEvent);
			if (valueOrDefault)
			{
				vevent.calendarWriter.StartParameter(ParameterId.ValueType);
				vevent.calendarWriter.WriteParameterValue("DATE");
				ExDateTime exDateTime = (ExDateTime)dateTime;
				vevent.calendarWriter.WritePropertyValue((DateTime)exDateTime, CalendarValueType.Date);
				return;
			}
			VItemBase.DemoteDateTimeValue(vevent, (ExDateTime)dateTime);
		}

		// Token: 0x06004D73 RID: 19827 RVA: 0x00142838 File Offset: 0x00140A38
		private static void DemoteStatus(VEvent vevent)
		{
			int valueOrDefault = vevent.GetValueOrDefault<int>(InternalSchema.AppointmentState, 0);
			string value;
			if ((valueOrDefault & 4) != 0)
			{
				value = "CANCELLED";
			}
			else
			{
				BusyType valueOrDefault2 = vevent.GetValueOrDefault<BusyType>(InternalSchema.IntendedFreeBusyStatus, BusyType.Busy);
				if (valueOrDefault2 == BusyType.Tentative)
				{
					value = "TENTATIVE";
				}
				else
				{
					value = "CONFIRMED";
				}
			}
			vevent.calendarWriter.StartProperty(PropertyId.Status);
			vevent.calendarWriter.WritePropertyValue(value);
		}

		// Token: 0x06004D74 RID: 19828 RVA: 0x00142898 File Offset: 0x00140A98
		private static void DemoteTransp(VEvent vevent)
		{
			string valueOrDefault = vevent.GetValueOrDefault<string>(InternalSchema.Transparent, "OPAQUE");
			vevent.calendarWriter.StartProperty(PropertyId.Transparency);
			vevent.calendarWriter.WritePropertyValue(valueOrDefault);
		}

		// Token: 0x06004D75 RID: 19829 RVA: 0x001428D0 File Offset: 0x00140AD0
		private static void DemoteXOwnerApptId(VEvent vevent)
		{
			int? valueAsNullable = vevent.GetValueAsNullable<int>(InternalSchema.OwnerAppointmentID);
			if (valueAsNullable != null)
			{
				vevent.calendarWriter.WriteProperty("X-MICROSOFT-CDO-OWNERAPPTID", valueAsNullable.Value.ToString());
			}
		}

		// Token: 0x06004D76 RID: 19830 RVA: 0x00142914 File Offset: 0x00140B14
		private static void DemoteXBusyStatus(VEvent vevent)
		{
			BusyType valueOrDefault = vevent.GetValueOrDefault<BusyType>(InternalSchema.FreeBusyStatus, BusyType.Busy);
			string value = CalendarUtil.BusyTypeToString(valueOrDefault);
			vevent.calendarWriter.StartProperty("X-MICROSOFT-CDO-BUSYSTATUS");
			vevent.calendarWriter.WritePropertyValue(value);
		}

		// Token: 0x06004D77 RID: 19831 RVA: 0x00142954 File Offset: 0x00140B54
		private static void DemoteXIntendedStatus(VEvent vevent)
		{
			BusyType valueOrDefault = vevent.GetValueOrDefault<BusyType>(InternalSchema.IntendedFreeBusyStatus, BusyType.Busy);
			string value = CalendarUtil.BusyTypeToString(valueOrDefault);
			vevent.calendarWriter.StartProperty("X-MICROSOFT-CDO-INTENDEDSTATUS");
			vevent.calendarWriter.WritePropertyValue(value);
		}

		// Token: 0x06004D78 RID: 19832 RVA: 0x00142994 File Offset: 0x00140B94
		private static void DemoteXAllDayEvent(VEvent vevent)
		{
			bool valueOrDefault = vevent.GetValueOrDefault<bool>(InternalSchema.IsAllDayEvent);
			vevent.calendarWriter.StartProperty("X-MICROSOFT-CDO-ALLDAYEVENT");
			vevent.calendarWriter.WritePropertyValue(valueOrDefault);
		}

		// Token: 0x06004D79 RID: 19833 RVA: 0x001429CC File Offset: 0x00140BCC
		private static void DemoteXApptSequence(VEvent vevent)
		{
			int value = Math.Max(vevent.GetValueOrDefault<int>(InternalSchema.AppointmentSequenceNumber), vevent.GetValueOrDefault<int>(InternalSchema.AppointmentLastSequenceNumber));
			vevent.calendarWriter.StartProperty("X-MICROSOFT-CDO-APPT-SEQUENCE");
			vevent.calendarWriter.WritePropertyValue(value);
		}

		// Token: 0x06004D7A RID: 19834 RVA: 0x00142A14 File Offset: 0x00140C14
		private static void DemoteXInstanceType(VEvent vevent)
		{
			bool valueOrDefault = vevent.GetValueOrDefault<bool>(InternalSchema.AppointmentRecurring);
			bool valueOrDefault2 = vevent.GetValueOrDefault<bool>(InternalSchema.IsRecurring);
			bool valueOrDefault3 = vevent.GetValueOrDefault<bool>(InternalSchema.IsException);
			CdoInstanceType value = CdoInstanceType.Single;
			if (valueOrDefault)
			{
				value = CdoInstanceType.Master;
			}
			else if (valueOrDefault2)
			{
				if (valueOrDefault3)
				{
					value = CdoInstanceType.Exception;
				}
				else
				{
					value = CdoInstanceType.Instance;
				}
			}
			vevent.calendarWriter.StartProperty("X-MICROSOFT-CDO-INSTTYPE");
			vevent.calendarWriter.WritePropertyValue((int)value);
		}

		// Token: 0x06004D7B RID: 19835 RVA: 0x00142A78 File Offset: 0x00140C78
		private static void DemoteXDisallowCounter(VEvent vevent)
		{
			bool valueOrDefault = vevent.GetValueOrDefault<bool>(InternalSchema.DisallowNewTimeProposal);
			vevent.calendarWriter.StartProperty("X-MICROSOFT-DISALLOW-COUNTER");
			vevent.calendarWriter.WritePropertyValue(valueOrDefault);
		}

		// Token: 0x06004D7C RID: 19836 RVA: 0x00142AB0 File Offset: 0x00140CB0
		private static void DemoteSubject(VEvent vevent)
		{
			string valueOrDefault = vevent.GetValueOrDefault<string>(InternalSchema.Subject, string.Empty);
			vevent.calendarWriter.StartProperty(PropertyId.Summary);
			if (vevent.itemLanguageName != null)
			{
				vevent.calendarWriter.StartParameter(ParameterId.Language);
				vevent.calendarWriter.WriteParameterValue(vevent.itemLanguageName);
			}
			vevent.calendarWriter.WritePropertyValue(valueOrDefault);
		}

		// Token: 0x06004D7D RID: 19837 RVA: 0x00142B10 File Offset: 0x00140D10
		private static void DemoteLocation(VEvent vevent)
		{
			string text = vevent.TryGetProperty(InternalSchema.Location) as string;
			if (text != null)
			{
				vevent.calendarWriter.StartProperty(PropertyId.Location);
				string text2 = vevent.TryGetProperty(InternalSchema.LocationURL) as string;
				if (text2 != null)
				{
					vevent.calendarWriter.StartParameter("ALTREP");
					vevent.calendarWriter.WriteParameterValue(CalendarUtil.RemoveDoubleQuotes(text2));
				}
				if (vevent.itemLanguageName != null)
				{
					vevent.calendarWriter.StartParameter(ParameterId.Language);
					vevent.calendarWriter.WriteParameterValue(vevent.itemLanguageName);
				}
				vevent.calendarWriter.WritePropertyValue(text);
			}
		}

		// Token: 0x06004D7E RID: 19838 RVA: 0x00142BA8 File Offset: 0x00140DA8
		private static void DemoteContact(VEvent vevent)
		{
			string text = vevent.TryGetProperty(InternalSchema.Contact) as string;
			if (text != null)
			{
				vevent.calendarWriter.StartProperty(PropertyId.Contact);
				string text2 = vevent.TryGetProperty(InternalSchema.ContactURL) as string;
				if (text2 != null)
				{
					vevent.calendarWriter.StartParameter("ALTREP");
					vevent.calendarWriter.WriteParameterValue(CalendarUtil.RemoveDoubleQuotes(text2));
				}
				vevent.calendarWriter.WritePropertyValue(text);
			}
		}

		// Token: 0x06004D7F RID: 19839 RVA: 0x00142C17 File Offset: 0x00140E17
		internal VEvent(CalendarComponentBase root) : base(root)
		{
		}

		// Token: 0x1700160E RID: 5646
		// (get) Token: 0x06004D80 RID: 19840 RVA: 0x00142C35 File Offset: 0x00140E35
		internal bool HasRecurrenceId
		{
			get
			{
				return this.hasRecurrenceId;
			}
		}

		// Token: 0x1700160F RID: 5647
		// (get) Token: 0x06004D81 RID: 19841 RVA: 0x00142C3D File Offset: 0x00140E3D
		// (set) Token: 0x06004D82 RID: 19842 RVA: 0x00142C45 File Offset: 0x00140E45
		internal bool HasExchangeIntendedStatus { get; set; }

		// Token: 0x17001610 RID: 5648
		// (get) Token: 0x06004D83 RID: 19843 RVA: 0x00142C4E File Offset: 0x00140E4E
		// (set) Token: 0x06004D84 RID: 19844 RVA: 0x00142C56 File Offset: 0x00140E56
		internal bool BusyStatusPromotedBasedOnXBusyStatus { get; set; }

		// Token: 0x17001611 RID: 5649
		// (get) Token: 0x06004D85 RID: 19845 RVA: 0x00142C5F File Offset: 0x00140E5F
		private bool IsOccurrence
		{
			get
			{
				return this.recurrenceId != null;
			}
		}

		// Token: 0x17001612 RID: 5650
		// (get) Token: 0x06004D86 RID: 19846 RVA: 0x00142C6D File Offset: 0x00140E6D
		private ExDateTime RecurrenceId
		{
			get
			{
				if (this.recurrenceId == null)
				{
					return ExDateTime.MinValue;
				}
				return (ExDateTime)this.recurrenceId.Value;
			}
		}

		// Token: 0x17001613 RID: 5651
		// (get) Token: 0x06004D87 RID: 19847 RVA: 0x00142C90 File Offset: 0x00140E90
		private CalendarAttendee RespondingAttendee
		{
			get
			{
				Participant participant = this.addressCache.Participants[ConversionItemParticipants.ParticipantIndex.From];
				if (participant != null)
				{
					using (List<CalendarAttendee>.Enumerator enumerator = this.AttendeeList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							CalendarAttendee calendarAttendee = enumerator.Current;
							if (StringComparer.OrdinalIgnoreCase.Equals(calendarAttendee.Address, participant.EmailAddress))
							{
								return calendarAttendee;
							}
						}
						goto IL_81;
					}
				}
				if (this.AttendeeList.Count > 0)
				{
					return this.AttendeeList[0];
				}
				IL_81:
				return null;
			}
		}

		// Token: 0x17001614 RID: 5652
		// (get) Token: 0x06004D88 RID: 19848 RVA: 0x00142D34 File Offset: 0x00140F34
		private bool IsResolvingFromAndSender
		{
			get
			{
				return base.InboundContext.AddressCache.IsResolvingAllParticipants;
			}
		}

		// Token: 0x17001615 RID: 5653
		// (get) Token: 0x06004D89 RID: 19849 RVA: 0x00142D46 File Offset: 0x00140F46
		private List<CalendarAttendee> AttendeeList
		{
			get
			{
				if (this.attendeeList == null)
				{
					this.attendeeList = new List<CalendarAttendee>();
				}
				return this.attendeeList;
			}
		}

		// Token: 0x17001616 RID: 5654
		// (get) Token: 0x06004D8A RID: 19850 RVA: 0x00142D64 File Offset: 0x00140F64
		internal bool DurationIsDayOrWeekValue
		{
			get
			{
				return this.duration.Value.Equals(new TimeSpan(this.duration.Value.Days, 0, 0, 0));
			}
		}

		// Token: 0x06004D8B RID: 19851 RVA: 0x00142DA0 File Offset: 0x00140FA0
		internal static CalendarType GetAlternateCalendarType(Item item)
		{
			if (item == null)
			{
				return CalendarType.Default;
			}
			InternalRecurrence recurrenceFromItem = CalendarItem.GetRecurrenceFromItem(item);
			return VEvent.GetAlternateCalendarType(recurrenceFromItem);
		}

		// Token: 0x06004D8C RID: 19852 RVA: 0x00142DC0 File Offset: 0x00140FC0
		internal static CalendarType GetAlternateCalendarType(InternalRecurrence recurrence)
		{
			if (recurrence == null)
			{
				return CalendarType.Default;
			}
			IMonthlyPatternInfo monthlyPatternInfo = recurrence.Pattern as IMonthlyPatternInfo;
			if (monthlyPatternInfo == null)
			{
				return CalendarType.Default;
			}
			if (!EnumValidator<CalendarType>.IsValidValue(monthlyPatternInfo.CalendarType))
			{
				throw new ConversionFailedException(ConversionFailureReason.CorruptContent, ServerStrings.ExCalendarTypeNotSupported(monthlyPatternInfo.CalendarType), null);
			}
			return monthlyPatternInfo.CalendarType;
		}

		// Token: 0x06004D8D RID: 19853 RVA: 0x00142E10 File Offset: 0x00141010
		internal bool Promote(Item item, List<VEvent> exceptions)
		{
			Util.ThrowOnNullArgument(item, "item");
			bool result = false;
			bool flag = true;
			this.item = item;
			this.addressCache = base.InboundContext.AddressCache;
			this.SetTimeZone(item.PropertyBag.ExTimeZone);
			try
			{
				this.SetDefaultProperties();
				result = (this.PromoteProperties() && this.PromoteAttendees(out flag) && this.PromoteMessageClass() && this.PromoteComplexProperties() && this.PromoteRecurrence(exceptions) && this.PromoteReminders());
				if (!flag)
				{
					base.SetProperty(InternalSchema.IsResponseRequested, false);
				}
			}
			catch (ArgumentException arg)
			{
				ExTraceGlobals.ICalTracer.TraceError<string, ArgumentException>((long)this.GetHashCode(), "VEvent::Promote. UID:'{0}'. Found exception:'{1}'", base.Uid, arg);
				base.Context.AddError(ServerStrings.InvalidICalElement(base.ComponentName));
			}
			catch (StoragePermanentException ex)
			{
				ExTraceGlobals.ICalTracer.TraceError<string, StoragePermanentException>((long)this.GetHashCode(), "VEvent::Promote. UID:'{0}'. Found exception:'{1}'", base.Uid, ex);
				base.Context.AddError(ex.LocalizedString);
			}
			return result;
		}

		// Token: 0x06004D8E RID: 19854 RVA: 0x00142F2C File Offset: 0x0014112C
		internal GlobalObjectId GetGlobalObjectId()
		{
			GlobalObjectId globalObjectId = null;
			if (!string.IsNullOrEmpty(this.uid))
			{
				try
				{
					globalObjectId = new GlobalObjectId(this.uid);
				}
				catch (CorruptDataException arg)
				{
					ExTraceGlobals.ICalTracer.TraceError<string, CorruptDataException>((long)this.GetHashCode(), "VEvent::GetGlobalObjectId. Failed to create GOID from uid string. UID:'{0}'. Found exception:'{1}'", this.uid, arg);
				}
			}
			globalObjectId = (globalObjectId ?? new GlobalObjectId());
			globalObjectId.Date = ((this.RecurrenceId == ExDateTime.MinValue) ? ExDateTime.MinValue : this.RecurrenceId.Date);
			return globalObjectId;
		}

		// Token: 0x06004D8F RID: 19855 RVA: 0x00142FC0 File Offset: 0x001411C0
		internal void Demote(Item item)
		{
			Util.ThrowOnNullArgument(item, "item");
			this.item = item;
			this.timeZone = item.PropertyBag.ExTimeZone;
			this.itemClassName = item.ClassName;
			this.calendarWriter = base.OutboundContext.Writer;
			this.addressCache = base.OutboundContext.AddressCache;
			int? valueAsNullable = base.GetValueAsNullable<int>(InternalSchema.MessageLocaleId);
			if (valueAsNullable != null)
			{
				Culture culture = null;
				if (Culture.TryGetCulture(valueAsNullable.Value, out culture))
				{
					this.itemLanguageName = culture.Name;
				}
			}
			this.calendarWriter.StartComponent(ComponentId.VEvent);
			this.DemoteOrganizer();
			if (base.Context.Method != CalendarMethod.Publish)
			{
				this.DemoteAttendees();
			}
			base.DemoteAttachments();
			if (base.Context.Method == CalendarMethod.Counter)
			{
				this.DemoteCounterProperties();
				base.DemoteBody(item.Body);
			}
			else
			{
				base.DemoteBody(item.Body);
				base.DemoteRecurrenceProperties(CalendarItem.GetRecurrenceFromItem(item));
				this.DemoteProperties();
				base.DemoteReminder();
				base.DemoteEmailReminders();
			}
			this.calendarWriter.EndComponent();
		}

		// Token: 0x06004D90 RID: 19856 RVA: 0x001430D8 File Offset: 0x001412D8
		internal void DemoteException(ExceptionInfo exceptionInfo, VEvent master)
		{
			this.calendarWriter = base.OutboundContext.Writer;
			exceptionInfo.PropertyBag.SetAllPropertiesLoaded();
			this.exceptionInfo = exceptionInfo;
			this.master = master;
			this.calendarWriter.StartComponent(ComponentId.VEvent);
			this.DemoteProperties();
			this.calendarWriter.EndComponent();
		}

		// Token: 0x06004D91 RID: 19857 RVA: 0x0014312C File Offset: 0x0014132C
		protected override bool ValidateProperties()
		{
			if (string.IsNullOrEmpty(this.uid))
			{
				ExTraceGlobals.ICalTracer.TraceError((long)this.GetHashCode(), "VEvent::ValidateProperties. No UID.");
				base.Context.AddError(ServerStrings.InvalidICalElement("UID"));
				return false;
			}
			if (!base.ValidateTimeZoneInfo(true))
			{
				return false;
			}
			if (base.Context.Method == CalendarMethod.Refresh)
			{
				return true;
			}
			if (base.Context.Method != CalendarMethod.Cancel && this.RecurrenceId == ExDateTime.MinValue)
			{
				if (this.hasRdate)
				{
					ExTraceGlobals.ICalTracer.TraceError<string>((long)this.GetHashCode(), "VEvent::ValidateProperties. Unsupported RDATE found. UID:'{0}'.", base.Uid);
					base.Context.AddError(ServerStrings.InvalidICalElement("RDATE"));
					return false;
				}
				if (this.hasExrule)
				{
					ExTraceGlobals.ICalTracer.TraceError<string>((long)this.GetHashCode(), "VEvent::ValidateProperties. Unsupported EXRULE found. UID:'{0}'.", base.Uid);
					base.Context.AddError(ServerStrings.InvalidICalElement("EXRULE"));
					return false;
				}
			}
			if (this.dtStart == null)
			{
				ExTraceGlobals.ICalTracer.TraceError<string>((long)this.GetHashCode(), "VEvent::ValidateProperties. DTSTART not found. UID:'{0}'.", base.Uid);
				base.Context.AddError(ServerStrings.InvalidICalElement("DTSTART"));
				return false;
			}
			if (this.dtEnd != null && (ExDateTime)this.dtEnd.Value < (ExDateTime)this.dtStart.Value)
			{
				ExTraceGlobals.ICalTracer.TraceError<string, object, object>((long)this.GetHashCode(), "VEvent::ValidateProperties. DTEND is less than DTSTART. UID:'{0}'. DTSTART:'{1}. DTEND:'{2}'.", base.Uid, this.dtStart.Value, this.dtEnd.Value);
				base.Context.AddError(ServerStrings.InvalidICalElement("DTEND"));
				return false;
			}
			if (this.dtStart.ValueType == CalendarValueType.Date)
			{
				this.isAllDayEvent = true;
				if (this.dtEnd != null && this.dtEnd.ValueType != CalendarValueType.Date)
				{
					ExTraceGlobals.ICalTracer.TraceError<string, object, object>((long)this.GetHashCode(), "VEvent::ValidateProperties. DTEND is not Date type while DTSTART is Date type. UID:'{0}'. DTSTART:'{1}. DTEND:'{2}'.", base.Uid, this.dtStart.Value, this.dtEnd.Value);
					base.Context.AddError(ServerStrings.InvalidICalElement("DTEND"));
					return false;
				}
				if (this.duration != null && !this.DurationIsDayOrWeekValue)
				{
					ExTraceGlobals.ICalTracer.TraceError<string, object, TimeSpan>((long)this.GetHashCode(), "VEvent::ValidateProperties. DURATION is not a 'dur-day' or 'dur-week' value while DTSTART is Date type. UID:'{0}'. DTSTART:'{1}. DURATION:'{2}'.", base.Uid, this.dtStart.Value, this.duration.Value);
					base.Context.AddError(ServerStrings.InvalidICalElement("DURATION"));
					return false;
				}
			}
			if (this.dtEnd == null)
			{
				this.dtEnd = new CalendarDateTime();
				ExDateTime exDateTime = (ExDateTime)this.dtStart.Value;
				if (this.duration != null)
				{
					this.dtEnd.TimeZoneId = this.dtStart.TimeZoneId;
					this.dtEnd.Value = exDateTime + this.duration.Value;
				}
				else
				{
					CalendarValueType valueType = this.dtStart.ValueType;
					if (valueType != CalendarValueType.Date)
					{
						if (valueType != CalendarValueType.DateTime)
						{
							ExTraceGlobals.ICalTracer.TraceError<string>((long)this.GetHashCode(), "VEvent::ValidateProperties. Both DTEND and DURATION not found. UID:'{0}'.", base.Uid);
							base.Context.AddError(ServerStrings.InvalidICalElement("DTEND"));
							return false;
						}
						this.dtEnd.Value = exDateTime;
					}
					else
					{
						this.dtEnd.Value = exDateTime.IncrementDays(1);
						if (this.busyStatus == BusyType.Unknown)
						{
							this.busyStatus = BusyType.Free;
						}
					}
				}
			}
			if (base.IcalRecurrence != null)
			{
				this.xsoRecurrence = base.XsoRecurrenceFromICalRecurrence(base.IcalRecurrence, (ExDateTime)this.dtStart.Value);
				if (this.xsoRecurrence == null)
				{
					return false;
				}
			}
			if (this.organizer != null)
			{
				this.AttendeeList.RemoveAll(new Predicate<CalendarAttendee>(this.MatchesOrganizer));
			}
			if (base.Context.Method == CalendarMethod.Reply && this.AttendeeList.Count == 0)
			{
				ExTraceGlobals.ICalTracer.TraceError<string>((long)this.GetHashCode(), "VEvent::ValidateProperties. No attendee found for reply. UID:'{0}'.", base.Uid);
				base.Context.AddError(ServerStrings.InvalidICalElement("ATTENDEE"));
				return false;
			}
			return true;
		}

		// Token: 0x06004D92 RID: 19858 RVA: 0x00143550 File Offset: 0x00141750
		protected override void ProcessProperty(CalendarPropertyBase calendarProperty)
		{
			base.ProcessProperty(calendarProperty);
			PropertyId propertyId = calendarProperty.CalendarPropertyId.PropertyId;
			if (propertyId != PropertyId.RecurrenceId)
			{
				return;
			}
			this.hasRecurrenceId = true;
		}

		// Token: 0x06004D93 RID: 19859 RVA: 0x00143580 File Offset: 0x00141780
		protected override bool ValidateProperty(CalendarPropertyBase calendarProperty)
		{
			bool flag = base.ValidateProperty(calendarProperty);
			PropertyId propertyId = calendarProperty.CalendarPropertyId.PropertyId;
			if (propertyId <= PropertyId.Location)
			{
				if (propertyId != PropertyId.Unknown)
				{
					switch (propertyId)
					{
					case PropertyId.Comment:
					case PropertyId.Description:
					case PropertyId.Location:
						break;
					case PropertyId.GeographicPosition:
						goto IL_1F5;
					default:
						goto IL_1F5;
					}
				}
				else
				{
					if (string.Compare(calendarProperty.CalendarPropertyId.PropertyName, "X-MICROSOFT-EXDATE", StringComparison.CurrentCultureIgnoreCase) == 0)
					{
						flag = this.ProcessExceptionDate(calendarProperty);
						goto IL_1F5;
					}
					if (string.Compare(calendarProperty.CalendarPropertyId.PropertyName, "X-MS-OLK-ORIGINALSTART", StringComparison.CurrentCultureIgnoreCase) == 0)
					{
						if (calendarProperty is CalendarDateTime)
						{
							this.originalCounterStartTime = (CalendarDateTime)calendarProperty;
							goto IL_1F5;
						}
						flag = false;
						goto IL_1F5;
					}
					else
					{
						if (string.Compare(calendarProperty.CalendarPropertyId.PropertyName, "X-MS-OLK-ORIGINALEND", StringComparison.CurrentCultureIgnoreCase) != 0)
						{
							goto IL_1F5;
						}
						if (calendarProperty is CalendarDateTime)
						{
							this.originalCounterEndTime = (CalendarDateTime)calendarProperty;
							goto IL_1F5;
						}
						flag = false;
						goto IL_1F5;
					}
				}
			}
			else
			{
				switch (propertyId)
				{
				case PropertyId.Summary:
					break;
				case PropertyId.Completed:
					goto IL_1F5;
				case PropertyId.DateTimeEnd:
					if (calendarProperty is CalendarDateTime)
					{
						this.dtEnd = (CalendarDateTime)calendarProperty;
						goto IL_1F5;
					}
					flag = false;
					goto IL_1F5;
				default:
					if (propertyId != PropertyId.Duration)
					{
						switch (propertyId)
						{
						case PropertyId.Attendee:
							flag = this.ProcessAttendee(calendarProperty);
							goto IL_1F5;
						case PropertyId.Contact:
						case PropertyId.RelatedTo:
						case PropertyId.Url:
						case PropertyId.Uid:
							goto IL_1F5;
						case PropertyId.Organizer:
							if (this.organizer == null)
							{
								this.organizer = (CalendarAttendee)calendarProperty;
								goto IL_1F5;
							}
							flag = false;
							goto IL_1F5;
						case PropertyId.RecurrenceId:
						{
							if (!(calendarProperty is CalendarDateTime))
							{
								flag = false;
								goto IL_1F5;
							}
							this.recurrenceId = (CalendarDateTime)calendarProperty;
							CalendarParameter parameter = calendarProperty.GetParameter(ParameterId.RecurrenceRange);
							if (parameter != null)
							{
								flag = false;
								goto IL_1F5;
							}
							goto IL_1F5;
						}
						case PropertyId.ExceptionDate:
							flag = this.ProcessExceptionDate(calendarProperty);
							goto IL_1F5;
						case PropertyId.ExceptionRule:
							this.hasExrule = true;
							goto IL_1F5;
						case PropertyId.RecurrenceDate:
							this.hasRdate = true;
							goto IL_1F5;
						default:
							goto IL_1F5;
						}
					}
					else
					{
						if (calendarProperty.Value is TimeSpan)
						{
							this.duration = new TimeSpan?((TimeSpan)calendarProperty.Value);
							goto IL_1F5;
						}
						flag = false;
						goto IL_1F5;
					}
					break;
				}
			}
			this.ProcessLanguage(calendarProperty);
			IL_1F5:
			if (!flag)
			{
				ExTraceGlobals.ICalTracer.TraceError<string, string>((long)this.GetHashCode(), "VEvent::ValidateProperty. Property validation failed. UID:'{0}'. Property:'{1}'", base.Uid, calendarProperty.CalendarPropertyId.PropertyName);
				base.Context.AddError(ServerStrings.InvalidICalElement(calendarProperty.CalendarPropertyId.PropertyName));
			}
			return flag;
		}

		// Token: 0x06004D94 RID: 19860 RVA: 0x001437C8 File Offset: 0x001419C8
		private void AppendAppointmentStateFlags(AppointmentStateFlags flags)
		{
			AppointmentStateFlags valueOrDefault = base.GetValueOrDefault<AppointmentStateFlags>(InternalSchema.AppointmentState, AppointmentStateFlags.None);
			AppointmentStateFlags appointmentStateFlags = valueOrDefault | flags;
			base.SetProperty(InternalSchema.AppointmentState, appointmentStateFlags);
		}

		// Token: 0x06004D95 RID: 19861 RVA: 0x001437F7 File Offset: 0x001419F7
		protected override PropertyBag GetPropertyBag()
		{
			if (this.item != null)
			{
				return this.item.PropertyBag;
			}
			if (this.exceptionInfo != null)
			{
				return this.exceptionInfo.PropertyBag;
			}
			throw new InvalidOperationException("VEvent.item and VEvent.exceptionInfo can't be both null");
		}

		// Token: 0x06004D96 RID: 19862 RVA: 0x0014382C File Offset: 0x00141A2C
		private void ProcessLanguage(CalendarPropertyBase calendarProperty)
		{
			if (calendarProperty.Parameters.Count <= 0)
			{
				return;
			}
			int propertyArgumentOrder = this.GetPropertyArgumentOrder(calendarProperty.CalendarPropertyId.PropertyId);
			int propertyArgumentOrder2 = this.GetPropertyArgumentOrder(this.languageSource);
			if (propertyArgumentOrder > propertyArgumentOrder2)
			{
				foreach (CalendarParameter calendarParameter in calendarProperty.Parameters)
				{
					if (calendarParameter.ParameterId == ParameterId.Language)
					{
						string text = calendarParameter.Value as string;
						Culture culture = null;
						if (text != null && text.Length > 0 && Culture.TryGetCulture(text, out culture))
						{
							this.languageSource = calendarProperty.CalendarPropertyId.PropertyId;
							this.itemLanguageName = text;
							break;
						}
						break;
					}
				}
			}
		}

		// Token: 0x06004D97 RID: 19863 RVA: 0x001438F8 File Offset: 0x00141AF8
		private int GetPropertyArgumentOrder(PropertyId property)
		{
			switch (property)
			{
			case PropertyId.Comment:
				return 3;
			case PropertyId.Description:
				return 4;
			case PropertyId.GeographicPosition:
				break;
			case PropertyId.Location:
				return 2;
			default:
				if (property == PropertyId.Summary)
				{
					return 1;
				}
				break;
			}
			return 0;
		}

		// Token: 0x06004D98 RID: 19864 RVA: 0x00143930 File Offset: 0x00141B30
		private bool ProcessExceptionDate(CalendarPropertyBase calendarProperty)
		{
			CalendarDateTime calendarDateTime = calendarProperty as CalendarDateTime;
			if (calendarDateTime != null)
			{
				if (this.deletedOccurrences == null)
				{
					this.deletedOccurrences = calendarDateTime;
				}
				else
				{
					if (!(this.deletedOccurrences.TimeZoneId == calendarDateTime.TimeZoneId))
					{
						ExTraceGlobals.ICalTracer.TraceError<CalendarDateTime>((long)this.GetHashCode(), "VEvent::ProcessExceptionDate. EXDATE is not same time zone id with preceding ones. Not supported. Property:'{0}'.", calendarDateTime);
						return false;
					}
					((List<object>)this.deletedOccurrences.Value).AddRange((List<object>)calendarDateTime.Value);
				}
				return true;
			}
			ExTraceGlobals.ICalTracer.TraceError((long)this.GetHashCode(), "VEvent::ProcessExceptionDate. EXDATE value is not expected type. Property:'{0}'.", new object[]
			{
				calendarProperty ?? string.Empty
			});
			return false;
		}

		// Token: 0x06004D99 RID: 19865 RVA: 0x001439DC File Offset: 0x00141BDC
		private bool ProcessAttendee(CalendarPropertyBase calendarProperty)
		{
			bool result = true;
			this.AttendeeList.Add((CalendarAttendee)calendarProperty);
			return result;
		}

		// Token: 0x06004D9A RID: 19866 RVA: 0x001439FD File Offset: 0x00141BFD
		private void SetDefaultProperties()
		{
			base.SetProperty(InternalSchema.AppointmentClass, "IPM.Appointment");
			if (base.Context.Method == CalendarMethod.Request)
			{
				base.SetProperty(InternalSchema.IsResponseRequested, true);
			}
		}

		// Token: 0x06004D9B RID: 19867 RVA: 0x00143A30 File Offset: 0x00141C30
		private bool PromoteReminders()
		{
			if (base.Context.Method == CalendarMethod.Request || base.Context.Method == CalendarMethod.Publish)
			{
				ExDateTime valueOrDefault = base.GetValueOrDefault<ExDateTime>(InternalSchema.MapiStartTime, ExDateTime.MinValue);
				ExDateTime valueOrDefault2 = base.GetValueOrDefault<ExDateTime>(InternalSchema.MapiEndTime, ExDateTime.MinValue);
				bool flag = false;
				if (this.item != null && this.item.Session != null && this.item.Session.IsOlcMoveDestination)
				{
					InternalRecurrence recurrenceFromItem = CalendarItem.GetRecurrenceFromItem(this.item);
					ExDateTime exDateTime;
					if (recurrenceFromItem != null)
					{
						exDateTime = recurrenceFromItem.EndDate + recurrenceFromItem.EndOffset;
					}
					else
					{
						exDateTime = valueOrDefault2;
					}
					flag = (exDateTime.CompareTo(ExDateTime.UtcNow) < 0);
				}
				if (this.displayVAlarm != null && this.displayVAlarm.Validate() && !flag)
				{
					int minutesBeforeStart = VAlarm.CalculateReminderMinutesBeforeStart(this.displayVAlarm, valueOrDefault, valueOrDefault2);
					base.SetProperty(InternalSchema.ReminderIsSetInternal, true);
					base.SetProperty(InternalSchema.ReminderMinutesBeforeStart, Reminder.NormalizeMinutesBeforeStart(minutesBeforeStart, 15));
					base.SetProperty(InternalSchema.ReminderDueBy, valueOrDefault);
				}
				else
				{
					base.SetProperty(InternalSchema.ReminderMinutesBeforeStart, 0);
					base.SetProperty(InternalSchema.ReminderIsSetInternal, false);
				}
				if (!flag)
				{
					VAlarm.PromoteEmailReminders(this.item, this.emailVAlarms, valueOrDefault, valueOrDefault2, this.IsOccurrence);
				}
			}
			return true;
		}

		// Token: 0x06004D9C RID: 19868 RVA: 0x00143B84 File Offset: 0x00141D84
		private bool PromoteMessageClass()
		{
			if (this.item == null)
			{
				return true;
			}
			string text = CalendarUtil.ItemClassFromMethod(base.Context.Method);
			if (CalendarUtil.IsReplyOrCounter(base.Context.Method))
			{
				string status = "NEEDS-ACTION";
				if (this.RespondingAttendee != null)
				{
					status = this.RespondingAttendee.ParticipationStatus;
				}
				text = CalendarUtil.ItemClassFromParticipationStatus(status);
				base.SetProperty(CalendarItemBaseSchema.ResponseType, CalendarUtil.ResponseTypeFromParticipationStatus(status));
			}
			if (string.IsNullOrEmpty(text))
			{
				ExTraceGlobals.ICalTracer.TraceError<string>((long)this.GetHashCode(), "VEvent::PromoteMessageClass. Message class can not be determined from iCal. UID:'{0}'.", base.Uid);
				base.Context.AddError(ServerStrings.InvalidICalElement("METHOD"));
				return false;
			}
			this.item.ClassName = text;
			return true;
		}

		// Token: 0x06004D9D RID: 19869 RVA: 0x00143C40 File Offset: 0x00141E40
		private bool PromoteProperties()
		{
			this.HandleFloatingTime();
			if (base.Context.Method == CalendarMethod.Counter)
			{
				base.SetProperty(InternalSchema.AppointmentCounterProposal, true);
				base.SetProperty(InternalSchema.AppointmentCounterStartWhole, this.dtStart.Value);
				base.SetProperty(InternalSchema.AppointmentCounterEndWhole, this.dtEnd.Value);
				this.SetStartTime(this.originalCounterStartTime);
				this.SetEndTime(this.originalCounterEndTime);
			}
			else
			{
				this.SetStartTime(this.dtStart);
				this.SetEndTime(this.dtEnd);
			}
			string valueOrDefault = base.GetValueOrDefault<string>(InternalSchema.AppointmentClass);
			if (ObjectClass.IsCalendarItemCalendarItemOccurrenceOrRecurrenceException(valueOrDefault))
			{
				base.SetProperty(InternalSchema.IsAllDayEvent, this.isAllDayEvent);
			}
			CalendarMethod method = base.Context.Method;
			foreach (CalendarPropertyBase calendarPropertyBase in base.ICalProperties)
			{
				SchemaInfo schemaInfo;
				if (VEvent.conversionSchema.TryGetValue(calendarPropertyBase.CalendarPropertyId.Key, out schemaInfo) && schemaInfo.PromotionMethod != null)
				{
					if ((method & schemaInfo.Methods) != method)
					{
						continue;
					}
					object promotionMethod = schemaInfo.PromotionMethod;
					PromotePropertyDelegate promotePropertyDelegate = promotionMethod as PromotePropertyDelegate;
					try
					{
						if (promotePropertyDelegate != null)
						{
							if (!promotePropertyDelegate(this, calendarPropertyBase))
							{
								string propertyName = calendarPropertyBase.CalendarPropertyId.PropertyName;
								ExTraceGlobals.ICalTracer.TraceError<string, string>((long)this.GetHashCode(), "VEvent::PromoteProperties. Failed to promote property. UID:'{0}'. Property:'{1}'.", base.Uid, propertyName);
								base.Context.AddError(ServerStrings.InvalidICalElement(propertyName));
								return false;
							}
						}
						else
						{
							PropertyDefinition propertyDefinition = (PropertyDefinition)promotionMethod;
							base.SetProperty(propertyDefinition, calendarPropertyBase.Value);
						}
						continue;
					}
					catch (ArgumentException)
					{
						if (calendarPropertyBase.ValueType == CalendarValueType.DateTime || calendarPropertyBase.ValueType == CalendarValueType.Date)
						{
							string propertyName2 = calendarPropertyBase.CalendarPropertyId.PropertyName;
							ExTraceGlobals.ICalTracer.TraceError<string, string>((long)this.GetHashCode(), "VEvent::PromoteProperties. Failed to promote data time property. UID:'{0}'. Property:'{1}'.", base.Uid, propertyName2);
							base.Context.AddError(ServerStrings.InvalidICalElement(propertyName2));
							return false;
						}
						throw;
					}
				}
				ExTraceGlobals.ICalTracer.TraceDebug<CalendarPropertyId>((long)this.GetHashCode(), "VEvent::PromoteProperties. There is no method to promote property: {0}", calendarPropertyBase.CalendarPropertyId);
			}
			return true;
		}

		// Token: 0x06004D9E RID: 19870 RVA: 0x00143EA0 File Offset: 0x001420A0
		private void HandleFloatingTime()
		{
			if (this.dtStart != null)
			{
				ExDateTime exDateTime = (ExDateTime)this.dtStart.Value;
				if (!exDateTime.HasTimeZone)
				{
					this.dtStart.Value = this.timeZone.Assign(exDateTime);
				}
			}
			if (this.dtEnd != null)
			{
				ExDateTime exDateTime2 = (ExDateTime)this.dtEnd.Value;
				if (!exDateTime2.HasTimeZone)
				{
					this.dtEnd.Value = this.timeZone.Assign(exDateTime2);
				}
			}
		}

		// Token: 0x06004D9F RID: 19871 RVA: 0x00143F2C File Offset: 0x0014212C
		private bool PromoteAttendees(out bool isResponseRequested)
		{
			isResponseRequested = true;
			if (CalendarUtil.IsReplyOrCounter(base.Context.Method))
			{
				return this.PromoteRespondingAttendeeToFrom();
			}
			base.InboundContext.AddressCache.ClearRecipients();
			bool flag = false;
			foreach (CalendarAttendee calendarAttendee in this.AttendeeList)
			{
				AttendeeType attendeeType;
				if (!this.PromoteAttendee(calendarAttendee, out attendeeType))
				{
					return false;
				}
				if (calendarAttendee.IsResponseRequested && attendeeType != AttendeeType.Resource)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				isResponseRequested = false;
			}
			return true;
		}

		// Token: 0x06004DA0 RID: 19872 RVA: 0x00143FD0 File Offset: 0x001421D0
		private bool PromoteRespondingAttendeeToFrom()
		{
			Participant participant = this.addressCache.Participants[ConversionItemParticipants.ParticipantIndex.From];
			CalendarAttendee respondingAttendee = this.RespondingAttendee;
			if (participant == null && respondingAttendee != null)
			{
				participant = InboundMimeHeadersParser.CreateParticipantFromMime(respondingAttendee.Name, respondingAttendee.Address, base.InboundContext.Options, base.InboundContext.Options.IsSenderTrusted);
				this.addressCache.Participants[ConversionItemParticipants.ParticipantIndex.From] = participant;
			}
			Participant participant2 = this.addressCache.Participants[ConversionItemParticipants.ParticipantIndex.Sender];
			if (participant2 == null && respondingAttendee != null)
			{
				participant2 = (string.IsNullOrEmpty(respondingAttendee.SentBy) ? participant : InboundMimeHeadersParser.CreateParticipantFromMime(null, respondingAttendee.SentBy, base.InboundContext.Options, base.InboundContext.Options.IsSenderTrusted));
				base.InboundContext.AddressCache.Participants[ConversionItemParticipants.ParticipantIndex.Sender] = participant2;
			}
			return !(participant == null) && !(participant2 == null);
		}

		// Token: 0x06004DA1 RID: 19873 RVA: 0x001440C4 File Offset: 0x001422C4
		private bool PromoteAttendee(CalendarAttendee attendee, out AttendeeType attendeeType)
		{
			Participant participant = InboundMimeHeadersParser.CreateParticipantFromMime(attendee.Name, attendee.Address, base.InboundContext.Options, true);
			if (participant == null)
			{
				attendeeType = AttendeeType.Required;
				return false;
			}
			if (string.Compare(attendee.ParticipationRole, "CHAIR", StringComparison.CurrentCultureIgnoreCase) == 0 || string.Compare(attendee.ParticipationRole, "REQ-PARTICIPANT", StringComparison.CurrentCultureIgnoreCase) == 0)
			{
				attendeeType = AttendeeType.Required;
			}
			else if (string.Compare(attendee.ParticipationRole, "OPT-PARTICIPANT", StringComparison.CurrentCultureIgnoreCase) == 0)
			{
				attendeeType = AttendeeType.Optional;
			}
			else if (string.Compare(attendee.CalendarUserType, "RESOURCE", StringComparison.CurrentCultureIgnoreCase) == 0 || string.Compare(attendee.CalendarUserType, "ROOM", StringComparison.CurrentCultureIgnoreCase) == 0)
			{
				attendeeType = AttendeeType.Resource;
			}
			else if (string.Compare(attendee.ParticipationRole, "NON-PARTICIPANT") == 0)
			{
				attendeeType = AttendeeType.Optional;
			}
			else
			{
				attendeeType = AttendeeType.Required;
			}
			RecipientItemType recipientItemType = Attendee.AttendeeTypeToRecipientItemType(attendeeType);
			ConversionRecipientEntry conversionRecipientEntry = new ConversionRecipientEntry(participant, recipientItemType);
			conversionRecipientEntry.SetProperty(InternalSchema.RecipientTrackStatus, CalendarUtil.ResponseTypeFromParticipationStatus(attendee.ParticipationStatus), true);
			base.InboundContext.AddressCache.AddRecipient(conversionRecipientEntry);
			return true;
		}

		// Token: 0x06004DA2 RID: 19874 RVA: 0x001441C4 File Offset: 0x001423C4
		protected override bool PromoteComplexProperties()
		{
			ExTimeZone exTimeZone = ExTimeZone.UnspecifiedTimeZone;
			ICalendarIcalConversionSettings calendarIcalConversionSettings = CalendarUtil.GetCalendarIcalConversionSettings();
			if (calendarIcalConversionSettings.LocalTimeZoneReferenceForRecurrenceNeeded)
			{
				exTimeZone = ExTimeZone.UtcTimeZone;
			}
			if (this.timeZone != exTimeZone && this.xsoRecurrence != null)
			{
				base.SetProperty(InternalSchema.TimeZone, this.timeZone.DisplayName);
				base.SetProperty(InternalSchema.TimeZoneBlob, O11TimeZoneFormatter.GetTimeZoneBlob(this.timeZone));
			}
			GlobalObjectId globalObjectId = this.GetGlobalObjectId();
			base.SetProperty(InternalSchema.CleanGlobalObjectId, globalObjectId.CleanGlobalObjectIdBytes);
			base.SetProperty(InternalSchema.GlobalObjectId, globalObjectId.Bytes);
			if (this.RecurrenceId != this.RecurrenceId.Date)
			{
				base.SetProperty(InternalSchema.StartRecurDate, Util.ConvertDateTimeToSCDDate(this.RecurrenceId.Date));
				base.SetProperty(InternalSchema.StartRecurTime, Util.ConvertTimeSpanToSCDTime(this.RecurrenceId.TimeOfDay));
			}
			if (CalendarMethod.Cancel == base.Context.Method)
			{
				this.AppendAppointmentStateFlags(AppointmentStateFlags.Meeting | AppointmentStateFlags.Received | AppointmentStateFlags.Cancelled);
			}
			else if (base.Context.Method != CalendarMethod.Publish)
			{
				this.AppendAppointmentStateFlags(AppointmentStateFlags.Meeting | AppointmentStateFlags.Received);
			}
			else
			{
				this.AppendAppointmentStateFlags((this.AttendeeList.Count == 0) ? AppointmentStateFlags.None : AppointmentStateFlags.Meeting);
			}
			if (this.sequence >= 0)
			{
				base.SetProperty(InternalSchema.AppointmentSequenceNumber, this.sequence);
				if (!this.IsOccurrence)
				{
					base.SetProperty(InternalSchema.AppointmentLastSequenceNumber, this.sequence);
				}
			}
			if (base.Context.Method == CalendarMethod.Request)
			{
				if (!this.HasExchangeIntendedStatus)
				{
					base.SetProperty(InternalSchema.IntendedFreeBusyStatus, (this.busyStatus == BusyType.Unknown) ? BusyType.Busy : this.busyStatus);
				}
				this.busyStatus = BusyType.Tentative;
			}
			base.SetProperty(InternalSchema.FreeBusyStatus, (this.busyStatus == BusyType.Unknown) ? BusyType.Tentative : this.busyStatus);
			if (this.itemLanguageName != null)
			{
				Culture culture = null;
				if (Culture.TryGetCulture(this.itemLanguageName, out culture))
				{
					base.SetProperty(InternalSchema.MessageLocaleId, culture.LCID);
				}
			}
			this.PromoteInstanceType();
			base.PromoteComplexProperties();
			return true;
		}

		// Token: 0x06004DA3 RID: 19875 RVA: 0x001443D8 File Offset: 0x001425D8
		private void PromoteInstanceType()
		{
			bool flag;
			bool flag2;
			bool flag3;
			switch (this.instanceType)
			{
			case 0:
				flag = false;
				flag2 = false;
				flag3 = false;
				goto IL_75;
			case 1:
				flag = false;
				flag2 = true;
				flag3 = true;
				goto IL_75;
			case 2:
				flag = false;
				flag2 = true;
				flag3 = false;
				goto IL_75;
			case 3:
				flag = true;
				flag2 = true;
				flag3 = false;
				goto IL_75;
			}
			if (this.RecurrenceId != ExDateTime.MinValue)
			{
				flag = true;
				flag2 = true;
				flag3 = false;
			}
			else if (base.IcalRecurrence != null)
			{
				flag = false;
				flag2 = true;
				flag3 = true;
			}
			else
			{
				flag = false;
				flag2 = false;
				flag3 = false;
			}
			IL_75:
			base.SetProperty(InternalSchema.IsException, flag);
			base.SetProperty(InternalSchema.IsRecurring, flag2);
			base.SetProperty(InternalSchema.AppointmentRecurring, flag3);
		}

		// Token: 0x06004DA4 RID: 19876 RVA: 0x00144490 File Offset: 0x00142690
		private bool PromoteRecurrence(List<VEvent> exceptions)
		{
			if (this.xsoRecurrence == null)
			{
				return true;
			}
			ExDateTime dt = (ExDateTime)this.dtStart.Value;
			ExDateTime dt2 = (ExDateTime)this.dtEnd.Value;
			InternalRecurrence internalRecurrence = new InternalRecurrence(this.xsoRecurrence.Pattern, this.xsoRecurrence.Range, null, this.xsoRecurrence.CreatedExTimeZone, this.xsoRecurrence.ReadExTimeZone, dt - dt.Date, dt2 - dt.Date);
			if (this.deletedOccurrences != null && this.deletedOccurrences.Value is List<object>)
			{
				foreach (object obj in ((List<object>)this.deletedOccurrences.Value))
				{
					ExDateTime exDateTime = (ExDateTime)obj;
					ExDateTime exDateTime2 = new ExDateTime(this.xsoRecurrence.CreatedExTimeZone, exDateTime.UniversalTime);
					internalRecurrence.TryDeleteOccurrence(exDateTime2.Date);
				}
			}
			if (this.PromoteExceptions(exceptions, internalRecurrence))
			{
				byte[] propertyValue = internalRecurrence.ToByteArray();
				base.SetProperty(InternalSchema.AppointmentRecurrenceBlob, propertyValue);
				base.SetProperty(InternalSchema.ClipStartTime, internalRecurrence.Range.StartDate);
				base.SetProperty(InternalSchema.ClipEndTime, internalRecurrence.EndDate);
				return true;
			}
			return false;
		}

		// Token: 0x06004DA5 RID: 19877 RVA: 0x001445FC File Offset: 0x001427FC
		private bool PromoteExceptions(List<VEvent> exceptions, InternalRecurrence recurrence)
		{
			if (exceptions == null || exceptions.Count == 0)
			{
				return true;
			}
			foreach (VEvent vevent in exceptions)
			{
				if (StringComparer.OrdinalIgnoreCase.Equals(vevent.Uid, base.Uid) && vevent.Validate())
				{
					vevent.SetTimeZone(this.item.PropertyBag.ExTimeZone);
					if (!vevent.PromoteException(recurrence))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06004DA6 RID: 19878 RVA: 0x00144698 File Offset: 0x00142898
		private bool PromoteException(InternalRecurrence recurrence)
		{
			MemoryPropertyBag memoryPropertyBag = new MemoryPropertyBag();
			memoryPropertyBag.SetAllPropertiesLoaded();
			this.exceptionInfo = new ExceptionInfo(null, this.RecurrenceId.Date, this.RecurrenceId, (ExDateTime)this.dtStart.Value, (ExDateTime)this.dtEnd.Value, (ModificationType)0, memoryPropertyBag);
			this.PromoteProperties();
			recurrence.ModifyOccurrence(this.exceptionInfo);
			return true;
		}

		// Token: 0x06004DA7 RID: 19879 RVA: 0x00144708 File Offset: 0x00142908
		private void DemoteCounterProperties()
		{
			ExDateTime? valueAsNullable = base.GetValueAsNullable<ExDateTime>(InternalSchema.AppointmentCounterStartWhole);
			ExDateTime? valueAsNullable2 = base.GetValueAsNullable<ExDateTime>(InternalSchema.AppointmentCounterEndWhole);
			if (valueAsNullable == null || valueAsNullable2 == null)
			{
				throw new ConversionFailedException(ConversionFailureReason.CorruptContent, null);
			}
			VEvent.DemoteDateTime(this, PropertyId.DateTimeStart, valueAsNullable.Value);
			VEvent.DemoteDateTime(this, PropertyId.DateTimeEnd, valueAsNullable2.Value);
			ExDateTime? valueAsNullable3 = base.GetValueAsNullable<ExDateTime>(InternalSchema.StartTime);
			ExDateTime? valueAsNullable4 = base.GetValueAsNullable<ExDateTime>(InternalSchema.EndTime);
			if (valueAsNullable3 != null && valueAsNullable4 != null)
			{
				VEvent.DemoteDateTime(this, "X-MS-OLK-ORIGINALSTART", valueAsNullable3.Value);
				VEvent.DemoteDateTime(this, "X-MS-OLK-ORIGINALEND", valueAsNullable4.Value);
			}
			VEvent.DemoteSubject(this);
			VEvent.DemoteLocation(this);
			VEvent.DemoteUid(this);
			VEvent.DemoteSequence(this);
			VEvent.DemoteDtStamp(this);
			VEvent.DemoteRecurrenceId(this);
		}

		// Token: 0x06004DA8 RID: 19880 RVA: 0x001447D8 File Offset: 0x001429D8
		private void DemoteProperties()
		{
			foreach (SchemaInfo schemaInfo in VEvent.conversionSchema.Values)
			{
				if (schemaInfo.DemotionMethod != null)
				{
					DemotePropertyDelegate demotePropertyDelegate = schemaInfo.DemotionMethod as DemotePropertyDelegate;
					if (demotePropertyDelegate != null)
					{
						demotePropertyDelegate(this);
					}
					else
					{
						base.DemoteSimpleProperty(schemaInfo);
					}
				}
			}
		}

		// Token: 0x06004DA9 RID: 19881 RVA: 0x00144850 File Offset: 0x00142A50
		private void DemoteOrganizer()
		{
			Participant participant = null;
			Participant participant2;
			if (CalendarUtil.IsReplyOrCounter(base.Context.Method))
			{
				participant2 = (this.addressCache.Participants[ConversionItemParticipants.ParticipantIndex.ReceivedRepresenting] ?? this.addressCache.Participants[ConversionItemParticipants.ParticipantIndex.ReceivedBy]);
			}
			else
			{
				participant2 = this.addressCache.Participants[ConversionItemParticipants.ParticipantIndex.From];
				participant = this.addressCache.Participants[ConversionItemParticipants.ParticipantIndex.Sender];
				if (participant2 == null)
				{
					participant2 = participant;
					participant = null;
				}
			}
			if (participant2 != null)
			{
				string participantSmtpAddress = ItemToMimeConverter.GetParticipantSmtpAddress(participant2, base.OutboundContext.Options);
				this.calendarWriter.StartProperty(PropertyId.Organizer);
				this.WriteCommonName(participant2.DisplayName);
				if (participant != null && !Participant.HasSameEmail(participant2, participant, this.item.Session as MailboxSession, true))
				{
					string participantSmtpAddress2 = ItemToMimeConverter.GetParticipantSmtpAddress(participant, base.OutboundContext.Options);
					this.calendarWriter.WriteParameter(ParameterId.SentBy, CalendarUtil.AddMailToPrefix(CalendarUtil.RemoveDoubleQuotes(participantSmtpAddress2)));
				}
				this.calendarWriter.WritePropertyValue(CalendarUtil.AddMailToPrefix(participantSmtpAddress));
				return;
			}
			ExTraceGlobals.ICalTracer.TraceDebug((long)this.GetHashCode(), "VEvent::DemoteOrganizer. Organizer is missing.");
		}

		// Token: 0x06004DAA RID: 19882 RVA: 0x0014497C File Offset: 0x00142B7C
		private void DemoteAttendees()
		{
			bool flag = false;
			if (CalendarUtil.IsReplyOrCounter(base.Context.Method))
			{
				this.DemoteRespondingAttendee();
				return;
			}
			Participant participant = this.addressCache.Participants[ConversionItemParticipants.ParticipantIndex.ReceivedRepresenting];
			Participant participant2 = this.addressCache.Participants[ConversionItemParticipants.ParticipantIndex.ReceivedBy];
			MailboxSession session = this.item.Session as MailboxSession;
			if (participant2 != null && participant != null && !Participant.HasSameEmail(participant2, participant, session, true))
			{
				flag = true;
			}
			OutboundAddressCache outboundAddressCache = (OutboundAddressCache)this.addressCache;
			ConversionRecipientList recipients = outboundAddressCache.Recipients;
			foreach (ConversionRecipientEntry conversionRecipientEntry in recipients)
			{
				if (flag && Participant.HasSameEmail(participant2, conversionRecipientEntry.Participant, session, true))
				{
					this.DemoteAttendee(participant);
					flag = false;
				}
				else
				{
					this.DemoteAttendee(conversionRecipientEntry);
				}
			}
		}

		// Token: 0x06004DAB RID: 19883 RVA: 0x00144A74 File Offset: 0x00142C74
		private void DemoteRespondingAttendee()
		{
			Participant participant = this.addressCache.Participants[ConversionItemParticipants.ParticipantIndex.From];
			Participant participant2 = this.addressCache.Participants[ConversionItemParticipants.ParticipantIndex.Sender];
			if (participant == null)
			{
				if (participant2 == null)
				{
					return;
				}
				participant = participant2;
				participant2 = null;
			}
			string value = CalendarUtil.ParticipationStatusFromItemClass(this.itemClassName);
			this.calendarWriter.StartProperty(PropertyId.Attendee);
			if (participant2 != null && !Participant.HasSameEmail(participant, participant2, this.item.Session as MailboxSession, true))
			{
				string participantSmtpAddress = ItemToMimeConverter.GetParticipantSmtpAddress(participant2, base.OutboundContext.Options);
				this.calendarWriter.WriteParameter(ParameterId.SentBy, CalendarUtil.AddMailToPrefix(CalendarUtil.RemoveDoubleQuotes(participantSmtpAddress)));
			}
			string participantSmtpAddress2 = ItemToMimeConverter.GetParticipantSmtpAddress(participant, base.OutboundContext.Options);
			this.calendarWriter.WriteParameter(ParameterId.ParticipationStatus, value);
			this.WriteCommonName(participant.DisplayName);
			this.calendarWriter.WritePropertyValue(CalendarUtil.AddMailToPrefix(participantSmtpAddress2));
		}

		// Token: 0x06004DAC RID: 19884 RVA: 0x00144B68 File Offset: 0x00142D68
		private void DemoteAttendee(Participant participant)
		{
			string value;
			if (CalendarUtil.IsReplyOrCounter(base.Context.Method))
			{
				value = CalendarUtil.ParticipationStatusFromItemClass(this.itemClassName);
			}
			else
			{
				value = "NEEDS-ACTION";
			}
			string value2 = base.GetValueOrDefault<bool>(InternalSchema.IsResponseRequested) ? "TRUE" : "FALSE";
			string participantSmtpAddress = ItemToMimeConverter.GetParticipantSmtpAddress(participant, base.OutboundContext.Options);
			this.calendarWriter.StartProperty(PropertyId.Attendee);
			this.calendarWriter.WriteParameter(ParameterId.ParticipationStatus, value);
			this.calendarWriter.WriteParameter(ParameterId.RsvpExpectation, value2);
			this.WriteCommonName(participant.DisplayName);
			this.calendarWriter.WritePropertyValue(CalendarUtil.AddMailToPrefix(participantSmtpAddress));
		}

		// Token: 0x06004DAD RID: 19885 RVA: 0x00144C18 File Offset: 0x00142E18
		private void DemoteAttendee(ConversionRecipientEntry recipient)
		{
			RecipientItemType recipientItemType = recipient.RecipientItemType;
			AttendeeType attendeeType = Attendee.RecipientItemTypeToAttendeeType(recipientItemType);
			Participant participant = recipient.Participant;
			string text;
			string text2;
			switch (attendeeType)
			{
			case AttendeeType.Optional:
				text = "OPT-PARTICIPANT";
				text2 = string.Empty;
				goto IL_89;
			case AttendeeType.Resource:
				if (participant.GetValueOrDefault<bool>(ParticipantSchema.IsRoom, false))
				{
					text = string.Empty;
					text2 = "ROOM";
					goto IL_89;
				}
				if (participant.GetValueOrDefault<bool>(ParticipantSchema.IsResource, false))
				{
					text = string.Empty;
					text2 = "RESOURCE";
					goto IL_89;
				}
				return;
			}
			text = "REQ-PARTICIPANT";
			text2 = string.Empty;
			IL_89:
			string value;
			if (CalendarUtil.IsReplyOrCounter(base.Context.Method))
			{
				value = CalendarUtil.ParticipationStatusFromItemClass(this.itemClassName);
			}
			else
			{
				value = "NEEDS-ACTION";
			}
			string value2 = this.item.GetValueOrDefault<bool>(InternalSchema.IsResponseRequested) ? "TRUE" : "FALSE";
			string participantSmtpAddress = ItemToMimeConverter.GetParticipantSmtpAddress(participant, base.OutboundContext.Options);
			this.calendarWriter.StartProperty(PropertyId.Attendee);
			if (string.Empty != text2)
			{
				this.calendarWriter.WriteParameter(ParameterId.CalendarUserType, text2);
			}
			if (string.Empty != text)
			{
				this.calendarWriter.WriteParameter(ParameterId.ParticipationRole, text);
			}
			this.calendarWriter.WriteParameter(ParameterId.ParticipationStatus, value);
			this.calendarWriter.WriteParameter(ParameterId.RsvpExpectation, value2);
			this.WriteCommonName(participant.DisplayName);
			this.calendarWriter.WritePropertyValue(CalendarUtil.AddMailToPrefix(participantSmtpAddress));
		}

		// Token: 0x06004DAE RID: 19886 RVA: 0x00144D91 File Offset: 0x00142F91
		private bool MatchesOrganizer(CalendarAttendee attendee)
		{
			return (attendee == null && this.organizer == null) || (attendee != null && this.organizer != null && string.Compare(this.organizer.Address, attendee.Address, StringComparison.CurrentCultureIgnoreCase) == 0);
		}

		// Token: 0x06004DAF RID: 19887 RVA: 0x00144DC9 File Offset: 0x00142FC9
		private void SetStartTime(CalendarDateTime propertyValue)
		{
			this.SetCalendarDateTimeProperty(InternalSchema.MapiStartTime, InternalSchema.TimeZoneDefinitionStart, propertyValue);
		}

		// Token: 0x06004DB0 RID: 19888 RVA: 0x00144DDC File Offset: 0x00142FDC
		private void SetEndTime(CalendarDateTime propertyValue)
		{
			this.SetCalendarDateTimeProperty(InternalSchema.MapiEndTime, InternalSchema.TimeZoneDefinitionEnd, propertyValue);
		}

		// Token: 0x06004DB1 RID: 19889 RVA: 0x00144DF0 File Offset: 0x00142FF0
		private void SetCalendarDateTimeProperty(PropertyDefinition timePropertyDefinition, PropertyDefinition timeZonePropertyDefinition, CalendarDateTime propertyValue)
		{
			if (propertyValue != null && !CalendarItemBase.OutlookRtmNone.Equals(propertyValue.Value))
			{
				base.SetProperty(timePropertyDefinition, propertyValue.Value);
				if (propertyValue.Value is ExDateTime)
				{
					ExTimeZone timeZone = ((ExDateTime)propertyValue.Value).TimeZone;
					if (timeZone != ExTimeZone.UnspecifiedTimeZone && timeZone != ExTimeZone.UtcTimeZone)
					{
						base.SetProperty(timeZonePropertyDefinition, O12TimeZoneFormatter.GetTimeZoneBlob(timeZone));
					}
				}
			}
		}

		// Token: 0x06004DB2 RID: 19890 RVA: 0x00144E63 File Offset: 0x00143063
		private void WriteCommonName(string displayName)
		{
			if (!base.OutboundContext.Options.SuppressDisplayName)
			{
				this.calendarWriter.WriteParameter(ParameterId.CommonName, CalendarUtil.RemoveDoubleQuotes(displayName));
			}
		}

		// Token: 0x04002A16 RID: 10774
		private static Dictionary<object, SchemaInfo> conversionSchema;

		// Token: 0x04002A17 RID: 10775
		private List<CalendarAttendee> attendeeList;

		// Token: 0x04002A18 RID: 10776
		private CalendarAttendee organizer;

		// Token: 0x04002A19 RID: 10777
		private CalendarDateTime dtEnd;

		// Token: 0x04002A1A RID: 10778
		private TimeSpan? duration;

		// Token: 0x04002A1B RID: 10779
		private CalendarDateTime recurrenceId;

		// Token: 0x04002A1C RID: 10780
		private int instanceType = -1;

		// Token: 0x04002A1D RID: 10781
		private int sequence = -1;

		// Token: 0x04002A1E RID: 10782
		private BusyType busyStatus = BusyType.Unknown;

		// Token: 0x04002A1F RID: 10783
		private CalendarDateTime deletedOccurrences;

		// Token: 0x04002A20 RID: 10784
		private bool hasRdate;

		// Token: 0x04002A21 RID: 10785
		private bool hasExrule;

		// Token: 0x04002A22 RID: 10786
		private bool hasRecurrenceId;

		// Token: 0x04002A23 RID: 10787
		private CalendarDateTime originalCounterStartTime;

		// Token: 0x04002A24 RID: 10788
		private CalendarDateTime originalCounterEndTime;

		// Token: 0x04002A25 RID: 10789
		private PropertyId languageSource;

		// Token: 0x04002A26 RID: 10790
		private bool isAllDayEvent;

		// Token: 0x04002A27 RID: 10791
		private VEvent master;

		// Token: 0x04002A28 RID: 10792
		private ExceptionInfo exceptionInfo;

		// Token: 0x04002A29 RID: 10793
		private ConversionAddressCache addressCache;

		// Token: 0x04002A2A RID: 10794
		private string itemClassName;
	}
}
