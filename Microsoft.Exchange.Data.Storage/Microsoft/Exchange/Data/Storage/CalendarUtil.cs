using System;
using System.Globalization;
using Microsoft.Exchange.Calendar;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200081B RID: 2075
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class CalendarUtil
	{
		// Token: 0x06004DCF RID: 19919 RVA: 0x0014554C File Offset: 0x0014374C
		internal static bool? BooleanFromString(string value)
		{
			if (string.Compare(value, "TRUE", StringComparison.CurrentCultureIgnoreCase) == 0)
			{
				return new bool?(true);
			}
			if (string.Compare(value, "FALSE", StringComparison.CurrentCultureIgnoreCase) == 0)
			{
				return new bool?(false);
			}
			return null;
		}

		// Token: 0x06004DD0 RID: 19920 RVA: 0x0014558C File Offset: 0x0014378C
		internal static string RemoveDoubleQuotes(string text)
		{
			return text.Replace("\"", string.Empty);
		}

		// Token: 0x06004DD1 RID: 19921 RVA: 0x0014559E File Offset: 0x0014379E
		internal static string AddMailToPrefix(string emailAddress)
		{
			if (emailAddress.ToUpperInvariant().StartsWith("MAILTO:"))
			{
				return emailAddress;
			}
			return "MAILTO:" + emailAddress;
		}

		// Token: 0x06004DD2 RID: 19922 RVA: 0x001455BF File Offset: 0x001437BF
		internal static string RemoveMailToPrefix(string emailAddress)
		{
			if (emailAddress.ToUpperInvariant().StartsWith("MAILTO:"))
			{
				return emailAddress.Substring("MAILTO:".Length);
			}
			return emailAddress;
		}

		// Token: 0x06004DD3 RID: 19923 RVA: 0x001455E5 File Offset: 0x001437E5
		internal static BusyType BusyTypeFromString(string busyStatusString)
		{
			return CalendarUtil.BusyTypeFromStringOrDefault(busyStatusString, BusyType.Busy);
		}

		// Token: 0x06004DD4 RID: 19924 RVA: 0x001455EE File Offset: 0x001437EE
		internal static BusyType BusyTypeFromStringOrDefault(string busyStatusString, BusyType defaultValue)
		{
			if (string.Compare(busyStatusString, "FREE", StringComparison.CurrentCultureIgnoreCase) == 0)
			{
				return BusyType.Free;
			}
			if (string.Compare(busyStatusString, "TENTATIVE", StringComparison.CurrentCultureIgnoreCase) == 0)
			{
				return BusyType.Tentative;
			}
			if (string.Compare(busyStatusString, "OOF", StringComparison.CurrentCultureIgnoreCase) == 0)
			{
				return BusyType.OOF;
			}
			return defaultValue;
		}

		// Token: 0x06004DD5 RID: 19925 RVA: 0x00145621 File Offset: 0x00143821
		internal static BusyType BusyTypeFromTranspStringOrDefault(string transpString, BusyType defaultValue)
		{
			if (string.Compare(transpString, "TRANSPARENT", StringComparison.CurrentCultureIgnoreCase) == 0)
			{
				return BusyType.Free;
			}
			if (string.Compare(transpString, "OPAQUE", StringComparison.CurrentCultureIgnoreCase) == 0)
			{
				return BusyType.Busy;
			}
			return defaultValue;
		}

		// Token: 0x06004DD6 RID: 19926 RVA: 0x00145644 File Offset: 0x00143844
		internal static string BusyTypeToString(BusyType busyType)
		{
			switch (busyType)
			{
			case BusyType.Free:
			case BusyType.WorkingElseWhere:
				return "FREE";
			case BusyType.Tentative:
				return "TENTATIVE";
			case BusyType.OOF:
				return "OOF";
			}
			return "BUSY";
		}

		// Token: 0x06004DD7 RID: 19927 RVA: 0x00145688 File Offset: 0x00143888
		internal static string ParticipationStatusFromItemClass(string itemClass)
		{
			string result = "NEEDS-ACTION";
			if (string.Compare(itemClass, "IPM.Schedule.Meeting.Resp.Pos", StringComparison.CurrentCultureIgnoreCase) == 0)
			{
				result = "ACCEPTED";
			}
			else if (string.Compare(itemClass, "IPM.Schedule.Meeting.Resp.Neg", StringComparison.CurrentCultureIgnoreCase) == 0)
			{
				result = "DECLINED";
			}
			else if (string.Compare(itemClass, "IPM.Schedule.Meeting.Resp.Tent", StringComparison.CurrentCultureIgnoreCase) == 0)
			{
				result = "TENTATIVE";
			}
			return result;
		}

		// Token: 0x06004DD8 RID: 19928 RVA: 0x001456DC File Offset: 0x001438DC
		internal static string ItemClassFromParticipationStatus(string status)
		{
			string result = "IPM.Schedule.Meeting.Resp.Tent";
			if (status != null)
			{
				if (string.Compare(status, "ACCEPTED", StringComparison.CurrentCultureIgnoreCase) == 0)
				{
					result = "IPM.Schedule.Meeting.Resp.Pos";
				}
				else if (string.Compare(status, "DECLINED", StringComparison.CurrentCultureIgnoreCase) == 0)
				{
					result = "IPM.Schedule.Meeting.Resp.Neg";
				}
				else if (string.Compare(status, "TENTATIVE", StringComparison.CurrentCultureIgnoreCase) == 0)
				{
					result = "IPM.Schedule.Meeting.Resp.Tent";
				}
			}
			return result;
		}

		// Token: 0x06004DD9 RID: 19929 RVA: 0x00145734 File Offset: 0x00143934
		internal static string ItemClassFromMethod(CalendarMethod method)
		{
			string result = null;
			if (method <= CalendarMethod.Cancel)
			{
				switch (method)
				{
				case CalendarMethod.Publish:
					result = "IPM.Appointment";
					break;
				case CalendarMethod.Request:
					result = "IPM.Schedule.Meeting.Request";
					break;
				case CalendarMethod.Publish | CalendarMethod.Request:
				case CalendarMethod.Reply:
					break;
				default:
					if (method == CalendarMethod.Cancel)
					{
						result = "IPM.Schedule.Meeting.Canceled";
					}
					break;
				}
			}
			else if (method != CalendarMethod.Refresh)
			{
				if (method != CalendarMethod.Counter)
				{
				}
			}
			else
			{
				result = "IPM.Note";
			}
			return result;
		}

		// Token: 0x06004DDA RID: 19930 RVA: 0x00145794 File Offset: 0x00143994
		internal static ResponseType ResponseTypeFromParticipationStatus(string status)
		{
			ResponseType result = ResponseType.None;
			if (status != null)
			{
				if (string.Compare(status, "ACCEPTED", StringComparison.CurrentCultureIgnoreCase) == 0)
				{
					result = ResponseType.Accept;
				}
				else if (string.Compare(status, "DECLINED", StringComparison.CurrentCultureIgnoreCase) == 0)
				{
					result = ResponseType.Decline;
				}
				else if (string.Compare(status, "TENTATIVE", StringComparison.CurrentCultureIgnoreCase) == 0)
				{
					result = ResponseType.Tentative;
				}
			}
			return result;
		}

		// Token: 0x06004DDB RID: 19931 RVA: 0x001457DC File Offset: 0x001439DC
		internal static string ParticipationStatusFromResponseType(ResponseType responseType)
		{
			string result = "NEEDS-ACTION";
			switch (responseType)
			{
			case ResponseType.Tentative:
				result = "TENTATIVE";
				break;
			case ResponseType.Accept:
				result = "ACCEPTED";
				break;
			case ResponseType.Decline:
				result = "DECLINED";
				break;
			}
			return result;
		}

		// Token: 0x06004DDC RID: 19932 RVA: 0x0014581E File Offset: 0x00143A1E
		internal static bool CanConvertToMeetingMessage(Item item)
		{
			return CalendarUtil.GetICalMethod(item) != CalendarMethod.None || ObjectClass.IsFailedInboundICal(item.ClassName);
		}

		// Token: 0x06004DDD RID: 19933 RVA: 0x00145838 File Offset: 0x00143A38
		internal static CalendarMethod GetICalMethod(Item item)
		{
			CalendarMethod result = CalendarMethod.None;
			string className = item.ClassName;
			if (ObjectClass.IsMeetingRequest(className))
			{
				result = CalendarMethod.Request;
			}
			else if (ObjectClass.IsMeetingCancellation(className))
			{
				result = CalendarMethod.Cancel;
			}
			else if (ObjectClass.IsMeetingResponse(className))
			{
				if (item.GetValueOrDefault<bool>(InternalSchema.AppointmentCounterProposal))
				{
					result = CalendarMethod.Counter;
				}
				else
				{
					result = CalendarMethod.Reply;
				}
			}
			else if (ObjectClass.IsCalendarItemCalendarItemOccurrenceOrRecurrenceException(className))
			{
				result = CalendarMethod.Publish;
			}
			return result;
		}

		// Token: 0x06004DDE RID: 19934 RVA: 0x00145890 File Offset: 0x00143A90
		internal static bool IsReplyOrCounter(CalendarMethod calendarMethod)
		{
			return calendarMethod == CalendarMethod.Reply || calendarMethod == CalendarMethod.Counter;
		}

		// Token: 0x06004DDF RID: 19935 RVA: 0x001458A0 File Offset: 0x00143AA0
		internal static string CalendarMethodToString(CalendarMethod calendarMethod)
		{
			switch (calendarMethod)
			{
			case CalendarMethod.Publish:
				return "PUBLISH";
			case CalendarMethod.Request:
				return "REQUEST";
			case CalendarMethod.Publish | CalendarMethod.Request:
				break;
			case CalendarMethod.Reply:
				return "REPLY";
			default:
				if (calendarMethod == CalendarMethod.Cancel)
				{
					return "CANCEL";
				}
				if (calendarMethod == CalendarMethod.Counter)
				{
					return "COUNTER";
				}
				break;
			}
			throw new ArgumentException("Not supported method: " + calendarMethod.ToString());
		}

		// Token: 0x06004DE0 RID: 19936 RVA: 0x00145917 File Offset: 0x00143B17
		internal static string CalendarTypeToString(CalendarType calendarType)
		{
			EnumValidator<CalendarType>.ThrowIfInvalid(calendarType, "calendarType");
			return calendarType.ToString().ToUpper();
		}

		// Token: 0x06004DE1 RID: 19937 RVA: 0x00145934 File Offset: 0x00143B34
		internal static CalendarType? CalendarTypeFromString(string value)
		{
			CalendarType value2;
			if (EnumValidator<CalendarType>.TryParse(value, EnumParseOptions.IgnoreCase, out value2))
			{
				return new CalendarType?(value2);
			}
			return null;
		}

		// Token: 0x06004DE2 RID: 19938 RVA: 0x0014595C File Offset: 0x00143B5C
		internal static string GetSubjectFromFreeBusyStatus(BusyType busyType, CultureInfo culture)
		{
			switch (busyType)
			{
			case BusyType.Free:
				return ClientStrings.Free.ToString(culture);
			case BusyType.Tentative:
				return ClientStrings.Tentative.ToString(culture);
			case BusyType.Busy:
				return ClientStrings.Busy.ToString(culture);
			case BusyType.OOF:
				return ClientStrings.OOF.ToString(culture);
			default:
				ExTraceGlobals.ICalTracer.TraceDebug<BusyType>(0L, "CalendarUtil::GetSubjectFromFreeBusy. Found invalid BusyType '{0}'.", busyType);
				return ClientStrings.Tentative.ToString(culture);
			}
		}

		// Token: 0x06004DE3 RID: 19939 RVA: 0x001459E4 File Offset: 0x00143BE4
		internal static ICalendarIcalConversionSettings GetCalendarIcalConversionSettings()
		{
			VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null);
			return snapshot.DataStorage.CalendarIcalConversionSettings;
		}

		// Token: 0x04002A39 RID: 10809
		internal const string UpperCaseMailToPrefix = "MAILTO:";

		// Token: 0x04002A3A RID: 10810
		internal static string NotSupportedInboundIcal = "not supported calendar message.ics";

		// Token: 0x0200081C RID: 2076
		internal static class StandardICalTokens
		{
			// Token: 0x04002A3B RID: 10811
			internal const string ICalRule = "RRULE";

			// Token: 0x04002A3C RID: 10812
			internal const string ICalExDate = "EXDATE";

			// Token: 0x04002A3D RID: 10813
			internal const string ICalRecur = "RECUR";

			// Token: 0x04002A3E RID: 10814
			internal const string ICalDateTime = "DATE-TIME";

			// Token: 0x04002A3F RID: 10815
			internal const string AltRep = "ALTREP";

			// Token: 0x04002A40 RID: 10816
			internal const string ByDayList = "BYDAYLIST";
		}

		// Token: 0x0200081D RID: 2077
		internal static class XICalProperties
		{
			// Token: 0x04002A41 RID: 10817
			internal const string ExchangeApptId = "X-MICROSOFT-CDO-OWNERAPPTID";

			// Token: 0x04002A42 RID: 10818
			internal const string ExchangeBusyStatus = "X-MICROSOFT-CDO-BUSYSTATUS";

			// Token: 0x04002A43 RID: 10819
			internal const string ExchangeCharmId = "X-MICROSOFT-CHARMID";

			// Token: 0x04002A44 RID: 10820
			internal const string ExchangeIntendedStatus = "X-MICROSOFT-CDO-INTENDEDSTATUS";

			// Token: 0x04002A45 RID: 10821
			internal const string ExchangeIsOrganizer = "X-MICROSOFT-ISORGANIZER";

			// Token: 0x04002A46 RID: 10822
			internal const string ExchangeAllDayEvent = "X-MICROSOFT-CDO-ALLDAYEVENT";

			// Token: 0x04002A47 RID: 10823
			internal const string ExchangeImportance = "X-MICROSOFT-CDO-IMPORTANCE";

			// Token: 0x04002A48 RID: 10824
			internal const string ExchangeInstanceType = "X-MICROSOFT-CDO-INSTTYPE";

			// Token: 0x04002A49 RID: 10825
			internal const string ExchangeApptSequence = "X-MICROSOFT-CDO-APPT-SEQUENCE";

			// Token: 0x04002A4A RID: 10826
			internal const string ExchangeCalScale = "X-MICROSOFT-CALSCALE";

			// Token: 0x04002A4B RID: 10827
			internal const string ExchangeRule = "X-MICROSOFT-RRULE";

			// Token: 0x04002A4C RID: 10828
			internal const string ExchangeExDate = "X-MICROSOFT-EXDATE";

			// Token: 0x04002A4D RID: 10829
			internal const string ExchangeIsLeap = "X-MICROSOFT-ISLEAPMONTH";

			// Token: 0x04002A4E RID: 10830
			internal const string ExchangeDisallowCounter = "X-MICROSOFT-DISALLOW-COUNTER";

			// Token: 0x04002A4F RID: 10831
			internal const string XAltDesc = "X-ALT-DESC";

			// Token: 0x04002A50 RID: 10832
			internal const string XMsOlkOriginalStart = "X-MS-OLK-ORIGINALSTART";

			// Token: 0x04002A51 RID: 10833
			internal const string XMsOlkOriginalEnd = "X-MS-OLK-ORIGINALEND";

			// Token: 0x04002A52 RID: 10834
			internal const string XCalendarName = "X-WR-CALNAME";
		}

		// Token: 0x0200081E RID: 2078
		internal static class ICalClass
		{
			// Token: 0x04002A53 RID: 10835
			internal const string ClassPublic = "PUBLIC";

			// Token: 0x04002A54 RID: 10836
			internal const string ClassPrivate = "PRIVATE";

			// Token: 0x04002A55 RID: 10837
			internal const string ClassPersonal = "PERSONAL";

			// Token: 0x04002A56 RID: 10838
			internal const string ClassConfidential = "CONFIDENTIAL";
		}

		// Token: 0x0200081F RID: 2079
		internal static class ICalStatus
		{
			// Token: 0x04002A57 RID: 10839
			internal const string StatusTentative = "TENTATIVE";

			// Token: 0x04002A58 RID: 10840
			internal const string StatusConfirmed = "CONFIRMED";

			// Token: 0x04002A59 RID: 10841
			internal const string StatusCancelled = "CANCELLED";
		}

		// Token: 0x02000820 RID: 2080
		internal static class ICalBusyType
		{
			// Token: 0x04002A5A RID: 10842
			internal const string BusyTypeFree = "FREE";

			// Token: 0x04002A5B RID: 10843
			internal const string BusyTypeTentative = "TENTATIVE";

			// Token: 0x04002A5C RID: 10844
			internal const string BusyTypeOof = "OOF";

			// Token: 0x04002A5D RID: 10845
			internal const string BusyTypeBusy = "BUSY";
		}

		// Token: 0x02000821 RID: 2081
		internal static class ICalTransp
		{
			// Token: 0x04002A5E RID: 10846
			internal const string TranspTransparent = "TRANSPARENT";

			// Token: 0x04002A5F RID: 10847
			internal const string TranspOpaque = "OPAQUE";
		}

		// Token: 0x02000822 RID: 2082
		internal static class ICalMethod
		{
			// Token: 0x04002A60 RID: 10848
			internal const string MethodPublish = "PUBLISH";

			// Token: 0x04002A61 RID: 10849
			internal const string MethodRequest = "REQUEST";

			// Token: 0x04002A62 RID: 10850
			internal const string MethodReply = "REPLY";

			// Token: 0x04002A63 RID: 10851
			internal const string MethodCancel = "CANCEL";

			// Token: 0x04002A64 RID: 10852
			internal const string MethodRefresh = "REFRESH";

			// Token: 0x04002A65 RID: 10853
			internal const string MethodCounter = "COUNTER";
		}

		// Token: 0x02000823 RID: 2083
		internal static class ICalParticipationStatus
		{
			// Token: 0x04002A66 RID: 10854
			internal const string PartstatNeedsAction = "NEEDS-ACTION";

			// Token: 0x04002A67 RID: 10855
			internal const string PartstatAccepted = "ACCEPTED";

			// Token: 0x04002A68 RID: 10856
			internal const string PartstatDeclined = "DECLINED";

			// Token: 0x04002A69 RID: 10857
			internal const string PartstatTentative = "TENTATIVE";

			// Token: 0x04002A6A RID: 10858
			internal const string PartstatDelegated = "DELEGATED";
		}

		// Token: 0x02000824 RID: 2084
		internal static class ICalParticipationRole
		{
			// Token: 0x04002A6B RID: 10859
			internal const string PartRoleChair = "CHAIR";

			// Token: 0x04002A6C RID: 10860
			internal const string PartRoleRequired = "REQ-PARTICIPANT";

			// Token: 0x04002A6D RID: 10861
			internal const string PartRoleOptional = "OPT-PARTICIPANT";

			// Token: 0x04002A6E RID: 10862
			internal const string PartRoleNonParticipant = "NON-PARTICIPANT";
		}

		// Token: 0x02000825 RID: 2085
		internal static class ICalCalendarUserType
		{
			// Token: 0x04002A6F RID: 10863
			internal const string CalendarUserTypeResource = "RESOURCE";

			// Token: 0x04002A70 RID: 10864
			internal const string CalendarUserTypeRoom = "ROOM";
		}

		// Token: 0x02000826 RID: 2086
		internal static class ICalTaskStatusType
		{
			// Token: 0x04002A71 RID: 10865
			internal const string TaskStatusTypeCompleted = "COMPLETED";

			// Token: 0x04002A72 RID: 10866
			internal const string TaskStatusTypeNeedsAction = "NEEDS-ACTION";

			// Token: 0x04002A73 RID: 10867
			internal const string TaskStatusTypeInProcess = "IN-PROCESS";

			// Token: 0x04002A74 RID: 10868
			internal const string TaskStatusTypeCancelled = "CANCELLED";
		}
	}
}
