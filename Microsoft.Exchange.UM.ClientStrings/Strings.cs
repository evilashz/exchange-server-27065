using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.UM.UMCommon.MessageContent
{
	// Token: 0x02000002 RID: 2
	internal static class Strings
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		static Strings()
		{
			Strings.stringIDs.Add(1163166080U, "VoicemailSettingsInstruction2");
			Strings.stringIDs.Add(1025467514U, "TranscriptionSkippedDueToProtectedVoiceMail");
			Strings.stringIDs.Add(3395113910U, "FaxSearchFolderName");
			Strings.stringIDs.Add(399665603U, "MissedCallSubject");
			Strings.stringIDs.Add(440863302U, "TranscriptionSkippedDueToRejectionDetails");
			Strings.stringIDs.Add(3618250418U, "Font1");
			Strings.stringIDs.Add(2631270417U, "Extension");
			Strings.stringIDs.Add(986397318U, "Recipients");
			Strings.stringIDs.Add(647750600U, "CallAnsweringNDRForDRMSubject");
			Strings.stringIDs.Add(559962466U, "OneMinute");
			Strings.stringIDs.Add(3428332004U, "AnonymousCaller");
			Strings.stringIDs.Add(1801440247U, "SentHeader");
			Strings.stringIDs.Add(1655837788U, "WelcomeMailBodyHeader");
			Strings.stringIDs.Add(1505519378U, "IncompleteFaxMailSubjectInPage");
			Strings.stringIDs.Add(4118032635U, "AccessNumber");
			Strings.stringIDs.Add(1708549140U, "ConsumerGetStartedText");
			Strings.stringIDs.Add(2942039717U, "InterpersonalNDRForDRMFooter");
			Strings.stringIDs.Add(1163166083U, "VoicemailSettingsInstruction1");
			Strings.stringIDs.Add(1712197018U, "FollowUp");
			Strings.stringIDs.Add(4145413099U, "NoTranscription");
			Strings.stringIDs.Add(1111077458U, "Email");
			Strings.stringIDs.Add(3394475845U, "TwelveNoon");
			Strings.stringIDs.Add(1827164709U, "InformationText");
			Strings.stringIDs.Add(2254833676U, "UMBodyDownload");
			Strings.stringIDs.Add(931278067U, "ConsumerUpdatePhoneNoExtension");
			Strings.stringIDs.Add(1158653436U, "MobilePhone");
			Strings.stringIDs.Add(2757445932U, "TranscriptionSkippedDueToSystemErrorDetails");
			Strings.stringIDs.Add(2279047064U, "AccessMailText");
			Strings.stringIDs.Add(1617820192U, "AccessNumberSeparator");
			Strings.stringIDs.Add(4151878805U, "HomePhoneLabel");
			Strings.stringIDs.Add(1455517480U, "WrittenTimeWithZeroMinutesFormat");
			Strings.stringIDs.Add(761385855U, "SubjectHeader");
			Strings.stringIDs.Add(2682518168U, "TranscriptionSkippedDueToLongMessageDetails");
			Strings.stringIDs.Add(1163166081U, "VoicemailSettingsInstruction3");
			Strings.stringIDs.Add(2844710954U, "OneSecond");
			Strings.stringIDs.Add(3899977494U, "OriginalAppointment");
			Strings.stringIDs.Add(642177943U, "Company");
			Strings.stringIDs.Add(2815524084U, "VoiceMailPreviewWithColon");
			Strings.stringIDs.Add(1689870310U, "FaxMailSubjectInPage");
			Strings.stringIDs.Add(3572895339U, "WhenHeader");
			Strings.stringIDs.Add(3435115712U, "IMAddress");
			Strings.stringIDs.Add(1106684832U, "WhereHeader");
			Strings.stringIDs.Add(2138533332U, "TranscriptionSkippedDueToThrottlingDetails");
			Strings.stringIDs.Add(587115635U, "JobTitle");
			Strings.stringIDs.Add(2397318150U, "TranscriptionSkippedDueToTimeoutDetails");
			Strings.stringIDs.Add(2980268693U, "TwelveMidnight");
			Strings.stringIDs.Add(2102852648U, "TranscriptionSkippedDueToPoorAudioQualityDetails");
			Strings.stringIDs.Add(2100892799U, "ConsumerConfigurePhoneGeneric");
			Strings.stringIDs.Add(2065109949U, "UnknownCaller");
			Strings.stringIDs.Add(3334166220U, "ParagraphEndNewLines");
			Strings.stringIDs.Add(3992915955U, "CcHeader");
			Strings.stringIDs.Add(3618250421U, "Font2");
			Strings.stringIDs.Add(1448959650U, "MobilePhoneLabel");
			Strings.stringIDs.Add(1915770880U, "OriginalMessage");
			Strings.stringIDs.Add(1683967347U, "CallAnsweringNDRForDRMFooter");
			Strings.stringIDs.Add(4087711248U, "InterpersonalNDRForDRMSubject");
			Strings.stringIDs.Add(3460905333U, "PasswordResetHeader");
			Strings.stringIDs.Add(2644606382U, "TranscriptionSkippedDueToErrorReadingSettings");
			Strings.stringIDs.Add(605091578U, "ConfigureVoicemailSettings");
			Strings.stringIDs.Add(1133914413U, "LearnMore");
			Strings.stringIDs.Add(2872664749U, "FromHeader");
			Strings.stringIDs.Add(1095233868U, "AccessMailTextConsumer");
			Strings.stringIDs.Add(2366595845U, "ReservedTimeTitle");
			Strings.stringIDs.Add(1736107127U, "WelcomeMailSubject");
			Strings.stringIDs.Add(2199732666U, "ToHeader");
			Strings.stringIDs.Add(3413781026U, "CallNotForwardedText");
			Strings.stringIDs.Add(3756600696U, "NumSpaceBeforeEOS");
			Strings.stringIDs.Add(35799476U, "InterpersonalNDRForDRM");
			Strings.stringIDs.Add(1051645941U, "TranscriptionSkippedDueToBadRequestDetails");
			Strings.stringIDs.Add(2839150813U, "WorkPhone");
			Strings.stringIDs.Add(3423762652U, "Sent");
			Strings.stringIDs.Add(776589994U, "EndOfParagraphMarker");
			Strings.stringIDs.Add(2961085015U, "WorkPhoneLabel");
			Strings.stringIDs.Add(3317306807U, "NumSpaceAfterEOS");
			Strings.stringIDs.Add(2450102343U, "HomePhone");
			Strings.stringIDs.Add(2800104273U, "VoiceMailPreview");
			Strings.stringIDs.Add(711958779U, "Recipient");
			Strings.stringIDs.Add(107281980U, "CallerId");
			Strings.stringIDs.Add(1976913622U, "UMWebServicePage");
			Strings.stringIDs.Add(1801195254U, "PasswordResetSubject");
			Strings.stringIDs.Add(339800695U, "Pin");
			Strings.stringIDs.Add(1655162919U, "TranscriptionSkippedDefaultDetails");
			Strings.stringIDs.Add(3733401898U, "ConsumerConfigurePhone");
			Strings.stringIDs.Add(3439047833U, "EndOfSentenceMarker");
			Strings.stringIDs.Add(143792894U, "OneMinuteOneSecond");
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000027B0 File Offset: 0x000009B0
		public static LocalizedString ForwardWithRecording(string displayName)
		{
			return new LocalizedString("ForwardWithRecording", Strings.ResourceManager, new object[]
			{
				displayName
			});
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000027D8 File Offset: 0x000009D8
		public static LocalizedString MinutesSeconds(int min, int sec)
		{
			return new LocalizedString("MinutesSeconds", Strings.ResourceManager, new object[]
			{
				min,
				sec
			});
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002810 File Offset: 0x00000A10
		public static LocalizedString FaxMailSubjectInPages(uint pages)
		{
			return new LocalizedString("FaxMailSubjectInPages", Strings.ResourceManager, new object[]
			{
				pages
			});
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002840 File Offset: 0x00000A40
		public static LocalizedString MissedCallBodyCallerResolved(string senderName, string senderPhone, string phoneLabel)
		{
			return new LocalizedString("MissedCallBodyCallerResolved", Strings.ResourceManager, new object[]
			{
				senderName,
				senderPhone,
				phoneLabel
			});
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002870 File Offset: 0x00000A70
		public static LocalizedString OutgoingCallLogBodyTargetMultipleResolved(string multipleResolvedTargets)
		{
			return new LocalizedString("OutgoingCallLogBodyTargetMultipleResolved", Strings.ResourceManager, new object[]
			{
				multipleResolvedTargets
			});
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002898 File Offset: 0x00000A98
		public static LocalizedString VoicemailSettingsInstruction2
		{
			get
			{
				return new LocalizedString("VoicemailSettingsInstruction2", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000028AF File Offset: 0x00000AAF
		public static LocalizedString TranscriptionSkippedDueToProtectedVoiceMail
		{
			get
			{
				return new LocalizedString("TranscriptionSkippedDueToProtectedVoiceMail", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000028C8 File Offset: 0x00000AC8
		public static LocalizedString IncomingCallLogBodyCallerMultipleResolved(string multipleResolvedSenders)
		{
			return new LocalizedString("IncomingCallLogBodyCallerMultipleResolved", Strings.ResourceManager, new object[]
			{
				multipleResolvedSenders
			});
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000028F0 File Offset: 0x00000AF0
		public static LocalizedString FaxSearchFolderName
		{
			get
			{
				return new LocalizedString("FaxSearchFolderName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002908 File Offset: 0x00000B08
		public static LocalizedString VoicemailSubject(string duration)
		{
			return new LocalizedString("VoicemailSubject", Strings.ResourceManager, new object[]
			{
				duration
			});
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002930 File Offset: 0x00000B30
		public static LocalizedString MissedCallSubject
		{
			get
			{
				return new LocalizedString("MissedCallSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002947 File Offset: 0x00000B47
		public static LocalizedString TranscriptionSkippedDueToRejectionDetails
		{
			get
			{
				return new LocalizedString("TranscriptionSkippedDueToRejectionDetails", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000295E File Offset: 0x00000B5E
		public static LocalizedString Font1
		{
			get
			{
				return new LocalizedString("Font1", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002978 File Offset: 0x00000B78
		public static LocalizedString MissedCallBodyCallerUnresolved(string senderPhone)
		{
			return new LocalizedString("MissedCallBodyCallerUnresolved", Strings.ResourceManager, new object[]
			{
				senderPhone
			});
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000029A0 File Offset: 0x00000BA0
		public static LocalizedString Extension
		{
			get
			{
				return new LocalizedString("Extension", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000029B7 File Offset: 0x00000BB7
		public static LocalizedString Recipients
		{
			get
			{
				return new LocalizedString("Recipients", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000029CE File Offset: 0x00000BCE
		public static LocalizedString CallAnsweringNDRForDRMSubject
		{
			get
			{
				return new LocalizedString("CallAnsweringNDRForDRMSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000029E5 File Offset: 0x00000BE5
		public static LocalizedString OneMinute
		{
			get
			{
				return new LocalizedString("OneMinute", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000029FC File Offset: 0x00000BFC
		public static LocalizedString AnonymousCaller
		{
			get
			{
				return new LocalizedString("AnonymousCaller", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002A14 File Offset: 0x00000C14
		public static LocalizedString IncomingCallLogSubject(string sender)
		{
			return new LocalizedString("IncomingCallLogSubject", Strings.ResourceManager, new object[]
			{
				sender
			});
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002A3C File Offset: 0x00000C3C
		public static LocalizedString SentHeader
		{
			get
			{
				return new LocalizedString("SentHeader", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002A54 File Offset: 0x00000C54
		public static LocalizedString ShortTimeMonthDay(string shortTime, string monthDay)
		{
			return new LocalizedString("ShortTimeMonthDay", Strings.ResourceManager, new object[]
			{
				shortTime,
				monthDay
			});
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002A80 File Offset: 0x00000C80
		public static LocalizedString MultipleResolvedContactDisplayWithMoreThanTwoMatches(string number, string firstContact, string secondContact)
		{
			return new LocalizedString("MultipleResolvedContactDisplayWithMoreThanTwoMatches", Strings.ResourceManager, new object[]
			{
				number,
				firstContact,
				secondContact
			});
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public static LocalizedString LateForMeeting_Plural2(int minutesLate)
		{
			return new LocalizedString("LateForMeeting_Plural2", Strings.ResourceManager, new object[]
			{
				minutesLate
			});
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002AE0 File Offset: 0x00000CE0
		public static LocalizedString OutgoingCallLogBodyTargetUnresolved(string target)
		{
			return new LocalizedString("OutgoingCallLogBodyTargetUnresolved", Strings.ResourceManager, new object[]
			{
				target
			});
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002B08 File Offset: 0x00000D08
		public static LocalizedString WelcomeMailBodyHeader
		{
			get
			{
				return new LocalizedString("WelcomeMailBodyHeader", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002B1F File Offset: 0x00000D1F
		public static LocalizedString IncompleteFaxMailSubjectInPage
		{
			get
			{
				return new LocalizedString("IncompleteFaxMailSubjectInPage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002B38 File Offset: 0x00000D38
		public static LocalizedString FaxBodyCallerResolved(string senderName, string senderPhone, string phoneLabel)
		{
			return new LocalizedString("FaxBodyCallerResolved", Strings.ResourceManager, new object[]
			{
				senderName,
				senderPhone,
				phoneLabel
			});
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002B68 File Offset: 0x00000D68
		public static LocalizedString AccessNumber
		{
			get
			{
				return new LocalizedString("AccessNumber", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002B80 File Offset: 0x00000D80
		public static LocalizedString CallNotForwardedBody(string sender, string target)
		{
			return new LocalizedString("CallNotForwardedBody", Strings.ResourceManager, new object[]
			{
				sender,
				target
			});
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002BAC File Offset: 0x00000DAC
		public static LocalizedString ConsumerGetStartedText
		{
			get
			{
				return new LocalizedString("ConsumerGetStartedText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002BC3 File Offset: 0x00000DC3
		public static LocalizedString InterpersonalNDRForDRMFooter
		{
			get
			{
				return new LocalizedString("InterpersonalNDRForDRMFooter", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002BDA File Offset: 0x00000DDA
		public static LocalizedString VoicemailSettingsInstruction1
		{
			get
			{
				return new LocalizedString("VoicemailSettingsInstruction1", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002BF1 File Offset: 0x00000DF1
		public static LocalizedString FollowUp
		{
			get
			{
				return new LocalizedString("FollowUp", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002C08 File Offset: 0x00000E08
		public static LocalizedString NoTranscription
		{
			get
			{
				return new LocalizedString("NoTranscription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002C1F File Offset: 0x00000E1F
		public static LocalizedString Email
		{
			get
			{
				return new LocalizedString("Email", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002C36 File Offset: 0x00000E36
		public static LocalizedString TwelveNoon
		{
			get
			{
				return new LocalizedString("TwelveNoon", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002C4D File Offset: 0x00000E4D
		public static LocalizedString InformationText
		{
			get
			{
				return new LocalizedString("InformationText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002C64 File Offset: 0x00000E64
		public static LocalizedString OutgoingCallLogBodyTargetResolved(string target, string phone)
		{
			return new LocalizedString("OutgoingCallLogBodyTargetResolved", Strings.ResourceManager, new object[]
			{
				target,
				phone
			});
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002C90 File Offset: 0x00000E90
		public static LocalizedString FaxBodyCallerMultipleResolved(string multipleResolvedSenders)
		{
			return new LocalizedString("FaxBodyCallerMultipleResolved", Strings.ResourceManager, new object[]
			{
				multipleResolvedSenders
			});
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002CB8 File Offset: 0x00000EB8
		public static LocalizedString UMBodyDownload
		{
			get
			{
				return new LocalizedString("UMBodyDownload", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002CD0 File Offset: 0x00000ED0
		public static LocalizedString TeamPickUpSubjectWithLabel(string sender, LocalizedString phoneLabel, string answeredBy)
		{
			return new LocalizedString("TeamPickUpSubjectWithLabel", Strings.ResourceManager, new object[]
			{
				sender,
				phoneLabel,
				answeredBy
			});
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002D08 File Offset: 0x00000F08
		public static LocalizedString LateForMeetingRange_Plural2(int minutesLateMin, int minutesLateMax)
		{
			return new LocalizedString("LateForMeetingRange_Plural2", Strings.ResourceManager, new object[]
			{
				minutesLateMin,
				minutesLateMax
			});
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002D3E File Offset: 0x00000F3E
		public static LocalizedString ConsumerUpdatePhoneNoExtension
		{
			get
			{
				return new LocalizedString("ConsumerUpdatePhoneNoExtension", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002D58 File Offset: 0x00000F58
		public static LocalizedString CallForwardedSubject(string sender, string target)
		{
			return new LocalizedString("CallForwardedSubject", Strings.ResourceManager, new object[]
			{
				sender,
				target
			});
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002D84 File Offset: 0x00000F84
		public static LocalizedString VoiceMailBodyCallerResolvedNoCallerId(string senderName)
		{
			return new LocalizedString("VoiceMailBodyCallerResolvedNoCallerId", Strings.ResourceManager, new object[]
			{
				senderName
			});
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002DAC File Offset: 0x00000FAC
		public static LocalizedString MissedCallBodyCallerMultipleResolved(string multipleResolvedSenders)
		{
			return new LocalizedString("MissedCallBodyCallerMultipleResolved", Strings.ResourceManager, new object[]
			{
				multipleResolvedSenders
			});
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002DD4 File Offset: 0x00000FD4
		public static LocalizedString MobilePhone
		{
			get
			{
				return new LocalizedString("MobilePhone", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002DEC File Offset: 0x00000FEC
		public static LocalizedString AttachmentNameWithNumber(string callerId, string duration, int dupeNumber)
		{
			return new LocalizedString("AttachmentNameWithNumber", Strings.ResourceManager, new object[]
			{
				callerId,
				duration,
				dupeNumber
			});
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002E21 File Offset: 0x00001021
		public static LocalizedString TranscriptionSkippedDueToSystemErrorDetails
		{
			get
			{
				return new LocalizedString("TranscriptionSkippedDueToSystemErrorDetails", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002E38 File Offset: 0x00001038
		public static LocalizedString AccessMailText
		{
			get
			{
				return new LocalizedString("AccessMailText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002E4F File Offset: 0x0000104F
		public static LocalizedString AccessNumberSeparator
		{
			get
			{
				return new LocalizedString("AccessNumberSeparator", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002E68 File Offset: 0x00001068
		public static LocalizedString Seconds(int sec)
		{
			return new LocalizedString("Seconds", Strings.ResourceManager, new object[]
			{
				sec
			});
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002E98 File Offset: 0x00001098
		public static LocalizedString TeamPickUpBody(string sender, string answeredBy)
		{
			return new LocalizedString("TeamPickUpBody", Strings.ResourceManager, new object[]
			{
				sender,
				answeredBy
			});
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002EC4 File Offset: 0x000010C4
		public static LocalizedString OutgoingCallLogSubject(string target)
		{
			return new LocalizedString("OutgoingCallLogSubject", Strings.ResourceManager, new object[]
			{
				target
			});
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002EEC File Offset: 0x000010EC
		public static LocalizedString HomePhoneLabel
		{
			get
			{
				return new LocalizedString("HomePhoneLabel", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002F04 File Offset: 0x00001104
		public static LocalizedString LateForMeeting_Singular(int minutesLate)
		{
			return new LocalizedString("LateForMeeting_Singular", Strings.ResourceManager, new object[]
			{
				minutesLate
			});
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002F31 File Offset: 0x00001131
		public static LocalizedString WrittenTimeWithZeroMinutesFormat
		{
			get
			{
				return new LocalizedString("WrittenTimeWithZeroMinutesFormat", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002F48 File Offset: 0x00001148
		public static LocalizedString SubjectHeader
		{
			get
			{
				return new LocalizedString("SubjectHeader", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002F5F File Offset: 0x0000115F
		public static LocalizedString TranscriptionSkippedDueToLongMessageDetails
		{
			get
			{
				return new LocalizedString("TranscriptionSkippedDueToLongMessageDetails", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002F78 File Offset: 0x00001178
		public static LocalizedString FaxBodyCallerResolvedNoPhoneLabel(string senderName, string senderPhone)
		{
			return new LocalizedString("FaxBodyCallerResolvedNoPhoneLabel", Strings.ResourceManager, new object[]
			{
				senderName,
				senderPhone
			});
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002FA4 File Offset: 0x000011A4
		public static LocalizedString ConsumerUpdatePhoneWithExtension(string extension)
		{
			return new LocalizedString("ConsumerUpdatePhoneWithExtension", Strings.ResourceManager, new object[]
			{
				extension
			});
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002FCC File Offset: 0x000011CC
		public static LocalizedString LateForMeetingRange_Singular(int minutesLateMin, int minutesLateMax)
		{
			return new LocalizedString("LateForMeetingRange_Singular", Strings.ResourceManager, new object[]
			{
				minutesLateMin,
				minutesLateMax
			});
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003004 File Offset: 0x00001204
		public static LocalizedString LateForMeeting_Plural(int minutesLate)
		{
			return new LocalizedString("LateForMeeting_Plural", Strings.ResourceManager, new object[]
			{
				minutesLate
			});
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003034 File Offset: 0x00001234
		public static LocalizedString CallNotForwardedSubjectWithLabel(string sender, LocalizedString phoneLabel, string target)
		{
			return new LocalizedString("CallNotForwardedSubjectWithLabel", Strings.ResourceManager, new object[]
			{
				sender,
				phoneLabel,
				target
			});
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000306C File Offset: 0x0000126C
		public static LocalizedString CallForwardedBody(string sender, string target)
		{
			return new LocalizedString("CallForwardedBody", Strings.ResourceManager, new object[]
			{
				sender,
				target
			});
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00003098 File Offset: 0x00001298
		public static LocalizedString VoicemailSettingsInstruction3
		{
			get
			{
				return new LocalizedString("VoicemailSettingsInstruction3", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000030B0 File Offset: 0x000012B0
		public static LocalizedString IncomingCallLogSubjectWithLabel(string sender, LocalizedString phoneLabel)
		{
			return new LocalizedString("IncomingCallLogSubjectWithLabel", Strings.ResourceManager, new object[]
			{
				sender,
				phoneLabel
			});
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000030E4 File Offset: 0x000012E4
		public static LocalizedString IncomingCallLogBodyCallerResolved(string sender, string phone)
		{
			return new LocalizedString("IncomingCallLogBodyCallerResolved", Strings.ResourceManager, new object[]
			{
				sender,
				phone
			});
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003110 File Offset: 0x00001310
		public static LocalizedString QualifiedMeetingTime(string start, string end, string timezone)
		{
			return new LocalizedString("QualifiedMeetingTime", Strings.ResourceManager, new object[]
			{
				start,
				end,
				timezone
			});
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003140 File Offset: 0x00001340
		public static LocalizedString OfficeAddress(string off, string addr, string city, string state, string zipcode, string country)
		{
			return new LocalizedString("OfficeAddress", Strings.ResourceManager, new object[]
			{
				off,
				addr,
				city,
				state,
				zipcode,
				country
			});
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003180 File Offset: 0x00001380
		public static LocalizedString CallNotForwardedSubject(string sender, string target)
		{
			return new LocalizedString("CallNotForwardedSubject", Strings.ResourceManager, new object[]
			{
				sender,
				target
			});
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000031AC File Offset: 0x000013AC
		public static LocalizedString OneSecond
		{
			get
			{
				return new LocalizedString("OneSecond", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000031C4 File Offset: 0x000013C4
		public static LocalizedString VoiceMessageSubject(string duration)
		{
			return new LocalizedString("VoiceMessageSubject", Strings.ResourceManager, new object[]
			{
				duration
			});
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000031EC File Offset: 0x000013EC
		public static LocalizedString DisambiguatedNamePrefix(string userName)
		{
			return new LocalizedString("DisambiguatedNamePrefix", Strings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00003214 File Offset: 0x00001414
		public static LocalizedString OriginalAppointment
		{
			get
			{
				return new LocalizedString("OriginalAppointment", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000322B File Offset: 0x0000142B
		public static LocalizedString Company
		{
			get
			{
				return new LocalizedString("Company", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003242 File Offset: 0x00001442
		public static LocalizedString VoiceMailPreviewWithColon
		{
			get
			{
				return new LocalizedString("VoiceMailPreviewWithColon", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003259 File Offset: 0x00001459
		public static LocalizedString FaxMailSubjectInPage
		{
			get
			{
				return new LocalizedString("FaxMailSubjectInPage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003270 File Offset: 0x00001470
		public static LocalizedString OutgoingCallLogSubjectWithLabel(string target, LocalizedString phoneLabel)
		{
			return new LocalizedString("OutgoingCallLogSubjectWithLabel", Strings.ResourceManager, new object[]
			{
				target,
				phoneLabel
			});
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000032A4 File Offset: 0x000014A4
		public static LocalizedString IncompleteFaxMailSubjectInPages(uint pages)
		{
			return new LocalizedString("IncompleteFaxMailSubjectInPages", Strings.ResourceManager, new object[]
			{
				pages
			});
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000032D1 File Offset: 0x000014D1
		public static LocalizedString WhenHeader
		{
			get
			{
				return new LocalizedString("WhenHeader", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000032E8 File Offset: 0x000014E8
		public static LocalizedString DateTime(string timeFormat, string dayFormat)
		{
			return new LocalizedString("DateTime", Strings.ResourceManager, new object[]
			{
				timeFormat,
				dayFormat
			});
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003314 File Offset: 0x00001514
		public static LocalizedString IMAddress
		{
			get
			{
				return new LocalizedString("IMAddress", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000056 RID: 86 RVA: 0x0000332B File Offset: 0x0000152B
		public static LocalizedString WhereHeader
		{
			get
			{
				return new LocalizedString("WhereHeader", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003342 File Offset: 0x00001542
		public static LocalizedString TranscriptionSkippedDueToThrottlingDetails
		{
			get
			{
				return new LocalizedString("TranscriptionSkippedDueToThrottlingDetails", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000335C File Offset: 0x0000155C
		public static LocalizedString CallAnsweringNDRForDRMCallerUnResolved(string senderPhone)
		{
			return new LocalizedString("CallAnsweringNDRForDRMCallerUnResolved", Strings.ResourceManager, new object[]
			{
				senderPhone
			});
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003384 File Offset: 0x00001584
		public static LocalizedString VoiceMailBodyCallerResolved(string senderName, string senderPhone, string phoneLabel)
		{
			return new LocalizedString("VoiceMailBodyCallerResolved", Strings.ResourceManager, new object[]
			{
				senderName,
				senderPhone,
				phoneLabel
			});
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000033B4 File Offset: 0x000015B4
		public static LocalizedString JobTitle
		{
			get
			{
				return new LocalizedString("JobTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000033CB File Offset: 0x000015CB
		public static LocalizedString TranscriptionSkippedDueToTimeoutDetails
		{
			get
			{
				return new LocalizedString("TranscriptionSkippedDueToTimeoutDetails", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000033E4 File Offset: 0x000015E4
		public static LocalizedString NoEmailAddressDisplayName(string displayName)
		{
			return new LocalizedString("NoEmailAddressDisplayName", Strings.ResourceManager, new object[]
			{
				displayName
			});
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000340C File Offset: 0x0000160C
		public static LocalizedString AutogeneratedPlusOriginalSubject(LocalizedString autoGenerated, string original)
		{
			return new LocalizedString("AutogeneratedPlusOriginalSubject", Strings.ResourceManager, new object[]
			{
				autoGenerated,
				original
			});
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600005E RID: 94 RVA: 0x0000343D File Offset: 0x0000163D
		public static LocalizedString TwelveMidnight
		{
			get
			{
				return new LocalizedString("TwelveMidnight", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003454 File Offset: 0x00001654
		public static LocalizedString FaxAttachmentInPages(string senderNumber, uint numPages, string tifFileExtension)
		{
			return new LocalizedString("FaxAttachmentInPages", Strings.ResourceManager, new object[]
			{
				senderNumber,
				numPages,
				tifFileExtension
			});
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003489 File Offset: 0x00001689
		public static LocalizedString TranscriptionSkippedDueToPoorAudioQualityDetails
		{
			get
			{
				return new LocalizedString("TranscriptionSkippedDueToPoorAudioQualityDetails", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000034A0 File Offset: 0x000016A0
		public static LocalizedString VoiceMailBodyCallerMultipleResolved(string multipleResolvedSenders)
		{
			return new LocalizedString("VoiceMailBodyCallerMultipleResolved", Strings.ResourceManager, new object[]
			{
				multipleResolvedSenders
			});
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000034C8 File Offset: 0x000016C8
		public static LocalizedString ConsumerConfigurePhoneGeneric
		{
			get
			{
				return new LocalizedString("ConsumerConfigurePhoneGeneric", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000034DF File Offset: 0x000016DF
		public static LocalizedString UnknownCaller
		{
			get
			{
				return new LocalizedString("UnknownCaller", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000034F6 File Offset: 0x000016F6
		public static LocalizedString ParagraphEndNewLines
		{
			get
			{
				return new LocalizedString("ParagraphEndNewLines", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000065 RID: 101 RVA: 0x0000350D File Offset: 0x0000170D
		public static LocalizedString CcHeader
		{
			get
			{
				return new LocalizedString("CcHeader", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003524 File Offset: 0x00001724
		public static LocalizedString Font2
		{
			get
			{
				return new LocalizedString("Font2", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000353C File Offset: 0x0000173C
		public static LocalizedString Minutes(int min)
		{
			return new LocalizedString("Minutes", Strings.ResourceManager, new object[]
			{
				min
			});
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003569 File Offset: 0x00001769
		public static LocalizedString MobilePhoneLabel
		{
			get
			{
				return new LocalizedString("MobilePhoneLabel", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003580 File Offset: 0x00001780
		public static LocalizedString MinutesOneSecond(int min)
		{
			return new LocalizedString("MinutesOneSecond", Strings.ResourceManager, new object[]
			{
				min
			});
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000035AD File Offset: 0x000017AD
		public static LocalizedString OriginalMessage
		{
			get
			{
				return new LocalizedString("OriginalMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000035C4 File Offset: 0x000017C4
		public static LocalizedString LateForMeetingRange_Zero(int minutesLateMin, int minutesLateMax)
		{
			return new LocalizedString("LateForMeetingRange_Zero", Strings.ResourceManager, new object[]
			{
				minutesLateMin,
				minutesLateMax
			});
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000035FC File Offset: 0x000017FC
		public static LocalizedString MissedCallBodyCallerResolvedNoPhoneLabel(string senderName, string senderPhone)
		{
			return new LocalizedString("MissedCallBodyCallerResolvedNoPhoneLabel", Strings.ResourceManager, new object[]
			{
				senderName,
				senderPhone
			});
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003628 File Offset: 0x00001828
		public static LocalizedString FaxBodyCallerUnresolved(string senderPhone)
		{
			return new LocalizedString("FaxBodyCallerUnresolved", Strings.ResourceManager, new object[]
			{
				senderPhone
			});
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003650 File Offset: 0x00001850
		public static LocalizedString CallAnsweringNDRForDRMFooter
		{
			get
			{
				return new LocalizedString("CallAnsweringNDRForDRMFooter", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003668 File Offset: 0x00001868
		public static LocalizedString CallForwardedSubjectWithLabel(string sender, LocalizedString phoneLabel, string target)
		{
			return new LocalizedString("CallForwardedSubjectWithLabel", Strings.ResourceManager, new object[]
			{
				sender,
				phoneLabel,
				target
			});
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000369D File Offset: 0x0000189D
		public static LocalizedString InterpersonalNDRForDRMSubject
		{
			get
			{
				return new LocalizedString("InterpersonalNDRForDRMSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000036B4 File Offset: 0x000018B4
		public static LocalizedString PasswordResetHeader
		{
			get
			{
				return new LocalizedString("PasswordResetHeader", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000036CB File Offset: 0x000018CB
		public static LocalizedString TranscriptionSkippedDueToErrorReadingSettings
		{
			get
			{
				return new LocalizedString("TranscriptionSkippedDueToErrorReadingSettings", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000036E4 File Offset: 0x000018E4
		public static LocalizedString FaxAttachmentInPage(string senderNumber, string tifFileExtension)
		{
			return new LocalizedString("FaxAttachmentInPage", Strings.ResourceManager, new object[]
			{
				senderNumber,
				tifFileExtension
			});
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003710 File Offset: 0x00001910
		public static LocalizedString ConsumerAccess(string pin)
		{
			return new LocalizedString("ConsumerAccess", Strings.ResourceManager, new object[]
			{
				pin
			});
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003738 File Offset: 0x00001938
		public static LocalizedString ConfigureVoicemailSettings
		{
			get
			{
				return new LocalizedString("ConfigureVoicemailSettings", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000076 RID: 118 RVA: 0x0000374F File Offset: 0x0000194F
		public static LocalizedString LearnMore
		{
			get
			{
				return new LocalizedString("LearnMore", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003766 File Offset: 0x00001966
		public static LocalizedString FromHeader
		{
			get
			{
				return new LocalizedString("FromHeader", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000377D File Offset: 0x0000197D
		public static LocalizedString AccessMailTextConsumer
		{
			get
			{
				return new LocalizedString("AccessMailTextConsumer", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003794 File Offset: 0x00001994
		public static LocalizedString ReservedTimeTitle
		{
			get
			{
				return new LocalizedString("ReservedTimeTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000037AB File Offset: 0x000019AB
		public static LocalizedString WelcomeMailSubject
		{
			get
			{
				return new LocalizedString("WelcomeMailSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000037C2 File Offset: 0x000019C2
		public static LocalizedString ToHeader
		{
			get
			{
				return new LocalizedString("ToHeader", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000037DC File Offset: 0x000019DC
		public static LocalizedString InformationTextWithLink(string val1, string val2)
		{
			return new LocalizedString("InformationTextWithLink", Strings.ResourceManager, new object[]
			{
				val1,
				val2
			});
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003808 File Offset: 0x00001A08
		public static LocalizedString VoiceMailBodyCallerResolvedNoPhoneLabel(string senderName, string senderPhone)
		{
			return new LocalizedString("VoiceMailBodyCallerResolvedNoPhoneLabel", Strings.ResourceManager, new object[]
			{
				senderName,
				senderPhone
			});
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003834 File Offset: 0x00001A34
		public static LocalizedString CallNotForwardedText
		{
			get
			{
				return new LocalizedString("CallNotForwardedText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000384B File Offset: 0x00001A4B
		public static LocalizedString NumSpaceBeforeEOS
		{
			get
			{
				return new LocalizedString("NumSpaceBeforeEOS", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003862 File Offset: 0x00001A62
		public static LocalizedString InterpersonalNDRForDRM
		{
			get
			{
				return new LocalizedString("InterpersonalNDRForDRM", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003879 File Offset: 0x00001A79
		public static LocalizedString TranscriptionSkippedDueToBadRequestDetails
		{
			get
			{
				return new LocalizedString("TranscriptionSkippedDueToBadRequestDetails", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003890 File Offset: 0x00001A90
		public static LocalizedString WorkPhone
		{
			get
			{
				return new LocalizedString("WorkPhone", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000038A7 File Offset: 0x00001AA7
		public static LocalizedString Sent
		{
			get
			{
				return new LocalizedString("Sent", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000038BE File Offset: 0x00001ABE
		public static LocalizedString EndOfParagraphMarker
		{
			get
			{
				return new LocalizedString("EndOfParagraphMarker", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000038D8 File Offset: 0x00001AD8
		public static LocalizedString SayNumberAndName(int number, string name)
		{
			return new LocalizedString("SayNumberAndName", Strings.ResourceManager, new object[]
			{
				number,
				name
			});
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003909 File Offset: 0x00001B09
		public static LocalizedString WorkPhoneLabel
		{
			get
			{
				return new LocalizedString("WorkPhoneLabel", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003920 File Offset: 0x00001B20
		public static LocalizedString LateForMeetingRange_Plural(int minutesLateMin, int minutesLateMax)
		{
			return new LocalizedString("LateForMeetingRange_Plural", Strings.ResourceManager, new object[]
			{
				minutesLateMin,
				minutesLateMax
			});
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003958 File Offset: 0x00001B58
		public static LocalizedString ReplyWithRecording(string displayName)
		{
			return new LocalizedString("ReplyWithRecording", Strings.ResourceManager, new object[]
			{
				displayName
			});
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003980 File Offset: 0x00001B80
		public static LocalizedString DefaultEmailOOFText(string userName)
		{
			return new LocalizedString("DefaultEmailOOFText", Strings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000039A8 File Offset: 0x00001BA8
		public static LocalizedString NumSpaceAfterEOS
		{
			get
			{
				return new LocalizedString("NumSpaceAfterEOS", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000039C0 File Offset: 0x00001BC0
		public static LocalizedString IncomingCallLogBodyCallerUnresolved(string phone)
		{
			return new LocalizedString("IncomingCallLogBodyCallerUnresolved", Strings.ResourceManager, new object[]
			{
				phone
			});
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000039E8 File Offset: 0x00001BE8
		public static LocalizedString HomePhone
		{
			get
			{
				return new LocalizedString("HomePhone", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000039FF File Offset: 0x00001BFF
		public static LocalizedString VoiceMailPreview
		{
			get
			{
				return new LocalizedString("VoiceMailPreview", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003A16 File Offset: 0x00001C16
		public static LocalizedString Recipient
		{
			get
			{
				return new LocalizedString("Recipient", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003A30 File Offset: 0x00001C30
		public static LocalizedString TeamPickUpSubject(string sender, string answeredBy)
		{
			return new LocalizedString("TeamPickUpSubject", Strings.ResourceManager, new object[]
			{
				sender,
				answeredBy
			});
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003A5C File Offset: 0x00001C5C
		public static LocalizedString ReservedTimeBody(string userName, string creationDate)
		{
			return new LocalizedString("ReservedTimeBody", Strings.ResourceManager, new object[]
			{
				userName,
				creationDate
			});
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003A88 File Offset: 0x00001C88
		public static LocalizedString CallerId
		{
			get
			{
				return new LocalizedString("CallerId", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003A9F File Offset: 0x00001C9F
		public static LocalizedString UMWebServicePage
		{
			get
			{
				return new LocalizedString("UMWebServicePage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003AB8 File Offset: 0x00001CB8
		public static LocalizedString VoiceMailBodyCallerUnresolved(string senderPhone)
		{
			return new LocalizedString("VoiceMailBodyCallerUnresolved", Strings.ResourceManager, new object[]
			{
				senderPhone
			});
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003AE0 File Offset: 0x00001CE0
		public static LocalizedString PasswordResetSubject
		{
			get
			{
				return new LocalizedString("PasswordResetSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003AF8 File Offset: 0x00001CF8
		public static LocalizedString HeaderEntry(string entryName, string entryValue)
		{
			return new LocalizedString("HeaderEntry", Strings.ResourceManager, new object[]
			{
				entryName,
				entryValue
			});
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003B24 File Offset: 0x00001D24
		public static LocalizedString MultipleResolvedContactDisplayWithTwoMatches(string number, string firstContact, string secondContact)
		{
			return new LocalizedString("MultipleResolvedContactDisplayWithTwoMatches", Strings.ResourceManager, new object[]
			{
				number,
				firstContact,
				secondContact
			});
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003B54 File Offset: 0x00001D54
		public static LocalizedString Pin
		{
			get
			{
				return new LocalizedString("Pin", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003B6B File Offset: 0x00001D6B
		public static LocalizedString TranscriptionSkippedDefaultDetails
		{
			get
			{
				return new LocalizedString("TranscriptionSkippedDefaultDetails", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003B84 File Offset: 0x00001D84
		public static LocalizedString WeekDayShortTime(string weekDay, string shortTime)
		{
			return new LocalizedString("WeekDayShortTime", Strings.ResourceManager, new object[]
			{
				weekDay,
				shortTime
			});
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003BB0 File Offset: 0x00001DB0
		public static LocalizedString CallAnsweringNDRForDRMCallerResolved(string senderName)
		{
			return new LocalizedString("CallAnsweringNDRForDRMCallerResolved", Strings.ResourceManager, new object[]
			{
				senderName
			});
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003BD8 File Offset: 0x00001DD8
		public static LocalizedString ConsumerConfigurePhone
		{
			get
			{
				return new LocalizedString("ConsumerConfigurePhone", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003BF0 File Offset: 0x00001DF0
		public static LocalizedString AttachmentName(string callerId, string duration)
		{
			return new LocalizedString("AttachmentName", Strings.ResourceManager, new object[]
			{
				callerId,
				duration
			});
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003C1C File Offset: 0x00001E1C
		public static LocalizedString EndOfSentenceMarker
		{
			get
			{
				return new LocalizedString("EndOfSentenceMarker", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003C34 File Offset: 0x00001E34
		public static LocalizedString LateForMeeting_Zero(int minutesLate)
		{
			return new LocalizedString("LateForMeeting_Zero", Strings.ResourceManager, new object[]
			{
				minutesLate
			});
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003C64 File Offset: 0x00001E64
		public static LocalizedString OneMinuteSeconds(int sec)
		{
			return new LocalizedString("OneMinuteSeconds", Strings.ResourceManager, new object[]
			{
				sec
			});
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003C94 File Offset: 0x00001E94
		public static LocalizedString CancelledMeetingSubject(string subject)
		{
			return new LocalizedString("CancelledMeetingSubject", Strings.ResourceManager, new object[]
			{
				subject
			});
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003CBC File Offset: 0x00001EBC
		public static LocalizedString OneMinuteOneSecond
		{
			get
			{
				return new LocalizedString("OneMinuteOneSecond", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003CD3 File Offset: 0x00001ED3
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000001 RID: 1
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(85);

		// Token: 0x04000002 RID: 2
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.UM.UMCommon.MessageContent.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000003 RID: 3
		public enum IDs : uint
		{
			// Token: 0x04000004 RID: 4
			VoicemailSettingsInstruction2 = 1163166080U,
			// Token: 0x04000005 RID: 5
			TranscriptionSkippedDueToProtectedVoiceMail = 1025467514U,
			// Token: 0x04000006 RID: 6
			FaxSearchFolderName = 3395113910U,
			// Token: 0x04000007 RID: 7
			MissedCallSubject = 399665603U,
			// Token: 0x04000008 RID: 8
			TranscriptionSkippedDueToRejectionDetails = 440863302U,
			// Token: 0x04000009 RID: 9
			Font1 = 3618250418U,
			// Token: 0x0400000A RID: 10
			Extension = 2631270417U,
			// Token: 0x0400000B RID: 11
			Recipients = 986397318U,
			// Token: 0x0400000C RID: 12
			CallAnsweringNDRForDRMSubject = 647750600U,
			// Token: 0x0400000D RID: 13
			OneMinute = 559962466U,
			// Token: 0x0400000E RID: 14
			AnonymousCaller = 3428332004U,
			// Token: 0x0400000F RID: 15
			SentHeader = 1801440247U,
			// Token: 0x04000010 RID: 16
			WelcomeMailBodyHeader = 1655837788U,
			// Token: 0x04000011 RID: 17
			IncompleteFaxMailSubjectInPage = 1505519378U,
			// Token: 0x04000012 RID: 18
			AccessNumber = 4118032635U,
			// Token: 0x04000013 RID: 19
			ConsumerGetStartedText = 1708549140U,
			// Token: 0x04000014 RID: 20
			InterpersonalNDRForDRMFooter = 2942039717U,
			// Token: 0x04000015 RID: 21
			VoicemailSettingsInstruction1 = 1163166083U,
			// Token: 0x04000016 RID: 22
			FollowUp = 1712197018U,
			// Token: 0x04000017 RID: 23
			NoTranscription = 4145413099U,
			// Token: 0x04000018 RID: 24
			Email = 1111077458U,
			// Token: 0x04000019 RID: 25
			TwelveNoon = 3394475845U,
			// Token: 0x0400001A RID: 26
			InformationText = 1827164709U,
			// Token: 0x0400001B RID: 27
			UMBodyDownload = 2254833676U,
			// Token: 0x0400001C RID: 28
			ConsumerUpdatePhoneNoExtension = 931278067U,
			// Token: 0x0400001D RID: 29
			MobilePhone = 1158653436U,
			// Token: 0x0400001E RID: 30
			TranscriptionSkippedDueToSystemErrorDetails = 2757445932U,
			// Token: 0x0400001F RID: 31
			AccessMailText = 2279047064U,
			// Token: 0x04000020 RID: 32
			AccessNumberSeparator = 1617820192U,
			// Token: 0x04000021 RID: 33
			HomePhoneLabel = 4151878805U,
			// Token: 0x04000022 RID: 34
			WrittenTimeWithZeroMinutesFormat = 1455517480U,
			// Token: 0x04000023 RID: 35
			SubjectHeader = 761385855U,
			// Token: 0x04000024 RID: 36
			TranscriptionSkippedDueToLongMessageDetails = 2682518168U,
			// Token: 0x04000025 RID: 37
			VoicemailSettingsInstruction3 = 1163166081U,
			// Token: 0x04000026 RID: 38
			OneSecond = 2844710954U,
			// Token: 0x04000027 RID: 39
			OriginalAppointment = 3899977494U,
			// Token: 0x04000028 RID: 40
			Company = 642177943U,
			// Token: 0x04000029 RID: 41
			VoiceMailPreviewWithColon = 2815524084U,
			// Token: 0x0400002A RID: 42
			FaxMailSubjectInPage = 1689870310U,
			// Token: 0x0400002B RID: 43
			WhenHeader = 3572895339U,
			// Token: 0x0400002C RID: 44
			IMAddress = 3435115712U,
			// Token: 0x0400002D RID: 45
			WhereHeader = 1106684832U,
			// Token: 0x0400002E RID: 46
			TranscriptionSkippedDueToThrottlingDetails = 2138533332U,
			// Token: 0x0400002F RID: 47
			JobTitle = 587115635U,
			// Token: 0x04000030 RID: 48
			TranscriptionSkippedDueToTimeoutDetails = 2397318150U,
			// Token: 0x04000031 RID: 49
			TwelveMidnight = 2980268693U,
			// Token: 0x04000032 RID: 50
			TranscriptionSkippedDueToPoorAudioQualityDetails = 2102852648U,
			// Token: 0x04000033 RID: 51
			ConsumerConfigurePhoneGeneric = 2100892799U,
			// Token: 0x04000034 RID: 52
			UnknownCaller = 2065109949U,
			// Token: 0x04000035 RID: 53
			ParagraphEndNewLines = 3334166220U,
			// Token: 0x04000036 RID: 54
			CcHeader = 3992915955U,
			// Token: 0x04000037 RID: 55
			Font2 = 3618250421U,
			// Token: 0x04000038 RID: 56
			MobilePhoneLabel = 1448959650U,
			// Token: 0x04000039 RID: 57
			OriginalMessage = 1915770880U,
			// Token: 0x0400003A RID: 58
			CallAnsweringNDRForDRMFooter = 1683967347U,
			// Token: 0x0400003B RID: 59
			InterpersonalNDRForDRMSubject = 4087711248U,
			// Token: 0x0400003C RID: 60
			PasswordResetHeader = 3460905333U,
			// Token: 0x0400003D RID: 61
			TranscriptionSkippedDueToErrorReadingSettings = 2644606382U,
			// Token: 0x0400003E RID: 62
			ConfigureVoicemailSettings = 605091578U,
			// Token: 0x0400003F RID: 63
			LearnMore = 1133914413U,
			// Token: 0x04000040 RID: 64
			FromHeader = 2872664749U,
			// Token: 0x04000041 RID: 65
			AccessMailTextConsumer = 1095233868U,
			// Token: 0x04000042 RID: 66
			ReservedTimeTitle = 2366595845U,
			// Token: 0x04000043 RID: 67
			WelcomeMailSubject = 1736107127U,
			// Token: 0x04000044 RID: 68
			ToHeader = 2199732666U,
			// Token: 0x04000045 RID: 69
			CallNotForwardedText = 3413781026U,
			// Token: 0x04000046 RID: 70
			NumSpaceBeforeEOS = 3756600696U,
			// Token: 0x04000047 RID: 71
			InterpersonalNDRForDRM = 35799476U,
			// Token: 0x04000048 RID: 72
			TranscriptionSkippedDueToBadRequestDetails = 1051645941U,
			// Token: 0x04000049 RID: 73
			WorkPhone = 2839150813U,
			// Token: 0x0400004A RID: 74
			Sent = 3423762652U,
			// Token: 0x0400004B RID: 75
			EndOfParagraphMarker = 776589994U,
			// Token: 0x0400004C RID: 76
			WorkPhoneLabel = 2961085015U,
			// Token: 0x0400004D RID: 77
			NumSpaceAfterEOS = 3317306807U,
			// Token: 0x0400004E RID: 78
			HomePhone = 2450102343U,
			// Token: 0x0400004F RID: 79
			VoiceMailPreview = 2800104273U,
			// Token: 0x04000050 RID: 80
			Recipient = 711958779U,
			// Token: 0x04000051 RID: 81
			CallerId = 107281980U,
			// Token: 0x04000052 RID: 82
			UMWebServicePage = 1976913622U,
			// Token: 0x04000053 RID: 83
			PasswordResetSubject = 1801195254U,
			// Token: 0x04000054 RID: 84
			Pin = 339800695U,
			// Token: 0x04000055 RID: 85
			TranscriptionSkippedDefaultDetails = 1655162919U,
			// Token: 0x04000056 RID: 86
			ConsumerConfigurePhone = 3733401898U,
			// Token: 0x04000057 RID: 87
			EndOfSentenceMarker = 3439047833U,
			// Token: 0x04000058 RID: 88
			OneMinuteOneSecond = 143792894U
		}

		// Token: 0x02000004 RID: 4
		private enum ParamIDs
		{
			// Token: 0x0400005A RID: 90
			ForwardWithRecording,
			// Token: 0x0400005B RID: 91
			MinutesSeconds,
			// Token: 0x0400005C RID: 92
			FaxMailSubjectInPages,
			// Token: 0x0400005D RID: 93
			MissedCallBodyCallerResolved,
			// Token: 0x0400005E RID: 94
			OutgoingCallLogBodyTargetMultipleResolved,
			// Token: 0x0400005F RID: 95
			IncomingCallLogBodyCallerMultipleResolved,
			// Token: 0x04000060 RID: 96
			VoicemailSubject,
			// Token: 0x04000061 RID: 97
			MissedCallBodyCallerUnresolved,
			// Token: 0x04000062 RID: 98
			IncomingCallLogSubject,
			// Token: 0x04000063 RID: 99
			ShortTimeMonthDay,
			// Token: 0x04000064 RID: 100
			MultipleResolvedContactDisplayWithMoreThanTwoMatches,
			// Token: 0x04000065 RID: 101
			LateForMeeting_Plural2,
			// Token: 0x04000066 RID: 102
			OutgoingCallLogBodyTargetUnresolved,
			// Token: 0x04000067 RID: 103
			FaxBodyCallerResolved,
			// Token: 0x04000068 RID: 104
			CallNotForwardedBody,
			// Token: 0x04000069 RID: 105
			OutgoingCallLogBodyTargetResolved,
			// Token: 0x0400006A RID: 106
			FaxBodyCallerMultipleResolved,
			// Token: 0x0400006B RID: 107
			TeamPickUpSubjectWithLabel,
			// Token: 0x0400006C RID: 108
			LateForMeetingRange_Plural2,
			// Token: 0x0400006D RID: 109
			CallForwardedSubject,
			// Token: 0x0400006E RID: 110
			VoiceMailBodyCallerResolvedNoCallerId,
			// Token: 0x0400006F RID: 111
			MissedCallBodyCallerMultipleResolved,
			// Token: 0x04000070 RID: 112
			AttachmentNameWithNumber,
			// Token: 0x04000071 RID: 113
			Seconds,
			// Token: 0x04000072 RID: 114
			TeamPickUpBody,
			// Token: 0x04000073 RID: 115
			OutgoingCallLogSubject,
			// Token: 0x04000074 RID: 116
			LateForMeeting_Singular,
			// Token: 0x04000075 RID: 117
			FaxBodyCallerResolvedNoPhoneLabel,
			// Token: 0x04000076 RID: 118
			ConsumerUpdatePhoneWithExtension,
			// Token: 0x04000077 RID: 119
			LateForMeetingRange_Singular,
			// Token: 0x04000078 RID: 120
			LateForMeeting_Plural,
			// Token: 0x04000079 RID: 121
			CallNotForwardedSubjectWithLabel,
			// Token: 0x0400007A RID: 122
			CallForwardedBody,
			// Token: 0x0400007B RID: 123
			IncomingCallLogSubjectWithLabel,
			// Token: 0x0400007C RID: 124
			IncomingCallLogBodyCallerResolved,
			// Token: 0x0400007D RID: 125
			QualifiedMeetingTime,
			// Token: 0x0400007E RID: 126
			OfficeAddress,
			// Token: 0x0400007F RID: 127
			CallNotForwardedSubject,
			// Token: 0x04000080 RID: 128
			VoiceMessageSubject,
			// Token: 0x04000081 RID: 129
			DisambiguatedNamePrefix,
			// Token: 0x04000082 RID: 130
			OutgoingCallLogSubjectWithLabel,
			// Token: 0x04000083 RID: 131
			IncompleteFaxMailSubjectInPages,
			// Token: 0x04000084 RID: 132
			DateTime,
			// Token: 0x04000085 RID: 133
			CallAnsweringNDRForDRMCallerUnResolved,
			// Token: 0x04000086 RID: 134
			VoiceMailBodyCallerResolved,
			// Token: 0x04000087 RID: 135
			NoEmailAddressDisplayName,
			// Token: 0x04000088 RID: 136
			AutogeneratedPlusOriginalSubject,
			// Token: 0x04000089 RID: 137
			FaxAttachmentInPages,
			// Token: 0x0400008A RID: 138
			VoiceMailBodyCallerMultipleResolved,
			// Token: 0x0400008B RID: 139
			Minutes,
			// Token: 0x0400008C RID: 140
			MinutesOneSecond,
			// Token: 0x0400008D RID: 141
			LateForMeetingRange_Zero,
			// Token: 0x0400008E RID: 142
			MissedCallBodyCallerResolvedNoPhoneLabel,
			// Token: 0x0400008F RID: 143
			FaxBodyCallerUnresolved,
			// Token: 0x04000090 RID: 144
			CallForwardedSubjectWithLabel,
			// Token: 0x04000091 RID: 145
			FaxAttachmentInPage,
			// Token: 0x04000092 RID: 146
			ConsumerAccess,
			// Token: 0x04000093 RID: 147
			InformationTextWithLink,
			// Token: 0x04000094 RID: 148
			VoiceMailBodyCallerResolvedNoPhoneLabel,
			// Token: 0x04000095 RID: 149
			SayNumberAndName,
			// Token: 0x04000096 RID: 150
			LateForMeetingRange_Plural,
			// Token: 0x04000097 RID: 151
			ReplyWithRecording,
			// Token: 0x04000098 RID: 152
			DefaultEmailOOFText,
			// Token: 0x04000099 RID: 153
			IncomingCallLogBodyCallerUnresolved,
			// Token: 0x0400009A RID: 154
			TeamPickUpSubject,
			// Token: 0x0400009B RID: 155
			ReservedTimeBody,
			// Token: 0x0400009C RID: 156
			VoiceMailBodyCallerUnresolved,
			// Token: 0x0400009D RID: 157
			HeaderEntry,
			// Token: 0x0400009E RID: 158
			MultipleResolvedContactDisplayWithTwoMatches,
			// Token: 0x0400009F RID: 159
			WeekDayShortTime,
			// Token: 0x040000A0 RID: 160
			CallAnsweringNDRForDRMCallerResolved,
			// Token: 0x040000A1 RID: 161
			AttachmentName,
			// Token: 0x040000A2 RID: 162
			LateForMeeting_Zero,
			// Token: 0x040000A3 RID: 163
			OneMinuteSeconds,
			// Token: 0x040000A4 RID: 164
			CancelledMeetingSubject
		}
	}
}
