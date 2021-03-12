using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200004C RID: 76
	internal static class Strings
	{
		// Token: 0x060003B2 RID: 946 RVA: 0x00010118 File Offset: 0x0000E318
		static Strings()
		{
			Strings.stringIDs.Add(4054043086U, "ReservedString1");
			Strings.stringIDs.Add(2470358009U, "ErrorUnableToGetGroupOwners");
			Strings.stringIDs.Add(366868245U, "SenderNotSpecifiedAndNotPresentInMessage");
			Strings.stringIDs.Add(2628020095U, "ValidateRepairInvalidUser");
			Strings.stringIDs.Add(3952589442U, "MimeDoesNotComplyWithStandards");
			Strings.stringIDs.Add(1272170093U, "ErrorUnableToGetCreatorFromGroupMailbox");
			Strings.stringIDs.Add(3855259485U, "ErrorLocalMachineIsNotExchangeServer");
			Strings.stringIDs.Add(3353703238U, "TestMessageDefaultBody");
			Strings.stringIDs.Add(3038652633U, "MustSpecifyAtLeastOneSmtpRecipientAddress");
			Strings.stringIDs.Add(3248953978U, "ValidateRepairUpdateMissingStatus");
			Strings.stringIDs.Add(210240938U, "WarningUnableToUpdateUserMailboxes");
			Strings.stringIDs.Add(1157487U, "ErrorUnableToGetUnifiedGroup");
			Strings.stringIDs.Add(110985478U, "CalendarValidationTask");
			Strings.stringIDs.Add(84207768U, "ErrorUnableToSessionWithAAD");
			Strings.stringIDs.Add(2906052647U, "WarningUnableToUpdateExchangeResources");
			Strings.stringIDs.Add(46749802U, "ErrorUnableToUpdateUnifiedGroup");
			Strings.stringIDs.Add(891487637U, "ErrorUnableToCreateUnifiedGroup");
			Strings.stringIDs.Add(3322833458U, "TestMessageDefaultSubject");
			Strings.stringIDs.Add(775190192U, "UnableToDiscoverDefaultDomain");
			Strings.stringIDs.Add(2995683657U, "MessageFileOrSenderMustBeSpecified");
			Strings.stringIDs.Add(2481051232U, "NoHubsAvailable");
			Strings.stringIDs.Add(2635939130U, "ExchangeSupportPSSnapInDescription");
			Strings.stringIDs.Add(332607451U, "SpnRegistrationSucceeded");
			Strings.stringIDs.Add(784901781U, "ErrorMissingWebDnsInformation");
			Strings.stringIDs.Add(4054043087U, "ReservedString2");
			Strings.stringIDs.Add(3311173118U, "ValidateRepairUpdateStatus");
			Strings.stringIDs.Add(4054043081U, "ReservedString4");
			Strings.stringIDs.Add(2589164433U, "ConfirmationMessageTestMessage");
			Strings.stringIDs.Add(1531210305U, "ConfirmationMessageRepairMigration");
			Strings.stringIDs.Add(1622178204U, "ErrorLocalServerIsNotMailboxServer");
			Strings.stringIDs.Add(4054043088U, "ReservedString3");
			Strings.stringIDs.Add(4142544366U, "MessageFileDataSpecifiedAsNull");
			Strings.stringIDs.Add(4054043082U, "ReservedString5");
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x000103E8 File Offset: 0x0000E5E8
		public static LocalizedString ReservedString1
		{
			get
			{
				return new LocalizedString("ReservedString1", "ExDC7495", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00010408 File Offset: 0x0000E608
		public static LocalizedString WarningUnableToRemoveMembers(string members)
		{
			return new LocalizedString("WarningUnableToRemoveMembers", "", false, false, Strings.ResourceManager, new object[]
			{
				members
			});
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00010437 File Offset: 0x0000E637
		public static LocalizedString ErrorUnableToGetGroupOwners
		{
			get
			{
				return new LocalizedString("ErrorUnableToGetGroupOwners", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00010458 File Offset: 0x0000E658
		public static LocalizedString SpnRegistrationFailed(int errorCode)
		{
			return new LocalizedString("SpnRegistrationFailed", "Ex5337E8", false, true, Strings.ResourceManager, new object[]
			{
				errorCode
			});
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0001048C File Offset: 0x0000E68C
		public static LocalizedString SenderNotSpecifiedAndNotPresentInMessage
		{
			get
			{
				return new LocalizedString("SenderNotSpecifiedAndNotPresentInMessage", "Ex1EE6BD", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x000104AA File Offset: 0x0000E6AA
		public static LocalizedString ValidateRepairInvalidUser
		{
			get
			{
				return new LocalizedString("ValidateRepairInvalidUser", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x000104C8 File Offset: 0x0000E6C8
		public static LocalizedString ValidatingCalendar(string username)
		{
			return new LocalizedString("ValidatingCalendar", "Ex304041", false, true, Strings.ResourceManager, new object[]
			{
				username
			});
		}

		// Token: 0x060003BA RID: 954 RVA: 0x000104F8 File Offset: 0x0000E6F8
		public static LocalizedString ValidateRepairInvalidRevert(string name, string status)
		{
			return new LocalizedString("ValidateRepairInvalidRevert", "", false, false, Strings.ResourceManager, new object[]
			{
				name,
				status
			});
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0001052B File Offset: 0x0000E72B
		public static LocalizedString MimeDoesNotComplyWithStandards
		{
			get
			{
				return new LocalizedString("MimeDoesNotComplyWithStandards", "ExC2EC33", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001054C File Offset: 0x0000E74C
		public static LocalizedString ValidateRepairMultipleUsers(string name, int count)
		{
			return new LocalizedString("ValidateRepairMultipleUsers", "", false, false, Strings.ResourceManager, new object[]
			{
				name,
				count
			});
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060003BD RID: 957 RVA: 0x00010584 File Offset: 0x0000E784
		public static LocalizedString ErrorUnableToGetCreatorFromGroupMailbox
		{
			get
			{
				return new LocalizedString("ErrorUnableToGetCreatorFromGroupMailbox", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000105A4 File Offset: 0x0000E7A4
		public static LocalizedString ErrorMissingServerFqdn(string idStringValue)
		{
			return new LocalizedString("ErrorMissingServerFqdn", "Ex174BA5", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000105D4 File Offset: 0x0000E7D4
		public static LocalizedString ConfirmRepairSubscription(string action, string type, string mailboxData)
		{
			return new LocalizedString("ConfirmRepairSubscription", "", false, false, Strings.ResourceManager, new object[]
			{
				action,
				type,
				mailboxData
			});
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0001060C File Offset: 0x0000E80C
		public static LocalizedString ErrorNoLocalOrganizationMailbox(string identity)
		{
			return new LocalizedString("ErrorNoLocalOrganizationMailbox", "", false, false, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0001063B File Offset: 0x0000E83B
		public static LocalizedString ErrorLocalMachineIsNotExchangeServer
		{
			get
			{
				return new LocalizedString("ErrorLocalMachineIsNotExchangeServer", "ExF7A5D4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00010659 File Offset: 0x0000E859
		public static LocalizedString TestMessageDefaultBody
		{
			get
			{
				return new LocalizedString("TestMessageDefaultBody", "Ex5D93AE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x00010677 File Offset: 0x0000E877
		public static LocalizedString MustSpecifyAtLeastOneSmtpRecipientAddress
		{
			get
			{
				return new LocalizedString("MustSpecifyAtLeastOneSmtpRecipientAddress", "Ex814118", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x00010695 File Offset: 0x0000E895
		public static LocalizedString ValidateRepairUpdateMissingStatus
		{
			get
			{
				return new LocalizedString("ValidateRepairUpdateMissingStatus", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x000106B3 File Offset: 0x0000E8B3
		public static LocalizedString WarningUnableToUpdateUserMailboxes
		{
			get
			{
				return new LocalizedString("WarningUnableToUpdateUserMailboxes", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000106D4 File Offset: 0x0000E8D4
		public static LocalizedString ErrorGetRestrictionTableForFolderFailed(string databaseId, string folderId)
		{
			return new LocalizedString("ErrorGetRestrictionTableForFolderFailed", "", false, false, Strings.ResourceManager, new object[]
			{
				databaseId,
				folderId
			});
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00010707 File Offset: 0x0000E907
		public static LocalizedString ErrorUnableToGetUnifiedGroup
		{
			get
			{
				return new LocalizedString("ErrorUnableToGetUnifiedGroup", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00010728 File Offset: 0x0000E928
		public static LocalizedString WarningUnableToAddMembers(string members)
		{
			return new LocalizedString("WarningUnableToAddMembers", "", false, false, Strings.ResourceManager, new object[]
			{
				members
			});
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00010758 File Offset: 0x0000E958
		public static LocalizedString WorkItemNotFoundException(string workitemId)
		{
			return new LocalizedString("WorkItemNotFoundException", "", false, false, Strings.ResourceManager, new object[]
			{
				workitemId
			});
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00010787 File Offset: 0x0000E987
		public static LocalizedString CalendarValidationTask
		{
			get
			{
				return new LocalizedString("CalendarValidationTask", "Ex22AA7E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060003CB RID: 971 RVA: 0x000107A5 File Offset: 0x0000E9A5
		public static LocalizedString ErrorUnableToSessionWithAAD
		{
			get
			{
				return new LocalizedString("ErrorUnableToSessionWithAAD", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000107C4 File Offset: 0x0000E9C4
		public static LocalizedString ErrorSGOwningServerNotFound(string idStringValue)
		{
			return new LocalizedString("ErrorSGOwningServerNotFound", "ExA888DC", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x060003CD RID: 973 RVA: 0x000107F4 File Offset: 0x0000E9F4
		public static LocalizedString ErrorUnableToRemove(string id)
		{
			return new LocalizedString("ErrorUnableToRemove", "", false, false, Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00010823 File Offset: 0x0000EA23
		public static LocalizedString WarningUnableToUpdateExchangeResources
		{
			get
			{
				return new LocalizedString("WarningUnableToUpdateExchangeResources", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00010844 File Offset: 0x0000EA44
		public static LocalizedString ErrorStorageGroupNotUnique(string idStringValue)
		{
			return new LocalizedString("ErrorStorageGroupNotUnique", "ExA9CCAF", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00010873 File Offset: 0x0000EA73
		public static LocalizedString ErrorUnableToUpdateUnifiedGroup
		{
			get
			{
				return new LocalizedString("ErrorUnableToUpdateUnifiedGroup", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00010891 File Offset: 0x0000EA91
		public static LocalizedString ErrorUnableToCreateUnifiedGroup
		{
			get
			{
				return new LocalizedString("ErrorUnableToCreateUnifiedGroup", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000108B0 File Offset: 0x0000EAB0
		public static LocalizedString ErrorDatabaseNotUnique(string idStringValue)
		{
			return new LocalizedString("ErrorDatabaseNotUnique", "Ex99FB63", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x000108DF File Offset: 0x0000EADF
		public static LocalizedString TestMessageDefaultSubject
		{
			get
			{
				return new LocalizedString("TestMessageDefaultSubject", "Ex89393F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x000108FD File Offset: 0x0000EAFD
		public static LocalizedString UnableToDiscoverDefaultDomain
		{
			get
			{
				return new LocalizedString("UnableToDiscoverDefaultDomain", "Ex1EBAE3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001091C File Offset: 0x0000EB1C
		public static LocalizedString ConfirmRepairRemoveProperty(string property, string value, string migrationObject)
		{
			return new LocalizedString("ConfirmRepairRemoveProperty", "", false, false, Strings.ResourceManager, new object[]
			{
				property,
				value,
				migrationObject
			});
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00010953 File Offset: 0x0000EB53
		public static LocalizedString MessageFileOrSenderMustBeSpecified
		{
			get
			{
				return new LocalizedString("MessageFileOrSenderMustBeSpecified", "ExDC8C6A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00010974 File Offset: 0x0000EB74
		public static LocalizedString ConfirmRepairResumeIMAPSubscription(string user)
		{
			return new LocalizedString("ConfirmRepairResumeIMAPSubscription", "", false, false, Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x000109A4 File Offset: 0x0000EBA4
		public static LocalizedString ErrorCannotUpdateExternalDirectoryObjectId(string id, string objectId)
		{
			return new LocalizedString("ErrorCannotUpdateExternalDirectoryObjectId", "", false, false, Strings.ResourceManager, new object[]
			{
				id,
				objectId
			});
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x000109D8 File Offset: 0x0000EBD8
		public static LocalizedString WarningUnableToAddOwners(string owners)
		{
			return new LocalizedString("WarningUnableToAddOwners", "", false, false, Strings.ResourceManager, new object[]
			{
				owners
			});
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00010A07 File Offset: 0x0000EC07
		public static LocalizedString NoHubsAvailable
		{
			get
			{
				return new LocalizedString("NoHubsAvailable", "ExC4054F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00010A28 File Offset: 0x0000EC28
		public static LocalizedString ErrorServerNotFound(string idStringValue)
		{
			return new LocalizedString("ErrorServerNotFound", "ExAA7402", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00010A58 File Offset: 0x0000EC58
		public static LocalizedString SetUpgradeWorkItemConfirmationMessage(string workitemId, string modifiedProperties)
		{
			return new LocalizedString("SetUpgradeWorkItemConfirmationMessage", "", false, false, Strings.ResourceManager, new object[]
			{
				workitemId,
				modifiedProperties
			});
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00010A8C File Offset: 0x0000EC8C
		public static LocalizedString ErrorInvalidObjectMissingCriticalProperty(string type, string identity, string property)
		{
			return new LocalizedString("ErrorInvalidObjectMissingCriticalProperty", "ExFD016D", false, true, Strings.ResourceManager, new object[]
			{
				type,
				identity,
				property
			});
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00010AC3 File Offset: 0x0000ECC3
		public static LocalizedString ExchangeSupportPSSnapInDescription
		{
			get
			{
				return new LocalizedString("ExchangeSupportPSSnapInDescription", "Ex3C4848", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00010AE1 File Offset: 0x0000ECE1
		public static LocalizedString SpnRegistrationSucceeded
		{
			get
			{
				return new LocalizedString("SpnRegistrationSucceeded", "ExEE0658", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00010B00 File Offset: 0x0000ED00
		public static LocalizedString ErrorCannotReadDatabaseEvents(string databaseId)
		{
			return new LocalizedString("ErrorCannotReadDatabaseEvents", "Ex2BDE89", false, true, Strings.ResourceManager, new object[]
			{
				databaseId
			});
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00010B30 File Offset: 0x0000ED30
		public static LocalizedString WarningUnableToRemoveOwners(string owners)
		{
			return new LocalizedString("WarningUnableToRemoveOwners", "", false, false, Strings.ResourceManager, new object[]
			{
				owners
			});
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00010B5F File Offset: 0x0000ED5F
		public static LocalizedString ErrorMissingWebDnsInformation
		{
			get
			{
				return new LocalizedString("ErrorMissingWebDnsInformation", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00010B7D File Offset: 0x0000ED7D
		public static LocalizedString ReservedString2
		{
			get
			{
				return new LocalizedString("ReservedString2", "Ex8B43F6", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00010B9B File Offset: 0x0000ED9B
		public static LocalizedString ValidateRepairUpdateStatus
		{
			get
			{
				return new LocalizedString("ValidateRepairUpdateStatus", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00010BBC File Offset: 0x0000EDBC
		public static LocalizedString ErrorStorageGroupNotFound(string idStringValue)
		{
			return new LocalizedString("ErrorStorageGroupNotFound", "ExE6BB2D", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00010BEC File Offset: 0x0000EDEC
		public static LocalizedString ErrorServerNotUnique(string idStringValue)
		{
			return new LocalizedString("ErrorServerNotUnique", "Ex9943D0", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00010C1C File Offset: 0x0000EE1C
		public static LocalizedString TryingToSubmitTestmessage(string serverName)
		{
			return new LocalizedString("TryingToSubmitTestmessage", "ExF32415", false, true, Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00010C4C File Offset: 0x0000EE4C
		public static LocalizedString ValidateRepairMissingReport(string name)
		{
			return new LocalizedString("ValidateRepairMissingReport", "", false, false, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00010C7C File Offset: 0x0000EE7C
		public static LocalizedString ValidateRepairMissingSubscription(string name, string error)
		{
			return new LocalizedString("ValidateRepairMissingSubscription", "", false, false, Strings.ResourceManager, new object[]
			{
				name,
				error
			});
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00010CB0 File Offset: 0x0000EEB0
		public static LocalizedString ConfirmRepairUser(string action, string user)
		{
			return new LocalizedString("ConfirmRepairUser", "", false, false, Strings.ResourceManager, new object[]
			{
				action,
				user
			});
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00010CE4 File Offset: 0x0000EEE4
		public static LocalizedString InvalidTenantGuidError(string id)
		{
			return new LocalizedString("InvalidTenantGuidError", "", false, false, Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00010D14 File Offset: 0x0000EF14
		public static LocalizedString ErrorMaxThreadPoolThreads(int maxTPoolThreads)
		{
			return new LocalizedString("ErrorMaxThreadPoolThreads", "ExD494AC", false, true, Strings.ResourceManager, new object[]
			{
				maxTPoolThreads
			});
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00010D48 File Offset: 0x0000EF48
		public static LocalizedString ConfirmRepairUpdateProperty(string property, string oldValue, string newValue, string migrationObject)
		{
			return new LocalizedString("ConfirmRepairUpdateProperty", "", false, false, Strings.ResourceManager, new object[]
			{
				property,
				oldValue,
				newValue,
				migrationObject
			});
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00010D83 File Offset: 0x0000EF83
		public static LocalizedString ReservedString4
		{
			get
			{
				return new LocalizedString("ReservedString4", "Ex99A1B0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00010DA4 File Offset: 0x0000EFA4
		public static LocalizedString ConfirmRepairUpdateCacheEntry(string org)
		{
			return new LocalizedString("ConfirmRepairUpdateCacheEntry", "", false, false, Strings.ResourceManager, new object[]
			{
				org
			});
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00010DD4 File Offset: 0x0000EFD4
		public static LocalizedString SubmittedSuccessfully(string serverName)
		{
			return new LocalizedString("SubmittedSuccessfully", "ExA14C26", false, true, Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00010E03 File Offset: 0x0000F003
		public static LocalizedString ConfirmationMessageTestMessage
		{
			get
			{
				return new LocalizedString("ConfirmationMessageTestMessage", "Ex791CD8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00010E24 File Offset: 0x0000F024
		public static LocalizedString UsingDefaultDomainFromAD(string domain)
		{
			return new LocalizedString("UsingDefaultDomainFromAD", "Ex822608", false, true, Strings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00010E54 File Offset: 0x0000F054
		public static LocalizedString ConfirmRepairRemoveReport(string report)
		{
			return new LocalizedString("ConfirmRepairRemoveReport", "", false, false, Strings.ResourceManager, new object[]
			{
				report
			});
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00010E83 File Offset: 0x0000F083
		public static LocalizedString ConfirmationMessageRepairMigration
		{
			get
			{
				return new LocalizedString("ConfirmationMessageRepairMigration", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00010EA4 File Offset: 0x0000F0A4
		public static LocalizedString UnableToCreateFromMsg(string exceptionTest)
		{
			return new LocalizedString("UnableToCreateFromMsg", "Ex2778D7", false, true, Strings.ResourceManager, new object[]
			{
				exceptionTest
			});
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00010ED4 File Offset: 0x0000F0D4
		public static LocalizedString ErrorDatabaseNotFound(string idStringValue)
		{
			return new LocalizedString("ErrorDatabaseNotFound", "Ex293997", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00010F04 File Offset: 0x0000F104
		public static LocalizedString ConfirmRepairRemoveFolder(string folder)
		{
			return new LocalizedString("ConfirmRepairRemoveFolder", "", false, false, Strings.ResourceManager, new object[]
			{
				folder
			});
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00010F34 File Offset: 0x0000F134
		public static LocalizedString ErrorStartDateEqualGreaterThanEndDate(string startDate, string endDate)
		{
			return new LocalizedString("ErrorStartDateEqualGreaterThanEndDate", "Ex2130B8", false, true, Strings.ResourceManager, new object[]
			{
				startDate,
				endDate
			});
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00010F68 File Offset: 0x0000F168
		public static LocalizedString ErrorRepairConvertStatus(string name, string type)
		{
			return new LocalizedString("ErrorRepairConvertStatus", "", false, false, Strings.ResourceManager, new object[]
			{
				name,
				type
			});
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00010F9C File Offset: 0x0000F19C
		public static LocalizedString ValidateRepairMissingSubscriptionHandler(string name)
		{
			return new LocalizedString("ValidateRepairMissingSubscriptionHandler", "", false, false, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00010FCC File Offset: 0x0000F1CC
		public static LocalizedString ErrorDBOwningServerNotFound(string idStringValue)
		{
			return new LocalizedString("ErrorDBOwningServerNotFound", "Ex5A9B8A", false, true, Strings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00010FFC File Offset: 0x0000F1FC
		public static LocalizedString ErrorResultSizeOutOfRange(string min, string max)
		{
			return new LocalizedString("ErrorResultSizeOutOfRange", "Ex9AEA0D", false, true, Strings.ResourceManager, new object[]
			{
				min,
				max
			});
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00011030 File Offset: 0x0000F230
		public static LocalizedString InvalidTestMessageFileData(string mimeError)
		{
			return new LocalizedString("InvalidTestMessageFileData", "ExC952D6", false, true, Strings.ResourceManager, new object[]
			{
				mimeError
			});
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00011060 File Offset: 0x0000F260
		public static LocalizedString ErrorRepairReverting(string name)
		{
			return new LocalizedString("ErrorRepairReverting", "", false, false, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0001108F File Offset: 0x0000F28F
		public static LocalizedString ErrorLocalServerIsNotMailboxServer
		{
			get
			{
				return new LocalizedString("ErrorLocalServerIsNotMailboxServer", "Ex3BF88C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000110B0 File Offset: 0x0000F2B0
		public static LocalizedString ConfirmRepairRemoveUsers(string email)
		{
			return new LocalizedString("ConfirmRepairRemoveUsers", "", false, false, Strings.ResourceManager, new object[]
			{
				email
			});
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000110E0 File Offset: 0x0000F2E0
		public static LocalizedString ConfirmRepairBatch(string action, string job)
		{
			return new LocalizedString("ConfirmRepairBatch", "", false, false, Strings.ResourceManager, new object[]
			{
				action,
				job
			});
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00011113 File Offset: 0x0000F313
		public static LocalizedString ReservedString3
		{
			get
			{
				return new LocalizedString("ReservedString3", "Ex26688F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00011134 File Offset: 0x0000F334
		public static LocalizedString ValidateRepairInvalidRevertJobType(string name)
		{
			return new LocalizedString("ValidateRepairInvalidRevertJobType", "", false, false, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00011164 File Offset: 0x0000F364
		public static LocalizedString NoExternalDirectoryObjectIdForRecipientId(string recipId)
		{
			return new LocalizedString("NoExternalDirectoryObjectIdForRecipientId", "", false, false, Strings.ResourceManager, new object[]
			{
				recipId
			});
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00011193 File Offset: 0x0000F393
		public static LocalizedString MessageFileDataSpecifiedAsNull
		{
			get
			{
				return new LocalizedString("MessageFileDataSpecifiedAsNull", "Ex65970C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x000111B1 File Offset: 0x0000F3B1
		public static LocalizedString ReservedString5
		{
			get
			{
				return new LocalizedString("ReservedString5", "Ex090BFC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000111D0 File Offset: 0x0000F3D0
		public static LocalizedString ConfirmRepairRemoveStoreObject(string id)
		{
			return new LocalizedString("ConfirmRepairRemoveStoreObject", "", false, false, Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00011200 File Offset: 0x0000F400
		public static LocalizedString UsingDefaultDomainFromRecipient(string domain)
		{
			return new LocalizedString("UsingDefaultDomainFromRecipient", "Ex60CB42", false, true, Strings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00011230 File Offset: 0x0000F430
		public static LocalizedString ErrorCannotReadDatabaseWatermarks(string databaseId)
		{
			return new LocalizedString("ErrorCannotReadDatabaseWatermarks", "ExF17064", false, true, Strings.ResourceManager, new object[]
			{
				databaseId
			});
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00011260 File Offset: 0x0000F460
		public static LocalizedString WarnRepairRemovingUser(string name)
		{
			return new LocalizedString("WarnRepairRemovingUser", "", false, false, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00011290 File Offset: 0x0000F490
		public static LocalizedString InvalidStatusDetailError(string uri)
		{
			return new LocalizedString("InvalidStatusDetailError", "", false, false, Strings.ResourceManager, new object[]
			{
				uri
			});
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000112BF File Offset: 0x0000F4BF
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000165 RID: 357
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(33);

		// Token: 0x04000166 RID: 358
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Management.Powershell.Support.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200004D RID: 77
		public enum IDs : uint
		{
			// Token: 0x04000168 RID: 360
			ReservedString1 = 4054043086U,
			// Token: 0x04000169 RID: 361
			ErrorUnableToGetGroupOwners = 2470358009U,
			// Token: 0x0400016A RID: 362
			SenderNotSpecifiedAndNotPresentInMessage = 366868245U,
			// Token: 0x0400016B RID: 363
			ValidateRepairInvalidUser = 2628020095U,
			// Token: 0x0400016C RID: 364
			MimeDoesNotComplyWithStandards = 3952589442U,
			// Token: 0x0400016D RID: 365
			ErrorUnableToGetCreatorFromGroupMailbox = 1272170093U,
			// Token: 0x0400016E RID: 366
			ErrorLocalMachineIsNotExchangeServer = 3855259485U,
			// Token: 0x0400016F RID: 367
			TestMessageDefaultBody = 3353703238U,
			// Token: 0x04000170 RID: 368
			MustSpecifyAtLeastOneSmtpRecipientAddress = 3038652633U,
			// Token: 0x04000171 RID: 369
			ValidateRepairUpdateMissingStatus = 3248953978U,
			// Token: 0x04000172 RID: 370
			WarningUnableToUpdateUserMailboxes = 210240938U,
			// Token: 0x04000173 RID: 371
			ErrorUnableToGetUnifiedGroup = 1157487U,
			// Token: 0x04000174 RID: 372
			CalendarValidationTask = 110985478U,
			// Token: 0x04000175 RID: 373
			ErrorUnableToSessionWithAAD = 84207768U,
			// Token: 0x04000176 RID: 374
			WarningUnableToUpdateExchangeResources = 2906052647U,
			// Token: 0x04000177 RID: 375
			ErrorUnableToUpdateUnifiedGroup = 46749802U,
			// Token: 0x04000178 RID: 376
			ErrorUnableToCreateUnifiedGroup = 891487637U,
			// Token: 0x04000179 RID: 377
			TestMessageDefaultSubject = 3322833458U,
			// Token: 0x0400017A RID: 378
			UnableToDiscoverDefaultDomain = 775190192U,
			// Token: 0x0400017B RID: 379
			MessageFileOrSenderMustBeSpecified = 2995683657U,
			// Token: 0x0400017C RID: 380
			NoHubsAvailable = 2481051232U,
			// Token: 0x0400017D RID: 381
			ExchangeSupportPSSnapInDescription = 2635939130U,
			// Token: 0x0400017E RID: 382
			SpnRegistrationSucceeded = 332607451U,
			// Token: 0x0400017F RID: 383
			ErrorMissingWebDnsInformation = 784901781U,
			// Token: 0x04000180 RID: 384
			ReservedString2 = 4054043087U,
			// Token: 0x04000181 RID: 385
			ValidateRepairUpdateStatus = 3311173118U,
			// Token: 0x04000182 RID: 386
			ReservedString4 = 4054043081U,
			// Token: 0x04000183 RID: 387
			ConfirmationMessageTestMessage = 2589164433U,
			// Token: 0x04000184 RID: 388
			ConfirmationMessageRepairMigration = 1531210305U,
			// Token: 0x04000185 RID: 389
			ErrorLocalServerIsNotMailboxServer = 1622178204U,
			// Token: 0x04000186 RID: 390
			ReservedString3 = 4054043088U,
			// Token: 0x04000187 RID: 391
			MessageFileDataSpecifiedAsNull = 4142544366U,
			// Token: 0x04000188 RID: 392
			ReservedString5 = 4054043082U
		}

		// Token: 0x0200004E RID: 78
		private enum ParamIDs
		{
			// Token: 0x0400018A RID: 394
			WarningUnableToRemoveMembers,
			// Token: 0x0400018B RID: 395
			SpnRegistrationFailed,
			// Token: 0x0400018C RID: 396
			ValidatingCalendar,
			// Token: 0x0400018D RID: 397
			ValidateRepairInvalidRevert,
			// Token: 0x0400018E RID: 398
			ValidateRepairMultipleUsers,
			// Token: 0x0400018F RID: 399
			ErrorMissingServerFqdn,
			// Token: 0x04000190 RID: 400
			ConfirmRepairSubscription,
			// Token: 0x04000191 RID: 401
			ErrorNoLocalOrganizationMailbox,
			// Token: 0x04000192 RID: 402
			ErrorGetRestrictionTableForFolderFailed,
			// Token: 0x04000193 RID: 403
			WarningUnableToAddMembers,
			// Token: 0x04000194 RID: 404
			WorkItemNotFoundException,
			// Token: 0x04000195 RID: 405
			ErrorSGOwningServerNotFound,
			// Token: 0x04000196 RID: 406
			ErrorUnableToRemove,
			// Token: 0x04000197 RID: 407
			ErrorStorageGroupNotUnique,
			// Token: 0x04000198 RID: 408
			ErrorDatabaseNotUnique,
			// Token: 0x04000199 RID: 409
			ConfirmRepairRemoveProperty,
			// Token: 0x0400019A RID: 410
			ConfirmRepairResumeIMAPSubscription,
			// Token: 0x0400019B RID: 411
			ErrorCannotUpdateExternalDirectoryObjectId,
			// Token: 0x0400019C RID: 412
			WarningUnableToAddOwners,
			// Token: 0x0400019D RID: 413
			ErrorServerNotFound,
			// Token: 0x0400019E RID: 414
			SetUpgradeWorkItemConfirmationMessage,
			// Token: 0x0400019F RID: 415
			ErrorInvalidObjectMissingCriticalProperty,
			// Token: 0x040001A0 RID: 416
			ErrorCannotReadDatabaseEvents,
			// Token: 0x040001A1 RID: 417
			WarningUnableToRemoveOwners,
			// Token: 0x040001A2 RID: 418
			ErrorStorageGroupNotFound,
			// Token: 0x040001A3 RID: 419
			ErrorServerNotUnique,
			// Token: 0x040001A4 RID: 420
			TryingToSubmitTestmessage,
			// Token: 0x040001A5 RID: 421
			ValidateRepairMissingReport,
			// Token: 0x040001A6 RID: 422
			ValidateRepairMissingSubscription,
			// Token: 0x040001A7 RID: 423
			ConfirmRepairUser,
			// Token: 0x040001A8 RID: 424
			InvalidTenantGuidError,
			// Token: 0x040001A9 RID: 425
			ErrorMaxThreadPoolThreads,
			// Token: 0x040001AA RID: 426
			ConfirmRepairUpdateProperty,
			// Token: 0x040001AB RID: 427
			ConfirmRepairUpdateCacheEntry,
			// Token: 0x040001AC RID: 428
			SubmittedSuccessfully,
			// Token: 0x040001AD RID: 429
			UsingDefaultDomainFromAD,
			// Token: 0x040001AE RID: 430
			ConfirmRepairRemoveReport,
			// Token: 0x040001AF RID: 431
			UnableToCreateFromMsg,
			// Token: 0x040001B0 RID: 432
			ErrorDatabaseNotFound,
			// Token: 0x040001B1 RID: 433
			ConfirmRepairRemoveFolder,
			// Token: 0x040001B2 RID: 434
			ErrorStartDateEqualGreaterThanEndDate,
			// Token: 0x040001B3 RID: 435
			ErrorRepairConvertStatus,
			// Token: 0x040001B4 RID: 436
			ValidateRepairMissingSubscriptionHandler,
			// Token: 0x040001B5 RID: 437
			ErrorDBOwningServerNotFound,
			// Token: 0x040001B6 RID: 438
			ErrorResultSizeOutOfRange,
			// Token: 0x040001B7 RID: 439
			InvalidTestMessageFileData,
			// Token: 0x040001B8 RID: 440
			ErrorRepairReverting,
			// Token: 0x040001B9 RID: 441
			ConfirmRepairRemoveUsers,
			// Token: 0x040001BA RID: 442
			ConfirmRepairBatch,
			// Token: 0x040001BB RID: 443
			ValidateRepairInvalidRevertJobType,
			// Token: 0x040001BC RID: 444
			NoExternalDirectoryObjectIdForRecipientId,
			// Token: 0x040001BD RID: 445
			ConfirmRepairRemoveStoreObject,
			// Token: 0x040001BE RID: 446
			UsingDefaultDomainFromRecipient,
			// Token: 0x040001BF RID: 447
			ErrorCannotReadDatabaseWatermarks,
			// Token: 0x040001C0 RID: 448
			WarnRepairRemovingUser,
			// Token: 0x040001C1 RID: 449
			InvalidStatusDetailError
		}
	}
}
