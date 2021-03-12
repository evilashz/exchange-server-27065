using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Entities.Calendaring
{
	// Token: 0x02000002 RID: 2
	internal static class CalendaringStrings
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		static CalendaringStrings()
		{
			CalendaringStrings.stringIDs.Add(3467777808U, "ErrorOrganizerCantRespond");
			CalendaringStrings.stringIDs.Add(3656881890U, "ErrorForwardNotSupportedForNprAppointment");
			CalendaringStrings.stringIDs.Add(1984582116U, "ErrorResponseNotRequested");
			CalendaringStrings.stringIDs.Add(1952627901U, "ErrorMissingRequiredRespondParameter");
			CalendaringStrings.stringIDs.Add(2515296808U, "ErrorAllDayTimeZoneMismatch");
			CalendaringStrings.stringIDs.Add(3570207334U, "ClientIdAlreadyInUse");
			CalendaringStrings.stringIDs.Add(1428267663U, "ErrorAllDayTimesMustBeMidnight");
			CalendaringStrings.stringIDs.Add(1919329599U, "InvalidCalendarGroupName");
			CalendaringStrings.stringIDs.Add(2859986641U, "ErrorCallerCantChangeSeriesMasterId");
			CalendaringStrings.stringIDs.Add(4235503886U, "CalendarNameCannotBeEmpty");
			CalendaringStrings.stringIDs.Add(2207773804U, "UpdateEventParametersAndAttendeesCantBeSpecified");
			CalendaringStrings.stringIDs.Add(3442398301U, "ErrorProposedNewTimeNotSupportedForNpr");
			CalendaringStrings.stringIDs.Add(86332937U, "CannotDeleteDefaultCalendar");
			CalendaringStrings.stringIDs.Add(419720883U, "ErrorInvalidAttendee");
			CalendaringStrings.stringIDs.Add(1369588146U, "ErrorCallerCantSpecifyClientId");
			CalendaringStrings.stringIDs.Add(2766594189U, "MandatoryParameterClientIdNotSpecified");
			CalendaringStrings.stringIDs.Add(1170527623U, "CalendarGroupEntryUpdateFailed");
			CalendaringStrings.stringIDs.Add(3192618457U, "ErrorCantExpandSingleItem");
			CalendaringStrings.stringIDs.Add(3858227364U, "CalendarFolderUpdateFailed");
			CalendaringStrings.stringIDs.Add(1206102081U, "ErrorRespondToCancelledEvent");
			CalendaringStrings.stringIDs.Add(3539194640U, "ErrorInvalidIdentifier");
			CalendaringStrings.stringIDs.Add(3452208988U, "ErrorCallerCantSpecifySeriesId");
			CalendaringStrings.stringIDs.Add(2770247355U, "ErrorCallerMustSpecifySeriesId");
			CalendaringStrings.stringIDs.Add(2133888287U, "ErrorNotAuthorizedToCancel");
			CalendaringStrings.stringIDs.Add(3220621404U, "InvalidNewReminderSettingId");
			CalendaringStrings.stringIDs.Add(1567702372U, "ErrorNeedToSendMessagesWhenCriticalPropertiesAreChanging");
			CalendaringStrings.stringIDs.Add(2957997666U, "ErrorCallerCantSpecifySeriesMasterId");
			CalendaringStrings.stringIDs.Add(4279355164U, "ErrorMeetingMessageNotFoundOrCantBeUsed");
			CalendaringStrings.stringIDs.Add(661617243U, "ErrorCorruptedSeriesData");
			CalendaringStrings.stringIDs.Add(494393448U, "NullPopupReminderSettings");
			CalendaringStrings.stringIDs.Add(2267819751U, "ErrorOccurrencesListRequired");
			CalendaringStrings.stringIDs.Add(2237189786U, "CannotRenameDefaultCalendar");
			CalendaringStrings.stringIDs.Add(609639808U, "InvalidReminderSettingId");
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000023A0 File Offset: 0x000005A0
		public static LocalizedString ErrorOrganizerCantRespond
		{
			get
			{
				return new LocalizedString("ErrorOrganizerCantRespond", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000023B7 File Offset: 0x000005B7
		public static LocalizedString ErrorForwardNotSupportedForNprAppointment
		{
			get
			{
				return new LocalizedString("ErrorForwardNotSupportedForNprAppointment", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000023CE File Offset: 0x000005CE
		public static LocalizedString ErrorResponseNotRequested
		{
			get
			{
				return new LocalizedString("ErrorResponseNotRequested", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000023E8 File Offset: 0x000005E8
		public static LocalizedString CalendarGroupIsNotEmpty(StoreId groupId, Guid groupClassId, string groupName, int calendarsCount)
		{
			return new LocalizedString("CalendarGroupIsNotEmpty", CalendaringStrings.ResourceManager, new object[]
			{
				groupId,
				groupClassId,
				groupName,
				calendarsCount
			});
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002426 File Offset: 0x00000626
		public static LocalizedString ErrorMissingRequiredRespondParameter
		{
			get
			{
				return new LocalizedString("ErrorMissingRequiredRespondParameter", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000243D File Offset: 0x0000063D
		public static LocalizedString ErrorAllDayTimeZoneMismatch
		{
			get
			{
				return new LocalizedString("ErrorAllDayTimeZoneMismatch", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002454 File Offset: 0x00000654
		public static LocalizedString ClientIdAlreadyInUse
		{
			get
			{
				return new LocalizedString("ClientIdAlreadyInUse", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000246B File Offset: 0x0000066B
		public static LocalizedString ErrorAllDayTimesMustBeMidnight
		{
			get
			{
				return new LocalizedString("ErrorAllDayTimesMustBeMidnight", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002482 File Offset: 0x00000682
		public static LocalizedString InvalidCalendarGroupName
		{
			get
			{
				return new LocalizedString("InvalidCalendarGroupName", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002499 File Offset: 0x00000699
		public static LocalizedString ErrorCallerCantChangeSeriesMasterId
		{
			get
			{
				return new LocalizedString("ErrorCallerCantChangeSeriesMasterId", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000024B0 File Offset: 0x000006B0
		public static LocalizedString InvalidPopupReminderSettingsCount(int count)
		{
			return new LocalizedString("InvalidPopupReminderSettingsCount", CalendaringStrings.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000024DD File Offset: 0x000006DD
		public static LocalizedString CalendarNameCannotBeEmpty
		{
			get
			{
				return new LocalizedString("CalendarNameCannotBeEmpty", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000024F4 File Offset: 0x000006F4
		public static LocalizedString UpdateEventParametersAndAttendeesCantBeSpecified
		{
			get
			{
				return new LocalizedString("UpdateEventParametersAndAttendeesCantBeSpecified", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000250C File Offset: 0x0000070C
		public static LocalizedString SeriesNotFound(string seriesId)
		{
			return new LocalizedString("SeriesNotFound", CalendaringStrings.ResourceManager, new object[]
			{
				seriesId
			});
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002534 File Offset: 0x00000734
		public static LocalizedString UnableToFindUser(ADOperationErrorCode operationErrorCode)
		{
			return new LocalizedString("UnableToFindUser", CalendaringStrings.ResourceManager, new object[]
			{
				operationErrorCode
			});
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002561 File Offset: 0x00000761
		public static LocalizedString ErrorProposedNewTimeNotSupportedForNpr
		{
			get
			{
				return new LocalizedString("ErrorProposedNewTimeNotSupportedForNpr", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002578 File Offset: 0x00000778
		public static LocalizedString CannotDeleteDefaultCalendar
		{
			get
			{
				return new LocalizedString("CannotDeleteDefaultCalendar", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002590 File Offset: 0x00000790
		public static LocalizedString CalendarNameAlreadyInUse(string name)
		{
			return new LocalizedString("CalendarNameAlreadyInUse", CalendaringStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000025B8 File Offset: 0x000007B8
		public static LocalizedString ErrorInvalidAttendee
		{
			get
			{
				return new LocalizedString("ErrorInvalidAttendee", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000025CF File Offset: 0x000007CF
		public static LocalizedString ErrorCallerCantSpecifyClientId
		{
			get
			{
				return new LocalizedString("ErrorCallerCantSpecifyClientId", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000025E6 File Offset: 0x000007E6
		public static LocalizedString MandatoryParameterClientIdNotSpecified
		{
			get
			{
				return new LocalizedString("MandatoryParameterClientIdNotSpecified", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000025FD File Offset: 0x000007FD
		public static LocalizedString CalendarGroupEntryUpdateFailed
		{
			get
			{
				return new LocalizedString("CalendarGroupEntryUpdateFailed", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002614 File Offset: 0x00000814
		public static LocalizedString ErrorCantExpandSingleItem
		{
			get
			{
				return new LocalizedString("ErrorCantExpandSingleItem", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000262B File Offset: 0x0000082B
		public static LocalizedString CalendarFolderUpdateFailed
		{
			get
			{
				return new LocalizedString("CalendarFolderUpdateFailed", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002642 File Offset: 0x00000842
		public static LocalizedString ErrorRespondToCancelledEvent
		{
			get
			{
				return new LocalizedString("ErrorRespondToCancelledEvent", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002659 File Offset: 0x00000859
		public static LocalizedString ErrorInvalidIdentifier
		{
			get
			{
				return new LocalizedString("ErrorInvalidIdentifier", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002670 File Offset: 0x00000870
		public static LocalizedString CannotDeleteWellKnownCalendarGroup(StoreId groupId, Guid groupClassId, string groupName)
		{
			return new LocalizedString("CannotDeleteWellKnownCalendarGroup", CalendaringStrings.ResourceManager, new object[]
			{
				groupId,
				groupClassId,
				groupName
			});
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000026A8 File Offset: 0x000008A8
		public static LocalizedString FolderNotFound(string folderType)
		{
			return new LocalizedString("FolderNotFound", CalendaringStrings.ResourceManager, new object[]
			{
				folderType
			});
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000026D0 File Offset: 0x000008D0
		public static LocalizedString ErrorCallerCantSpecifySeriesId
		{
			get
			{
				return new LocalizedString("ErrorCallerCantSpecifySeriesId", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000026E7 File Offset: 0x000008E7
		public static LocalizedString ErrorCallerMustSpecifySeriesId
		{
			get
			{
				return new LocalizedString("ErrorCallerMustSpecifySeriesId", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000026FE File Offset: 0x000008FE
		public static LocalizedString ErrorNotAuthorizedToCancel
		{
			get
			{
				return new LocalizedString("ErrorNotAuthorizedToCancel", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002715 File Offset: 0x00000915
		public static LocalizedString InvalidNewReminderSettingId
		{
			get
			{
				return new LocalizedString("InvalidNewReminderSettingId", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000272C File Offset: 0x0000092C
		public static LocalizedString ErrorNeedToSendMessagesWhenCriticalPropertiesAreChanging
		{
			get
			{
				return new LocalizedString("ErrorNeedToSendMessagesWhenCriticalPropertiesAreChanging", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002743 File Offset: 0x00000943
		public static LocalizedString ErrorCallerCantSpecifySeriesMasterId
		{
			get
			{
				return new LocalizedString("ErrorCallerCantSpecifySeriesMasterId", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000275A File Offset: 0x0000095A
		public static LocalizedString ErrorMeetingMessageNotFoundOrCantBeUsed
		{
			get
			{
				return new LocalizedString("ErrorMeetingMessageNotFoundOrCantBeUsed", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002771 File Offset: 0x00000971
		public static LocalizedString ErrorCorruptedSeriesData
		{
			get
			{
				return new LocalizedString("ErrorCorruptedSeriesData", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002788 File Offset: 0x00000988
		public static LocalizedString NullPopupReminderSettings
		{
			get
			{
				return new LocalizedString("NullPopupReminderSettings", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000279F File Offset: 0x0000099F
		public static LocalizedString ErrorOccurrencesListRequired
		{
			get
			{
				return new LocalizedString("ErrorOccurrencesListRequired", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000027B6 File Offset: 0x000009B6
		public static LocalizedString CannotRenameDefaultCalendar
		{
			get
			{
				return new LocalizedString("CannotRenameDefaultCalendar", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000027CD File Offset: 0x000009CD
		public static LocalizedString InvalidReminderSettingId
		{
			get
			{
				return new LocalizedString("InvalidReminderSettingId", CalendaringStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000027E4 File Offset: 0x000009E4
		public static LocalizedString GetLocalizedString(CalendaringStrings.IDs key)
		{
			return new LocalizedString(CalendaringStrings.stringIDs[(uint)key], CalendaringStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04000001 RID: 1
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(33);

		// Token: 0x04000002 RID: 2
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Entities.Calendaring.CalendaringStrings", typeof(CalendaringStrings).GetTypeInfo().Assembly);

		// Token: 0x02000003 RID: 3
		public enum IDs : uint
		{
			// Token: 0x04000004 RID: 4
			ErrorOrganizerCantRespond = 3467777808U,
			// Token: 0x04000005 RID: 5
			ErrorForwardNotSupportedForNprAppointment = 3656881890U,
			// Token: 0x04000006 RID: 6
			ErrorResponseNotRequested = 1984582116U,
			// Token: 0x04000007 RID: 7
			ErrorMissingRequiredRespondParameter = 1952627901U,
			// Token: 0x04000008 RID: 8
			ErrorAllDayTimeZoneMismatch = 2515296808U,
			// Token: 0x04000009 RID: 9
			ClientIdAlreadyInUse = 3570207334U,
			// Token: 0x0400000A RID: 10
			ErrorAllDayTimesMustBeMidnight = 1428267663U,
			// Token: 0x0400000B RID: 11
			InvalidCalendarGroupName = 1919329599U,
			// Token: 0x0400000C RID: 12
			ErrorCallerCantChangeSeriesMasterId = 2859986641U,
			// Token: 0x0400000D RID: 13
			CalendarNameCannotBeEmpty = 4235503886U,
			// Token: 0x0400000E RID: 14
			UpdateEventParametersAndAttendeesCantBeSpecified = 2207773804U,
			// Token: 0x0400000F RID: 15
			ErrorProposedNewTimeNotSupportedForNpr = 3442398301U,
			// Token: 0x04000010 RID: 16
			CannotDeleteDefaultCalendar = 86332937U,
			// Token: 0x04000011 RID: 17
			ErrorInvalidAttendee = 419720883U,
			// Token: 0x04000012 RID: 18
			ErrorCallerCantSpecifyClientId = 1369588146U,
			// Token: 0x04000013 RID: 19
			MandatoryParameterClientIdNotSpecified = 2766594189U,
			// Token: 0x04000014 RID: 20
			CalendarGroupEntryUpdateFailed = 1170527623U,
			// Token: 0x04000015 RID: 21
			ErrorCantExpandSingleItem = 3192618457U,
			// Token: 0x04000016 RID: 22
			CalendarFolderUpdateFailed = 3858227364U,
			// Token: 0x04000017 RID: 23
			ErrorRespondToCancelledEvent = 1206102081U,
			// Token: 0x04000018 RID: 24
			ErrorInvalidIdentifier = 3539194640U,
			// Token: 0x04000019 RID: 25
			ErrorCallerCantSpecifySeriesId = 3452208988U,
			// Token: 0x0400001A RID: 26
			ErrorCallerMustSpecifySeriesId = 2770247355U,
			// Token: 0x0400001B RID: 27
			ErrorNotAuthorizedToCancel = 2133888287U,
			// Token: 0x0400001C RID: 28
			InvalidNewReminderSettingId = 3220621404U,
			// Token: 0x0400001D RID: 29
			ErrorNeedToSendMessagesWhenCriticalPropertiesAreChanging = 1567702372U,
			// Token: 0x0400001E RID: 30
			ErrorCallerCantSpecifySeriesMasterId = 2957997666U,
			// Token: 0x0400001F RID: 31
			ErrorMeetingMessageNotFoundOrCantBeUsed = 4279355164U,
			// Token: 0x04000020 RID: 32
			ErrorCorruptedSeriesData = 661617243U,
			// Token: 0x04000021 RID: 33
			NullPopupReminderSettings = 494393448U,
			// Token: 0x04000022 RID: 34
			ErrorOccurrencesListRequired = 2267819751U,
			// Token: 0x04000023 RID: 35
			CannotRenameDefaultCalendar = 2237189786U,
			// Token: 0x04000024 RID: 36
			InvalidReminderSettingId = 609639808U
		}

		// Token: 0x02000004 RID: 4
		private enum ParamIDs
		{
			// Token: 0x04000026 RID: 38
			CalendarGroupIsNotEmpty,
			// Token: 0x04000027 RID: 39
			InvalidPopupReminderSettingsCount,
			// Token: 0x04000028 RID: 40
			SeriesNotFound,
			// Token: 0x04000029 RID: 41
			UnableToFindUser,
			// Token: 0x0400002A RID: 42
			CalendarNameAlreadyInUse,
			// Token: 0x0400002B RID: 43
			CannotDeleteWellKnownCalendarGroup,
			// Token: 0x0400002C RID: 44
			FolderNotFound
		}
	}
}
