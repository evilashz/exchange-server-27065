using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x02000002 RID: 2
	internal static class Strings
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		static Strings()
		{
			Strings.stringIDs.Add(1416971879U, "DatacenterSecretIsMissingOrInvalid");
			Strings.stringIDs.Add(358167539U, "InvalidConfigurationMissingLinkedInAccessTokenEndpoint");
			Strings.stringIDs.Add(507132958U, "FailedToCreateAuditEwsConnection");
			Strings.stringIDs.Add(425380821U, "UserPhotoStoreNotFound");
			Strings.stringIDs.Add(1386497625U, "ErrorReasonNoMailboxInHosts");
			Strings.stringIDs.Add(2430608892U, "ErrorReasonIgnoreCaseWithoutRegExInEntitiesRules");
			Strings.stringIDs.Add(2258791064U, "ErrorReasonManifestSchemaUnknown");
			Strings.stringIDs.Add(1262244671U, "ErrorCannotDisableMandatoryExtension");
			Strings.stringIDs.Add(1644547849U, "InvalidConfigurationFacebookAppId");
			Strings.stringIDs.Add(1736107127U, "WelcomeMailSubject");
			Strings.stringIDs.Add(3597849903U, "ErrorExtensionAlreadyInstalled");
			Strings.stringIDs.Add(3654277798U, "ErrorReasonDefaultVersionIsInvalid");
			Strings.stringIDs.Add(2929113449U, "InvalidConfigurationMissingLinkedInConnectionsEndpoint");
			Strings.stringIDs.Add(1132961834U, "ErrorExtensionVersionInvalid");
			Strings.stringIDs.Add(3473955170U, "ErrorReasonItemTypeInvalid");
			Strings.stringIDs.Add(1902437266U, "FailureReasonSourceLocationTagMissing");
			Strings.stringIDs.Add(1272553825U, "ErrorExtensionWithIdAlreadyInstalledForOrg");
			Strings.stringIDs.Add(809039298U, "InvalidConfigurationMissingFacebookAppSecret");
			Strings.stringIDs.Add(1528417217U, "FailedToReadWebProxyConfigurationFromAD");
			Strings.stringIDs.Add(693971404U, "UserPhotoNotFound");
			Strings.stringIDs.Add(3286161403U, "InvalidConfigurationMissingLinkedInAppId");
			Strings.stringIDs.Add(224007719U, "PhotoHasBeenDeleted");
			Strings.stringIDs.Add(994301062U, "FailedToAccessAuditLogWithInnerException");
			Strings.stringIDs.Add(980865226U, "InvalidConfigurationMissingLinkedInRequestTokenEndpoint");
			Strings.stringIDs.Add(895214716U, "ErrorExtensionAlreadyInstalledForOrg");
			Strings.stringIDs.Add(2103946832U, "AuditLogAccessDenied");
			Strings.stringIDs.Add(2566788983U, "ErrorReasonItemTypeAllTypes");
			Strings.stringIDs.Add(1057073101U, "ErrorMasterTableSaveConflict");
			Strings.stringIDs.Add(3960918573U, "InvalidConfigurationMissingFacebookAppId");
			Strings.stringIDs.Add(3353268377U, "ErrorCantOverwriteDefaultExtension");
			Strings.stringIDs.Add(4017558222U, "ErrorReasonOnlySelectedEntitiesInRestricted");
			Strings.stringIDs.Add(1445064246U, "UserPhotoAccessDenied");
			Strings.stringIDs.Add(3896524780U, "ErrorReasonItemIsRuleAttributesNotValidForEdit");
			Strings.stringIDs.Add(1303673802U, "ErrorReasonNoRegexRuleInRestricted");
			Strings.stringIDs.Add(2340791895U, "ErrorManifestSignatureInvalidExtension");
			Strings.stringIDs.Add(921746356U, "InvalidConfigurationMissingLinkedInAppSecret");
			Strings.stringIDs.Add(103323614U, "ErrorReasonRegExNameAndValueRequiredInEntitiesRules");
			Strings.stringIDs.Add(531489075U, "SecureStringParameter");
			Strings.stringIDs.Add(3364919108U, "InvalidConfigurationFacebookAppSecret");
			Strings.stringIDs.Add(163140049U, "InvalidConfigurationMissingFacebookGraphTokenEndpoint");
			Strings.stringIDs.Add(3689335943U, "InvalidConfigurationLinkedInAppId");
			Strings.stringIDs.Add(3369915033U, "ErrorOrgLevelAppMustBeSiteLicense");
			Strings.stringIDs.Add(2727153851U, "ErrorMarketplaceWebServicesUnavailable");
			Strings.stringIDs.Add(2267978298U, "InvalidConfigurationMissingFacebookGraphApiEndpoint");
			Strings.stringIDs.Add(3571146374U, "InvalidConfigurationLinkedInAppSecret");
			Strings.stringIDs.Add(3104695745U, "InvalidConfigurationMissingFacebookAuthorizationEndpoint");
			Strings.stringIDs.Add(3368339595U, "InvalidConfigurationMissingLinkedInProfileEndpoint");
			Strings.stringIDs.Add(1184760770U, "FailureReasonNoAttributes");
			Strings.stringIDs.Add(3864317541U, "ErrorReasonRegexRuleInvalidValue");
			Strings.stringIDs.Add(3958377408U, "InvalidConfigurationMissingLinkedInInvalidateTokenEndpoint");
			Strings.stringIDs.Add(1579586375U, "ErrorReasonMinVersionIsInvalid");
			Strings.stringIDs.Add(190956125U, "ErrorReasonMissingOfficeApp");
			Strings.stringIDs.Add(1368134268U, "ErrorReasonOnlyMailboxSetAllowedInRequirement");
			Strings.stringIDs.Add(2777862026U, "ErrorReasonInvalidID");
			Strings.stringIDs.Add(330725840U, "ErrorReasonFormInFormSettingsNotUnique");
			Strings.stringIDs.Add(2661873235U, "WelcomeMailBodyNote");
			Strings.stringIDs.Add(1979709530U, "ErrorExtensionWithIdAlreadyInstalled");
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002580 File Offset: 0x00000780
		public static LocalizedString DatacenterSecretIsMissingOrInvalid
		{
			get
			{
				return new LocalizedString("DatacenterSecretIsMissingOrInvalid", "ExE92D84", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000259E File Offset: 0x0000079E
		public static LocalizedString InvalidConfigurationMissingLinkedInAccessTokenEndpoint
		{
			get
			{
				return new LocalizedString("InvalidConfigurationMissingLinkedInAccessTokenEndpoint", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000025BC File Offset: 0x000007BC
		public static LocalizedString ErrorEtokenWithInvalidDeploymentId(string deploymentId)
		{
			return new LocalizedString("ErrorEtokenWithInvalidDeploymentId", "", false, false, Strings.ResourceManager, new object[]
			{
				deploymentId
			});
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000025EC File Offset: 0x000007EC
		public static LocalizedString ErrorProtocolConfigurationMissing(string lastServer, string settingsType)
		{
			return new LocalizedString("ErrorProtocolConfigurationMissing", "", false, false, Strings.ResourceManager, new object[]
			{
				lastServer,
				settingsType
			});
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002620 File Offset: 0x00000820
		public static LocalizedString ErrorCanNotReadInstalledList(string failureReason)
		{
			return new LocalizedString("ErrorCanNotReadInstalledList", "", false, false, Strings.ResourceManager, new object[]
			{
				failureReason
			});
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002650 File Offset: 0x00000850
		public static LocalizedString FailureReasonAttributeMissing(string targetName)
		{
			return new LocalizedString("FailureReasonAttributeMissing", "", false, false, Strings.ResourceManager, new object[]
			{
				targetName
			});
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000267F File Offset: 0x0000087F
		public static LocalizedString FailedToCreateAuditEwsConnection
		{
			get
			{
				return new LocalizedString("FailedToCreateAuditEwsConnection", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000269D File Offset: 0x0000089D
		public static LocalizedString UserPhotoStoreNotFound
		{
			get
			{
				return new LocalizedString("UserPhotoStoreNotFound", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000026BB File Offset: 0x000008BB
		public static LocalizedString ErrorReasonNoMailboxInHosts
		{
			get
			{
				return new LocalizedString("ErrorReasonNoMailboxInHosts", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000026DC File Offset: 0x000008DC
		public static LocalizedString ADUserNoPhoto(ADObjectId userId)
		{
			return new LocalizedString("ADUserNoPhoto", "", false, false, Strings.ResourceManager, new object[]
			{
				userId
			});
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000270B File Offset: 0x0000090B
		public static LocalizedString ErrorReasonIgnoreCaseWithoutRegExInEntitiesRules
		{
			get
			{
				return new LocalizedString("ErrorReasonIgnoreCaseWithoutRegExInEntitiesRules", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002729 File Offset: 0x00000929
		public static LocalizedString ErrorReasonManifestSchemaUnknown
		{
			get
			{
				return new LocalizedString("ErrorReasonManifestSchemaUnknown", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002747 File Offset: 0x00000947
		public static LocalizedString ErrorCannotDisableMandatoryExtension
		{
			get
			{
				return new LocalizedString("ErrorCannotDisableMandatoryExtension", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002765 File Offset: 0x00000965
		public static LocalizedString InvalidConfigurationFacebookAppId
		{
			get
			{
				return new LocalizedString("InvalidConfigurationFacebookAppId", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002783 File Offset: 0x00000983
		public static LocalizedString WelcomeMailSubject
		{
			get
			{
				return new LocalizedString("WelcomeMailSubject", "ExC92635", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000027A1 File Offset: 0x000009A1
		public static LocalizedString ErrorExtensionAlreadyInstalled
		{
			get
			{
				return new LocalizedString("ErrorExtensionAlreadyInstalled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000027BF File Offset: 0x000009BF
		public static LocalizedString ErrorReasonDefaultVersionIsInvalid
		{
			get
			{
				return new LocalizedString("ErrorReasonDefaultVersionIsInvalid", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000027E0 File Offset: 0x000009E0
		public static LocalizedString CannotMapInvalidSmtpAddressToPhotoFile(string address)
		{
			return new LocalizedString("CannotMapInvalidSmtpAddressToPhotoFile", "", false, false, Strings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000280F File Offset: 0x00000A0F
		public static LocalizedString InvalidConfigurationMissingLinkedInConnectionsEndpoint
		{
			get
			{
				return new LocalizedString("InvalidConfigurationMissingLinkedInConnectionsEndpoint", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000282D File Offset: 0x00000A2D
		public static LocalizedString ErrorExtensionVersionInvalid
		{
			get
			{
				return new LocalizedString("ErrorExtensionVersionInvalid", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000284B File Offset: 0x00000A4B
		public static LocalizedString ErrorReasonItemTypeInvalid
		{
			get
			{
				return new LocalizedString("ErrorReasonItemTypeInvalid", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002869 File Offset: 0x00000A69
		public static LocalizedString FailureReasonSourceLocationTagMissing
		{
			get
			{
				return new LocalizedString("FailureReasonSourceLocationTagMissing", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002887 File Offset: 0x00000A87
		public static LocalizedString ErrorExtensionWithIdAlreadyInstalledForOrg
		{
			get
			{
				return new LocalizedString("ErrorExtensionWithIdAlreadyInstalledForOrg", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000028A8 File Offset: 0x00000AA8
		public static LocalizedString ErrorReasonTooManyRule(int maxRuleNumber)
		{
			return new LocalizedString("ErrorReasonTooManyRule", "", false, false, Strings.ResourceManager, new object[]
			{
				maxRuleNumber
			});
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000028DC File Offset: 0x00000ADC
		public static LocalizedString FailureReasonUserMasterTableInvalidScope(string scope, string id)
		{
			return new LocalizedString("FailureReasonUserMasterTableInvalidScope", "", false, false, Strings.ResourceManager, new object[]
			{
				scope,
				id
			});
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002910 File Offset: 0x00000B10
		public static LocalizedString ErrorInvalidManifestData(string ErrorReason)
		{
			return new LocalizedString("ErrorInvalidManifestData", "", false, false, Strings.ResourceManager, new object[]
			{
				ErrorReason
			});
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000293F File Offset: 0x00000B3F
		public static LocalizedString InvalidConfigurationMissingFacebookAppSecret
		{
			get
			{
				return new LocalizedString("InvalidConfigurationMissingFacebookAppSecret", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002960 File Offset: 0x00000B60
		public static LocalizedString ErrorReasonInvalidXml(string xmlExceptionMessage)
		{
			return new LocalizedString("ErrorReasonInvalidXml", "", false, false, Strings.ResourceManager, new object[]
			{
				xmlExceptionMessage
			});
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000298F File Offset: 0x00000B8F
		public static LocalizedString FailedToReadWebProxyConfigurationFromAD
		{
			get
			{
				return new LocalizedString("FailedToReadWebProxyConfigurationFromAD", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000029AD File Offset: 0x00000BAD
		public static LocalizedString UserPhotoNotFound
		{
			get
			{
				return new LocalizedString("UserPhotoNotFound", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000029CC File Offset: 0x00000BCC
		public static LocalizedString ErrorCouldNotRegisterComponent(string componentName)
		{
			return new LocalizedString("ErrorCouldNotRegisterComponent", "", false, false, Strings.ResourceManager, new object[]
			{
				componentName
			});
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000029FB File Offset: 0x00000BFB
		public static LocalizedString InvalidConfigurationMissingLinkedInAppId
		{
			get
			{
				return new LocalizedString("InvalidConfigurationMissingLinkedInAppId", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002A19 File Offset: 0x00000C19
		public static LocalizedString PhotoHasBeenDeleted
		{
			get
			{
				return new LocalizedString("PhotoHasBeenDeleted", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002A37 File Offset: 0x00000C37
		public static LocalizedString FailedToAccessAuditLogWithInnerException
		{
			get
			{
				return new LocalizedString("FailedToAccessAuditLogWithInnerException", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002A58 File Offset: 0x00000C58
		public static LocalizedString ErrorReasonUrlMustBeAbsolute(string name, string value)
		{
			return new LocalizedString("ErrorReasonUrlMustBeAbsolute", "", false, false, Strings.ResourceManager, new object[]
			{
				name,
				value
			});
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002A8B File Offset: 0x00000C8B
		public static LocalizedString InvalidConfigurationMissingLinkedInRequestTokenEndpoint
		{
			get
			{
				return new LocalizedString("InvalidConfigurationMissingLinkedInRequestTokenEndpoint", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002AA9 File Offset: 0x00000CA9
		public static LocalizedString ErrorExtensionAlreadyInstalledForOrg
		{
			get
			{
				return new LocalizedString("ErrorExtensionAlreadyInstalledForOrg", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002AC7 File Offset: 0x00000CC7
		public static LocalizedString AuditLogAccessDenied
		{
			get
			{
				return new LocalizedString("AuditLogAccessDenied", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public static LocalizedString ErrorMissingNodeInEtoken(string NodeName)
		{
			return new LocalizedString("ErrorMissingNodeInEtoken", "", false, false, Strings.ResourceManager, new object[]
			{
				NodeName
			});
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002B17 File Offset: 0x00000D17
		public static LocalizedString ErrorReasonItemTypeAllTypes
		{
			get
			{
				return new LocalizedString("ErrorReasonItemTypeAllTypes", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002B35 File Offset: 0x00000D35
		public static LocalizedString ErrorMasterTableSaveConflict
		{
			get
			{
				return new LocalizedString("ErrorMasterTableSaveConflict", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002B54 File Offset: 0x00000D54
		public static LocalizedString ErrorReasonManifestVersionNotSupported(string schemaVersion, Version serverVersion)
		{
			return new LocalizedString("ErrorReasonManifestVersionNotSupported", "", false, false, Strings.ResourceManager, new object[]
			{
				schemaVersion,
				serverVersion
			});
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002B87 File Offset: 0x00000D87
		public static LocalizedString InvalidConfigurationMissingFacebookAppId
		{
			get
			{
				return new LocalizedString("InvalidConfigurationMissingFacebookAppId", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002BA8 File Offset: 0x00000DA8
		public static LocalizedString UserPhotoWithClassNotFound(string itemClass)
		{
			return new LocalizedString("UserPhotoWithClassNotFound", "", false, false, Strings.ResourceManager, new object[]
			{
				itemClass
			});
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002BD8 File Offset: 0x00000DD8
		public static LocalizedString ErrorReasonManifestTooLarge(int maxSize)
		{
			return new LocalizedString("ErrorReasonManifestTooLarge", "", false, false, Strings.ResourceManager, new object[]
			{
				maxSize
			});
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002C0C File Offset: 0x00000E0C
		public static LocalizedString ErrorCantOverwriteDefaultExtension
		{
			get
			{
				return new LocalizedString("ErrorCantOverwriteDefaultExtension", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002C2A File Offset: 0x00000E2A
		public static LocalizedString ErrorReasonOnlySelectedEntitiesInRestricted
		{
			get
			{
				return new LocalizedString("ErrorReasonOnlySelectedEntitiesInRestricted", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002C48 File Offset: 0x00000E48
		public static LocalizedString ErrorCannotUninstallProvidedExtension(string ExtensionID)
		{
			return new LocalizedString("ErrorCannotUninstallProvidedExtension", "", false, false, Strings.ResourceManager, new object[]
			{
				ExtensionID
			});
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002C78 File Offset: 0x00000E78
		public static LocalizedString ErrorReasonMultipleRulesWithSameRegExName(string regExName)
		{
			return new LocalizedString("ErrorReasonMultipleRulesWithSameRegExName", "", false, false, Strings.ResourceManager, new object[]
			{
				regExName
			});
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002CA7 File Offset: 0x00000EA7
		public static LocalizedString UserPhotoAccessDenied
		{
			get
			{
				return new LocalizedString("UserPhotoAccessDenied", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002CC8 File Offset: 0x00000EC8
		public static LocalizedString UserPhotoThumbprintNotFound(bool preview)
		{
			return new LocalizedString("UserPhotoThumbprintNotFound", "", false, false, Strings.ResourceManager, new object[]
			{
				preview
			});
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002CFC File Offset: 0x00000EFC
		public static LocalizedString ErrorReasonItemIsRuleAttributesNotValidForEdit
		{
			get
			{
				return new LocalizedString("ErrorReasonItemIsRuleAttributesNotValidForEdit", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002D1A File Offset: 0x00000F1A
		public static LocalizedString ErrorReasonNoRegexRuleInRestricted
		{
			get
			{
				return new LocalizedString("ErrorReasonNoRegexRuleInRestricted", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002D38 File Offset: 0x00000F38
		public static LocalizedString ErrorReasonInvalidUrlValue(string name, string value)
		{
			return new LocalizedString("ErrorReasonInvalidUrlValue", "", false, false, Strings.ResourceManager, new object[]
			{
				name,
				value
			});
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002D6C File Offset: 0x00000F6C
		public static LocalizedString FailedToFindEwsEndpoint(string mailbox)
		{
			return new LocalizedString("FailedToFindEwsEndpoint", "", false, false, Strings.ResourceManager, new object[]
			{
				mailbox
			});
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002D9B File Offset: 0x00000F9B
		public static LocalizedString ErrorManifestSignatureInvalidExtension
		{
			get
			{
				return new LocalizedString("ErrorManifestSignatureInvalidExtension", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002DBC File Offset: 0x00000FBC
		public static LocalizedString ErrorVDirConfigurationMissing(string lastServer, string urlType, string missingServiceType)
		{
			return new LocalizedString("ErrorVDirConfigurationMissing", "", false, false, Strings.ResourceManager, new object[]
			{
				lastServer,
				urlType,
				missingServiceType
			});
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002DF4 File Offset: 0x00000FF4
		public static LocalizedString WelcomeMailBodyMain(string link, string note)
		{
			return new LocalizedString("WelcomeMailBodyMain", "Ex74CCEB", false, true, Strings.ResourceManager, new object[]
			{
				link,
				note
			});
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002E27 File Offset: 0x00001027
		public static LocalizedString InvalidConfigurationMissingLinkedInAppSecret
		{
			get
			{
				return new LocalizedString("InvalidConfigurationMissingLinkedInAppSecret", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002E48 File Offset: 0x00001048
		public static LocalizedString FailedToReadProviderConfigurationSeeInnerException(string provider)
		{
			return new LocalizedString("FailedToReadProviderConfigurationSeeInnerException", "", false, false, Strings.ResourceManager, new object[]
			{
				provider
			});
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002E78 File Offset: 0x00001078
		public static LocalizedString ErrorReasonMinApiVersionNotSupported(Version minApiVersion, Version serverVersion)
		{
			return new LocalizedString("ErrorReasonMinApiVersionNotSupported", "", false, false, Strings.ResourceManager, new object[]
			{
				minApiVersion,
				serverVersion
			});
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002EAB File Offset: 0x000010AB
		public static LocalizedString ErrorReasonRegExNameAndValueRequiredInEntitiesRules
		{
			get
			{
				return new LocalizedString("ErrorReasonRegExNameAndValueRequiredInEntitiesRules", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002EC9 File Offset: 0x000010C9
		public static LocalizedString SecureStringParameter
		{
			get
			{
				return new LocalizedString("SecureStringParameter", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002EE7 File Offset: 0x000010E7
		public static LocalizedString InvalidConfigurationFacebookAppSecret
		{
			get
			{
				return new LocalizedString("InvalidConfigurationFacebookAppSecret", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002F08 File Offset: 0x00001108
		public static LocalizedString ErrorExtensionUnableToUpgrade(string extensionName)
		{
			return new LocalizedString("ErrorExtensionUnableToUpgrade", "", false, false, Strings.ResourceManager, new object[]
			{
				extensionName
			});
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002F38 File Offset: 0x00001138
		public static LocalizedString FailureReasonOrgMasterTableInvalidScope(string scope, string id)
		{
			return new LocalizedString("FailureReasonOrgMasterTableInvalidScope", "", false, false, Strings.ResourceManager, new object[]
			{
				scope,
				id
			});
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002F6B File Offset: 0x0000116B
		public static LocalizedString InvalidConfigurationMissingFacebookGraphTokenEndpoint
		{
			get
			{
				return new LocalizedString("InvalidConfigurationMissingFacebookGraphTokenEndpoint", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002F8C File Offset: 0x0000118C
		public static LocalizedString ErrorReasonUnsupportedUrlScheme(string name, string value)
		{
			return new LocalizedString("ErrorReasonUnsupportedUrlScheme", "", false, false, Strings.ResourceManager, new object[]
			{
				name,
				value
			});
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002FBF File Offset: 0x000011BF
		public static LocalizedString InvalidConfigurationLinkedInAppId
		{
			get
			{
				return new LocalizedString("InvalidConfigurationLinkedInAppId", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002FDD File Offset: 0x000011DD
		public static LocalizedString ErrorOrgLevelAppMustBeSiteLicense
		{
			get
			{
				return new LocalizedString("ErrorOrgLevelAppMustBeSiteLicense", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002FFB File Offset: 0x000011FB
		public static LocalizedString ErrorMarketplaceWebServicesUnavailable
		{
			get
			{
				return new LocalizedString("ErrorMarketplaceWebServicesUnavailable", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003019 File Offset: 0x00001219
		public static LocalizedString InvalidConfigurationMissingFacebookGraphApiEndpoint
		{
			get
			{
				return new LocalizedString("InvalidConfigurationMissingFacebookGraphApiEndpoint", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00003037 File Offset: 0x00001237
		public static LocalizedString InvalidConfigurationLinkedInAppSecret
		{
			get
			{
				return new LocalizedString("InvalidConfigurationLinkedInAppSecret", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00003055 File Offset: 0x00001255
		public static LocalizedString InvalidConfigurationMissingFacebookAuthorizationEndpoint
		{
			get
			{
				return new LocalizedString("InvalidConfigurationMissingFacebookAuthorizationEndpoint", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003073 File Offset: 0x00001273
		public static LocalizedString InvalidConfigurationMissingLinkedInProfileEndpoint
		{
			get
			{
				return new LocalizedString("InvalidConfigurationMissingLinkedInProfileEndpoint", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00003091 File Offset: 0x00001291
		public static LocalizedString FailureReasonNoAttributes
		{
			get
			{
				return new LocalizedString("FailureReasonNoAttributes", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000030AF File Offset: 0x000012AF
		public static LocalizedString ErrorReasonRegexRuleInvalidValue
		{
			get
			{
				return new LocalizedString("ErrorReasonRegexRuleInvalidValue", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000030D0 File Offset: 0x000012D0
		public static LocalizedString ErrorReasonTooManyRegexRule(int maxRuleNumber)
		{
			return new LocalizedString("ErrorReasonTooManyRegexRule", "", false, false, Strings.ResourceManager, new object[]
			{
				maxRuleNumber
			});
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003104 File Offset: 0x00001304
		public static LocalizedString FailureReasonAttributeValueInvalid(string targetName, string value)
		{
			return new LocalizedString("FailureReasonAttributeValueInvalid", "", false, false, Strings.ResourceManager, new object[]
			{
				targetName,
				value
			});
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003137 File Offset: 0x00001337
		public static LocalizedString InvalidConfigurationMissingLinkedInInvalidateTokenEndpoint
		{
			get
			{
				return new LocalizedString("InvalidConfigurationMissingLinkedInInvalidateTokenEndpoint", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003155 File Offset: 0x00001355
		public static LocalizedString ErrorReasonMinVersionIsInvalid
		{
			get
			{
				return new LocalizedString("ErrorReasonMinVersionIsInvalid", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003174 File Offset: 0x00001374
		public static LocalizedString ErrorExtensionNotFound(string extensionID)
		{
			return new LocalizedString("ErrorExtensionNotFound", "", false, false, Strings.ResourceManager, new object[]
			{
				extensionID
			});
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000031A3 File Offset: 0x000013A3
		public static LocalizedString ErrorReasonMissingOfficeApp
		{
			get
			{
				return new LocalizedString("ErrorReasonMissingOfficeApp", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000031C1 File Offset: 0x000013C1
		public static LocalizedString ErrorReasonOnlyMailboxSetAllowedInRequirement
		{
			get
			{
				return new LocalizedString("ErrorReasonOnlyMailboxSetAllowedInRequirement", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000031DF File Offset: 0x000013DF
		public static LocalizedString ErrorReasonInvalidID
		{
			get
			{
				return new LocalizedString("ErrorReasonInvalidID", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003200 File Offset: 0x00001400
		public static LocalizedString UserPhotoTooManyItems(string itemClass)
		{
			return new LocalizedString("UserPhotoTooManyItems", "", false, false, Strings.ResourceManager, new object[]
			{
				itemClass
			});
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000058 RID: 88 RVA: 0x0000322F File Offset: 0x0000142F
		public static LocalizedString ErrorReasonFormInFormSettingsNotUnique
		{
			get
			{
				return new LocalizedString("ErrorReasonFormInFormSettingsNotUnique", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003250 File Offset: 0x00001450
		public static LocalizedString FailedToAccessAuditLog(string responseclass, string code, string error)
		{
			return new LocalizedString("FailedToAccessAuditLog", "", false, false, Strings.ResourceManager, new object[]
			{
				responseclass,
				code,
				error
			});
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003288 File Offset: 0x00001488
		public static LocalizedString ErrorReasonMultipleRulesWithSameFilterName(string filterName)
		{
			return new LocalizedString("ErrorReasonMultipleRulesWithSameFilterName", "", false, false, Strings.ResourceManager, new object[]
			{
				filterName
			});
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000032B8 File Offset: 0x000014B8
		public static LocalizedString FailureReasonTagValueInvalid(string targetName, string value)
		{
			return new LocalizedString("FailureReasonTagValueInvalid", "", false, false, Strings.ResourceManager, new object[]
			{
				targetName,
				value
			});
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000032EB File Offset: 0x000014EB
		public static LocalizedString WelcomeMailBodyNote
		{
			get
			{
				return new LocalizedString("WelcomeMailBodyNote", "Ex7B5215", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000330C File Offset: 0x0000150C
		public static LocalizedString FailureReasonTagMissing(string targetName)
		{
			return new LocalizedString("FailureReasonTagMissing", "", false, false, Strings.ResourceManager, new object[]
			{
				targetName
			});
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000333C File Offset: 0x0000153C
		public static LocalizedString ErrorReasonManifestValidationError(string exceptionMessage)
		{
			return new LocalizedString("ErrorReasonManifestValidationError", "", false, false, Strings.ResourceManager, new object[]
			{
				exceptionMessage
			});
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000336B File Offset: 0x0000156B
		public static LocalizedString ErrorExtensionWithIdAlreadyInstalled
		{
			get
			{
				return new LocalizedString("ErrorExtensionWithIdAlreadyInstalled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000338C File Offset: 0x0000158C
		public static LocalizedString ErrorReasonInvalidRegEx(string ruleName, string attributeName)
		{
			return new LocalizedString("ErrorReasonInvalidRegEx", "", false, false, Strings.ResourceManager, new object[]
			{
				ruleName,
				attributeName
			});
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000033C0 File Offset: 0x000015C0
		public static LocalizedString ErrorAssetIdNotMatchInEtoken(string assetId, string assetIdInToken)
		{
			return new LocalizedString("ErrorAssetIdNotMatchInEtoken", "", false, false, Strings.ResourceManager, new object[]
			{
				assetId,
				assetIdInToken
			});
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000033F3 File Offset: 0x000015F3
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000001 RID: 1
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(57);

		// Token: 0x04000002 RID: 2
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Data.ApplicationLogic.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000003 RID: 3
		public enum IDs : uint
		{
			// Token: 0x04000004 RID: 4
			DatacenterSecretIsMissingOrInvalid = 1416971879U,
			// Token: 0x04000005 RID: 5
			InvalidConfigurationMissingLinkedInAccessTokenEndpoint = 358167539U,
			// Token: 0x04000006 RID: 6
			FailedToCreateAuditEwsConnection = 507132958U,
			// Token: 0x04000007 RID: 7
			UserPhotoStoreNotFound = 425380821U,
			// Token: 0x04000008 RID: 8
			ErrorReasonNoMailboxInHosts = 1386497625U,
			// Token: 0x04000009 RID: 9
			ErrorReasonIgnoreCaseWithoutRegExInEntitiesRules = 2430608892U,
			// Token: 0x0400000A RID: 10
			ErrorReasonManifestSchemaUnknown = 2258791064U,
			// Token: 0x0400000B RID: 11
			ErrorCannotDisableMandatoryExtension = 1262244671U,
			// Token: 0x0400000C RID: 12
			InvalidConfigurationFacebookAppId = 1644547849U,
			// Token: 0x0400000D RID: 13
			WelcomeMailSubject = 1736107127U,
			// Token: 0x0400000E RID: 14
			ErrorExtensionAlreadyInstalled = 3597849903U,
			// Token: 0x0400000F RID: 15
			ErrorReasonDefaultVersionIsInvalid = 3654277798U,
			// Token: 0x04000010 RID: 16
			InvalidConfigurationMissingLinkedInConnectionsEndpoint = 2929113449U,
			// Token: 0x04000011 RID: 17
			ErrorExtensionVersionInvalid = 1132961834U,
			// Token: 0x04000012 RID: 18
			ErrorReasonItemTypeInvalid = 3473955170U,
			// Token: 0x04000013 RID: 19
			FailureReasonSourceLocationTagMissing = 1902437266U,
			// Token: 0x04000014 RID: 20
			ErrorExtensionWithIdAlreadyInstalledForOrg = 1272553825U,
			// Token: 0x04000015 RID: 21
			InvalidConfigurationMissingFacebookAppSecret = 809039298U,
			// Token: 0x04000016 RID: 22
			FailedToReadWebProxyConfigurationFromAD = 1528417217U,
			// Token: 0x04000017 RID: 23
			UserPhotoNotFound = 693971404U,
			// Token: 0x04000018 RID: 24
			InvalidConfigurationMissingLinkedInAppId = 3286161403U,
			// Token: 0x04000019 RID: 25
			PhotoHasBeenDeleted = 224007719U,
			// Token: 0x0400001A RID: 26
			FailedToAccessAuditLogWithInnerException = 994301062U,
			// Token: 0x0400001B RID: 27
			InvalidConfigurationMissingLinkedInRequestTokenEndpoint = 980865226U,
			// Token: 0x0400001C RID: 28
			ErrorExtensionAlreadyInstalledForOrg = 895214716U,
			// Token: 0x0400001D RID: 29
			AuditLogAccessDenied = 2103946832U,
			// Token: 0x0400001E RID: 30
			ErrorReasonItemTypeAllTypes = 2566788983U,
			// Token: 0x0400001F RID: 31
			ErrorMasterTableSaveConflict = 1057073101U,
			// Token: 0x04000020 RID: 32
			InvalidConfigurationMissingFacebookAppId = 3960918573U,
			// Token: 0x04000021 RID: 33
			ErrorCantOverwriteDefaultExtension = 3353268377U,
			// Token: 0x04000022 RID: 34
			ErrorReasonOnlySelectedEntitiesInRestricted = 4017558222U,
			// Token: 0x04000023 RID: 35
			UserPhotoAccessDenied = 1445064246U,
			// Token: 0x04000024 RID: 36
			ErrorReasonItemIsRuleAttributesNotValidForEdit = 3896524780U,
			// Token: 0x04000025 RID: 37
			ErrorReasonNoRegexRuleInRestricted = 1303673802U,
			// Token: 0x04000026 RID: 38
			ErrorManifestSignatureInvalidExtension = 2340791895U,
			// Token: 0x04000027 RID: 39
			InvalidConfigurationMissingLinkedInAppSecret = 921746356U,
			// Token: 0x04000028 RID: 40
			ErrorReasonRegExNameAndValueRequiredInEntitiesRules = 103323614U,
			// Token: 0x04000029 RID: 41
			SecureStringParameter = 531489075U,
			// Token: 0x0400002A RID: 42
			InvalidConfigurationFacebookAppSecret = 3364919108U,
			// Token: 0x0400002B RID: 43
			InvalidConfigurationMissingFacebookGraphTokenEndpoint = 163140049U,
			// Token: 0x0400002C RID: 44
			InvalidConfigurationLinkedInAppId = 3689335943U,
			// Token: 0x0400002D RID: 45
			ErrorOrgLevelAppMustBeSiteLicense = 3369915033U,
			// Token: 0x0400002E RID: 46
			ErrorMarketplaceWebServicesUnavailable = 2727153851U,
			// Token: 0x0400002F RID: 47
			InvalidConfigurationMissingFacebookGraphApiEndpoint = 2267978298U,
			// Token: 0x04000030 RID: 48
			InvalidConfigurationLinkedInAppSecret = 3571146374U,
			// Token: 0x04000031 RID: 49
			InvalidConfigurationMissingFacebookAuthorizationEndpoint = 3104695745U,
			// Token: 0x04000032 RID: 50
			InvalidConfigurationMissingLinkedInProfileEndpoint = 3368339595U,
			// Token: 0x04000033 RID: 51
			FailureReasonNoAttributes = 1184760770U,
			// Token: 0x04000034 RID: 52
			ErrorReasonRegexRuleInvalidValue = 3864317541U,
			// Token: 0x04000035 RID: 53
			InvalidConfigurationMissingLinkedInInvalidateTokenEndpoint = 3958377408U,
			// Token: 0x04000036 RID: 54
			ErrorReasonMinVersionIsInvalid = 1579586375U,
			// Token: 0x04000037 RID: 55
			ErrorReasonMissingOfficeApp = 190956125U,
			// Token: 0x04000038 RID: 56
			ErrorReasonOnlyMailboxSetAllowedInRequirement = 1368134268U,
			// Token: 0x04000039 RID: 57
			ErrorReasonInvalidID = 2777862026U,
			// Token: 0x0400003A RID: 58
			ErrorReasonFormInFormSettingsNotUnique = 330725840U,
			// Token: 0x0400003B RID: 59
			WelcomeMailBodyNote = 2661873235U,
			// Token: 0x0400003C RID: 60
			ErrorExtensionWithIdAlreadyInstalled = 1979709530U
		}

		// Token: 0x02000004 RID: 4
		private enum ParamIDs
		{
			// Token: 0x0400003E RID: 62
			ErrorEtokenWithInvalidDeploymentId,
			// Token: 0x0400003F RID: 63
			ErrorProtocolConfigurationMissing,
			// Token: 0x04000040 RID: 64
			ErrorCanNotReadInstalledList,
			// Token: 0x04000041 RID: 65
			FailureReasonAttributeMissing,
			// Token: 0x04000042 RID: 66
			ADUserNoPhoto,
			// Token: 0x04000043 RID: 67
			CannotMapInvalidSmtpAddressToPhotoFile,
			// Token: 0x04000044 RID: 68
			ErrorReasonTooManyRule,
			// Token: 0x04000045 RID: 69
			FailureReasonUserMasterTableInvalidScope,
			// Token: 0x04000046 RID: 70
			ErrorInvalidManifestData,
			// Token: 0x04000047 RID: 71
			ErrorReasonInvalidXml,
			// Token: 0x04000048 RID: 72
			ErrorCouldNotRegisterComponent,
			// Token: 0x04000049 RID: 73
			ErrorReasonUrlMustBeAbsolute,
			// Token: 0x0400004A RID: 74
			ErrorMissingNodeInEtoken,
			// Token: 0x0400004B RID: 75
			ErrorReasonManifestVersionNotSupported,
			// Token: 0x0400004C RID: 76
			UserPhotoWithClassNotFound,
			// Token: 0x0400004D RID: 77
			ErrorReasonManifestTooLarge,
			// Token: 0x0400004E RID: 78
			ErrorCannotUninstallProvidedExtension,
			// Token: 0x0400004F RID: 79
			ErrorReasonMultipleRulesWithSameRegExName,
			// Token: 0x04000050 RID: 80
			UserPhotoThumbprintNotFound,
			// Token: 0x04000051 RID: 81
			ErrorReasonInvalidUrlValue,
			// Token: 0x04000052 RID: 82
			FailedToFindEwsEndpoint,
			// Token: 0x04000053 RID: 83
			ErrorVDirConfigurationMissing,
			// Token: 0x04000054 RID: 84
			WelcomeMailBodyMain,
			// Token: 0x04000055 RID: 85
			FailedToReadProviderConfigurationSeeInnerException,
			// Token: 0x04000056 RID: 86
			ErrorReasonMinApiVersionNotSupported,
			// Token: 0x04000057 RID: 87
			ErrorExtensionUnableToUpgrade,
			// Token: 0x04000058 RID: 88
			FailureReasonOrgMasterTableInvalidScope,
			// Token: 0x04000059 RID: 89
			ErrorReasonUnsupportedUrlScheme,
			// Token: 0x0400005A RID: 90
			ErrorReasonTooManyRegexRule,
			// Token: 0x0400005B RID: 91
			FailureReasonAttributeValueInvalid,
			// Token: 0x0400005C RID: 92
			ErrorExtensionNotFound,
			// Token: 0x0400005D RID: 93
			UserPhotoTooManyItems,
			// Token: 0x0400005E RID: 94
			FailedToAccessAuditLog,
			// Token: 0x0400005F RID: 95
			ErrorReasonMultipleRulesWithSameFilterName,
			// Token: 0x04000060 RID: 96
			FailureReasonTagValueInvalid,
			// Token: 0x04000061 RID: 97
			FailureReasonTagMissing,
			// Token: 0x04000062 RID: 98
			ErrorReasonManifestValidationError,
			// Token: 0x04000063 RID: 99
			ErrorReasonInvalidRegEx,
			// Token: 0x04000064 RID: 100
			ErrorAssetIdNotMatchInEtoken
		}
	}
}
