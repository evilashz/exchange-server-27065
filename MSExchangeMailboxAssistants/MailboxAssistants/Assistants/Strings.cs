using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x02000135 RID: 309
	internal static class Strings
	{
		// Token: 0x06000C4F RID: 3151 RVA: 0x0004FA68 File Offset: 0x0004DC68
		static Strings()
		{
			Strings.stringIDs.Add(1670246961U, "ElcEmailToMeCcMe");
			Strings.stringIDs.Add(3607788283U, "mailTipsTenant");
			Strings.stringIDs.Add(3898628637U, "notifTypeChangedUpdate");
			Strings.stringIDs.Add(934453170U, "ElcEmailHardDeletedItems");
			Strings.stringIDs.Add(1495048183U, "notifTypeNewUpdate");
			Strings.stringIDs.Add(1744461765U, "descFAIAvailabilityCannotBeDetermined");
			Strings.stringIDs.Add(1770375896U, "oabGeneratorAssistantName");
			Strings.stringIDs.Add(1016266277U, "ElcEmailModifiedColumn");
			Strings.stringIDs.Add(2082164915U, "ElcEmailAdditionalTagColumn");
			Strings.stringIDs.Add(3734275464U, "CalendarInteropAssistantName");
			Strings.stringIDs.Add(1879708454U, "InferenceTrainingAssistantName");
			Strings.stringIDs.Add(95279870U, "notifTypeReminder");
			Strings.stringIDs.Add(1877001794U, "topNName");
			Strings.stringIDs.Add(855842099U, "descTransientErrorInRequest");
			Strings.stringIDs.Add(539335430U, "ElcEmailIntro");
			Strings.stringIDs.Add(3607350954U, "groupMailboxAssistantName");
			Strings.stringIDs.Add(3372243916U, "remindersAssistantName");
			Strings.stringIDs.Add(1613737558U, "GrammarGeneratorADException");
			Strings.stringIDs.Add(163635142U, "UnchangedPIN");
			Strings.stringIDs.Add(2547632703U, "descDisconnectedMailboxException");
			Strings.stringIDs.Add(4035688143U, "ShutdownCalled");
			Strings.stringIDs.Add(3307549972U, "notifTypeSummary");
			Strings.stringIDs.Add(1391575642U, "umPartnerMessageName");
			Strings.stringIDs.Add(1203309977U, "SplitPlanFoldersInvalidError");
			Strings.stringIDs.Add(3518211443U, "jeoName");
			Strings.stringIDs.Add(4134802168U, "ElcEmailSubject");
			Strings.stringIDs.Add(715744377U, "searchIndexRepairTimeBasedAssistant");
			Strings.stringIDs.Add(2561243001U, "peopleRelevanceAssistantName");
			Strings.stringIDs.Add(3467239576U, "notifAllDayEventDesc");
			Strings.stringIDs.Add(2572670974U, "ElcEmailFromColumn");
			Strings.stringIDs.Add(487410631U, "sharePointSignalStoreAssistantName");
			Strings.stringIDs.Add(1484655823U, "discoverySearchAssistantName");
			Strings.stringIDs.Add(1060070951U, "descCalendarSyncAssistantName");
			Strings.stringIDs.Add(651459436U, "pushNotificationAssistantName");
			Strings.stringIDs.Add(2943105749U, "umReportingAssistantName");
			Strings.stringIDs.Add(1996610701U, "descFreeBusyPublicFolderReplicaServerNotFound");
			Strings.stringIDs.Add(3416505838U, "directoryProcessorAssistantName");
			Strings.stringIDs.Add(711233414U, "ElcEmailFolderColumn");
			Strings.stringIDs.Add(3328664694U, "freebusyName");
			Strings.stringIDs.Add(414347382U, "approvalName");
			Strings.stringIDs.Add(3793807764U, "storeUrgentMaintenanceAssistantName");
			Strings.stringIDs.Add(859008014U, "storeScheduledIntegrityCheckAssistantName");
			Strings.stringIDs.Add(172990383U, "descSkipException");
			Strings.stringIDs.Add(554388309U, "recipientDLExpansionAssistantName");
			Strings.stringIDs.Add(769806998U, "descFreeBusyMaxRetriesReached");
			Strings.stringIDs.Add(3856328827U, "calnotifName");
			Strings.stringIDs.Add(2919188624U, "mailboxAssociationReplicationAssistantName");
			Strings.stringIDs.Add(752294172U, "notifTypeDeletedUpdate");
			Strings.stringIDs.Add(3060357127U, "ElcEmailDefaultTag");
			Strings.stringIDs.Add(3518494575U, "resName");
			Strings.stringIDs.Add(2499712017U, "IssuePublicFolderMoveRequestFailedError");
			Strings.stringIDs.Add(2451913696U, "descSkipExceptionFailedToLoadCalItem");
			Strings.stringIDs.Add(3486031153U, "calName");
			Strings.stringIDs.Add(1845178530U, "PeopleCentricTriageAssistantName");
			Strings.stringIDs.Add(1791545818U, "provisioningAssistantName");
			Strings.stringIDs.Add(1032088888U, "FailedToAcquireUseLicense");
			Strings.stringIDs.Add(372335526U, "FailedToReadIRMConfig");
			Strings.stringIDs.Add(1252626508U, "descTransientErrorInDelegates");
			Strings.stringIDs.Add(2422654799U, "descFreeBusyPublicFolderReplicaServerNotUsable");
			Strings.stringIDs.Add(2741658320U, "ElcEmailColumnTotal");
			Strings.stringIDs.Add(2032772234U, "descSharingPolicyAssistantName");
			Strings.stringIDs.Add(1087429672U, "ElcEmailItemsAffected");
			Strings.stringIDs.Add(3540686486U, "ElcEmailConversationExplanation");
			Strings.stringIDs.Add(2931777031U, "MoveInProgressError");
			Strings.stringIDs.Add(2395385945U, "descSkipExceptionFailedValidateCalItem");
			Strings.stringIDs.Add(2487450483U, "ElcEmailSoftDeletedItems");
			Strings.stringIDs.Add(93518037U, "elcEventName");
			Strings.stringIDs.Add(2288936341U, "PublicFolderMoveSuspendedError");
			Strings.stringIDs.Add(889744429U, "conversationsAssistantName");
			Strings.stringIDs.Add(1696885034U, "ElcEmailSubjectArchive");
			Strings.stringIDs.Add(2062651674U, "PublicFolderAssistantName");
			Strings.stringIDs.Add(3743219558U, "SiteMailboxAssistantName");
			Strings.stringIDs.Add(3042223409U, "DarTaskStoreTimeBasedAssistant");
			Strings.stringIDs.Add(2546519693U, "storeMaintenanceAssistantName");
			Strings.stringIDs.Add(429959798U, "ElcEmailFolderReport");
			Strings.stringIDs.Add(210323189U, "oofName");
			Strings.stringIDs.Add(697109800U, "descServiceOutOfMemory");
			Strings.stringIDs.Add(3275433870U, "ElcEmailSubjectColumn");
			Strings.stringIDs.Add(990733156U, "birthdayName");
			Strings.stringIDs.Add(1583752896U, "probeTimeBasedAssistant");
			Strings.stringIDs.Add(2332901205U, "descTransientErrorInCancellation");
			Strings.stringIDs.Add(2700274642U, "ElcEmailUnknownTag");
			Strings.stringIDs.Add(3132186513U, "IndexRepairQueryFailure");
			Strings.stringIDs.Add(334332329U, "ElcEmailNA");
			Strings.stringIDs.Add(2132209446U, "ElcEmailArchivedItems");
			Strings.stringIDs.Add(4038161348U, "descSkipExceptionFailedUpdateFromMfn");
			Strings.stringIDs.Add(3007313627U, "descTransientErrorInMfn");
			Strings.stringIDs.Add(1197808294U, "jeocName");
			Strings.stringIDs.Add(2180425345U, "mailboxProcessorAssistantName");
			Strings.stringIDs.Add(3392314348U, "storeDSMaintenanceAssistantName");
			Strings.stringIDs.Add(625647950U, "DarTaskStoreEventBasedAssistant");
			Strings.stringIDs.Add(4107256397U, "descTransientErrorInResponse");
			Strings.stringIDs.Add(2235130016U, "descMissingServerConfig");
			Strings.stringIDs.Add(2278191609U, "storeIntegrityCheckAssistantName");
			Strings.stringIDs.Add(3575203865U, "elcName");
			Strings.stringIDs.Add(1711506384U, "descTransientException");
			Strings.stringIDs.Add(1288993595U, "ElcEmailReceivedColumn");
			Strings.stringIDs.Add(812103732U, "inferenceDataCollectionAssistantName");
			Strings.stringIDs.Add(1793359514U, "mwiName");
			Strings.stringIDs.Add(4084691760U, "ElcEmailPolicyTagColumn");
			Strings.stringIDs.Add(1033153844U, "calRepairName");
			Strings.stringIDs.Add(229277916U, "descSharingFolderAssistantName");
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x0005029C File Offset: 0x0004E49C
		public static LocalizedString ElcEmailToMeCcMe
		{
			get
			{
				return new LocalizedString("ElcEmailToMeCcMe", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x000502BA File Offset: 0x0004E4BA
		public static LocalizedString mailTipsTenant
		{
			get
			{
				return new LocalizedString("mailTipsTenant", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x000502D8 File Offset: 0x0004E4D8
		public static LocalizedString descUnableToSaveFolderTagProperties(string folderName, string mailbox, string error)
		{
			return new LocalizedString("descUnableToSaveFolderTagProperties", "Ex429C3D", false, true, Strings.ResourceManager, new object[]
			{
				folderName,
				mailbox,
				error
			});
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x0005030F File Offset: 0x0004E50F
		public static LocalizedString notifTypeChangedUpdate
		{
			get
			{
				return new LocalizedString("notifTypeChangedUpdate", "Ex2EC909", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00050330 File Offset: 0x0004E530
		public static LocalizedString descExpiryDestNotProvisioned(string mailbox, string destFolderDN, string policyDN)
		{
			return new LocalizedString("descExpiryDestNotProvisioned", "ExE4AC43", false, true, Strings.ResourceManager, new object[]
			{
				mailbox,
				destFolderDN,
				policyDN
			});
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x00050367 File Offset: 0x0004E567
		public static LocalizedString ElcEmailHardDeletedItems
		{
			get
			{
				return new LocalizedString("ElcEmailHardDeletedItems", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x00050385 File Offset: 0x0004E585
		public static LocalizedString notifTypeNewUpdate
		{
			get
			{
				return new LocalizedString("notifTypeNewUpdate", "Ex0B3E04", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x000503A4 File Offset: 0x0004E5A4
		public static LocalizedString descInvalidCommentChange(string folderName)
		{
			return new LocalizedString("descInvalidCommentChange", "Ex236A0C", false, true, Strings.ResourceManager, new object[]
			{
				folderName
			});
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x000503D3 File Offset: 0x0004E5D3
		public static LocalizedString descFAIAvailabilityCannotBeDetermined
		{
			get
			{
				return new LocalizedString("descFAIAvailabilityCannotBeDetermined", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x000503F1 File Offset: 0x0004E5F1
		public static LocalizedString oabGeneratorAssistantName
		{
			get
			{
				return new LocalizedString("oabGeneratorAssistantName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x0005040F File Offset: 0x0004E60F
		public static LocalizedString ElcEmailModifiedColumn
		{
			get
			{
				return new LocalizedString("ElcEmailModifiedColumn", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x0005042D File Offset: 0x0004E62D
		public static LocalizedString ElcEmailAdditionalTagColumn
		{
			get
			{
				return new LocalizedString("ElcEmailAdditionalTagColumn", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0005044C File Offset: 0x0004E64C
		public static LocalizedString descFailedToCreateTempFolder(string mailbox, string database)
		{
			return new LocalizedString("descFailedToCreateTempFolder", "ExE3E5F8", false, true, Strings.ResourceManager, new object[]
			{
				mailbox,
				database
			});
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x0005047F File Offset: 0x0004E67F
		public static LocalizedString CalendarInteropAssistantName
		{
			get
			{
				return new LocalizedString("CalendarInteropAssistantName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x000504A0 File Offset: 0x0004E6A0
		public static LocalizedString descExpiryDestSameAsSource(string folderName, string mailbox, string policyName)
		{
			return new LocalizedString("descExpiryDestSameAsSource", "Ex26AF0B", false, true, Strings.ResourceManager, new object[]
			{
				folderName,
				mailbox,
				policyName
			});
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x000504D7 File Offset: 0x0004E6D7
		public static LocalizedString InferenceTrainingAssistantName
		{
			get
			{
				return new LocalizedString("InferenceTrainingAssistantName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x000504F8 File Offset: 0x0004E6F8
		public static LocalizedString descInvalidFolderUpdateOrgToDef(string folderName, string mailbox)
		{
			return new LocalizedString("descInvalidFolderUpdateOrgToDef", "Ex71C29F", false, true, Strings.ResourceManager, new object[]
			{
				folderName,
				mailbox
			});
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0005052C File Offset: 0x0004E72C
		public static LocalizedString descFolderNotEmpty(string folderName, string mailbox)
		{
			return new LocalizedString("descFolderNotEmpty", "Ex4E7401", false, true, Strings.ResourceManager, new object[]
			{
				folderName,
				mailbox
			});
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x00050560 File Offset: 0x0004E760
		public static LocalizedString UnexpectedMoveStateError(string moveState)
		{
			return new LocalizedString("UnexpectedMoveStateError", "", false, false, Strings.ResourceManager, new object[]
			{
				moveState
			});
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0005058F File Offset: 0x0004E78F
		public static LocalizedString notifTypeReminder
		{
			get
			{
				return new LocalizedString("notifTypeReminder", "Ex5EF976", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x000505B0 File Offset: 0x0004E7B0
		public static LocalizedString ErrorCorruptDiscoverySearchObject(string searchObject, string orgid)
		{
			return new LocalizedString("ErrorCorruptDiscoverySearchObject", "", false, false, Strings.ResourceManager, new object[]
			{
				searchObject,
				orgid
			});
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x000505E3 File Offset: 0x0004E7E3
		public static LocalizedString topNName
		{
			get
			{
				return new LocalizedString("topNName", "Ex9EEB38", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x00050604 File Offset: 0x0004E804
		public static LocalizedString IndexRepairUnexpectedRpcResultLength(int rowCount)
		{
			return new LocalizedString("IndexRepairUnexpectedRpcResultLength", "", false, false, Strings.ResourceManager, new object[]
			{
				rowCount
			});
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00050638 File Offset: 0x0004E838
		public static LocalizedString ElcEmailReportOverflow(string itemsReported)
		{
			return new LocalizedString("ElcEmailReportOverflow", "", false, false, Strings.ResourceManager, new object[]
			{
				itemsReported
			});
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x00050667 File Offset: 0x0004E867
		public static LocalizedString descTransientErrorInRequest
		{
			get
			{
				return new LocalizedString("descTransientErrorInRequest", "ExBFC422", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x00050685 File Offset: 0x0004E885
		public static LocalizedString ElcEmailIntro
		{
			get
			{
				return new LocalizedString("ElcEmailIntro", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x000506A3 File Offset: 0x0004E8A3
		public static LocalizedString groupMailboxAssistantName
		{
			get
			{
				return new LocalizedString("groupMailboxAssistantName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x000506C1 File Offset: 0x0004E8C1
		public static LocalizedString remindersAssistantName
		{
			get
			{
				return new LocalizedString("remindersAssistantName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x000506DF File Offset: 0x0004E8DF
		public static LocalizedString GrammarGeneratorADException
		{
			get
			{
				return new LocalizedString("GrammarGeneratorADException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x000506FD File Offset: 0x0004E8FD
		public static LocalizedString UnchangedPIN
		{
			get
			{
				return new LocalizedString("UnchangedPIN", "ExD23E87", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x0005071B File Offset: 0x0004E91B
		public static LocalizedString descDisconnectedMailboxException
		{
			get
			{
				return new LocalizedString("descDisconnectedMailboxException", "Ex2CBB79", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00050739 File Offset: 0x0004E939
		public static LocalizedString ShutdownCalled
		{
			get
			{
				return new LocalizedString("ShutdownCalled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x00050757 File Offset: 0x0004E957
		public static LocalizedString notifTypeSummary
		{
			get
			{
				return new LocalizedString("notifTypeSummary", "Ex59BC59", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00050778 File Offset: 0x0004E978
		public static LocalizedString descFreeBusyPublicFolderNotFound(string folderName)
		{
			return new LocalizedString("descFreeBusyPublicFolderNotFound", "Ex95D86E", false, true, Strings.ResourceManager, new object[]
			{
				folderName
			});
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x000507A7 File Offset: 0x0004E9A7
		public static LocalizedString umPartnerMessageName
		{
			get
			{
				return new LocalizedString("umPartnerMessageName", "Ex3F6AE7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x000507C5 File Offset: 0x0004E9C5
		public static LocalizedString SplitPlanFoldersInvalidError
		{
			get
			{
				return new LocalizedString("SplitPlanFoldersInvalidError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x000507E3 File Offset: 0x0004E9E3
		public static LocalizedString jeoName
		{
			get
			{
				return new LocalizedString("jeoName", "Ex9762C4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x00050801 File Offset: 0x0004EA01
		public static LocalizedString ElcEmailSubject
		{
			get
			{
				return new LocalizedString("ElcEmailSubject", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0005081F File Offset: 0x0004EA1F
		public static LocalizedString searchIndexRepairTimeBasedAssistant
		{
			get
			{
				return new LocalizedString("searchIndexRepairTimeBasedAssistant", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00050840 File Offset: 0x0004EA40
		public static LocalizedString NormalizationFailedError(string name)
		{
			return new LocalizedString("NormalizationFailedError", "", false, false, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00050870 File Offset: 0x0004EA70
		public static LocalizedString ProgressCheckTimeoutError(string orgName, string mailboxGuid)
		{
			return new LocalizedString("ProgressCheckTimeoutError", "", false, false, Strings.ResourceManager, new object[]
			{
				orgName,
				mailboxGuid
			});
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x000508A4 File Offset: 0x0004EAA4
		public static LocalizedString descInvalidFolderUpdateDefToOrg(string folderName, string mailbox)
		{
			return new LocalizedString("descInvalidFolderUpdateDefToOrg", "Ex115E3D", false, true, Strings.ResourceManager, new object[]
			{
				folderName,
				mailbox
			});
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x000508D8 File Offset: 0x0004EAD8
		public static LocalizedString descMissingAuditLogPath(string database)
		{
			return new LocalizedString("descMissingAuditLogPath", "ExE95353", false, true, Strings.ResourceManager, new object[]
			{
				database
			});
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x00050907 File Offset: 0x0004EB07
		public static LocalizedString peopleRelevanceAssistantName
		{
			get
			{
				return new LocalizedString("peopleRelevanceAssistantName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00050928 File Offset: 0x0004EB28
		public static LocalizedString ElcEmailArchiveInDays(string days)
		{
			return new LocalizedString("ElcEmailArchiveInDays", "", false, false, Strings.ResourceManager, new object[]
			{
				days
			});
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x00050957 File Offset: 0x0004EB57
		public static LocalizedString notifAllDayEventDesc
		{
			get
			{
				return new LocalizedString("notifAllDayEventDesc", "Ex9F6CAA", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00050978 File Offset: 0x0004EB78
		public static LocalizedString descADUserLookupFailure(string mailbox)
		{
			return new LocalizedString("descADUserLookupFailure", "Ex9168FF", false, true, Strings.ResourceManager, new object[]
			{
				mailbox
			});
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x000509A7 File Offset: 0x0004EBA7
		public static LocalizedString ElcEmailFromColumn
		{
			get
			{
				return new LocalizedString("ElcEmailFromColumn", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x000509C5 File Offset: 0x0004EBC5
		public static LocalizedString sharePointSignalStoreAssistantName
		{
			get
			{
				return new LocalizedString("sharePointSignalStoreAssistantName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x000509E3 File Offset: 0x0004EBE3
		public static LocalizedString discoverySearchAssistantName
		{
			get
			{
				return new LocalizedString("discoverySearchAssistantName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x00050A01 File Offset: 0x0004EC01
		public static LocalizedString descCalendarSyncAssistantName
		{
			get
			{
				return new LocalizedString("descCalendarSyncAssistantName", "ExA446F0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x00050A1F File Offset: 0x0004EC1F
		public static LocalizedString pushNotificationAssistantName
		{
			get
			{
				return new LocalizedString("pushNotificationAssistantName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00050A40 File Offset: 0x0004EC40
		public static LocalizedString TargetMailboxOutofQuotaError(string orgName, string mailboxGuid)
		{
			return new LocalizedString("TargetMailboxOutofQuotaError", "", false, false, Strings.ResourceManager, new object[]
			{
				orgName,
				mailboxGuid
			});
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00050A74 File Offset: 0x0004EC74
		public static LocalizedString descConfigureAuditLogFailed(string database)
		{
			return new LocalizedString("descConfigureAuditLogFailed", "Ex2221A6", false, true, Strings.ResourceManager, new object[]
			{
				database
			});
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x00050AA3 File Offset: 0x0004ECA3
		public static LocalizedString umReportingAssistantName
		{
			get
			{
				return new LocalizedString("umReportingAssistantName", "ExD85619", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00050AC4 File Offset: 0x0004ECC4
		public static LocalizedString descUMServerFailure(string server, LocalizedString partnerMsg, string mailbox)
		{
			return new LocalizedString("descUMServerFailure", "Ex12192C", false, true, Strings.ResourceManager, new object[]
			{
				server,
				partnerMsg,
				mailbox
			});
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00050B00 File Offset: 0x0004ED00
		public static LocalizedString ErrorDiscoveryHoldsCIIndexDisabledOnDatabase(string criteria, string userid)
		{
			return new LocalizedString("ErrorDiscoveryHoldsCIIndexDisabledOnDatabase", "", false, false, Strings.ResourceManager, new object[]
			{
				criteria,
				userid
			});
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x00050B33 File Offset: 0x0004ED33
		public static LocalizedString descFreeBusyPublicFolderReplicaServerNotFound
		{
			get
			{
				return new LocalizedString("descFreeBusyPublicFolderReplicaServerNotFound", "Ex8D3942", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x00050B51 File Offset: 0x0004ED51
		public static LocalizedString directoryProcessorAssistantName
		{
			get
			{
				return new LocalizedString("directoryProcessorAssistantName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x00050B6F File Offset: 0x0004ED6F
		public static LocalizedString ElcEmailFolderColumn
		{
			get
			{
				return new LocalizedString("ElcEmailFolderColumn", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x00050B8D File Offset: 0x0004ED8D
		public static LocalizedString freebusyName
		{
			get
			{
				return new LocalizedString("freebusyName", "ExE6748E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x00050BAB File Offset: 0x0004EDAB
		public static LocalizedString approvalName
		{
			get
			{
				return new LocalizedString("approvalName", "Ex99AE08", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x00050BC9 File Offset: 0x0004EDC9
		public static LocalizedString storeUrgentMaintenanceAssistantName
		{
			get
			{
				return new LocalizedString("storeUrgentMaintenanceAssistantName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x00050BE7 File Offset: 0x0004EDE7
		public static LocalizedString storeScheduledIntegrityCheckAssistantName
		{
			get
			{
				return new LocalizedString("storeScheduledIntegrityCheckAssistantName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00050C08 File Offset: 0x0004EE08
		public static LocalizedString descInvalidStoreObjectInstance(Type actualInstance)
		{
			return new LocalizedString("descInvalidStoreObjectInstance", "", false, false, Strings.ResourceManager, new object[]
			{
				actualInstance
			});
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00050C38 File Offset: 0x0004EE38
		public static LocalizedString descArchiveNotAvailable(string nameOfUser)
		{
			return new LocalizedString("descArchiveNotAvailable", "Ex2B43BB", false, true, Strings.ResourceManager, new object[]
			{
				nameOfUser
			});
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x00050C67 File Offset: 0x0004EE67
		public static LocalizedString descSkipException
		{
			get
			{
				return new LocalizedString("descSkipException", "ExE87C24", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x00050C85 File Offset: 0x0004EE85
		public static LocalizedString recipientDLExpansionAssistantName
		{
			get
			{
				return new LocalizedString("recipientDLExpansionAssistantName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00050CA4 File Offset: 0x0004EEA4
		public static LocalizedString ErrorUserMissingOrWithoutRBAC(string username)
		{
			return new LocalizedString("ErrorUserMissingOrWithoutRBAC", "", false, false, Strings.ResourceManager, new object[]
			{
				username
			});
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00050CD4 File Offset: 0x0004EED4
		public static LocalizedString descUMServerNotAvailable(LocalizedString partnerMsg, string mailbox)
		{
			return new LocalizedString("descUMServerNotAvailable", "Ex6F1A8B", false, true, Strings.ResourceManager, new object[]
			{
				partnerMsg,
				mailbox
			});
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x00050D0C File Offset: 0x0004EF0C
		public static LocalizedString descFreeBusyMaxRetriesReached
		{
			get
			{
				return new LocalizedString("descFreeBusyMaxRetriesReached", "ExE2AFA2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x00050D2A File Offset: 0x0004EF2A
		public static LocalizedString calnotifName
		{
			get
			{
				return new LocalizedString("calnotifName", "ExC8DEE0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x00050D48 File Offset: 0x0004EF48
		public static LocalizedString mailboxAssociationReplicationAssistantName
		{
			get
			{
				return new LocalizedString("mailboxAssociationReplicationAssistantName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x00050D66 File Offset: 0x0004EF66
		public static LocalizedString notifTypeDeletedUpdate
		{
			get
			{
				return new LocalizedString("notifTypeDeletedUpdate", "ExCFDC18", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00050D84 File Offset: 0x0004EF84
		public static LocalizedString descUMAllServersFailed(LocalizedString partnerMsg, string mailbox)
		{
			return new LocalizedString("descUMAllServersFailed", "ExEFE9E2", false, true, Strings.ResourceManager, new object[]
			{
				partnerMsg,
				mailbox
			});
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x00050DBC File Offset: 0x0004EFBC
		public static LocalizedString ElcEmailDefaultTag
		{
			get
			{
				return new LocalizedString("ElcEmailDefaultTag", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x00050DDA File Offset: 0x0004EFDA
		public static LocalizedString resName
		{
			get
			{
				return new LocalizedString("resName", "ExE6480F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00050DF8 File Offset: 0x0004EFF8
		public static LocalizedString notifCountOfEventsDesc(string number)
		{
			return new LocalizedString("notifCountOfEventsDesc", "ExE6E1C7", false, true, Strings.ResourceManager, new object[]
			{
				number
			});
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00050E28 File Offset: 0x0004F028
		public static LocalizedString descNullExpiryDestination(string mailbox, string policyName)
		{
			return new LocalizedString("descNullExpiryDestination", "ExA8852E", false, true, Strings.ResourceManager, new object[]
			{
				mailbox,
				policyName
			});
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x00050E5B File Offset: 0x0004F05B
		public static LocalizedString IssuePublicFolderMoveRequestFailedError
		{
			get
			{
				return new LocalizedString("IssuePublicFolderMoveRequestFailedError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00050E7C File Offset: 0x0004F07C
		public static LocalizedString descFailedToReadProbeResult(int stateAttribute, string lookingFor, string actualValue)
		{
			return new LocalizedString("descFailedToReadProbeResult", "", false, false, Strings.ResourceManager, new object[]
			{
				stateAttribute,
				lookingFor,
				actualValue
			});
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x00050EB8 File Offset: 0x0004F0B8
		public static LocalizedString descSkipExceptionFailedToLoadCalItem
		{
			get
			{
				return new LocalizedString("descSkipExceptionFailedToLoadCalItem", "Ex93E02B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x00050ED6 File Offset: 0x0004F0D6
		public static LocalizedString calName
		{
			get
			{
				return new LocalizedString("calName", "ExF2E23E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x00050EF4 File Offset: 0x0004F0F4
		public static LocalizedString PeopleCentricTriageAssistantName
		{
			get
			{
				return new LocalizedString("PeopleCentricTriageAssistantName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x00050F12 File Offset: 0x0004F112
		public static LocalizedString provisioningAssistantName
		{
			get
			{
				return new LocalizedString("provisioningAssistantName", "ExBA9598", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00050F30 File Offset: 0x0004F130
		public static LocalizedString descMissingParentFolder(object childFolderName, object parentFolderId)
		{
			return new LocalizedString("descMissingParentFolder", "Ex9600B9", false, true, Strings.ResourceManager, new object[]
			{
				childFolderName,
				parentFolderId
			});
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x00050F63 File Offset: 0x0004F163
		public static LocalizedString FailedToAcquireUseLicense
		{
			get
			{
				return new LocalizedString("FailedToAcquireUseLicense", "Ex07F6DC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x00050F81 File Offset: 0x0004F181
		public static LocalizedString FailedToReadIRMConfig
		{
			get
			{
				return new LocalizedString("FailedToReadIRMConfig", "ExD4787A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x00050F9F File Offset: 0x0004F19F
		public static LocalizedString descTransientErrorInDelegates
		{
			get
			{
				return new LocalizedString("descTransientErrorInDelegates", "Ex289B17", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x00050FBD File Offset: 0x0004F1BD
		public static LocalizedString descFreeBusyPublicFolderReplicaServerNotUsable
		{
			get
			{
				return new LocalizedString("descFreeBusyPublicFolderReplicaServerNotUsable", "ExD8DD42", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00050FDC File Offset: 0x0004F1DC
		public static LocalizedString ErrorDiscoverySearchTimeout(string timeout, string criteria, string userid)
		{
			return new LocalizedString("ErrorDiscoverySearchTimeout", "", false, false, Strings.ResourceManager, new object[]
			{
				timeout,
				criteria,
				userid
			});
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x00051013 File Offset: 0x0004F213
		public static LocalizedString ElcEmailColumnTotal
		{
			get
			{
				return new LocalizedString("ElcEmailColumnTotal", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x00051031 File Offset: 0x0004F231
		public static LocalizedString descSharingPolicyAssistantName
		{
			get
			{
				return new LocalizedString("descSharingPolicyAssistantName", "Ex7CF5BE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00051050 File Offset: 0x0004F250
		public static LocalizedString EDiscoveryMailboxAttachmentSizeTooLow(string name, string exception)
		{
			return new LocalizedString("EDiscoveryMailboxAttachmentSizeTooLow", "", false, false, Strings.ResourceManager, new object[]
			{
				name,
				exception
			});
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00051084 File Offset: 0x0004F284
		public static LocalizedString descELCEnforcerTooManyErrors(string mailbox, int maxErrors)
		{
			return new LocalizedString("descELCEnforcerTooManyErrors", "Ex4F2B20", false, true, Strings.ResourceManager, new object[]
			{
				mailbox,
				maxErrors
			});
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x000510BC File Offset: 0x0004F2BC
		public static LocalizedString ElcEmailItemsAffected
		{
			get
			{
				return new LocalizedString("ElcEmailItemsAffected", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x000510DA File Offset: 0x0004F2DA
		public static LocalizedString ElcEmailConversationExplanation
		{
			get
			{
				return new LocalizedString("ElcEmailConversationExplanation", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x000510F8 File Offset: 0x0004F2F8
		public static LocalizedString MoveInProgressError
		{
			get
			{
				return new LocalizedString("MoveInProgressError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00051118 File Offset: 0x0004F318
		public static LocalizedString UnableToGenerateValidPassword(string userName)
		{
			return new LocalizedString("UnableToGenerateValidPassword", "", false, false, Strings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x00051147 File Offset: 0x0004F347
		public static LocalizedString descSkipExceptionFailedValidateCalItem
		{
			get
			{
				return new LocalizedString("descSkipExceptionFailedValidateCalItem", "Ex69F921", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x00051165 File Offset: 0x0004F365
		public static LocalizedString ElcEmailSoftDeletedItems
		{
			get
			{
				return new LocalizedString("ElcEmailSoftDeletedItems", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x00051183 File Offset: 0x0004F383
		public static LocalizedString elcEventName
		{
			get
			{
				return new LocalizedString("elcEventName", "Ex8D9096", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x000511A4 File Offset: 0x0004F3A4
		public static LocalizedString PublicFolderMoveFailedError(string error)
		{
			return new LocalizedString("PublicFolderMoveFailedError", "", false, false, Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x000511D3 File Offset: 0x0004F3D3
		public static LocalizedString PublicFolderMoveSuspendedError
		{
			get
			{
				return new LocalizedString("PublicFolderMoveSuspendedError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x000511F4 File Offset: 0x0004F3F4
		public static LocalizedString descMissingFolderIdOnExpiryDest(string mailbox, string destFolderDN, string policyDN)
		{
			return new LocalizedString("descMissingFolderIdOnExpiryDest", "ExAB94AE", false, true, Strings.ResourceManager, new object[]
			{
				mailbox,
				destFolderDN,
				policyDN
			});
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0005122B File Offset: 0x0004F42B
		public static LocalizedString conversationsAssistantName
		{
			get
			{
				return new LocalizedString("conversationsAssistantName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00051249 File Offset: 0x0004F449
		public static LocalizedString ElcEmailSubjectArchive
		{
			get
			{
				return new LocalizedString("ElcEmailSubjectArchive", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00051268 File Offset: 0x0004F468
		public static LocalizedString EDiscoveryMailboxFull(string name, string exception)
		{
			return new LocalizedString("EDiscoveryMailboxFull", "", false, false, Strings.ResourceManager, new object[]
			{
				name,
				exception
			});
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0005129C File Offset: 0x0004F49C
		public static LocalizedString ElcEmailDeleteInDays(string days)
		{
			return new LocalizedString("ElcEmailDeleteInDays", "", false, false, Strings.ResourceManager, new object[]
			{
				days
			});
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x000512CB File Offset: 0x0004F4CB
		public static LocalizedString PublicFolderAssistantName
		{
			get
			{
				return new LocalizedString("PublicFolderAssistantName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x000512E9 File Offset: 0x0004F4E9
		public static LocalizedString SiteMailboxAssistantName
		{
			get
			{
				return new LocalizedString("SiteMailboxAssistantName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00051308 File Offset: 0x0004F508
		public static LocalizedString descFailedToResolveInMemoryCache(Guid mailboxInstance)
		{
			return new LocalizedString("descFailedToResolveInMemoryCache", "", false, false, Strings.ResourceManager, new object[]
			{
				mailboxInstance
			});
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0005133C File Offset: 0x0004F53C
		public static LocalizedString descSourceUnderExpiryDest(string folderName, string mailbox, string policyName, string sourcePath, string destPath)
		{
			return new LocalizedString("descSourceUnderExpiryDest", "Ex5529D7", false, true, Strings.ResourceManager, new object[]
			{
				folderName,
				mailbox,
				policyName,
				sourcePath,
				destPath
			});
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0005137C File Offset: 0x0004F57C
		public static LocalizedString DarTaskStoreTimeBasedAssistant
		{
			get
			{
				return new LocalizedString("DarTaskStoreTimeBasedAssistant", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x0005139A File Offset: 0x0004F59A
		public static LocalizedString storeMaintenanceAssistantName
		{
			get
			{
				return new LocalizedString("storeMaintenanceAssistantName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x000513B8 File Offset: 0x0004F5B8
		public static LocalizedString descAppendAuditLogFailed(string database)
		{
			return new LocalizedString("descAppendAuditLogFailed", "ExF0A2FD", false, true, Strings.ResourceManager, new object[]
			{
				database
			});
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x000513E7 File Offset: 0x0004F5E7
		public static LocalizedString ElcEmailFolderReport
		{
			get
			{
				return new LocalizedString("ElcEmailFolderReport", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00051408 File Offset: 0x0004F608
		public static LocalizedString EntryExcludedFromGrammar(string name)
		{
			return new LocalizedString("EntryExcludedFromGrammar", "", false, false, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x00051437 File Offset: 0x0004F637
		public static LocalizedString oofName
		{
			get
			{
				return new LocalizedString("oofName", "Ex56DA40", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x00051455 File Offset: 0x0004F655
		public static LocalizedString descServiceOutOfMemory
		{
			get
			{
				return new LocalizedString("descServiceOutOfMemory", "Ex043D03", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x00051473 File Offset: 0x0004F673
		public static LocalizedString ElcEmailSubjectColumn
		{
			get
			{
				return new LocalizedString("ElcEmailSubjectColumn", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00051494 File Offset: 0x0004F694
		public static LocalizedString descUnableToFetchOrganizationEhaMigrationSetting(string mailbox, string orgid)
		{
			return new LocalizedString("descUnableToFetchOrganizationEhaMigrationSetting", "", false, false, Strings.ResourceManager, new object[]
			{
				mailbox,
				orgid
			});
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x000514C8 File Offset: 0x0004F6C8
		public static LocalizedString CouldNotAddExchangeSnapIn(string snapInName)
		{
			return new LocalizedString("CouldNotAddExchangeSnapIn", "", false, false, Strings.ResourceManager, new object[]
			{
				snapInName
			});
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x000514F7 File Offset: 0x0004F6F7
		public static LocalizedString birthdayName
		{
			get
			{
				return new LocalizedString("birthdayName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x00051515 File Offset: 0x0004F715
		public static LocalizedString probeTimeBasedAssistant
		{
			get
			{
				return new LocalizedString("probeTimeBasedAssistant", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x00051533 File Offset: 0x0004F733
		public static LocalizedString descTransientErrorInCancellation
		{
			get
			{
				return new LocalizedString("descTransientErrorInCancellation", "Ex71F088", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x00051551 File Offset: 0x0004F751
		public static LocalizedString ElcEmailUnknownTag
		{
			get
			{
				return new LocalizedString("ElcEmailUnknownTag", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00051570 File Offset: 0x0004F770
		public static LocalizedString descInvalidFolderUpdateDefTypeChange(string folderName, int adFolderType, int mailboxFolderType, string mailbox)
		{
			return new LocalizedString("descInvalidFolderUpdateDefTypeChange", "Ex881730", false, true, Strings.ResourceManager, new object[]
			{
				folderName,
				adFolderType,
				mailboxFolderType,
				mailbox
			});
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x000515B5 File Offset: 0x0004F7B5
		public static LocalizedString IndexRepairQueryFailure
		{
			get
			{
				return new LocalizedString("IndexRepairQueryFailure", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x000515D3 File Offset: 0x0004F7D3
		public static LocalizedString ElcEmailNA
		{
			get
			{
				return new LocalizedString("ElcEmailNA", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x000515F1 File Offset: 0x0004F7F1
		public static LocalizedString ElcEmailArchivedItems
		{
			get
			{
				return new LocalizedString("ElcEmailArchivedItems", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x0005160F File Offset: 0x0004F80F
		public static LocalizedString descSkipExceptionFailedUpdateFromMfn
		{
			get
			{
				return new LocalizedString("descSkipExceptionFailedUpdateFromMfn", "Ex1756C5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0005162D File Offset: 0x0004F82D
		public static LocalizedString descTransientErrorInMfn
		{
			get
			{
				return new LocalizedString("descTransientErrorInMfn", "Ex130DD2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0005164B File Offset: 0x0004F84B
		public static LocalizedString jeocName
		{
			get
			{
				return new LocalizedString("jeocName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x00051669 File Offset: 0x0004F869
		public static LocalizedString mailboxProcessorAssistantName
		{
			get
			{
				return new LocalizedString("mailboxProcessorAssistantName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x00051687 File Offset: 0x0004F887
		public static LocalizedString storeDSMaintenanceAssistantName
		{
			get
			{
				return new LocalizedString("storeDSMaintenanceAssistantName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x000516A8 File Offset: 0x0004F8A8
		public static LocalizedString ErrorDiscoveryHoldsCIIndexNotRunning(string criteria, string userid)
		{
			return new LocalizedString("ErrorDiscoveryHoldsCIIndexNotRunning", "", false, false, Strings.ResourceManager, new object[]
			{
				criteria,
				userid
			});
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x000516DB File Offset: 0x0004F8DB
		public static LocalizedString DarTaskStoreEventBasedAssistant
		{
			get
			{
				return new LocalizedString("DarTaskStoreEventBasedAssistant", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x000516F9 File Offset: 0x0004F8F9
		public static LocalizedString descTransientErrorInResponse
		{
			get
			{
				return new LocalizedString("descTransientErrorInResponse", "ExAF8CA8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00051718 File Offset: 0x0004F918
		public static LocalizedString descFailedToCreateFolderHierarchy(string sourcefolder, string mailbox)
		{
			return new LocalizedString("descFailedToCreateFolderHierarchy", "Ex8293E3", false, true, Strings.ResourceManager, new object[]
			{
				sourcefolder,
				mailbox
			});
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0005174C File Offset: 0x0004F94C
		public static LocalizedString descUMPartnerMessage(string content, string status, string messageId)
		{
			return new LocalizedString("descUMPartnerMessage", "Ex5A6E95", false, true, Strings.ResourceManager, new object[]
			{
				content,
				status,
				messageId
			});
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00051784 File Offset: 0x0004F984
		public static LocalizedString descUnableToGetId(string nameOfItem, string mailbox)
		{
			return new LocalizedString("descUnableToGetId", "ExCD60A6", false, true, Strings.ResourceManager, new object[]
			{
				nameOfItem,
				mailbox
			});
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x000517B8 File Offset: 0x0004F9B8
		public static LocalizedString descCycleInPolicies(string mailbox, string policyName)
		{
			return new LocalizedString("descCycleInPolicies", "ExE6A529", false, true, Strings.ResourceManager, new object[]
			{
				mailbox,
				policyName
			});
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x000517EB File Offset: 0x0004F9EB
		public static LocalizedString descMissingServerConfig
		{
			get
			{
				return new LocalizedString("descMissingServerConfig", "Ex4F6FA8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x00051809 File Offset: 0x0004FA09
		public static LocalizedString storeIntegrityCheckAssistantName
		{
			get
			{
				return new LocalizedString("storeIntegrityCheckAssistantName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x00051827 File Offset: 0x0004FA27
		public static LocalizedString elcName
		{
			get
			{
				return new LocalizedString("elcName", "Ex73230A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x00051845 File Offset: 0x0004FA45
		public static LocalizedString descTransientException
		{
			get
			{
				return new LocalizedString("descTransientException", "ExBE7677", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x00051863 File Offset: 0x0004FA63
		public static LocalizedString ElcEmailReceivedColumn
		{
			get
			{
				return new LocalizedString("ElcEmailReceivedColumn", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x00051881 File Offset: 0x0004FA81
		public static LocalizedString inferenceDataCollectionAssistantName
		{
			get
			{
				return new LocalizedString("inferenceDataCollectionAssistantName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x000518A0 File Offset: 0x0004FAA0
		public static LocalizedString descFreeBusyPublishFailed(string exception)
		{
			return new LocalizedString("descFreeBusyPublishFailed", "Ex09D208", false, true, Strings.ResourceManager, new object[]
			{
				exception
			});
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x000518CF File Offset: 0x0004FACF
		public static LocalizedString mwiName
		{
			get
			{
				return new LocalizedString("mwiName", "ExB19544", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x000518F0 File Offset: 0x0004FAF0
		public static LocalizedString IndexRepairUnexpectedRpcResultRowLength(int rowCount)
		{
			return new LocalizedString("IndexRepairUnexpectedRpcResultRowLength", "", false, false, Strings.ResourceManager, new object[]
			{
				rowCount
			});
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x00051924 File Offset: 0x0004FB24
		public static LocalizedString ElcEmailPolicyTagColumn
		{
			get
			{
				return new LocalizedString("ElcEmailPolicyTagColumn", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x00051942 File Offset: 0x0004FB42
		public static LocalizedString calRepairName
		{
			get
			{
				return new LocalizedString("calRepairName", "Ex338260", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x00051960 File Offset: 0x0004FB60
		public static LocalizedString PartialStepCountExceededException(string partialStep, int limit)
		{
			return new LocalizedString("PartialStepCountExceededException", "", false, false, Strings.ResourceManager, new object[]
			{
				partialStep,
				limit
			});
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x00051998 File Offset: 0x0004FB98
		public static LocalizedString descSharingFolderAssistantName
		{
			get
			{
				return new LocalizedString("descSharingFolderAssistantName", "Ex956408", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x000519B8 File Offset: 0x0004FBB8
		public static LocalizedString descMissingFolderUrl(string sourcefolder, string mailbox)
		{
			return new LocalizedString("descMissingFolderUrl", "ExD4306D", false, true, Strings.ResourceManager, new object[]
			{
				sourcefolder,
				mailbox
			});
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x000519EB File Offset: 0x0004FBEB
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000793 RID: 1939
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(102);

		// Token: 0x04000794 RID: 1940
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.MailboxAssistants.Assistants.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000136 RID: 310
		public enum IDs : uint
		{
			// Token: 0x04000796 RID: 1942
			ElcEmailToMeCcMe = 1670246961U,
			// Token: 0x04000797 RID: 1943
			mailTipsTenant = 3607788283U,
			// Token: 0x04000798 RID: 1944
			notifTypeChangedUpdate = 3898628637U,
			// Token: 0x04000799 RID: 1945
			ElcEmailHardDeletedItems = 934453170U,
			// Token: 0x0400079A RID: 1946
			notifTypeNewUpdate = 1495048183U,
			// Token: 0x0400079B RID: 1947
			descFAIAvailabilityCannotBeDetermined = 1744461765U,
			// Token: 0x0400079C RID: 1948
			oabGeneratorAssistantName = 1770375896U,
			// Token: 0x0400079D RID: 1949
			ElcEmailModifiedColumn = 1016266277U,
			// Token: 0x0400079E RID: 1950
			ElcEmailAdditionalTagColumn = 2082164915U,
			// Token: 0x0400079F RID: 1951
			CalendarInteropAssistantName = 3734275464U,
			// Token: 0x040007A0 RID: 1952
			InferenceTrainingAssistantName = 1879708454U,
			// Token: 0x040007A1 RID: 1953
			notifTypeReminder = 95279870U,
			// Token: 0x040007A2 RID: 1954
			topNName = 1877001794U,
			// Token: 0x040007A3 RID: 1955
			descTransientErrorInRequest = 855842099U,
			// Token: 0x040007A4 RID: 1956
			ElcEmailIntro = 539335430U,
			// Token: 0x040007A5 RID: 1957
			groupMailboxAssistantName = 3607350954U,
			// Token: 0x040007A6 RID: 1958
			remindersAssistantName = 3372243916U,
			// Token: 0x040007A7 RID: 1959
			GrammarGeneratorADException = 1613737558U,
			// Token: 0x040007A8 RID: 1960
			UnchangedPIN = 163635142U,
			// Token: 0x040007A9 RID: 1961
			descDisconnectedMailboxException = 2547632703U,
			// Token: 0x040007AA RID: 1962
			ShutdownCalled = 4035688143U,
			// Token: 0x040007AB RID: 1963
			notifTypeSummary = 3307549972U,
			// Token: 0x040007AC RID: 1964
			umPartnerMessageName = 1391575642U,
			// Token: 0x040007AD RID: 1965
			SplitPlanFoldersInvalidError = 1203309977U,
			// Token: 0x040007AE RID: 1966
			jeoName = 3518211443U,
			// Token: 0x040007AF RID: 1967
			ElcEmailSubject = 4134802168U,
			// Token: 0x040007B0 RID: 1968
			searchIndexRepairTimeBasedAssistant = 715744377U,
			// Token: 0x040007B1 RID: 1969
			peopleRelevanceAssistantName = 2561243001U,
			// Token: 0x040007B2 RID: 1970
			notifAllDayEventDesc = 3467239576U,
			// Token: 0x040007B3 RID: 1971
			ElcEmailFromColumn = 2572670974U,
			// Token: 0x040007B4 RID: 1972
			sharePointSignalStoreAssistantName = 487410631U,
			// Token: 0x040007B5 RID: 1973
			discoverySearchAssistantName = 1484655823U,
			// Token: 0x040007B6 RID: 1974
			descCalendarSyncAssistantName = 1060070951U,
			// Token: 0x040007B7 RID: 1975
			pushNotificationAssistantName = 651459436U,
			// Token: 0x040007B8 RID: 1976
			umReportingAssistantName = 2943105749U,
			// Token: 0x040007B9 RID: 1977
			descFreeBusyPublicFolderReplicaServerNotFound = 1996610701U,
			// Token: 0x040007BA RID: 1978
			directoryProcessorAssistantName = 3416505838U,
			// Token: 0x040007BB RID: 1979
			ElcEmailFolderColumn = 711233414U,
			// Token: 0x040007BC RID: 1980
			freebusyName = 3328664694U,
			// Token: 0x040007BD RID: 1981
			approvalName = 414347382U,
			// Token: 0x040007BE RID: 1982
			storeUrgentMaintenanceAssistantName = 3793807764U,
			// Token: 0x040007BF RID: 1983
			storeScheduledIntegrityCheckAssistantName = 859008014U,
			// Token: 0x040007C0 RID: 1984
			descSkipException = 172990383U,
			// Token: 0x040007C1 RID: 1985
			recipientDLExpansionAssistantName = 554388309U,
			// Token: 0x040007C2 RID: 1986
			descFreeBusyMaxRetriesReached = 769806998U,
			// Token: 0x040007C3 RID: 1987
			calnotifName = 3856328827U,
			// Token: 0x040007C4 RID: 1988
			mailboxAssociationReplicationAssistantName = 2919188624U,
			// Token: 0x040007C5 RID: 1989
			notifTypeDeletedUpdate = 752294172U,
			// Token: 0x040007C6 RID: 1990
			ElcEmailDefaultTag = 3060357127U,
			// Token: 0x040007C7 RID: 1991
			resName = 3518494575U,
			// Token: 0x040007C8 RID: 1992
			IssuePublicFolderMoveRequestFailedError = 2499712017U,
			// Token: 0x040007C9 RID: 1993
			descSkipExceptionFailedToLoadCalItem = 2451913696U,
			// Token: 0x040007CA RID: 1994
			calName = 3486031153U,
			// Token: 0x040007CB RID: 1995
			PeopleCentricTriageAssistantName = 1845178530U,
			// Token: 0x040007CC RID: 1996
			provisioningAssistantName = 1791545818U,
			// Token: 0x040007CD RID: 1997
			FailedToAcquireUseLicense = 1032088888U,
			// Token: 0x040007CE RID: 1998
			FailedToReadIRMConfig = 372335526U,
			// Token: 0x040007CF RID: 1999
			descTransientErrorInDelegates = 1252626508U,
			// Token: 0x040007D0 RID: 2000
			descFreeBusyPublicFolderReplicaServerNotUsable = 2422654799U,
			// Token: 0x040007D1 RID: 2001
			ElcEmailColumnTotal = 2741658320U,
			// Token: 0x040007D2 RID: 2002
			descSharingPolicyAssistantName = 2032772234U,
			// Token: 0x040007D3 RID: 2003
			ElcEmailItemsAffected = 1087429672U,
			// Token: 0x040007D4 RID: 2004
			ElcEmailConversationExplanation = 3540686486U,
			// Token: 0x040007D5 RID: 2005
			MoveInProgressError = 2931777031U,
			// Token: 0x040007D6 RID: 2006
			descSkipExceptionFailedValidateCalItem = 2395385945U,
			// Token: 0x040007D7 RID: 2007
			ElcEmailSoftDeletedItems = 2487450483U,
			// Token: 0x040007D8 RID: 2008
			elcEventName = 93518037U,
			// Token: 0x040007D9 RID: 2009
			PublicFolderMoveSuspendedError = 2288936341U,
			// Token: 0x040007DA RID: 2010
			conversationsAssistantName = 889744429U,
			// Token: 0x040007DB RID: 2011
			ElcEmailSubjectArchive = 1696885034U,
			// Token: 0x040007DC RID: 2012
			PublicFolderAssistantName = 2062651674U,
			// Token: 0x040007DD RID: 2013
			SiteMailboxAssistantName = 3743219558U,
			// Token: 0x040007DE RID: 2014
			DarTaskStoreTimeBasedAssistant = 3042223409U,
			// Token: 0x040007DF RID: 2015
			storeMaintenanceAssistantName = 2546519693U,
			// Token: 0x040007E0 RID: 2016
			ElcEmailFolderReport = 429959798U,
			// Token: 0x040007E1 RID: 2017
			oofName = 210323189U,
			// Token: 0x040007E2 RID: 2018
			descServiceOutOfMemory = 697109800U,
			// Token: 0x040007E3 RID: 2019
			ElcEmailSubjectColumn = 3275433870U,
			// Token: 0x040007E4 RID: 2020
			birthdayName = 990733156U,
			// Token: 0x040007E5 RID: 2021
			probeTimeBasedAssistant = 1583752896U,
			// Token: 0x040007E6 RID: 2022
			descTransientErrorInCancellation = 2332901205U,
			// Token: 0x040007E7 RID: 2023
			ElcEmailUnknownTag = 2700274642U,
			// Token: 0x040007E8 RID: 2024
			IndexRepairQueryFailure = 3132186513U,
			// Token: 0x040007E9 RID: 2025
			ElcEmailNA = 334332329U,
			// Token: 0x040007EA RID: 2026
			ElcEmailArchivedItems = 2132209446U,
			// Token: 0x040007EB RID: 2027
			descSkipExceptionFailedUpdateFromMfn = 4038161348U,
			// Token: 0x040007EC RID: 2028
			descTransientErrorInMfn = 3007313627U,
			// Token: 0x040007ED RID: 2029
			jeocName = 1197808294U,
			// Token: 0x040007EE RID: 2030
			mailboxProcessorAssistantName = 2180425345U,
			// Token: 0x040007EF RID: 2031
			storeDSMaintenanceAssistantName = 3392314348U,
			// Token: 0x040007F0 RID: 2032
			DarTaskStoreEventBasedAssistant = 625647950U,
			// Token: 0x040007F1 RID: 2033
			descTransientErrorInResponse = 4107256397U,
			// Token: 0x040007F2 RID: 2034
			descMissingServerConfig = 2235130016U,
			// Token: 0x040007F3 RID: 2035
			storeIntegrityCheckAssistantName = 2278191609U,
			// Token: 0x040007F4 RID: 2036
			elcName = 3575203865U,
			// Token: 0x040007F5 RID: 2037
			descTransientException = 1711506384U,
			// Token: 0x040007F6 RID: 2038
			ElcEmailReceivedColumn = 1288993595U,
			// Token: 0x040007F7 RID: 2039
			inferenceDataCollectionAssistantName = 812103732U,
			// Token: 0x040007F8 RID: 2040
			mwiName = 1793359514U,
			// Token: 0x040007F9 RID: 2041
			ElcEmailPolicyTagColumn = 4084691760U,
			// Token: 0x040007FA RID: 2042
			calRepairName = 1033153844U,
			// Token: 0x040007FB RID: 2043
			descSharingFolderAssistantName = 229277916U
		}

		// Token: 0x02000137 RID: 311
		private enum ParamIDs
		{
			// Token: 0x040007FD RID: 2045
			descUnableToSaveFolderTagProperties,
			// Token: 0x040007FE RID: 2046
			descExpiryDestNotProvisioned,
			// Token: 0x040007FF RID: 2047
			descInvalidCommentChange,
			// Token: 0x04000800 RID: 2048
			descFailedToCreateTempFolder,
			// Token: 0x04000801 RID: 2049
			descExpiryDestSameAsSource,
			// Token: 0x04000802 RID: 2050
			descInvalidFolderUpdateOrgToDef,
			// Token: 0x04000803 RID: 2051
			descFolderNotEmpty,
			// Token: 0x04000804 RID: 2052
			UnexpectedMoveStateError,
			// Token: 0x04000805 RID: 2053
			ErrorCorruptDiscoverySearchObject,
			// Token: 0x04000806 RID: 2054
			IndexRepairUnexpectedRpcResultLength,
			// Token: 0x04000807 RID: 2055
			ElcEmailReportOverflow,
			// Token: 0x04000808 RID: 2056
			descFreeBusyPublicFolderNotFound,
			// Token: 0x04000809 RID: 2057
			NormalizationFailedError,
			// Token: 0x0400080A RID: 2058
			ProgressCheckTimeoutError,
			// Token: 0x0400080B RID: 2059
			descInvalidFolderUpdateDefToOrg,
			// Token: 0x0400080C RID: 2060
			descMissingAuditLogPath,
			// Token: 0x0400080D RID: 2061
			ElcEmailArchiveInDays,
			// Token: 0x0400080E RID: 2062
			descADUserLookupFailure,
			// Token: 0x0400080F RID: 2063
			TargetMailboxOutofQuotaError,
			// Token: 0x04000810 RID: 2064
			descConfigureAuditLogFailed,
			// Token: 0x04000811 RID: 2065
			descUMServerFailure,
			// Token: 0x04000812 RID: 2066
			ErrorDiscoveryHoldsCIIndexDisabledOnDatabase,
			// Token: 0x04000813 RID: 2067
			descInvalidStoreObjectInstance,
			// Token: 0x04000814 RID: 2068
			descArchiveNotAvailable,
			// Token: 0x04000815 RID: 2069
			ErrorUserMissingOrWithoutRBAC,
			// Token: 0x04000816 RID: 2070
			descUMServerNotAvailable,
			// Token: 0x04000817 RID: 2071
			descUMAllServersFailed,
			// Token: 0x04000818 RID: 2072
			notifCountOfEventsDesc,
			// Token: 0x04000819 RID: 2073
			descNullExpiryDestination,
			// Token: 0x0400081A RID: 2074
			descFailedToReadProbeResult,
			// Token: 0x0400081B RID: 2075
			descMissingParentFolder,
			// Token: 0x0400081C RID: 2076
			ErrorDiscoverySearchTimeout,
			// Token: 0x0400081D RID: 2077
			EDiscoveryMailboxAttachmentSizeTooLow,
			// Token: 0x0400081E RID: 2078
			descELCEnforcerTooManyErrors,
			// Token: 0x0400081F RID: 2079
			UnableToGenerateValidPassword,
			// Token: 0x04000820 RID: 2080
			PublicFolderMoveFailedError,
			// Token: 0x04000821 RID: 2081
			descMissingFolderIdOnExpiryDest,
			// Token: 0x04000822 RID: 2082
			EDiscoveryMailboxFull,
			// Token: 0x04000823 RID: 2083
			ElcEmailDeleteInDays,
			// Token: 0x04000824 RID: 2084
			descFailedToResolveInMemoryCache,
			// Token: 0x04000825 RID: 2085
			descSourceUnderExpiryDest,
			// Token: 0x04000826 RID: 2086
			descAppendAuditLogFailed,
			// Token: 0x04000827 RID: 2087
			EntryExcludedFromGrammar,
			// Token: 0x04000828 RID: 2088
			descUnableToFetchOrganizationEhaMigrationSetting,
			// Token: 0x04000829 RID: 2089
			CouldNotAddExchangeSnapIn,
			// Token: 0x0400082A RID: 2090
			descInvalidFolderUpdateDefTypeChange,
			// Token: 0x0400082B RID: 2091
			ErrorDiscoveryHoldsCIIndexNotRunning,
			// Token: 0x0400082C RID: 2092
			descFailedToCreateFolderHierarchy,
			// Token: 0x0400082D RID: 2093
			descUMPartnerMessage,
			// Token: 0x0400082E RID: 2094
			descUnableToGetId,
			// Token: 0x0400082F RID: 2095
			descCycleInPolicies,
			// Token: 0x04000830 RID: 2096
			descFreeBusyPublishFailed,
			// Token: 0x04000831 RID: 2097
			IndexRepairUnexpectedRpcResultRowLength,
			// Token: 0x04000832 RID: 2098
			PartialStepCountExceededException,
			// Token: 0x04000833 RID: 2099
			descMissingFolderUrl
		}
	}
}
