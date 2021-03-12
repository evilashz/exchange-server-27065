using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.MessageContent;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020000A3 RID: 163
	internal abstract class Constants
	{
		// Token: 0x0600064D RID: 1613 RVA: 0x0001A964 File Offset: 0x00018B64
		private Constants()
		{
		}

		// Token: 0x04000284 RID: 644
		internal const string TestModeCanaryFileName = "UMTEST-C798DBA2-1B87-11DC-9A33-5A6656D89593.bin";

		// Token: 0x04000285 RID: 645
		internal const string Zero = "Zero";

		// Token: 0x04000286 RID: 646
		internal const string Singular = "Singular";

		// Token: 0x04000287 RID: 647
		internal const string Plural = "Plural";

		// Token: 0x04000288 RID: 648
		internal const string Plural2 = "Plural2";

		// Token: 0x04000289 RID: 649
		internal const int ThreadInitTimeout = 30;

		// Token: 0x0400028A RID: 650
		internal const int ContactSearchLimit = 100;

		// Token: 0x0400028B RID: 651
		internal const int HResultMask = 65535;

		// Token: 0x0400028C RID: 652
		internal const byte MaxDtmfSizeUpper = 16;

		// Token: 0x0400028D RID: 653
		internal const byte MaxPromptsPerActivity = 16;

		// Token: 0x0400028E RID: 654
		internal const string VuiPromptPrefix = "vui";

		// Token: 0x0400028F RID: 655
		internal const string UmConfigSchemaFile = "umconfig.xsd";

		// Token: 0x04000290 RID: 656
		internal const string DtmfGrammarFile = "dtmfrules.grxml";

		// Token: 0x04000291 RID: 657
		internal const string LanguageAutoDetectionMinLength = "LanguageAutoDetectionMinLength";

		// Token: 0x04000292 RID: 658
		internal const string LanguageAutoDetectionMaxLength = "LanguageAutoDetectionMaxLength";

		// Token: 0x04000293 RID: 659
		internal const string DtmfGrammarRuleName = "DtmfSequence";

		// Token: 0x04000294 RID: 660
		internal const string DtmfGrammarDigitsNode = "<item repeat=\"{0}-{1}\"><ruleref uri=\"#Digit\" type=\"application/srgs+xml\"/><tag>$._value = $._value + $$._value</tag></item>";

		// Token: 0x04000295 RID: 661
		internal const string DtmfGrammarStopTonesNode = "<one-of>{0}</one-of>";

		// Token: 0x04000296 RID: 662
		internal const string DtmfGrammarStopToneNode = "<item><item>{0}</item><tag>$._value = $._value + \"{0}\"</tag></item>";

		// Token: 0x04000297 RID: 663
		internal const string DtmfGrammarStopPatternNode = "<item><item>{0}</item><tag>$._value = \"{1}\"</tag></item>";

		// Token: 0x04000298 RID: 664
		internal const string AnonymousUser = "Anonymous";

		// Token: 0x04000299 RID: 665
		internal const string AllDtmfDigits = "0123456789#*ABCD";

		// Token: 0x0400029A RID: 666
		internal const int DtmfTimeout = 10;

		// Token: 0x0400029B RID: 667
		internal const int InitialSilenceTimeout = 6;

		// Token: 0x0400029C RID: 668
		internal const int InterDigitTimeout = 1;

		// Token: 0x0400029D RID: 669
		internal const int IncompleteTimeout = 3;

		// Token: 0x0400029E RID: 670
		internal const int NumberIncorrectInputs = 3;

		// Token: 0x0400029F RID: 671
		internal const int MaxRecordingSilence = 3;

		// Token: 0x040002A0 RID: 672
		internal const int WaveFileType = 6;

		// Token: 0x040002A1 RID: 673
		internal const int MaxSpeechRecognitionAlternatives = 10;

		// Token: 0x040002A2 RID: 674
		internal const int MaxRecordingSeconds = 600;

		// Token: 0x040002A3 RID: 675
		internal const int MaxErrorsAllowed = 3;

		// Token: 0x040002A4 RID: 676
		internal const int TTSPromptsVolume = 70;

		// Token: 0x040002A5 RID: 677
		internal const float ProsodyRateIncrement = 0.15f;

		// Token: 0x040002A6 RID: 678
		internal const float MaxProsodyRate = 0.6f;

		// Token: 0x040002A7 RID: 679
		internal const float MinProsodyRate = -0.6f;

		// Token: 0x040002A8 RID: 680
		internal const long MinFreeMegabytesDiskSpaceDatacenter = 50L;

		// Token: 0x040002A9 RID: 681
		internal const long MinFreeMegabytesDiskSpaceEnterprise = 500L;

		// Token: 0x040002AA RID: 682
		internal const long MinFreeMegabytesDiskSpaceWarning = 750L;

		// Token: 0x040002AB RID: 683
		internal const char DtmfEscapeCharacter = '#';

		// Token: 0x040002AC RID: 684
		internal const string FaxTone = "faxtone";

		// Token: 0x040002AD RID: 685
		internal const string Diversion = "Diversion";

		// Token: 0x040002AE RID: 686
		internal const string CallId = "CALL-ID";

		// Token: 0x040002AF RID: 687
		internal const string CertSNHeader = "P-Certificate-Subject-Common-Name";

		// Token: 0x040002B0 RID: 688
		internal const string CertSANHeader = "P-Certificate-Subject-Alternative-Name";

		// Token: 0x040002B1 RID: 689
		internal const string SipDiversion = "sip:";

		// Token: 0x040002B2 RID: 690
		internal const string SipUriFormat = "<sip:{0}>";

		// Token: 0x040002B3 RID: 691
		internal const string MsDiagnostics = "ms-diagnostics";

		// Token: 0x040002B4 RID: 692
		internal const string MsDiagnosticsPublic = "ms-diagnostics-public";

		// Token: 0x040002B5 RID: 693
		internal const string UserAgent = "User-Agent";

		// Token: 0x040002B6 RID: 694
		internal const int InvalidMailboxClearDigitsTimeout = 1000;

		// Token: 0x040002B7 RID: 695
		internal const double RecordBytesPerSecond = 16000.0;

		// Token: 0x040002B8 RID: 696
		internal const string TestUserAgentName = "Unified Messaging Test Client";

		// Token: 0x040002B9 RID: 697
		internal const string ActiveMonitoringUserAgentName = "ActiveMonitoringClient";

		// Token: 0x040002BA RID: 698
		internal const string MonitoringCertSN = "um.o365.exchangemon.net";

		// Token: 0x040002BB RID: 699
		internal const string MonitoringDomain = "o365.exchangemon.net";

		// Token: 0x040002BC RID: 700
		internal const int ProvisionalResponseCode = 101;

		// Token: 0x040002BD RID: 701
		internal const string ProvisionalResponseName = "Diagnostics";

		// Token: 0x040002BE RID: 702
		internal const string RedirectDiagnosticsFormat = "{0};source=\"{1}\";reason=\"Redirecting to:{2};time={3}\"";

		// Token: 0x040002BF RID: 703
		internal const string CallReceivedDiagnosticsFormat = "{0};source=\"{1}\";reason=\"{2}\";service=\"{3}\";time=\"{4}\"";

		// Token: 0x040002C0 RID: 704
		internal const string ServerHealthDiagnosticsFormat = "{0};source=\"{1}\";reason=\"{2}\";service=\"{3}\";health=\"{4}\";time=\"{5}\"";

		// Token: 0x040002C1 RID: 705
		internal const string CallTimeoutDiagnosticsFormat = "{0};source=\"{1}\";reason=\"{2}\";service=\"{3}\";time=\"{4}\"";

		// Token: 0x040002C2 RID: 706
		internal const string CallState = "Call-State: ";

		// Token: 0x040002C3 RID: 707
		internal const string DiversionHeader = "Diversion";

		// Token: 0x040002C4 RID: 708
		internal const string DiversionNumber = "number";

		// Token: 0x040002C5 RID: 709
		internal const int AudioBufferLength = 4096;

		// Token: 0x040002C6 RID: 710
		internal const int AudioNumOfBuffers = 5;

		// Token: 0x040002C7 RID: 711
		internal const int DtmfCngTone = 36;

		// Token: 0x040002C8 RID: 712
		internal const int MediaInitializeTimeout = 5;

		// Token: 0x040002C9 RID: 713
		internal const int SampleRate = 8000;

		// Token: 0x040002CA RID: 714
		internal const int BitsPerSample = 16;

		// Token: 0x040002CB RID: 715
		internal const int NumMediaPoolElements = 128;

		// Token: 0x040002CC RID: 716
		internal const int BitsperSec = 128000;

		// Token: 0x040002CD RID: 717
		internal const int WmaBitsperSec = 13312;

		// Token: 0x040002CE RID: 718
		internal const int WmaHeaderBytes = 8192;

		// Token: 0x040002CF RID: 719
		internal const int WavFileHeaderBytes = 44;

		// Token: 0x040002D0 RID: 720
		internal const int VoiceMailSecsLimit = 2;

		// Token: 0x040002D1 RID: 721
		internal const int MaxNumOfSearchAttempts = 3;

		// Token: 0x040002D2 RID: 722
		internal const int MaxNumOfDiversionLookups = 6;

		// Token: 0x040002D3 RID: 723
		internal const string UnicodeCharset = "unicode";

		// Token: 0x040002D4 RID: 724
		internal const string VoiceCAContentClass = "Voice-CA";

		// Token: 0x040002D5 RID: 725
		internal const string VoiceUCContentClass = "Voice-UC";

		// Token: 0x040002D6 RID: 726
		internal const string VoiceContentClass = "Voice";

		// Token: 0x040002D7 RID: 727
		internal const string MissedCallContentClass = "MissedCall";

		// Token: 0x040002D8 RID: 728
		internal const string FaxCAContentClass = "Fax-CA";

		// Token: 0x040002D9 RID: 729
		internal const string ProviderName = "Exchange12";

		// Token: 0x040002DA RID: 730
		internal const string WaveFileExtensionName = "wav";

		// Token: 0x040002DB RID: 731
		internal const int FileLogMaxChars = 128;

		// Token: 0x040002DC RID: 732
		internal const int BadDigitTimeoutMsec = 1000;

		// Token: 0x040002DD RID: 733
		internal const int RelativeMinutesThreshold = 180;

		// Token: 0x040002DE RID: 734
		internal const double MeetingOverThreshold = 0.5;

		// Token: 0x040002DF RID: 735
		internal const string CancelledMeetingClass = "IPM.Schedule.Meeting.Canceled";

		// Token: 0x040002E0 RID: 736
		internal const string BeepFileName = "Beep.wav";

		// Token: 0x040002E1 RID: 737
		internal const int ChangePasswordChances = 5;

		// Token: 0x040002E2 RID: 738
		internal const int MaxExtensionDigits = 15;

		// Token: 0x040002E3 RID: 739
		internal const int MaxNameDigits = 75;

		// Token: 0x040002E4 RID: 740
		internal const string TrunkStateIdle = "Idle";

		// Token: 0x040002E5 RID: 741
		internal const string TrunkStateConnected = "Connected";

		// Token: 0x040002E6 RID: 742
		internal const string TrunkStateRemoteDisconnected = "RemoteDisconnected";

		// Token: 0x040002E7 RID: 743
		internal const string VoiceMailHeaderFileExtension = ".txt";

		// Token: 0x040002E8 RID: 744
		internal const string XSOMessageFileExtension = ".msg";

		// Token: 0x040002E9 RID: 745
		internal const int CbConversationHeaderBlock = 22;

		// Token: 0x040002EA RID: 746
		internal const string GrammarLogFileName = "UMSpeechGrammar.log";

		// Token: 0x040002EB RID: 747
		internal const int MrasErrorRetryInterval = 10;

		// Token: 0x040002EC RID: 748
		internal const string EnabledForAsr = "EnabledForAsr";

		// Token: 0x040002ED RID: 749
		internal const string EnabledForTranscription = "EnabledForTranscription";

		// Token: 0x040002EE RID: 750
		internal const string TranscriptionHignConfidence = "TranscriptionHignConfidence";

		// Token: 0x040002EF RID: 751
		internal const string TranscriptionLowConfidence = "TranscriptionLowConfidence";

		// Token: 0x040002F0 RID: 752
		internal const string TtsVolume = "TtsVolume";

		// Token: 0x040002F1 RID: 753
		internal const string SmartReadingHours = "SmartReadingHours";

		// Token: 0x040002F2 RID: 754
		internal const int MaxConcurrentPlayOnPhoneCalls = 2;

		// Token: 0x040002F3 RID: 755
		public const string CarriageReturnNewLine = "\r\n";

		// Token: 0x040002F4 RID: 756
		public const string PipelineGuidStringForUserWithNoMailbox = "af360a7e-e6d4-494a-ac69-6ae14896d16b";

		// Token: 0x040002F5 RID: 757
		public const string PipelineGuidRecipientStringForUserWithNoMailbox = "455e5330-ce1f-48d1-b6b1-2e318d2ff2c4";

		// Token: 0x040002F6 RID: 758
		public const string PipelineGuidStringForTransportServers = "70cb6c39-83d9-4123-8013-d95aadda7712";

		// Token: 0x040002F7 RID: 759
		internal static readonly byte StarByte = Encoding.ASCII.GetBytes("*")[0];

		// Token: 0x040002F8 RID: 760
		internal static readonly byte PoundByte = Encoding.ASCII.GetBytes("#")[0];

		// Token: 0x040002F9 RID: 761
		internal static readonly TimeSpan GalGrammarFetchTimeout = TimeSpan.FromSeconds(10.0);

		// Token: 0x040002FA RID: 762
		internal static readonly TimeSpan DLGramamrFetchTimeout = TimeSpan.FromSeconds(5.0);

		// Token: 0x040002FB RID: 763
		internal static readonly TimeSpan RemoveDTMFTime = TimeSpan.FromMilliseconds(300.0);

		// Token: 0x040002FC RID: 764
		internal static readonly TimeSpan TimeZoneErrorSpan = TimeSpan.FromMinutes(7.0);

		// Token: 0x040002FD RID: 765
		internal static readonly char[] CDOCalendarSeparator = "*~*~*~*~*~*~*~*~*~*".ToCharArray();

		// Token: 0x040002FE RID: 766
		internal static readonly TimeSpan DtmfEndSilenceTimeout = TimeSpan.FromMilliseconds(500.0);

		// Token: 0x040002FF RID: 767
		internal static readonly TimeSpan DtmfEndSilenceTimeoutDiagnostic = TimeSpan.FromSeconds(3.0);

		// Token: 0x04000300 RID: 768
		internal static readonly TimeSpan UpdateCurrentCallsTimerInterval = TimeSpan.FromSeconds(1.0);

		// Token: 0x04000301 RID: 769
		internal static readonly TimeSpan SeekTime = TimeSpan.FromSeconds(5.0);

		// Token: 0x04000302 RID: 770
		internal static readonly TimeSpan HeavyBlockingOperationDelay = TimeSpan.FromSeconds(2.0);

		// Token: 0x04000303 RID: 771
		internal static readonly TimeSpan ADNotificationsRetryTime = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000304 RID: 772
		internal static readonly TimeSpan DisableContactResolutionMaxTime = TimeSpan.FromMinutes(2.0);

		// Token: 0x04000305 RID: 773
		internal static readonly ContentType ContentTypeSourceParty = new ContentType("text/source-party");

		// Token: 0x04000306 RID: 774
		internal static readonly Regex DiversionRegex = new Regex("<(tel|sip):(?<number>[^@\\s]+)(@.*)?>(.*;reason=(?<reason>[^;\\s]+))?");

		// Token: 0x04000307 RID: 775
		internal static readonly TimeSpan CallInfoExpirationTime = TimeSpan.FromSeconds(300.0);

		// Token: 0x020000A4 RID: 164
		internal abstract class RegularExpressions
		{
			// Token: 0x0600064F RID: 1615 RVA: 0x0001AAB6 File Offset: 0x00018CB6
			private RegularExpressions()
			{
			}

			// Token: 0x06000650 RID: 1616 RVA: 0x0001AAC0 File Offset: 0x00018CC0
			private static Regex BuildTextNormalizer()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(?<basicURL>https?://[^\\s?]+)[^\\s]*");
				stringBuilder.Append("|");
				stringBuilder.Append("(?<nuanceHack>[^\\s\\d\\w\\(\\)\\?\\.\\!\\{\\}\\<\\>]{2,}\\.)");
				return new Regex(stringBuilder.ToString(), RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
			}

			// Token: 0x06000651 RID: 1617 RVA: 0x0001AB08 File Offset: 0x00018D08
			private static Regex BuildEmailNormalizer()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(?<basicURL>https?://[^\\s?]+)[^\\s]*");
				stringBuilder.Append("|");
				stringBuilder.Append("(?<cidURL>\\[cid:[^\\]]{1,256}\\])");
				stringBuilder.Append("|");
				stringBuilder.Append("(?<nuanceHack>[^\\s\\d\\w\\(\\)\\?\\.\\!\\{\\}\\<\\>]{2,}\\.)");
				string format = string.Empty + "(?<fromHeader>[\\r\\n]{{1,2}}\\s*{0}[^\\r\\n]*[\\r\\n]{{1,2}})(?:\\s*{1}[^\\r\\n]*[\\r\\n]{{1,2}})(?:[^\\r\\n:]{{1,50}}:[^\\r\\n]*[\\r\\n]{{1,2}}){{1,8}}[\\r\\n]{{1,2}}";
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				foreach (CultureInfo formatProvider in UmCultures.GetSupportedClientCultures())
				{
					string text = string.Format(CultureInfo.InvariantCulture, format, new object[]
					{
						Strings.FromHeader.ToString(formatProvider),
						Strings.SentHeader.ToString(formatProvider)
					});
					if (!dictionary.ContainsKey(text))
					{
						stringBuilder.Append("|").Append(text);
						dictionary[text] = null;
					}
				}
				return new Regex(stringBuilder.ToString(), RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
			}

			// Token: 0x04000308 RID: 776
			internal const string RoomLocationMatch = "(?<building>\\d\\d+)/(?<room>\\d\\d\\d\\d+)";

			// Token: 0x04000309 RID: 777
			internal const string RoomLocationReplacement = "${building} / ${room}";

			// Token: 0x0400030A RID: 778
			internal const string FromHeaderGroupName = "fromHeader";

			// Token: 0x0400030B RID: 779
			internal const string BasicURLGroupName = "basicURL";

			// Token: 0x0400030C RID: 780
			internal const string CidURLGroupName = "cidURL";

			// Token: 0x0400030D RID: 781
			internal const string NuanceHackGroupName = "nuanceHack";

			// Token: 0x0400030E RID: 782
			internal static readonly Regex ValidNumberRegex = new Regex("^\\d+$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

			// Token: 0x0400030F RID: 783
			internal static readonly Regex ValidDigitRegex = new Regex("^\\d$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

			// Token: 0x04000310 RID: 784
			internal static readonly Regex TextNormalizer = Constants.RegularExpressions.BuildTextNormalizer();

			// Token: 0x04000311 RID: 785
			internal static readonly Regex EmailNormalizer = Constants.RegularExpressions.BuildEmailNormalizer();
		}

		// Token: 0x020000A5 RID: 165
		internal abstract class DirectorySearch
		{
			// Token: 0x06000653 RID: 1619 RVA: 0x0001AC5E File Offset: 0x00018E5E
			private DirectorySearch()
			{
			}

			// Token: 0x04000312 RID: 786
			internal static readonly int MaxResultsToPreprocess = 100;

			// Token: 0x04000313 RID: 787
			internal static readonly int MaxResultsToDisplay = 9;

			// Token: 0x04000314 RID: 788
			internal static readonly int MaxPersonalContacts = 5000;
		}

		// Token: 0x020000A6 RID: 166
		internal abstract class SpeechMenu
		{
			// Token: 0x04000315 RID: 789
			internal const int DefaultBabbleSeconds = 10;

			// Token: 0x04000316 RID: 790
			internal const int DefaultEndSilenceSeconds = 10;

			// Token: 0x04000317 RID: 791
			internal const float DefaultConfidence = 0.25f;

			// Token: 0x04000318 RID: 792
			internal static readonly byte DtmfFallbackKey = Encoding.ASCII.GetBytes("0")[0];
		}

		// Token: 0x020000A7 RID: 167
		internal abstract class HeaderFile
		{
			// Token: 0x04000319 RID: 793
			internal const string CallId = "CallId";

			// Token: 0x0400031A RID: 794
			internal const string CallerId = "CallerId";

			// Token: 0x0400031B RID: 795
			internal const string ContactInfo = "ContactInfo";

			// Token: 0x0400031C RID: 796
			internal const string AttachmentPath = "AttachmentPath";

			// Token: 0x0400031D RID: 797
			internal const string AttachmentName = "AttachmentName";

			// Token: 0x0400031E RID: 798
			internal const string Duration = "Duration";

			// Token: 0x0400031F RID: 799
			internal const string ProcessedCount = "ProcessedCount";

			// Token: 0x04000320 RID: 800
			internal const string MessageID = "MessageID";

			// Token: 0x04000321 RID: 801
			internal const string SentTime = "SentTime";

			// Token: 0x04000322 RID: 802
			internal const string NumberOfPages = "NumberOfPages";

			// Token: 0x04000323 RID: 803
			internal const string SenderAddress = "SenderAddress";

			// Token: 0x04000324 RID: 804
			internal const string RecipientName = "RecipientName";

			// Token: 0x04000325 RID: 805
			internal const string RecipientObjectGuid = "RecipientObjectGuid";

			// Token: 0x04000326 RID: 806
			internal const string SenderObjectGuid = "SenderObjectGuid";

			// Token: 0x04000327 RID: 807
			internal const string MessageFilePath = "MessageFilePath";

			// Token: 0x04000328 RID: 808
			internal const string CultureInfo = "CultureInfo";

			// Token: 0x04000329 RID: 809
			internal const string CallerName = "CallerNAme";

			// Token: 0x0400032A RID: 810
			internal const string CallerIdDisplayName = "CallerIdDisplayName";

			// Token: 0x0400032B RID: 811
			internal const string CallerAddress = "CallerAddress";

			// Token: 0x0400032C RID: 812
			internal const string Important = "Important";

			// Token: 0x0400032D RID: 813
			internal const string Private = "Private";

			// Token: 0x0400032E RID: 814
			internal const string PureInterpersonalMessage = "PureInterpersonalMessage";

			// Token: 0x0400032F RID: 815
			internal const string ProtectedReply = "ProtectedReply";

			// Token: 0x04000330 RID: 816
			internal const string CallAnswering = "CallAnswering";

			// Token: 0x04000331 RID: 817
			internal const string Incomplete = "InComplete";

			// Token: 0x04000332 RID: 818
			internal const string MessageType = "MessageType";

			// Token: 0x04000333 RID: 819
			internal const string SMTPVoiceMailType = "SMTPVoiceMail";

			// Token: 0x04000334 RID: 820
			internal const string XSOVoiceMailType = "XSOVoiceMail";

			// Token: 0x04000335 RID: 821
			internal const string CDRMessageType = "CDR";

			// Token: 0x04000336 RID: 822
			internal const string HealthCheckType = "HealthCheck";

			// Token: 0x04000337 RID: 823
			internal const string CDRData = "CDRData";

			// Token: 0x04000338 RID: 824
			internal const string FaxType = "Fax";

			// Token: 0x04000339 RID: 825
			internal const string Codec = "Codec";

			// Token: 0x0400033A RID: 826
			internal const string MissedCallType = "MissedCall";

			// Token: 0x0400033B RID: 827
			internal const string IncomingCallLogType = "IncomingCallLog";

			// Token: 0x0400033C RID: 828
			internal const string OutgoingCallLogType = "OutgoingCallLog";

			// Token: 0x0400033D RID: 829
			internal const string PartnerTranscriptionRequestType = "PartnerTranscriptionRequest";

			// Token: 0x0400033E RID: 830
			internal const string PartnerTranscriptionContext = "PartnerTranscriptionContext";

			// Token: 0x0400033F RID: 831
			internal const string TranscriptionData = "TranscriptionData";

			// Token: 0x04000340 RID: 832
			internal const string Priority = "Priority";

			// Token: 0x04000341 RID: 833
			internal const string Subject = "Subject";

			// Token: 0x04000342 RID: 834
			internal const string HeaderSeperator = " : ";

			// Token: 0x04000343 RID: 835
			internal const string OCSNotificationType = "OCSNotification";

			// Token: 0x04000344 RID: 836
			internal const string OCSNotificationData = "OCSNotificationData";

			// Token: 0x04000345 RID: 837
			internal const string TenantGuid = "TenantGuid";
		}

		// Token: 0x020000A8 RID: 168
		internal abstract class SipUriParameters
		{
			// Token: 0x04000346 RID: 838
			internal const string E12Referrer = "Referrer";

			// Token: 0x04000347 RID: 839
			internal const string Referrer = "referrer";

			// Token: 0x04000348 RID: 840
			internal const string E12Extension = "Extension";

			// Token: 0x04000349 RID: 841
			internal const string Extension = "extension";

			// Token: 0x0400034A RID: 842
			internal const string PhoneContext = "phone-context";

			// Token: 0x0400034B RID: 843
			internal const string Version = "v";

			// Token: 0x0400034C RID: 844
			internal const string Command = "c";

			// Token: 0x0400034D RID: 845
			internal const string FaxRecipient = "msExchUMFaxRecipient";

			// Token: 0x0400034E RID: 846
			internal const string UMContext = "msExchUMContext";
		}

		// Token: 0x020000A9 RID: 169
		internal abstract class SmtpHeaders
		{
			// Token: 0x0400034F RID: 847
			internal const string Precedence = "Precedence";

			// Token: 0x04000350 RID: 848
			internal const string MimeVersion = "Mime-Version";
		}

		// Token: 0x020000AA RID: 170
		internal abstract class XHeaders
		{
			// Token: 0x04000351 RID: 849
			internal const string ContentClass = "X-ContentClass";

			// Token: 0x04000352 RID: 850
			internal const string CallingTelephoneNumber = "X-CallingTelephoneNumber";

			// Token: 0x04000353 RID: 851
			internal const string VoiceMessageDuration = "X-VoiceMessageDuration";

			// Token: 0x04000354 RID: 852
			internal const string VoiceMessageSenderName = "X-VoiceMessageSenderName";

			// Token: 0x04000355 RID: 853
			internal const string AttachmentOrder = "X-AttachmentOrder";

			// Token: 0x04000356 RID: 854
			internal const string FaxNumberOfPages = "X-FaxNumberOfPages";

			// Token: 0x04000357 RID: 855
			internal const string CallId = "X-CallID";

			// Token: 0x04000358 RID: 856
			internal const string ExchOrganizationSCL = "X-MS-Exchange-Organization-SCL";
		}

		// Token: 0x020000AB RID: 171
		internal abstract class Transcription
		{
			// Token: 0x020000AC RID: 172
			internal abstract class Grammars
			{
				// Token: 0x04000359 RID: 857
				internal const string LMGrammarSubDirTemplate = "Common Files\\microsoft shared\\Speech\\Tokens\\SR_MS_{0}_TRANS_11.0\\Grammars";

				// Token: 0x0400035A RID: 858
				internal const string LMGrammarName = "TSR-LM.cfp";

				// Token: 0x0400035B RID: 859
				internal const string RootSemanticTagKey = "Fragments";

				// Token: 0x0400035C RID: 860
				internal const string CustomGrammarPrefix = "<?xml version=\"1.0\"?>\r\n<grammar xml:lang=\"{0}\" version=\"1.0\" xmlns=\"http://www.w3.org/2001/06/grammar\" tag-format=\"semantics/1.0\">\r\n<tag>out.customGrammarWords=false;out.topNWords=false;</tag>\r\n    <rule id=\"{1}\" scope=\"public\">\r\n        <one-of>";

				// Token: 0x0400035D RID: 861
				internal const string CustomGrammarSuffix = "      \r\n        </one-of>\r\n    </rule>\r\n</grammar>";

				// Token: 0x0400035E RID: 862
				internal const string SetValueSemanticTagFormat = "out.{0} = \"{1}\";";

				// Token: 0x0400035F RID: 863
				internal const string CustomTopNFileName = "ExtTopN.grxml";

				// Token: 0x04000360 RID: 864
				internal const string CustomCallerInfoFileName = "ExtCallerInfo.grxml";

				// Token: 0x04000361 RID: 865
				internal const string CustomCallerInfoRule = "ExtCallerInfo";

				// Token: 0x04000362 RID: 866
				internal const string CustomPersonNameFileName = "ExtPersonName.grxml";

				// Token: 0x04000363 RID: 867
				internal const string CustomTopNRule = "ExtTopN";

				// Token: 0x04000364 RID: 868
				internal const string CustomPersonNameRule = "ExtPersonName";

				// Token: 0x04000365 RID: 869
				internal const string CustomPhoneNumberFileName = "ExtPhoneNumber.grxml";

				// Token: 0x04000366 RID: 870
				internal const string CustomPhoneNumberRule = "ExtPhoneNumber";

				// Token: 0x04000367 RID: 871
				internal const string CustomGenAppFileName = "ExtGenAppRule.grxml";

				// Token: 0x04000368 RID: 872
				internal const string CustomGenAppRule = "ExtGenAppRule";

				// Token: 0x04000369 RID: 873
				internal const string OuterItemFormat = "\r\n            <item >{0}\r\n            </item>";

				// Token: 0x0400036A RID: 874
				internal const string OuterItemFormatWeight = "\r\n            <item weight='{0}'>{1}\r\n            </item>";

				// Token: 0x0400036B RID: 875
				internal const string SemanticTagItemFormat = "\r\n                <item>{0}</item>\r\n                <tag>{1}</tag>";

				// Token: 0x0400036C RID: 876
				internal const string NoSemanticTagItemFormat = "\r\n                <item>{0}</item>";

				// Token: 0x0400036D RID: 877
				internal const string TagSetValueSemanticTagFormat = "\r\n                <tag>out.{0} = {1};</tag>";

				// Token: 0x0400036E RID: 878
				internal const string RuleRefUri = "\r\n                <ruleref uri=\"{0}#{1}\"/>";

				// Token: 0x0400036F RID: 879
				internal const string RuleRefUriWithHoist = "\r\n                <ruleref uri=\"{0}#{1}\"/>\r\n                <tag>out=rules.latest();</tag>";

				// Token: 0x04000370 RID: 880
				internal const string ScgPersonNameSemanticItemName = "PersonName";

				// Token: 0x04000371 RID: 881
				internal const string ScgPhoneNumberSemanticItemName = "PhoneNumber";

				// Token: 0x04000372 RID: 882
				internal const string ScgDateSemanticItemName = "Date";

				// Token: 0x04000373 RID: 883
				internal const string ScgTimeSemanticItemName = "Time";

				// Token: 0x04000374 RID: 884
				internal const string MailboxSemanticItem = "Mailbox";

				// Token: 0x04000375 RID: 885
				internal const string ContactSemanticItem = "Contact";

				// Token: 0x04000376 RID: 886
				internal const string PhoneNumberSemanticItem = "PhoneNumber";

				// Token: 0x04000377 RID: 887
				internal const string DateSemanticItem = "Date";

				// Token: 0x04000378 RID: 888
				internal const string TimeSemanticItem = "Time";

				// Token: 0x04000379 RID: 889
				internal const string PersonNameSemanticItem = "PersonName";

				// Token: 0x0400037A RID: 890
				internal const string AreaCodeSemanticItem = "AreaCode";

				// Token: 0x0400037B RID: 891
				internal const string LocalNumberSemanticItem = "LocalNumber";

				// Token: 0x0400037C RID: 892
				internal const string ExtensionSemanticItem = "Extension";

				// Token: 0x0400037D RID: 893
				internal const string HourSemanticItem = "Hour";

				// Token: 0x0400037E RID: 894
				internal const string MinuteSemanticItem = "Minute";

				// Token: 0x0400037F RID: 895
				internal const string IsValidDateSemanticItem = "IsValidDate";

				// Token: 0x04000380 RID: 896
				internal const string DaySemanticItem = "Day";

				// Token: 0x04000381 RID: 897
				internal const string MonthSemanticItem = "Month";

				// Token: 0x04000382 RID: 898
				internal const string YearSemanticItem = "Year";

				// Token: 0x04000383 RID: 899
				internal const string AttributesSemanticItem = "_attributes";

				// Token: 0x04000384 RID: 900
				internal const string AttributesNameSemanticItem = "name";

				// Token: 0x04000385 RID: 901
				internal const string AttributesFirstWordIndexSemanticItem = "FirstWordIndex";

				// Token: 0x04000386 RID: 902
				internal const string AttributesCountOfWordsSemanticItem = "CountOfWords";

				// Token: 0x04000387 RID: 903
				internal const string AttributesTextSemanticItem = "text";

				// Token: 0x04000388 RID: 904
				internal const string CustomGrammarWordsSemanticItem = "customGrammarWords";

				// Token: 0x04000389 RID: 905
				internal const string TopNWordsSemanticItem = "topNWords";
			}
		}

		// Token: 0x020000AD RID: 173
		internal abstract class OCS
		{
			// Token: 0x0400038A RID: 906
			internal const string Time = "Time";

			// Token: 0x0400038B RID: 907
			internal const string User = "User";

			// Token: 0x0400038C RID: 908
			internal const string From = "From";

			// Token: 0x0400038D RID: 909
			internal const string Event = "Event";

			// Token: 0x0400038E RID: 910
			internal const string Target = "Target";

			// Token: 0x0400038F RID: 911
			internal const string CallId = "CallId";

			// Token: 0x04000390 RID: 912
			internal const string Subject = "Subject";

			// Token: 0x04000391 RID: 913
			internal const string Priority = "Priority";

			// Token: 0x04000392 RID: 914
			internal const string Template = "Template";

			// Token: 0x04000393 RID: 915
			internal const string ReferredBy = "ReferredBy";

			// Token: 0x04000394 RID: 916
			internal const string TargetClass = "TargetClass";

			// Token: 0x04000395 RID: 917
			internal const string MissedReason = "MissedReason";

			// Token: 0x04000396 RID: 918
			internal const string ConversationId = "ConversationID";

			// Token: 0x04000397 RID: 919
			internal const string EumProxyAddress = "EumProxyAddress";

			// Token: 0x04000398 RID: 920
			internal const string RecipientObjectGuid = "RecipientObjectGuid";

			// Token: 0x04000399 RID: 921
			internal const string TenantGuid = "TenantGuid";

			// Token: 0x0400039A RID: 922
			internal const string UserNotification = "UserNotification";

			// Token: 0x0400039B RID: 923
			internal const string Type = "type";

			// Token: 0x0400039C RID: 924
			internal const string SkipPin = "skip-pin";

			// Token: 0x0400039D RID: 925
			internal const string MsExchangeCommand = "Ms-Exchange-Command";

			// Token: 0x0400039E RID: 926
			internal const string PrivateNoDiversion = "private-no-diversion";

			// Token: 0x0400039F RID: 927
			internal const string MsSensitivityHeader = "Ms-Sensitivity";

			// Token: 0x040003A0 RID: 928
			internal const string PAssertedIdentityHeader = "P-Asserted-Identity";

			// Token: 0x040003A1 RID: 929
			internal const string MsTargetClass = "Ms-Target-Class";

			// Token: 0x040003A2 RID: 930
			internal const string SecondaryTargetClass = "secondary";

			// Token: 0x040003A3 RID: 931
			internal const string OpaqueParameter = "opaque";

			// Token: 0x040003A4 RID: 932
			internal const string OpaqueAppVoicemailPrefix = "app:voicemail";

			// Token: 0x040003A5 RID: 933
			internal const string LocalResourcePathParam = "local-resource-path";

			// Token: 0x040003A6 RID: 934
			internal const string ItemIdParamPrefix = "itemId=";

			// Token: 0x040003A7 RID: 935
			internal const string Urgent = "urgent";

			// Token: 0x040003A8 RID: 936
			internal const string Normal = "normal";

			// Token: 0x040003A9 RID: 937
			internal const string Emergency = "emergency";

			// Token: 0x040003AA RID: 938
			internal const string NonUrgent = "non-urgent";

			// Token: 0x040003AB RID: 939
			internal const string FromHeader = "FROM";

			// Token: 0x040003AC RID: 940
			internal const string RTCOpaqueParam = "opaque=app:rtcevent";

			// Token: 0x040003AD RID: 941
			internal const string RTCNotificationSender = "sip:A410AA79-D874-4e56-9B46-709BDD0EB850";

			// Token: 0x040003AE RID: 942
			internal const string SDPMediaDescriptionName = "application";

			// Token: 0x040003AF RID: 943
			internal static readonly ContentType SDPContentType = new ContentType("application/sdp");

			// Token: 0x040003B0 RID: 944
			internal static readonly TimeSpan SessionMaxIdleTime = TimeSpan.FromMinutes(12.0);

			// Token: 0x040003B1 RID: 945
			internal static readonly TimeSpan SessionCheckTimerInterval = TimeSpan.FromMinutes(1.0);

			// Token: 0x020000AE RID: 174
			internal abstract class SDPAttributes
			{
				// Token: 0x040003B2 RID: 946
				internal const string SendOnly = "sendonly";

				// Token: 0x040003B3 RID: 947
				internal const string ReceiveOnly = "recvonly";

				// Token: 0x040003B4 RID: 948
				internal const string AcceptTypes = "accept-types";

				// Token: 0x040003B5 RID: 949
				internal const string AcceptTemplates = "ms-rtc-accept-eventtemplates";

				// Token: 0x040003B6 RID: 950
				internal const string UserNotification = "application/ms-rtc-usernotification+xml";

				// Token: 0x040003B7 RID: 951
				internal const string RtcDefaultTemplate = "RtcDefault";
			}
		}

		// Token: 0x020000AF RID: 175
		internal abstract class MowaGrammar
		{
			// Token: 0x040003B8 RID: 952
			internal const string EmailPeopleKeywordGrammarId = "grEmailPersonByNameMobile";

			// Token: 0x040003B9 RID: 953
			internal const string FindPeopleKeywordGrammarId = "grFindPersonByNameMobile";

			// Token: 0x040003BA RID: 954
			internal const string AppointmentCreationKeywordGrammarId = "grCalendarDayNewAppointment";

			// Token: 0x040003BB RID: 955
			internal const string DaySearchKeywordGrammarId = "grCalendarDaySearch";
		}

		// Token: 0x020000B0 RID: 176
		internal abstract class Xml
		{
			// Token: 0x06000661 RID: 1633 RVA: 0x0001AD1F File Offset: 0x00018F1F
			private Xml()
			{
			}

			// Token: 0x040003BC RID: 956
			internal const string Menu = "Menu";

			// Token: 0x040003BD RID: 957
			internal const string SpeechMenu = "SpeechMenu";

			// Token: 0x040003BE RID: 958
			internal const string PlayBackMenu = "PlayBackMenu";

			// Token: 0x040003BF RID: 959
			internal const string Transition = "Transition";

			// Token: 0x040003C0 RID: 960
			internal const string Record = "Record";

			// Token: 0x040003C1 RID: 961
			internal const string Prompt = "Prompt";

			// Token: 0x040003C2 RID: 962
			internal const string PromptGroup = "PromptGroup";

			// Token: 0x040003C3 RID: 963
			internal const string MessageCount = "MessageCount";

			// Token: 0x040003C4 RID: 964
			internal const string FaxRequest = "FaxRequest";

			// Token: 0x040003C5 RID: 965
			internal const string CallTransfer = "CallTransfer";

			// Token: 0x040003C6 RID: 966
			internal const string ConditionNode = "Condition";

			// Token: 0x040003C7 RID: 967
			internal const string Grammars = "Grammars";

			// Token: 0x040003C8 RID: 968
			internal const string Transitions = "Transitions";

			// Token: 0x040003C9 RID: 969
			internal const string Grammar = "Grammar";

			// Token: 0x040003CA RID: 970
			internal const string Main = "Main";

			// Token: 0x040003CB RID: 971
			internal const string Help = "Help";

			// Token: 0x040003CC RID: 972
			internal const string Mumble1 = "Mumble1";

			// Token: 0x040003CD RID: 973
			internal const string Mumble2 = "Mumble2";

			// Token: 0x040003CE RID: 974
			internal const string Silence1 = "Silence1";

			// Token: 0x040003CF RID: 975
			internal const string Silence2 = "Silence2";

			// Token: 0x040003D0 RID: 976
			internal const string SpeechError = "SpeechError";

			// Token: 0x040003D1 RID: 977
			internal const string Repeat = "Repeat";

			// Token: 0x040003D2 RID: 978
			internal const string InvalidCommand = "InvalidCommand";

			// Token: 0x040003D3 RID: 979
			internal const string Tag = "Tag";

			// Token: 0x040003D4 RID: 980
			internal const string FsmImport = "FsmImport";

			// Token: 0x040003D5 RID: 981
			internal const string FsmModule = "FsmModule";

			// Token: 0x040003D6 RID: 982
			internal const string FiniteStateMachine = "FiniteStateMachine";

			// Token: 0x040003D7 RID: 983
			internal const string GlobalManager = "GlobalActivityManager";

			// Token: 0x040003D8 RID: 984
			internal const string SingularPlural = "singularPlural";

			// Token: 0x040003D9 RID: 985
			internal const string Event = "event";

			// Token: 0x040003DA RID: 986
			internal const string RefId = "refId";

			// Token: 0x040003DB RID: 987
			internal const string RefInfo = "refInfo";

			// Token: 0x040003DC RID: 988
			internal const string Action = "action";

			// Token: 0x040003DD RID: 989
			internal const string HeavyAction = "heavyaction";

			// Token: 0x040003DE RID: 990
			internal const string PlaybackAction = "playbackAction";

			// Token: 0x040003DF RID: 991
			internal const string Id = "id";

			// Token: 0x040003E0 RID: 992
			internal const string MaxDtmfSize = "maxDtmfSize";

			// Token: 0x040003E1 RID: 993
			internal const string MinDtmfSize = "minDtmfSize";

			// Token: 0x040003E2 RID: 994
			internal const string MaxNumericInput = "maxNumericInput";

			// Token: 0x040003E3 RID: 995
			internal const string MinNumericInput = "minNumericInput";

			// Token: 0x040003E4 RID: 996
			internal const string DtmfInputValue = "dtmfInputValue";

			// Token: 0x040003E5 RID: 997
			internal const string Type = "type";

			// Token: 0x040003E6 RID: 998
			internal const string Name = "name";

			// Token: 0x040003E7 RID: 999
			internal const string FirstActivityId = "firstActivityId";

			// Token: 0x040003E8 RID: 1000
			internal const string DtmfStopTones = "dtmfStopTones";

			// Token: 0x040003E9 RID: 1001
			internal const string Condition = "condition";

			// Token: 0x040003EA RID: 1002
			internal const string InterDigitTimeout = "interDigitTimeout";

			// Token: 0x040003EB RID: 1003
			internal const string InputTimeout = "inputTimeout";

			// Token: 0x040003EC RID: 1004
			internal const string InitialSilenceTimeout = "initialSilenceTimeout";

			// Token: 0x040003ED RID: 1005
			internal const string Value = "value";

			// Token: 0x040003EE RID: 1006
			internal const string PhoneNumber = "number";

			// Token: 0x040003EF RID: 1007
			internal const string PhoneNumberType = "numberType";

			// Token: 0x040003F0 RID: 1008
			internal const string Message = "message";

			// Token: 0x040003F1 RID: 1009
			internal const string Greeting = "greeting";

			// Token: 0x040003F2 RID: 1010
			internal const string SearchPurpose = "searchPurpose";

			// Token: 0x040003F3 RID: 1011
			internal const string Suffix = "suffix";

			// Token: 0x040003F4 RID: 1012
			internal const string Culture = "culture";

			// Token: 0x040003F5 RID: 1013
			internal const string Rule = "rule";

			// Token: 0x040003F6 RID: 1014
			internal const string BabbleSeconds = "babbleSeconds";

			// Token: 0x040003F7 RID: 1015
			internal const string EndSilenceSeconds = "endSilenceSeconds";

			// Token: 0x040003F8 RID: 1016
			internal const string Confidence = "confidence";

			// Token: 0x040003F9 RID: 1017
			internal const string Scope = "scope";

			// Token: 0x040003FA RID: 1018
			internal const string Uninterruptible = "uninterruptible";

			// Token: 0x040003FB RID: 1019
			internal const string StopPromptOnBargeIn = "stopPromptOnBargeIn";

			// Token: 0x040003FC RID: 1020
			internal const string KeepDtmfOnNoMatch = "keepDtmfOnNoMatch";

			// Token: 0x040003FD RID: 1021
			internal const string Href = "href";

			// Token: 0x040003FE RID: 1022
			internal const string Module = "module";

			// Token: 0x040003FF RID: 1023
			internal const string ProsodyRate = "prosodyRate";

			// Token: 0x04000400 RID: 1024
			internal const string Language = "language";

			// Token: 0x04000401 RID: 1025
			internal const string LimitKey = "limitKey";

			// Token: 0x04000402 RID: 1026
			internal const string Volume = "volume";

			// Token: 0x04000403 RID: 1027
			internal const string Public = "public";

			// Token: 0x04000404 RID: 1028
			internal const string User = "user";

			// Token: 0x020000B1 RID: 177
			internal abstract class GlobCfg
			{
				// Token: 0x04000405 RID: 1029
				internal const string SingularPlural = "singularPlural";

				// Token: 0x04000406 RID: 1030
				internal const string Singular = "_Singular";

				// Token: 0x04000407 RID: 1031
				internal const string Plural = "_Plural";

				// Token: 0x04000408 RID: 1032
				internal const string Plural2 = "_Plural2";

				// Token: 0x04000409 RID: 1033
				internal const string StartTime = "startTime";

				// Token: 0x0400040A RID: 1034
				internal const string EndTime = "endTime";

				// Token: 0x0400040B RID: 1035
				internal const string StartDay = "startDay";

				// Token: 0x0400040C RID: 1036
				internal const string EndDay = "endDay";

				// Token: 0x0400040D RID: 1037
				internal const string StartDayTime = "startDayTime";

				// Token: 0x0400040E RID: 1038
				internal const string EndDayTime = "endDayTime";

				// Token: 0x0400040F RID: 1039
				internal const string BusinessAddress = "businessAddress";

				// Token: 0x04000410 RID: 1040
				internal const string BusinessSchedule = "businessSchedule";

				// Token: 0x04000411 RID: 1041
				internal const string UserName = "userName";

				// Token: 0x04000412 RID: 1042
				internal const string CustomGreeting = "customGreeting";

				// Token: 0x04000413 RID: 1043
				internal const string CustomPrompt = "customPrompt";

				// Token: 0x04000414 RID: 1044
				internal const string CustomMenu = "customMenu";

				// Token: 0x04000415 RID: 1045
				internal const string DepartmentName = "departmentName";

				// Token: 0x04000416 RID: 1046
				internal const string SelectedMenu = "selectedMenu";

				// Token: 0x04000417 RID: 1047
				internal const string BusinessName = "businessName";

				// Token: 0x04000418 RID: 1048
				internal const string VarTimeZone = "varTimeZone";

				// Token: 0x04000419 RID: 1049
				internal const string VarScheduleGroupList = "varScheduleGroupList";

				// Token: 0x0400041A RID: 1050
				internal const string VarScheduleIntervalList = "varScheduleIntervalList";

				// Token: 0x0400041B RID: 1051
				internal const string AAContext = "AAContext";

				// Token: 0x0400041C RID: 1052
				internal const string AALocationContext = "aaLocationContext";
			}
		}

		// Token: 0x020000B2 RID: 178
		internal abstract class VariableName
		{
			// Token: 0x0400041D RID: 1053
			internal const string UseAsr = "useAsr";

			// Token: 0x0400041E RID: 1054
			internal const string CurrentActivity = "currentActivity";

			// Token: 0x0400041F RID: 1055
			internal const string LastActivity = "lastActivity";

			// Token: 0x04000420 RID: 1056
			internal const string LastRecoEvent = "lastRecoEvent";

			// Token: 0x04000421 RID: 1057
			internal const string SavedRecoEvent = "savedRecoEvent";

			// Token: 0x04000422 RID: 1058
			internal const string LastInput = "lastInput";

			// Token: 0x04000423 RID: 1059
			internal const string Greeting = "greeting";

			// Token: 0x04000424 RID: 1060
			internal const string Recording = "recording";

			// Token: 0x04000425 RID: 1061
			internal const string UserName = "userName";

			// Token: 0x04000426 RID: 1062
			internal const string DefaultLanguage = "defaultLanguage";

			// Token: 0x04000427 RID: 1063
			internal const string SelectableLanguages = "selectableLanguages";

			// Token: 0x04000428 RID: 1064
			internal const string MessageLanguage = "messageLanguage";

			// Token: 0x04000429 RID: 1065
			internal const string LanguageDetected = "languageDetected";

			// Token: 0x0400042A RID: 1066
			internal const string InvalidExtension = "invalidExtension";

			// Token: 0x0400042B RID: 1067
			internal const string AdminMinPwdLen = "adminMinPwdLen";

			// Token: 0x0400042C RID: 1068
			internal const string AdminOldPwdLen = "adminOldPwdLen";

			// Token: 0x0400042D RID: 1069
			internal const string PilotNumberWelcomeGreetingFilename = "pilotNumberWelcomeGreetingFilename";

			// Token: 0x0400042E RID: 1070
			internal const string PilotNumberWelcomeGreetingEnabled = "pilotNumberWelcomeGreetingEnabled";

			// Token: 0x0400042F RID: 1071
			internal const string PilotNumberInfoAnnouncementFilename = "pilotNumberInfoAnnouncementFilename";

			// Token: 0x04000430 RID: 1072
			internal const string PilotNumberInfoAnnouncementEnabled = "pilotNumberInfoAnnouncementEnabled";

			// Token: 0x04000431 RID: 1073
			internal const string PilotNumberInfoAnnouncementInterruptible = "pilotNumberInfoAnnouncementInterruptible";

			// Token: 0x04000432 RID: 1074
			internal const string PilotNumberTransferToOperatorEnabled = "pilotNumberTransferToOperatorEnabled";

			// Token: 0x04000433 RID: 1075
			internal const string TUIPromptEditingEnabled = "tuiPromptEditingEnabled";

			// Token: 0x04000434 RID: 1076
			internal const string ContactSomeoneEnabled = "contactSomeoneEnabled";

			// Token: 0x04000435 RID: 1077
			internal const string Mode = "mode";

			// Token: 0x04000436 RID: 1078
			internal const string OCFeature = "ocFeature";

			// Token: 0x04000437 RID: 1079
			internal const string SkipPinCheck = "skipPinCheck";

			// Token: 0x04000438 RID: 1080
			internal const string DiagnosticDtmfDigits = "diagnosticDtmfDigits";

			// Token: 0x020000B3 RID: 179
			internal abstract class Voicemail
			{
				// Token: 0x04000439 RID: 1081
				internal const string SenderInfo = "senderInfo";

				// Token: 0x0400043A RID: 1082
				internal const string MessageRecievedTime = "messageReceivedTime";

				// Token: 0x0400043B RID: 1083
				internal const string CurrentVoicemailMessage = "currentVoicemailMessage";

				// Token: 0x0400043C RID: 1084
				internal const string NumberOfMessages = "numberOfMessages";

				// Token: 0x0400043D RID: 1085
				internal const string DurationMinutes = "durationMinutes";

				// Token: 0x0400043E RID: 1086
				internal const string DurationSeconds = "durationSeconds";

				// Token: 0x0400043F RID: 1087
				internal const string OperatorNumber = "operatorNumber";
			}

			// Token: 0x020000B4 RID: 180
			internal abstract class Calendar
			{
				// Token: 0x04000440 RID: 1088
				internal const string Remaining = "remaining";

				// Token: 0x04000441 RID: 1089
				internal const string Time = "time";

				// Token: 0x04000442 RID: 1090
				internal const string ConflictTime = "conflictTime";

				// Token: 0x04000443 RID: 1091
				internal const string Subject = "subject";

				// Token: 0x04000444 RID: 1092
				internal const string Location = "location";

				// Token: 0x04000445 RID: 1093
				internal const string NumConflicts = "numConflicts";

				// Token: 0x04000446 RID: 1094
				internal const string NumAccepted = "numAccepted";

				// Token: 0x04000447 RID: 1095
				internal const string NumDeclined = "numDeclined";

				// Token: 0x04000448 RID: 1096
				internal const string NumUndecided = "numUndecided";

				// Token: 0x04000449 RID: 1097
				internal const string OwnerName = "ownerName";

				// Token: 0x0400044A RID: 1098
				internal const string NumAttendees = "numAttendees";

				// Token: 0x0400044B RID: 1099
				internal const string AcceptedList = "acceptedList";

				// Token: 0x0400044C RID: 1100
				internal const string DeclinedList = "declinedList";

				// Token: 0x0400044D RID: 1101
				internal const string UndecidedList = "undecidedList";

				// Token: 0x0400044E RID: 1102
				internal const string AttendeeList = "attendeeList";

				// Token: 0x0400044F RID: 1103
				internal const string CalendarDate = "calendarDate";

				// Token: 0x04000450 RID: 1104
				internal const string MinutesLateMax = "minutesLateMax";

				// Token: 0x04000451 RID: 1105
				internal const string MinutesLateMin = "minutesLateMin";

				// Token: 0x04000452 RID: 1106
				internal const string Current = "current";

				// Token: 0x04000453 RID: 1107
				internal const string DayOfWeek = "dayOfWeek";

				// Token: 0x04000454 RID: 1108
				internal const string DayOffset = "dayOffset";

				// Token: 0x04000455 RID: 1109
				internal const string StartTime = "startTime";

				// Token: 0x04000456 RID: 1110
				internal const string EndTime = "endTime";

				// Token: 0x04000457 RID: 1111
				internal const string ClearTime = "clearTime";

				// Token: 0x04000458 RID: 1112
				internal const string ClearDays = "clearDays";

				// Token: 0x04000459 RID: 1113
				internal const string MeetingTimeRange = "meetingTimeRange";
			}

			// Token: 0x020000B5 RID: 181
			internal abstract class SendMessage
			{
				// Token: 0x0400045A RID: 1114
				internal const string NumRecipients = "numRecipients";
			}

			// Token: 0x020000B6 RID: 182
			internal abstract class MessageCount
			{
				// Token: 0x0400045B RID: 1115
				internal const string HaveSummary = "haveSummary";

				// Token: 0x0400045C RID: 1116
				internal const string NumEmail = "numEmail";

				// Token: 0x0400045D RID: 1117
				internal const string NumEmailMax = "numEmailMax";

				// Token: 0x0400045E RID: 1118
				internal const string NumVoicemail = "numVoicemail";

				// Token: 0x0400045F RID: 1119
				internal const string NumMeetings = "numMeetings";

				// Token: 0x04000460 RID: 1120
				internal const string Location = "location";

				// Token: 0x04000461 RID: 1121
				internal const string StartTime = "startTime";
			}

			// Token: 0x020000B7 RID: 183
			internal abstract class DirectorySearch
			{
				// Token: 0x04000462 RID: 1122
				internal const string PrimarySearchMode = "primarySearchMode";

				// Token: 0x04000463 RID: 1123
				internal const string SecondarySearchMode = "secondarySearchMode";

				// Token: 0x04000464 RID: 1124
				internal const string CurrentSearchMode = "currentSearchMode";

				// Token: 0x04000465 RID: 1125
				internal const string None = "none";

				// Token: 0x04000466 RID: 1126
				internal const string FirstNameLastName = "firstNameLastName";

				// Token: 0x04000467 RID: 1127
				internal const string LastNameFirstName = "lastNameFirstName";

				// Token: 0x04000468 RID: 1128
				internal const string EmailAlias = "emailAlias";

				// Token: 0x04000469 RID: 1129
				internal const string NewSearch = "newSearch";

				// Token: 0x0400046A RID: 1130
				internal const string SpokenNameFromAD = "spokenNameFromAD";

				// Token: 0x0400046B RID: 1131
				internal const string SpokenNameFromTTS = "spokenNameFromTTS";

				// Token: 0x0400046C RID: 1132
				internal const string PromptIndex = "promptindex";

				// Token: 0x0400046D RID: 1133
				internal const string ADSpokenName = "adspokenname";

				// Token: 0x0400046E RID: 1134
				internal const string InvalidSearchSelection = "invalidSearchSelection";

				// Token: 0x0400046F RID: 1135
				internal const string LastDtmfSearchInput = "lastDtmfSearchInput";

				// Token: 0x04000470 RID: 1136
				internal const string NumResults = "numResults";

				// Token: 0x04000471 RID: 1137
				internal const string SearchInput = "searchInput";

				// Token: 0x04000472 RID: 1138
				internal const string Result = "directorySearchResult";

				// Token: 0x04000473 RID: 1139
				internal const string AuthenticatedUser = "authenticatedUser";

				// Token: 0x04000474 RID: 1140
				internal const string SearchTarget = "searchTarget";

				// Token: 0x04000475 RID: 1141
				internal const string InitialSearchTarget = "initialSearchTarget";

				// Token: 0x04000476 RID: 1142
				internal const string PersonalContacts = "personalContacts";

				// Token: 0x04000477 RID: 1143
				internal const string GlobalAddressList = "globalAddressList";

				// Token: 0x04000478 RID: 1144
				internal const string CompanyName = "companyName";

				// Token: 0x04000479 RID: 1145
				internal const string HaveDialableMobileNumber = "haveDialableMobileNumber";

				// Token: 0x0400047A RID: 1146
				internal const string HaveDialableBusinessNumber = "haveDialableBusinessNumber";

				// Token: 0x0400047B RID: 1147
				internal const string HaveDialableHomeNumber = "haveDialableHomeNumber";

				// Token: 0x0400047C RID: 1148
				internal const string Email = "email";

				// Token: 0x0400047D RID: 1149
				internal const string Email1 = "email1";

				// Token: 0x0400047E RID: 1150
				internal const string Email2 = "email2";

				// Token: 0x0400047F RID: 1151
				internal const string Email3 = "email3";

				// Token: 0x04000480 RID: 1152
				internal const string HaveEmail = "haveEmail";

				// Token: 0x04000481 RID: 1153
				internal const string HaveEmail1 = "haveEmail1";

				// Token: 0x04000482 RID: 1154
				internal const string HaveEmail2 = "haveEmail2";

				// Token: 0x04000483 RID: 1155
				internal const string HaveEmail3 = "haveEmail3";

				// Token: 0x04000484 RID: 1156
				internal const string SearchByExtension = "searchByExtension";

				// Token: 0x04000485 RID: 1157
				internal const string CurrentMenu = "currentMenu";

				// Token: 0x04000486 RID: 1158
				internal const string ExactMatches = "exactMatches";

				// Token: 0x04000487 RID: 1159
				internal const string PartialMatches = "partialMatches";

				// Token: 0x04000488 RID: 1160
				internal const string HaveMorePartialMatches = "haveMorePartialMatches";

				// Token: 0x04000489 RID: 1161
				internal const string ExceedRetryLimit = "exceedRetryLimit";
			}

			// Token: 0x020000B8 RID: 184
			internal abstract class AsrSearch
			{
				// Token: 0x0400048A RID: 1162
				internal const string SearchResult = "searchResult";

				// Token: 0x0400048B RID: 1163
				internal const string SearchContext = "searchContext";

				// Token: 0x0400048C RID: 1164
				internal const string NamesOnly = "namesOnly";

				// Token: 0x0400048D RID: 1165
				internal const string RecordedNamesOnly = "recordedNamesOnly";

				// Token: 0x0400048E RID: 1166
				internal const string ResultList = "resultList";

				// Token: 0x0400048F RID: 1167
				internal const string NumUsers = "numUsers";

				// Token: 0x04000490 RID: 1168
				internal const string ResultType = "resultType";

				// Token: 0x04000491 RID: 1169
				internal const string HaveNameRecording = "haveNameRecording";

				// Token: 0x04000492 RID: 1170
				internal const string HaveNameRecording1 = "haveNameRecording1";

				// Token: 0x04000493 RID: 1171
				internal const string HaveNameRecording2 = "haveNameRecording2";

				// Token: 0x04000494 RID: 1172
				internal const string HaveNameRecording3 = "haveNameRecording3";

				// Token: 0x04000495 RID: 1173
				internal const string HaveNameRecording4 = "haveNameRecording4";

				// Token: 0x04000496 RID: 1174
				internal const string HaveNameRecording5 = "haveNameRecording5";

				// Token: 0x04000497 RID: 1175
				internal const string HaveNameRecording6 = "haveNameRecording6";

				// Token: 0x04000498 RID: 1176
				internal const string HaveNameRecording7 = "haveNameRecording7";

				// Token: 0x04000499 RID: 1177
				internal const string HaveNameRecording8 = "haveNameRecording8";

				// Token: 0x0400049A RID: 1178
				internal const string HaveNameRecording9 = "haveNameRecording9";

				// Token: 0x0400049B RID: 1179
				internal const string Mode = "mode";
			}

			// Token: 0x020000B9 RID: 185
			internal abstract class AsrContacts
			{
				// Token: 0x0400049C RID: 1180
				internal const string NamesGrammar = "namesGrammar";

				// Token: 0x0400049D RID: 1181
				internal const string DistributionListGrammar = "distributionListGrammar";

				// Token: 0x0400049E RID: 1182
				internal const string EmailAliasGrammar = "emailAliasGrammar";

				// Token: 0x0400049F RID: 1183
				internal const string NameLookupEnabled = "contacts_nameLookupEnabled";

				// Token: 0x040004A0 RID: 1184
				internal const string ResultType = "resultType";

				// Token: 0x040004A1 RID: 1185
				internal const string ResultTypeString = "resultTypeString";

				// Token: 0x040004A2 RID: 1186
				internal const string SelectedUser = "selectedUser";

				// Token: 0x040004A3 RID: 1187
				internal const string SelectedPhoneNumber = "selectedPhoneNumber";

				// Token: 0x040004A4 RID: 1188
				internal const string EmailAddressSelection = "emailAddressSelection";

				// Token: 0x040004A5 RID: 1189
				internal const string HasCell = "hasCell";

				// Token: 0x040004A6 RID: 1190
				internal const string HasHome = "hasHome";

				// Token: 0x040004A7 RID: 1191
				internal const string HasOffice = "hasOffice";

				// Token: 0x040004A8 RID: 1192
				internal const string CallingType = "callingType";

				// Token: 0x040004A9 RID: 1193
				internal const string RetryAsrSearch = "retryAsrSearch";
			}

			// Token: 0x020000BA RID: 186
			internal abstract class AutoAttendant
			{
				// Token: 0x040004AA RID: 1194
				internal const string InfoAnnouncementFilename = "infoAnnouncementFilename";

				// Token: 0x040004AB RID: 1195
				internal const string InfoAnnouncementEnabled = "infoAnnouncementEnabled";

				// Token: 0x040004AC RID: 1196
				internal const string InfoAnnouncementUninterruptible = "infoAnnouncementUninterruptible";

				// Token: 0x040004AD RID: 1197
				internal const string MainMenuCustomPromptFilename = "mainMenuCustomPromptFilename";

				// Token: 0x040004AE RID: 1198
				internal const string MainMenuCustomPromptEnabled = "mainMenuCustomPromptEnabled";

				// Token: 0x040004AF RID: 1199
				internal const string DirectorySearchEnabled = "directorySearchEnabled";

				// Token: 0x040004B0 RID: 1200
				internal const string TransferToOperatorEnabled = "aa_transferToOperatorEnabled";

				// Token: 0x040004B1 RID: 1201
				internal const string CallSomeoneEnabled = "aa_callSomeoneEnabled";

				// Token: 0x040004B2 RID: 1202
				internal const string ContactSomeoneEnabled = "aa_contactSomeoneEnabled";

				// Token: 0x040004B3 RID: 1203
				internal const string ConnectToExtensionsEnabled = "connectToExtensionsEnabled";

				// Token: 0x040004B4 RID: 1204
				internal const string CustomizedMenuEnabled = "aa_customizedMenuEnabled";

				// Token: 0x040004B5 RID: 1205
				internal const string CustomizedMenuOptions = "customizedMenuOptions";

				// Token: 0x040004B6 RID: 1206
				internal const string HolidayIntroductoryGreetingPrompt = "holidayIntroductoryGreetingPrompt";

				// Token: 0x040004B7 RID: 1207
				internal const string HolidayHours = "holidayHours";

				// Token: 0x040004B8 RID: 1208
				internal const string TransferExtensionVariable = "transferExtension";

				// Token: 0x040004B9 RID: 1209
				internal const string IsBusinessHours = "aa_isBusinessHours";

				// Token: 0x040004BA RID: 1210
				internal const string DtmfFallbackEnabled = "aa_dtmfFallbackEnabled";

				// Token: 0x040004BB RID: 1211
				internal const string FirstDepartment = "firstDepartment";

				// Token: 0x040004BC RID: 1212
				internal const string CustomizedMenuGrammar = "customizedMenuGrammar";

				// Token: 0x040004BD RID: 1213
				internal const string DepartmentName = "departmentName";

				// Token: 0x040004BE RID: 1214
				internal const string RecordedNamesAndTTS = "recordedNamesAndTTS";

				// Token: 0x040004BF RID: 1215
				internal const string User1 = "user1";

				// Token: 0x040004C0 RID: 1216
				internal const string User2 = "user2";

				// Token: 0x040004C1 RID: 1217
				internal const string User3 = "user3";

				// Token: 0x040004C2 RID: 1218
				internal const string User4 = "user4";

				// Token: 0x040004C3 RID: 1219
				internal const string User5 = "user5";

				// Token: 0x040004C4 RID: 1220
				internal const string User6 = "user6";

				// Token: 0x040004C5 RID: 1221
				internal const string User7 = "user7";

				// Token: 0x040004C6 RID: 1222
				internal const string User8 = "user8";

				// Token: 0x040004C7 RID: 1223
				internal const string User9 = "user9";

				// Token: 0x040004C8 RID: 1224
				internal const string DtmfKey1 = "DtmfKey1";

				// Token: 0x040004C9 RID: 1225
				internal const string DtmfKey2 = "DtmfKey2";

				// Token: 0x040004CA RID: 1226
				internal const string DtmfKey3 = "DtmfKey3";

				// Token: 0x040004CB RID: 1227
				internal const string DtmfKey4 = "DtmfKey4";

				// Token: 0x040004CC RID: 1228
				internal const string DtmfKey5 = "DtmfKey5";

				// Token: 0x040004CD RID: 1229
				internal const string DtmfKey6 = "DtmfKey6";

				// Token: 0x040004CE RID: 1230
				internal const string DtmfKey7 = "DtmfKey7";

				// Token: 0x040004CF RID: 1231
				internal const string DtmfKey8 = "DtmfKey8";

				// Token: 0x040004D0 RID: 1232
				internal const string DtmfKey9 = "DtmfKey9";

				// Token: 0x040004D1 RID: 1233
				internal const string TimeoutOption = "TimeoutOption";

				// Token: 0x040004D2 RID: 1234
				internal const string AllowCall = "allowCall";

				// Token: 0x040004D3 RID: 1235
				internal const string AllowMessage = "allowMessage";

				// Token: 0x040004D4 RID: 1236
				internal const string HaveCustomMenuOptionPrompt = "haveCustomMenuOptionPrompt";

				// Token: 0x040004D5 RID: 1237
				internal const string CustomMenuOption = "customMenuOption";

				// Token: 0x040004D6 RID: 1238
				internal const string CustomMenuOptionPrompt = "customMenuOptionPrompt";

				// Token: 0x040004D7 RID: 1239
				internal const string AAMainMenuQA = "AA_MainMenu_QA";

				// Token: 0x040004D8 RID: 1240
				internal const string AAGotoDtmfAutoAttendant = "aa_goto_dtmf_autoattendant";

				// Token: 0x040004D9 RID: 1241
				internal const string AAGotoOperator = "aa_goto_operator";

				// Token: 0x040004DA RID: 1242
				internal const string NameOfDepartmentFormat = "nameOfDepartment{0}";

				// Token: 0x040004DB RID: 1243
				internal const string SelectableDepartments = "selectableDepartments";

				// Token: 0x040004DC RID: 1244
				internal const string ForwardCallsToDefaultMailbox = "forwardCallsToDefaultMailbox";
			}

			// Token: 0x020000BB RID: 187
			internal abstract class CallTransfer
			{
				// Token: 0x040004DD RID: 1245
				internal const string Variable = "variable";

				// Token: 0x040004DE RID: 1246
				internal const string Literal = "literal";

				// Token: 0x040004DF RID: 1247
				internal const string TargetContactInfo = "targetContactInfo";

				// Token: 0x040004E0 RID: 1248
				internal const string ReferredByUri = "ReferredByUri";
			}

			// Token: 0x020000BC RID: 188
			internal abstract class OutDialing
			{
				// Token: 0x040004E1 RID: 1249
				internal const string DialingAccessDeniedPrompt = "dialingAccessDeniedPrompt";

				// Token: 0x040004E2 RID: 1250
				internal const string PhoneNumberToDial = "phoneNumberToDial";

				// Token: 0x040004E3 RID: 1251
				internal const string CanonicalizedNumber = "canonicalizedNumber";
			}

			// Token: 0x020000BD RID: 189
			internal abstract class Email
			{
				// Token: 0x040004E4 RID: 1252
				internal const string EmailSender = "emailSender";

				// Token: 0x040004E5 RID: 1253
				internal const string EmailSubject = "emailSubject";

				// Token: 0x040004E6 RID: 1254
				internal const string NormalizedSubject = "normalizedSubject";

				// Token: 0x040004E7 RID: 1255
				internal const string EmailReceivedTime = "emailReceivedTime";

				// Token: 0x040004E8 RID: 1256
				internal const string EmailRequestTime = "emailRequestTime";

				// Token: 0x040004E9 RID: 1257
				internal const string EmailRequestTimeRange = "emailRequestTimeRange";

				// Token: 0x040004EA RID: 1258
				internal const string EmailReplyTime = "emailReplyTime";

				// Token: 0x040004EB RID: 1259
				internal const string Location = "location";

				// Token: 0x040004EC RID: 1260
				internal const string CalendarStatus = "calendarStatus";

				// Token: 0x040004ED RID: 1261
				internal const string EmailToField = "emailToField";

				// Token: 0x040004EE RID: 1262
				internal const string EmailCCField = "emailCCField";

				// Token: 0x040004EF RID: 1263
				internal const string NumMessagesFromName = "numMessagesFromName";

				// Token: 0x040004F0 RID: 1264
				internal const string FindByName = "findByName";

				// Token: 0x040004F1 RID: 1265
				internal const string SenderCallerID = "senderCallerID";
			}

			// Token: 0x020000BE RID: 190
			internal abstract class MessagePlayer
			{
				// Token: 0x040004F2 RID: 1266
				internal const string MessagePartValue = "messagePart";

				// Token: 0x040004F3 RID: 1267
				internal const string WaveMessagePartValue = "waveMessagePart";

				// Token: 0x040004F4 RID: 1268
				internal const string TextMessagePartValue = "textMessagePart";
			}

			// Token: 0x020000BF RID: 191
			internal abstract class PlayOnPhone
			{
				// Token: 0x040004F5 RID: 1269
				internal const string GreetingType = "greetingType";
			}

			// Token: 0x020000C0 RID: 192
			internal abstract class Record
			{
				// Token: 0x040004F6 RID: 1270
				internal const string Timeout = "recordingTimedOut";

				// Token: 0x040004F7 RID: 1271
				internal const string FailureCount = "recordingFailureCount";
			}

			// Token: 0x020000C1 RID: 193
			internal abstract class PromptProvisioning
			{
				// Token: 0x040004F8 RID: 1272
				internal const string PromptProvContext = "promptProvContext";

				// Token: 0x040004F9 RID: 1273
				internal const string SelectedPrompt = "selectedPrompt";

				// Token: 0x040004FA RID: 1274
				internal const string SelectedPromptGroup = "selectedPromptGroup";

				// Token: 0x040004FB RID: 1275
				internal const string SelectedPromptType = "selectedPromptType";

				// Token: 0x040004FC RID: 1276
				internal const string HolidayName = "holidayName";

				// Token: 0x040004FD RID: 1277
				internal const string HolidayStartDate = "holidayStartDate";

				// Token: 0x040004FE RID: 1278
				internal const string HolidayEndDate = "holidayEndDate";

				// Token: 0x040004FF RID: 1279
				internal const string PlaybackIndex = "playbackIndex";

				// Token: 0x04000500 RID: 1280
				internal const string HolidayCount = "holidayCount";

				// Token: 0x04000501 RID: 1281
				internal const string MoreHolidaysAvailable = "moreHolidaysAvailable";

				// Token: 0x04000502 RID: 1282
				internal const string HaveBusinessHoursPrompts = "haveBusinessHoursPrompts";

				// Token: 0x04000503 RID: 1283
				internal const string HaveAfterHoursPrompts = "haveAfterHoursPrompts";

				// Token: 0x04000504 RID: 1284
				internal const string HaveAutoAttendantPrompts = "haveAutoAttendantPrompts";

				// Token: 0x04000505 RID: 1285
				internal const string HaveDialPlanPrompts = "haveDialPlanPrompts";

				// Token: 0x04000506 RID: 1286
				internal const string HaveInfoAnnouncement = "haveInfoAnnouncement";

				// Token: 0x04000507 RID: 1287
				internal const string HaveHolidayPrompts = "haveHolidayPrompts";

				// Token: 0x04000508 RID: 1288
				internal const string HaveWelcomeGreeting = "haveWelcomeGreeting";

				// Token: 0x04000509 RID: 1289
				internal const string HaveKeyMapping = "haveKeyMapping";

				// Token: 0x0400050A RID: 1290
				internal const string HaveMainMenu = "haveMainMenu";
			}

			// Token: 0x020000C2 RID: 194
			internal abstract class PersonalOptions
			{
				// Token: 0x0400050B RID: 1291
				internal const string CurrentTimeZone = "currentTimeZone";

				// Token: 0x0400050C RID: 1292
				internal const string TimeZoneIndex = "timeZoneIndex";

				// Token: 0x0400050D RID: 1293
				internal const string OffsetHours = "offsetHours";

				// Token: 0x0400050E RID: 1294
				internal const string OffsetMinutes = "offsetMinutes";
			}
		}

		// Token: 0x020000C3 RID: 195
		internal abstract class Action
		{
			// Token: 0x06000674 RID: 1652 RVA: 0x0001ADB7 File Offset: 0x00018FB7
			private Action()
			{
			}

			// Token: 0x0400050F RID: 1295
			internal const string NullAction = "null";

			// Token: 0x04000510 RID: 1296
			internal const string GetExtension = "getExtension";

			// Token: 0x04000511 RID: 1297
			internal const string DoLogon = "doLogon";

			// Token: 0x04000512 RID: 1298
			internal const string ValidateMailbox = "validateMailbox";

			// Token: 0x04000513 RID: 1299
			internal const string CreateCallee = "createCallee";

			// Token: 0x04000514 RID: 1300
			internal const string ValidateCaller = "validateCaller";

			// Token: 0x04000515 RID: 1301
			internal const string ClearCaller = "clearCaller";

			// Token: 0x04000516 RID: 1302
			internal const string ResetCallType = "resetCallType";

			// Token: 0x04000517 RID: 1303
			internal const string QuickMessage = "quickMessage";

			// Token: 0x04000518 RID: 1304
			internal const string OofShortcut = "oofShortcut";

			// Token: 0x04000519 RID: 1305
			internal const string HandleCallSomeone = "handleCallSomeone";

			// Token: 0x0400051A RID: 1306
			internal const string SetInitialSearchTargetGAL = "setInitialSearchTargetGAL";

			// Token: 0x0400051B RID: 1307
			internal const string SetInitialSearchTargetContacts = "setInitialSearchTargetContacts";

			// Token: 0x0400051C RID: 1308
			internal const string SetPromptProvContext = "setPromptProvContext";

			// Token: 0x0400051D RID: 1309
			internal const string GetExternal = "getExternal";

			// Token: 0x0400051E RID: 1310
			internal const string GetInternal = "getInternal";

			// Token: 0x0400051F RID: 1311
			internal const string GetOof = "getOof";

			// Token: 0x04000520 RID: 1312
			internal const string GetName = "getName";

			// Token: 0x04000521 RID: 1313
			internal const string SaveExternal = "saveExternal";

			// Token: 0x04000522 RID: 1314
			internal const string SaveInternal = "saveInternal";

			// Token: 0x04000523 RID: 1315
			internal const string SaveOof = "saveOof";

			// Token: 0x04000524 RID: 1316
			internal const string SaveName = "saveName";

			// Token: 0x04000525 RID: 1317
			internal const string DeleteExternal = "deleteExternal";

			// Token: 0x04000526 RID: 1318
			internal const string DeleteInternal = "deleteInternal";

			// Token: 0x04000527 RID: 1319
			internal const string DeleteOof = "deleteOof";

			// Token: 0x04000528 RID: 1320
			internal const string DeleteName = "deleteName";

			// Token: 0x04000529 RID: 1321
			internal const string ValidatePassword = "validatePassword";

			// Token: 0x0400052A RID: 1322
			internal const string MatchPasswords = "matchPasswords";

			// Token: 0x0400052B RID: 1323
			internal const string GetSystemTask = "getSystemTask";

			// Token: 0x0400052C RID: 1324
			internal const string GetFirstTimeUserTask = "getFirstTimeUserTask";

			// Token: 0x0400052D RID: 1325
			internal const string FirstTimeUserComplete = "firstTimeUserComplete";

			// Token: 0x0400052E RID: 1326
			internal const string GetOofStatus = "getOofStatus";

			// Token: 0x0400052F RID: 1327
			internal const string SetOofStatus = "setOofStatus";

			// Token: 0x04000530 RID: 1328
			internal const string UnsetOofStatus = "unsetOofStatus";

			// Token: 0x04000531 RID: 1329
			internal const string GetGreeting = "getGreeting";

			// Token: 0x04000532 RID: 1330
			internal const string SubmitVoiceMail = "submitVoiceMail";

			// Token: 0x04000533 RID: 1331
			internal const string ClearVoiceMail = "clearVoiceMail";

			// Token: 0x04000534 RID: 1332
			internal const string AppendVoiceMail = "appendVoiceMail";

			// Token: 0x04000535 RID: 1333
			internal const string RecordPlayTime = "recordPlayTime";

			// Token: 0x04000536 RID: 1334
			internal const string FillCallerInfo = "fillCallerInfo";

			// Token: 0x04000537 RID: 1335
			internal const string SubmitVoiceMailUrgent = "submitVoiceMailUrgent";

			// Token: 0x04000538 RID: 1336
			internal const string GetNewMessages = "getNewMessages";

			// Token: 0x04000539 RID: 1337
			internal const string GetPriorityOfMessage = "getPriorityOfMessage";

			// Token: 0x0400053A RID: 1338
			internal const string DeleteVoiceMail = "deleteVoiceMail";

			// Token: 0x0400053B RID: 1339
			internal const string UndeleteVoiceMail = "undeleteVoiceMail";

			// Token: 0x0400053C RID: 1340
			internal const string SaveVoiceMail = "saveVoiceMail";

			// Token: 0x0400053D RID: 1341
			internal const string MarkUnreadVoiceMail = "markUnreadVoiceMail";

			// Token: 0x0400053E RID: 1342
			internal const string FlagVoiceMail = "flagVoiceMail";

			// Token: 0x0400053F RID: 1343
			internal const string GetEnvelopInfo = "getEnvelopInfo";

			// Token: 0x04000540 RID: 1344
			internal const string GetNextMessage = "getNextMessage";

			// Token: 0x04000541 RID: 1345
			internal const string GetPreviousMessage = "getPreviousMessage";

			// Token: 0x04000542 RID: 1346
			internal const string ReplyVoiceMail = "replyVoiceMail";

			// Token: 0x04000543 RID: 1347
			internal const string ForwardVoiceMail = "forwardVoiceMail";

			// Token: 0x04000544 RID: 1348
			internal const string GetSavedMessages = "getSavedMessages";

			// Token: 0x04000545 RID: 1349
			internal const string GetMessageReadProperty = "getMessageReadProperty";

			// Token: 0x04000546 RID: 1350
			internal const string GetSenderName = "getSenderName";

			// Token: 0x04000547 RID: 1351
			internal const string GetPlayOnPhoneType = "getPlayOnPhoneType";

			// Token: 0x04000548 RID: 1352
			internal const string DiagnosticIsLocal = "isLocal";

			// Token: 0x04000549 RID: 1353
			internal const string DiagnosticSendDtmf = "sendDtmf";

			// Token: 0x020000C4 RID: 196
			internal abstract class Base
			{
				// Token: 0x0400054A RID: 1354
				internal const string Disconnect = "disconnect";

				// Token: 0x0400054B RID: 1355
				internal const string MoreOptions = "more";

				// Token: 0x0400054C RID: 1356
				internal const string ClearRecording = "clearRecording";

				// Token: 0x0400054D RID: 1357
				internal const string AppendRecording = "appendRecording";

				// Token: 0x0400054E RID: 1358
				internal const string StopASR = "stopASR";

				// Token: 0x0400054F RID: 1359
				internal const string SaveRecoEvent = "saveRecoEvent";

				// Token: 0x04000550 RID: 1360
				internal const string SetSpeechError = "setSpeechError";
			}

			// Token: 0x020000C5 RID: 197
			internal abstract class MessageCount
			{
				// Token: 0x04000551 RID: 1361
				internal const string GetSummaryInfo = "getSummaryInfo";
			}

			// Token: 0x020000C6 RID: 198
			internal abstract class Calendar
			{
				// Token: 0x04000552 RID: 1362
				internal const string GetTodaysMeetings = "getTodaysMeetings";

				// Token: 0x04000553 RID: 1363
				internal const string PreviousMeeting = "previousMeeting";

				// Token: 0x04000554 RID: 1364
				internal const string NextMeeting = "nextMeeting";

				// Token: 0x04000555 RID: 1365
				internal const string NextDay = "nextDay";

				// Token: 0x04000556 RID: 1366
				internal const string GetDetails = "getDetails";

				// Token: 0x04000557 RID: 1367
				internal const string GetParticipants = "getParticipants";

				// Token: 0x04000558 RID: 1368
				internal const string LateForMeeting = "lateForMeeting";

				// Token: 0x04000559 RID: 1369
				internal const string CancelOrDecline = "cancelOrDecline";

				// Token: 0x0400055A RID: 1370
				internal const string CancelSeveral = "cancelSeveral";

				// Token: 0x0400055B RID: 1371
				internal const string ReplyToOrganizer = "replyToOrganizer";

				// Token: 0x0400055C RID: 1372
				internal const string Forward = "forward";

				// Token: 0x0400055D RID: 1373
				internal const string CallOrganizer = "callOrganizer";

				// Token: 0x0400055E RID: 1374
				internal const string ReplyToAll = "replyToAll";

				// Token: 0x0400055F RID: 1375
				internal const string GiveShortcutHint = "giveShortcutHint";

				// Token: 0x04000560 RID: 1376
				internal const string ParseDateSpeech = "parseDateSpeech";

				// Token: 0x04000561 RID: 1377
				internal const string OpenCalendarDate = "openCalendarDate";

				// Token: 0x04000562 RID: 1378
				internal const string NextMeetingSameDay = "nextMeetingSameDay";

				// Token: 0x04000563 RID: 1379
				internal const string PreviousMeetingSameDay = "previousMeetingSameDay";

				// Token: 0x04000564 RID: 1380
				internal const string LastMeetingSameDay = "lastMeetingSameDay";

				// Token: 0x04000565 RID: 1381
				internal const string FirstMeetingSameDay = "firstMeetingSameDay";

				// Token: 0x04000566 RID: 1382
				internal const string AcceptMeeting = "acceptMeeting";

				// Token: 0x04000567 RID: 1383
				internal const string MarkAsTentative = "markAsTentative";

				// Token: 0x04000568 RID: 1384
				internal const string SeekValidMeeting = "seekValidMeeting";

				// Token: 0x04000569 RID: 1385
				internal const string IsValidMeeting = "isValidMeeting";

				// Token: 0x0400056A RID: 1386
				internal const string SkipHeader = "skipHeader";

				// Token: 0x0400056B RID: 1387
				internal const string ReadTheHeader = "readTheHeader";

				// Token: 0x0400056C RID: 1388
				internal const string ClearLateMinutes = "clearMinutesLate";

				// Token: 0x0400056D RID: 1389
				internal const string ParseLateMinutes = "parseLateMinutes";

				// Token: 0x0400056E RID: 1390
				internal const string ParseClearTimeDays = "parseClearTimeDays";

				// Token: 0x0400056F RID: 1391
				internal const string ParseClearHours = "parseClearHours";

				// Token: 0x04000570 RID: 1392
				internal const string GiveLateMinutesHint = "giveLateMinutesHint";

				// Token: 0x04000571 RID: 1393
				internal const string SelectLanguage = "selectLanguage";

				// Token: 0x04000572 RID: 1394
				internal const string NextLanguage = "nextLanguage";
			}

			// Token: 0x020000C7 RID: 199
			internal abstract class PersonalOptions
			{
				// Token: 0x04000573 RID: 1395
				internal const string ToggleASR = "toggleASR";

				// Token: 0x04000574 RID: 1396
				internal const string ToggleOOF = "toggleOOF";

				// Token: 0x04000575 RID: 1397
				internal const string ToggleEmailOOF = "toggleEmailOOF";

				// Token: 0x04000576 RID: 1398
				internal const string ToggleTimeFormat = "toggleTimeFormat";

				// Token: 0x04000577 RID: 1399
				internal const string SetGreetingsAction = "setGreetingsAction";

				// Token: 0x04000578 RID: 1400
				internal const string FindTimeZone = "findTimeZone";

				// Token: 0x04000579 RID: 1401
				internal const string NextTimeZone = "nextTimeZone";

				// Token: 0x0400057A RID: 1402
				internal const string FirstTimeZone = "firstTimeZone";

				// Token: 0x0400057B RID: 1403
				internal const string SelectTimeZone = "selectTimeZone";
			}

			// Token: 0x020000C8 RID: 200
			internal abstract class PlayBack
			{
				// Token: 0x0400057C RID: 1404
				internal const string Pause = "pause";

				// Token: 0x0400057D RID: 1405
				internal const string Rewind = "rewind";

				// Token: 0x0400057E RID: 1406
				internal const string FastForward = "fastForward";

				// Token: 0x0400057F RID: 1407
				internal const string SlowDown = "slowDown";

				// Token: 0x04000580 RID: 1408
				internal const string SpeedUp = "speedUp";

				// Token: 0x04000581 RID: 1409
				internal const string Help = "playBackHelp";

				// Token: 0x04000582 RID: 1410
				internal const string ResetPlayback = "resetPlayback";
			}

			// Token: 0x020000C9 RID: 201
			internal abstract class SendMessage
			{
				// Token: 0x04000583 RID: 1411
				internal const string AddRecipientBySearch = "addRecipientBySearch";

				// Token: 0x04000584 RID: 1412
				internal const string RemoveRecipient = "removeRecipient";

				// Token: 0x04000585 RID: 1413
				internal const string CancelMessage = "cancelMessage";

				// Token: 0x04000586 RID: 1414
				internal const string Send = "sendMessage";

				// Token: 0x04000587 RID: 1415
				internal const string SendUrgent = "sendMessageUrgent";
			}

			// Token: 0x020000CA RID: 202
			internal abstract class DirectorySearch
			{
				// Token: 0x04000588 RID: 1416
				internal const string ChangeSearchMode = "changeSearchMode";

				// Token: 0x04000589 RID: 1417
				internal const string SearchDirectory = "searchDirectory";

				// Token: 0x0400058A RID: 1418
				internal const string StartNewSearch = "startNewSearch";

				// Token: 0x0400058B RID: 1419
				internal const string ContinueSearch = "continueSearch";

				// Token: 0x0400058C RID: 1420
				internal const string AnyMoreResultsToPlay = "anyMoreResultsToPlay";

				// Token: 0x0400058D RID: 1421
				internal const string ValidateSearchSelection = "validateSearchSelection";

				// Token: 0x0400058E RID: 1422
				internal const string ValidateInput = "ValidateInput";

				// Token: 0x0400058F RID: 1423
				internal const string ReplayResults = "replayResults";

				// Token: 0x04000590 RID: 1424
				internal const string ChangeSearchTarget = "changeSearchTarget";

				// Token: 0x04000591 RID: 1425
				internal const string SetSearchTargetToContacts = "setSearchTargetToContacts";

				// Token: 0x04000592 RID: 1426
				internal const string SetSearchTargetToGlobalAddressList = "setSearchTargetToGlobalAddressList";

				// Token: 0x04000593 RID: 1427
				internal const string SearchDirectoryByExtension = "searchDirectoryByExtension";

				// Token: 0x04000594 RID: 1428
				internal const string SetMobileNumber = "setMobileNumber";

				// Token: 0x04000595 RID: 1429
				internal const string SetBusinessNumber = "setBusinessNumber";

				// Token: 0x04000596 RID: 1430
				internal const string SetHomeNumber = "setHomeNumber";

				// Token: 0x04000597 RID: 1431
				internal const string HandleInvalidSearchKey = "handleInvalidSearchKey";

				// Token: 0x04000598 RID: 1432
				internal const string PlayContactDetails = "playContactDetails";

				// Token: 0x04000599 RID: 1433
				internal const string CheckNonUmExtension = "checkNonUmExtension";
			}

			// Token: 0x020000CB RID: 203
			internal abstract class AsrSearch
			{
				// Token: 0x0400059A RID: 1434
				internal const string InitConfirmQA = "initConfirmQA";

				// Token: 0x0400059B RID: 1435
				internal const string InitConfirmAgainQA = "initConfirmAgainQA";

				// Token: 0x0400059C RID: 1436
				internal const string InitAskAgainQA = "initAskAgainQA";

				// Token: 0x0400059D RID: 1437
				internal const string InitNameCollisionQA = "initNameCollisionQA";

				// Token: 0x0400059E RID: 1438
				internal const string InitConfirmViaListQA = "initConfirmViaListQA";

				// Token: 0x0400059F RID: 1439
				internal const string InitPromptForAliasConfirmQA = "initPromptForAliasConfirmQA";

				// Token: 0x040005A0 RID: 1440
				internal const string SetExtensionNumber = "setExtensionNumber";

				// Token: 0x040005A1 RID: 1441
				internal const string HandleRecognition = "handleRecognition";

				// Token: 0x040005A2 RID: 1442
				internal const string HandleYes = "handleYes";

				// Token: 0x040005A3 RID: 1443
				internal const string HandleNo = "handleNo";

				// Token: 0x040005A4 RID: 1444
				internal const string HandleNotListed = "handleNotListed";

				// Token: 0x040005A5 RID: 1445
				internal const string HandleNotSure = "handleNotSure";

				// Token: 0x040005A6 RID: 1446
				internal const string HandleMaybe = "handleMaybe";

				// Token: 0x040005A7 RID: 1447
				internal const string HandlePoliteEnd = "handlePoliteEnd";

				// Token: 0x040005A8 RID: 1448
				internal const string HandleChoice = "handleChoice";

				// Token: 0x040005A9 RID: 1449
				internal const string HandleValidChoice = "handleValidChoice";

				// Token: 0x040005AA RID: 1450
				internal const string HandleDtmfChoice = "handleDtmfChoice";

				// Token: 0x040005AB RID: 1451
				internal const string ResetSearchState = "resetSearchState";
			}

			// Token: 0x020000CC RID: 204
			internal abstract class AsrContacts
			{
				// Token: 0x040005AC RID: 1452
				internal const string ProcessResult = "processResult";

				// Token: 0x040005AD RID: 1453
				internal const string SetName = "setName";

				// Token: 0x040005AE RID: 1454
				internal const string SetContactInfoVariables = "setContactInfoVariables";

				// Token: 0x040005AF RID: 1455
				internal const string PrepareForTransferToCell = "prepareForTransferToCell";

				// Token: 0x040005B0 RID: 1456
				internal const string PrepareForTransferToOffice = "prepareForTransferToOffice";

				// Token: 0x040005B1 RID: 1457
				internal const string PrepareForTransferToHome = "prepareForTransferToHome";

				// Token: 0x040005B2 RID: 1458
				internal const string SetEmailAddress = "setEmailAddress";

				// Token: 0x040005B3 RID: 1459
				internal const string SelectEmailAddress = "selectEmailAddress";

				// Token: 0x040005B4 RID: 1460
				internal const string SetContactInfo = "setContactInfo";

				// Token: 0x040005B5 RID: 1461
				internal const string InitializeNamesGrammar = "initializeNamesGrammar";

				// Token: 0x040005B6 RID: 1462
				internal const string RetryAsrSearch = "retryAsrSearch";

				// Token: 0x040005B7 RID: 1463
				internal const string HandlePlatformFailure = "handlePlatformFailure";
			}

			// Token: 0x020000CD RID: 205
			internal abstract class AutoAttendant
			{
				// Token: 0x040005B8 RID: 1464
				internal const string SetExtensionNumber = "setExtensionNumber";

				// Token: 0x040005B9 RID: 1465
				internal const string InitializeState = "initializeState";

				// Token: 0x040005BA RID: 1466
				internal const string TransferToOperator = "transferToOperator";

				// Token: 0x040005BB RID: 1467
				internal const string SetOperatorNumber = "setOperatorNumber";

				// Token: 0x040005BC RID: 1468
				internal const string SetCustomMenuVoicemailTarget = "setCustomMenuVoicemailTarget";

				// Token: 0x040005BD RID: 1469
				internal const string TransferToPAASiteFailed = "transferToPAASiteFailed";

				// Token: 0x040005BE RID: 1470
				internal const string SetCustomMenuTargetPAA = "setCustomMenuTargetPAA";

				// Token: 0x040005BF RID: 1471
				internal const string SetCustomExtensionNumber = "setCustomExtensionNumber";

				// Token: 0x040005C0 RID: 1472
				internal const string SetFallbackAutoAttendant = "setFallbackAutoAttendant";

				// Token: 0x040005C1 RID: 1473
				internal const string SetCustomMenuAutoAttendant = "setCustomMenuAutoAttendant";

				// Token: 0x040005C2 RID: 1474
				internal const string ProcessCustomMenuSelection = "processCustomMenuSelection";

				// Token: 0x040005C3 RID: 1475
				internal const string PrepareForTransferToPaa = "prepareForTransferToPaa";

				// Token: 0x040005C4 RID: 1476
				internal const string ComputeDtmfFallbackAction = "computeDtmfFallbackAction";

				// Token: 0x040005C5 RID: 1477
				internal const string PrepareForANROperatorTransfer = "prepareForANROperatorTransfer";

				// Token: 0x040005C6 RID: 1478
				internal const string PrepareForProtectedSubscriberOperatorTransfer = "prepareForProtectedSubscriberOperatorTransfer";

				// Token: 0x040005C7 RID: 1479
				internal const string PrepareForTransferToDtmfFallbackAutoAttendant = "prepareForTransferToDtmfFallbackAutoAttendant";

				// Token: 0x040005C8 RID: 1480
				internal const string PrepareForTransferToSendMessage = "prepareForTransferToSendMessage";

				// Token: 0x040005C9 RID: 1481
				internal const string PrepareForTransferToKeyMappingExtension = "prepareForTransferToKeyMappingExtension";

				// Token: 0x040005CA RID: 1482
				internal const string PrepareForTransferToKeyMappingAutoAttendant = "prepareForTransferToKeyMappingAutoAttendant";

				// Token: 0x040005CB RID: 1483
				internal const string PrepareForUserInitiatedOperatorTransfer = "prepareForUserInitiatedOperatorTransfer";

				// Token: 0x040005CC RID: 1484
				internal const string PrepareForUserInitiatedOperatorTransferFromOpeningMenu = "prepareForUserInitiatedOperatorTransferFromOpeningMenu";

				// Token: 0x040005CD RID: 1485
				internal const string HandleMissingGrammarFile = "handleMissingGrammarFile";

				// Token: 0x040005CE RID: 1486
				internal const string HandleFaxTone = "handleFaxTone";
			}

			// Token: 0x020000CE RID: 206
			internal abstract class OutDialing
			{
				// Token: 0x040005CF RID: 1487
				internal const string CheckRestrictedUser = "checkRestrictedUser";

				// Token: 0x040005D0 RID: 1488
				internal const string CanonicalizeNumber = "canonicalizeNumber";

				// Token: 0x040005D1 RID: 1489
				internal const string CheckDialPermissions = "checkDialPermissions";

				// Token: 0x040005D2 RID: 1490
				internal const string ProcessResult = "processResult";
			}

			// Token: 0x020000CF RID: 207
			internal abstract class Email
			{
				// Token: 0x040005D3 RID: 1491
				internal const string NextMessage = "nextMessage";

				// Token: 0x040005D4 RID: 1492
				internal const string NextUnreadMessage = "nextUnreadMessage";

				// Token: 0x040005D5 RID: 1493
				internal const string PreviousMessage = "previousMessage";

				// Token: 0x040005D6 RID: 1494
				internal const string AcceptMeeting = "acceptMeeting";

				// Token: 0x040005D7 RID: 1495
				internal const string AcceptMeetingTentative = "acceptMeetingTentative";

				// Token: 0x040005D8 RID: 1496
				internal const string DeclineMeeting = "declineMeeting";

				// Token: 0x040005D9 RID: 1497
				internal const string DeleteMessage = "deleteMessage";

				// Token: 0x040005DA RID: 1498
				internal const string DeleteThread = "deleteThread";

				// Token: 0x040005DB RID: 1499
				internal const string HideThread = "hideThread";

				// Token: 0x040005DC RID: 1500
				internal const string CommitPendingDeletions = "commitPendingDeletions";

				// Token: 0x040005DD RID: 1501
				internal const string FindByName = "findByName";

				// Token: 0x040005DE RID: 1502
				internal const string UndeleteMessage = "undeleteMessage";

				// Token: 0x040005DF RID: 1503
				internal const string Reply = "reply";

				// Token: 0x040005E0 RID: 1504
				internal const string ReplyAll = "replyAll";

				// Token: 0x040005E1 RID: 1505
				internal const string Forward = "forward";

				// Token: 0x040005E2 RID: 1506
				internal const string SaveMessage = "saveMessage";

				// Token: 0x040005E3 RID: 1507
				internal const string MarkUnread = "markUnread";

				// Token: 0x040005E4 RID: 1508
				internal const string FlagMessage = "flagMessage";

				// Token: 0x040005E5 RID: 1509
				internal const string SetMobileNumber = "setMobileNumber";

				// Token: 0x040005E6 RID: 1510
				internal const string SetBusinessNumber = "setBusinessNumber";

				// Token: 0x040005E7 RID: 1511
				internal const string SetHomeNumber = "setHomeNumber";

				// Token: 0x040005E8 RID: 1512
				internal const string SelectLanguage = "selectLanguage";

				// Token: 0x040005E9 RID: 1513
				internal const string NextLanguage = "nextLanguage";
			}

			// Token: 0x020000D0 RID: 208
			internal abstract class MessagePlayer
			{
				// Token: 0x040005EA RID: 1514
				internal const string NextMessagePart = "nextMessagePart";

				// Token: 0x040005EB RID: 1515
				internal const string FirstMessagePart = "firstMessagePart";

				// Token: 0x040005EC RID: 1516
				internal const string NextMessageSection = "nextMessageSection";

				// Token: 0x040005ED RID: 1517
				internal const string PreviousMessageSection = "firstMessageSection";

				// Token: 0x040005EE RID: 1518
				internal const string SelectLanguagePause = "selectLanguagePause";

				// Token: 0x040005EF RID: 1519
				internal const string NextLanguagePause = "nextLanguagePause";
			}

			// Token: 0x020000D1 RID: 209
			internal abstract class CAMessageSubmission
			{
				// Token: 0x040005F0 RID: 1520
				internal const string IsQuotaExceeded = "isQuotaExceeded";

				// Token: 0x040005F1 RID: 1521
				internal const string IsPipelineHealthy = "isPipelineHealthy";

				// Token: 0x040005F2 RID: 1522
				internal const string CanAnnonLeaveMessage = "canAnnonLeaveMessage";

				// Token: 0x040005F3 RID: 1523
				internal const string HandleFailedTransfer = "HandleFailedTransfer";
			}

			// Token: 0x020000D2 RID: 210
			internal abstract class PromptProvisioning
			{
				// Token: 0x040005F4 RID: 1524
				internal const string PublishPrompt = "publishPrompt";

				// Token: 0x040005F5 RID: 1525
				internal const string CanUpdatePrompts = "canUpdatePrompts";

				// Token: 0x040005F6 RID: 1526
				internal const string SetDialPlanContext = "setDialPlanContext";

				// Token: 0x040005F7 RID: 1527
				internal const string SetAutoAttendantContext = "setAutoAttendantContext";

				// Token: 0x040005F8 RID: 1528
				internal const string PrepareForPlayback = "prepareForPlayback";

				// Token: 0x040005F9 RID: 1529
				internal const string SelectGroupBusinessHours = "selectBusinessHoursGroup";

				// Token: 0x040005FA RID: 1530
				internal const string SelectGroupAfterHours = "selectAfterHoursGroup";

				// Token: 0x040005FB RID: 1531
				internal const string SelectKeyMapping = "selectKeyMapping";

				// Token: 0x040005FC RID: 1532
				internal const string SelectHolidaySchedule = "selectHolidaySchedule";

				// Token: 0x040005FD RID: 1533
				internal const string SelectWelcomeGreeting = "selectWelcomeGreeting";

				// Token: 0x040005FE RID: 1534
				internal const string SelectInfoAnnouncement = "selectInfoAnnouncement";

				// Token: 0x040005FF RID: 1535
				internal const string SelectMainMenuCustomPrompt = "selectMainMenuCustomPrompt";

				// Token: 0x04000600 RID: 1536
				internal const string SelectPromptIndex = "selectPromptIndex";

				// Token: 0x04000601 RID: 1537
				internal const string NextPlaybackIndex = "nextPlaybackIndex";

				// Token: 0x04000602 RID: 1538
				internal const string ResetPlaybackIndex = "resetPlaybackIndex";

				// Token: 0x04000603 RID: 1539
				internal const string SelectNextHolidayPage = "selectNextHolidayPage";

				// Token: 0x04000604 RID: 1540
				internal const string ExitPromptProvisioning = "exitPromptProvisioning";
			}
		}

		// Token: 0x020000D3 RID: 211
		internal abstract class Condition
		{
			// Token: 0x04000605 RID: 1541
			internal const string RepeatMode = "repeat";

			// Token: 0x04000606 RID: 1542
			internal const string MoreOptions = "more";

			// Token: 0x04000607 RID: 1543
			internal const string ReplyIntro = "replyIntro";

			// Token: 0x04000608 RID: 1544
			internal const string ReplyAllIntro = "replyAllIntro";

			// Token: 0x04000609 RID: 1545
			internal const string DeclineIntro = "declineIntro";

			// Token: 0x0400060A RID: 1546
			internal const string CancelIntro = "cancelIntro";

			// Token: 0x0400060B RID: 1547
			internal const string ClearCalendarIntro = "clearCalendarIntro";

			// Token: 0x0400060C RID: 1548
			internal const string ForwardIntro = "forwardIntro";

			// Token: 0x0400060D RID: 1549
			internal const string CalendarAccessEnabled = "calendarAccessEnabled";

			// Token: 0x0400060E RID: 1550
			internal const string EmailAccessEnabled = "emailAccessEnabled";

			// Token: 0x0400060F RID: 1551
			internal const string WavePart = "wavePart";

			// Token: 0x04000610 RID: 1552
			internal const string TextPart = "textPart";

			// Token: 0x04000611 RID: 1553
			internal const string KnowSenderPhoneNumber = "knowSenderPhoneNumber";

			// Token: 0x04000612 RID: 1554
			internal const string WaitForSourcePartyInfo = "waitForSourcePartyInfo";

			// Token: 0x04000613 RID: 1555
			internal const string DiagnosticTUILogonCheck = "diagnosticTUILogonCheck";

			// Token: 0x020000D4 RID: 212
			internal abstract class MessageCount
			{
				// Token: 0x04000614 RID: 1556
				internal const string IsInProgress = "isInProgress";

				// Token: 0x04000615 RID: 1557
				internal const string IsMaxEmail = "isMaxEmail";
			}

			// Token: 0x020000D5 RID: 213
			internal abstract class Calendar
			{
				// Token: 0x04000616 RID: 1558
				internal const string Past = "past";

				// Token: 0x04000617 RID: 1559
				internal const string Present = "present";

				// Token: 0x04000618 RID: 1560
				internal const string Future = "future";

				// Token: 0x04000619 RID: 1561
				internal const string Conflict = "conflict";

				// Token: 0x0400061A RID: 1562
				internal const string First = "first";

				// Token: 0x0400061B RID: 1563
				internal const string FirstConflict = "firstConflict";

				// Token: 0x0400061C RID: 1564
				internal const string Middle = "middle";

				// Token: 0x0400061D RID: 1565
				internal const string Last = "last";

				// Token: 0x0400061E RID: 1566
				internal const string Initial = "initial";

				// Token: 0x0400061F RID: 1567
				internal const string Today = "today";

				// Token: 0x04000620 RID: 1568
				internal const string Tentative = "tentative";

				// Token: 0x04000621 RID: 1569
				internal const string Owner = "owner";

				// Token: 0x04000622 RID: 1570
				internal const string LocationPhone = "locationPhone";

				// Token: 0x04000623 RID: 1571
				internal const string OrganizerPhone = "organizerPhone";

				// Token: 0x04000624 RID: 1572
				internal const string IsMeeting = "isMeeting";

				// Token: 0x04000625 RID: 1573
				internal const string IsAllDayEvent = "isAllDayEvent";

				// Token: 0x04000626 RID: 1574
				internal const string GiveShortcutHint = "giveShortcutHint";

				// Token: 0x04000627 RID: 1575
				internal const string SkipHeader = "skipHeader";

				// Token: 0x04000628 RID: 1576
				internal const string GiveMinutesLateHint = "giveMinutesLateHint";

				// Token: 0x04000629 RID: 1577
				internal const string ConflictsWithLastHeard = "conflictWithLastHeard";

				// Token: 0x0400062A RID: 1578
				internal const string DateChanged = "dateChanged";
			}

			// Token: 0x020000D6 RID: 214
			internal abstract class Email
			{
				// Token: 0x0400062B RID: 1579
				internal const string MeetingRequest = "meetingRequest";

				// Token: 0x0400062C RID: 1580
				internal const string FirstMessage = "firstMessage";

				// Token: 0x0400062D RID: 1581
				internal const string LastMessage = "lastMessage";

				// Token: 0x0400062E RID: 1582
				internal const string Free = "free";

				// Token: 0x0400062F RID: 1583
				internal const string Busy = "busy";

				// Token: 0x04000630 RID: 1584
				internal const string Tentative = "tentative";

				// Token: 0x04000631 RID: 1585
				internal const string OOF = "oof";

				// Token: 0x04000632 RID: 1586
				internal const string MeetingAccepted = "alreadyAccepted";

				// Token: 0x04000633 RID: 1587
				internal const string MeetingOver = "meetingOver";

				// Token: 0x04000634 RID: 1588
				internal const string OutOfDate = "outOfDate";

				// Token: 0x04000635 RID: 1589
				internal const string MeetingCancellation = "meetingCancellation";

				// Token: 0x04000636 RID: 1590
				internal const string Urgent = "urgent";

				// Token: 0x04000637 RID: 1591
				internal const string Protected = "protected";

				// Token: 0x04000638 RID: 1592
				internal const string Attachments = "attachments";

				// Token: 0x04000639 RID: 1593
				internal const string Drm = "drm";

				// Token: 0x0400063A RID: 1594
				internal const string Read = "read";

				// Token: 0x0400063B RID: 1595
				internal const string PlayedUndelete = "playedUndelete";

				// Token: 0x0400063C RID: 1596
				internal const string ContactEntry = "ContactEntry";

				// Token: 0x0400063D RID: 1597
				internal const string GALEntry = "GALEntry";

				// Token: 0x0400063E RID: 1598
				internal const string ValidSenderPhone = "validSenderPhone";

				// Token: 0x0400063F RID: 1599
				internal const string IsRecorded = "isRecorded";

				// Token: 0x04000640 RID: 1600
				internal const string IsReply = "isReply";

				// Token: 0x04000641 RID: 1601
				internal const string IsForward = "isForward";

				// Token: 0x04000642 RID: 1602
				internal const string IsMissedCall = "isMissedCall";

				// Token: 0x04000643 RID: 1603
				internal const string ReceivedDayOfWeek = "receivedDayOfWeek";

				// Token: 0x04000644 RID: 1604
				internal const string ReceivedOffset = "receivedOffset";

				// Token: 0x04000645 RID: 1605
				internal const string MeetingDayOfWeek = "meetingDayOfWeek";

				// Token: 0x04000646 RID: 1606
				internal const string MeetingOffset = "meetingOffset";

				// Token: 0x04000647 RID: 1607
				internal const string CanUndelete = "canUndelete";

				// Token: 0x04000648 RID: 1608
				internal const string UndeletedAConversation = "undeletedAConversation";

				// Token: 0x04000649 RID: 1609
				internal const string InFindMode = "inFindMode";
			}

			// Token: 0x020000D7 RID: 215
			internal abstract class MessagePlayer
			{
				// Token: 0x0400064A RID: 1610
				internal const string IsEmptyText = "isEmptyText";

				// Token: 0x0400064B RID: 1611
				internal const string IsEmptyWave = "isEmptyWave";

				// Token: 0x0400064C RID: 1612
				internal const string PlayMixedContentIntro = "playMixedContentIntro";

				// Token: 0x0400064D RID: 1613
				internal const string PlayAudioContentIntro = "playAudioContentIntro";

				// Token: 0x0400064E RID: 1614
				internal const string PlayTextContentIntro = "playTextContentIntro";
			}

			// Token: 0x020000D8 RID: 216
			internal abstract class Voicemail
			{
				// Token: 0x0400064F RID: 1615
				internal const string PlayedUndelete = "playedUndelete";

				// Token: 0x04000650 RID: 1616
				internal const string KnowVoicemailSender = "knowVoicemailSender";

				// Token: 0x04000651 RID: 1617
				internal const string IsHighPriority = "isHighPriority";

				// Token: 0x04000652 RID: 1618
				internal const string IsProtected = "isProtected";
			}

			// Token: 0x020000D9 RID: 217
			internal abstract class PersonalOptions
			{
				// Token: 0x04000653 RID: 1619
				internal const string Oof = "Oof";

				// Token: 0x04000654 RID: 1620
				internal const string EmailOof = "emailOof";

				// Token: 0x04000655 RID: 1621
				internal const string TimeFormat24 = "timeFormat24";

				// Token: 0x04000656 RID: 1622
				internal const string CanToggleTimeFormat = "canToggleTimeFormat";

				// Token: 0x04000657 RID: 1623
				internal const string CanToggleASR = "canToggleASR";

				// Token: 0x04000658 RID: 1624
				internal const string LastAction = "lastAction";

				// Token: 0x04000659 RID: 1625
				internal const string PlayGMTOffset = "playGMTOffset";

				// Token: 0x0400065A RID: 1626
				internal const string PositiveOffset = "positiveOffset";
			}

			// Token: 0x020000DA RID: 218
			internal abstract class QA
			{
				// Token: 0x0400065B RID: 1627
				internal const string MainMenuQA = "MainMenuQA";
			}

			// Token: 0x020000DB RID: 219
			internal abstract class PlayOnPhone
			{
				// Token: 0x0400065C RID: 1628
				internal const string OofCustom = "OofCustom";

				// Token: 0x0400065D RID: 1629
				internal const string NormalCustom = "NormalCustom";
			}
		}

		// Token: 0x020000DC RID: 220
		internal abstract class TransitionEvent
		{
			// Token: 0x0600068D RID: 1677 RVA: 0x0001AE7F File Offset: 0x0001907F
			private TransitionEvent()
			{
			}

			// Token: 0x0600068E RID: 1678 RVA: 0x0001AE88 File Offset: 0x00019088
			internal static string OutId(uint depth)
			{
				return string.Format(CultureInfo.InvariantCulture, "out-{0}", new object[]
				{
					depth.ToString(CultureInfo.InvariantCulture)
				});
			}

			// Token: 0x0400065E RID: 1630
			internal const string NoKey = "noKey";

			// Token: 0x0400065F RID: 1631
			internal const string DtmfSent = "dtmfSent";

			// Token: 0x04000660 RID: 1632
			internal const string ToolInfoSent = "toolInfoSent";

			// Token: 0x04000661 RID: 1633
			internal const string DivertedExtensionNotAllowVoiceMail = "divertedExtensionNotAllowVoiceMail";

			// Token: 0x04000662 RID: 1634
			internal const string Timeout = "timeout";

			// Token: 0x04000663 RID: 1635
			internal const string Silence = "silence";

			// Token: 0x04000664 RID: 1636
			internal const string RecordFailure = "recordFailure";

			// Token: 0x04000665 RID: 1637
			internal const string AnyKey = "anyKey";

			// Token: 0x04000666 RID: 1638
			internal const string StopEvent = "stopEvent";

			// Token: 0x04000667 RID: 1639
			internal const string UserHangup = "userHangup";

			// Token: 0x04000668 RID: 1640
			internal const string MaxProsodyRate = "maxProsodyRate";

			// Token: 0x04000669 RID: 1641
			internal const string MinProsodyRate = "minProsodyRate";

			// Token: 0x0400066A RID: 1642
			internal const string XsoError = "xsoError";

			// Token: 0x0400066B RID: 1643
			internal const string QuotaExceeded = "quotaExceeded";

			// Token: 0x0400066C RID: 1644
			internal const string UnknownLanguage = "unknownLanguage";

			// Token: 0x0400066D RID: 1645
			internal const string ExtensionFound = "extensionFound";

			// Token: 0x0400066E RID: 1646
			internal const string VirtualNumberCall = "virtualNumberCall";

			// Token: 0x0400066F RID: 1647
			internal const string TroubleshootingToolCall = "troubleshootingToolCall";

			// Token: 0x04000670 RID: 1648
			internal const string InvalidExtension = "invalidExtension";

			// Token: 0x04000671 RID: 1649
			internal const string MaxInvalidExtensions = "maxInvalidExtensions";

			// Token: 0x04000672 RID: 1650
			internal const string ValidUser = "validUser";

			// Token: 0x04000673 RID: 1651
			internal const string ValidDtmfAutoAttendant = "validDtmfAutoAttendant";

			// Token: 0x04000674 RID: 1652
			internal const string ValidSpeechAutoAttendant = "validSpeechAutoAttendant";

			// Token: 0x04000675 RID: 1653
			internal const string RunDefaultAutoAttendant = "runDefaultAutoAttendant";

			// Token: 0x04000676 RID: 1654
			internal const string RunCallExtension = "runCallExtension";

			// Token: 0x04000677 RID: 1655
			internal const string MailboxFound = "mailboxFound";

			// Token: 0x04000678 RID: 1656
			internal const string MailboxNotSupported = "mailboxNotSupported";

			// Token: 0x04000679 RID: 1657
			internal const string NoMoreServers = "noMoreServers";

			// Token: 0x0400067A RID: 1658
			internal const string MaxInvalidMailbox = "maxInvalidMailbox";

			// Token: 0x0400067B RID: 1659
			internal const string LogonAsr = "logonAsr";

			// Token: 0x0400067C RID: 1660
			internal const string LogonOk = "logonOk";

			// Token: 0x0400067D RID: 1661
			internal const string LogonPP = "logonPP";

			// Token: 0x0400067E RID: 1662
			internal const string BadPasswordDisconnect = "badPasswordDisconnect";

			// Token: 0x0400067F RID: 1663
			internal const string BadPasswordLockout = "badPasswordLockout";

			// Token: 0x04000680 RID: 1664
			internal const string BadPasswordReset = "badPasswordReset";

			// Token: 0x04000681 RID: 1665
			internal const string StaleChecksum = "staleChecksum";

			// Token: 0x04000682 RID: 1666
			internal const string NoExternal = "noExternal";

			// Token: 0x04000683 RID: 1667
			internal const string NoInternal = "noInternal";

			// Token: 0x04000684 RID: 1668
			internal const string NoOof = "noOof";

			// Token: 0x04000685 RID: 1669
			internal const string NoName = "noName";

			// Token: 0x04000686 RID: 1670
			internal const string PasswordValidated = "passwordValidated";

			// Token: 0x04000687 RID: 1671
			internal const string PasswordsMatch = "passwordsMatch";

			// Token: 0x04000688 RID: 1672
			internal const string ChangePasswordTask = "changePasswordTask";

			// Token: 0x04000689 RID: 1673
			internal const string FirstTimeUserTask = "firstTimeUserTask";

			// Token: 0x0400068A RID: 1674
			internal const string OofStatusTask = "oofStatusTask";

			// Token: 0x0400068B RID: 1675
			internal const string FaxRequestAccepted = "faxRequestAccepted";

			// Token: 0x0400068C RID: 1676
			internal const string RecordNameTask = "recordNameTask";

			// Token: 0x0400068D RID: 1677
			internal const string RecordInternalTask = "recordInternalTask";

			// Token: 0x0400068E RID: 1678
			internal const string RecordExternalTask = "recordExternalTask";

			// Token: 0x0400068F RID: 1679
			internal const string NoGreeting = "noGreeting";

			// Token: 0x04000690 RID: 1680
			internal const string NoGreetingOof = "noGreetingOof";

			// Token: 0x04000691 RID: 1681
			internal const string QuotaNotExceeded = "quotaNotExceeded";

			// Token: 0x04000692 RID: 1682
			internal const string PipelineHealthy = "pipelineHealthy";

			// Token: 0x04000693 RID: 1683
			internal const string AnnonCanLeaveMessage = "annonCanLeaveMessage";

			// Token: 0x04000694 RID: 1684
			internal const string IsHighPriority = "isHighPriority";

			// Token: 0x04000695 RID: 1685
			internal const string NoNewMessages = "noNewMessages";

			// Token: 0x04000696 RID: 1686
			internal const string NoSavedMessages = "noSavedMessages";

			// Token: 0x04000697 RID: 1687
			internal const string NoPreviousNewMessages = "noPreviousNewMessages";

			// Token: 0x04000698 RID: 1688
			internal const string NoPreviousSavedMessages = "noPreviousSavedMessages";

			// Token: 0x04000699 RID: 1689
			internal const string CurrentNewMessage = "currentNewMessage";

			// Token: 0x0400069A RID: 1690
			internal const string CurrentSavedMessage = "currentSavedMessage";

			// Token: 0x0400069B RID: 1691
			internal const string InvalidWaveAttachment = "invalidWaveAttachment";

			// Token: 0x0400069C RID: 1692
			internal const string NameNotAvailable = "nameNotAvailable";

			// Token: 0x0400069D RID: 1693
			internal const string FaxTone = "faxtone";

			// Token: 0x0400069E RID: 1694
			internal const string PlayOnPhone = "playOnPhone";

			// Token: 0x0400069F RID: 1695
			internal const string FindMeSubscriberCall = "findMeSubscriberCall";

			// Token: 0x040006A0 RID: 1696
			internal const string PlayOnPhoneVoicemail = "playOnPhoneVoicemail";

			// Token: 0x040006A1 RID: 1697
			internal const string PlayOnPhoneAAGreeting = "playOnPhoneAAGreeting";

			// Token: 0x040006A2 RID: 1698
			internal const string PlayOnPhoneGreeting = "playOnPhoneGreeting";

			// Token: 0x040006A3 RID: 1699
			internal const string PlayOnPhonePAAGreeting = "playOnPhonePAAGreeting";

			// Token: 0x040006A4 RID: 1700
			internal const string UMDiagnosticCall = "umdiagnosticCall";

			// Token: 0x040006A5 RID: 1701
			internal const string ForcePinLogin = "forcePinLogin";

			// Token: 0x040006A6 RID: 1702
			internal const string LocalDiagnostic = "local";

			// Token: 0x040006A7 RID: 1703
			internal const string PAAFindmeMoreNumbersLeft = "moreFindMeNumbersLeft";

			// Token: 0x040006A8 RID: 1704
			internal const string AllFindMeNumbersFailedAccessCheck = "dialingRulesCheckFailed";

			// Token: 0x040006A9 RID: 1705
			internal const string MaxAllowedCallsLimitReached = "maxAllowedCallsLimitReached";

			// Token: 0x040006AA RID: 1706
			internal const string BlockedCall = "blockedCall";

			// Token: 0x040006AB RID: 1707
			internal const string NoActionLeft = "noActionLeft";

			// Token: 0x040006AC RID: 1708
			internal static readonly byte[] AnyKeyBytes = Encoding.ASCII.GetBytes("anyKey");

			// Token: 0x020000DD RID: 221
			internal abstract class Calendar
			{
				// Token: 0x040006AD RID: 1709
				internal const string NoMeetings = "noMeetings";

				// Token: 0x040006AE RID: 1710
				internal const string EmptyCalendar = "emptyCalendar";

				// Token: 0x040006AF RID: 1711
				internal const string InvalidTime = "invalidTime";
			}

			// Token: 0x020000DE RID: 222
			internal abstract class CallTransfer
			{
				// Token: 0x040006B0 RID: 1712
				internal const string TransferOK = "transferOK";

				// Token: 0x040006B1 RID: 1713
				internal const string TransferFailed = "transferFailed";
			}

			// Token: 0x020000DF RID: 223
			internal abstract class SendMessage
			{
				// Token: 0x040006B2 RID: 1714
				internal const string UnknownRecipient = "unknownRecipient";

				// Token: 0x040006B3 RID: 1715
				internal const string NoRecipients = "noRecipients";
			}

			// Token: 0x020000E0 RID: 224
			internal abstract class DirectorySearch
			{
				// Token: 0x040006B4 RID: 1716
				internal const string ResultsLessThanAllowed = "resultsLessThanAllowed";

				// Token: 0x040006B5 RID: 1717
				internal const string ResultsMoreThanAllowed = "resultsMoreThanAllowed";

				// Token: 0x040006B6 RID: 1718
				internal const string MoreResultsToPlay = "moreResultsToPlay";

				// Token: 0x040006B7 RID: 1719
				internal const string NoMoreResultsToPlay = "noMoreResultsToPlay";

				// Token: 0x040006B8 RID: 1720
				internal const string ValidSelection = "validSelection";

				// Token: 0x040006B9 RID: 1721
				internal const string InvalidSelection = "invalidSelection";

				// Token: 0x040006BA RID: 1722
				internal const string InvalidInput = "invalidInput";

				// Token: 0x040006BB RID: 1723
				internal const string ValidInput = "validInput";

				// Token: 0x040006BC RID: 1724
				internal const string InvalidSearchKey = "invalidSearchKey";

				// Token: 0x040006BD RID: 1725
				internal const string NoResultsMatched = "noResultsMatched";

				// Token: 0x040006BE RID: 1726
				internal const string PhoneNumberNotFound = "phoneNumberNotFound";

				// Token: 0x040006BF RID: 1727
				internal const string UserNotEnabledForUm = "userNotEnabledForUm";

				// Token: 0x040006C0 RID: 1728
				internal const string MaxNumberOfTriesExceeded = "maxNumberOfTriesExceeded";

				// Token: 0x040006C1 RID: 1729
				internal const string OperatorFallback = "operatorFallback";

				// Token: 0x040006C2 RID: 1730
				internal const string AmbiguousMatches = "ambiguousMatches";

				// Token: 0x040006C3 RID: 1731
				internal const string PromptForAlias = "promptForAlias";

				// Token: 0x040006C4 RID: 1732
				internal const string ADTransientError = "adTransientError";

				// Token: 0x040006C5 RID: 1733
				internal const string AllowCallNonUmExtension = "allowCallNonUmExtension";

				// Token: 0x040006C6 RID: 1734
				internal const string DenyCallNonUmExtension = "denyCallNonUmExtension";
			}

			// Token: 0x020000E1 RID: 225
			internal abstract class AsrSearch
			{
				// Token: 0x040006C7 RID: 1735
				internal const string DoFallback = "doFallback";

				// Token: 0x040006C8 RID: 1736
				internal const string InvalidSearchResult = "invalidSearchResult";

				// Token: 0x040006C9 RID: 1737
				internal const string Collision = "collision";

				// Token: 0x040006CA RID: 1738
				internal const string ValidChoice = "validChoice";

				// Token: 0x040006CB RID: 1739
				internal const string ConfirmViaList = "confirmViaList";

				// Token: 0x040006CC RID: 1740
				internal const string DoAskAgainQA = "doAskAgainQA";

				// Token: 0x040006CD RID: 1741
				internal const string InvalidSelection = "invalidSelection";

				// Token: 0x040006CE RID: 1742
				internal const string ResultsMoreThanAllowed = "resultsMoreThanAllowed";

				// Token: 0x040006CF RID: 1743
				internal const string PromptForAlias = "promptForAlias";

				// Token: 0x040006D0 RID: 1744
				internal const string OperatorFallback = "operatorFallback";

				// Token: 0x040006D1 RID: 1745
				internal const string RetrySearch = "retrySearch";
			}

			// Token: 0x020000E2 RID: 226
			internal abstract class AsrContacts
			{
				// Token: 0x040006D2 RID: 1746
				internal const string InvalidOption = "invalidOption";

				// Token: 0x040006D3 RID: 1747
				internal const string InvalidResult = "invalidResult";

				// Token: 0x040006D4 RID: 1748
				internal const string MoreThanOneAddress = "moreThanOneAddress";

				// Token: 0x040006D5 RID: 1749
				internal const string NoGrammarFile = "noGrammarFile";
			}

			// Token: 0x020000E3 RID: 227
			internal abstract class AutoAttendant
			{
				// Token: 0x040006D6 RID: 1750
				internal const string InvalidOption = "invalidOption";

				// Token: 0x040006D7 RID: 1751
				internal const string NoPAAFound = "noPAAFound";

				// Token: 0x040006D8 RID: 1752
				internal const string CannotTransferToCustomExtension = "cannotTransferToCustomExtension";

				// Token: 0x040006D9 RID: 1753
				internal const string TargetPAAInDifferentSite = "targetPAAInDifferentSite";

				// Token: 0x040006DA RID: 1754
				internal const string NoTransferToOperator = "noTransferToOperator";

				// Token: 0x040006DB RID: 1755
				internal const string FallbackAutoAttendantFailure = "fallbackAutoAttendantFailure";
			}

			// Token: 0x020000E4 RID: 228
			internal abstract class OutDialing
			{
				// Token: 0x040006DC RID: 1756
				internal const string DialingPermissionCheckFailed = "dialingPermissionCheckFailed";

				// Token: 0x040006DD RID: 1757
				internal const string ValidCanonicalNumber = "validCanonicalNumber";

				// Token: 0x040006DE RID: 1758
				internal const string NumberCanonicalizationFailed = "numberCanonicalizationFailed";

				// Token: 0x040006DF RID: 1759
				internal const string RestrictedUser = "restrictedUser";

				// Token: 0x040006E0 RID: 1760
				internal const string UnrestrictedUser = "unrestrictedUser";

				// Token: 0x040006E1 RID: 1761
				internal const string UnreachableUser = "unreachableUser";

				// Token: 0x040006E2 RID: 1762
				internal const string AllowSendMessageOnly = "allowSendMessageOnly";

				// Token: 0x040006E3 RID: 1763
				internal const string AllowCallOnly = "allowCallOnly";
			}

			// Token: 0x020000E5 RID: 229
			internal abstract class Email
			{
				// Token: 0x040006E4 RID: 1764
				internal const string EndOfMessages = "endOfMessages";

				// Token: 0x040006E5 RID: 1765
				internal const string NoPreviousMessages = "noPreviousMessages";
			}

			// Token: 0x020000E6 RID: 230
			internal abstract class MessagePlayer
			{
				// Token: 0x040006E6 RID: 1766
				internal const string EndOfSection = "endOfSection";
			}

			// Token: 0x020000E7 RID: 231
			internal abstract class SpeechMenu
			{
				// Token: 0x040006E7 RID: 1767
				internal const string Main = "main";

				// Token: 0x040006E8 RID: 1768
				internal const string Repeat = "repeat";

				// Token: 0x040006E9 RID: 1769
				internal const string Mumble1 = "mumble1";

				// Token: 0x040006EA RID: 1770
				internal const string Mumble2 = "mumble2";

				// Token: 0x040006EB RID: 1771
				internal const string Silence1 = "silence1";

				// Token: 0x040006EC RID: 1772
				internal const string Silence2 = "silence2";

				// Token: 0x040006ED RID: 1773
				internal const string SpeechError = "speechError";

				// Token: 0x040006EE RID: 1774
				internal const string Help = "help";

				// Token: 0x040006EF RID: 1775
				internal const string InvalidCommand = "invalidCommand";

				// Token: 0x040006F0 RID: 1776
				internal const string DtmfFallback = "dtmfFallback";
			}

			// Token: 0x020000E8 RID: 232
			internal abstract class PlayOnPhoneAA
			{
				// Token: 0x040006F1 RID: 1777
				internal const string FailedToSaveGreeting = "failedToSaveGreeting";
			}

			// Token: 0x020000E9 RID: 233
			internal abstract class PromptProvisioning
			{
				// Token: 0x040006F2 RID: 1778
				internal const string UpdatePromptsAllowed = "updatePromptsAllowed";

				// Token: 0x040006F3 RID: 1779
				internal const string PublishingError = "publishingError";

				// Token: 0x040006F4 RID: 1780
				internal const string InvalidSelectedPrompt = "invalidSelectedPrompt";

				// Token: 0x040006F5 RID: 1781
				internal const string EndOfHolidayPage = "endOfHolidayPage";
			}

			// Token: 0x020000EA RID: 234
			internal abstract class PersonalOptions
			{
				// Token: 0x040006F6 RID: 1782
				internal const string InvalidTimeZone = "invalidTimeZone";

				// Token: 0x040006F7 RID: 1783
				internal const string InvalidTimeFormat = "invalidTimeFormat";

				// Token: 0x040006F8 RID: 1784
				internal const string EndOfTimeZoneList = "endOfTimeZoneList";
			}

			// Token: 0x020000EB RID: 235
			internal abstract class PersonalAutoAttendant
			{
				// Token: 0x040006F9 RID: 1785
				internal const string MenuRetriesExceeded = "menuRetriesExceeded";
			}
		}

		// Token: 0x020000EC RID: 236
		internal abstract class DtmfType
		{
			// Token: 0x0600069F RID: 1695 RVA: 0x0001AF49 File Offset: 0x00019149
			private DtmfType()
			{
			}

			// Token: 0x040006FA RID: 1786
			internal const string Option = "option";

			// Token: 0x040006FB RID: 1787
			internal const string Extension = "extension";

			// Token: 0x040006FC RID: 1788
			internal const string Password = "password";

			// Token: 0x040006FD RID: 1789
			internal const string Name = "name";

			// Token: 0x040006FE RID: 1790
			internal const string Numeric = "numeric";
		}

		// Token: 0x020000ED RID: 237
		internal abstract class PromptType
		{
			// Token: 0x040006FF RID: 1791
			internal const string WaveFile = "wave";

			// Token: 0x04000700 RID: 1792
			internal const string VarWaveFile = "varwave";

			// Token: 0x04000701 RID: 1793
			internal const string TempWaveFile = "tempwave";

			// Token: 0x04000702 RID: 1794
			internal const string TextVariable = "text";

			// Token: 0x04000703 RID: 1795
			internal const string DateVariable = "date";

			// Token: 0x04000704 RID: 1796
			internal const string DigitVariable = "digit";

			// Token: 0x04000705 RID: 1797
			internal const string TimeVariable = "time";

			// Token: 0x04000706 RID: 1798
			internal const string CardinalVariable = "cardinal";

			// Token: 0x04000707 RID: 1799
			internal const string EmailVariable = "email";

			// Token: 0x04000708 RID: 1800
			internal const string Statement = "statement";

			// Token: 0x04000709 RID: 1801
			internal const string MultiStatement = "multiStatement";

			// Token: 0x0400070A RID: 1802
			internal const string Group = "group";

			// Token: 0x0400070B RID: 1803
			internal const string SimpleTime = "simpleTime";

			// Token: 0x0400070C RID: 1804
			internal const string TimeRange = "timeRange";

			// Token: 0x0400070D RID: 1805
			internal const string TelephoneNumber = "telephone";

			// Token: 0x0400070E RID: 1806
			internal const string Name = "name";

			// Token: 0x0400070F RID: 1807
			internal const string Address = "address";

			// Token: 0x04000710 RID: 1808
			internal const string Silence = "silence";

			// Token: 0x04000711 RID: 1809
			internal const string TimeZone = "timeZone";

			// Token: 0x04000712 RID: 1810
			internal const string Bookmark = "bookmark";

			// Token: 0x04000713 RID: 1811
			internal const string EmailAddress = "emailAddress";

			// Token: 0x04000714 RID: 1812
			internal const string Language = "language";

			// Token: 0x04000715 RID: 1813
			internal const string LanguageList = "languageList";

			// Token: 0x04000716 RID: 1814
			internal const string TextList = "textList";

			// Token: 0x04000717 RID: 1815
			internal const string DisambiguatedName = "disambiguatedName";

			// Token: 0x04000718 RID: 1816
			internal const string BusinessHours = "businessHours";

			// Token: 0x04000719 RID: 1817
			internal const string DayOfWeekTime = "dayOfWeekTime";

			// Token: 0x0400071A RID: 1818
			internal const string ScheduleGroup = "scheduleGroup";

			// Token: 0x0400071B RID: 1819
			internal const string DayOfWeekList = "dayOfWeekList";

			// Token: 0x0400071C RID: 1820
			internal const string ScheduleInterval = "scheduleInterval";

			// Token: 0x0400071D RID: 1821
			internal const string AACustomMenu = "aaCustomMenu";

			// Token: 0x0400071E RID: 1822
			internal const string AAWelcomeGreeting = "aaWelcomeGreeting";

			// Token: 0x0400071F RID: 1823
			internal const string AABusinessLocation = "aaBusinessLocation";

			// Token: 0x04000720 RID: 1824
			internal const string MbxVoicemailGreeting = "mbxVoicemailGreeting";

			// Token: 0x04000721 RID: 1825
			internal const string MbxAwayGreeting = "mbxAwayGreeting";

			// Token: 0x04000722 RID: 1826
			internal const string ScheduleGroupList = "scheduleGroupList";

			// Token: 0x04000723 RID: 1827
			internal const string ScheduleIntervalList = "scheduleIntervalList";

			// Token: 0x04000724 RID: 1828
			internal const string SearchItemDetail = "searchItemDetail";

			// Token: 0x04000725 RID: 1829
			internal const string CallerNameOrNumber = "callerNameOrNumber";
		}

		// Token: 0x020000EE RID: 238
		internal abstract class GrammarType
		{
			// Token: 0x04000726 RID: 1830
			internal const string Static = "static";

			// Token: 0x04000727 RID: 1831
			internal const string Dynamic = "dynamic";
		}

		// Token: 0x020000EF RID: 239
		internal abstract class RecoEvent
		{
			// Token: 0x04000728 RID: 1832
			internal const string Failure = "recoFailure";

			// Token: 0x04000729 RID: 1833
			internal const string MainMenu = "recoMainMenu";

			// Token: 0x0400072A RID: 1834
			internal const string Repeat = "recoRepeat";

			// Token: 0x0400072B RID: 1835
			internal const string Help = "recoHelp";

			// Token: 0x0400072C RID: 1836
			internal const string Yes = "recoYes";

			// Token: 0x0400072D RID: 1837
			internal const string No = "recoNo";

			// Token: 0x0400072E RID: 1838
			internal const string Goodbye = "recoGoodbye";

			// Token: 0x0400072F RID: 1839
			internal const string CompleteDate = "recoCompleteDate";

			// Token: 0x04000730 RID: 1840
			internal const string PartialDate = "recoPartialDate";

			// Token: 0x04000731 RID: 1841
			internal const string CompleteDateWithStartTime = "recoCompleteDateWithStartTime";

			// Token: 0x04000732 RID: 1842
			internal const string CompleteDateWithStartTimeAndDuration = "recoCompleteDateWithStartTimeAndDuration";

			// Token: 0x020000F0 RID: 240
			internal abstract class MainMenuQA
			{
				// Token: 0x04000733 RID: 1843
				internal const string Calendar = "recoCalendar";

				// Token: 0x04000734 RID: 1844
				internal const string Contacts = "recoContacts";

				// Token: 0x04000735 RID: 1845
				internal const string Directory = "recoDirectory";

				// Token: 0x04000736 RID: 1846
				internal const string Email = "recoEmail";

				// Token: 0x04000737 RID: 1847
				internal const string VoiceMail = "recoVoiceMail";

				// Token: 0x04000738 RID: 1848
				internal const string PersonalOptions = "recoPersonalOptions";

				// Token: 0x04000739 RID: 1849
				internal const string SendMessage = "recoSendMessage";

				// Token: 0x0400073A RID: 1850
				internal const string Call = "recoCall";
			}

			// Token: 0x020000F1 RID: 241
			internal abstract class Record
			{
				// Token: 0x0400073B RID: 1851
				internal const string PlayItBack = "recoPlayItBack";

				// Token: 0x0400073C RID: 1852
				internal const string Restart = "recoRestart";

				// Token: 0x0400073D RID: 1853
				internal const string Send = "recoSend";

				// Token: 0x0400073E RID: 1854
				internal const string SendUrgent = "recoSendUrgent";

				// Token: 0x0400073F RID: 1855
				internal const string SendPrivate = "recoSendPrivate";

				// Token: 0x04000740 RID: 1856
				internal const string SendPrivateAndImportant = "recoSendPrivateAndUrgent";

				// Token: 0x04000741 RID: 1857
				internal const string Cancel = "recoCancel";

				// Token: 0x04000742 RID: 1858
				internal const string Continue = "recoContinue";
			}

			// Token: 0x020000F2 RID: 242
			internal abstract class PlayBack
			{
				// Token: 0x04000743 RID: 1859
				internal const string Rewind = "recoRewind";
			}

			// Token: 0x020000F3 RID: 243
			internal abstract class Calendar
			{
				// Token: 0x04000744 RID: 1860
				internal const string Day = "recoDay";

				// Token: 0x04000745 RID: 1861
				internal const string Date = "recoDate";

				// Token: 0x04000746 RID: 1862
				internal const string Next = "recoNext";

				// Token: 0x04000747 RID: 1863
				internal const string NextUnread = "recoNextUnread";

				// Token: 0x04000748 RID: 1864
				internal const string Previous = "recoPrevious";

				// Token: 0x04000749 RID: 1865
				internal const string First = "recoFirst";

				// Token: 0x0400074A RID: 1866
				internal const string Last = "recoLast";

				// Token: 0x0400074B RID: 1867
				internal const string NextDay = "recoNextDay";

				// Token: 0x0400074C RID: 1868
				internal const string Accept = "recoAccept";

				// Token: 0x0400074D RID: 1869
				internal const string Decline = "recoDecline";

				// Token: 0x0400074E RID: 1870
				internal const string Cancel = "recoCancel";

				// Token: 0x0400074F RID: 1871
				internal const string CallOrganizer = "recoCallOrganizer";

				// Token: 0x04000750 RID: 1872
				internal const string ReplyOrganizer = "recoReplyOrganizer";

				// Token: 0x04000751 RID: 1873
				internal const string ReplyAll = "recoReplyAll";

				// Token: 0x04000752 RID: 1874
				internal const string CallLocation = "recoCallLocation";

				// Token: 0x04000753 RID: 1875
				internal const string SendLateMessage = "recoSendLateMessage";

				// Token: 0x04000754 RID: 1876
				internal const string SendLateMessageMinutes = "recoSendLateMessageMinutes";

				// Token: 0x04000755 RID: 1877
				internal const string MeetingDetails = "recoMeetingDetails";

				// Token: 0x04000756 RID: 1878
				internal const string ParticipantDetails = "recoParticipantDetails";

				// Token: 0x04000757 RID: 1879
				internal const string ClearCalendar = "recoClearCalendar";

				// Token: 0x04000758 RID: 1880
				internal const string MoreOptions = "recoMoreOptions";

				// Token: 0x04000759 RID: 1881
				internal const string ReadTheHeader = "recoReadTheHeader";

				// Token: 0x0400075A RID: 1882
				internal const string Minutes = "recoMinutes";

				// Token: 0x0400075B RID: 1883
				internal const string MinutesRange = "recoMinutesRange";

				// Token: 0x0400075C RID: 1884
				internal const string NotSure = "recoNotSure";

				// Token: 0x0400075D RID: 1885
				internal const string TimePhrase = "recoTimePhrase";

				// Token: 0x0400075E RID: 1886
				internal const string DayPhrase = "recoDayPhrase";

				// Token: 0x0400075F RID: 1887
				internal const string AmbiguousPhrase = "recoAmbiguousPhrase";

				// Token: 0x04000760 RID: 1888
				internal const string TimeOfDay = "recoTimeOfDay";

				// Token: 0x04000761 RID: 1889
				internal const string NumberOfDays = "recoNumberOfDays";
			}

			// Token: 0x020000F4 RID: 244
			internal abstract class AutoAttendant
			{
				// Token: 0x04000762 RID: 1890
				internal const string RecoName = "recoName";

				// Token: 0x04000763 RID: 1891
				internal const string RecoDepartment = "recoDepartment";

				// Token: 0x04000764 RID: 1892
				internal const string RecoNameOrDepartment = "recoNameOrDepartment";

				// Token: 0x04000765 RID: 1893
				internal const string RecoReception = "recoReception";

				// Token: 0x04000766 RID: 1894
				internal const string RecoNotSure = "recoNotSure";

				// Token: 0x04000767 RID: 1895
				internal const string RecoMaybe = "recoMaybe";

				// Token: 0x04000768 RID: 1896
				internal const string RecoPoliteEnd = "recoPoliteEnd";

				// Token: 0x04000769 RID: 1897
				internal const string RecoChoice = "recoChoice";

				// Token: 0x0400076A RID: 1898
				internal const string RecoNotListed = "recoNotListed";

				// Token: 0x0400076B RID: 1899
				internal const string RecoSendMessage = "recoSendMessage";
			}

			// Token: 0x020000F5 RID: 245
			internal abstract class Email
			{
				// Token: 0x0400076C RID: 1900
				internal const string Reply = "recoReply";

				// Token: 0x0400076D RID: 1901
				internal const string ReplyAll = "recoReplyAll";

				// Token: 0x0400076E RID: 1902
				internal const string CallSender = "recoCallSender";

				// Token: 0x0400076F RID: 1903
				internal const string Forward = "recoForward";

				// Token: 0x04000770 RID: 1904
				internal const string ForwardWithName = "recoForwardWithName";

				// Token: 0x04000771 RID: 1905
				internal const string ForwardToContact = "recoForwardToContact";

				// Token: 0x04000772 RID: 1906
				internal const string FindByContact = "recoFindByContact";

				// Token: 0x04000773 RID: 1907
				internal const string FindByName = "recoFindByName";

				// Token: 0x04000774 RID: 1908
				internal const string FindByNameWithName = "recoFindByNameWithName";

				// Token: 0x04000775 RID: 1909
				internal const string Delete = "recoDelete";

				// Token: 0x04000776 RID: 1910
				internal const string Undelete = "recoUndelete";

				// Token: 0x04000777 RID: 1911
				internal const string MarkAsUnread = "recoMarkAsUnread";

				// Token: 0x04000778 RID: 1912
				internal const string Next = "recoNext";

				// Token: 0x04000779 RID: 1913
				internal const string Previous = "recoPrevious";

				// Token: 0x0400077A RID: 1914
				internal const string HideThread = "recoHideThread";

				// Token: 0x0400077B RID: 1915
				internal const string DeleteThread = "recoDeleteThread";

				// Token: 0x0400077C RID: 1916
				internal const string FlagForFollowup = "recoFlagForFollowup";

				// Token: 0x0400077D RID: 1917
				internal const string Play = "recoPlay";

				// Token: 0x0400077E RID: 1918
				internal const string FastForward = "recoFastForward";

				// Token: 0x0400077F RID: 1919
				internal const string Rewind = "recoRewind";

				// Token: 0x04000780 RID: 1920
				internal const string SpeedUp = "recoSpeedUp";

				// Token: 0x04000781 RID: 1921
				internal const string SlowDown = "recoSlowDown";

				// Token: 0x04000782 RID: 1922
				internal const string SelectLanguage = "recoSelectLanguage";

				// Token: 0x04000783 RID: 1923
				internal const string Accept = "recoAccept";

				// Token: 0x04000784 RID: 1924
				internal const string AcceptTentative = "recoAcceptTentative";

				// Token: 0x04000785 RID: 1925
				internal const string Decline = "recoDecline";

				// Token: 0x04000786 RID: 1926
				internal const string MoreOptions = "recoMoreOptions";

				// Token: 0x04000787 RID: 1927
				internal const string MessageDetails = "recoMessageDetails";

				// Token: 0x04000788 RID: 1928
				internal const string Pause = "recoPause";

				// Token: 0x04000789 RID: 1929
				internal const string ReadTheHeader = "recoReadTheHeader";

				// Token: 0x0400078A RID: 1930
				internal const string EndOfMessage = "recoEndOfMessage";

				// Token: 0x0400078B RID: 1931
				internal const string Language = "recoLanguage";

				// Token: 0x0400078C RID: 1932
				internal const string EnvelopeInfo = "recoEnvelopeInfo";
			}

			// Token: 0x020000F6 RID: 246
			internal abstract class Contacts
			{
				// Token: 0x0400078D RID: 1933
				internal const string RecoReadDetails = "recoReadDetails";

				// Token: 0x0400078E RID: 1934
				internal const string RecoCallCell = "recoCallCell";

				// Token: 0x0400078F RID: 1935
				internal const string RecoCallOffice = "recoCallOffice";

				// Token: 0x04000790 RID: 1936
				internal const string RecoCallHome = "recoCallHome";

				// Token: 0x04000791 RID: 1937
				internal const string RecoSendMessage = "recoSendMessage";

				// Token: 0x04000792 RID: 1938
				internal const string RecoFindAnotherContact = "recoFindAnotherContact";
			}
		}

		// Token: 0x020000F7 RID: 247
		internal abstract class RpcError
		{
			// Token: 0x04000793 RID: 1939
			internal const int EndpointNotRegistered = 1753;

			// Token: 0x04000794 RID: 1940
			internal const int RpcServerCallFailedDne = 1727;
		}

		// Token: 0x020000F8 RID: 248
		internal abstract class SemanticItems
		{
			// Token: 0x04000795 RID: 1941
			internal const string RecoEvent = "RecoEvent";

			// Token: 0x04000796 RID: 1942
			internal const string Extension = "Extension";

			// Token: 0x04000797 RID: 1943
			internal const string ResultType = "ResultType";

			// Token: 0x04000798 RID: 1944
			internal const string DepartmentName = "DepartmentName";

			// Token: 0x04000799 RID: 1945
			internal const string DirectoryContact = "DirectoryContact";

			// Token: 0x0400079A RID: 1946
			internal const string Department = "Department";

			// Token: 0x0400079B RID: 1947
			internal const string CustomMenuTarget = "CustomMenuTarget";

			// Token: 0x0400079C RID: 1948
			internal const string MappedKey = "MappedKey";

			// Token: 0x0400079D RID: 1949
			internal const string PromptFileName = "PromptFileName";

			// Token: 0x0400079E RID: 1950
			internal const string PersonalContact = "PersonalContact";

			// Token: 0x0400079F RID: 1951
			internal const string SMTP = "SMTP";

			// Token: 0x040007A0 RID: 1952
			internal const string PersonId = "PersonId";

			// Token: 0x040007A1 RID: 1953
			internal const string GalLinkId = "GALLinkID";

			// Token: 0x040007A2 RID: 1954
			internal const string ObjectGuid = "ObjectGuid";

			// Token: 0x040007A3 RID: 1955
			internal const string UmSubscriberObjectGuid = "UmSubscriberObjectGuid";

			// Token: 0x040007A4 RID: 1956
			internal const string ContactId = "ContactId";

			// Token: 0x040007A5 RID: 1957
			internal const string ContactName = "ContactName";

			// Token: 0x040007A6 RID: 1958
			internal const string DisambiguationField = "DisambiguationField";

			// Token: 0x040007A7 RID: 1959
			internal const string Choice = "Choice";

			// Token: 0x040007A8 RID: 1960
			internal const string Language = "Language";

			// Token: 0x020000F9 RID: 249
			internal abstract class Calendar
			{
				// Token: 0x040007A9 RID: 1961
				internal const string SpokenDay = "SpokenDay";

				// Token: 0x040007AA RID: 1962
				internal const string RelativeDayOffset = "RelativeDayOffset";

				// Token: 0x040007AB RID: 1963
				internal const string Month = "Month";

				// Token: 0x040007AC RID: 1964
				internal const string Day = "Day";

				// Token: 0x040007AD RID: 1965
				internal const string Year = "Year";

				// Token: 0x040007AE RID: 1966
				internal const string Minutes = "Minutes";

				// Token: 0x040007AF RID: 1967
				internal const string RangeStart = "RangeStart";

				// Token: 0x040007B0 RID: 1968
				internal const string RangeEnd = "RangeEnd";

				// Token: 0x040007B1 RID: 1969
				internal const string Time = "Time";

				// Token: 0x040007B2 RID: 1970
				internal const string Days = "Days";

				// Token: 0x040007B3 RID: 1971
				internal const string Hour = "Hour";

				// Token: 0x040007B4 RID: 1972
				internal const string AlternateHour = "AlternateHour";

				// Token: 0x040007B5 RID: 1973
				internal const string Minute = "Minute";

				// Token: 0x040007B6 RID: 1974
				internal const string DurationInMinutes = "DurationInMinutes";

				// Token: 0x040007B7 RID: 1975
				internal const string StartHour = "StartHour";

				// Token: 0x040007B8 RID: 1976
				internal const string StartMinute = "StartMinute";

				// Token: 0x040007B9 RID: 1977
				internal const string IsStartHourRelative = "IsStartHourRelative";
			}

			// Token: 0x020000FA RID: 250
			internal abstract class Email
			{
			}
		}

		// Token: 0x020000FB RID: 251
		internal abstract class PromptLimits
		{
			// Token: 0x040007BA RID: 1978
			internal const int DefaultPromptLimit = 5;

			// Token: 0x040007BB RID: 1979
			internal const string CalendarDateHint = "calendarDateHint";

			// Token: 0x040007BC RID: 1980
			internal const string MailForwardHint = "mailForwardHint";

			// Token: 0x040007BD RID: 1981
			internal const string MailFindHint = "mailFindHint";

			// Token: 0x040007BE RID: 1982
			internal const string VuiUndeleteHint = "vuiUndeleteHint";

			// Token: 0x040007BF RID: 1983
			internal const string DtmfDirectoryHint = "dtmfDirectoryHint";
		}

		// Token: 0x020000FC RID: 252
		internal abstract class MobileSpeechRecognition
		{
			// Token: 0x040007C0 RID: 1984
			internal const string MobilePeopleSearchGrammarRuleName = "MobilePeopleSearch";

			// Token: 0x040007C1 RID: 1985
			internal const string FindPersonByNameMobileGrammarRuleName = "FindPersonByNameMobile";

			// Token: 0x040007C2 RID: 1986
			internal const string MobileRecoElementName = "MobileReco";

			// Token: 0x040007C3 RID: 1987
			internal const string AlternateElementName = "Alternate";

			// Token: 0x040007C4 RID: 1988
			internal const string TextAttributeName = "text";

			// Token: 0x040007C5 RID: 1989
			internal const string PersonalContactSearchElementName = "PersonalContactSearch";

			// Token: 0x040007C6 RID: 1990
			internal const string GALSearchElementName = "GALSearch";

			// Token: 0x040007C7 RID: 1991
			internal const string ResultTypeAttributeName = "ResultType";

			// Token: 0x040007C8 RID: 1992
			internal const string ConfidenceAttributeName = "confidence";

			// Token: 0x040007C9 RID: 1993
			internal const string DaySearchBehaviorRuleName = "DaySearch";

			// Token: 0x040007CA RID: 1994
			internal const string DateTimeAndDurationRecognitionRuleName = "DateTimeAndDurationRecognition";

			// Token: 0x040007CB RID: 1995
			internal const string MowaGrammarName = "Mowascenarios.grxml";

			// Token: 0x040007CC RID: 1996
			internal const string PeopleSearchResultsXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><MobileReco ResultType=\"{4}\"><{0}>{1}</{0}><{2}>{3}</{2}></MobileReco>";
		}
	}
}
