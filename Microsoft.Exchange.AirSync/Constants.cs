using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000048 RID: 72
	internal static class Constants
	{
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0001C6DA File Offset: 0x0001A8DA
		internal static string ProtocolVersionsHeaderValue
		{
			get
			{
				if (GlobalSettings.EnableV160)
				{
					return Constants.ProtocolExperimantalVersionsHeaderValue;
				}
				return "2.0,2.1,2.5,12.0,12.1,14.0,14.1";
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0001C6EE File Offset: 0x0001A8EE
		internal static string ProtocolExperimantalVersionsHeaderValue
		{
			get
			{
				return "2.0,2.1,2.5,12.0,12.1,14.0,14.1,16.0";
			}
		}

		// Token: 0x0400031A RID: 794
		internal const string Exchange12Provider = "Exchange12";

		// Token: 0x0400031B RID: 795
		internal const string WbxmlHeader = "application/vnd.ms-sync.wbxml";

		// Token: 0x0400031C RID: 796
		internal const string MultiPartContentType = "application/vnd.ms-sync.multipart";

		// Token: 0x0400031D RID: 797
		internal const string TextHtmlContentType = "text/html";

		// Token: 0x0400031E RID: 798
		internal const string AcceptMultiPartHeader = "MS-ASAcceptMultiPart";

		// Token: 0x0400031F RID: 799
		internal const string ProtocolVersionsHeader = "MS-ASProtocolVersions";

		// Token: 0x04000320 RID: 800
		internal const string ProtocolCommandsHeader = "MS-ASProtocolCommands";

		// Token: 0x04000321 RID: 801
		internal const string OutlookExtensionsHeader = "X-OLK-Extensions";

		// Token: 0x04000322 RID: 802
		internal const string OutlookExtensionHeader = "X-OLK-Extension";

		// Token: 0x04000323 RID: 803
		internal const string OutlookExtensionsHeaderValue = "1=0E47";

		// Token: 0x04000324 RID: 804
		internal const string MinorVersion = "14.1.127";

		// Token: 0x04000325 RID: 805
		internal const string ProtocolOTAUpdateHeader = "X-MS-OTAUpdate";

		// Token: 0x04000326 RID: 806
		internal const string DirectPushOffHeader = "X-MS-NoPush";

		// Token: 0x04000327 RID: 807
		internal const string ProtocolCommandsHeaderValue = "Sync,SendMail,SmartForward,SmartReply,GetAttachment,GetHierarchy,CreateCollection,DeleteCollection,MoveCollection,FolderSync,FolderCreate,FolderDelete,FolderUpdate,MoveItems,GetItemEstimate,MeetingResponse,Search,Settings,Ping,ItemOperations,Provision,ResolveRecipients,ValidateCert";

		// Token: 0x04000328 RID: 808
		internal const string ProtocolCommandsHeaderConsumerVersion25OnlyValue = "Sync,SendMail,SmartForward,SmartReply,GetAttachment,FolderSync,FolderCreate,FolderDelete,FolderUpdate,MoveItems,GetItemEstimate,MeetingResponse,Ping";

		// Token: 0x04000329 RID: 809
		internal const string ProtocolCommandsHeaderConsumerContactsOnlyValue = "Sync,FolderSync,GetItemEstimate,Ping";

		// Token: 0x0400032A RID: 810
		internal const string ProtocolCommandsHeaderConsumerValue = "Sync,SendMail,SmartForward,SmartReply,GetAttachment,FolderSync,FolderCreate,FolderDelete,FolderUpdate,MoveItems,GetItemEstimate,MeetingResponse,Search,Settings,Ping,ItemOperations";

		// Token: 0x0400032B RID: 811
		internal const string DiagnosticDataHeader = "X-MS-Diagnostics";

		// Token: 0x0400032C RID: 812
		internal const string BackOffTimeHeader = "X-MS-BackOffDuration";

		// Token: 0x0400032D RID: 813
		internal const string BackOffReasonHeader = "X-MS-BackOffReason";

		// Token: 0x0400032E RID: 814
		internal const string DeviceTypeParam = "DeviceType";

		// Token: 0x0400032F RID: 815
		internal const string TestActiveSyncConnectivityDeviceType = "TestActiveSyncConnectivity";

		// Token: 0x04000330 RID: 816
		internal const string TestActiveSyncConnectivityUserAgent = "TestActiveSyncConnectivity";

		// Token: 0x04000331 RID: 817
		internal const string DeviceIdParam = "DeviceId";

		// Token: 0x04000332 RID: 818
		internal const string MailboxIdParam = "MailboxId";

		// Token: 0x04000333 RID: 819
		internal const string ContentType = "Content-Type";

		// Token: 0x04000334 RID: 820
		internal const string Host = "Host";

		// Token: 0x04000335 RID: 821
		internal const string ASProtocolVersion = "MS-ASProtocolVersion";

		// Token: 0x04000336 RID: 822
		internal const string ASErrorHeader = "X-MS-ASError";

		// Token: 0x04000337 RID: 823
		internal const string AutoBlockReasonHeader = "X-MS-ASThrottle";

		// Token: 0x04000338 RID: 824
		internal const string ActivityContextDiagnosticsHeader = "X-ActivityContextDiagnostics";

		// Token: 0x04000339 RID: 825
		internal const string ExceptionDiagnosticsHeader = "X-ExceptionDiagnostics";

		// Token: 0x0400033A RID: 826
		internal const string BEServerExceptionHeaderName = "X-BEServerException";

		// Token: 0x0400033B RID: 827
		internal const string BEServerExceptionHeaderValue = "Microsoft.Exchange.Data.Storage.IllegalCrossServerConnectionException";

		// Token: 0x0400033C RID: 828
		internal const string PrimaryMailboxId = "0";

		// Token: 0x0400033D RID: 829
		internal const string ASSeamlessUpgradeVersions = "MS-ASSeamlessUpgradeVersions";

		// Token: 0x0400033E RID: 830
		internal const string AirSync = "AirSync";

		// Token: 0x0400033F RID: 831
		internal const string MOWA = "MOWA";

		// Token: 0x04000340 RID: 832
		internal const string DOWA = "DOWA";

		// Token: 0x04000341 RID: 833
		internal const string LanguageHeader = "Accept-Language";

		// Token: 0x04000342 RID: 834
		internal const string UserAgentHeader = "User-Agent";

		// Token: 0x04000343 RID: 835
		internal const string RetryAfter = "Retry-After";

		// Token: 0x04000344 RID: 836
		internal const string ResetPartnership = "X-MS-RP";

		// Token: 0x04000345 RID: 837
		internal const string MinorVersionHeader = "X-MS-MV";

		// Token: 0x04000346 RID: 838
		internal const string SaveInSentParam = "SaveInSent";

		// Token: 0x04000347 RID: 839
		internal const string ItemIdParam = "ItemId";

		// Token: 0x04000348 RID: 840
		internal const string ParentIdParam = "ParentId";

		// Token: 0x04000349 RID: 841
		internal const string CollectionIdParam = "CollectionId";

		// Token: 0x0400034A RID: 842
		internal const string CollectionNameParam = "CollectionName";

		// Token: 0x0400034B RID: 843
		internal const string LongIdParam = "LongId";

		// Token: 0x0400034C RID: 844
		internal const string MessageRfc822Content = "message/rfc822";

		// Token: 0x0400034D RID: 845
		internal const string AttachmentNameParam = "AttachmentName";

		// Token: 0x0400034E RID: 846
		internal const string CommandParam = "Cmd";

		// Token: 0x0400034F RID: 847
		internal const string UserParam = "User";

		// Token: 0x04000350 RID: 848
		internal const string Occurrence = "Occurrence";

		// Token: 0x04000351 RID: 849
		internal const string DRMLicenseAttachmentId = "DRMLicense";

		// Token: 0x04000352 RID: 850
		internal const string DRMLicenseContentType = "application/x-microsoft-rpmsg-message-license";

		// Token: 0x04000353 RID: 851
		internal const string UTCDateTimeFormat = "yyyy-MM-dd\\THH:mm:ss.fff\\Z";

		// Token: 0x04000354 RID: 852
		internal const string LocalDateTimeFormat = "yyyyMMdd\\THHmmss\\Z";

		// Token: 0x04000355 RID: 853
		internal const string WapProvisionFormat = "MS-WAP-Provisioning-XML";

		// Token: 0x04000356 RID: 854
		internal const string EasProvisionFormat = "MS-EAS-Provisioning-WBXML";

		// Token: 0x04000357 RID: 855
		internal const string BadItemReportClassType = "IPM.Note.Exchange.ActiveSync.Report";

		// Token: 0x04000358 RID: 856
		internal const string DefaultSmimeAttachmentName = "smime.p7m";

		// Token: 0x04000359 RID: 857
		internal const string MultiPartSigned = "multipart/signed";

		// Token: 0x0400035A RID: 858
		internal const string EncryptedSMIMEMessageType = "IPM.Note.SMIME";

		// Token: 0x0400035B RID: 859
		internal const string MailboxLogClassType = "IPM.Note.Exchange.ActiveSync.MailboxLog";

		// Token: 0x0400035C RID: 860
		internal const string RemoteWipeConfirmationMessageType = "IPM.Note.Exchange.ActiveSync.RemoteWipeConfirmation";

		// Token: 0x0400035D RID: 861
		internal const string MeetingRequestClassType = "IPM.Schedule.Meeting.Request";

		// Token: 0x0400035E RID: 862
		internal const string BootstrapMailClassType = "IPM.Note";

		// Token: 0x0400035F RID: 863
		internal const string SmsClassType = "IPM.NOTE.MOBILE.SMS";

		// Token: 0x04000360 RID: 864
		internal const string MmsClassType = "IPM.NOTE.MOBILE.MMS";

		// Token: 0x04000361 RID: 865
		internal const int ConsumerSmsMaxSubjectLength = 78;

		// Token: 0x04000362 RID: 866
		internal const string ProxyHeader = "X-EAS-Proxy";

		// Token: 0x04000363 RID: 867
		internal const string PodProxyHeader = "X-EAS-DC-Proxy";

		// Token: 0x04000364 RID: 868
		internal const string BasicAuthProxyHeader = "X-EAS-BasicAuth-Proxy";

		// Token: 0x04000365 RID: 869
		internal const string ProxyLoginCommand = "ProxyLogin";

		// Token: 0x04000366 RID: 870
		internal const string IsFromCafeHeader = "X-IsFromCafe";

		// Token: 0x04000367 RID: 871
		internal const string ProxyUri = "msExchProxyUri";

		// Token: 0x04000368 RID: 872
		internal const string VDirSettingsHeader = "X-vDirObjectId";

		// Token: 0x04000369 RID: 873
		internal const string XMSWLHeader = "X-MS-WL";

		// Token: 0x0400036A RID: 874
		internal const int HttpStatusNeedIdentity = 441;

		// Token: 0x0400036B RID: 875
		internal const string OwaSettingsRoot = "SYSTEM\\CurrentControlSet\\Services\\MSExchange OWA";

		// Token: 0x0400036C RID: 876
		internal const string EasSettingsRoot = "SYSTEM\\CurrentControlSet\\Services\\MSExchange ActiveSync";

		// Token: 0x0400036D RID: 877
		internal const string ProtectedVMItemClassPrefix = "IPM.Note.RPMSG.Microsoft.Voicemail";

		// Token: 0x0400036E RID: 878
		internal const int MaxDeviceTypeLength = 32;

		// Token: 0x0400036F RID: 879
		internal const int MaxDeviceIDLength = 32;

		// Token: 0x04000370 RID: 880
		public const string CreateChatsFolder = "CreateChatsFolder";

		// Token: 0x04000371 RID: 881
		internal const string ProtocolVersionsHeaderConsumerVersion25OnlyValue = "2.5";

		// Token: 0x04000372 RID: 882
		internal const string ProtocolVersionsHeaderConsumerVersion140OnlyValue = "14.0";

		// Token: 0x04000373 RID: 883
		internal const string ProtocolVersionsHeaderConsumerValue = "2.5,14.0";

		// Token: 0x04000374 RID: 884
		internal const int ProtocolVersion160 = 160;

		// Token: 0x04000375 RID: 885
		internal const int ProtocolVersion161 = 161;

		// Token: 0x04000376 RID: 886
		internal static readonly Dictionary<OutlookExtension, bool> FeatureAccessMap = new Dictionary<OutlookExtension, bool>(Enum.GetValues(typeof(OutlookExtension)).Length)
		{
			{
				OutlookExtension.FolderTypes,
				true
			},
			{
				OutlookExtension.SystemCategories,
				true
			},
			{
				OutlookExtension.DefaultFromAddress,
				true
			},
			{
				OutlookExtension.Archive,
				false
			},
			{
				OutlookExtension.Unsubscribe,
				false
			},
			{
				OutlookExtension.MessageUpload,
				false
			},
			{
				OutlookExtension.AdvancedSearch,
				true
			},
			{
				OutlookExtension.Safety,
				false
			},
			{
				OutlookExtension.TrueMessageRead,
				false
			},
			{
				OutlookExtension.Rules,
				true
			},
			{
				OutlookExtension.ExtendedDateFilters,
				true
			},
			{
				OutlookExtension.Sms,
				true
			},
			{
				OutlookExtension.ActionableSearch,
				false
			},
			{
				OutlookExtension.FolderPermissions,
				false
			},
			{
				OutlookExtension.FolderExtensionType,
				false
			}
		};

		// Token: 0x04000377 RID: 887
		internal static readonly string[] Version10DeviceUserAgentPrefixes = new string[]
		{
			"Microsoft-AirSync/1.0",
			"Microsoft-PocketPC/4.",
			"Microsoft-SmartPhone/4."
		};

		// Token: 0x04000378 RID: 888
		internal static readonly string[] EvmSupportedItemClassPrefixes = new string[]
		{
			"IPM.Note.Microsoft.Voicemail",
			"IPM.Note.RPMSG.Microsoft.Voicemail",
			"IPM.Note.Microsoft.Missed.Voice"
		};

		// Token: 0x04000379 RID: 889
		internal static readonly Guid PsetidAppointment = new Guid("{00062002-0000-0000-c000-000000000046}");

		// Token: 0x0400037A RID: 890
		internal static readonly Exception FaultInjectionFormatException = new FormatException("Fault injection created exception: 3d911d3b-a4ad-4337-89a8-6e6218a1894e");

		// Token: 0x0400037B RID: 891
		internal static readonly ExPerformanceCounter[] ExceptionPerfCounters = new ExPerformanceCounter[]
		{
			AirSyncCounters.RatePerMinuteOfTransientMailboxConnectionFailures,
			AirSyncCounters.RatePerMinuteOfMailboxOfflineErrors,
			AirSyncCounters.RatePerMinuteOfTransientStorageErrors,
			AirSyncCounters.RatePerMinuteOfPermanentStorageErrors,
			AirSyncCounters.RatePerMinuteOfTransientActiveDirectoryErrors,
			AirSyncCounters.RatePerMinuteOfPermanentActiveDirectoryErrors,
			AirSyncCounters.RatePerMinuteOfTransientErrors
		};

		// Token: 0x0400037C RID: 892
		internal static readonly ExPerformanceCounter[] LatencyPerfCounters = new ExPerformanceCounter[]
		{
			AirSyncCounters.AverageRpcLatency,
			AirSyncCounters.AverageLdapLatency,
			AirSyncCounters.AverageRequestTime,
			AirSyncCounters.AverageHangingTime
		};

		// Token: 0x0400037D RID: 893
		internal static string ServerSideDeletes = "ServerSideDeletes";

		// Token: 0x0400037E RID: 894
		internal static string SyncMms = "SyncMMS";

		// Token: 0x02000049 RID: 73
		internal enum ExceptionPerfCountersType
		{
			// Token: 0x04000380 RID: 896
			ConnectionFailedTransientExceptionRate,
			// Token: 0x04000381 RID: 897
			MailboxOfflineExceptionRate,
			// Token: 0x04000382 RID: 898
			StorageTransientExceptionRate,
			// Token: 0x04000383 RID: 899
			StoragePermanentExceptionRate,
			// Token: 0x04000384 RID: 900
			AdTransientExceptionRate,
			// Token: 0x04000385 RID: 901
			AdPermanentExceptionRate,
			// Token: 0x04000386 RID: 902
			TransientErrorRate,
			// Token: 0x04000387 RID: 903
			MaxExceptionPerfCounters
		}

		// Token: 0x0200004A RID: 74
		internal enum LatencyPerfCountersType
		{
			// Token: 0x04000389 RID: 905
			AverageRpcLatency,
			// Token: 0x0400038A RID: 906
			AverageLdapLatency,
			// Token: 0x0400038B RID: 907
			AverageRequestLatency,
			// Token: 0x0400038C RID: 908
			AverageHangingLatency,
			// Token: 0x0400038D RID: 909
			MaxLatencyPerfCounters
		}
	}
}
