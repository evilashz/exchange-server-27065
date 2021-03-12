using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200004A RID: 74
	[DataContract]
	[Serializable]
	public sealed class SessionStatistics : XMLSerializableBase, ICloneable
	{
		// Token: 0x06000374 RID: 884 RVA: 0x00006237 File Offset: 0x00004437
		public SessionStatistics()
		{
			this.SourceProviderInfo = new ProviderInfo();
			this.DestinationProviderInfo = new ProviderInfo();
			this.SourceLatencyInfo = new LatencyInfo();
			this.DestinationLatencyInfo = new LatencyInfo();
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000626B File Offset: 0x0000446B
		// (set) Token: 0x06000376 RID: 886 RVA: 0x00006273 File Offset: 0x00004473
		[XmlIgnore]
		[IgnoreDataMember]
		public TimeSpan TotalTimeProcessingMessages { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000627C File Offset: 0x0000447C
		// (set) Token: 0x06000378 RID: 888 RVA: 0x00006284 File Offset: 0x00004484
		[XmlIgnore]
		[IgnoreDataMember]
		public TimeSpan TimeInGetConnection { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000628D File Offset: 0x0000448D
		// (set) Token: 0x0600037A RID: 890 RVA: 0x00006295 File Offset: 0x00004495
		[XmlIgnore]
		[IgnoreDataMember]
		public TimeSpan TimeInPropertyBagLoad { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000629E File Offset: 0x0000449E
		// (set) Token: 0x0600037C RID: 892 RVA: 0x000062A6 File Offset: 0x000044A6
		[XmlIgnore]
		[IgnoreDataMember]
		public TimeSpan TimeInMessageItemConversion { get; set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600037D RID: 893 RVA: 0x000062AF File Offset: 0x000044AF
		// (set) Token: 0x0600037E RID: 894 RVA: 0x000062B7 File Offset: 0x000044B7
		[IgnoreDataMember]
		[XmlIgnore]
		public TimeSpan TimeDeterminingAgeOfItem { get; set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600037F RID: 895 RVA: 0x000062C0 File Offset: 0x000044C0
		// (set) Token: 0x06000380 RID: 896 RVA: 0x000062C8 File Offset: 0x000044C8
		[IgnoreDataMember]
		[XmlIgnore]
		public TimeSpan TimeInMimeConversion { get; set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000381 RID: 897 RVA: 0x000062D1 File Offset: 0x000044D1
		// (set) Token: 0x06000382 RID: 898 RVA: 0x000062D9 File Offset: 0x000044D9
		[IgnoreDataMember]
		[XmlIgnore]
		public TimeSpan TimeInShouldAnnotateMessage { get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000383 RID: 899 RVA: 0x000062E2 File Offset: 0x000044E2
		// (set) Token: 0x06000384 RID: 900 RVA: 0x000062EA File Offset: 0x000044EA
		[XmlIgnore]
		[IgnoreDataMember]
		public TimeSpan TimeInQueue { get; set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000385 RID: 901 RVA: 0x000062F3 File Offset: 0x000044F3
		// (set) Token: 0x06000386 RID: 902 RVA: 0x000062FB File Offset: 0x000044FB
		[XmlIgnore]
		[IgnoreDataMember]
		public TimeSpan TimeInTransportRetriever { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00006304 File Offset: 0x00004504
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0000630C File Offset: 0x0000450C
		[IgnoreDataMember]
		[XmlIgnore]
		public TimeSpan TimeInDocParser { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00006315 File Offset: 0x00004515
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0000631D File Offset: 0x0000451D
		[XmlIgnore]
		[IgnoreDataMember]
		public TimeSpan TimeInWordbreaker { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00006326 File Offset: 0x00004526
		// (set) Token: 0x0600038C RID: 908 RVA: 0x0000632E File Offset: 0x0000452E
		[XmlIgnore]
		[IgnoreDataMember]
		public TimeSpan TimeInNLGSubflow { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00006337 File Offset: 0x00004537
		// (set) Token: 0x0600038E RID: 910 RVA: 0x0000633F File Offset: 0x0000453F
		[XmlIgnore]
		[IgnoreDataMember]
		public TimeSpan TimeProcessingFailedMessages { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00006348 File Offset: 0x00004548
		// (set) Token: 0x06000390 RID: 912 RVA: 0x00006350 File Offset: 0x00004550
		[XmlIgnore]
		[IgnoreDataMember]
		public TimeSpan PreFinalSyncDataProcessingDuration { get; set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000635C File Offset: 0x0000455C
		// (set) Token: 0x06000392 RID: 914 RVA: 0x00006377 File Offset: 0x00004577
		[DataMember(Name = "TotalTimeProcessingMessagesTicks", IsRequired = false)]
		[XmlElement(ElementName = "TotalTimeProcessingMessagesTicks")]
		public long TotalTimeProcessingMessagesTicks
		{
			get
			{
				return this.TotalTimeProcessingMessages.Ticks;
			}
			set
			{
				this.TotalTimeProcessingMessages = new TimeSpan(value);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00006388 File Offset: 0x00004588
		// (set) Token: 0x06000394 RID: 916 RVA: 0x000063A3 File Offset: 0x000045A3
		[DataMember(Name = "TimeInGetConnectionTicks", IsRequired = false)]
		[XmlElement(ElementName = "TimeInGetConnectionTicks")]
		public long TimeInGetConnectionTicks
		{
			get
			{
				return this.TimeInGetConnection.Ticks;
			}
			set
			{
				this.TimeInGetConnection = new TimeSpan(value);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000395 RID: 917 RVA: 0x000063B4 File Offset: 0x000045B4
		// (set) Token: 0x06000396 RID: 918 RVA: 0x000063CF File Offset: 0x000045CF
		[XmlElement(ElementName = "TimeInPropertyBagLoadTicks")]
		[DataMember(Name = "TimeInPropertyBagLoadTicks", IsRequired = false)]
		public long TimeInPropertyBagLoadTicks
		{
			get
			{
				return this.TimeInPropertyBagLoad.Ticks;
			}
			set
			{
				this.TimeInPropertyBagLoad = new TimeSpan(value);
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000397 RID: 919 RVA: 0x000063E0 File Offset: 0x000045E0
		// (set) Token: 0x06000398 RID: 920 RVA: 0x000063FB File Offset: 0x000045FB
		[XmlElement(ElementName = "TimeInMessageItemConversionTicks")]
		[DataMember(Name = "TimeInMessageItemConversionTicks", IsRequired = false)]
		public long TimeInMessageItemConversionTicks
		{
			get
			{
				return this.TimeInMessageItemConversion.Ticks;
			}
			set
			{
				this.TimeInMessageItemConversion = new TimeSpan(value);
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000640C File Offset: 0x0000460C
		// (set) Token: 0x0600039A RID: 922 RVA: 0x00006427 File Offset: 0x00004627
		[DataMember(Name = "TimeDeterminingAgeOfItemTicks", IsRequired = false)]
		[XmlElement(ElementName = "TimeDeterminingAgeOfItemTicks")]
		public long TimeDeterminingAgeOfItemTicks
		{
			get
			{
				return this.TimeDeterminingAgeOfItem.Ticks;
			}
			set
			{
				this.TimeDeterminingAgeOfItem = new TimeSpan(value);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00006438 File Offset: 0x00004638
		// (set) Token: 0x0600039C RID: 924 RVA: 0x00006453 File Offset: 0x00004653
		[XmlElement(ElementName = "TimeInMimeConversionTicks")]
		[DataMember(Name = "TimeInMimeConversionTicks", IsRequired = false)]
		public long TimeInMimeConversionTicks
		{
			get
			{
				return this.TimeInMimeConversion.Ticks;
			}
			set
			{
				this.TimeInMimeConversion = new TimeSpan(value);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00006464 File Offset: 0x00004664
		// (set) Token: 0x0600039E RID: 926 RVA: 0x0000647F File Offset: 0x0000467F
		[DataMember(Name = "TimeInShouldAnnotateMessageTicks", IsRequired = false)]
		[XmlElement(ElementName = "TimeInShouldAnnotateMessageTicks")]
		public long TimeInShouldAnnotateMessageTicks
		{
			get
			{
				return this.TimeInShouldAnnotateMessage.Ticks;
			}
			set
			{
				this.TimeInShouldAnnotateMessage = new TimeSpan(value);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00006490 File Offset: 0x00004690
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x000064AB File Offset: 0x000046AB
		[XmlElement(ElementName = "TimeInQueueTicks")]
		[DataMember(Name = "TimeInQueueTicks", IsRequired = false)]
		public long TimeInQueueTicks
		{
			get
			{
				return this.TimeInQueue.Ticks;
			}
			set
			{
				this.TimeInQueue = new TimeSpan(value);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x000064BC File Offset: 0x000046BC
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x000064D7 File Offset: 0x000046D7
		[DataMember(Name = "TimeInTransportRetrieverTicks", IsRequired = false)]
		[XmlElement(ElementName = "TimeInTransportRetrieverTicks")]
		public long TimeInTransportRetrieverTicks
		{
			get
			{
				return this.TimeInTransportRetriever.Ticks;
			}
			set
			{
				this.TimeInTransportRetriever = new TimeSpan(value);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x000064E8 File Offset: 0x000046E8
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x00006503 File Offset: 0x00004703
		[XmlElement(ElementName = "TimeInDocParserTicks")]
		[DataMember(Name = "TimeInDocParserTicks", IsRequired = false)]
		public long TimeInDocParserTicks
		{
			get
			{
				return this.TimeInDocParser.Ticks;
			}
			set
			{
				this.TimeInDocParser = new TimeSpan(value);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00006514 File Offset: 0x00004714
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x0000652F File Offset: 0x0000472F
		[XmlElement(ElementName = "TimeInWordbreakerTicks")]
		[DataMember(Name = "TimeInWordbreakerTicks", IsRequired = false)]
		public long TimeInWordbreakerTicks
		{
			get
			{
				return this.TimeInWordbreaker.Ticks;
			}
			set
			{
				this.TimeInWordbreaker = new TimeSpan(value);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x00006540 File Offset: 0x00004740
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x0000655B File Offset: 0x0000475B
		[XmlElement(ElementName = "TimeInNLGSubflowTicks")]
		[DataMember(Name = "TimeInNLGSubflowTicks", IsRequired = false)]
		public long TimeInNLGSubflowTicks
		{
			get
			{
				return this.TimeInNLGSubflow.Ticks;
			}
			set
			{
				this.TimeInNLGSubflow = new TimeSpan(value);
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000656C File Offset: 0x0000476C
		// (set) Token: 0x060003AA RID: 938 RVA: 0x00006587 File Offset: 0x00004787
		[XmlElement(ElementName = "TimeProcessingFailedMessagesTicks")]
		[DataMember(Name = "TimeProcessingFailedMessagesTicks", IsRequired = false)]
		public long TimeProcessingFailedMessagesTicks
		{
			get
			{
				return this.TimeProcessingFailedMessages.Ticks;
			}
			set
			{
				this.TimeProcessingFailedMessages = new TimeSpan(value);
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00006598 File Offset: 0x00004798
		// (set) Token: 0x060003AC RID: 940 RVA: 0x000065B3 File Offset: 0x000047B3
		[DataMember(Name = "PreFinalSyncDataProcessingDurationTicks", IsRequired = false)]
		[XmlElement(ElementName = "PreFinalSyncDataProcessingDurationTicks")]
		public long PreFinalSyncDataProcessingDurationTicks
		{
			get
			{
				return this.PreFinalSyncDataProcessingDuration.Ticks;
			}
			set
			{
				this.PreFinalSyncDataProcessingDuration = new TimeSpan(value);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060003AD RID: 941 RVA: 0x000065C1 File Offset: 0x000047C1
		// (set) Token: 0x060003AE RID: 942 RVA: 0x000065C9 File Offset: 0x000047C9
		[DataMember(Name = "TotalMessagesProcessed", IsRequired = false)]
		[XmlElement(ElementName = "TotalMessagesProcessed")]
		public int TotalMessagesProcessed { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060003AF RID: 943 RVA: 0x000065D2 File Offset: 0x000047D2
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x000065DA File Offset: 0x000047DA
		[DataMember(Name = "MessagesSuccessfullyAnnotated", IsRequired = false)]
		[XmlElement(ElementName = "MessagesSuccessfullyAnnotated")]
		public int MessagesSuccessfullyAnnotated { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x000065E3 File Offset: 0x000047E3
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x000065EB File Offset: 0x000047EB
		[XmlElement(ElementName = "AnnotationSkipped")]
		[DataMember(Name = "AnnotationSkipped", IsRequired = false)]
		public int AnnotationSkipped { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x000065F4 File Offset: 0x000047F4
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x000065FC File Offset: 0x000047FC
		[DataMember(Name = "ConnectionLevelFailures", IsRequired = false)]
		[XmlElement(ElementName = "ConnectionLevelFailures")]
		public int ConnectionLevelFailures { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00006605 File Offset: 0x00004805
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x0000660D File Offset: 0x0000480D
		[DataMember(Name = "MessageLevelFailures", IsRequired = false)]
		[XmlElement(ElementName = "MessageLevelFailures")]
		public int MessageLevelFailures { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00006616 File Offset: 0x00004816
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x0000661E File Offset: 0x0000481E
		[DataMember(Name = "MapiDiagnosticGetProp", IsRequired = false)]
		[XmlElement(ElementName = "MapiDiagnosticGetProp")]
		public string MapiDiagnosticGetProp { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x00006627 File Offset: 0x00004827
		// (set) Token: 0x060003BA RID: 954 RVA: 0x0000662F File Offset: 0x0000482F
		[XmlElement(ElementName = "SessionId")]
		[DataMember(Name = "SessionId", IsRequired = false)]
		public string SessionId { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060003BB RID: 955 RVA: 0x00006638 File Offset: 0x00004838
		// (set) Token: 0x060003BC RID: 956 RVA: 0x00006640 File Offset: 0x00004840
		[DataMember(Name = "SourceLatencyInfo", IsRequired = false)]
		[XmlElement(ElementName = "SourceLatencyInfo")]
		public LatencyInfo SourceLatencyInfo { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060003BD RID: 957 RVA: 0x00006649 File Offset: 0x00004849
		// (set) Token: 0x060003BE RID: 958 RVA: 0x00006651 File Offset: 0x00004851
		[XmlElement(ElementName = "DestinationLatencyInfo")]
		[DataMember(Name = "DestinationLatencyInfo", IsRequired = false)]
		public LatencyInfo DestinationLatencyInfo { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000665A File Offset: 0x0000485A
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x00006662 File Offset: 0x00004862
		[DataMember(Name = "SourceProviderInfo", IsRequired = false)]
		[XmlElement(ElementName = "SourceProviderInfo")]
		public ProviderInfo SourceProviderInfo { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000666B File Offset: 0x0000486B
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x00006673 File Offset: 0x00004873
		[DataMember(Name = "DestinationProviderInfo", IsRequired = false)]
		[XmlElement(ElementName = "DestinationProviderInfo")]
		public ProviderInfo DestinationProviderInfo { get; set; }

		// Token: 0x060003C3 RID: 963 RVA: 0x0000667C File Offset: 0x0000487C
		public static SessionStatistics operator +(SessionStatistics stats1, SessionStatistics stats2)
		{
			return new SessionStatistics
			{
				SessionId = stats2.SessionId,
				TotalMessagesProcessed = stats1.TotalMessagesProcessed + stats2.TotalMessagesProcessed,
				TotalTimeProcessingMessages = stats1.TotalTimeProcessingMessages + stats2.TotalTimeProcessingMessages,
				TimeInGetConnection = stats1.TimeInGetConnection + stats2.TimeInGetConnection,
				TimeInPropertyBagLoad = stats1.TimeInPropertyBagLoad + stats2.TimeInPropertyBagLoad,
				TimeInMessageItemConversion = stats1.TimeInMessageItemConversion + stats2.TimeInMessageItemConversion,
				TimeDeterminingAgeOfItem = stats1.TimeDeterminingAgeOfItem + stats2.TimeDeterminingAgeOfItem,
				TimeInMimeConversion = stats1.TimeInMimeConversion + stats2.TimeInMimeConversion,
				TimeInShouldAnnotateMessage = stats1.TimeInShouldAnnotateMessage + stats2.TimeInShouldAnnotateMessage,
				TimeInWordbreaker = stats1.TimeInWordbreaker + stats2.TimeInWordbreaker,
				TimeInQueue = stats1.TimeInQueue + stats2.TimeInQueue,
				TimeProcessingFailedMessages = stats1.TimeProcessingFailedMessages + stats2.TimeProcessingFailedMessages,
				TimeInTransportRetriever = stats1.TimeInTransportRetriever + stats2.TimeInTransportRetriever,
				TimeInDocParser = stats1.TimeInDocParser + stats2.TimeInDocParser,
				TimeInNLGSubflow = stats1.TimeInNLGSubflow + stats2.TimeInNLGSubflow,
				MessageLevelFailures = stats1.MessageLevelFailures + stats2.MessageLevelFailures,
				MessagesSuccessfullyAnnotated = stats1.MessagesSuccessfullyAnnotated + stats2.MessagesSuccessfullyAnnotated,
				AnnotationSkipped = stats1.AnnotationSkipped + stats2.AnnotationSkipped,
				ConnectionLevelFailures = stats1.ConnectionLevelFailures + stats2.ConnectionLevelFailures,
				PreFinalSyncDataProcessingDuration = stats2.PreFinalSyncDataProcessingDuration,
				SourceLatencyInfo = stats1.SourceLatencyInfo + stats2.SourceLatencyInfo,
				DestinationLatencyInfo = stats1.DestinationLatencyInfo + stats2.DestinationLatencyInfo,
				SourceProviderInfo = stats1.SourceProviderInfo + stats2.SourceProviderInfo,
				DestinationProviderInfo = stats1.DestinationProviderInfo + stats2.DestinationProviderInfo
			};
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00006890 File Offset: 0x00004A90
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("--------------------------");
			stringBuilder.AppendLine("Source Provider Durations: " + this.SourceProviderInfo.TotalDuration);
			stringBuilder.AppendLine("--------------------------");
			foreach (DurationInfo durationInfo in this.SourceProviderInfo.Durations)
			{
				stringBuilder.AppendLine(string.Format("{0}: {1}", durationInfo.Name, durationInfo.Duration));
			}
			stringBuilder.AppendLine("--------------------------");
			stringBuilder.AppendLine("Destination Provider Durations:" + this.DestinationProviderInfo.TotalDuration);
			stringBuilder.AppendLine("--------------------------");
			foreach (DurationInfo durationInfo2 in this.DestinationProviderInfo.Durations)
			{
				stringBuilder.AppendLine(string.Format("{0}: {1}", durationInfo2.Name, durationInfo2.Duration));
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			stringBuilder2.AppendLine("--------------------------");
			stringBuilder2.AppendLine("CI WordBreaking Stats:");
			stringBuilder2.AppendLine("--------------------------");
			stringBuilder2.AppendLine("TotalMessagesProcessed: " + this.TotalMessagesProcessed);
			stringBuilder2.Append("TotalTimeProcessingMessages: " + this.TotalTimeProcessingMessages);
			stringBuilder2.Append("TimeInGetConnection: " + this.TimeInGetConnection);
			stringBuilder2.Append("TimeInPropertyBagLoad: " + this.TimeInPropertyBagLoad);
			stringBuilder2.Append("TimeInMessageItemConversion: " + this.TimeInMessageItemConversion);
			stringBuilder2.Append("TimeDeterminingAgeOfItem: " + this.TimeDeterminingAgeOfItem);
			stringBuilder2.Append("TimeInMimeConversion: " + this.TimeInMimeConversion);
			stringBuilder2.Append("TimeInShouldAnnotateMessage: " + this.TimeInShouldAnnotateMessage);
			stringBuilder2.Append("TimeInQueue: " + this.TimeInQueue);
			stringBuilder2.Append("TimeInTransportRetriever: " + this.TimeInTransportRetriever);
			stringBuilder2.Append("TimeInDocParser: " + this.TimeInDocParser);
			stringBuilder2.Append("TimeInWordbreaker: " + this.TimeInWordbreaker);
			stringBuilder2.Append("TimeInNLGSubflow: " + this.TimeInNLGSubflow);
			stringBuilder2.Append("TimeProcessingFailedMessages: " + this.TimeProcessingFailedMessages);
			stringBuilder2.Append("AnnotationSkipped: " + this.AnnotationSkipped);
			stringBuilder2.Append("MessageLevelFailures: " + this.MessageLevelFailures);
			stringBuilder2.Append("ConnectionLevelFailures: " + this.ConnectionLevelFailures);
			return string.Concat(new object[]
			{
				"SessionId: ",
				this.SessionId,
				Environment.NewLine,
				stringBuilder2.ToString(),
				Environment.NewLine,
				"SourceLatencyInfo:",
				Environment.NewLine,
				"------------------",
				Environment.NewLine,
				this.SourceLatencyInfo,
				Environment.NewLine,
				"DestinationLatencyInfo:",
				Environment.NewLine,
				"------------------",
				Environment.NewLine,
				this.DestinationLatencyInfo,
				Environment.NewLine,
				stringBuilder.ToString(),
				Environment.NewLine,
				"PreFinalSyncDataProcessingDuration: ",
				this.PreFinalSyncDataProcessingDuration
			});
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00006CCC File Offset: 0x00004ECC
		public object Clone()
		{
			object result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
				binaryFormatter.Serialize(memoryStream, this);
				memoryStream.Position = 0L;
				result = binaryFormatter.Deserialize(memoryStream);
			}
			return result;
		}
	}
}
