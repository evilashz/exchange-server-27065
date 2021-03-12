using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.DeltaSync;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Imap;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pop;

namespace Microsoft.Exchange.Management.Mobility
{
	// Token: 0x0200005A RID: 90
	internal static class Strings
	{
		// Token: 0x060003EB RID: 1003 RVA: 0x0000F4C0 File Offset: 0x0000D6C0
		static Strings()
		{
			Strings.stringIDs.Add(1372907051U, "InsecureConfirmation");
			Strings.stringIDs.Add(1199310315U, "SubscriptionCannotBeChanged");
			Strings.stringIDs.Add(456403923U, "AutoProvisionComplete");
			Strings.stringIDs.Add(4170829984U, "SetLinkedInSubscriptionConfirmation");
			Strings.stringIDs.Add(843454792U, "FacebookCommunicationError");
			Strings.stringIDs.Add(433732448U, "AutoProvisionNoProtocols");
			Strings.stringIDs.Add(1081354883U, "WarningExtensionFeatureDisabled");
			Strings.stringIDs.Add(2674661771U, "FacebookAuthorizationError");
			Strings.stringIDs.Add(3132041271U, "AutoProvisionConnectivity");
			Strings.stringIDs.Add(3962568818U, "ErrorMissingFile");
			Strings.stringIDs.Add(1935884132U, "CreateLinkedInSubscriptionConfirmation");
			Strings.stringIDs.Add(232075358U, "SubscriptionCannotBeEnabled");
			Strings.stringIDs.Add(2121885544U, "ErrorServerCertificateError");
			Strings.stringIDs.Add(3868201023U, "ConfirmationMessageInstallOwaOrgExtension");
			Strings.stringIDs.Add(3256811515U, "ErrorCanNotDownloadPackage");
			Strings.stringIDs.Add(562700869U, "AutoProvisionQueryDNS");
			Strings.stringIDs.Add(424238761U, "IMAPAccountVerificationFailedException");
			Strings.stringIDs.Add(4012336426U, "FacebookEmptyAccessToken");
			Strings.stringIDs.Add(2684496906U, "NullSubscriptionStoreId");
			Strings.stringIDs.Add(966086681U, "ErrorCannotSpecifyParameterWithoutOrgExtensionParameter");
			Strings.stringIDs.Add(408801391U, "FacebookTimeoutError");
			Strings.stringIDs.Add(1307888814U, "CreateFacebookSubscriptionConfirmation");
			Strings.stringIDs.Add(1548922274U, "AutoProvisionResults");
			Strings.stringIDs.Add(3353546040U, "ErrorNoInputForExtensionInstall");
			Strings.stringIDs.Add(2973592384U, "AutoProvisionCreate");
			Strings.stringIDs.Add(4263159413U, "ErrorReasonUserNotAllowedToInstallReadWriteMailbox");
			Strings.stringIDs.Add(2907685502U, "SetFacebookSubscriptionConfirmation");
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000F718 File Offset: 0x0000D918
		public static LocalizedString ErrorTooManyUsersInUserList(int maxCount)
		{
			return new LocalizedString("ErrorTooManyUsersInUserList", Strings.ResourceManager, new object[]
			{
				maxCount
			});
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000F748 File Offset: 0x0000D948
		public static LocalizedString CreateHotmailSubscriptionConfirmation(HotmailSubscriptionProxy subscription)
		{
			return new LocalizedString("CreateHotmailSubscriptionConfirmation", Strings.ResourceManager, new object[]
			{
				subscription
			});
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000F770 File Offset: 0x0000D970
		public static LocalizedString PushNotificationAppPlatformMismatch(string appName, string originalApp)
		{
			return new LocalizedString("PushNotificationAppPlatformMismatch", Strings.ResourceManager, new object[]
			{
				appName,
				originalApp
			});
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000F79C File Offset: 0x0000D99C
		public static LocalizedString ConfirmationMessageNewApp(string id)
		{
			return new LocalizedString("ConfirmationMessageNewApp", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000F7C4 File Offset: 0x0000D9C4
		public static LocalizedString InsecureConfirmation
		{
			get
			{
				return new LocalizedString("InsecureConfirmation", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000F7DC File Offset: 0x0000D9DC
		public static LocalizedString ConfirmRemoveUserPushNotificationSubscriptions(string identity)
		{
			return new LocalizedString("ConfirmRemoveUserPushNotificationSubscriptions", Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000F804 File Offset: 0x0000DA04
		public static LocalizedString SubscriptionCannotBeChanged
		{
			get
			{
				return new LocalizedString("SubscriptionCannotBeChanged", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000F81C File Offset: 0x0000DA1C
		public static LocalizedString ImportContactListConfirmation(MailboxIdParameter identity)
		{
			return new LocalizedString("ImportContactListConfirmation", Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000F844 File Offset: 0x0000DA44
		public static LocalizedString AutoProvisionComplete
		{
			get
			{
				return new LocalizedString("AutoProvisionComplete", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000F85C File Offset: 0x0000DA5C
		public static LocalizedString SetHotmailSubscriptionConfirmation(AggregationSubscriptionIdParameter identity)
		{
			return new LocalizedString("SetHotmailSubscriptionConfirmation", Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000F884 File Offset: 0x0000DA84
		public static LocalizedString SetLinkedInSubscriptionConfirmation
		{
			get
			{
				return new LocalizedString("SetLinkedInSubscriptionConfirmation", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000F89C File Offset: 0x0000DA9C
		public static LocalizedString SetImapSubscriptionConfirmation(AggregationSubscriptionIdParameter identity)
		{
			return new LocalizedString("SetImapSubscriptionConfirmation", Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000F8C4 File Offset: 0x0000DAC4
		public static LocalizedString FacebookCommunicationError
		{
			get
			{
				return new LocalizedString("FacebookCommunicationError", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000F8DC File Offset: 0x0000DADC
		public static LocalizedString PushNotificationAppSecretEncryptionWarning(string appId)
		{
			return new LocalizedString("PushNotificationAppSecretEncryptionWarning", Strings.ResourceManager, new object[]
			{
				appId
			});
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000F904 File Offset: 0x0000DB04
		public static LocalizedString PushNotificationFailedToResolveFallbackPartition(string appId, string currentPartition)
		{
			return new LocalizedString("PushNotificationFailedToResolveFallbackPartition", Strings.ResourceManager, new object[]
			{
				appId,
				currentPartition
			});
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000F930 File Offset: 0x0000DB30
		public static LocalizedString ErrorAppTargetMailboxNotFound(string mailboxParameterName, string orgAppParameterName)
		{
			return new LocalizedString("ErrorAppTargetMailboxNotFound", Strings.ResourceManager, new object[]
			{
				mailboxParameterName,
				orgAppParameterName
			});
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000F95C File Offset: 0x0000DB5C
		public static LocalizedString WriteVerboseSerializedSubscription(string suscription)
		{
			return new LocalizedString("WriteVerboseSerializedSubscription", Strings.ResourceManager, new object[]
			{
				suscription
			});
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000F984 File Offset: 0x0000DB84
		public static LocalizedString PushNotificationFailedToValidatePartitionWarning(string appId, string partitionName, string fallback)
		{
			return new LocalizedString("PushNotificationFailedToValidatePartitionWarning", Strings.ResourceManager, new object[]
			{
				appId,
				partitionName,
				fallback
			});
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000F9B4 File Offset: 0x0000DBB4
		public static LocalizedString AutoProvisionNoProtocols
		{
			get
			{
				return new LocalizedString("AutoProvisionNoProtocols", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000F9CC File Offset: 0x0000DBCC
		public static LocalizedString ConfirmationMessageDisableProxy(string id, string status)
		{
			return new LocalizedString("ConfirmationMessageDisableProxy", Strings.ResourceManager, new object[]
			{
				id,
				status
			});
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000F9F8 File Offset: 0x0000DBF8
		public static LocalizedString ConfirmationMessageEnableProxy(string id, string status)
		{
			return new LocalizedString("ConfirmationMessageEnableProxy", Strings.ResourceManager, new object[]
			{
				id,
				status
			});
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000FA24 File Offset: 0x0000DC24
		public static LocalizedString ConfirmationMessageModifyOwaOrgExtension(string Identity)
		{
			return new LocalizedString("ConfirmationMessageModifyOwaOrgExtension", Strings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000FA4C File Offset: 0x0000DC4C
		public static LocalizedString WarningExtensionFeatureDisabled
		{
			get
			{
				return new LocalizedString("WarningExtensionFeatureDisabled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000FA64 File Offset: 0x0000DC64
		public static LocalizedString ConfirmationMessageSetApp(string id)
		{
			return new LocalizedString("ConfirmationMessageSetApp", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0000FA8C File Offset: 0x0000DC8C
		public static LocalizedString FacebookAuthorizationError
		{
			get
			{
				return new LocalizedString("FacebookAuthorizationError", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000FAA3 File Offset: 0x0000DCA3
		public static LocalizedString AutoProvisionConnectivity
		{
			get
			{
				return new LocalizedString("AutoProvisionConnectivity", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000FABC File Offset: 0x0000DCBC
		public static LocalizedString ConfirmationMessageUninstallOwaOrgExtension(string Identity)
		{
			return new LocalizedString("ConfirmationMessageUninstallOwaOrgExtension", Strings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000FAE4 File Offset: 0x0000DCE4
		public static LocalizedString ConfirmationMessageUninstallOwaExtension(string Identity, string mailbox)
		{
			return new LocalizedString("ConfirmationMessageUninstallOwaExtension", Strings.ResourceManager, new object[]
			{
				Identity,
				mailbox
			});
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000FB10 File Offset: 0x0000DD10
		public static LocalizedString PushNotificationAppNotFound(string appName)
		{
			return new LocalizedString("PushNotificationAppNotFound", Strings.ResourceManager, new object[]
			{
				appName
			});
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000FB38 File Offset: 0x0000DD38
		public static LocalizedString ConfirmationMessageRemoveSinglePushNotificationSubscription(string storeId)
		{
			return new LocalizedString("ConfirmationMessageRemoveSinglePushNotificationSubscription", Strings.ResourceManager, new object[]
			{
				storeId
			});
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000FB60 File Offset: 0x0000DD60
		public static LocalizedString RemoveCacheMessageConfirmation(CacheIdParameter identity)
		{
			return new LocalizedString("RemoveCacheMessageConfirmation", Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000FB88 File Offset: 0x0000DD88
		public static LocalizedString ErrorMissingFile
		{
			get
			{
				return new LocalizedString("ErrorMissingFile", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000FBA0 File Offset: 0x0000DDA0
		public static LocalizedString ConfirmationMessageRemovePushNotificationSubscription(string identity)
		{
			return new LocalizedString("ConfirmationMessageRemovePushNotificationSubscription", Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000FBC8 File Offset: 0x0000DDC8
		public static LocalizedString ErrorUnsupportedRecipientType(string user, string supportedTypeDetails)
		{
			return new LocalizedString("ErrorUnsupportedRecipientType", Strings.ResourceManager, new object[]
			{
				user,
				supportedTypeDetails
			});
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000FBF4 File Offset: 0x0000DDF4
		public static LocalizedString AutoProvisionDebug(LocalizedString activity, LocalizedString description)
		{
			return new LocalizedString("AutoProvisionDebug", Strings.ResourceManager, new object[]
			{
				activity,
				description
			});
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000FC2A File Offset: 0x0000DE2A
		public static LocalizedString CreateLinkedInSubscriptionConfirmation
		{
			get
			{
				return new LocalizedString("CreateLinkedInSubscriptionConfirmation", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000FC44 File Offset: 0x0000DE44
		public static LocalizedString RemoveSubscriptionConfirmation(AggregationSubscriptionIdParameter identity)
		{
			return new LocalizedString("RemoveSubscriptionConfirmation", Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000FC6C File Offset: 0x0000DE6C
		public static LocalizedString ErrorCannotReadManifestStream(string ErrorMessage)
		{
			return new LocalizedString("ErrorCannotReadManifestStream", Strings.ResourceManager, new object[]
			{
				ErrorMessage
			});
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000FC94 File Offset: 0x0000DE94
		public static LocalizedString SubscriptionCannotBeEnabled
		{
			get
			{
				return new LocalizedString("SubscriptionCannotBeEnabled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000FCAC File Offset: 0x0000DEAC
		public static LocalizedString AutoProvisionConfirmation(PimSubscriptionProxy subscription)
		{
			return new LocalizedString("AutoProvisionConfirmation", Strings.ResourceManager, new object[]
			{
				subscription
			});
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000FCD4 File Offset: 0x0000DED4
		public static LocalizedString ErrorServerCertificateError
		{
			get
			{
				return new LocalizedString("ErrorServerCertificateError", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000FCEC File Offset: 0x0000DEEC
		public static LocalizedString CacheRpcServerFailed(string rpcServer, string failureReason)
		{
			return new LocalizedString("CacheRpcServerFailed", Strings.ResourceManager, new object[]
			{
				rpcServer,
				failureReason
			});
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000FD18 File Offset: 0x0000DF18
		public static LocalizedString CacheRpcInvalidServerVersionIssue(string rpcServer)
		{
			return new LocalizedString("CacheRpcInvalidServerVersionIssue", Strings.ResourceManager, new object[]
			{
				rpcServer
			});
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0000FD40 File Offset: 0x0000DF40
		public static LocalizedString ConfirmationMessageInstallOwaOrgExtension
		{
			get
			{
				return new LocalizedString("ConfirmationMessageInstallOwaOrgExtension", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000FD57 File Offset: 0x0000DF57
		public static LocalizedString ErrorCanNotDownloadPackage
		{
			get
			{
				return new LocalizedString("ErrorCanNotDownloadPackage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000FD6E File Offset: 0x0000DF6E
		public static LocalizedString AutoProvisionQueryDNS
		{
			get
			{
				return new LocalizedString("AutoProvisionQueryDNS", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000FD88 File Offset: 0x0000DF88
		public static LocalizedString CacheRpcExceptionEncountered(Exception exception)
		{
			return new LocalizedString("CacheRpcExceptionEncountered", Strings.ResourceManager, new object[]
			{
				exception
			});
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000FDB0 File Offset: 0x0000DFB0
		public static LocalizedString ErrorUninstallProvidedExtension(string ExtensionName)
		{
			return new LocalizedString("ErrorUninstallProvidedExtension", Strings.ResourceManager, new object[]
			{
				ExtensionName
			});
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000FDD8 File Offset: 0x0000DFD8
		public static LocalizedString IMAPAccountVerificationFailedException
		{
			get
			{
				return new LocalizedString("IMAPAccountVerificationFailedException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000FDF0 File Offset: 0x0000DFF0
		public static LocalizedString ConfirmationMessageEnableOwaExtension(string Identity, string mailbox)
		{
			return new LocalizedString("ConfirmationMessageEnableOwaExtension", Strings.ResourceManager, new object[]
			{
				Identity,
				mailbox
			});
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000FE1C File Offset: 0x0000E01C
		public static LocalizedString PushNotificationSucceededToValidatePartition(string appId, string partitionName)
		{
			return new LocalizedString("PushNotificationSucceededToValidatePartition", Strings.ResourceManager, new object[]
			{
				appId,
				partitionName
			});
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0000FE48 File Offset: 0x0000E048
		public static LocalizedString FacebookEmptyAccessToken
		{
			get
			{
				return new LocalizedString("FacebookEmptyAccessToken", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000FE60 File Offset: 0x0000E060
		public static LocalizedString CreatePopSubscriptionConfirmation(PopSubscriptionProxy subscription)
		{
			return new LocalizedString("CreatePopSubscriptionConfirmation", Strings.ResourceManager, new object[]
			{
				subscription
			});
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000FE88 File Offset: 0x0000E088
		public static LocalizedString ErrorDeserializingSubscription(string serializedSubscription, string error)
		{
			return new LocalizedString("ErrorDeserializingSubscription", Strings.ResourceManager, new object[]
			{
				serializedSubscription,
				error
			});
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000FEB4 File Offset: 0x0000E0B4
		public static LocalizedString CreateIMAPSubscriptionConfirmation(IMAPSubscriptionProxy subscription)
		{
			return new LocalizedString("CreateIMAPSubscriptionConfirmation", Strings.ResourceManager, new object[]
			{
				subscription
			});
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000FEDC File Offset: 0x0000E0DC
		public static LocalizedString NullSubscriptionStoreId
		{
			get
			{
				return new LocalizedString("NullSubscriptionStoreId", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000FEF4 File Offset: 0x0000E0F4
		public static LocalizedString SubscriptionPasswordTooLong(int length, string subscription)
		{
			return new LocalizedString("SubscriptionPasswordTooLong", Strings.ResourceManager, new object[]
			{
				length,
				subscription
			});
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000FF28 File Offset: 0x0000E128
		public static LocalizedString InvalidCacheActionResult(uint cacheActionResult)
		{
			return new LocalizedString("InvalidCacheActionResult", Strings.ResourceManager, new object[]
			{
				cacheActionResult
			});
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000FF58 File Offset: 0x0000E158
		public static LocalizedString SetPopSubscriptionConfirmation(AggregationSubscriptionIdParameter identity)
		{
			return new LocalizedString("SetPopSubscriptionConfirmation", Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000FF80 File Offset: 0x0000E180
		public static LocalizedString acheRpcServerFailed(string rpcServer, string failureReason)
		{
			return new LocalizedString("acheRpcServerFailed", Strings.ResourceManager, new object[]
			{
				rpcServer,
				failureReason
			});
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000FFAC File Offset: 0x0000E1AC
		public static LocalizedString ConfirmationMessageRemoveApp(string id)
		{
			return new LocalizedString("ConfirmationMessageRemoveApp", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0000FFD4 File Offset: 0x0000E1D4
		public static LocalizedString ErrorCannotSpecifyParameterWithoutOrgExtensionParameter
		{
			get
			{
				return new LocalizedString("ErrorCannotSpecifyParameterWithoutOrgExtensionParameter", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000FFEC File Offset: 0x0000E1EC
		public static LocalizedString ConfirmationMessageInstallOwaExtension(string mailbox)
		{
			return new LocalizedString("ConfirmationMessageInstallOwaExtension", Strings.ResourceManager, new object[]
			{
				mailbox
			});
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00010014 File Offset: 0x0000E214
		public static LocalizedString FacebookTimeoutError
		{
			get
			{
				return new LocalizedString("FacebookTimeoutError", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001002C File Offset: 0x0000E22C
		public static LocalizedString CacheRpcServerStopped(string rpcServer)
		{
			return new LocalizedString("CacheRpcServerStopped", Strings.ResourceManager, new object[]
			{
				rpcServer
			});
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00010054 File Offset: 0x0000E254
		public static LocalizedString PushNotificationAppFound(string appName, string app)
		{
			return new LocalizedString("PushNotificationAppFound", Strings.ResourceManager, new object[]
			{
				appName,
				app
			});
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00010080 File Offset: 0x0000E280
		public static LocalizedString SetCacheMessageConfirmation(CacheIdParameter identity)
		{
			return new LocalizedString("SetCacheMessageConfirmation", Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x000100A8 File Offset: 0x0000E2A8
		public static LocalizedString CreateFacebookSubscriptionConfirmation
		{
			get
			{
				return new LocalizedString("CreateFacebookSubscriptionConfirmation", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x000100BF File Offset: 0x0000E2BF
		public static LocalizedString AutoProvisionResults
		{
			get
			{
				return new LocalizedString("AutoProvisionResults", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x000100D6 File Offset: 0x0000E2D6
		public static LocalizedString ErrorNoInputForExtensionInstall
		{
			get
			{
				return new LocalizedString("ErrorNoInputForExtensionInstall", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x000100F0 File Offset: 0x0000E2F0
		public static LocalizedString ConfirmationMessageDisableOwaExtension(string Identity, string mailbox)
		{
			return new LocalizedString("ConfirmationMessageDisableOwaExtension", Strings.ResourceManager, new object[]
			{
				Identity,
				mailbox
			});
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0001011C File Offset: 0x0000E31C
		public static LocalizedString AutoProvisionCreate
		{
			get
			{
				return new LocalizedString("AutoProvisionCreate", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x00010133 File Offset: 0x0000E333
		public static LocalizedString ErrorReasonUserNotAllowedToInstallReadWriteMailbox
		{
			get
			{
				return new LocalizedString("ErrorReasonUserNotAllowedToInstallReadWriteMailbox", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0001014A File Offset: 0x0000E34A
		public static LocalizedString SetFacebookSubscriptionConfirmation
		{
			get
			{
				return new LocalizedString("SetFacebookSubscriptionConfirmation", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00010164 File Offset: 0x0000E364
		public static LocalizedString ErrorUninstallDefaultExtension(string ExtensionName)
		{
			return new LocalizedString("ErrorUninstallDefaultExtension", Strings.ResourceManager, new object[]
			{
				ExtensionName
			});
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001018C File Offset: 0x0000E38C
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000108 RID: 264
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(27);

		// Token: 0x04000109 RID: 265
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Management.Mobility.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200005B RID: 91
		public enum IDs : uint
		{
			// Token: 0x0400010B RID: 267
			InsecureConfirmation = 1372907051U,
			// Token: 0x0400010C RID: 268
			SubscriptionCannotBeChanged = 1199310315U,
			// Token: 0x0400010D RID: 269
			AutoProvisionComplete = 456403923U,
			// Token: 0x0400010E RID: 270
			SetLinkedInSubscriptionConfirmation = 4170829984U,
			// Token: 0x0400010F RID: 271
			FacebookCommunicationError = 843454792U,
			// Token: 0x04000110 RID: 272
			AutoProvisionNoProtocols = 433732448U,
			// Token: 0x04000111 RID: 273
			WarningExtensionFeatureDisabled = 1081354883U,
			// Token: 0x04000112 RID: 274
			FacebookAuthorizationError = 2674661771U,
			// Token: 0x04000113 RID: 275
			AutoProvisionConnectivity = 3132041271U,
			// Token: 0x04000114 RID: 276
			ErrorMissingFile = 3962568818U,
			// Token: 0x04000115 RID: 277
			CreateLinkedInSubscriptionConfirmation = 1935884132U,
			// Token: 0x04000116 RID: 278
			SubscriptionCannotBeEnabled = 232075358U,
			// Token: 0x04000117 RID: 279
			ErrorServerCertificateError = 2121885544U,
			// Token: 0x04000118 RID: 280
			ConfirmationMessageInstallOwaOrgExtension = 3868201023U,
			// Token: 0x04000119 RID: 281
			ErrorCanNotDownloadPackage = 3256811515U,
			// Token: 0x0400011A RID: 282
			AutoProvisionQueryDNS = 562700869U,
			// Token: 0x0400011B RID: 283
			IMAPAccountVerificationFailedException = 424238761U,
			// Token: 0x0400011C RID: 284
			FacebookEmptyAccessToken = 4012336426U,
			// Token: 0x0400011D RID: 285
			NullSubscriptionStoreId = 2684496906U,
			// Token: 0x0400011E RID: 286
			ErrorCannotSpecifyParameterWithoutOrgExtensionParameter = 966086681U,
			// Token: 0x0400011F RID: 287
			FacebookTimeoutError = 408801391U,
			// Token: 0x04000120 RID: 288
			CreateFacebookSubscriptionConfirmation = 1307888814U,
			// Token: 0x04000121 RID: 289
			AutoProvisionResults = 1548922274U,
			// Token: 0x04000122 RID: 290
			ErrorNoInputForExtensionInstall = 3353546040U,
			// Token: 0x04000123 RID: 291
			AutoProvisionCreate = 2973592384U,
			// Token: 0x04000124 RID: 292
			ErrorReasonUserNotAllowedToInstallReadWriteMailbox = 4263159413U,
			// Token: 0x04000125 RID: 293
			SetFacebookSubscriptionConfirmation = 2907685502U
		}

		// Token: 0x0200005C RID: 92
		private enum ParamIDs
		{
			// Token: 0x04000127 RID: 295
			ErrorTooManyUsersInUserList,
			// Token: 0x04000128 RID: 296
			CreateHotmailSubscriptionConfirmation,
			// Token: 0x04000129 RID: 297
			PushNotificationAppPlatformMismatch,
			// Token: 0x0400012A RID: 298
			ConfirmationMessageNewApp,
			// Token: 0x0400012B RID: 299
			ConfirmRemoveUserPushNotificationSubscriptions,
			// Token: 0x0400012C RID: 300
			ImportContactListConfirmation,
			// Token: 0x0400012D RID: 301
			SetHotmailSubscriptionConfirmation,
			// Token: 0x0400012E RID: 302
			SetImapSubscriptionConfirmation,
			// Token: 0x0400012F RID: 303
			PushNotificationAppSecretEncryptionWarning,
			// Token: 0x04000130 RID: 304
			PushNotificationFailedToResolveFallbackPartition,
			// Token: 0x04000131 RID: 305
			ErrorAppTargetMailboxNotFound,
			// Token: 0x04000132 RID: 306
			WriteVerboseSerializedSubscription,
			// Token: 0x04000133 RID: 307
			PushNotificationFailedToValidatePartitionWarning,
			// Token: 0x04000134 RID: 308
			ConfirmationMessageDisableProxy,
			// Token: 0x04000135 RID: 309
			ConfirmationMessageEnableProxy,
			// Token: 0x04000136 RID: 310
			ConfirmationMessageModifyOwaOrgExtension,
			// Token: 0x04000137 RID: 311
			ConfirmationMessageSetApp,
			// Token: 0x04000138 RID: 312
			ConfirmationMessageUninstallOwaOrgExtension,
			// Token: 0x04000139 RID: 313
			ConfirmationMessageUninstallOwaExtension,
			// Token: 0x0400013A RID: 314
			PushNotificationAppNotFound,
			// Token: 0x0400013B RID: 315
			ConfirmationMessageRemoveSinglePushNotificationSubscription,
			// Token: 0x0400013C RID: 316
			RemoveCacheMessageConfirmation,
			// Token: 0x0400013D RID: 317
			ConfirmationMessageRemovePushNotificationSubscription,
			// Token: 0x0400013E RID: 318
			ErrorUnsupportedRecipientType,
			// Token: 0x0400013F RID: 319
			AutoProvisionDebug,
			// Token: 0x04000140 RID: 320
			RemoveSubscriptionConfirmation,
			// Token: 0x04000141 RID: 321
			ErrorCannotReadManifestStream,
			// Token: 0x04000142 RID: 322
			AutoProvisionConfirmation,
			// Token: 0x04000143 RID: 323
			CacheRpcServerFailed,
			// Token: 0x04000144 RID: 324
			CacheRpcInvalidServerVersionIssue,
			// Token: 0x04000145 RID: 325
			CacheRpcExceptionEncountered,
			// Token: 0x04000146 RID: 326
			ErrorUninstallProvidedExtension,
			// Token: 0x04000147 RID: 327
			ConfirmationMessageEnableOwaExtension,
			// Token: 0x04000148 RID: 328
			PushNotificationSucceededToValidatePartition,
			// Token: 0x04000149 RID: 329
			CreatePopSubscriptionConfirmation,
			// Token: 0x0400014A RID: 330
			ErrorDeserializingSubscription,
			// Token: 0x0400014B RID: 331
			CreateIMAPSubscriptionConfirmation,
			// Token: 0x0400014C RID: 332
			SubscriptionPasswordTooLong,
			// Token: 0x0400014D RID: 333
			InvalidCacheActionResult,
			// Token: 0x0400014E RID: 334
			SetPopSubscriptionConfirmation,
			// Token: 0x0400014F RID: 335
			acheRpcServerFailed,
			// Token: 0x04000150 RID: 336
			ConfirmationMessageRemoveApp,
			// Token: 0x04000151 RID: 337
			ConfirmationMessageInstallOwaExtension,
			// Token: 0x04000152 RID: 338
			CacheRpcServerStopped,
			// Token: 0x04000153 RID: 339
			PushNotificationAppFound,
			// Token: 0x04000154 RID: 340
			SetCacheMessageConfirmation,
			// Token: 0x04000155 RID: 341
			ConfirmationMessageDisableOwaExtension,
			// Token: 0x04000156 RID: 342
			ErrorUninstallDefaultExtension
		}
	}
}
