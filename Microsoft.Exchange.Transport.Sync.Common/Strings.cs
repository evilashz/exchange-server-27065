using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000002 RID: 2
	internal static class Strings
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		static Strings()
		{
			Strings.stringIDs.Add(995731409U, "IMAPUnsupportedVersionException");
			Strings.stringIDs.Add(147813322U, "NestedFoldersNotAllowedException");
			Strings.stringIDs.Add(1898862428U, "LeaveOnServerNotSupportedStatus");
			Strings.stringIDs.Add(1598126189U, "IMAPTrash");
			Strings.stringIDs.Add(1507388000U, "Pop3MirroredAccountNotPossibleException");
			Strings.stringIDs.Add(4121087442U, "RemoteMailboxQuotaWarningWithDisabledStatus");
			Strings.stringIDs.Add(3959479424U, "SubscriptionInvalidPasswordException");
			Strings.stringIDs.Add(1971329929U, "SubscriptionUpdateTransientException");
			Strings.stringIDs.Add(1075040750U, "ConnectionErrorWithDisabledStatus");
			Strings.stringIDs.Add(3599922107U, "ConnectedAccountsDetails");
			Strings.stringIDs.Add(3969501405U, "IMAPAllMail");
			Strings.stringIDs.Add(2030480939U, "MTOMParsingFailedException");
			Strings.stringIDs.Add(4079355374U, "LabsMailboxQuotaWarningWithDelayedDetailedStatus");
			Strings.stringIDs.Add(3801534216U, "AuthenticationErrorWithDelayedDetailedStatus");
			Strings.stringIDs.Add(1261870541U, "RemoteServerIsBackedOffException");
			Strings.stringIDs.Add(1401415251U, "LinkedInNonPromotableTransientException");
			Strings.stringIDs.Add(2432721285U, "RetryLaterException");
			Strings.stringIDs.Add(2971027178U, "ProviderExceptionDetailedStatus");
			Strings.stringIDs.Add(3410639299U, "MessageSizeLimitExceededException");
			Strings.stringIDs.Add(1298696584U, "CommunicationErrorWithDelayedStatus");
			Strings.stringIDs.Add(604756220U, "MailboxFailure");
			Strings.stringIDs.Add(3346793726U, "Pop3CapabilitiesNotSupportedException");
			Strings.stringIDs.Add(580763412U, "IMAPInvalidServerException");
			Strings.stringIDs.Add(3937165620U, "LeaveOnServerNotSupportedDetailedStatus");
			Strings.stringIDs.Add(3902086079U, "ConnectionClosedException");
			Strings.stringIDs.Add(2952144307U, "PoisonousRemoteServerException");
			Strings.stringIDs.Add(2394731155U, "SubscriptionInvalidVersionException");
			Strings.stringIDs.Add(3561039784U, "IMAPSentMail");
			Strings.stringIDs.Add(1756157035U, "InvalidVersionDetailedStatus");
			Strings.stringIDs.Add(3613930518U, "CommunicationErrorWithDisabledStatus");
			Strings.stringIDs.Add(3694666204U, "InProgressDetailedStatus");
			Strings.stringIDs.Add(517632853U, "AutoProvisionTestImap");
			Strings.stringIDs.Add(3137325842U, "DelayedStatus");
			Strings.stringIDs.Add(3444450370U, "ProviderExceptionStatus");
			Strings.stringIDs.Add(3530466102U, "CommunicationErrorWithDisabledDetailedStatus");
			Strings.stringIDs.Add(4176165696U, "RemoteServerIsSlowDisabledDetailedStatus");
			Strings.stringIDs.Add(2010701853U, "SendAsVerificationBottomBlock");
			Strings.stringIDs.Add(3292189374U, "AuthenticationErrorWithDisabledStatus");
			Strings.stringIDs.Add(591776832U, "ConnectionDownloadedLimitExceededException");
			Strings.stringIDs.Add(1477720531U, "MailboxOverQuotaException");
			Strings.stringIDs.Add(2149789026U, "InvalidServerResponseException");
			Strings.stringIDs.Add(2425965665U, "SyncStateSizeErrorDetailedStatus");
			Strings.stringIDs.Add(2492614440U, "RemoteAccountDoesNotExistDetailedStatus");
			Strings.stringIDs.Add(710927991U, "IMAPDrafts");
			Strings.stringIDs.Add(3842730909U, "MaxedOutSyncRelationshipsErrorWithDelayedDetailedStatus");
			Strings.stringIDs.Add(1812618676U, "IMAPInvalidItemException");
			Strings.stringIDs.Add(2019254494U, "SendAsVerificationSender");
			Strings.stringIDs.Add(2713854040U, "IMAPDraft");
			Strings.stringIDs.Add(882640929U, "IMAPSentMessages");
			Strings.stringIDs.Add(4155460975U, "TlsRemoteCertificateInvalid");
			Strings.stringIDs.Add(802159867U, "Pop3AuthErrorException");
			Strings.stringIDs.Add(2900222280U, "PoisonStatus");
			Strings.stringIDs.Add(2053678515U, "MessageIdGenerationTransientException");
			Strings.stringIDs.Add(3896029100U, "AutoProvisionTestAutoDiscover");
			Strings.stringIDs.Add(3699840920U, "Pop3TransientSystemAuthErrorException");
			Strings.stringIDs.Add(2759557964U, "SyncConflictException");
			Strings.stringIDs.Add(76218079U, "IMAPSentItems");
			Strings.stringIDs.Add(1404272750U, "SyncEngineSyncStorageProviderCreationException");
			Strings.stringIDs.Add(2942865760U, "Pop3TransientLoginDelayedAuthErrorException");
			Strings.stringIDs.Add(3228154330U, "IMAPDeletedMessages");
			Strings.stringIDs.Add(1124604668U, "RemoteMailboxQuotaWarningWithDelayedStatus");
			Strings.stringIDs.Add(1488733145U, "SyncStateSizeErrorStatus");
			Strings.stringIDs.Add(4092585897U, "MissingServerResponseException");
			Strings.stringIDs.Add(4257197704U, "LabsMailboxQuotaWarningWithDisabledStatus");
			Strings.stringIDs.Add(4287770443U, "Pop3TransientInUseAuthErrorException");
			Strings.stringIDs.Add(3000749982U, "Pop3DisabledResponseException");
			Strings.stringIDs.Add(233413355U, "SendAsVerificationSubject");
			Strings.stringIDs.Add(1726233450U, "IMAPAuthenticationException");
			Strings.stringIDs.Add(1413995526U, "FailedToGenerateVerificationEmail");
			Strings.stringIDs.Add(1235995580U, "RemoteMailboxQuotaWarningWithDelayedDetailedStatus");
			Strings.stringIDs.Add(715816254U, "AuthenticationErrorWithDisabledDetailedStatus");
			Strings.stringIDs.Add(1257586764U, "ConfigurationErrorStatus");
			Strings.stringIDs.Add(189434176U, "AuthenticationErrorWithDelayedStatus");
			Strings.stringIDs.Add(2936000979U, "MaxedOutSyncRelationshipsErrorBody");
			Strings.stringIDs.Add(2363855804U, "HotmailAccountVerificationFailedException");
			Strings.stringIDs.Add(1548922274U, "AutoProvisionResults");
			Strings.stringIDs.Add(2204337803U, "InvalidVersionStatus");
			Strings.stringIDs.Add(2984668342U, "IMAPDeletedItems");
			Strings.stringIDs.Add(601262870U, "TooManyFoldersStatus");
			Strings.stringIDs.Add(2617144415U, "SendAsVerificationSignatureTopPart");
			Strings.stringIDs.Add(2808236588U, "ConnectionErrorWithDelayedStatus");
			Strings.stringIDs.Add(3760020583U, "IMAPJunk");
			Strings.stringIDs.Add(974222160U, "RemoteAccountDoesNotExistStatus");
			Strings.stringIDs.Add(2167449654U, "TooManyFoldersDetailedStatus");
			Strings.stringIDs.Add(925456835U, "SuccessDetailedStatus");
			Strings.stringIDs.Add(2318825308U, "InProgressStatus");
			Strings.stringIDs.Add(3335416471U, "SubscriptionNotificationEmailBodyStartText");
			Strings.stringIDs.Add(3933215749U, "MaxedOutSyncRelationshipsErrorWithDisabledDetailedStatus");
			Strings.stringIDs.Add(2173734488U, "IMAPSpam");
			Strings.stringIDs.Add(1734582074U, "RemoteMailboxQuotaWarningWithDisabledDetailedStatus");
			Strings.stringIDs.Add(650547385U, "SendAsVerificationTopBlock");
			Strings.stringIDs.Add(3930645136U, "CommunicationErrorWithDelayedDetailedStatus");
			Strings.stringIDs.Add(4038775200U, "ContactCsvFileContainsNoKnownColumns");
			Strings.stringIDs.Add(82266192U, "LabsMailboxQuotaWarningWithDisabledDetailedStatus");
			Strings.stringIDs.Add(420875274U, "DelayedDetailedStatus");
			Strings.stringIDs.Add(3447698883U, "HttpResponseStreamNullException");
			Strings.stringIDs.Add(1059234861U, "RemoveSubscriptionStatus");
			Strings.stringIDs.Add(2325761520U, "DisabledDetailedStatus");
			Strings.stringIDs.Add(3091979858U, "InvalidAggregationSubscriptionIdentity");
			Strings.stringIDs.Add(1325089515U, "IMAPJunkEmail");
			Strings.stringIDs.Add(3269288896U, "DisabledStatus");
			Strings.stringIDs.Add(3779095989U, "MaxedOutSyncRelationshipsErrorWithDisabledStatus");
			Strings.stringIDs.Add(2754299458U, "FirstAppendToNotes");
			Strings.stringIDs.Add(3826985530U, "InvalidCsvFileFormat");
			Strings.stringIDs.Add(348158591U, "DeltaSyncServiceEndpointsLoadException");
			Strings.stringIDs.Add(2529513648U, "PoisonDetailedStatus");
			Strings.stringIDs.Add(2425850576U, "AutoProvisionTestHotmail");
			Strings.stringIDs.Add(1096710760U, "RemoteServerIsSlowStatus");
			Strings.stringIDs.Add(623751595U, "SubscriptionUpdatePermanentException");
			Strings.stringIDs.Add(603913161U, "PoisonousErrorBody");
			Strings.stringIDs.Add(2979796742U, "Pop3NonCompliantServerException");
			Strings.stringIDs.Add(1339731251U, "SuccessStatus");
			Strings.stringIDs.Add(3068969418U, "ConnectionErrorDetailedStatus");
			Strings.stringIDs.Add(3345637854U, "LabsMailboxQuotaWarningWithDelayedStatus");
			Strings.stringIDs.Add(2505373931U, "IMAPGmailNotSupportedException");
			Strings.stringIDs.Add(521865336U, "Pop3CannotConnectToServerException");
			Strings.stringIDs.Add(2222507634U, "RemoteServerIsSlowDelayedDetailedStatus");
			Strings.stringIDs.Add(2839399357U, "IMAPSent");
			Strings.stringIDs.Add(2049114804U, "StoreRestartedException");
			Strings.stringIDs.Add(3011192446U, "AccessTokenNullOrEmpty");
			Strings.stringIDs.Add(1745238072U, "Pop3LeaveOnServerNotPossibleException");
			Strings.stringIDs.Add(3743909068U, "AutoProvisionTestPop3");
			Strings.stringIDs.Add(3420783178U, "InvalidSyncEngineStateException");
			Strings.stringIDs.Add(3910384057U, "ContactCsvFileEmpty");
			Strings.stringIDs.Add(4000016521U, "FacebookNonPromotableTransientException");
			Strings.stringIDs.Add(305729253U, "MaxedOutSyncRelationshipsErrorWithDelayedStatus");
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002AE4 File Offset: 0x00000CE4
		public static LocalizedString IMAPUnsupportedVersionException
		{
			get
			{
				return new LocalizedString("IMAPUnsupportedVersionException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002B02 File Offset: 0x00000D02
		public static LocalizedString NestedFoldersNotAllowedException
		{
			get
			{
				return new LocalizedString("NestedFoldersNotAllowedException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002B20 File Offset: 0x00000D20
		public static LocalizedString DelayedDetailedStatusHours(int hours)
		{
			return new LocalizedString("DelayedDetailedStatusHours", "Ex70F5A8", false, true, Strings.ResourceManager, new object[]
			{
				hours
			});
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002B54 File Offset: 0x00000D54
		public static LocalizedString UnexpectedContentTypeException(string contentType)
		{
			return new LocalizedString("UnexpectedContentTypeException", "", false, false, Strings.ResourceManager, new object[]
			{
				contentType
			});
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002B83 File Offset: 0x00000D83
		public static LocalizedString LeaveOnServerNotSupportedStatus
		{
			get
			{
				return new LocalizedString("LeaveOnServerNotSupportedStatus", "ExFEE8E9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002BA1 File Offset: 0x00000DA1
		public static LocalizedString IMAPTrash
		{
			get
			{
				return new LocalizedString("IMAPTrash", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public static LocalizedString SendAsVerificationSalutation(string userDisplayName)
		{
			return new LocalizedString("SendAsVerificationSalutation", "Ex11593B", false, true, Strings.ResourceManager, new object[]
			{
				userDisplayName
			});
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002BF0 File Offset: 0x00000DF0
		public static LocalizedString SubscriptionInconsistent(string name)
		{
			return new LocalizedString("SubscriptionInconsistent", "ExD2BAA0", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002C1F File Offset: 0x00000E1F
		public static LocalizedString Pop3MirroredAccountNotPossibleException
		{
			get
			{
				return new LocalizedString("Pop3MirroredAccountNotPossibleException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002C40 File Offset: 0x00000E40
		public static LocalizedString UserAccessException(int statusCode)
		{
			return new LocalizedString("UserAccessException", "", false, false, Strings.ResourceManager, new object[]
			{
				statusCode
			});
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002C74 File Offset: 0x00000E74
		public static LocalizedString RemoteMailboxQuotaWarningWithDisabledStatus
		{
			get
			{
				return new LocalizedString("RemoteMailboxQuotaWarningWithDisabledStatus", "ExC85FB0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002C92 File Offset: 0x00000E92
		public static LocalizedString SubscriptionInvalidPasswordException
		{
			get
			{
				return new LocalizedString("SubscriptionInvalidPasswordException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public static LocalizedString SubscriptionUpdateTransientException
		{
			get
			{
				return new LocalizedString("SubscriptionUpdateTransientException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002CCE File Offset: 0x00000ECE
		public static LocalizedString ConnectionErrorWithDisabledStatus
		{
			get
			{
				return new LocalizedString("ConnectionErrorWithDisabledStatus", "Ex7A4C38", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002CEC File Offset: 0x00000EEC
		public static LocalizedString LeaveOnServerNotSupportedErrorBody(string connectedAccountsDetailsLinkedText)
		{
			return new LocalizedString("LeaveOnServerNotSupportedErrorBody", "Ex525E3E", false, true, Strings.ResourceManager, new object[]
			{
				connectedAccountsDetailsLinkedText
			});
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002D1B File Offset: 0x00000F1B
		public static LocalizedString ConnectedAccountsDetails
		{
			get
			{
				return new LocalizedString("ConnectedAccountsDetails", "Ex5DBE07", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002D3C File Offset: 0x00000F3C
		public static LocalizedString RemoteAccountDoesNotExistBody(string connectedAccountsDetailsLinkedText)
		{
			return new LocalizedString("RemoteAccountDoesNotExistBody", "Ex4CE140", false, true, Strings.ResourceManager, new object[]
			{
				connectedAccountsDetailsLinkedText
			});
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002D6C File Offset: 0x00000F6C
		public static LocalizedString ConnectionErrorBody(string connectedAccountsDetailsLinkedText)
		{
			return new LocalizedString("ConnectionErrorBody", "Ex9D9D39", false, true, Strings.ResourceManager, new object[]
			{
				connectedAccountsDetailsLinkedText
			});
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002D9C File Offset: 0x00000F9C
		public static LocalizedString SyncTooSlowException(TimeSpan syncDurationThreshold)
		{
			return new LocalizedString("SyncTooSlowException", "", false, false, Strings.ResourceManager, new object[]
			{
				syncDurationThreshold
			});
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002DD0 File Offset: 0x00000FD0
		public static LocalizedString ConnectionErrorBodyHour(int hour, string connectedAccountsDetailsLinkedText)
		{
			return new LocalizedString("ConnectionErrorBodyHour", "ExAED56C", false, true, Strings.ResourceManager, new object[]
			{
				hour,
				connectedAccountsDetailsLinkedText
			});
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002E08 File Offset: 0x00001008
		public static LocalizedString IMAPAllMail
		{
			get
			{
				return new LocalizedString("IMAPAllMail", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002E28 File Offset: 0x00001028
		public static LocalizedString MessageDecompressionFailedException(string serverId)
		{
			return new LocalizedString("MessageDecompressionFailedException", "", false, false, Strings.ResourceManager, new object[]
			{
				serverId
			});
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002E58 File Offset: 0x00001058
		public static LocalizedString FailedSetAggregationSubscription(string name)
		{
			return new LocalizedString("FailedSetAggregationSubscription", "Ex43A139", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002E87 File Offset: 0x00001087
		public static LocalizedString MTOMParsingFailedException
		{
			get
			{
				return new LocalizedString("MTOMParsingFailedException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002EA5 File Offset: 0x000010A5
		public static LocalizedString LabsMailboxQuotaWarningWithDelayedDetailedStatus
		{
			get
			{
				return new LocalizedString("LabsMailboxQuotaWarningWithDelayedDetailedStatus", "ExCE1D75", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002EC4 File Offset: 0x000010C4
		public static LocalizedString FailedDeleteAggregationSubscription(string name)
		{
			return new LocalizedString("FailedDeleteAggregationSubscription", "Ex901310", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002EF4 File Offset: 0x000010F4
		public static LocalizedString PartnerAuthenticationException(int statusCode)
		{
			return new LocalizedString("PartnerAuthenticationException", "", false, false, Strings.ResourceManager, new object[]
			{
				statusCode
			});
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002F28 File Offset: 0x00001128
		public static LocalizedString AuthenticationErrorWithDelayedDetailedStatus
		{
			get
			{
				return new LocalizedString("AuthenticationErrorWithDelayedDetailedStatus", "Ex4CA188", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002F46 File Offset: 0x00001146
		public static LocalizedString RemoteServerIsBackedOffException
		{
			get
			{
				return new LocalizedString("RemoteServerIsBackedOffException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002F64 File Offset: 0x00001164
		public static LocalizedString LinkedInNonPromotableTransientException
		{
			get
			{
				return new LocalizedString("LinkedInNonPromotableTransientException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002F82 File Offset: 0x00001182
		public static LocalizedString RetryLaterException
		{
			get
			{
				return new LocalizedString("RetryLaterException", "Ex82BE43", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002FA0 File Offset: 0x000011A0
		public static LocalizedString ProviderExceptionDetailedStatus
		{
			get
			{
				return new LocalizedString("ProviderExceptionDetailedStatus", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002FBE File Offset: 0x000011BE
		public static LocalizedString MessageSizeLimitExceededException
		{
			get
			{
				return new LocalizedString("MessageSizeLimitExceededException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002FDC File Offset: 0x000011DC
		public static LocalizedString FailedCreateAggregationSubscription(string name)
		{
			return new LocalizedString("FailedCreateAggregationSubscription", "ExEB3294", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000300B File Offset: 0x0000120B
		public static LocalizedString CommunicationErrorWithDelayedStatus
		{
			get
			{
				return new LocalizedString("CommunicationErrorWithDelayedStatus", "Ex780460", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00003029 File Offset: 0x00001229
		public static LocalizedString MailboxFailure
		{
			get
			{
				return new LocalizedString("MailboxFailure", "Ex9057FE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00003047 File Offset: 0x00001247
		public static LocalizedString Pop3CapabilitiesNotSupportedException
		{
			get
			{
				return new LocalizedString("Pop3CapabilitiesNotSupportedException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00003065 File Offset: 0x00001265
		public static LocalizedString IMAPInvalidServerException
		{
			get
			{
				return new LocalizedString("IMAPInvalidServerException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00003083 File Offset: 0x00001283
		public static LocalizedString LeaveOnServerNotSupportedDetailedStatus
		{
			get
			{
				return new LocalizedString("LeaveOnServerNotSupportedDetailedStatus", "Ex153CCE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000030A4 File Offset: 0x000012A4
		public static LocalizedString SubscriptionNotFound(string subscription)
		{
			return new LocalizedString("SubscriptionNotFound", "Ex7CE313", false, true, Strings.ResourceManager, new object[]
			{
				subscription
			});
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000030D3 File Offset: 0x000012D3
		public static LocalizedString ConnectionClosedException
		{
			get
			{
				return new LocalizedString("ConnectionClosedException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000030F4 File Offset: 0x000012F4
		public static LocalizedString MailboxPermanentErrorSavingContact(int failedContactIndex, int contactsSaved)
		{
			return new LocalizedString("MailboxPermanentErrorSavingContact", "", false, false, Strings.ResourceManager, new object[]
			{
				failedContactIndex,
				contactsSaved
			});
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00003131 File Offset: 0x00001331
		public static LocalizedString PoisonousRemoteServerException
		{
			get
			{
				return new LocalizedString("PoisonousRemoteServerException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003150 File Offset: 0x00001350
		public static LocalizedString MultipleNativeItemsHaveSameCloudIdException(string cloudId, Guid subscriptionGuid)
		{
			return new LocalizedString("MultipleNativeItemsHaveSameCloudIdException", "", false, false, Strings.ResourceManager, new object[]
			{
				cloudId,
				subscriptionGuid
			});
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00003188 File Offset: 0x00001388
		public static LocalizedString SubscriptionInvalidVersionException
		{
			get
			{
				return new LocalizedString("SubscriptionInvalidVersionException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000031A8 File Offset: 0x000013A8
		public static LocalizedString MailboxTransientExceptionSavingContact(int failedContactIndex, int contactsSaved)
		{
			return new LocalizedString("MailboxTransientExceptionSavingContact", "", false, false, Strings.ResourceManager, new object[]
			{
				failedContactIndex,
				contactsSaved
			});
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000031E5 File Offset: 0x000013E5
		public static LocalizedString IMAPSentMail
		{
			get
			{
				return new LocalizedString("IMAPSentMail", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00003203 File Offset: 0x00001403
		public static LocalizedString InvalidVersionDetailedStatus
		{
			get
			{
				return new LocalizedString("InvalidVersionDetailedStatus", "Ex39BACE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00003221 File Offset: 0x00001421
		public static LocalizedString CommunicationErrorWithDisabledStatus
		{
			get
			{
				return new LocalizedString("CommunicationErrorWithDisabledStatus", "Ex3100BA", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000323F File Offset: 0x0000143F
		public static LocalizedString InProgressDetailedStatus
		{
			get
			{
				return new LocalizedString("InProgressDetailedStatus", "Ex44EC73", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000325D File Offset: 0x0000145D
		public static LocalizedString AutoProvisionTestImap
		{
			get
			{
				return new LocalizedString("AutoProvisionTestImap", "Ex80AAE3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000327C File Offset: 0x0000147C
		public static LocalizedString ConnectionErrorDetailedStatusHour(int hour)
		{
			return new LocalizedString("ConnectionErrorDetailedStatusHour", "Ex7B4143", false, true, Strings.ResourceManager, new object[]
			{
				hour
			});
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000032B0 File Offset: 0x000014B0
		public static LocalizedString DelayedStatus
		{
			get
			{
				return new LocalizedString("DelayedStatus", "ExD77583", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000032D0 File Offset: 0x000014D0
		public static LocalizedString SyncPoisonItemFoundException(string syncPoisonItem, Guid subscriptionId)
		{
			return new LocalizedString("SyncPoisonItemFoundException", "", false, false, Strings.ResourceManager, new object[]
			{
				syncPoisonItem,
				subscriptionId
			});
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00003308 File Offset: 0x00001508
		public static LocalizedString ProviderExceptionStatus
		{
			get
			{
				return new LocalizedString("ProviderExceptionStatus", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003328 File Offset: 0x00001528
		public static LocalizedString ConnectionErrorBodyHours(int hours, string connectedAccountsDetailsLinkedText)
		{
			return new LocalizedString("ConnectionErrorBodyHours", "Ex296F05", false, true, Strings.ResourceManager, new object[]
			{
				hours,
				connectedAccountsDetailsLinkedText
			});
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003360 File Offset: 0x00001560
		public static LocalizedString SubscriptionSyncException(string subscriptionName)
		{
			return new LocalizedString("SubscriptionSyncException", "", false, false, Strings.ResourceManager, new object[]
			{
				subscriptionName
			});
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000338F File Offset: 0x0000158F
		public static LocalizedString CommunicationErrorWithDisabledDetailedStatus
		{
			get
			{
				return new LocalizedString("CommunicationErrorWithDisabledDetailedStatus", "Ex087643", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000033AD File Offset: 0x000015AD
		public static LocalizedString RemoteServerIsSlowDisabledDetailedStatus
		{
			get
			{
				return new LocalizedString("RemoteServerIsSlowDisabledDetailedStatus", "Ex1749DA", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000033CB File Offset: 0x000015CB
		public static LocalizedString SendAsVerificationBottomBlock
		{
			get
			{
				return new LocalizedString("SendAsVerificationBottomBlock", "Ex619B79", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000033E9 File Offset: 0x000015E9
		public static LocalizedString AuthenticationErrorWithDisabledStatus
		{
			get
			{
				return new LocalizedString("AuthenticationErrorWithDisabledStatus", "Ex9BF3DC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003408 File Offset: 0x00001608
		public static LocalizedString CorruptSubscriptionException(Guid guid)
		{
			return new LocalizedString("CorruptSubscriptionException", "", false, false, Strings.ResourceManager, new object[]
			{
				guid
			});
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000343C File Offset: 0x0000163C
		public static LocalizedString DelayedDetailedStatusDay(int day)
		{
			return new LocalizedString("DelayedDetailedStatusDay", "ExD3990F", false, true, Strings.ResourceManager, new object[]
			{
				day
			});
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00003470 File Offset: 0x00001670
		public static LocalizedString ConnectionDownloadedLimitExceededException
		{
			get
			{
				return new LocalizedString("ConnectionDownloadedLimitExceededException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000348E File Offset: 0x0000168E
		public static LocalizedString MailboxOverQuotaException
		{
			get
			{
				return new LocalizedString("MailboxOverQuotaException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000034AC File Offset: 0x000016AC
		public static LocalizedString InvalidServerResponseException
		{
			get
			{
				return new LocalizedString("InvalidServerResponseException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000034CA File Offset: 0x000016CA
		public static LocalizedString SyncStateSizeErrorDetailedStatus
		{
			get
			{
				return new LocalizedString("SyncStateSizeErrorDetailedStatus", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000034E8 File Offset: 0x000016E8
		public static LocalizedString RemoteAccountDoesNotExistDetailedStatus
		{
			get
			{
				return new LocalizedString("RemoteAccountDoesNotExistDetailedStatus", "Ex506CFF", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003506 File Offset: 0x00001706
		public static LocalizedString IMAPDrafts
		{
			get
			{
				return new LocalizedString("IMAPDrafts", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003524 File Offset: 0x00001724
		public static LocalizedString MaxedOutSyncRelationshipsErrorWithDelayedDetailedStatus
		{
			get
			{
				return new LocalizedString("MaxedOutSyncRelationshipsErrorWithDelayedDetailedStatus", "Ex7E5344", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003542 File Offset: 0x00001742
		public static LocalizedString IMAPInvalidItemException
		{
			get
			{
				return new LocalizedString("IMAPInvalidItemException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003560 File Offset: 0x00001760
		public static LocalizedString SendAsVerificationSender
		{
			get
			{
				return new LocalizedString("SendAsVerificationSender", "ExE337C1", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003580 File Offset: 0x00001780
		public static LocalizedString QuotaExceededSavingContact(int failedContactIndex, int contactsSaved)
		{
			return new LocalizedString("QuotaExceededSavingContact", "", false, false, Strings.ResourceManager, new object[]
			{
				failedContactIndex,
				contactsSaved
			});
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000035BD File Offset: 0x000017BD
		public static LocalizedString IMAPDraft
		{
			get
			{
				return new LocalizedString("IMAPDraft", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000035DB File Offset: 0x000017DB
		public static LocalizedString IMAPSentMessages
		{
			get
			{
				return new LocalizedString("IMAPSentMessages", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000035F9 File Offset: 0x000017F9
		public static LocalizedString TlsRemoteCertificateInvalid
		{
			get
			{
				return new LocalizedString("TlsRemoteCertificateInvalid", "Ex87C115", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00003617 File Offset: 0x00001817
		public static LocalizedString Pop3AuthErrorException
		{
			get
			{
				return new LocalizedString("Pop3AuthErrorException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003638 File Offset: 0x00001838
		public static LocalizedString Pop3ErrorResponseException(string command, string response)
		{
			return new LocalizedString("Pop3ErrorResponseException", "", false, false, Strings.ResourceManager, new object[]
			{
				command,
				response
			});
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000366C File Offset: 0x0000186C
		public static LocalizedString AutoProvisionStatus(string authority, string username, string security, string authentication)
		{
			return new LocalizedString("AutoProvisionStatus", "", false, false, Strings.ResourceManager, new object[]
			{
				authority,
				username,
				security,
				authentication
			});
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000036A8 File Offset: 0x000018A8
		public static LocalizedString InternalErrorSavingContact(int failedContactIndex, int contactsSaved)
		{
			return new LocalizedString("InternalErrorSavingContact", "", false, false, Strings.ResourceManager, new object[]
			{
				failedContactIndex,
				contactsSaved
			});
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000036E8 File Offset: 0x000018E8
		public static LocalizedString DeltaSyncServerException(int statusCode)
		{
			return new LocalizedString("DeltaSyncServerException", "", false, false, Strings.ResourceManager, new object[]
			{
				statusCode
			});
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000371C File Offset: 0x0000191C
		public static LocalizedString PoisonStatus
		{
			get
			{
				return new LocalizedString("PoisonStatus", "Ex4671DD", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000054 RID: 84 RVA: 0x0000373A File Offset: 0x0000193A
		public static LocalizedString MessageIdGenerationTransientException
		{
			get
			{
				return new LocalizedString("MessageIdGenerationTransientException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003758 File Offset: 0x00001958
		public static LocalizedString IMAPException(string failureReason)
		{
			return new LocalizedString("IMAPException", "", false, false, Strings.ResourceManager, new object[]
			{
				failureReason
			});
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003788 File Offset: 0x00001988
		public static LocalizedString SubscriptionNotificationEmailSubject(string subscriptionEmailAddress)
		{
			return new LocalizedString("SubscriptionNotificationEmailSubject", "ExBFF38A", false, true, Strings.ResourceManager, new object[]
			{
				subscriptionEmailAddress
			});
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000037B7 File Offset: 0x000019B7
		public static LocalizedString AutoProvisionTestAutoDiscover
		{
			get
			{
				return new LocalizedString("AutoProvisionTestAutoDiscover", "ExC150D2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000037D5 File Offset: 0x000019D5
		public static LocalizedString Pop3TransientSystemAuthErrorException
		{
			get
			{
				return new LocalizedString("Pop3TransientSystemAuthErrorException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000037F4 File Offset: 0x000019F4
		public static LocalizedString SyncUnhandledException(Type type)
		{
			return new LocalizedString("SyncUnhandledException", "", false, false, Strings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003823 File Offset: 0x00001A23
		public static LocalizedString SyncConflictException
		{
			get
			{
				return new LocalizedString("SyncConflictException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003844 File Offset: 0x00001A44
		public static LocalizedString ConnectionErrorDetailedStatusDays(int days)
		{
			return new LocalizedString("ConnectionErrorDetailedStatusDays", "Ex605693", false, true, Strings.ResourceManager, new object[]
			{
				days
			});
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003878 File Offset: 0x00001A78
		public static LocalizedString IMAPSentItems
		{
			get
			{
				return new LocalizedString("IMAPSentItems", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003898 File Offset: 0x00001A98
		public static LocalizedString Pop3PermErrorResponseException(string command, string response)
		{
			return new LocalizedString("Pop3PermErrorResponseException", "", false, false, Strings.ResourceManager, new object[]
			{
				command,
				response
			});
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000038CC File Offset: 0x00001ACC
		public static LocalizedString DelayedDetailedStatusHour(int hour)
		{
			return new LocalizedString("DelayedDetailedStatusHour", "Ex4EAD4C", false, true, Strings.ResourceManager, new object[]
			{
				hour
			});
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003900 File Offset: 0x00001B00
		public static LocalizedString FailedDeletePeopleConnectSubscription(AggregationSubscriptionType subscriptionType)
		{
			return new LocalizedString("FailedDeletePeopleConnectSubscription", "", false, false, Strings.ResourceManager, new object[]
			{
				subscriptionType
			});
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003934 File Offset: 0x00001B34
		public static LocalizedString SyncEngineSyncStorageProviderCreationException
		{
			get
			{
				return new LocalizedString("SyncEngineSyncStorageProviderCreationException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003952 File Offset: 0x00001B52
		public static LocalizedString Pop3TransientLoginDelayedAuthErrorException
		{
			get
			{
				return new LocalizedString("Pop3TransientLoginDelayedAuthErrorException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003970 File Offset: 0x00001B70
		public static LocalizedString TlsFailureException(string failureReason)
		{
			return new LocalizedString("TlsFailureException", "", false, false, Strings.ResourceManager, new object[]
			{
				failureReason
			});
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000063 RID: 99 RVA: 0x0000399F File Offset: 0x00001B9F
		public static LocalizedString IMAPDeletedMessages
		{
			get
			{
				return new LocalizedString("IMAPDeletedMessages", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000039BD File Offset: 0x00001BBD
		public static LocalizedString RemoteMailboxQuotaWarningWithDelayedStatus
		{
			get
			{
				return new LocalizedString("RemoteMailboxQuotaWarningWithDelayedStatus", "Ex40402E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000039DB File Offset: 0x00001BDB
		public static LocalizedString SyncStateSizeErrorStatus
		{
			get
			{
				return new LocalizedString("SyncStateSizeErrorStatus", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000039F9 File Offset: 0x00001BF9
		public static LocalizedString MissingServerResponseException
		{
			get
			{
				return new LocalizedString("MissingServerResponseException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003A18 File Offset: 0x00001C18
		public static LocalizedString RemoteServerTooSlowException(string remoteServer, int port, TimeSpan actualLatency, TimeSpan expectedLatency)
		{
			return new LocalizedString("RemoteServerTooSlowException", "", false, false, Strings.ResourceManager, new object[]
			{
				remoteServer,
				port,
				actualLatency,
				expectedLatency
			});
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003A64 File Offset: 0x00001C64
		public static LocalizedString Pop3BrokenResponseException(string command, string response)
		{
			return new LocalizedString("Pop3BrokenResponseException", "", false, false, Strings.ResourceManager, new object[]
			{
				command,
				response
			});
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003A97 File Offset: 0x00001C97
		public static LocalizedString LabsMailboxQuotaWarningWithDisabledStatus
		{
			get
			{
				return new LocalizedString("LabsMailboxQuotaWarningWithDisabledStatus", "ExC59B39", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003AB5 File Offset: 0x00001CB5
		public static LocalizedString Pop3TransientInUseAuthErrorException
		{
			get
			{
				return new LocalizedString("Pop3TransientInUseAuthErrorException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003AD3 File Offset: 0x00001CD3
		public static LocalizedString Pop3DisabledResponseException
		{
			get
			{
				return new LocalizedString("Pop3DisabledResponseException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003AF4 File Offset: 0x00001CF4
		public static LocalizedString RemoteServerIsSlowErrorBody(string connectedAccountsDetailsLinkedText)
		{
			return new LocalizedString("RemoteServerIsSlowErrorBody", "ExF197B6", false, true, Strings.ResourceManager, new object[]
			{
				connectedAccountsDetailsLinkedText
			});
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003B23 File Offset: 0x00001D23
		public static LocalizedString SendAsVerificationSubject
		{
			get
			{
				return new LocalizedString("SendAsVerificationSubject", "ExF655E3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003B41 File Offset: 0x00001D41
		public static LocalizedString IMAPAuthenticationException
		{
			get
			{
				return new LocalizedString("IMAPAuthenticationException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003B5F File Offset: 0x00001D5F
		public static LocalizedString FailedToGenerateVerificationEmail
		{
			get
			{
				return new LocalizedString("FailedToGenerateVerificationEmail", "ExD9B280", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003B80 File Offset: 0x00001D80
		public static LocalizedString CommunicationErrorBody(string connectedAccountsDetailsLinkedText)
		{
			return new LocalizedString("CommunicationErrorBody", "Ex89DF6D", false, true, Strings.ResourceManager, new object[]
			{
				connectedAccountsDetailsLinkedText
			});
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003BAF File Offset: 0x00001DAF
		public static LocalizedString RemoteMailboxQuotaWarningWithDelayedDetailedStatus
		{
			get
			{
				return new LocalizedString("RemoteMailboxQuotaWarningWithDelayedDetailedStatus", "Ex935EA1", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003BCD File Offset: 0x00001DCD
		public static LocalizedString AuthenticationErrorWithDisabledDetailedStatus
		{
			get
			{
				return new LocalizedString("AuthenticationErrorWithDisabledDetailedStatus", "Ex690DF9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003BEB File Offset: 0x00001DEB
		public static LocalizedString ConfigurationErrorStatus
		{
			get
			{
				return new LocalizedString("ConfigurationErrorStatus", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003C09 File Offset: 0x00001E09
		public static LocalizedString AuthenticationErrorWithDelayedStatus
		{
			get
			{
				return new LocalizedString("AuthenticationErrorWithDelayedStatus", "ExD59937", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003C28 File Offset: 0x00001E28
		public static LocalizedString UncompressedSyncStateSizeExceededException(string syncStateId, Guid subscriptionId, ByteQuantifiedSize currentUncompressedSyncStateSize, ByteQuantifiedSize loadedSyncStateSizeLimit)
		{
			return new LocalizedString("UncompressedSyncStateSizeExceededException", "", false, false, Strings.ResourceManager, new object[]
			{
				syncStateId,
				subscriptionId,
				currentUncompressedSyncStateSize,
				loadedSyncStateSizeLimit
			});
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003C72 File Offset: 0x00001E72
		public static LocalizedString MaxedOutSyncRelationshipsErrorBody
		{
			get
			{
				return new LocalizedString("MaxedOutSyncRelationshipsErrorBody", "Ex8C1104", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003C90 File Offset: 0x00001E90
		public static LocalizedString HotmailAccountVerificationFailedException
		{
			get
			{
				return new LocalizedString("HotmailAccountVerificationFailedException", "Ex0CE4D2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003CAE File Offset: 0x00001EAE
		public static LocalizedString AutoProvisionResults
		{
			get
			{
				return new LocalizedString("AutoProvisionResults", "Ex4E3CEC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003CCC File Offset: 0x00001ECC
		public static LocalizedString InvalidVersionStatus
		{
			get
			{
				return new LocalizedString("InvalidVersionStatus", "Ex097C26", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003CEA File Offset: 0x00001EEA
		public static LocalizedString IMAPDeletedItems
		{
			get
			{
				return new LocalizedString("IMAPDeletedItems", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003D08 File Offset: 0x00001F08
		public static LocalizedString AuthenticationErrorBody(string connectedAccountsDetailsLinkedText)
		{
			return new LocalizedString("AuthenticationErrorBody", "ExE642A6", false, true, Strings.ResourceManager, new object[]
			{
				connectedAccountsDetailsLinkedText
			});
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003D38 File Offset: 0x00001F38
		public static LocalizedString RedundantPimSubscription(string emailAddress)
		{
			return new LocalizedString("RedundantPimSubscription", "Ex874294", false, true, Strings.ResourceManager, new object[]
			{
				emailAddress
			});
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003D67 File Offset: 0x00001F67
		public static LocalizedString TooManyFoldersStatus
		{
			get
			{
				return new LocalizedString("TooManyFoldersStatus", "Ex712E6D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003D85 File Offset: 0x00001F85
		public static LocalizedString SendAsVerificationSignatureTopPart
		{
			get
			{
				return new LocalizedString("SendAsVerificationSignatureTopPart", "ExA39B17", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003DA3 File Offset: 0x00001FA3
		public static LocalizedString ConnectionErrorWithDelayedStatus
		{
			get
			{
				return new LocalizedString("ConnectionErrorWithDelayedStatus", "ExA90BC0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003DC1 File Offset: 0x00001FC1
		public static LocalizedString IMAPJunk
		{
			get
			{
				return new LocalizedString("IMAPJunk", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003DE0 File Offset: 0x00001FE0
		public static LocalizedString SyncStateSizeErrorBody(string connectedAccountsDetailsLinkedText)
		{
			return new LocalizedString("SyncStateSizeErrorBody", "", false, false, Strings.ResourceManager, new object[]
			{
				connectedAccountsDetailsLinkedText
			});
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003E10 File Offset: 0x00002010
		public static LocalizedString Pop3UnknownResponseException(string command, string response)
		{
			return new LocalizedString("Pop3UnknownResponseException", "", false, false, Strings.ResourceManager, new object[]
			{
				command,
				response
			});
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003E44 File Offset: 0x00002044
		public static LocalizedString CompressedSyncStateSizeExceededException(string syncStateId, Guid subscriptionId, StoragePermanentException e)
		{
			return new LocalizedString("CompressedSyncStateSizeExceededException", "", false, false, Strings.ResourceManager, new object[]
			{
				syncStateId,
				subscriptionId,
				e
			});
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003E80 File Offset: 0x00002080
		public static LocalizedString DelayedDetailedStatusDays(int days)
		{
			return new LocalizedString("DelayedDetailedStatusDays", "ExE90DC4", false, true, Strings.ResourceManager, new object[]
			{
				days
			});
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003EB4 File Offset: 0x000020B4
		public static LocalizedString RemoteAccountDoesNotExistStatus
		{
			get
			{
				return new LocalizedString("RemoteAccountDoesNotExistStatus", "ExB18D81", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003ED4 File Offset: 0x000020D4
		public static LocalizedString ConnectionErrorBodyDays(int days, string connectedAccountsDetailsLinkedText)
		{
			return new LocalizedString("ConnectionErrorBodyDays", "Ex83D02B", false, true, Strings.ResourceManager, new object[]
			{
				days,
				connectedAccountsDetailsLinkedText
			});
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003F0C File Offset: 0x0000210C
		public static LocalizedString TlsFailureErrorOccurred(SecurityStatus securityStatus)
		{
			return new LocalizedString("TlsFailureErrorOccurred", "Ex1A9BAC", false, true, Strings.ResourceManager, new object[]
			{
				securityStatus
			});
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003F40 File Offset: 0x00002140
		public static LocalizedString TooManyFoldersDetailedStatus
		{
			get
			{
				return new LocalizedString("TooManyFoldersDetailedStatus", "Ex9142EC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003F5E File Offset: 0x0000215E
		public static LocalizedString SuccessDetailedStatus
		{
			get
			{
				return new LocalizedString("SuccessDetailedStatus", "Ex0B254E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003F7C File Offset: 0x0000217C
		public static LocalizedString UnknownDeltaSyncException(int statusCode)
		{
			return new LocalizedString("UnknownDeltaSyncException", "", false, false, Strings.ResourceManager, new object[]
			{
				statusCode
			});
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003FB0 File Offset: 0x000021B0
		public static LocalizedString InProgressStatus
		{
			get
			{
				return new LocalizedString("InProgressStatus", "Ex6D4483", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003FCE File Offset: 0x000021CE
		public static LocalizedString SubscriptionNotificationEmailBodyStartText
		{
			get
			{
				return new LocalizedString("SubscriptionNotificationEmailBodyStartText", "ExE34D33", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003FEC File Offset: 0x000021EC
		public static LocalizedString RequestFormatException(int statusCode)
		{
			return new LocalizedString("RequestFormatException", "", false, false, Strings.ResourceManager, new object[]
			{
				statusCode
			});
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00004020 File Offset: 0x00002220
		public static LocalizedString MaxedOutSyncRelationshipsErrorWithDisabledDetailedStatus
		{
			get
			{
				return new LocalizedString("MaxedOutSyncRelationshipsErrorWithDisabledDetailedStatus", "ExAE3E49", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004040 File Offset: 0x00002240
		public static LocalizedString UnresolveableFolderNameException(string folderName)
		{
			return new LocalizedString("UnresolveableFolderNameException", "", false, false, Strings.ResourceManager, new object[]
			{
				folderName
			});
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004070 File Offset: 0x00002270
		public static LocalizedString RedundantAccountSubscription(string username, string server)
		{
			return new LocalizedString("RedundantAccountSubscription", "ExA964A4", false, true, Strings.ResourceManager, new object[]
			{
				username,
				server
			});
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000040A3 File Offset: 0x000022A3
		public static LocalizedString IMAPSpam
		{
			get
			{
				return new LocalizedString("IMAPSpam", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000040C1 File Offset: 0x000022C1
		public static LocalizedString RemoteMailboxQuotaWarningWithDisabledDetailedStatus
		{
			get
			{
				return new LocalizedString("RemoteMailboxQuotaWarningWithDisabledDetailedStatus", "Ex9FBFAF", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000040E0 File Offset: 0x000022E0
		public static LocalizedString RequestSettingsException(int statusCode)
		{
			return new LocalizedString("RequestSettingsException", "", false, false, Strings.ResourceManager, new object[]
			{
				statusCode
			});
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00004114 File Offset: 0x00002314
		public static LocalizedString SendAsVerificationTopBlock
		{
			get
			{
				return new LocalizedString("SendAsVerificationTopBlock", "Ex11E78B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004134 File Offset: 0x00002334
		public static LocalizedString ConnectionErrorBodyDay(int day, string connectedAccountsDetailsLinkedText)
		{
			return new LocalizedString("ConnectionErrorBodyDay", "Ex8E098F", false, true, Strings.ResourceManager, new object[]
			{
				day,
				connectedAccountsDetailsLinkedText
			});
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000416C File Offset: 0x0000236C
		public static LocalizedString SubscriptionNumberExceedLimit(int number)
		{
			return new LocalizedString("SubscriptionNumberExceedLimit", "Ex69D7B2", false, true, Strings.ResourceManager, new object[]
			{
				number
			});
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000041A0 File Offset: 0x000023A0
		public static LocalizedString CommunicationErrorWithDelayedDetailedStatus
		{
			get
			{
				return new LocalizedString("CommunicationErrorWithDelayedDetailedStatus", "Ex5A8691", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000041C0 File Offset: 0x000023C0
		public static LocalizedString DataOutOfSyncException(int statusCode)
		{
			return new LocalizedString("DataOutOfSyncException", "", false, false, Strings.ResourceManager, new object[]
			{
				statusCode
			});
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000041F4 File Offset: 0x000023F4
		public static LocalizedString ContactCsvFileContainsNoKnownColumns
		{
			get
			{
				return new LocalizedString("ContactCsvFileContainsNoKnownColumns", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00004212 File Offset: 0x00002412
		public static LocalizedString LabsMailboxQuotaWarningWithDisabledDetailedStatus
		{
			get
			{
				return new LocalizedString("LabsMailboxQuotaWarningWithDisabledDetailedStatus", "ExB10331", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00004230 File Offset: 0x00002430
		public static LocalizedString DelayedDetailedStatus
		{
			get
			{
				return new LocalizedString("DelayedDetailedStatus", "ExDDC385", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600009C RID: 156 RVA: 0x0000424E File Offset: 0x0000244E
		public static LocalizedString HttpResponseStreamNullException
		{
			get
			{
				return new LocalizedString("HttpResponseStreamNullException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000426C File Offset: 0x0000246C
		public static LocalizedString RemoveSubscriptionStatus
		{
			get
			{
				return new LocalizedString("RemoveSubscriptionStatus", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000428A File Offset: 0x0000248A
		public static LocalizedString DisabledDetailedStatus
		{
			get
			{
				return new LocalizedString("DisabledDetailedStatus", "ExE5870F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000042A8 File Offset: 0x000024A8
		public static LocalizedString InvalidAggregationSubscriptionIdentity
		{
			get
			{
				return new LocalizedString("InvalidAggregationSubscriptionIdentity", "Ex66A82E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000042C6 File Offset: 0x000024C6
		public static LocalizedString IMAPJunkEmail
		{
			get
			{
				return new LocalizedString("IMAPJunkEmail", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000042E4 File Offset: 0x000024E4
		public static LocalizedString DisabledStatus
		{
			get
			{
				return new LocalizedString("DisabledStatus", "ExE1EB04", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004302 File Offset: 0x00002502
		public static LocalizedString MaxedOutSyncRelationshipsErrorWithDisabledStatus
		{
			get
			{
				return new LocalizedString("MaxedOutSyncRelationshipsErrorWithDisabledStatus", "ExB49C16", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004320 File Offset: 0x00002520
		public static LocalizedString FirstAppendToNotes
		{
			get
			{
				return new LocalizedString("FirstAppendToNotes", "Ex95492E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000433E File Offset: 0x0000253E
		public static LocalizedString InvalidCsvFileFormat
		{
			get
			{
				return new LocalizedString("InvalidCsvFileFormat", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000435C File Offset: 0x0000255C
		public static LocalizedString TooManyFoldersException(int maxNumber)
		{
			return new LocalizedString("TooManyFoldersException", "", false, false, Strings.ResourceManager, new object[]
			{
				maxNumber
			});
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004390 File Offset: 0x00002590
		public static LocalizedString SubscriptionNameAlreadyExists(string name)
		{
			return new LocalizedString("SubscriptionNameAlreadyExists", "ExB14F4B", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000043BF File Offset: 0x000025BF
		public static LocalizedString DeltaSyncServiceEndpointsLoadException
		{
			get
			{
				return new LocalizedString("DeltaSyncServiceEndpointsLoadException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000043DD File Offset: 0x000025DD
		public static LocalizedString PoisonDetailedStatus
		{
			get
			{
				return new LocalizedString("PoisonDetailedStatus", "ExC70BEB", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000043FB File Offset: 0x000025FB
		public static LocalizedString AutoProvisionTestHotmail
		{
			get
			{
				return new LocalizedString("AutoProvisionTestHotmail", "Ex8CD58D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000441C File Offset: 0x0000261C
		public static LocalizedString TooManyFoldersErrorBody(int maxFolders, string connectedAccountsDetailsLinkedText)
		{
			return new LocalizedString("TooManyFoldersErrorBody", "Ex6DF77D", false, true, Strings.ResourceManager, new object[]
			{
				maxFolders,
				connectedAccountsDetailsLinkedText
			});
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004454 File Offset: 0x00002654
		public static LocalizedString RemoteServerIsSlowStatus
		{
			get
			{
				return new LocalizedString("RemoteServerIsSlowStatus", "Ex501EDD", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00004472 File Offset: 0x00002672
		public static LocalizedString SubscriptionUpdatePermanentException
		{
			get
			{
				return new LocalizedString("SubscriptionUpdatePermanentException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004490 File Offset: 0x00002690
		public static LocalizedString PoisonousErrorBody
		{
			get
			{
				return new LocalizedString("PoisonousErrorBody", "ExEF3233", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000044AE File Offset: 0x000026AE
		public static LocalizedString Pop3NonCompliantServerException
		{
			get
			{
				return new LocalizedString("Pop3NonCompliantServerException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000044CC File Offset: 0x000026CC
		public static LocalizedString SuccessStatus
		{
			get
			{
				return new LocalizedString("SuccessStatus", "Ex3072EC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000044EA File Offset: 0x000026EA
		public static LocalizedString ConnectionErrorDetailedStatus
		{
			get
			{
				return new LocalizedString("ConnectionErrorDetailedStatus", "Ex385FD7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00004508 File Offset: 0x00002708
		public static LocalizedString LabsMailboxQuotaWarningWithDelayedStatus
		{
			get
			{
				return new LocalizedString("LabsMailboxQuotaWarningWithDelayedStatus", "ExBFDF43", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004526 File Offset: 0x00002726
		public static LocalizedString IMAPGmailNotSupportedException
		{
			get
			{
				return new LocalizedString("IMAPGmailNotSupportedException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004544 File Offset: 0x00002744
		public static LocalizedString Pop3CannotConnectToServerException
		{
			get
			{
				return new LocalizedString("Pop3CannotConnectToServerException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004562 File Offset: 0x00002762
		public static LocalizedString RemoteServerIsSlowDelayedDetailedStatus
		{
			get
			{
				return new LocalizedString("RemoteServerIsSlowDelayedDetailedStatus", "Ex13382A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004580 File Offset: 0x00002780
		public static LocalizedString LabsMailboxQuoteWarningBody(string connectedAccountsDetailsLinkedText)
		{
			return new LocalizedString("LabsMailboxQuoteWarningBody", "ExE5EEFF", false, true, Strings.ResourceManager, new object[]
			{
				connectedAccountsDetailsLinkedText
			});
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000045B0 File Offset: 0x000027B0
		public static LocalizedString ConnectionErrorDetailedStatusDay(int day)
		{
			return new LocalizedString("ConnectionErrorDetailedStatusDay", "Ex4D4EB0", false, true, Strings.ResourceManager, new object[]
			{
				day
			});
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000045E4 File Offset: 0x000027E4
		public static LocalizedString IMAPSent
		{
			get
			{
				return new LocalizedString("IMAPSent", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004602 File Offset: 0x00002802
		public static LocalizedString StoreRestartedException
		{
			get
			{
				return new LocalizedString("StoreRestartedException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004620 File Offset: 0x00002820
		public static LocalizedString AccessTokenNullOrEmpty
		{
			get
			{
				return new LocalizedString("AccessTokenNullOrEmpty", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004640 File Offset: 0x00002840
		public static LocalizedString ConnectionErrorDetailedStatusHours(int hours)
		{
			return new LocalizedString("ConnectionErrorDetailedStatusHours", "Ex757E95", false, true, Strings.ResourceManager, new object[]
			{
				hours
			});
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004674 File Offset: 0x00002874
		public static LocalizedString Pop3LeaveOnServerNotPossibleException
		{
			get
			{
				return new LocalizedString("Pop3LeaveOnServerNotPossibleException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004694 File Offset: 0x00002894
		public static LocalizedString SyncPropertyValidationException(string property, string value)
		{
			return new LocalizedString("SyncPropertyValidationException", "", false, false, Strings.ResourceManager, new object[]
			{
				property,
				value
			});
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000046C7 File Offset: 0x000028C7
		public static LocalizedString AutoProvisionTestPop3
		{
			get
			{
				return new LocalizedString("AutoProvisionTestPop3", "ExDF5D32", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000046E5 File Offset: 0x000028E5
		public static LocalizedString InvalidSyncEngineStateException
		{
			get
			{
				return new LocalizedString("InvalidSyncEngineStateException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004704 File Offset: 0x00002904
		public static LocalizedString SyncStoreUnhealthyExceptionInfo(Guid databaseGuid, int backOff)
		{
			return new LocalizedString("SyncStoreUnhealthyExceptionInfo", "", false, false, Strings.ResourceManager, new object[]
			{
				databaseGuid,
				backOff
			});
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00004741 File Offset: 0x00002941
		public static LocalizedString ContactCsvFileEmpty
		{
			get
			{
				return new LocalizedString("ContactCsvFileEmpty", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004760 File Offset: 0x00002960
		public static LocalizedString RequestContentException(int statusCode)
		{
			return new LocalizedString("RequestContentException", "", false, false, Strings.ResourceManager, new object[]
			{
				statusCode
			});
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004794 File Offset: 0x00002994
		public static LocalizedString FacebookNonPromotableTransientException
		{
			get
			{
				return new LocalizedString("FacebookNonPromotableTransientException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000047B2 File Offset: 0x000029B2
		public static LocalizedString MaxedOutSyncRelationshipsErrorWithDelayedStatus
		{
			get
			{
				return new LocalizedString("MaxedOutSyncRelationshipsErrorWithDelayedStatus", "Ex044E5F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000047D0 File Offset: 0x000029D0
		public static LocalizedString ContactCsvFileTooLarge(int maxContacts)
		{
			return new LocalizedString("ContactCsvFileTooLarge", "", false, false, Strings.ResourceManager, new object[]
			{
				maxContacts
			});
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004804 File Offset: 0x00002A04
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000001 RID: 1
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(126);

		// Token: 0x04000002 RID: 2
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Transport.Sync.Common.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000003 RID: 3
		public enum IDs : uint
		{
			// Token: 0x04000004 RID: 4
			IMAPUnsupportedVersionException = 995731409U,
			// Token: 0x04000005 RID: 5
			NestedFoldersNotAllowedException = 147813322U,
			// Token: 0x04000006 RID: 6
			LeaveOnServerNotSupportedStatus = 1898862428U,
			// Token: 0x04000007 RID: 7
			IMAPTrash = 1598126189U,
			// Token: 0x04000008 RID: 8
			Pop3MirroredAccountNotPossibleException = 1507388000U,
			// Token: 0x04000009 RID: 9
			RemoteMailboxQuotaWarningWithDisabledStatus = 4121087442U,
			// Token: 0x0400000A RID: 10
			SubscriptionInvalidPasswordException = 3959479424U,
			// Token: 0x0400000B RID: 11
			SubscriptionUpdateTransientException = 1971329929U,
			// Token: 0x0400000C RID: 12
			ConnectionErrorWithDisabledStatus = 1075040750U,
			// Token: 0x0400000D RID: 13
			ConnectedAccountsDetails = 3599922107U,
			// Token: 0x0400000E RID: 14
			IMAPAllMail = 3969501405U,
			// Token: 0x0400000F RID: 15
			MTOMParsingFailedException = 2030480939U,
			// Token: 0x04000010 RID: 16
			LabsMailboxQuotaWarningWithDelayedDetailedStatus = 4079355374U,
			// Token: 0x04000011 RID: 17
			AuthenticationErrorWithDelayedDetailedStatus = 3801534216U,
			// Token: 0x04000012 RID: 18
			RemoteServerIsBackedOffException = 1261870541U,
			// Token: 0x04000013 RID: 19
			LinkedInNonPromotableTransientException = 1401415251U,
			// Token: 0x04000014 RID: 20
			RetryLaterException = 2432721285U,
			// Token: 0x04000015 RID: 21
			ProviderExceptionDetailedStatus = 2971027178U,
			// Token: 0x04000016 RID: 22
			MessageSizeLimitExceededException = 3410639299U,
			// Token: 0x04000017 RID: 23
			CommunicationErrorWithDelayedStatus = 1298696584U,
			// Token: 0x04000018 RID: 24
			MailboxFailure = 604756220U,
			// Token: 0x04000019 RID: 25
			Pop3CapabilitiesNotSupportedException = 3346793726U,
			// Token: 0x0400001A RID: 26
			IMAPInvalidServerException = 580763412U,
			// Token: 0x0400001B RID: 27
			LeaveOnServerNotSupportedDetailedStatus = 3937165620U,
			// Token: 0x0400001C RID: 28
			ConnectionClosedException = 3902086079U,
			// Token: 0x0400001D RID: 29
			PoisonousRemoteServerException = 2952144307U,
			// Token: 0x0400001E RID: 30
			SubscriptionInvalidVersionException = 2394731155U,
			// Token: 0x0400001F RID: 31
			IMAPSentMail = 3561039784U,
			// Token: 0x04000020 RID: 32
			InvalidVersionDetailedStatus = 1756157035U,
			// Token: 0x04000021 RID: 33
			CommunicationErrorWithDisabledStatus = 3613930518U,
			// Token: 0x04000022 RID: 34
			InProgressDetailedStatus = 3694666204U,
			// Token: 0x04000023 RID: 35
			AutoProvisionTestImap = 517632853U,
			// Token: 0x04000024 RID: 36
			DelayedStatus = 3137325842U,
			// Token: 0x04000025 RID: 37
			ProviderExceptionStatus = 3444450370U,
			// Token: 0x04000026 RID: 38
			CommunicationErrorWithDisabledDetailedStatus = 3530466102U,
			// Token: 0x04000027 RID: 39
			RemoteServerIsSlowDisabledDetailedStatus = 4176165696U,
			// Token: 0x04000028 RID: 40
			SendAsVerificationBottomBlock = 2010701853U,
			// Token: 0x04000029 RID: 41
			AuthenticationErrorWithDisabledStatus = 3292189374U,
			// Token: 0x0400002A RID: 42
			ConnectionDownloadedLimitExceededException = 591776832U,
			// Token: 0x0400002B RID: 43
			MailboxOverQuotaException = 1477720531U,
			// Token: 0x0400002C RID: 44
			InvalidServerResponseException = 2149789026U,
			// Token: 0x0400002D RID: 45
			SyncStateSizeErrorDetailedStatus = 2425965665U,
			// Token: 0x0400002E RID: 46
			RemoteAccountDoesNotExistDetailedStatus = 2492614440U,
			// Token: 0x0400002F RID: 47
			IMAPDrafts = 710927991U,
			// Token: 0x04000030 RID: 48
			MaxedOutSyncRelationshipsErrorWithDelayedDetailedStatus = 3842730909U,
			// Token: 0x04000031 RID: 49
			IMAPInvalidItemException = 1812618676U,
			// Token: 0x04000032 RID: 50
			SendAsVerificationSender = 2019254494U,
			// Token: 0x04000033 RID: 51
			IMAPDraft = 2713854040U,
			// Token: 0x04000034 RID: 52
			IMAPSentMessages = 882640929U,
			// Token: 0x04000035 RID: 53
			TlsRemoteCertificateInvalid = 4155460975U,
			// Token: 0x04000036 RID: 54
			Pop3AuthErrorException = 802159867U,
			// Token: 0x04000037 RID: 55
			PoisonStatus = 2900222280U,
			// Token: 0x04000038 RID: 56
			MessageIdGenerationTransientException = 2053678515U,
			// Token: 0x04000039 RID: 57
			AutoProvisionTestAutoDiscover = 3896029100U,
			// Token: 0x0400003A RID: 58
			Pop3TransientSystemAuthErrorException = 3699840920U,
			// Token: 0x0400003B RID: 59
			SyncConflictException = 2759557964U,
			// Token: 0x0400003C RID: 60
			IMAPSentItems = 76218079U,
			// Token: 0x0400003D RID: 61
			SyncEngineSyncStorageProviderCreationException = 1404272750U,
			// Token: 0x0400003E RID: 62
			Pop3TransientLoginDelayedAuthErrorException = 2942865760U,
			// Token: 0x0400003F RID: 63
			IMAPDeletedMessages = 3228154330U,
			// Token: 0x04000040 RID: 64
			RemoteMailboxQuotaWarningWithDelayedStatus = 1124604668U,
			// Token: 0x04000041 RID: 65
			SyncStateSizeErrorStatus = 1488733145U,
			// Token: 0x04000042 RID: 66
			MissingServerResponseException = 4092585897U,
			// Token: 0x04000043 RID: 67
			LabsMailboxQuotaWarningWithDisabledStatus = 4257197704U,
			// Token: 0x04000044 RID: 68
			Pop3TransientInUseAuthErrorException = 4287770443U,
			// Token: 0x04000045 RID: 69
			Pop3DisabledResponseException = 3000749982U,
			// Token: 0x04000046 RID: 70
			SendAsVerificationSubject = 233413355U,
			// Token: 0x04000047 RID: 71
			IMAPAuthenticationException = 1726233450U,
			// Token: 0x04000048 RID: 72
			FailedToGenerateVerificationEmail = 1413995526U,
			// Token: 0x04000049 RID: 73
			RemoteMailboxQuotaWarningWithDelayedDetailedStatus = 1235995580U,
			// Token: 0x0400004A RID: 74
			AuthenticationErrorWithDisabledDetailedStatus = 715816254U,
			// Token: 0x0400004B RID: 75
			ConfigurationErrorStatus = 1257586764U,
			// Token: 0x0400004C RID: 76
			AuthenticationErrorWithDelayedStatus = 189434176U,
			// Token: 0x0400004D RID: 77
			MaxedOutSyncRelationshipsErrorBody = 2936000979U,
			// Token: 0x0400004E RID: 78
			HotmailAccountVerificationFailedException = 2363855804U,
			// Token: 0x0400004F RID: 79
			AutoProvisionResults = 1548922274U,
			// Token: 0x04000050 RID: 80
			InvalidVersionStatus = 2204337803U,
			// Token: 0x04000051 RID: 81
			IMAPDeletedItems = 2984668342U,
			// Token: 0x04000052 RID: 82
			TooManyFoldersStatus = 601262870U,
			// Token: 0x04000053 RID: 83
			SendAsVerificationSignatureTopPart = 2617144415U,
			// Token: 0x04000054 RID: 84
			ConnectionErrorWithDelayedStatus = 2808236588U,
			// Token: 0x04000055 RID: 85
			IMAPJunk = 3760020583U,
			// Token: 0x04000056 RID: 86
			RemoteAccountDoesNotExistStatus = 974222160U,
			// Token: 0x04000057 RID: 87
			TooManyFoldersDetailedStatus = 2167449654U,
			// Token: 0x04000058 RID: 88
			SuccessDetailedStatus = 925456835U,
			// Token: 0x04000059 RID: 89
			InProgressStatus = 2318825308U,
			// Token: 0x0400005A RID: 90
			SubscriptionNotificationEmailBodyStartText = 3335416471U,
			// Token: 0x0400005B RID: 91
			MaxedOutSyncRelationshipsErrorWithDisabledDetailedStatus = 3933215749U,
			// Token: 0x0400005C RID: 92
			IMAPSpam = 2173734488U,
			// Token: 0x0400005D RID: 93
			RemoteMailboxQuotaWarningWithDisabledDetailedStatus = 1734582074U,
			// Token: 0x0400005E RID: 94
			SendAsVerificationTopBlock = 650547385U,
			// Token: 0x0400005F RID: 95
			CommunicationErrorWithDelayedDetailedStatus = 3930645136U,
			// Token: 0x04000060 RID: 96
			ContactCsvFileContainsNoKnownColumns = 4038775200U,
			// Token: 0x04000061 RID: 97
			LabsMailboxQuotaWarningWithDisabledDetailedStatus = 82266192U,
			// Token: 0x04000062 RID: 98
			DelayedDetailedStatus = 420875274U,
			// Token: 0x04000063 RID: 99
			HttpResponseStreamNullException = 3447698883U,
			// Token: 0x04000064 RID: 100
			RemoveSubscriptionStatus = 1059234861U,
			// Token: 0x04000065 RID: 101
			DisabledDetailedStatus = 2325761520U,
			// Token: 0x04000066 RID: 102
			InvalidAggregationSubscriptionIdentity = 3091979858U,
			// Token: 0x04000067 RID: 103
			IMAPJunkEmail = 1325089515U,
			// Token: 0x04000068 RID: 104
			DisabledStatus = 3269288896U,
			// Token: 0x04000069 RID: 105
			MaxedOutSyncRelationshipsErrorWithDisabledStatus = 3779095989U,
			// Token: 0x0400006A RID: 106
			FirstAppendToNotes = 2754299458U,
			// Token: 0x0400006B RID: 107
			InvalidCsvFileFormat = 3826985530U,
			// Token: 0x0400006C RID: 108
			DeltaSyncServiceEndpointsLoadException = 348158591U,
			// Token: 0x0400006D RID: 109
			PoisonDetailedStatus = 2529513648U,
			// Token: 0x0400006E RID: 110
			AutoProvisionTestHotmail = 2425850576U,
			// Token: 0x0400006F RID: 111
			RemoteServerIsSlowStatus = 1096710760U,
			// Token: 0x04000070 RID: 112
			SubscriptionUpdatePermanentException = 623751595U,
			// Token: 0x04000071 RID: 113
			PoisonousErrorBody = 603913161U,
			// Token: 0x04000072 RID: 114
			Pop3NonCompliantServerException = 2979796742U,
			// Token: 0x04000073 RID: 115
			SuccessStatus = 1339731251U,
			// Token: 0x04000074 RID: 116
			ConnectionErrorDetailedStatus = 3068969418U,
			// Token: 0x04000075 RID: 117
			LabsMailboxQuotaWarningWithDelayedStatus = 3345637854U,
			// Token: 0x04000076 RID: 118
			IMAPGmailNotSupportedException = 2505373931U,
			// Token: 0x04000077 RID: 119
			Pop3CannotConnectToServerException = 521865336U,
			// Token: 0x04000078 RID: 120
			RemoteServerIsSlowDelayedDetailedStatus = 2222507634U,
			// Token: 0x04000079 RID: 121
			IMAPSent = 2839399357U,
			// Token: 0x0400007A RID: 122
			StoreRestartedException = 2049114804U,
			// Token: 0x0400007B RID: 123
			AccessTokenNullOrEmpty = 3011192446U,
			// Token: 0x0400007C RID: 124
			Pop3LeaveOnServerNotPossibleException = 1745238072U,
			// Token: 0x0400007D RID: 125
			AutoProvisionTestPop3 = 3743909068U,
			// Token: 0x0400007E RID: 126
			InvalidSyncEngineStateException = 3420783178U,
			// Token: 0x0400007F RID: 127
			ContactCsvFileEmpty = 3910384057U,
			// Token: 0x04000080 RID: 128
			FacebookNonPromotableTransientException = 4000016521U,
			// Token: 0x04000081 RID: 129
			MaxedOutSyncRelationshipsErrorWithDelayedStatus = 305729253U
		}

		// Token: 0x02000004 RID: 4
		private enum ParamIDs
		{
			// Token: 0x04000083 RID: 131
			DelayedDetailedStatusHours,
			// Token: 0x04000084 RID: 132
			UnexpectedContentTypeException,
			// Token: 0x04000085 RID: 133
			SendAsVerificationSalutation,
			// Token: 0x04000086 RID: 134
			SubscriptionInconsistent,
			// Token: 0x04000087 RID: 135
			UserAccessException,
			// Token: 0x04000088 RID: 136
			LeaveOnServerNotSupportedErrorBody,
			// Token: 0x04000089 RID: 137
			RemoteAccountDoesNotExistBody,
			// Token: 0x0400008A RID: 138
			ConnectionErrorBody,
			// Token: 0x0400008B RID: 139
			SyncTooSlowException,
			// Token: 0x0400008C RID: 140
			ConnectionErrorBodyHour,
			// Token: 0x0400008D RID: 141
			MessageDecompressionFailedException,
			// Token: 0x0400008E RID: 142
			FailedSetAggregationSubscription,
			// Token: 0x0400008F RID: 143
			FailedDeleteAggregationSubscription,
			// Token: 0x04000090 RID: 144
			PartnerAuthenticationException,
			// Token: 0x04000091 RID: 145
			FailedCreateAggregationSubscription,
			// Token: 0x04000092 RID: 146
			SubscriptionNotFound,
			// Token: 0x04000093 RID: 147
			MailboxPermanentErrorSavingContact,
			// Token: 0x04000094 RID: 148
			MultipleNativeItemsHaveSameCloudIdException,
			// Token: 0x04000095 RID: 149
			MailboxTransientExceptionSavingContact,
			// Token: 0x04000096 RID: 150
			ConnectionErrorDetailedStatusHour,
			// Token: 0x04000097 RID: 151
			SyncPoisonItemFoundException,
			// Token: 0x04000098 RID: 152
			ConnectionErrorBodyHours,
			// Token: 0x04000099 RID: 153
			SubscriptionSyncException,
			// Token: 0x0400009A RID: 154
			CorruptSubscriptionException,
			// Token: 0x0400009B RID: 155
			DelayedDetailedStatusDay,
			// Token: 0x0400009C RID: 156
			QuotaExceededSavingContact,
			// Token: 0x0400009D RID: 157
			Pop3ErrorResponseException,
			// Token: 0x0400009E RID: 158
			AutoProvisionStatus,
			// Token: 0x0400009F RID: 159
			InternalErrorSavingContact,
			// Token: 0x040000A0 RID: 160
			DeltaSyncServerException,
			// Token: 0x040000A1 RID: 161
			IMAPException,
			// Token: 0x040000A2 RID: 162
			SubscriptionNotificationEmailSubject,
			// Token: 0x040000A3 RID: 163
			SyncUnhandledException,
			// Token: 0x040000A4 RID: 164
			ConnectionErrorDetailedStatusDays,
			// Token: 0x040000A5 RID: 165
			Pop3PermErrorResponseException,
			// Token: 0x040000A6 RID: 166
			DelayedDetailedStatusHour,
			// Token: 0x040000A7 RID: 167
			FailedDeletePeopleConnectSubscription,
			// Token: 0x040000A8 RID: 168
			TlsFailureException,
			// Token: 0x040000A9 RID: 169
			RemoteServerTooSlowException,
			// Token: 0x040000AA RID: 170
			Pop3BrokenResponseException,
			// Token: 0x040000AB RID: 171
			RemoteServerIsSlowErrorBody,
			// Token: 0x040000AC RID: 172
			CommunicationErrorBody,
			// Token: 0x040000AD RID: 173
			UncompressedSyncStateSizeExceededException,
			// Token: 0x040000AE RID: 174
			AuthenticationErrorBody,
			// Token: 0x040000AF RID: 175
			RedundantPimSubscription,
			// Token: 0x040000B0 RID: 176
			SyncStateSizeErrorBody,
			// Token: 0x040000B1 RID: 177
			Pop3UnknownResponseException,
			// Token: 0x040000B2 RID: 178
			CompressedSyncStateSizeExceededException,
			// Token: 0x040000B3 RID: 179
			DelayedDetailedStatusDays,
			// Token: 0x040000B4 RID: 180
			ConnectionErrorBodyDays,
			// Token: 0x040000B5 RID: 181
			TlsFailureErrorOccurred,
			// Token: 0x040000B6 RID: 182
			UnknownDeltaSyncException,
			// Token: 0x040000B7 RID: 183
			RequestFormatException,
			// Token: 0x040000B8 RID: 184
			UnresolveableFolderNameException,
			// Token: 0x040000B9 RID: 185
			RedundantAccountSubscription,
			// Token: 0x040000BA RID: 186
			RequestSettingsException,
			// Token: 0x040000BB RID: 187
			ConnectionErrorBodyDay,
			// Token: 0x040000BC RID: 188
			SubscriptionNumberExceedLimit,
			// Token: 0x040000BD RID: 189
			DataOutOfSyncException,
			// Token: 0x040000BE RID: 190
			TooManyFoldersException,
			// Token: 0x040000BF RID: 191
			SubscriptionNameAlreadyExists,
			// Token: 0x040000C0 RID: 192
			TooManyFoldersErrorBody,
			// Token: 0x040000C1 RID: 193
			LabsMailboxQuoteWarningBody,
			// Token: 0x040000C2 RID: 194
			ConnectionErrorDetailedStatusDay,
			// Token: 0x040000C3 RID: 195
			ConnectionErrorDetailedStatusHours,
			// Token: 0x040000C4 RID: 196
			SyncPropertyValidationException,
			// Token: 0x040000C5 RID: 197
			SyncStoreUnhealthyExceptionInfo,
			// Token: 0x040000C6 RID: 198
			RequestContentException,
			// Token: 0x040000C7 RID: 199
			ContactCsvFileTooLarge
		}
	}
}
