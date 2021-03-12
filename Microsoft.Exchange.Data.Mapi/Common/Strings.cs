using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x02000039 RID: 57
	internal static class Strings
	{
		// Token: 0x060001FD RID: 509 RVA: 0x0000CA8C File Offset: 0x0000AC8C
		static Strings()
		{
			Strings.stringIDs.Add(1413397944U, "ExceptionIdentityNull");
			Strings.stringIDs.Add(4167957496U, "ExceptionWriteReadOnlyData");
			Strings.stringIDs.Add(2515788448U, "SessionLimitExceptionError");
			Strings.stringIDs.Add(2090231513U, "ConstantNull");
			Strings.stringIDs.Add(773445177U, "ExceptionModifyIpmSubtree");
			Strings.stringIDs.Add(2760297639U, "ExceptionFormatNotSupported");
			Strings.stringIDs.Add(2613900068U, "ExceptionSessionInvalid");
			Strings.stringIDs.Add(1292191704U, "MapiNetworkErrorExceptionErrorSimple");
			Strings.stringIDs.Add(2484551555U, "ErrorMailboxStatisticsMailboxGuidEmpty");
			Strings.stringIDs.Add(3459524064U, "ExceptionIdentityTypeInvalid");
			Strings.stringIDs.Add(806229667U, "MapiAccessDeniedExceptionErrorSimple");
			Strings.stringIDs.Add(1655815296U, "ExceptionIdentityInvalid");
			Strings.stringIDs.Add(477740681U, "ExceptionConnectionNotConfigurated");
			Strings.stringIDs.Add(1136983657U, "ExceptionRawMapiEntryNull");
			Strings.stringIDs.Add(440370356U, "DatabaseUnavailableExceptionErrorSimple");
			Strings.stringIDs.Add(2921286064U, "ExceptionUnexpected");
			Strings.stringIDs.Add(1733928998U, "ExceptionModifyNonIpmSubtree");
			Strings.stringIDs.Add(461525725U, "ConstantNa");
			Strings.stringIDs.Add(695822400U, "ExceptionSessionNull");
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000CC44 File Offset: 0x0000AE44
		public static LocalizedString ExceptionIdentityNull
		{
			get
			{
				return new LocalizedString("ExceptionIdentityNull", "ExF9B53F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000CC62 File Offset: 0x0000AE62
		public static LocalizedString ExceptionWriteReadOnlyData
		{
			get
			{
				return new LocalizedString("ExceptionWriteReadOnlyData", "Ex4D7608", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000CC80 File Offset: 0x0000AE80
		public static LocalizedString ErrorMapiTableSetColumn(string id, string server)
		{
			return new LocalizedString("ErrorMapiTableSetColumn", "Ex6B87ED", false, true, Strings.ResourceManager, new object[]
			{
				id,
				server
			});
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000CCB4 File Offset: 0x0000AEB4
		public static LocalizedString ExceptionFindObject(string typeTarget, string root)
		{
			return new LocalizedString("ExceptionFindObject", "ExB6ECCE", false, true, Strings.ResourceManager, new object[]
			{
				typeTarget,
				root
			});
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000CCE8 File Offset: 0x0000AEE8
		public static LocalizedString PublicFolderNotFoundExceptionError(string folder)
		{
			return new LocalizedString("PublicFolderNotFoundExceptionError", "ExDBA130", false, true, Strings.ResourceManager, new object[]
			{
				folder
			});
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000CD18 File Offset: 0x0000AF18
		public static LocalizedString ExceptionNewObject(string identity)
		{
			return new LocalizedString("ExceptionNewObject", "ExE0BBF6", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000CD47 File Offset: 0x0000AF47
		public static LocalizedString SessionLimitExceptionError
		{
			get
			{
				return new LocalizedString("SessionLimitExceptionError", "ExF50FBF", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public static LocalizedString FolderAlreadyExistsExceptionError(string folder)
		{
			return new LocalizedString("FolderAlreadyExistsExceptionError", "ExBB1A32", false, true, Strings.ResourceManager, new object[]
			{
				folder
			});
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000CD98 File Offset: 0x0000AF98
		public static LocalizedString ErrorMapiTableSeekRow(string id, string server)
		{
			return new LocalizedString("ErrorMapiTableSeekRow", "ExA1789E", false, true, Strings.ResourceManager, new object[]
			{
				id,
				server
			});
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000CDCC File Offset: 0x0000AFCC
		public static LocalizedString ErrorGetAddressBookEntryIdFromLegacyDN(string legacyDN)
		{
			return new LocalizedString("ErrorGetAddressBookEntryIdFromLegacyDN", "ExFFCC96", false, true, Strings.ResourceManager, new object[]
			{
				legacyDN
			});
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000CDFC File Offset: 0x0000AFFC
		public static LocalizedString ExceptionSchemaInvalidCast(string type)
		{
			return new LocalizedString("ExceptionSchemaInvalidCast", "Ex2BE881", false, true, Strings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000CE2B File Offset: 0x0000B02B
		public static LocalizedString ConstantNull
		{
			get
			{
				return new LocalizedString("ConstantNull", "Ex5D99C9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000CE4C File Offset: 0x0000B04C
		public static LocalizedString PublicStoreLogonFailedExceptionError(string server)
		{
			return new LocalizedString("PublicStoreLogonFailedExceptionError", "Ex61A326", false, true, Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000CE7C File Offset: 0x0000B07C
		public static LocalizedString MapiCalculatedPropertyGettingExceptionError(string name, string details)
		{
			return new LocalizedString("MapiCalculatedPropertyGettingExceptionError", "Ex8806A2", false, true, Strings.ResourceManager, new object[]
			{
				name,
				details
			});
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000CEB0 File Offset: 0x0000B0B0
		public static LocalizedString ExceptionStartHierarchyReplication(string databaseId)
		{
			return new LocalizedString("ExceptionStartHierarchyReplication", "Ex622F86", false, true, Strings.ResourceManager, new object[]
			{
				databaseId
			});
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000CEDF File Offset: 0x0000B0DF
		public static LocalizedString ExceptionModifyIpmSubtree
		{
			get
			{
				return new LocalizedString("ExceptionModifyIpmSubtree", "Ex848D2C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000CF00 File Offset: 0x0000B100
		public static LocalizedString FailedToRefreshMailboxExceptionError(string exception, string mailbox)
		{
			return new LocalizedString("FailedToRefreshMailboxExceptionError", "ExF068FA", false, true, Strings.ResourceManager, new object[]
			{
				exception,
				mailbox
			});
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000CF33 File Offset: 0x0000B133
		public static LocalizedString ExceptionFormatNotSupported
		{
			get
			{
				return new LocalizedString("ExceptionFormatNotSupported", "ExA296C3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000CF54 File Offset: 0x0000B154
		public static LocalizedString ErrorFolderSeparatorInFolderName(string separator, string folderName)
		{
			return new LocalizedString("ErrorFolderSeparatorInFolderName", "Ex3210D1", false, true, Strings.ResourceManager, new object[]
			{
				separator,
				folderName
			});
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000CF88 File Offset: 0x0000B188
		public static LocalizedString ErrorMapiTableQueryRows(string id, string server)
		{
			return new LocalizedString("ErrorMapiTableQueryRows", "Ex99ECEA", false, true, Strings.ResourceManager, new object[]
			{
				id,
				server
			});
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000CFBC File Offset: 0x0000B1BC
		public static LocalizedString ExceptionReadObject(string type, string identity)
		{
			return new LocalizedString("ExceptionReadObject", "Ex0ED9FD", false, true, Strings.ResourceManager, new object[]
			{
				type,
				identity
			});
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000CFF0 File Offset: 0x0000B1F0
		public static LocalizedString ErrorGetPublicFolderAclTableMapiModifyTable(string id, string server)
		{
			return new LocalizedString("ErrorGetPublicFolderAclTableMapiModifyTable", "ExBF8905", false, true, Strings.ResourceManager, new object[]
			{
				id,
				server
			});
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000D024 File Offset: 0x0000B224
		public static LocalizedString ExceptionUnmatchedPropTag(string sourceTag, string targetTag)
		{
			return new LocalizedString("ExceptionUnmatchedPropTag", "Ex31BFA5", false, true, Strings.ResourceManager, new object[]
			{
				sourceTag,
				targetTag
			});
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000D057 File Offset: 0x0000B257
		public static LocalizedString ExceptionSessionInvalid
		{
			get
			{
				return new LocalizedString("ExceptionSessionInvalid", "ExAD9089", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000D078 File Offset: 0x0000B278
		public static LocalizedString ExceptionSetLocalReplicaAgeLimit(string databaseId)
		{
			return new LocalizedString("ExceptionSetLocalReplicaAgeLimit", "ExD7D31C", false, true, Strings.ResourceManager, new object[]
			{
				databaseId
			});
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000D0A8 File Offset: 0x0000B2A8
		public static LocalizedString ErrorRemovalPartialCompleted(string identity)
		{
			return new LocalizedString("ErrorRemovalPartialCompleted", "Ex70C83C", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000D0D8 File Offset: 0x0000B2D8
		public static LocalizedString ErrorDeletePropsProblem(string identity, string problemCount)
		{
			return new LocalizedString("ErrorDeletePropsProblem", "Ex962F9E", false, true, Strings.ResourceManager, new object[]
			{
				identity,
				problemCount
			});
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000D10B File Offset: 0x0000B30B
		public static LocalizedString MapiNetworkErrorExceptionErrorSimple
		{
			get
			{
				return new LocalizedString("MapiNetworkErrorExceptionErrorSimple", "Ex50A5C2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000D12C File Offset: 0x0000B32C
		public static LocalizedString ErrorSetPublicFolderAdminSecurityDescriptor(string folderId, string server)
		{
			return new LocalizedString("ErrorSetPublicFolderAdminSecurityDescriptor", "Ex220D05", false, true, Strings.ResourceManager, new object[]
			{
				folderId,
				server
			});
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000D15F File Offset: 0x0000B35F
		public static LocalizedString ErrorMailboxStatisticsMailboxGuidEmpty
		{
			get
			{
				return new LocalizedString("ErrorMailboxStatisticsMailboxGuidEmpty", "ExD4BBB4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000D17D File Offset: 0x0000B37D
		public static LocalizedString ExceptionIdentityTypeInvalid
		{
			get
			{
				return new LocalizedString("ExceptionIdentityTypeInvalid", "Ex6D9AAB", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000D19C File Offset: 0x0000B39C
		public static LocalizedString DatabaseUnavailableByIdentityExceptionError(string database, string server)
		{
			return new LocalizedString("DatabaseUnavailableByIdentityExceptionError", "ExD3EB33", false, true, Strings.ResourceManager, new object[]
			{
				database,
				server
			});
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
		public static LocalizedString ExceptionNotMultiValuedPropertyDefinition(string name)
		{
			return new LocalizedString("ExceptionNotMultiValuedPropertyDefinition", "Ex502CD6", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000D200 File Offset: 0x0000B400
		public static LocalizedString ErrorCannotUpdateIdentityFolderPath(string identity)
		{
			return new LocalizedString("ErrorCannotUpdateIdentityFolderPath", "Ex89B9D5", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000D230 File Offset: 0x0000B430
		public static LocalizedString ExceptionNoIdeaConvertPropType(string propType)
		{
			return new LocalizedString("ExceptionNoIdeaConvertPropType", "Ex86622A", false, true, Strings.ResourceManager, new object[]
			{
				propType
			});
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000D260 File Offset: 0x0000B460
		public static LocalizedString MailboxNotFoundExceptionError(string mailbox)
		{
			return new LocalizedString("MailboxNotFoundExceptionError", "ExE692E3", false, true, Strings.ResourceManager, new object[]
			{
				mailbox
			});
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000D290 File Offset: 0x0000B490
		public static LocalizedString ErrorPropProblem(string propTag, string propType, string sCode)
		{
			return new LocalizedString("ErrorPropProblem", "Ex334E31", false, true, Strings.ResourceManager, new object[]
			{
				propTag,
				propType,
				sCode
			});
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000D2C8 File Offset: 0x0000B4C8
		public static LocalizedString ExceptionObjectNotConsistent(string identity)
		{
			return new LocalizedString("ExceptionObjectNotConsistent", "Ex500682", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000D2F8 File Offset: 0x0000B4F8
		public static LocalizedString ExceptionGetDatabaseStatus(string exception)
		{
			return new LocalizedString("ExceptionGetDatabaseStatus", "Ex2FCB42", false, true, Strings.ResourceManager, new object[]
			{
				exception
			});
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000D328 File Offset: 0x0000B528
		public static LocalizedString MapiNetworkErrorExceptionError(string server)
		{
			return new LocalizedString("MapiNetworkErrorExceptionError", "ExB9132A", false, true, Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000D358 File Offset: 0x0000B558
		public static LocalizedString DatabaseUnavailableExceptionError(string server)
		{
			return new LocalizedString("DatabaseUnavailableExceptionError", "Ex1C84F8", false, true, Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000D388 File Offset: 0x0000B588
		public static LocalizedString ExceptionDeleteObject(string identiy)
		{
			return new LocalizedString("ExceptionDeleteObject", "ExF3BE50", false, true, Strings.ResourceManager, new object[]
			{
				identiy
			});
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
		public static LocalizedString ErrorGetMapiTableWithIdentityAndServer(string id, string server)
		{
			return new LocalizedString("ErrorGetMapiTableWithIdentityAndServer", "Ex8CEA10", false, true, Strings.ResourceManager, new object[]
			{
				id,
				server
			});
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000D3EC File Offset: 0x0000B5EC
		public static LocalizedString ExceptionCriticalPropTagMissing(string propTag)
		{
			return new LocalizedString("ExceptionCriticalPropTagMissing", "ExD89884", false, true, Strings.ResourceManager, new object[]
			{
				propTag
			});
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000D41C File Offset: 0x0000B61C
		public static LocalizedString ExceptionNoIdeaGenerateMultiValuedProperty(string type)
		{
			return new LocalizedString("ExceptionNoIdeaGenerateMultiValuedProperty", "Ex416625", false, true, Strings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000D44C File Offset: 0x0000B64C
		public static LocalizedString ErrorCannotUpdateIdentityLegacyDistinguishedName(string identity)
		{
			return new LocalizedString("ErrorCannotUpdateIdentityLegacyDistinguishedName", "ExFEDF53", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000D47C File Offset: 0x0000B67C
		public static LocalizedString ExceptionSetMailboxSecurityDescriptor(string databaseGuid, string mailboxGuid)
		{
			return new LocalizedString("ExceptionSetMailboxSecurityDescriptor", "ExF63558", false, true, Strings.ResourceManager, new object[]
			{
				databaseGuid,
				mailboxGuid
			});
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000D4B0 File Offset: 0x0000B6B0
		public static LocalizedString ErrorSetPublicFolderAdminSecurityDescriptorWithErrorCodes(string folderId, string server, string problems)
		{
			return new LocalizedString("ErrorSetPublicFolderAdminSecurityDescriptorWithErrorCodes", "Ex793F32", false, true, Strings.ResourceManager, new object[]
			{
				folderId,
				server,
				problems
			});
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000D4E8 File Offset: 0x0000B6E8
		public static LocalizedString ErrorByteArrayLength(string expected, string actual)
		{
			return new LocalizedString("ErrorByteArrayLength", "Ex8BA501", false, true, Strings.ResourceManager, new object[]
			{
				expected,
				actual
			});
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000D51C File Offset: 0x0000B71C
		public static LocalizedString MapiPackingExceptionError(string name, string value, string type, string isMultiValued, string propTag, string propType, string details)
		{
			return new LocalizedString("MapiPackingExceptionError", "Ex2CC01D", false, true, Strings.ResourceManager, new object[]
			{
				name,
				value,
				type,
				isMultiValued,
				propTag,
				propType,
				details
			});
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000D568 File Offset: 0x0000B768
		public static LocalizedString ErrorGetPublicFolderAdminSecurityDescriptor(string folderId, string server)
		{
			return new LocalizedString("ErrorGetPublicFolderAdminSecurityDescriptor", "Ex8AF0E2", false, true, Strings.ResourceManager, new object[]
			{
				folderId,
				server
			});
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000D59C File Offset: 0x0000B79C
		public static LocalizedString ExceptionObjectNotRemovable(string identity)
		{
			return new LocalizedString("ExceptionObjectNotRemovable", "ExC1198C", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000D5CB File Offset: 0x0000B7CB
		public static LocalizedString MapiAccessDeniedExceptionErrorSimple
		{
			get
			{
				return new LocalizedString("MapiAccessDeniedExceptionErrorSimple", "Ex28386E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000D5EC File Offset: 0x0000B7EC
		public static LocalizedString MailboxLogonFailedInDatabaseExceptionError(string mailbox, string database, string server)
		{
			return new LocalizedString("MailboxLogonFailedInDatabaseExceptionError", "ExC43ABC", false, true, Strings.ResourceManager, new object[]
			{
				mailbox,
				database,
				server
			});
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000D624 File Offset: 0x0000B824
		public static LocalizedString ErrorModifyMapiTableWithIdentityAndServer(string id, string server)
		{
			return new LocalizedString("ErrorModifyMapiTableWithIdentityAndServer", "ExA1634E", false, true, Strings.ResourceManager, new object[]
			{
				id,
				server
			});
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000D658 File Offset: 0x0000B858
		public static LocalizedString ErrorSetPropsProblem(string identity, string problemCount)
		{
			return new LocalizedString("ErrorSetPropsProblem", "Ex286F9C", false, true, Strings.ResourceManager, new object[]
			{
				identity,
				problemCount
			});
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000D68C File Offset: 0x0000B88C
		public static LocalizedString ExceptionGetMailboxSecurityDescriptor(string databaseGuid, string mailboxGuid)
		{
			return new LocalizedString("ExceptionGetMailboxSecurityDescriptor", "Ex325678", false, true, Strings.ResourceManager, new object[]
			{
				databaseGuid,
				mailboxGuid
			});
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000D6C0 File Offset: 0x0000B8C0
		public static LocalizedString ExceptionModifyFolder(string folderId)
		{
			return new LocalizedString("ExceptionModifyFolder", "ExC74894", false, true, Strings.ResourceManager, new object[]
			{
				folderId
			});
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000D6EF File Offset: 0x0000B8EF
		public static LocalizedString ExceptionIdentityInvalid
		{
			get
			{
				return new LocalizedString("ExceptionIdentityInvalid", "Ex4F7C9E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000D70D File Offset: 0x0000B90D
		public static LocalizedString ExceptionConnectionNotConfigurated
		{
			get
			{
				return new LocalizedString("ExceptionConnectionNotConfigurated", "Ex52E8B2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000D72C File Offset: 0x0000B92C
		public static LocalizedString MapiAccessDeniedExceptionError(string id)
		{
			return new LocalizedString("MapiAccessDeniedExceptionError", "Ex4B80CC", false, true, Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000D75C File Offset: 0x0000B95C
		public static LocalizedString MapiExceptionNoReplicaHereError(string type, string root)
		{
			return new LocalizedString("MapiExceptionNoReplicaHereError", "Ex0CA822", false, true, Strings.ResourceManager, new object[]
			{
				type,
				root
			});
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000D790 File Offset: 0x0000B990
		public static LocalizedString MapiExtractingExceptionError(string name, string propTag, string propType, string rawValue, string rawValueType, string type, string isMultiValued, string details)
		{
			return new LocalizedString("MapiExtractingExceptionError", "Ex87CB90", false, true, Strings.ResourceManager, new object[]
			{
				name,
				propTag,
				propType,
				rawValue,
				rawValueType,
				type,
				isMultiValued,
				details
			});
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000D7E0 File Offset: 0x0000B9E0
		public static LocalizedString ErrorCannotUpdateIdentityEntryId(string identity)
		{
			return new LocalizedString("ErrorCannotUpdateIdentityEntryId", "Ex1F0A53", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000D810 File Offset: 0x0000BA10
		public static LocalizedString MailboxNotFoundInDatabaseExceptionError(string mailbox, string database)
		{
			return new LocalizedString("MailboxNotFoundInDatabaseExceptionError", "ExAA6208", false, true, Strings.ResourceManager, new object[]
			{
				mailbox,
				database
			});
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000D844 File Offset: 0x0000BA44
		public static LocalizedString ExceptionDeleteMailbox(string database, string mailbox)
		{
			return new LocalizedString("ExceptionDeleteMailbox", "Ex05632B", false, true, Strings.ResourceManager, new object[]
			{
				database,
				mailbox
			});
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000D877 File Offset: 0x0000BA77
		public static LocalizedString ExceptionRawMapiEntryNull
		{
			get
			{
				return new LocalizedString("ExceptionRawMapiEntryNull", "Ex2B7298", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000D895 File Offset: 0x0000BA95
		public static LocalizedString DatabaseUnavailableExceptionErrorSimple
		{
			get
			{
				return new LocalizedString("DatabaseUnavailableExceptionErrorSimple", "ExF71825", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000D8B3 File Offset: 0x0000BAB3
		public static LocalizedString ExceptionUnexpected
		{
			get
			{
				return new LocalizedString("ExceptionUnexpected", "ExDB8AD0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000D8D4 File Offset: 0x0000BAD4
		public static LocalizedString ExceptionStartContentReplication(string databaseId, string folderId)
		{
			return new LocalizedString("ExceptionStartContentReplication", "Ex774B89", false, true, Strings.ResourceManager, new object[]
			{
				databaseId,
				folderId
			});
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000D908 File Offset: 0x0000BB08
		public static LocalizedString ErrorMandatoryPropertyMissing(string property)
		{
			return new LocalizedString("ErrorMandatoryPropertyMissing", "ExB9901F", false, true, Strings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000D938 File Offset: 0x0000BB38
		public static LocalizedString ExceptionSaveObject(string identity)
		{
			return new LocalizedString("ExceptionSaveObject", "Ex99F3B7", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000D968 File Offset: 0x0000BB68
		public static LocalizedString LogonFailedExceptionError(string message, string server)
		{
			return new LocalizedString("LogonFailedExceptionError", "ExE6399D", false, true, Strings.ResourceManager, new object[]
			{
				message,
				server
			});
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000D99C File Offset: 0x0000BB9C
		public static LocalizedString MapiCalculatedPropertySettingExceptionError(string name, string value, string details)
		{
			return new LocalizedString("MapiCalculatedPropertySettingExceptionError", "Ex87915A", false, true, Strings.ResourceManager, new object[]
			{
				name,
				value,
				details
			});
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000D9D3 File Offset: 0x0000BBD3
		public static LocalizedString ExceptionModifyNonIpmSubtree
		{
			get
			{
				return new LocalizedString("ExceptionModifyNonIpmSubtree", "Ex580166", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000D9F1 File Offset: 0x0000BBF1
		public static LocalizedString ConstantNa
		{
			get
			{
				return new LocalizedString("ConstantNa", "ExB2E702", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000DA10 File Offset: 0x0000BC10
		public static LocalizedString MailboxLogonFailedExceptionError(string mailbox, string server)
		{
			return new LocalizedString("MailboxLogonFailedExceptionError", "ExC7D380", false, true, Strings.ResourceManager, new object[]
			{
				mailbox,
				server
			});
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000DA43 File Offset: 0x0000BC43
		public static LocalizedString ExceptionSessionNull
		{
			get
			{
				return new LocalizedString("ExceptionSessionNull", "ExAC8234", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000DA64 File Offset: 0x0000BC64
		public static LocalizedString ErrorGetGetLegacyDNFromAddressBookEntryId(string entryId)
		{
			return new LocalizedString("ErrorGetGetLegacyDNFromAddressBookEntryId", "ExD29F1F", false, true, Strings.ResourceManager, new object[]
			{
				entryId
			});
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000DA94 File Offset: 0x0000BC94
		public static LocalizedString ExceptionObjectStateInvalid(string state)
		{
			return new LocalizedString("ExceptionObjectStateInvalid", "Ex290F60", false, true, Strings.ResourceManager, new object[]
			{
				state
			});
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000DAC4 File Offset: 0x0000BCC4
		public static LocalizedString ExceptionFailedToLetStorePickupMailboxChange(string mailbox, string mdb)
		{
			return new LocalizedString("ExceptionFailedToLetStorePickupMailboxChange", "Ex46AFD1", false, true, Strings.ResourceManager, new object[]
			{
				mailbox,
				mdb
			});
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000DAF7 File Offset: 0x0000BCF7
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000135 RID: 309
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(19);

		// Token: 0x04000136 RID: 310
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Data.Mapi.Common.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200003A RID: 58
		public enum IDs : uint
		{
			// Token: 0x04000138 RID: 312
			ExceptionIdentityNull = 1413397944U,
			// Token: 0x04000139 RID: 313
			ExceptionWriteReadOnlyData = 4167957496U,
			// Token: 0x0400013A RID: 314
			SessionLimitExceptionError = 2515788448U,
			// Token: 0x0400013B RID: 315
			ConstantNull = 2090231513U,
			// Token: 0x0400013C RID: 316
			ExceptionModifyIpmSubtree = 773445177U,
			// Token: 0x0400013D RID: 317
			ExceptionFormatNotSupported = 2760297639U,
			// Token: 0x0400013E RID: 318
			ExceptionSessionInvalid = 2613900068U,
			// Token: 0x0400013F RID: 319
			MapiNetworkErrorExceptionErrorSimple = 1292191704U,
			// Token: 0x04000140 RID: 320
			ErrorMailboxStatisticsMailboxGuidEmpty = 2484551555U,
			// Token: 0x04000141 RID: 321
			ExceptionIdentityTypeInvalid = 3459524064U,
			// Token: 0x04000142 RID: 322
			MapiAccessDeniedExceptionErrorSimple = 806229667U,
			// Token: 0x04000143 RID: 323
			ExceptionIdentityInvalid = 1655815296U,
			// Token: 0x04000144 RID: 324
			ExceptionConnectionNotConfigurated = 477740681U,
			// Token: 0x04000145 RID: 325
			ExceptionRawMapiEntryNull = 1136983657U,
			// Token: 0x04000146 RID: 326
			DatabaseUnavailableExceptionErrorSimple = 440370356U,
			// Token: 0x04000147 RID: 327
			ExceptionUnexpected = 2921286064U,
			// Token: 0x04000148 RID: 328
			ExceptionModifyNonIpmSubtree = 1733928998U,
			// Token: 0x04000149 RID: 329
			ConstantNa = 461525725U,
			// Token: 0x0400014A RID: 330
			ExceptionSessionNull = 695822400U
		}

		// Token: 0x0200003B RID: 59
		private enum ParamIDs
		{
			// Token: 0x0400014C RID: 332
			ErrorMapiTableSetColumn,
			// Token: 0x0400014D RID: 333
			ExceptionFindObject,
			// Token: 0x0400014E RID: 334
			PublicFolderNotFoundExceptionError,
			// Token: 0x0400014F RID: 335
			ExceptionNewObject,
			// Token: 0x04000150 RID: 336
			FolderAlreadyExistsExceptionError,
			// Token: 0x04000151 RID: 337
			ErrorMapiTableSeekRow,
			// Token: 0x04000152 RID: 338
			ErrorGetAddressBookEntryIdFromLegacyDN,
			// Token: 0x04000153 RID: 339
			ExceptionSchemaInvalidCast,
			// Token: 0x04000154 RID: 340
			PublicStoreLogonFailedExceptionError,
			// Token: 0x04000155 RID: 341
			MapiCalculatedPropertyGettingExceptionError,
			// Token: 0x04000156 RID: 342
			ExceptionStartHierarchyReplication,
			// Token: 0x04000157 RID: 343
			FailedToRefreshMailboxExceptionError,
			// Token: 0x04000158 RID: 344
			ErrorFolderSeparatorInFolderName,
			// Token: 0x04000159 RID: 345
			ErrorMapiTableQueryRows,
			// Token: 0x0400015A RID: 346
			ExceptionReadObject,
			// Token: 0x0400015B RID: 347
			ErrorGetPublicFolderAclTableMapiModifyTable,
			// Token: 0x0400015C RID: 348
			ExceptionUnmatchedPropTag,
			// Token: 0x0400015D RID: 349
			ExceptionSetLocalReplicaAgeLimit,
			// Token: 0x0400015E RID: 350
			ErrorRemovalPartialCompleted,
			// Token: 0x0400015F RID: 351
			ErrorDeletePropsProblem,
			// Token: 0x04000160 RID: 352
			ErrorSetPublicFolderAdminSecurityDescriptor,
			// Token: 0x04000161 RID: 353
			DatabaseUnavailableByIdentityExceptionError,
			// Token: 0x04000162 RID: 354
			ExceptionNotMultiValuedPropertyDefinition,
			// Token: 0x04000163 RID: 355
			ErrorCannotUpdateIdentityFolderPath,
			// Token: 0x04000164 RID: 356
			ExceptionNoIdeaConvertPropType,
			// Token: 0x04000165 RID: 357
			MailboxNotFoundExceptionError,
			// Token: 0x04000166 RID: 358
			ErrorPropProblem,
			// Token: 0x04000167 RID: 359
			ExceptionObjectNotConsistent,
			// Token: 0x04000168 RID: 360
			ExceptionGetDatabaseStatus,
			// Token: 0x04000169 RID: 361
			MapiNetworkErrorExceptionError,
			// Token: 0x0400016A RID: 362
			DatabaseUnavailableExceptionError,
			// Token: 0x0400016B RID: 363
			ExceptionDeleteObject,
			// Token: 0x0400016C RID: 364
			ErrorGetMapiTableWithIdentityAndServer,
			// Token: 0x0400016D RID: 365
			ExceptionCriticalPropTagMissing,
			// Token: 0x0400016E RID: 366
			ExceptionNoIdeaGenerateMultiValuedProperty,
			// Token: 0x0400016F RID: 367
			ErrorCannotUpdateIdentityLegacyDistinguishedName,
			// Token: 0x04000170 RID: 368
			ExceptionSetMailboxSecurityDescriptor,
			// Token: 0x04000171 RID: 369
			ErrorSetPublicFolderAdminSecurityDescriptorWithErrorCodes,
			// Token: 0x04000172 RID: 370
			ErrorByteArrayLength,
			// Token: 0x04000173 RID: 371
			MapiPackingExceptionError,
			// Token: 0x04000174 RID: 372
			ErrorGetPublicFolderAdminSecurityDescriptor,
			// Token: 0x04000175 RID: 373
			ExceptionObjectNotRemovable,
			// Token: 0x04000176 RID: 374
			MailboxLogonFailedInDatabaseExceptionError,
			// Token: 0x04000177 RID: 375
			ErrorModifyMapiTableWithIdentityAndServer,
			// Token: 0x04000178 RID: 376
			ErrorSetPropsProblem,
			// Token: 0x04000179 RID: 377
			ExceptionGetMailboxSecurityDescriptor,
			// Token: 0x0400017A RID: 378
			ExceptionModifyFolder,
			// Token: 0x0400017B RID: 379
			MapiAccessDeniedExceptionError,
			// Token: 0x0400017C RID: 380
			MapiExceptionNoReplicaHereError,
			// Token: 0x0400017D RID: 381
			MapiExtractingExceptionError,
			// Token: 0x0400017E RID: 382
			ErrorCannotUpdateIdentityEntryId,
			// Token: 0x0400017F RID: 383
			MailboxNotFoundInDatabaseExceptionError,
			// Token: 0x04000180 RID: 384
			ExceptionDeleteMailbox,
			// Token: 0x04000181 RID: 385
			ExceptionStartContentReplication,
			// Token: 0x04000182 RID: 386
			ErrorMandatoryPropertyMissing,
			// Token: 0x04000183 RID: 387
			ExceptionSaveObject,
			// Token: 0x04000184 RID: 388
			LogonFailedExceptionError,
			// Token: 0x04000185 RID: 389
			MapiCalculatedPropertySettingExceptionError,
			// Token: 0x04000186 RID: 390
			MailboxLogonFailedExceptionError,
			// Token: 0x04000187 RID: 391
			ErrorGetGetLegacyDNFromAddressBookEntryId,
			// Token: 0x04000188 RID: 392
			ExceptionObjectStateInvalid,
			// Token: 0x04000189 RID: 393
			ExceptionFailedToLetStorePickupMailboxChange
		}
	}
}
