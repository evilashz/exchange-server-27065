using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.AssistantsClientResources
{
	// Token: 0x02000002 RID: 2
	public static class Strings
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		static Strings()
		{
			Strings.stringIDs.Add(394366253U, "descRecurringNotAllowed");
			Strings.stringIDs.Add(1148026803U, "descSingleBooked");
			Strings.stringIDs.Add(566335833U, "descDeclined");
			Strings.stringIDs.Add(95666746U, "descDelegateConflictList");
			Strings.stringIDs.Add(3021003899U, "descDelegateRecurring");
			Strings.stringIDs.Add(167053646U, "descAccepted");
			Strings.stringIDs.Add(1075460866U, "descCredit");
			Strings.stringIDs.Add(2827051240U, "descRoleNotAllowed");
			Strings.stringIDs.Add(736995773U, "descInThePast");
			Strings.stringIDs.Add(3503731055U, "RetainUntil");
			Strings.stringIDs.Add(3589522754U, "descRecurringBooked");
			Strings.stringIDs.Add(1579274145U, "descRecurringSomeAccepted");
			Strings.stringIDs.Add(3779075844U, "descTahomaBlackMediumFontTag");
			Strings.stringIDs.Add(3990825802U, "descSingleAccepted");
			Strings.stringIDs.Add(3328869470U, "descCorruptWorkingHours");
			Strings.stringIDs.Add(3716547363U, "SMSLowConfidenceTranscription");
			Strings.stringIDs.Add(2683960550U, "descAcknowledgeReceived");
			Strings.stringIDs.Add(2847829128U, "descOutOfPolicyDelegateMessage");
			Strings.stringIDs.Add(1043033118U, "descSingleConflicts");
			Strings.stringIDs.Add(2938636641U, "descRecurringConflicts");
			Strings.stringIDs.Add(4274878084U, "descTimeZoneInfo");
			Strings.stringIDs.Add(4066812006U, "descSingleBookedSomeAccepted");
			Strings.stringIDs.Add(445569887U, "descMeetingTimeLabel");
			Strings.stringIDs.Add(1687944893U, "descBody");
			Strings.stringIDs.Add(2293492535U, "descDelegateConflicts");
			Strings.stringIDs.Add(203356656U, "descDeclinedAll");
			Strings.stringIDs.Add(3332843108U, "SMSProtectedVoicemail");
			Strings.stringIDs.Add(1630794581U, "descArialGreySmallFontTag");
			Strings.stringIDs.Add(918712133U, "descDelegateNoPerm");
			Strings.stringIDs.Add(3244013810U, "descTahomaGreyMediumFontTag");
			Strings.stringIDs.Add(2931808677U, "descAcceptedAll");
			Strings.stringIDs.Add(2892660168U, "descInPolicyDelegateMessage");
			Strings.stringIDs.Add(1177578330U, "SMSEmptyCallerId");
			Strings.stringIDs.Add(4066928937U, "descMeetingOrganizerAndTimeLabel");
			Strings.stringIDs.Add(3209486321U, "descAcknowledgeTentativeAccept");
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000023C8 File Offset: 0x000005C8
		public static LocalizedString descRecurringNotAllowed
		{
			get
			{
				return new LocalizedString("descRecurringNotAllowed", "Ex10BBBE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000023E6 File Offset: 0x000005E6
		public static LocalizedString descSingleBooked
		{
			get
			{
				return new LocalizedString("descSingleBooked", "ExD58370", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002404 File Offset: 0x00000604
		public static LocalizedString descDeclined
		{
			get
			{
				return new LocalizedString("descDeclined", "Ex0F0ED6", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002422 File Offset: 0x00000622
		public static LocalizedString descDelegateConflictList
		{
			get
			{
				return new LocalizedString("descDelegateConflictList", "Ex0CAD29", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002440 File Offset: 0x00000640
		public static LocalizedString SMSMissedCallWithCallerInfo(string callerName, string callerId)
		{
			return new LocalizedString("SMSMissedCallWithCallerInfo", "Ex3EA246", false, true, Strings.ResourceManager, new object[]
			{
				callerName,
				callerId
			});
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002473 File Offset: 0x00000673
		public static LocalizedString descDelegateRecurring
		{
			get
			{
				return new LocalizedString("descDelegateRecurring", "ExBC122E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002491 File Offset: 0x00000691
		public static LocalizedString descAccepted
		{
			get
			{
				return new LocalizedString("descAccepted", "Ex0043C4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000024B0 File Offset: 0x000006B0
		public static LocalizedString DateTimeSingleDay(string date, string startTime, string endTime)
		{
			return new LocalizedString("DateTimeSingleDay", "", false, false, Strings.ResourceManager, new object[]
			{
				date,
				startTime,
				endTime
			});
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000024E7 File Offset: 0x000006E7
		public static LocalizedString descCredit
		{
			get
			{
				return new LocalizedString("descCredit", "ExBEF798", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002508 File Offset: 0x00000708
		public static LocalizedString descDelegateEndDate(string date)
		{
			return new LocalizedString("descDelegateEndDate", "Ex0CA0C8", false, true, Strings.ResourceManager, new object[]
			{
				date
			});
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002538 File Offset: 0x00000738
		public static LocalizedString descAcceptedThrough(string shortDate)
		{
			return new LocalizedString("descAcceptedThrough", "ExF11589", false, true, Strings.ResourceManager, new object[]
			{
				shortDate
			});
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002567 File Offset: 0x00000767
		public static LocalizedString descRoleNotAllowed
		{
			get
			{
				return new LocalizedString("descRoleNotAllowed", "ExE9E956", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002588 File Offset: 0x00000788
		public static LocalizedString SMSNewVoicemailWithCallerInfo(int durationInSecs, string callerName, string callerId)
		{
			return new LocalizedString("SMSNewVoicemailWithCallerInfo", "Ex03119B", false, true, Strings.ResourceManager, new object[]
			{
				durationInSecs,
				callerName,
				callerId
			});
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000025C4 File Offset: 0x000007C4
		public static LocalizedString descInThePast
		{
			get
			{
				return new LocalizedString("descInThePast", "Ex4F8870", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000025E2 File Offset: 0x000007E2
		public static LocalizedString RetainUntil
		{
			get
			{
				return new LocalizedString("RetainUntil", "Ex77FB38", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002600 File Offset: 0x00000800
		public static LocalizedString descRecurringBooked
		{
			get
			{
				return new LocalizedString("descRecurringBooked", "ExEC240C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000261E File Offset: 0x0000081E
		public static LocalizedString descRecurringSomeAccepted
		{
			get
			{
				return new LocalizedString("descRecurringSomeAccepted", "Ex13707D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000263C File Offset: 0x0000083C
		public static LocalizedString descTahomaBlackMediumFontTag
		{
			get
			{
				return new LocalizedString("descTahomaBlackMediumFontTag", "ExE2D423", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000265C File Offset: 0x0000085C
		public static LocalizedString descBookingWindow(string days, string date)
		{
			return new LocalizedString("descBookingWindow", "ExCD069B", false, true, Strings.ResourceManager, new object[]
			{
				days,
				date
			});
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000268F File Offset: 0x0000088F
		public static LocalizedString descSingleAccepted
		{
			get
			{
				return new LocalizedString("descSingleAccepted", "Ex7B2BBB", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000026AD File Offset: 0x000008AD
		public static LocalizedString descCorruptWorkingHours
		{
			get
			{
				return new LocalizedString("descCorruptWorkingHours", "Ex4268E5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000026CB File Offset: 0x000008CB
		public static LocalizedString SMSLowConfidenceTranscription
		{
			get
			{
				return new LocalizedString("SMSLowConfidenceTranscription", "Ex02DF8B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000026EC File Offset: 0x000008EC
		public static LocalizedString descToList(string start, string end)
		{
			return new LocalizedString("descToList", "Ex373ED4", false, true, Strings.ResourceManager, new object[]
			{
				start,
				end
			});
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002720 File Offset: 0x00000920
		public static LocalizedString EventReminderSubject(string subject)
		{
			return new LocalizedString("EventReminderSubject", "", false, false, Strings.ResourceManager, new object[]
			{
				subject
			});
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000274F File Offset: 0x0000094F
		public static LocalizedString descAcknowledgeReceived
		{
			get
			{
				return new LocalizedString("descAcknowledgeReceived", "ExC60BEC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002770 File Offset: 0x00000970
		public static LocalizedString SMSMissedCallWithCallerId(string callerId)
		{
			return new LocalizedString("SMSMissedCallWithCallerId", "Ex31FFD4", false, true, Strings.ResourceManager, new object[]
			{
				callerId
			});
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000279F File Offset: 0x0000099F
		public static LocalizedString descOutOfPolicyDelegateMessage
		{
			get
			{
				return new LocalizedString("descOutOfPolicyDelegateMessage", "Ex3110DC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000027C0 File Offset: 0x000009C0
		public static LocalizedString descCommaList(string first, string last)
		{
			return new LocalizedString("descCommaList", "Ex9FA536", false, true, Strings.ResourceManager, new object[]
			{
				first,
				last
			});
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000027F4 File Offset: 0x000009F4
		public static LocalizedString descRecurringEndDateWindow(string window, string endDate)
		{
			return new LocalizedString("descRecurringEndDateWindow", "Ex957418", false, true, Strings.ResourceManager, new object[]
			{
				window,
				endDate
			});
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002828 File Offset: 0x00000A28
		public static LocalizedString descOn(string list)
		{
			return new LocalizedString("descOn", "Ex9DD11E", false, true, Strings.ResourceManager, new object[]
			{
				list
			});
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002858 File Offset: 0x00000A58
		public static LocalizedString descWorkingHours(string startHour, string endHour, string days)
		{
			return new LocalizedString("descWorkingHours", "Ex9A64E4", false, true, Strings.ResourceManager, new object[]
			{
				startHour,
				endHour,
				days
			});
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000288F File Offset: 0x00000A8F
		public static LocalizedString descSingleConflicts
		{
			get
			{
				return new LocalizedString("descSingleConflicts", "ExE3CD6B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000028B0 File Offset: 0x00000AB0
		public static LocalizedString descRecurringAccepted(string endDate)
		{
			return new LocalizedString("descRecurringAccepted", "Ex1B2ABC", false, true, Strings.ResourceManager, new object[]
			{
				endDate
			});
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000028E0 File Offset: 0x00000AE0
		public static LocalizedString DateTimeMultiDay(string startDate, string startTime, string endDate, string endTime)
		{
			return new LocalizedString("DateTimeMultiDay", "", false, false, Strings.ResourceManager, new object[]
			{
				startDate,
				startTime,
				endDate,
				endTime
			});
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000291C File Offset: 0x00000B1C
		public static LocalizedString descDelegateWorkHours(string hours)
		{
			return new LocalizedString("descDelegateWorkHours", "ExABB9A7", false, true, Strings.ResourceManager, new object[]
			{
				hours
			});
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000294B File Offset: 0x00000B4B
		public static LocalizedString descRecurringConflicts
		{
			get
			{
				return new LocalizedString("descRecurringConflicts", "Ex5E68AA", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002969 File Offset: 0x00000B69
		public static LocalizedString descTimeZoneInfo
		{
			get
			{
				return new LocalizedString("descTimeZoneInfo", "Ex8A8335", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002988 File Offset: 0x00000B88
		public static LocalizedString SMSNewVoicemailWithCallerId(int durationInSecs, string callerId)
		{
			return new LocalizedString("SMSNewVoicemailWithCallerId", "Ex1F8B83", false, true, Strings.ResourceManager, new object[]
			{
				durationInSecs,
				callerId
			});
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000029C0 File Offset: 0x00000BC0
		public static LocalizedString descToLong(string maxDuration)
		{
			return new LocalizedString("descToLong", "Ex3D5C9A", false, true, Strings.ResourceManager, new object[]
			{
				maxDuration
			});
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000029EF File Offset: 0x00000BEF
		public static LocalizedString descSingleBookedSomeAccepted
		{
			get
			{
				return new LocalizedString("descSingleBookedSomeAccepted", "ExD1C45A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002A0D File Offset: 0x00000C0D
		public static LocalizedString descMeetingTimeLabel
		{
			get
			{
				return new LocalizedString("descMeetingTimeLabel", "Ex7955C3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002A2C File Offset: 0x00000C2C
		public static LocalizedString descDeclinedInstance(string shortDate)
		{
			return new LocalizedString("descDeclinedInstance", "ExABCDE1", false, true, Strings.ResourceManager, new object[]
			{
				shortDate
			});
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002A5B File Offset: 0x00000C5B
		public static LocalizedString descBody
		{
			get
			{
				return new LocalizedString("descBody", "Ex0DF370", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002A79 File Offset: 0x00000C79
		public static LocalizedString descDelegateConflicts
		{
			get
			{
				return new LocalizedString("descDelegateConflicts", "ExB336BA", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002A98 File Offset: 0x00000C98
		public static LocalizedString EventReminderSummary(string dateTime, string location)
		{
			return new LocalizedString("EventReminderSummary", "", false, false, Strings.ResourceManager, new object[]
			{
				dateTime,
				location
			});
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002ACC File Offset: 0x00000CCC
		public static LocalizedString descDelegatePleaseVerify(string user)
		{
			return new LocalizedString("descDelegatePleaseVerify", "Ex51BE8F", false, true, Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002AFB File Offset: 0x00000CFB
		public static LocalizedString descDeclinedAll
		{
			get
			{
				return new LocalizedString("descDeclinedAll", "Ex80AB69", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002B1C File Offset: 0x00000D1C
		public static LocalizedString EventReminderSummaryNoLocation(string dateTime)
		{
			return new LocalizedString("EventReminderSummaryNoLocation", "", false, false, Strings.ResourceManager, new object[]
			{
				dateTime
			});
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002B4C File Offset: 0x00000D4C
		public static LocalizedString ReminderMessageBody(string summary, string customMessage)
		{
			return new LocalizedString("ReminderMessageBody", "", false, false, Strings.ResourceManager, new object[]
			{
				summary,
				customMessage
			});
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002B80 File Offset: 0x00000D80
		public static LocalizedString descAndList(string first, string last)
		{
			return new LocalizedString("descAndList", "Ex3D8456", false, true, Strings.ResourceManager, new object[]
			{
				first,
				last
			});
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002BB3 File Offset: 0x00000DB3
		public static LocalizedString SMSProtectedVoicemail
		{
			get
			{
				return new LocalizedString("SMSProtectedVoicemail", "ExBEA9CA", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002BD1 File Offset: 0x00000DD1
		public static LocalizedString descArialGreySmallFontTag
		{
			get
			{
				return new LocalizedString("descArialGreySmallFontTag", "ExBEEFA8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002BEF File Offset: 0x00000DEF
		public static LocalizedString descDelegateNoPerm
		{
			get
			{
				return new LocalizedString("descDelegateNoPerm", "Ex0A52A7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002C10 File Offset: 0x00000E10
		public static LocalizedString descDelegateNoEndDate(string date)
		{
			return new LocalizedString("descDelegateNoEndDate", "Ex3ED270", false, true, Strings.ResourceManager, new object[]
			{
				date
			});
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002C3F File Offset: 0x00000E3F
		public static LocalizedString descTahomaGreyMediumFontTag
		{
			get
			{
				return new LocalizedString("descTahomaGreyMediumFontTag", "ExC1F40F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002C5D File Offset: 0x00000E5D
		public static LocalizedString descAcceptedAll
		{
			get
			{
				return new LocalizedString("descAcceptedAll", "ExBB42F6", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002C7C File Offset: 0x00000E7C
		public static LocalizedString descRecurringEndDate(string endDate)
		{
			return new LocalizedString("descRecurringEndDate", "ExB13AC3", false, true, Strings.ResourceManager, new object[]
			{
				endDate
			});
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002CAB File Offset: 0x00000EAB
		public static LocalizedString descInPolicyDelegateMessage
		{
			get
			{
				return new LocalizedString("descInPolicyDelegateMessage", "Ex6D23AD", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002CC9 File Offset: 0x00000EC9
		public static LocalizedString SMSEmptyCallerId
		{
			get
			{
				return new LocalizedString("SMSEmptyCallerId", "ExDF7549", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002CE8 File Offset: 0x00000EE8
		public static LocalizedString descDelegateTooLong(string minutes)
		{
			return new LocalizedString("descDelegateTooLong", "ExCDBC94", false, true, Strings.ResourceManager, new object[]
			{
				minutes
			});
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002D17 File Offset: 0x00000F17
		public static LocalizedString descMeetingOrganizerAndTimeLabel
		{
			get
			{
				return new LocalizedString("descMeetingOrganizerAndTimeLabel", "ExE98BCC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002D35 File Offset: 0x00000F35
		public static LocalizedString descAcknowledgeTentativeAccept
		{
			get
			{
				return new LocalizedString("descAcknowledgeTentativeAccept", "ExD300A0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002D53 File Offset: 0x00000F53
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000001 RID: 1
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(35);

		// Token: 0x04000002 RID: 2
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.InfoWorker.AssistantsClientResources.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000003 RID: 3
		public enum IDs : uint
		{
			// Token: 0x04000004 RID: 4
			descRecurringNotAllowed = 394366253U,
			// Token: 0x04000005 RID: 5
			descSingleBooked = 1148026803U,
			// Token: 0x04000006 RID: 6
			descDeclined = 566335833U,
			// Token: 0x04000007 RID: 7
			descDelegateConflictList = 95666746U,
			// Token: 0x04000008 RID: 8
			descDelegateRecurring = 3021003899U,
			// Token: 0x04000009 RID: 9
			descAccepted = 167053646U,
			// Token: 0x0400000A RID: 10
			descCredit = 1075460866U,
			// Token: 0x0400000B RID: 11
			descRoleNotAllowed = 2827051240U,
			// Token: 0x0400000C RID: 12
			descInThePast = 736995773U,
			// Token: 0x0400000D RID: 13
			RetainUntil = 3503731055U,
			// Token: 0x0400000E RID: 14
			descRecurringBooked = 3589522754U,
			// Token: 0x0400000F RID: 15
			descRecurringSomeAccepted = 1579274145U,
			// Token: 0x04000010 RID: 16
			descTahomaBlackMediumFontTag = 3779075844U,
			// Token: 0x04000011 RID: 17
			descSingleAccepted = 3990825802U,
			// Token: 0x04000012 RID: 18
			descCorruptWorkingHours = 3328869470U,
			// Token: 0x04000013 RID: 19
			SMSLowConfidenceTranscription = 3716547363U,
			// Token: 0x04000014 RID: 20
			descAcknowledgeReceived = 2683960550U,
			// Token: 0x04000015 RID: 21
			descOutOfPolicyDelegateMessage = 2847829128U,
			// Token: 0x04000016 RID: 22
			descSingleConflicts = 1043033118U,
			// Token: 0x04000017 RID: 23
			descRecurringConflicts = 2938636641U,
			// Token: 0x04000018 RID: 24
			descTimeZoneInfo = 4274878084U,
			// Token: 0x04000019 RID: 25
			descSingleBookedSomeAccepted = 4066812006U,
			// Token: 0x0400001A RID: 26
			descMeetingTimeLabel = 445569887U,
			// Token: 0x0400001B RID: 27
			descBody = 1687944893U,
			// Token: 0x0400001C RID: 28
			descDelegateConflicts = 2293492535U,
			// Token: 0x0400001D RID: 29
			descDeclinedAll = 203356656U,
			// Token: 0x0400001E RID: 30
			SMSProtectedVoicemail = 3332843108U,
			// Token: 0x0400001F RID: 31
			descArialGreySmallFontTag = 1630794581U,
			// Token: 0x04000020 RID: 32
			descDelegateNoPerm = 918712133U,
			// Token: 0x04000021 RID: 33
			descTahomaGreyMediumFontTag = 3244013810U,
			// Token: 0x04000022 RID: 34
			descAcceptedAll = 2931808677U,
			// Token: 0x04000023 RID: 35
			descInPolicyDelegateMessage = 2892660168U,
			// Token: 0x04000024 RID: 36
			SMSEmptyCallerId = 1177578330U,
			// Token: 0x04000025 RID: 37
			descMeetingOrganizerAndTimeLabel = 4066928937U,
			// Token: 0x04000026 RID: 38
			descAcknowledgeTentativeAccept = 3209486321U
		}

		// Token: 0x02000004 RID: 4
		private enum ParamIDs
		{
			// Token: 0x04000028 RID: 40
			SMSMissedCallWithCallerInfo,
			// Token: 0x04000029 RID: 41
			DateTimeSingleDay,
			// Token: 0x0400002A RID: 42
			descDelegateEndDate,
			// Token: 0x0400002B RID: 43
			descAcceptedThrough,
			// Token: 0x0400002C RID: 44
			SMSNewVoicemailWithCallerInfo,
			// Token: 0x0400002D RID: 45
			descBookingWindow,
			// Token: 0x0400002E RID: 46
			descToList,
			// Token: 0x0400002F RID: 47
			EventReminderSubject,
			// Token: 0x04000030 RID: 48
			SMSMissedCallWithCallerId,
			// Token: 0x04000031 RID: 49
			descCommaList,
			// Token: 0x04000032 RID: 50
			descRecurringEndDateWindow,
			// Token: 0x04000033 RID: 51
			descOn,
			// Token: 0x04000034 RID: 52
			descWorkingHours,
			// Token: 0x04000035 RID: 53
			descRecurringAccepted,
			// Token: 0x04000036 RID: 54
			DateTimeMultiDay,
			// Token: 0x04000037 RID: 55
			descDelegateWorkHours,
			// Token: 0x04000038 RID: 56
			SMSNewVoicemailWithCallerId,
			// Token: 0x04000039 RID: 57
			descToLong,
			// Token: 0x0400003A RID: 58
			descDeclinedInstance,
			// Token: 0x0400003B RID: 59
			EventReminderSummary,
			// Token: 0x0400003C RID: 60
			descDelegatePleaseVerify,
			// Token: 0x0400003D RID: 61
			EventReminderSummaryNoLocation,
			// Token: 0x0400003E RID: 62
			ReminderMessageBody,
			// Token: 0x0400003F RID: 63
			descAndList,
			// Token: 0x04000040 RID: 64
			descDelegateNoEndDate,
			// Token: 0x04000041 RID: 65
			descRecurringEndDate,
			// Token: 0x04000042 RID: 66
			descDelegateTooLong
		}
	}
}
