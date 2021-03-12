using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200013E RID: 318
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class Strings
	{
		// Token: 0x060014D8 RID: 5336 RVA: 0x0006BA3C File Offset: 0x00069C3C
		static Strings()
		{
			Strings.stringIDs.Add(86638814U, "RemoteMailboxQuotaWarningStatus");
			Strings.stringIDs.Add(1530080577U, "CompletingMigrationJobCannotBeAppendedTo");
			Strings.stringIDs.Add(2718293401U, "MigrationReportJobInitialSyncComplete");
			Strings.stringIDs.Add(2060675488U, "LabelRunTime");
			Strings.stringIDs.Add(149572871U, "CorruptedMigrationBatchCannotBeStopped");
			Strings.stringIDs.Add(1676389812U, "MigrationExchangeRpcConnectionFailure");
			Strings.stringIDs.Add(1899202903U, "MigrationJobCannotBeRemoved");
			Strings.stringIDs.Add(3099209002U, "CannotSpecifyUnicodeInCredentials");
			Strings.stringIDs.Add(1798653784U, "MigrationCancelledByUserRequest");
			Strings.stringIDs.Add(4872333U, "FailureDuringRemoval");
			Strings.stringIDs.Add(223688197U, "FinalizationErrorSummaryRetryMessage");
			Strings.stringIDs.Add(3674744977U, "MigrationJobAlreadyStopping");
			Strings.stringIDs.Add(2530769896U, "MigrationJobCannotBeStopped");
			Strings.stringIDs.Add(1168096436U, "SubscriptionNotFound");
			Strings.stringIDs.Add(3851331319U, "ContactsMigration");
			Strings.stringIDs.Add(4089501710U, "EmailMigration");
			Strings.stringIDs.Add(3003515263U, "UserProvisioningInternalError");
			Strings.stringIDs.Add(1509794908U, "CompletedMigrationJobCannotBeStartedMultiBatch");
			Strings.stringIDs.Add(1898862428U, "LeaveOnServerNotSupportedStatus");
			Strings.stringIDs.Add(2871031082U, "MigrationLocalDatabasesNotFound");
			Strings.stringIDs.Add(829120357U, "OnlyOnePublicFolderBatchIsAllowedError");
			Strings.stringIDs.Add(1144436407U, "LabelFileName");
			Strings.stringIDs.Add(3073036977U, "MigrationTempMissingDatabase");
			Strings.stringIDs.Add(1781379657U, "MigrationDataCorruptionError");
			Strings.stringIDs.Add(1300976335U, "CutoverAndStagedBatchesCannotCoexistError");
			Strings.stringIDs.Add(2350676710U, "MaximumNumberOfBatchesReached");
			Strings.stringIDs.Add(4040662282U, "PublicFolderMailboxesNotProvisionedError");
			Strings.stringIDs.Add(2999872780U, "CannotSpecifyUnicodeUserIdPasswordWithBasicAuth");
			Strings.stringIDs.Add(853242704U, "CompletingMigrationJobCannotBeStarted");
			Strings.stringIDs.Add(514394488U, "MigrationJobAlreadyStarted");
			Strings.stringIDs.Add(850869622U, "LabelTotalMailboxes");
			Strings.stringIDs.Add(2694765716U, "MigrationJobAlreadyStopped");
			Strings.stringIDs.Add(391393381U, "MigrationGenericError");
			Strings.stringIDs.Add(3393024142U, "CommunicationErrorStatus");
			Strings.stringIDs.Add(3351583406U, "ErrorReportLink");
			Strings.stringIDs.Add(2548932531U, "LabelStartDateTime");
			Strings.stringIDs.Add(1956114709U, "MigrationJobDoesNotSupportAppendingUserCSV");
			Strings.stringIDs.Add(934722052U, "SubscriptionRpcThresholdExceeded");
			Strings.stringIDs.Add(2142750819U, "CorruptedMigrationBatchCannotBeCompleted");
			Strings.stringIDs.Add(437021588U, "RemovedMigrationJobCannotBeStarted");
			Strings.stringIDs.Add(2530286416U, "ErrorCouldNotDeserializeConnectionSettings");
			Strings.stringIDs.Add(1754366117U, "UnknownTimespan");
			Strings.stringIDs.Add(1419436392U, "LabsMailboxQuotaWarningStatus");
			Strings.stringIDs.Add(2571873410U, "ConnectionErrorStatus");
			Strings.stringIDs.Add(1951127083U, "StatisticsReportLink");
			Strings.stringIDs.Add(571725646U, "RemoteServerIsSlow");
			Strings.stringIDs.Add(672478732U, "ErrorMigrationMailboxMissingOrInvalid");
			Strings.stringIDs.Add(1978952169U, "CompletedMigrationJobCannotBeStarted");
			Strings.stringIDs.Add(2036241881U, "MigrationReportFailed");
			Strings.stringIDs.Add(1271432035U, "MigrationCancelledDueToInternalError");
			Strings.stringIDs.Add(1786933629U, "PublicFolderMigrationBatchCannotBeCompletedWithErrors");
			Strings.stringIDs.Add(85986654U, "MigrationJobCannotRetryCompletion");
			Strings.stringIDs.Add(3662584967U, "MissingRequiredSubscriptionId");
			Strings.stringIDs.Add(1905833821U, "IMAPPathPrefixInvalidStatus");
			Strings.stringIDs.Add(601262870U, "TooManyFoldersStatus");
			Strings.stringIDs.Add(1066087601U, "LabelSubmittedByUser");
			Strings.stringIDs.Add(1842619565U, "CSVFileTooLarge");
			Strings.stringIDs.Add(1189164420U, "RemovedMigrationJobCannotBeModified");
			Strings.stringIDs.Add(4257873367U, "LabelCompleted");
			Strings.stringIDs.Add(1047974326U, "MigrationPublicFolderWireUpFailed");
			Strings.stringIDs.Add(1386196812U, "LabelCouldntMigrate");
			Strings.stringIDs.Add(3955323767U, "MigrationTenantPermissionFailure");
			Strings.stringIDs.Add(1043780694U, "UnknownMigrationBatchError");
			Strings.stringIDs.Add(1332982264U, "MigrationReportJobComplete");
			Strings.stringIDs.Add(367906109U, "UnableToDisableSubscription");
			Strings.stringIDs.Add(1279919936U, "OnlyOneCutoverBatchIsAllowedError");
			Strings.stringIDs.Add(2536192239U, "CouldNotUpdateSubscriptionSettingsWithoutBatch");
			Strings.stringIDs.Add(3327693399U, "CorruptedMigrationBatchCannotBeStarted");
			Strings.stringIDs.Add(617690654U, "ProvisioningThrottledBack");
			Strings.stringIDs.Add(4177113737U, "CompletedMigrationJobCannotBeModified");
			Strings.stringIDs.Add(3751987975U, "CorruptedMigrationBatchCannotBeModified");
			Strings.stringIDs.Add(5503809U, "LabelTotalRows");
			Strings.stringIDs.Add(1136304310U, "LabelSynced");
			Strings.stringIDs.Add(1088630310U, "CouldNotDiscoverNSPISettings");
			Strings.stringIDs.Add(16533286U, "SyncingMigrationJobCannotBeAppendedTo");
			Strings.stringIDs.Add(4038100652U, "MigrationExchangeCredentialFailure");
			Strings.stringIDs.Add(1756157035U, "InvalidVersionDetailedStatus");
			Strings.stringIDs.Add(2104158819U, "MigrationReportJobAutoStarted");
			Strings.stringIDs.Add(320046086U, "RemovingMigrationUserBatchMustBeIdle");
			Strings.stringIDs.Add(759640474U, "NoDataMigrated");
			Strings.stringIDs.Add(2605498839U, "SubscriptionDisabledSinceFinalized");
			Strings.stringIDs.Add(2047379006U, "AuthenticationErrorStatus");
			Strings.stringIDs.Add(986156097U, "MigrationReportJobTransientError");
			Strings.stringIDs.Add(1214466260U, "MigrationJobAlreadyHasPendingCSV");
			Strings.stringIDs.Add(3504597832U, "ErrorConnectionSettingsNotSerialized");
			Strings.stringIDs.Add(1236042521U, "PasswordPreviouslySet");
			Strings.stringIDs.Add(3020735710U, "MigrationJobAlreadyQueued");
			Strings.stringIDs.Add(2529513648U, "PoisonDetailedStatus");
			Strings.stringIDs.Add(1500792389U, "NoPartialMigrationSummaryMessage");
			Strings.stringIDs.Add(3968870632U, "StoppingMigrationJobCannotBeStarted");
			Strings.stringIDs.Add(1142848800U, "FailedToReadPublicFoldersOnPremise");
			Strings.stringIDs.Add(1731044986U, "MailboxNotFoundSubscriptionStatus");
			Strings.stringIDs.Add(1197149433U, "ReadPublicFoldersOnTargetInternalError");
			Strings.stringIDs.Add(3334133771U, "ExternallySuspendedFailure");
			Strings.stringIDs.Add(4060495101U, "SyncStateSizeError");
			Strings.stringIDs.Add(1246705638U, "LabelLogMailFooter");
			Strings.stringIDs.Add(4122602291U, "CorruptedSubscriptionStatus");
			Strings.stringIDs.Add(295927301U, "SignInMightBeRequired");
			Strings.stringIDs.Add(4162391104U, "MigrationTempMissingMigrationMailbox");
			Strings.stringIDs.Add(501292080U, "MigrationJobCannotBeCompleted");
			Strings.stringIDs.Add(2595351182U, "UnknownMigrationError");
			Strings.stringIDs.Add(1980396424U, "CompleteMigrationBatchNotSupported");
			Strings.stringIDs.Add(2353197456U, "ConfigAccessRuntimeError");
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x0006C284 File Offset: 0x0006A484
		public static LocalizedString RemoteMailboxQuotaWarningStatus
		{
			get
			{
				return new LocalizedString("RemoteMailboxQuotaWarningStatus", "Ex434D8F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060014DA RID: 5338 RVA: 0x0006C2A2 File Offset: 0x0006A4A2
		public static LocalizedString CompletingMigrationJobCannotBeAppendedTo
		{
			get
			{
				return new LocalizedString("CompletingMigrationJobCannotBeAppendedTo", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x0006C2C0 File Offset: 0x0006A4C0
		public static LocalizedString MigrationBatchCompletionReportMailErrorHeader(string batchName)
		{
			return new LocalizedString("MigrationBatchCompletionReportMailErrorHeader", "ExA4F7F4", false, true, Strings.ResourceManager, new object[]
			{
				batchName
			});
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0006C2F0 File Offset: 0x0006A4F0
		public static LocalizedString MigrationReportJobItemCorrupted(string user)
		{
			return new LocalizedString("MigrationReportJobItemCorrupted", "", false, false, Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0006C320 File Offset: 0x0006A520
		public static LocalizedString UnsupportedSourceRecipientTypeError(string type)
		{
			return new LocalizedString("UnsupportedSourceRecipientTypeError", "Ex3271B6", false, true, Strings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0006C350 File Offset: 0x0006A550
		public static LocalizedString MigrationJobCannotBeDeletedWithPendingItems(int count)
		{
			return new LocalizedString("MigrationJobCannotBeDeletedWithPendingItems", "", false, false, Strings.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x0006C384 File Offset: 0x0006A584
		public static LocalizedString UnsupportedAdminCulture(string culture)
		{
			return new LocalizedString("UnsupportedAdminCulture", "", false, false, Strings.ResourceManager, new object[]
			{
				culture
			});
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x0006C3B4 File Offset: 0x0006A5B4
		public static LocalizedString MigrationProcessorInvalidation(string processor, string jobname)
		{
			return new LocalizedString("MigrationProcessorInvalidation", "", false, false, Strings.ResourceManager, new object[]
			{
				processor,
				jobname
			});
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x0006C3E7 File Offset: 0x0006A5E7
		public static LocalizedString MigrationReportJobInitialSyncComplete
		{
			get
			{
				return new LocalizedString("MigrationReportJobInitialSyncComplete", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0006C408 File Offset: 0x0006A608
		public static LocalizedString MigrationJobItemNotFound(string identity)
		{
			return new LocalizedString("MigrationJobItemNotFound", "", false, false, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x0006C437 File Offset: 0x0006A637
		public static LocalizedString LabelRunTime
		{
			get
			{
				return new LocalizedString("LabelRunTime", "Ex6C0D2C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x0006C458 File Offset: 0x0006A658
		public static LocalizedString MigrationFeatureNotSupported(string feature)
		{
			return new LocalizedString("MigrationFeatureNotSupported", "", false, false, Strings.ResourceManager, new object[]
			{
				feature
			});
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x0006C487 File Offset: 0x0006A687
		public static LocalizedString CorruptedMigrationBatchCannotBeStopped
		{
			get
			{
				return new LocalizedString("CorruptedMigrationBatchCannotBeStopped", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x0006C4A8 File Offset: 0x0006A6A8
		public static LocalizedString MigrationJobNotFound(string mailboxName)
		{
			return new LocalizedString("MigrationJobNotFound", "ExBDDC58", false, true, Strings.ResourceManager, new object[]
			{
				mailboxName
			});
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x0006C4D7 File Offset: 0x0006A6D7
		public static LocalizedString MigrationExchangeRpcConnectionFailure
		{
			get
			{
				return new LocalizedString("MigrationExchangeRpcConnectionFailure", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x0006C4F5 File Offset: 0x0006A6F5
		public static LocalizedString MigrationJobCannotBeRemoved
		{
			get
			{
				return new LocalizedString("MigrationJobCannotBeRemoved", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x0006C514 File Offset: 0x0006A714
		public static LocalizedString ErrorNspiNotSupportedForEndpointType(MigrationType type)
		{
			return new LocalizedString("ErrorNspiNotSupportedForEndpointType", "", false, false, Strings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x0006C548 File Offset: 0x0006A748
		public static LocalizedString UserDuplicateInCSV(string alias)
		{
			return new LocalizedString("UserDuplicateInCSV", "Ex75BB6D", false, true, Strings.ResourceManager, new object[]
			{
				alias
			});
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0006C578 File Offset: 0x0006A778
		public static LocalizedString MigrationOrganizationNotFound(string mailboxName)
		{
			return new LocalizedString("MigrationOrganizationNotFound", "", false, false, Strings.ResourceManager, new object[]
			{
				mailboxName
			});
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x0006C5A7 File Offset: 0x0006A7A7
		public static LocalizedString CannotSpecifyUnicodeInCredentials
		{
			get
			{
				return new LocalizedString("CannotSpecifyUnicodeInCredentials", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0006C5C8 File Offset: 0x0006A7C8
		public static LocalizedString MigrationReportRepaired(string user, string action)
		{
			return new LocalizedString("MigrationReportRepaired", "", false, false, Strings.ResourceManager, new object[]
			{
				user,
				action
			});
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x0006C5FC File Offset: 0x0006A7FC
		public static LocalizedString LabelAutoRetry(int attemptsRemaining)
		{
			return new LocalizedString("LabelAutoRetry", "", false, false, Strings.ResourceManager, new object[]
			{
				attemptsRemaining
			});
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x0006C630 File Offset: 0x0006A830
		public static LocalizedString MigrationCancelledByUserRequest
		{
			get
			{
				return new LocalizedString("MigrationCancelledByUserRequest", "ExA58CCA", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x0006C64E File Offset: 0x0006A84E
		public static LocalizedString FailureDuringRemoval
		{
			get
			{
				return new LocalizedString("FailureDuringRemoval", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x0006C66C File Offset: 0x0006A86C
		public static LocalizedString FinalizationErrorSummaryRetryMessage
		{
			get
			{
				return new LocalizedString("FinalizationErrorSummaryRetryMessage", "ExC05D2F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x0006C68C File Offset: 0x0006A88C
		public static LocalizedString CouldNotLoadMigrationPersistedItem(string itemId)
		{
			return new LocalizedString("CouldNotLoadMigrationPersistedItem", "", false, false, Strings.ResourceManager, new object[]
			{
				itemId
			});
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0006C6BC File Offset: 0x0006A8BC
		public static LocalizedString MigrationReportJobItemRetried(string user)
		{
			return new LocalizedString("MigrationReportJobItemRetried", "", false, false, Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0006C6EC File Offset: 0x0006A8EC
		public static LocalizedString MigrationItemLastUpdatedInTheFuture(string time)
		{
			return new LocalizedString("MigrationItemLastUpdatedInTheFuture", "", false, false, Strings.ResourceManager, new object[]
			{
				time
			});
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x0006C71B File Offset: 0x0006A91B
		public static LocalizedString MigrationJobAlreadyStopping
		{
			get
			{
				return new LocalizedString("MigrationJobAlreadyStopping", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x0006C739 File Offset: 0x0006A939
		public static LocalizedString MigrationJobCannotBeStopped
		{
			get
			{
				return new LocalizedString("MigrationJobCannotBeStopped", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x0006C758 File Offset: 0x0006A958
		public static LocalizedString FailedToUpdateRecipientSource(string targetSmtpAddress)
		{
			return new LocalizedString("FailedToUpdateRecipientSource", "ExE49A98", false, true, Strings.ResourceManager, new object[]
			{
				targetSmtpAddress
			});
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x0006C788 File Offset: 0x0006A988
		public static LocalizedString MigrationReportJobCreated(string userName, MigrationType migrationType)
		{
			return new LocalizedString("MigrationReportJobCreated", "", false, false, Strings.ResourceManager, new object[]
			{
				userName,
				migrationType
			});
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x0006C7C0 File Offset: 0x0006A9C0
		public static LocalizedString MigrationSubscriptionCreationFailed(string user)
		{
			return new LocalizedString("MigrationSubscriptionCreationFailed", "", false, false, Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x0006C7EF File Offset: 0x0006A9EF
		public static LocalizedString SubscriptionNotFound
		{
			get
			{
				return new LocalizedString("SubscriptionNotFound", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x0006C810 File Offset: 0x0006AA10
		public static LocalizedString MigrationReportNspiFailed(string context, string status)
		{
			return new LocalizedString("MigrationReportNspiFailed", "", false, false, Strings.ResourceManager, new object[]
			{
				context,
				status
			});
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x0006C843 File Offset: 0x0006AA43
		public static LocalizedString ContactsMigration
		{
			get
			{
				return new LocalizedString("ContactsMigration", "Ex8D84CD", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x0006C864 File Offset: 0x0006AA64
		public static LocalizedString UnexpectedSubscriptionStatus(string status)
		{
			return new LocalizedString("UnexpectedSubscriptionStatus", "", false, false, Strings.ResourceManager, new object[]
			{
				status
			});
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x0006C893 File Offset: 0x0006AA93
		public static LocalizedString EmailMigration
		{
			get
			{
				return new LocalizedString("EmailMigration", "ExEB071C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x0006C8B4 File Offset: 0x0006AAB4
		public static LocalizedString MigrationRpcFailed(string result)
		{
			return new LocalizedString("MigrationRpcFailed", "", false, false, Strings.ResourceManager, new object[]
			{
				result
			});
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x0006C8E3 File Offset: 0x0006AAE3
		public static LocalizedString UserProvisioningInternalError
		{
			get
			{
				return new LocalizedString("UserProvisioningInternalError", "ExF29C1F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x0006C904 File Offset: 0x0006AB04
		public static LocalizedString MigrationBatchCompletionReportMailHeader(string batchName)
		{
			return new LocalizedString("MigrationBatchCompletionReportMailHeader", "Ex37C091", false, true, Strings.ResourceManager, new object[]
			{
				batchName
			});
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x0006C934 File Offset: 0x0006AB34
		public static LocalizedString MigrationReportNspiRfrFailed(string context, string status)
		{
			return new LocalizedString("MigrationReportNspiRfrFailed", "", false, false, Strings.ResourceManager, new object[]
			{
				context,
				status
			});
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x0006C967 File Offset: 0x0006AB67
		public static LocalizedString CompletedMigrationJobCannotBeStartedMultiBatch
		{
			get
			{
				return new LocalizedString("CompletedMigrationJobCannotBeStartedMultiBatch", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x0006C988 File Offset: 0x0006AB88
		public static LocalizedString MigrationVersionMismatch(long version, long expectedVersion)
		{
			return new LocalizedString("MigrationVersionMismatch", "", false, false, Strings.ResourceManager, new object[]
			{
				version,
				expectedVersion
			});
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x0006C9C8 File Offset: 0x0006ABC8
		public static LocalizedString MigrationBatchCancelledBySystem(string batchName)
		{
			return new LocalizedString("MigrationBatchCancelledBySystem", "Ex1516F4", false, true, Strings.ResourceManager, new object[]
			{
				batchName
			});
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0006C9F8 File Offset: 0x0006ABF8
		public static LocalizedString BatchCancelledByUser(string fileName)
		{
			return new LocalizedString("BatchCancelledByUser", "Ex59EB12", false, true, Strings.ResourceManager, new object[]
			{
				fileName
			});
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001507 RID: 5383 RVA: 0x0006CA27 File Offset: 0x0006AC27
		public static LocalizedString LeaveOnServerNotSupportedStatus
		{
			get
			{
				return new LocalizedString("LeaveOnServerNotSupportedStatus", "ExE8E9AF", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x0006CA48 File Offset: 0x0006AC48
		public static LocalizedString InvalidRootFolderMappingInCSVError(int rowIndex, string folderPath, string identifier, string hierarchyMailboxName)
		{
			return new LocalizedString("InvalidRootFolderMappingInCSVError", "", false, false, Strings.ResourceManager, new object[]
			{
				rowIndex,
				folderPath,
				identifier,
				hierarchyMailboxName
			});
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x0006CA88 File Offset: 0x0006AC88
		public static LocalizedString DuplicateFolderInCSVError(int rowIndex, string folderPath, string identifier)
		{
			return new LocalizedString("DuplicateFolderInCSVError", "", false, false, Strings.ResourceManager, new object[]
			{
				rowIndex,
				folderPath,
				identifier
			});
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x0006CAC4 File Offset: 0x0006ACC4
		public static LocalizedString MigrationLocalDatabasesNotFound
		{
			get
			{
				return new LocalizedString("MigrationLocalDatabasesNotFound", "ExC4DB1F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x0006CAE4 File Offset: 0x0006ACE4
		public static LocalizedString ErrorTooManyTransientFailures(string batchIdentity)
		{
			return new LocalizedString("ErrorTooManyTransientFailures", "", false, false, Strings.ResourceManager, new object[]
			{
				batchIdentity
			});
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x0006CB13 File Offset: 0x0006AD13
		public static LocalizedString OnlyOnePublicFolderBatchIsAllowedError
		{
			get
			{
				return new LocalizedString("OnlyOnePublicFolderBatchIsAllowedError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x0006CB31 File Offset: 0x0006AD31
		public static LocalizedString LabelFileName
		{
			get
			{
				return new LocalizedString("LabelFileName", "Ex57FED3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x0600150E RID: 5390 RVA: 0x0006CB4F File Offset: 0x0006AD4F
		public static LocalizedString MigrationTempMissingDatabase
		{
			get
			{
				return new LocalizedString("MigrationTempMissingDatabase", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x0006CB6D File Offset: 0x0006AD6D
		public static LocalizedString MigrationDataCorruptionError
		{
			get
			{
				return new LocalizedString("MigrationDataCorruptionError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x0006CB8B File Offset: 0x0006AD8B
		public static LocalizedString CutoverAndStagedBatchesCannotCoexistError
		{
			get
			{
				return new LocalizedString("CutoverAndStagedBatchesCannotCoexistError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x0006CBAC File Offset: 0x0006ADAC
		public static LocalizedString RunTimeFormatHours(int hours, int minutes)
		{
			return new LocalizedString("RunTimeFormatHours", "ExC7DA67", false, true, Strings.ResourceManager, new object[]
			{
				hours,
				minutes
			});
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x0006CBE9 File Offset: 0x0006ADE9
		public static LocalizedString MaximumNumberOfBatchesReached
		{
			get
			{
				return new LocalizedString("MaximumNumberOfBatchesReached", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x0006CC07 File Offset: 0x0006AE07
		public static LocalizedString PublicFolderMailboxesNotProvisionedError
		{
			get
			{
				return new LocalizedString("PublicFolderMailboxesNotProvisionedError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0006CC28 File Offset: 0x0006AE28
		public static LocalizedString MigrationReportJobItemRemovedInternal(string identity)
		{
			return new LocalizedString("MigrationReportJobItemRemovedInternal", "", false, false, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x0006CC58 File Offset: 0x0006AE58
		public static LocalizedString RecipientDoesNotExistAtSource(string email)
		{
			return new LocalizedString("RecipientDoesNotExistAtSource", "Ex4F0A6F", false, true, Strings.ResourceManager, new object[]
			{
				email
			});
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x0006CC87 File Offset: 0x0006AE87
		public static LocalizedString CannotSpecifyUnicodeUserIdPasswordWithBasicAuth
		{
			get
			{
				return new LocalizedString("CannotSpecifyUnicodeUserIdPasswordWithBasicAuth", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0006CCA8 File Offset: 0x0006AEA8
		public static LocalizedString MigrationReportJobItemCreatedInternal(string identity)
		{
			return new LocalizedString("MigrationReportJobItemCreatedInternal", "", false, false, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x0006CCD7 File Offset: 0x0006AED7
		public static LocalizedString CompletingMigrationJobCannotBeStarted
		{
			get
			{
				return new LocalizedString("CompletingMigrationJobCannotBeStarted", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x0006CCF5 File Offset: 0x0006AEF5
		public static LocalizedString MigrationJobAlreadyStarted
		{
			get
			{
				return new LocalizedString("MigrationJobAlreadyStarted", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x0006CD14 File Offset: 0x0006AF14
		public static LocalizedString ErrorSummaryMessageSingular(int errorCount)
		{
			return new LocalizedString("ErrorSummaryMessageSingular", "ExC5BA76", false, true, Strings.ResourceManager, new object[]
			{
				errorCount
			});
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x0006CD48 File Offset: 0x0006AF48
		public static LocalizedString MigrationReportJobItemWithError(string user, LocalizedString message)
		{
			return new LocalizedString("MigrationReportJobItemWithError", "", false, false, Strings.ResourceManager, new object[]
			{
				user,
				message
			});
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x0006CD80 File Offset: 0x0006AF80
		public static LocalizedString ErrorUserAlreadyBeingMigrated(string identity)
		{
			return new LocalizedString("ErrorUserAlreadyBeingMigrated", "", false, false, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x0006CDB0 File Offset: 0x0006AFB0
		public static LocalizedString MigrationBatchReportMailHeader(string batchName)
		{
			return new LocalizedString("MigrationBatchReportMailHeader", "", false, false, Strings.ResourceManager, new object[]
			{
				batchName
			});
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0006CDE0 File Offset: 0x0006AFE0
		public static LocalizedString DetailedAggregationStatus(string code)
		{
			return new LocalizedString("DetailedAggregationStatus", "", false, false, Strings.ResourceManager, new object[]
			{
				code
			});
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x0006CE0F File Offset: 0x0006B00F
		public static LocalizedString LabelTotalMailboxes
		{
			get
			{
				return new LocalizedString("LabelTotalMailboxes", "Ex4D931B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x0006CE2D File Offset: 0x0006B02D
		public static LocalizedString MigrationJobAlreadyStopped
		{
			get
			{
				return new LocalizedString("MigrationJobAlreadyStopped", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x0006CE4B File Offset: 0x0006B04B
		public static LocalizedString MigrationGenericError
		{
			get
			{
				return new LocalizedString("MigrationGenericError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0006CE6C File Offset: 0x0006B06C
		public static LocalizedString UnsupportedTargetRecipientTypeError(string type)
		{
			return new LocalizedString("UnsupportedTargetRecipientTypeError", "ExFB3FDA", false, true, Strings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x0006CE9B File Offset: 0x0006B09B
		public static LocalizedString CommunicationErrorStatus
		{
			get
			{
				return new LocalizedString("CommunicationErrorStatus", "Ex0866B8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x0006CEB9 File Offset: 0x0006B0B9
		public static LocalizedString ErrorReportLink
		{
			get
			{
				return new LocalizedString("ErrorReportLink", "Ex779419", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x0006CED7 File Offset: 0x0006B0D7
		public static LocalizedString LabelStartDateTime
		{
			get
			{
				return new LocalizedString("LabelStartDateTime", "Ex797C8C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0006CEF8 File Offset: 0x0006B0F8
		public static LocalizedString ErrorUserMissingOrWithoutRBAC(string username)
		{
			return new LocalizedString("ErrorUserMissingOrWithoutRBAC", "", false, false, Strings.ResourceManager, new object[]
			{
				username
			});
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0006CF28 File Offset: 0x0006B128
		public static LocalizedString PartialMigrationSummaryMessagePlural(int partialMigrationCount)
		{
			return new LocalizedString("PartialMigrationSummaryMessagePlural", "Ex63E0A5", false, true, Strings.ResourceManager, new object[]
			{
				partialMigrationCount
			});
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0006CF5C File Offset: 0x0006B15C
		public static LocalizedString MigrationReportJobItemRemoved(string username, string identity)
		{
			return new LocalizedString("MigrationReportJobItemRemoved", "", false, false, Strings.ResourceManager, new object[]
			{
				username,
				identity
			});
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x0006CF8F File Offset: 0x0006B18F
		public static LocalizedString MigrationJobDoesNotSupportAppendingUserCSV
		{
			get
			{
				return new LocalizedString("MigrationJobDoesNotSupportAppendingUserCSV", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x0006CFAD File Offset: 0x0006B1AD
		public static LocalizedString SubscriptionRpcThresholdExceeded
		{
			get
			{
				return new LocalizedString("SubscriptionRpcThresholdExceeded", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0006CFCC File Offset: 0x0006B1CC
		public static LocalizedString CannotUpgradeMigrationVersion(string explanation)
		{
			return new LocalizedString("CannotUpgradeMigrationVersion", "", false, false, Strings.ResourceManager, new object[]
			{
				explanation
			});
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0006CFFC File Offset: 0x0006B1FC
		public static LocalizedString AutoDiscoverInternalError(LocalizedString details)
		{
			return new LocalizedString("AutoDiscoverInternalError", "", false, false, Strings.ResourceManager, new object[]
			{
				details
			});
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0006D030 File Offset: 0x0006B230
		public static LocalizedString MigrationUserAlreadyExistsInDifferentType(string batchName, string batchType)
		{
			return new LocalizedString("MigrationUserAlreadyExistsInDifferentType", "", false, false, Strings.ResourceManager, new object[]
			{
				batchName,
				batchType
			});
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x0006D063 File Offset: 0x0006B263
		public static LocalizedString CorruptedMigrationBatchCannotBeCompleted
		{
			get
			{
				return new LocalizedString("CorruptedMigrationBatchCannotBeCompleted", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x0006D081 File Offset: 0x0006B281
		public static LocalizedString RemovedMigrationJobCannotBeStarted
		{
			get
			{
				return new LocalizedString("RemovedMigrationJobCannotBeStarted", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x0006D09F File Offset: 0x0006B29F
		public static LocalizedString ErrorCouldNotDeserializeConnectionSettings
		{
			get
			{
				return new LocalizedString("ErrorCouldNotDeserializeConnectionSettings", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x0006D0BD File Offset: 0x0006B2BD
		public static LocalizedString UnknownTimespan
		{
			get
			{
				return new LocalizedString("UnknownTimespan", "Ex20DFAB", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x0006D0DB File Offset: 0x0006B2DB
		public static LocalizedString LabsMailboxQuotaWarningStatus
		{
			get
			{
				return new LocalizedString("LabsMailboxQuotaWarningStatus", "Ex953087", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x0006D0FC File Offset: 0x0006B2FC
		public static LocalizedString MigrationReportJobModifiedByUser(string userName)
		{
			return new LocalizedString("MigrationReportJobModifiedByUser", "", false, false, Strings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x0006D12C File Offset: 0x0006B32C
		public static LocalizedString ErrorInvalidRecipientType(string actual, string expected)
		{
			return new LocalizedString("ErrorInvalidRecipientType", "", false, false, Strings.ResourceManager, new object[]
			{
				actual,
				expected
			});
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x0006D15F File Offset: 0x0006B35F
		public static LocalizedString ConnectionErrorStatus
		{
			get
			{
				return new LocalizedString("ConnectionErrorStatus", "Ex23333F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0006D180 File Offset: 0x0006B380
		public static LocalizedString MigrationUserAlreadyRemoved(string user)
		{
			return new LocalizedString("MigrationUserAlreadyRemoved", "", false, false, Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x0006D1AF File Offset: 0x0006B3AF
		public static LocalizedString StatisticsReportLink
		{
			get
			{
				return new LocalizedString("StatisticsReportLink", "ExBC8D8F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x0006D1CD File Offset: 0x0006B3CD
		public static LocalizedString RemoteServerIsSlow
		{
			get
			{
				return new LocalizedString("RemoteServerIsSlow", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x0006D1EB File Offset: 0x0006B3EB
		public static LocalizedString ErrorMigrationMailboxMissingOrInvalid
		{
			get
			{
				return new LocalizedString("ErrorMigrationMailboxMissingOrInvalid", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x0006D209 File Offset: 0x0006B409
		public static LocalizedString CompletedMigrationJobCannotBeStarted
		{
			get
			{
				return new LocalizedString("CompletedMigrationJobCannotBeStarted", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0006D228 File Offset: 0x0006B428
		public static LocalizedString FinalizationErrorSummaryMessageSingular(int errorCount)
		{
			return new LocalizedString("FinalizationErrorSummaryMessageSingular", "Ex72CD80", false, true, Strings.ResourceManager, new object[]
			{
				errorCount
			});
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0006D25C File Offset: 0x0006B45C
		public static LocalizedString ErrorParsingCSV(int rowIndex, string errorMessage)
		{
			return new LocalizedString("ErrorParsingCSV", "", false, false, Strings.ResourceManager, new object[]
			{
				rowIndex,
				errorMessage
			});
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0006D294 File Offset: 0x0006B494
		public static LocalizedString MigrationConfigString(string maxnumbatches, string features)
		{
			return new LocalizedString("MigrationConfigString", "", false, false, Strings.ResourceManager, new object[]
			{
				maxnumbatches,
				features
			});
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x0006D2C7 File Offset: 0x0006B4C7
		public static LocalizedString MigrationReportFailed
		{
			get
			{
				return new LocalizedString("MigrationReportFailed", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0006D2E8 File Offset: 0x0006B4E8
		public static LocalizedString ErrorSummaryMessagePlural(int errorCount)
		{
			return new LocalizedString("ErrorSummaryMessagePlural", "Ex1A34B0", false, true, Strings.ResourceManager, new object[]
			{
				errorCount
			});
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0006D31C File Offset: 0x0006B51C
		public static LocalizedString ValueNotProvidedForColumn(string columnName)
		{
			return new LocalizedString("ValueNotProvidedForColumn", "ExFA1E73", false, true, Strings.ResourceManager, new object[]
			{
				columnName
			});
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0006D34C File Offset: 0x0006B54C
		public static LocalizedString UserAlreadyMigratedWithAlternateEmail(string previous)
		{
			return new LocalizedString("UserAlreadyMigratedWithAlternateEmail", "", false, false, Strings.ResourceManager, new object[]
			{
				previous
			});
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x0006D37B File Offset: 0x0006B57B
		public static LocalizedString MigrationCancelledDueToInternalError
		{
			get
			{
				return new LocalizedString("MigrationCancelledDueToInternalError", "ExA70D62", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0006D39C File Offset: 0x0006B59C
		public static LocalizedString MigrationFinalizationReportMailErrorHeader(string batchName)
		{
			return new LocalizedString("MigrationFinalizationReportMailErrorHeader", "Ex1A8B79", false, true, Strings.ResourceManager, new object[]
			{
				batchName
			});
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001544 RID: 5444 RVA: 0x0006D3CB File Offset: 0x0006B5CB
		public static LocalizedString PublicFolderMigrationBatchCannotBeCompletedWithErrors
		{
			get
			{
				return new LocalizedString("PublicFolderMigrationBatchCannotBeCompletedWithErrors", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x0006D3EC File Offset: 0x0006B5EC
		public static LocalizedString ErrorAutoDiscoveryNotSupported(MigrationType type)
		{
			return new LocalizedString("ErrorAutoDiscoveryNotSupported", "", false, false, Strings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x0006D420 File Offset: 0x0006B620
		public static LocalizedString MigrationJobCannotRetryCompletion
		{
			get
			{
				return new LocalizedString("MigrationJobCannotRetryCompletion", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x0006D43E File Offset: 0x0006B63E
		public static LocalizedString MissingRequiredSubscriptionId
		{
			get
			{
				return new LocalizedString("MissingRequiredSubscriptionId", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x0006D45C File Offset: 0x0006B65C
		public static LocalizedString RunTimeFormatMinutes(int minutes)
		{
			return new LocalizedString("RunTimeFormatMinutes", "ExF67C7F", false, true, Strings.ResourceManager, new object[]
			{
				minutes
			});
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x0006D490 File Offset: 0x0006B690
		public static LocalizedString RunTimeFormatDays(int days, int hours, int minutes)
		{
			return new LocalizedString("RunTimeFormatDays", "ExE5DB04", false, true, Strings.ResourceManager, new object[]
			{
				days,
				hours,
				minutes
			});
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x0006D4D8 File Offset: 0x0006B6D8
		public static LocalizedString ErrorMissingExpectedCapability(string user, string capability)
		{
			return new LocalizedString("ErrorMissingExpectedCapability", "", false, false, Strings.ResourceManager, new object[]
			{
				user,
				capability
			});
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x0006D50B File Offset: 0x0006B70B
		public static LocalizedString IMAPPathPrefixInvalidStatus
		{
			get
			{
				return new LocalizedString("IMAPPathPrefixInvalidStatus", "ExCBABF6", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0006D52C File Offset: 0x0006B72C
		public static LocalizedString MigrationReportSetString(string creationTime, string successUrl, string errorUrl)
		{
			return new LocalizedString("MigrationReportSetString", "", false, false, Strings.ResourceManager, new object[]
			{
				creationTime,
				successUrl,
				errorUrl
			});
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0006D564 File Offset: 0x0006B764
		public static LocalizedString MigrationEndpointAlreadyExistsError(string endpointIdentity)
		{
			return new LocalizedString("MigrationEndpointAlreadyExistsError", "", false, false, Strings.ResourceManager, new object[]
			{
				endpointIdentity
			});
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x0006D593 File Offset: 0x0006B793
		public static LocalizedString TooManyFoldersStatus
		{
			get
			{
				return new LocalizedString("TooManyFoldersStatus", "Ex2E6D50", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x0006D5B1 File Offset: 0x0006B7B1
		public static LocalizedString LabelSubmittedByUser
		{
			get
			{
				return new LocalizedString("LabelSubmittedByUser", "ExFB88A4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x0006D5D0 File Offset: 0x0006B7D0
		public static LocalizedString MigrationBatchReportMailErrorHeader(string batchName)
		{
			return new LocalizedString("MigrationBatchReportMailErrorHeader", "", false, false, Strings.ResourceManager, new object[]
			{
				batchName
			});
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0006D600 File Offset: 0x0006B800
		public static LocalizedString CouldNotAddExchangeSnapIn(string snapInName)
		{
			return new LocalizedString("CouldNotAddExchangeSnapIn", "", false, false, Strings.ResourceManager, new object[]
			{
				snapInName
			});
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x0006D630 File Offset: 0x0006B830
		public static LocalizedString ErrorNoEndpointSupportForMigrationType(MigrationType migrationType)
		{
			return new LocalizedString("ErrorNoEndpointSupportForMigrationType", "", false, false, Strings.ResourceManager, new object[]
			{
				migrationType
			});
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x0006D664 File Offset: 0x0006B864
		public static LocalizedString CSVFileTooLarge
		{
			get
			{
				return new LocalizedString("CSVFileTooLarge", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x0006D682 File Offset: 0x0006B882
		public static LocalizedString RemovedMigrationJobCannotBeModified
		{
			get
			{
				return new LocalizedString("RemovedMigrationJobCannotBeModified", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0006D6A0 File Offset: 0x0006B8A0
		public static LocalizedString MigrationRecipientNotFound(string mailboxName)
		{
			return new LocalizedString("MigrationRecipientNotFound", "", false, false, Strings.ResourceManager, new object[]
			{
				mailboxName
			});
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x0006D6D0 File Offset: 0x0006B8D0
		public static LocalizedString ErrorCannotRemoveUserWithoutBatch(string userName)
		{
			return new LocalizedString("ErrorCannotRemoveUserWithoutBatch", "", false, false, Strings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x0006D6FF File Offset: 0x0006B8FF
		public static LocalizedString LabelCompleted
		{
			get
			{
				return new LocalizedString("LabelCompleted", "Ex70D639", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x0006D720 File Offset: 0x0006B920
		public static LocalizedString ErrorCannotRemoveEndpointWithAssociatedBatches(string endpointId, string batches)
		{
			return new LocalizedString("ErrorCannotRemoveEndpointWithAssociatedBatches", "", false, false, Strings.ResourceManager, new object[]
			{
				endpointId,
				batches
			});
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x0006D753 File Offset: 0x0006B953
		public static LocalizedString MigrationPublicFolderWireUpFailed
		{
			get
			{
				return new LocalizedString("MigrationPublicFolderWireUpFailed", "ExB111AD", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0006D774 File Offset: 0x0006B974
		public static LocalizedString UserAlreadyMigrated(string alias)
		{
			return new LocalizedString("UserAlreadyMigrated", "Ex2F7769", false, true, Strings.ResourceManager, new object[]
			{
				alias
			});
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x0006D7A3 File Offset: 0x0006B9A3
		public static LocalizedString LabelCouldntMigrate
		{
			get
			{
				return new LocalizedString("LabelCouldntMigrate", "ExB6A237", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x0006D7C4 File Offset: 0x0006B9C4
		public static LocalizedString MigrationReportJobItemFailed(string user, LocalizedString message)
		{
			return new LocalizedString("MigrationReportJobItemFailed", "", false, false, Strings.ResourceManager, new object[]
			{
				user,
				message
			});
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x0006D7FC File Offset: 0x0006B9FC
		public static LocalizedString MigrationTenantPermissionFailure
		{
			get
			{
				return new LocalizedString("MigrationTenantPermissionFailure", "Ex5A26A0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x0006D81A File Offset: 0x0006BA1A
		public static LocalizedString UnknownMigrationBatchError
		{
			get
			{
				return new LocalizedString("UnknownMigrationBatchError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x0006D838 File Offset: 0x0006BA38
		public static LocalizedString MigrationReportJobComplete
		{
			get
			{
				return new LocalizedString("MigrationReportJobComplete", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0006D858 File Offset: 0x0006BA58
		public static LocalizedString MigrationUserMovedToAnotherBatch(string batchName)
		{
			return new LocalizedString("MigrationUserMovedToAnotherBatch", "", false, false, Strings.ResourceManager, new object[]
			{
				batchName
			});
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x0006D887 File Offset: 0x0006BA87
		public static LocalizedString UnableToDisableSubscription
		{
			get
			{
				return new LocalizedString("UnableToDisableSubscription", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x0006D8A8 File Offset: 0x0006BAA8
		public static LocalizedString MigrationReportJobAutomaticallyRestarting(int numOfFailures, int retryCount, int maxRetries)
		{
			return new LocalizedString("MigrationReportJobAutomaticallyRestarting", "", false, false, Strings.ResourceManager, new object[]
			{
				numOfFailures,
				retryCount,
				maxRetries
			});
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0006D8F0 File Offset: 0x0006BAF0
		public static LocalizedString ConfigKeyAccessRuntimeError(string keyname)
		{
			return new LocalizedString("ConfigKeyAccessRuntimeError", "", false, false, Strings.ResourceManager, new object[]
			{
				keyname
			});
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x0006D91F File Offset: 0x0006BB1F
		public static LocalizedString OnlyOneCutoverBatchIsAllowedError
		{
			get
			{
				return new LocalizedString("OnlyOneCutoverBatchIsAllowedError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x0006D93D File Offset: 0x0006BB3D
		public static LocalizedString CouldNotUpdateSubscriptionSettingsWithoutBatch
		{
			get
			{
				return new LocalizedString("CouldNotUpdateSubscriptionSettingsWithoutBatch", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0006D95C File Offset: 0x0006BB5C
		public static LocalizedString ErrorMigrationSlotCapacityExceeded(Unlimited<int> availableCapacity, int requestedCapacity)
		{
			return new LocalizedString("ErrorMigrationSlotCapacityExceeded", "", false, false, Strings.ResourceManager, new object[]
			{
				availableCapacity,
				requestedCapacity
			});
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x0006D999 File Offset: 0x0006BB99
		public static LocalizedString CorruptedMigrationBatchCannotBeStarted
		{
			get
			{
				return new LocalizedString("CorruptedMigrationBatchCannotBeStarted", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x0006D9B8 File Offset: 0x0006BBB8
		public static LocalizedString ErrorConnectionTimeout(string remoteHost, TimeSpan timeout)
		{
			return new LocalizedString("ErrorConnectionTimeout", "", false, false, Strings.ResourceManager, new object[]
			{
				remoteHost,
				timeout
			});
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0006D9F0 File Offset: 0x0006BBF0
		public static LocalizedString CouldNotDetermineExchangeRemoteSettings(string serverName)
		{
			return new LocalizedString("CouldNotDetermineExchangeRemoteSettings", "", false, false, Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0006DA20 File Offset: 0x0006BC20
		public static LocalizedString FinalizationErrorSummaryMessagePlural(int errorCount)
		{
			return new LocalizedString("FinalizationErrorSummaryMessagePlural", "Ex39E5BE", false, true, Strings.ResourceManager, new object[]
			{
				errorCount
			});
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x0006DA54 File Offset: 0x0006BC54
		public static LocalizedString ProvisioningThrottledBack
		{
			get
			{
				return new LocalizedString("ProvisioningThrottledBack", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x0600156C RID: 5484 RVA: 0x0006DA72 File Offset: 0x0006BC72
		public static LocalizedString CompletedMigrationJobCannotBeModified
		{
			get
			{
				return new LocalizedString("CompletedMigrationJobCannotBeModified", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x0006DA90 File Offset: 0x0006BC90
		public static LocalizedString CorruptedMigrationBatchCannotBeModified
		{
			get
			{
				return new LocalizedString("CorruptedMigrationBatchCannotBeModified", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x0006DAAE File Offset: 0x0006BCAE
		public static LocalizedString LabelTotalRows
		{
			get
			{
				return new LocalizedString("LabelTotalRows", "Ex808717", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x0006DACC File Offset: 0x0006BCCC
		public static LocalizedString LabelSynced
		{
			get
			{
				return new LocalizedString("LabelSynced", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x0006DAEA File Offset: 0x0006BCEA
		public static LocalizedString CouldNotDiscoverNSPISettings
		{
			get
			{
				return new LocalizedString("CouldNotDiscoverNSPISettings", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x0006DB08 File Offset: 0x0006BD08
		public static LocalizedString FeatureCannotBeDisabled(string feature)
		{
			return new LocalizedString("FeatureCannotBeDisabled", "", false, false, Strings.ResourceManager, new object[]
			{
				feature
			});
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x0006DB37 File Offset: 0x0006BD37
		public static LocalizedString SyncingMigrationJobCannotBeAppendedTo
		{
			get
			{
				return new LocalizedString("SyncingMigrationJobCannotBeAppendedTo", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x0006DB55 File Offset: 0x0006BD55
		public static LocalizedString MigrationExchangeCredentialFailure
		{
			get
			{
				return new LocalizedString("MigrationExchangeCredentialFailure", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x0006DB74 File Offset: 0x0006BD74
		public static LocalizedString ErrorMaximumConcurrentMigrationLimitExceeded(string endpointValue, string limitValue, string migrationType)
		{
			return new LocalizedString("ErrorMaximumConcurrentMigrationLimitExceeded", "", false, false, Strings.ResourceManager, new object[]
			{
				endpointValue,
				limitValue,
				migrationType
			});
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x0006DBAC File Offset: 0x0006BDAC
		public static LocalizedString UserDuplicateInOtherBatch(string alias, string batchName)
		{
			return new LocalizedString("UserDuplicateInOtherBatch", "", false, false, Strings.ResourceManager, new object[]
			{
				alias,
				batchName
			});
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x0006DBE0 File Offset: 0x0006BDE0
		public static LocalizedString MultipleMigrationJobItems(string email)
		{
			return new LocalizedString("MultipleMigrationJobItems", "", false, false, Strings.ResourceManager, new object[]
			{
				email
			});
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x0006DC0F File Offset: 0x0006BE0F
		public static LocalizedString InvalidVersionDetailedStatus
		{
			get
			{
				return new LocalizedString("InvalidVersionDetailedStatus", "ExBACE1A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0006DC30 File Offset: 0x0006BE30
		public static LocalizedString MigrationReportJobStarted(string userName)
		{
			return new LocalizedString("MigrationReportJobStarted", "", false, false, Strings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001579 RID: 5497 RVA: 0x0006DC5F File Offset: 0x0006BE5F
		public static LocalizedString MigrationReportJobAutoStarted
		{
			get
			{
				return new LocalizedString("MigrationReportJobAutoStarted", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x0006DC80 File Offset: 0x0006BE80
		public static LocalizedString MigrationReportJobRemoved(string username)
		{
			return new LocalizedString("MigrationReportJobRemoved", "", false, false, Strings.ResourceManager, new object[]
			{
				username
			});
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x0006DCAF File Offset: 0x0006BEAF
		public static LocalizedString RemovingMigrationUserBatchMustBeIdle
		{
			get
			{
				return new LocalizedString("RemovingMigrationUserBatchMustBeIdle", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0006DCD0 File Offset: 0x0006BED0
		public static LocalizedString BatchCompletionReportMailHeader(string fileName)
		{
			return new LocalizedString("BatchCompletionReportMailHeader", "ExC678DF", false, true, Strings.ResourceManager, new object[]
			{
				fileName
			});
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0006DD00 File Offset: 0x0006BF00
		public static LocalizedString MigrationObjectNotFoundInADError(string legDN, string server)
		{
			return new LocalizedString("MigrationObjectNotFoundInADError", "", false, false, Strings.ResourceManager, new object[]
			{
				legDN,
				server
			});
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x0006DD33 File Offset: 0x0006BF33
		public static LocalizedString NoDataMigrated
		{
			get
			{
				return new LocalizedString("NoDataMigrated", "Ex0AEE83", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0006DD54 File Offset: 0x0006BF54
		public static LocalizedString ErrorConnectionFailed(string remoteHost)
		{
			return new LocalizedString("ErrorConnectionFailed", "", false, false, Strings.ResourceManager, new object[]
			{
				remoteHost
			});
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x0006DD84 File Offset: 0x0006BF84
		public static LocalizedString UserDuplicateOrphanedFromBatch(string alias)
		{
			return new LocalizedString("UserDuplicateOrphanedFromBatch", "", false, false, Strings.ResourceManager, new object[]
			{
				alias
			});
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0006DDB4 File Offset: 0x0006BFB4
		public static LocalizedString PartialMigrationSummaryMessageSingular(int partialMigrationCount)
		{
			return new LocalizedString("PartialMigrationSummaryMessageSingular", "Ex3C7464", false, true, Strings.ResourceManager, new object[]
			{
				partialMigrationCount
			});
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0006DDE8 File Offset: 0x0006BFE8
		public static LocalizedString AutoDiscoverConfigurationError(LocalizedString details)
		{
			return new LocalizedString("AutoDiscoverConfigurationError", "", false, false, Strings.ResourceManager, new object[]
			{
				details
			});
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0006DE1C File Offset: 0x0006C01C
		public static LocalizedString ErrorCouldNotCreateCredentials(string username)
		{
			return new LocalizedString("ErrorCouldNotCreateCredentials", "", false, false, Strings.ResourceManager, new object[]
			{
				username
			});
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x0006DE4C File Offset: 0x0006C04C
		public static LocalizedString ErrorUnknownConnectionSettingsType(string root)
		{
			return new LocalizedString("ErrorUnknownConnectionSettingsType", "", false, false, Strings.ResourceManager, new object[]
			{
				root
			});
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x0006DE7B File Offset: 0x0006C07B
		public static LocalizedString SubscriptionDisabledSinceFinalized
		{
			get
			{
				return new LocalizedString("SubscriptionDisabledSinceFinalized", "ExB1EBC1", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x0006DE9C File Offset: 0x0006C09C
		public static LocalizedString ErrorAuthenticationMethodNotSupported(string authenticationMethod, string protocol, string validValues)
		{
			return new LocalizedString("ErrorAuthenticationMethodNotSupported", "", false, false, Strings.ResourceManager, new object[]
			{
				authenticationMethod,
				protocol,
				validValues
			});
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x0006DED3 File Offset: 0x0006C0D3
		public static LocalizedString AuthenticationErrorStatus
		{
			get
			{
				return new LocalizedString("AuthenticationErrorStatus", "Ex2DE54F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x0006DEF4 File Offset: 0x0006C0F4
		public static LocalizedString RecipientInfoInvalidAtSource(string email)
		{
			return new LocalizedString("RecipientInfoInvalidAtSource", "", false, false, Strings.ResourceManager, new object[]
			{
				email
			});
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x0006DF24 File Offset: 0x0006C124
		public static LocalizedString ErrorMissingExchangeGuid(string identity)
		{
			return new LocalizedString("ErrorMissingExchangeGuid", "", false, false, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x0006DF54 File Offset: 0x0006C154
		public static LocalizedString ErrorCouldNotEncryptPassword(string username)
		{
			return new LocalizedString("ErrorCouldNotEncryptPassword", "", false, false, Strings.ResourceManager, new object[]
			{
				username
			});
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x0006DF83 File Offset: 0x0006C183
		public static LocalizedString MigrationReportJobTransientError
		{
			get
			{
				return new LocalizedString("MigrationReportJobTransientError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x0006DFA4 File Offset: 0x0006C1A4
		public static LocalizedString ErrorUnsupportedRecipientTypeForProtocol(string type, string protocol)
		{
			return new LocalizedString("ErrorUnsupportedRecipientTypeForProtocol", "", false, false, Strings.ResourceManager, new object[]
			{
				type,
				protocol
			});
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x0006DFD8 File Offset: 0x0006C1D8
		public static LocalizedString ErrorUnexpectedMigrationType(string discoveredType, string expectedType)
		{
			return new LocalizedString("ErrorUnexpectedMigrationType", "", false, false, Strings.ResourceManager, new object[]
			{
				discoveredType,
				expectedType
			});
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x0006E00B File Offset: 0x0006C20B
		public static LocalizedString MigrationJobAlreadyHasPendingCSV
		{
			get
			{
				return new LocalizedString("MigrationJobAlreadyHasPendingCSV", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x0006E029 File Offset: 0x0006C229
		public static LocalizedString ErrorConnectionSettingsNotSerialized
		{
			get
			{
				return new LocalizedString("ErrorConnectionSettingsNotSerialized", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x0006E048 File Offset: 0x0006C248
		public static LocalizedString BatchCancelledBySystem(string fileName)
		{
			return new LocalizedString("BatchCancelledBySystem", "Ex2B0B04", false, true, Strings.ResourceManager, new object[]
			{
				fileName
			});
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x0006E077 File Offset: 0x0006C277
		public static LocalizedString PasswordPreviouslySet
		{
			get
			{
				return new LocalizedString("PasswordPreviouslySet", "Ex849B35", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x0006E095 File Offset: 0x0006C295
		public static LocalizedString MigrationJobAlreadyQueued
		{
			get
			{
				return new LocalizedString("MigrationJobAlreadyQueued", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001593 RID: 5523 RVA: 0x0006E0B3 File Offset: 0x0006C2B3
		public static LocalizedString PoisonDetailedStatus
		{
			get
			{
				return new LocalizedString("PoisonDetailedStatus", "Ex0BEB90", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001594 RID: 5524 RVA: 0x0006E0D1 File Offset: 0x0006C2D1
		public static LocalizedString NoPartialMigrationSummaryMessage
		{
			get
			{
				return new LocalizedString("NoPartialMigrationSummaryMessage", "Ex22EA1D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001595 RID: 5525 RVA: 0x0006E0EF File Offset: 0x0006C2EF
		public static LocalizedString StoppingMigrationJobCannotBeStarted
		{
			get
			{
				return new LocalizedString("StoppingMigrationJobCannotBeStarted", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x0006E110 File Offset: 0x0006C310
		public static LocalizedString MigrationBatchCancelledByUser(string batchName)
		{
			return new LocalizedString("MigrationBatchCancelledByUser", "Ex56E109", false, true, Strings.ResourceManager, new object[]
			{
				batchName
			});
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x0006E140 File Offset: 0x0006C340
		public static LocalizedString RpcRequestFailed(string requestType, string serverName)
		{
			return new LocalizedString("RpcRequestFailed", "", false, false, Strings.ResourceManager, new object[]
			{
				requestType,
				serverName
			});
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x0006E174 File Offset: 0x0006C374
		public static LocalizedString ErrorMigrationJobNotFound(Guid identity)
		{
			return new LocalizedString("ErrorMigrationJobNotFound", "", false, false, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001599 RID: 5529 RVA: 0x0006E1A8 File Offset: 0x0006C3A8
		public static LocalizedString FailedToReadPublicFoldersOnPremise
		{
			get
			{
				return new LocalizedString("FailedToReadPublicFoldersOnPremise", "Ex88CD84", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x0006E1C8 File Offset: 0x0006C3C8
		public static LocalizedString MigrationFinalizationReportMailHeader(string batchName)
		{
			return new LocalizedString("MigrationFinalizationReportMailHeader", "Ex6EAFCA", false, true, Strings.ResourceManager, new object[]
			{
				batchName
			});
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x0006E1F7 File Offset: 0x0006C3F7
		public static LocalizedString MailboxNotFoundSubscriptionStatus
		{
			get
			{
				return new LocalizedString("MailboxNotFoundSubscriptionStatus", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x0600159C RID: 5532 RVA: 0x0006E215 File Offset: 0x0006C415
		public static LocalizedString ReadPublicFoldersOnTargetInternalError
		{
			get
			{
				return new LocalizedString("ReadPublicFoldersOnTargetInternalError", "Ex1CE50E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x0006E233 File Offset: 0x0006C433
		public static LocalizedString ExternallySuspendedFailure
		{
			get
			{
				return new LocalizedString("ExternallySuspendedFailure", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x0006E251 File Offset: 0x0006C451
		public static LocalizedString SyncStateSizeError
		{
			get
			{
				return new LocalizedString("SyncStateSizeError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x0006E270 File Offset: 0x0006C470
		public static LocalizedString MigrationReportJobCompletedByUser(string userName)
		{
			return new LocalizedString("MigrationReportJobCompletedByUser", "", false, false, Strings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x0006E2A0 File Offset: 0x0006C4A0
		public static LocalizedString MoacWarningMessage(string url)
		{
			return new LocalizedString("MoacWarningMessage", "Ex62298B", false, true, Strings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x0006E2CF File Offset: 0x0006C4CF
		public static LocalizedString LabelLogMailFooter
		{
			get
			{
				return new LocalizedString("LabelLogMailFooter", "Ex32026C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x0006E2F0 File Offset: 0x0006C4F0
		public static LocalizedString MissingRootFolderMappingInCSVError(string hierarchyMailboxName)
		{
			return new LocalizedString("MissingRootFolderMappingInCSVError", "", false, false, Strings.ResourceManager, new object[]
			{
				hierarchyMailboxName
			});
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x060015A3 RID: 5539 RVA: 0x0006E31F File Offset: 0x0006C51F
		public static LocalizedString CorruptedSubscriptionStatus
		{
			get
			{
				return new LocalizedString("CorruptedSubscriptionStatus", "Ex6F410E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x0006E340 File Offset: 0x0006C540
		public static LocalizedString SourceEmailAddressNotUnique(string smtpAddress)
		{
			return new LocalizedString("SourceEmailAddressNotUnique", "", false, false, Strings.ResourceManager, new object[]
			{
				smtpAddress
			});
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x0006E36F File Offset: 0x0006C56F
		public static LocalizedString SignInMightBeRequired
		{
			get
			{
				return new LocalizedString("SignInMightBeRequired", "Ex29EFE6", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x0006E38D File Offset: 0x0006C58D
		public static LocalizedString MigrationTempMissingMigrationMailbox
		{
			get
			{
				return new LocalizedString("MigrationTempMissingMigrationMailbox", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x0006E3AB File Offset: 0x0006C5AB
		public static LocalizedString MigrationJobCannotBeCompleted
		{
			get
			{
				return new LocalizedString("MigrationJobCannotBeCompleted", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x0006E3C9 File Offset: 0x0006C5C9
		public static LocalizedString UnknownMigrationError
		{
			get
			{
				return new LocalizedString("UnknownMigrationError", "Ex4B5361", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x0006E3E8 File Offset: 0x0006C5E8
		public static LocalizedString InvalidValueProvidedForColumn(string columnName, string value)
		{
			return new LocalizedString("InvalidValueProvidedForColumn", "ExE97005", false, true, Strings.ResourceManager, new object[]
			{
				columnName,
				value
			});
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x0006E41C File Offset: 0x0006C61C
		public static LocalizedString SyncTimeOutFailure(string timespan)
		{
			return new LocalizedString("SyncTimeOutFailure", "Ex529E25", false, true, Strings.ResourceManager, new object[]
			{
				timespan
			});
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x0006E44B File Offset: 0x0006C64B
		public static LocalizedString CompleteMigrationBatchNotSupported
		{
			get
			{
				return new LocalizedString("CompleteMigrationBatchNotSupported", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x0006E469 File Offset: 0x0006C669
		public static LocalizedString ConfigAccessRuntimeError
		{
			get
			{
				return new LocalizedString("ConfigAccessRuntimeError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x0006E488 File Offset: 0x0006C688
		public static LocalizedString BatchCompletionReportMailErrorHeader(string fileName)
		{
			return new LocalizedString("BatchCompletionReportMailErrorHeader", "ExCFE22C", false, true, Strings.ResourceManager, new object[]
			{
				fileName
			});
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x0006E4B8 File Offset: 0x0006C6B8
		public static LocalizedString DisplayNameFormat(string firstname, string lastname)
		{
			return new LocalizedString("DisplayNameFormat", "Ex520F15", false, true, Strings.ResourceManager, new object[]
			{
				firstname,
				lastname
			});
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x0006E4EC File Offset: 0x0006C6EC
		public static LocalizedString UnsupportedMigrationTypeError(MigrationType type)
		{
			return new LocalizedString("UnsupportedMigrationTypeError", "", false, false, Strings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x0006E520 File Offset: 0x0006C720
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x040009FA RID: 2554
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(103);

		// Token: 0x040009FB RID: 2555
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Data.Migration.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200013F RID: 319
		public enum IDs : uint
		{
			// Token: 0x040009FD RID: 2557
			RemoteMailboxQuotaWarningStatus = 86638814U,
			// Token: 0x040009FE RID: 2558
			CompletingMigrationJobCannotBeAppendedTo = 1530080577U,
			// Token: 0x040009FF RID: 2559
			MigrationReportJobInitialSyncComplete = 2718293401U,
			// Token: 0x04000A00 RID: 2560
			LabelRunTime = 2060675488U,
			// Token: 0x04000A01 RID: 2561
			CorruptedMigrationBatchCannotBeStopped = 149572871U,
			// Token: 0x04000A02 RID: 2562
			MigrationExchangeRpcConnectionFailure = 1676389812U,
			// Token: 0x04000A03 RID: 2563
			MigrationJobCannotBeRemoved = 1899202903U,
			// Token: 0x04000A04 RID: 2564
			CannotSpecifyUnicodeInCredentials = 3099209002U,
			// Token: 0x04000A05 RID: 2565
			MigrationCancelledByUserRequest = 1798653784U,
			// Token: 0x04000A06 RID: 2566
			FailureDuringRemoval = 4872333U,
			// Token: 0x04000A07 RID: 2567
			FinalizationErrorSummaryRetryMessage = 223688197U,
			// Token: 0x04000A08 RID: 2568
			MigrationJobAlreadyStopping = 3674744977U,
			// Token: 0x04000A09 RID: 2569
			MigrationJobCannotBeStopped = 2530769896U,
			// Token: 0x04000A0A RID: 2570
			SubscriptionNotFound = 1168096436U,
			// Token: 0x04000A0B RID: 2571
			ContactsMigration = 3851331319U,
			// Token: 0x04000A0C RID: 2572
			EmailMigration = 4089501710U,
			// Token: 0x04000A0D RID: 2573
			UserProvisioningInternalError = 3003515263U,
			// Token: 0x04000A0E RID: 2574
			CompletedMigrationJobCannotBeStartedMultiBatch = 1509794908U,
			// Token: 0x04000A0F RID: 2575
			LeaveOnServerNotSupportedStatus = 1898862428U,
			// Token: 0x04000A10 RID: 2576
			MigrationLocalDatabasesNotFound = 2871031082U,
			// Token: 0x04000A11 RID: 2577
			OnlyOnePublicFolderBatchIsAllowedError = 829120357U,
			// Token: 0x04000A12 RID: 2578
			LabelFileName = 1144436407U,
			// Token: 0x04000A13 RID: 2579
			MigrationTempMissingDatabase = 3073036977U,
			// Token: 0x04000A14 RID: 2580
			MigrationDataCorruptionError = 1781379657U,
			// Token: 0x04000A15 RID: 2581
			CutoverAndStagedBatchesCannotCoexistError = 1300976335U,
			// Token: 0x04000A16 RID: 2582
			MaximumNumberOfBatchesReached = 2350676710U,
			// Token: 0x04000A17 RID: 2583
			PublicFolderMailboxesNotProvisionedError = 4040662282U,
			// Token: 0x04000A18 RID: 2584
			CannotSpecifyUnicodeUserIdPasswordWithBasicAuth = 2999872780U,
			// Token: 0x04000A19 RID: 2585
			CompletingMigrationJobCannotBeStarted = 853242704U,
			// Token: 0x04000A1A RID: 2586
			MigrationJobAlreadyStarted = 514394488U,
			// Token: 0x04000A1B RID: 2587
			LabelTotalMailboxes = 850869622U,
			// Token: 0x04000A1C RID: 2588
			MigrationJobAlreadyStopped = 2694765716U,
			// Token: 0x04000A1D RID: 2589
			MigrationGenericError = 391393381U,
			// Token: 0x04000A1E RID: 2590
			CommunicationErrorStatus = 3393024142U,
			// Token: 0x04000A1F RID: 2591
			ErrorReportLink = 3351583406U,
			// Token: 0x04000A20 RID: 2592
			LabelStartDateTime = 2548932531U,
			// Token: 0x04000A21 RID: 2593
			MigrationJobDoesNotSupportAppendingUserCSV = 1956114709U,
			// Token: 0x04000A22 RID: 2594
			SubscriptionRpcThresholdExceeded = 934722052U,
			// Token: 0x04000A23 RID: 2595
			CorruptedMigrationBatchCannotBeCompleted = 2142750819U,
			// Token: 0x04000A24 RID: 2596
			RemovedMigrationJobCannotBeStarted = 437021588U,
			// Token: 0x04000A25 RID: 2597
			ErrorCouldNotDeserializeConnectionSettings = 2530286416U,
			// Token: 0x04000A26 RID: 2598
			UnknownTimespan = 1754366117U,
			// Token: 0x04000A27 RID: 2599
			LabsMailboxQuotaWarningStatus = 1419436392U,
			// Token: 0x04000A28 RID: 2600
			ConnectionErrorStatus = 2571873410U,
			// Token: 0x04000A29 RID: 2601
			StatisticsReportLink = 1951127083U,
			// Token: 0x04000A2A RID: 2602
			RemoteServerIsSlow = 571725646U,
			// Token: 0x04000A2B RID: 2603
			ErrorMigrationMailboxMissingOrInvalid = 672478732U,
			// Token: 0x04000A2C RID: 2604
			CompletedMigrationJobCannotBeStarted = 1978952169U,
			// Token: 0x04000A2D RID: 2605
			MigrationReportFailed = 2036241881U,
			// Token: 0x04000A2E RID: 2606
			MigrationCancelledDueToInternalError = 1271432035U,
			// Token: 0x04000A2F RID: 2607
			PublicFolderMigrationBatchCannotBeCompletedWithErrors = 1786933629U,
			// Token: 0x04000A30 RID: 2608
			MigrationJobCannotRetryCompletion = 85986654U,
			// Token: 0x04000A31 RID: 2609
			MissingRequiredSubscriptionId = 3662584967U,
			// Token: 0x04000A32 RID: 2610
			IMAPPathPrefixInvalidStatus = 1905833821U,
			// Token: 0x04000A33 RID: 2611
			TooManyFoldersStatus = 601262870U,
			// Token: 0x04000A34 RID: 2612
			LabelSubmittedByUser = 1066087601U,
			// Token: 0x04000A35 RID: 2613
			CSVFileTooLarge = 1842619565U,
			// Token: 0x04000A36 RID: 2614
			RemovedMigrationJobCannotBeModified = 1189164420U,
			// Token: 0x04000A37 RID: 2615
			LabelCompleted = 4257873367U,
			// Token: 0x04000A38 RID: 2616
			MigrationPublicFolderWireUpFailed = 1047974326U,
			// Token: 0x04000A39 RID: 2617
			LabelCouldntMigrate = 1386196812U,
			// Token: 0x04000A3A RID: 2618
			MigrationTenantPermissionFailure = 3955323767U,
			// Token: 0x04000A3B RID: 2619
			UnknownMigrationBatchError = 1043780694U,
			// Token: 0x04000A3C RID: 2620
			MigrationReportJobComplete = 1332982264U,
			// Token: 0x04000A3D RID: 2621
			UnableToDisableSubscription = 367906109U,
			// Token: 0x04000A3E RID: 2622
			OnlyOneCutoverBatchIsAllowedError = 1279919936U,
			// Token: 0x04000A3F RID: 2623
			CouldNotUpdateSubscriptionSettingsWithoutBatch = 2536192239U,
			// Token: 0x04000A40 RID: 2624
			CorruptedMigrationBatchCannotBeStarted = 3327693399U,
			// Token: 0x04000A41 RID: 2625
			ProvisioningThrottledBack = 617690654U,
			// Token: 0x04000A42 RID: 2626
			CompletedMigrationJobCannotBeModified = 4177113737U,
			// Token: 0x04000A43 RID: 2627
			CorruptedMigrationBatchCannotBeModified = 3751987975U,
			// Token: 0x04000A44 RID: 2628
			LabelTotalRows = 5503809U,
			// Token: 0x04000A45 RID: 2629
			LabelSynced = 1136304310U,
			// Token: 0x04000A46 RID: 2630
			CouldNotDiscoverNSPISettings = 1088630310U,
			// Token: 0x04000A47 RID: 2631
			SyncingMigrationJobCannotBeAppendedTo = 16533286U,
			// Token: 0x04000A48 RID: 2632
			MigrationExchangeCredentialFailure = 4038100652U,
			// Token: 0x04000A49 RID: 2633
			InvalidVersionDetailedStatus = 1756157035U,
			// Token: 0x04000A4A RID: 2634
			MigrationReportJobAutoStarted = 2104158819U,
			// Token: 0x04000A4B RID: 2635
			RemovingMigrationUserBatchMustBeIdle = 320046086U,
			// Token: 0x04000A4C RID: 2636
			NoDataMigrated = 759640474U,
			// Token: 0x04000A4D RID: 2637
			SubscriptionDisabledSinceFinalized = 2605498839U,
			// Token: 0x04000A4E RID: 2638
			AuthenticationErrorStatus = 2047379006U,
			// Token: 0x04000A4F RID: 2639
			MigrationReportJobTransientError = 986156097U,
			// Token: 0x04000A50 RID: 2640
			MigrationJobAlreadyHasPendingCSV = 1214466260U,
			// Token: 0x04000A51 RID: 2641
			ErrorConnectionSettingsNotSerialized = 3504597832U,
			// Token: 0x04000A52 RID: 2642
			PasswordPreviouslySet = 1236042521U,
			// Token: 0x04000A53 RID: 2643
			MigrationJobAlreadyQueued = 3020735710U,
			// Token: 0x04000A54 RID: 2644
			PoisonDetailedStatus = 2529513648U,
			// Token: 0x04000A55 RID: 2645
			NoPartialMigrationSummaryMessage = 1500792389U,
			// Token: 0x04000A56 RID: 2646
			StoppingMigrationJobCannotBeStarted = 3968870632U,
			// Token: 0x04000A57 RID: 2647
			FailedToReadPublicFoldersOnPremise = 1142848800U,
			// Token: 0x04000A58 RID: 2648
			MailboxNotFoundSubscriptionStatus = 1731044986U,
			// Token: 0x04000A59 RID: 2649
			ReadPublicFoldersOnTargetInternalError = 1197149433U,
			// Token: 0x04000A5A RID: 2650
			ExternallySuspendedFailure = 3334133771U,
			// Token: 0x04000A5B RID: 2651
			SyncStateSizeError = 4060495101U,
			// Token: 0x04000A5C RID: 2652
			LabelLogMailFooter = 1246705638U,
			// Token: 0x04000A5D RID: 2653
			CorruptedSubscriptionStatus = 4122602291U,
			// Token: 0x04000A5E RID: 2654
			SignInMightBeRequired = 295927301U,
			// Token: 0x04000A5F RID: 2655
			MigrationTempMissingMigrationMailbox = 4162391104U,
			// Token: 0x04000A60 RID: 2656
			MigrationJobCannotBeCompleted = 501292080U,
			// Token: 0x04000A61 RID: 2657
			UnknownMigrationError = 2595351182U,
			// Token: 0x04000A62 RID: 2658
			CompleteMigrationBatchNotSupported = 1980396424U,
			// Token: 0x04000A63 RID: 2659
			ConfigAccessRuntimeError = 2353197456U
		}

		// Token: 0x02000140 RID: 320
		private enum ParamIDs
		{
			// Token: 0x04000A65 RID: 2661
			MigrationBatchCompletionReportMailErrorHeader,
			// Token: 0x04000A66 RID: 2662
			MigrationReportJobItemCorrupted,
			// Token: 0x04000A67 RID: 2663
			UnsupportedSourceRecipientTypeError,
			// Token: 0x04000A68 RID: 2664
			MigrationJobCannotBeDeletedWithPendingItems,
			// Token: 0x04000A69 RID: 2665
			UnsupportedAdminCulture,
			// Token: 0x04000A6A RID: 2666
			MigrationProcessorInvalidation,
			// Token: 0x04000A6B RID: 2667
			MigrationJobItemNotFound,
			// Token: 0x04000A6C RID: 2668
			MigrationFeatureNotSupported,
			// Token: 0x04000A6D RID: 2669
			MigrationJobNotFound,
			// Token: 0x04000A6E RID: 2670
			ErrorNspiNotSupportedForEndpointType,
			// Token: 0x04000A6F RID: 2671
			UserDuplicateInCSV,
			// Token: 0x04000A70 RID: 2672
			MigrationOrganizationNotFound,
			// Token: 0x04000A71 RID: 2673
			MigrationReportRepaired,
			// Token: 0x04000A72 RID: 2674
			LabelAutoRetry,
			// Token: 0x04000A73 RID: 2675
			CouldNotLoadMigrationPersistedItem,
			// Token: 0x04000A74 RID: 2676
			MigrationReportJobItemRetried,
			// Token: 0x04000A75 RID: 2677
			MigrationItemLastUpdatedInTheFuture,
			// Token: 0x04000A76 RID: 2678
			FailedToUpdateRecipientSource,
			// Token: 0x04000A77 RID: 2679
			MigrationReportJobCreated,
			// Token: 0x04000A78 RID: 2680
			MigrationSubscriptionCreationFailed,
			// Token: 0x04000A79 RID: 2681
			MigrationReportNspiFailed,
			// Token: 0x04000A7A RID: 2682
			UnexpectedSubscriptionStatus,
			// Token: 0x04000A7B RID: 2683
			MigrationRpcFailed,
			// Token: 0x04000A7C RID: 2684
			MigrationBatchCompletionReportMailHeader,
			// Token: 0x04000A7D RID: 2685
			MigrationReportNspiRfrFailed,
			// Token: 0x04000A7E RID: 2686
			MigrationVersionMismatch,
			// Token: 0x04000A7F RID: 2687
			MigrationBatchCancelledBySystem,
			// Token: 0x04000A80 RID: 2688
			BatchCancelledByUser,
			// Token: 0x04000A81 RID: 2689
			InvalidRootFolderMappingInCSVError,
			// Token: 0x04000A82 RID: 2690
			DuplicateFolderInCSVError,
			// Token: 0x04000A83 RID: 2691
			ErrorTooManyTransientFailures,
			// Token: 0x04000A84 RID: 2692
			RunTimeFormatHours,
			// Token: 0x04000A85 RID: 2693
			MigrationReportJobItemRemovedInternal,
			// Token: 0x04000A86 RID: 2694
			RecipientDoesNotExistAtSource,
			// Token: 0x04000A87 RID: 2695
			MigrationReportJobItemCreatedInternal,
			// Token: 0x04000A88 RID: 2696
			ErrorSummaryMessageSingular,
			// Token: 0x04000A89 RID: 2697
			MigrationReportJobItemWithError,
			// Token: 0x04000A8A RID: 2698
			ErrorUserAlreadyBeingMigrated,
			// Token: 0x04000A8B RID: 2699
			MigrationBatchReportMailHeader,
			// Token: 0x04000A8C RID: 2700
			DetailedAggregationStatus,
			// Token: 0x04000A8D RID: 2701
			UnsupportedTargetRecipientTypeError,
			// Token: 0x04000A8E RID: 2702
			ErrorUserMissingOrWithoutRBAC,
			// Token: 0x04000A8F RID: 2703
			PartialMigrationSummaryMessagePlural,
			// Token: 0x04000A90 RID: 2704
			MigrationReportJobItemRemoved,
			// Token: 0x04000A91 RID: 2705
			CannotUpgradeMigrationVersion,
			// Token: 0x04000A92 RID: 2706
			AutoDiscoverInternalError,
			// Token: 0x04000A93 RID: 2707
			MigrationUserAlreadyExistsInDifferentType,
			// Token: 0x04000A94 RID: 2708
			MigrationReportJobModifiedByUser,
			// Token: 0x04000A95 RID: 2709
			ErrorInvalidRecipientType,
			// Token: 0x04000A96 RID: 2710
			MigrationUserAlreadyRemoved,
			// Token: 0x04000A97 RID: 2711
			FinalizationErrorSummaryMessageSingular,
			// Token: 0x04000A98 RID: 2712
			ErrorParsingCSV,
			// Token: 0x04000A99 RID: 2713
			MigrationConfigString,
			// Token: 0x04000A9A RID: 2714
			ErrorSummaryMessagePlural,
			// Token: 0x04000A9B RID: 2715
			ValueNotProvidedForColumn,
			// Token: 0x04000A9C RID: 2716
			UserAlreadyMigratedWithAlternateEmail,
			// Token: 0x04000A9D RID: 2717
			MigrationFinalizationReportMailErrorHeader,
			// Token: 0x04000A9E RID: 2718
			ErrorAutoDiscoveryNotSupported,
			// Token: 0x04000A9F RID: 2719
			RunTimeFormatMinutes,
			// Token: 0x04000AA0 RID: 2720
			RunTimeFormatDays,
			// Token: 0x04000AA1 RID: 2721
			ErrorMissingExpectedCapability,
			// Token: 0x04000AA2 RID: 2722
			MigrationReportSetString,
			// Token: 0x04000AA3 RID: 2723
			MigrationEndpointAlreadyExistsError,
			// Token: 0x04000AA4 RID: 2724
			MigrationBatchReportMailErrorHeader,
			// Token: 0x04000AA5 RID: 2725
			CouldNotAddExchangeSnapIn,
			// Token: 0x04000AA6 RID: 2726
			ErrorNoEndpointSupportForMigrationType,
			// Token: 0x04000AA7 RID: 2727
			MigrationRecipientNotFound,
			// Token: 0x04000AA8 RID: 2728
			ErrorCannotRemoveUserWithoutBatch,
			// Token: 0x04000AA9 RID: 2729
			ErrorCannotRemoveEndpointWithAssociatedBatches,
			// Token: 0x04000AAA RID: 2730
			UserAlreadyMigrated,
			// Token: 0x04000AAB RID: 2731
			MigrationReportJobItemFailed,
			// Token: 0x04000AAC RID: 2732
			MigrationUserMovedToAnotherBatch,
			// Token: 0x04000AAD RID: 2733
			MigrationReportJobAutomaticallyRestarting,
			// Token: 0x04000AAE RID: 2734
			ConfigKeyAccessRuntimeError,
			// Token: 0x04000AAF RID: 2735
			ErrorMigrationSlotCapacityExceeded,
			// Token: 0x04000AB0 RID: 2736
			ErrorConnectionTimeout,
			// Token: 0x04000AB1 RID: 2737
			CouldNotDetermineExchangeRemoteSettings,
			// Token: 0x04000AB2 RID: 2738
			FinalizationErrorSummaryMessagePlural,
			// Token: 0x04000AB3 RID: 2739
			FeatureCannotBeDisabled,
			// Token: 0x04000AB4 RID: 2740
			ErrorMaximumConcurrentMigrationLimitExceeded,
			// Token: 0x04000AB5 RID: 2741
			UserDuplicateInOtherBatch,
			// Token: 0x04000AB6 RID: 2742
			MultipleMigrationJobItems,
			// Token: 0x04000AB7 RID: 2743
			MigrationReportJobStarted,
			// Token: 0x04000AB8 RID: 2744
			MigrationReportJobRemoved,
			// Token: 0x04000AB9 RID: 2745
			BatchCompletionReportMailHeader,
			// Token: 0x04000ABA RID: 2746
			MigrationObjectNotFoundInADError,
			// Token: 0x04000ABB RID: 2747
			ErrorConnectionFailed,
			// Token: 0x04000ABC RID: 2748
			UserDuplicateOrphanedFromBatch,
			// Token: 0x04000ABD RID: 2749
			PartialMigrationSummaryMessageSingular,
			// Token: 0x04000ABE RID: 2750
			AutoDiscoverConfigurationError,
			// Token: 0x04000ABF RID: 2751
			ErrorCouldNotCreateCredentials,
			// Token: 0x04000AC0 RID: 2752
			ErrorUnknownConnectionSettingsType,
			// Token: 0x04000AC1 RID: 2753
			ErrorAuthenticationMethodNotSupported,
			// Token: 0x04000AC2 RID: 2754
			RecipientInfoInvalidAtSource,
			// Token: 0x04000AC3 RID: 2755
			ErrorMissingExchangeGuid,
			// Token: 0x04000AC4 RID: 2756
			ErrorCouldNotEncryptPassword,
			// Token: 0x04000AC5 RID: 2757
			ErrorUnsupportedRecipientTypeForProtocol,
			// Token: 0x04000AC6 RID: 2758
			ErrorUnexpectedMigrationType,
			// Token: 0x04000AC7 RID: 2759
			BatchCancelledBySystem,
			// Token: 0x04000AC8 RID: 2760
			MigrationBatchCancelledByUser,
			// Token: 0x04000AC9 RID: 2761
			RpcRequestFailed,
			// Token: 0x04000ACA RID: 2762
			ErrorMigrationJobNotFound,
			// Token: 0x04000ACB RID: 2763
			MigrationFinalizationReportMailHeader,
			// Token: 0x04000ACC RID: 2764
			MigrationReportJobCompletedByUser,
			// Token: 0x04000ACD RID: 2765
			MoacWarningMessage,
			// Token: 0x04000ACE RID: 2766
			MissingRootFolderMappingInCSVError,
			// Token: 0x04000ACF RID: 2767
			SourceEmailAddressNotUnique,
			// Token: 0x04000AD0 RID: 2768
			InvalidValueProvidedForColumn,
			// Token: 0x04000AD1 RID: 2769
			SyncTimeOutFailure,
			// Token: 0x04000AD2 RID: 2770
			BatchCompletionReportMailErrorHeader,
			// Token: 0x04000AD3 RID: 2771
			DisplayNameFormat,
			// Token: 0x04000AD4 RID: 2772
			UnsupportedMigrationTypeError
		}
	}
}
