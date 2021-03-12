using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000152 RID: 338
	internal static class Strings
	{
		// Token: 0x06000E0F RID: 3599 RVA: 0x000339BC File Offset: 0x00031BBC
		static Strings()
		{
			Strings.stringIDs.Add(2376986056U, "ErrorOnlyAllowInEop");
			Strings.stringIDs.Add(2845151070U, "InvalidContentDateFromAndContentDateToPredicate");
			Strings.stringIDs.Add(3231761178U, "PolicyNotifyErrorErrorMsg");
			Strings.stringIDs.Add(1183239240U, "CannotChangeDeviceConfigurationPolicyWorkload");
			Strings.stringIDs.Add(2160431466U, "CannotChangeDeviceConditionalAccessRuleName");
			Strings.stringIDs.Add(857423920U, "VerboseValidatingAddSharepointBinding");
			Strings.stringIDs.Add(629789999U, "UnexpectedConditionOrActionDetected");
			Strings.stringIDs.Add(107430575U, "SiteOutOfQuotaErrorMsg");
			Strings.stringIDs.Add(464180248U, "CanOnlyChangeDeviceConditionalAccessPolicy");
			Strings.stringIDs.Add(3201126371U, "InvalidCompliancePolicyWorkload");
			Strings.stringIDs.Add(178440487U, "VerboseValidatingExchangeBinding");
			Strings.stringIDs.Add(1412691609U, "InvalidAuditRuleWorkload");
			Strings.stringIDs.Add(4074170900U, "CanOnlyChangeDeviceConfigurationPolicy");
			Strings.stringIDs.Add(1306124383U, "VerboseValidatingRemoveSharepointBinding");
			Strings.stringIDs.Add(1508525573U, "CannotChangeDeviceConfigurationPolicyScenario");
			Strings.stringIDs.Add(3499233439U, "ShouldExpandGroups");
			Strings.stringIDs.Add(1830331352U, "InvalidDeviceRuleWorkload");
			Strings.stringIDs.Add(1930430118U, "ErrorInvalidPolicyCenterSiteOwner");
			Strings.stringIDs.Add(1713451535U, "CannotChangeAuditConfigurationRuleWorkload");
			Strings.stringIDs.Add(1005978434U, "FailedToOpenContainerErrorMsg");
			Strings.stringIDs.Add(2981356586U, "CannotChangeDeviceTenantRuleName");
			Strings.stringIDs.Add(228813078U, "CannotChangeDeviceConfigurationRuleName");
			Strings.stringIDs.Add(4091585218U, "InvalidAccessScopeIsPredicate");
			Strings.stringIDs.Add(2039724013U, "SpParserVersionNotSpecified");
			Strings.stringIDs.Add(1066947153U, "VerboseValidatingRemoveExchangeBinding");
			Strings.stringIDs.Add(317700209U, "InvalidHoldContentAction");
			Strings.stringIDs.Add(2522696186U, "CannotSetDeviceConfigurationPolicyWorkload");
			Strings.stringIDs.Add(167859032U, "CanOnlyManipulateDeviceConfigurationRule");
			Strings.stringIDs.Add(82360181U, "MulipleSpBindingObjectDetected");
			Strings.stringIDs.Add(605330666U, "ErrorNeedOrganizationId");
			Strings.stringIDs.Add(2976502162U, "CanOnlyChangeDeviceTenantPolicy");
			Strings.stringIDs.Add(631692221U, "SkippingInvalidTypeInGroupExpansion");
			Strings.stringIDs.Add(1853496069U, "FailedToGetExecutingUser");
			Strings.stringIDs.Add(1582525285U, "SensitiveInformationDoesNotContainId");
			Strings.stringIDs.Add(639724051U, "CannotChangeAuditConfigurationRuleName");
			Strings.stringIDs.Add(1614982109U, "PolicySyncTimeoutErrorMsg");
			Strings.stringIDs.Add(3781430831U, "MulipleExBindingObjectDetected");
			Strings.stringIDs.Add(766476708U, "CannotManipulateAuditConfigurationPolicy");
			Strings.stringIDs.Add(2985777218U, "CanOnlyManipulateDeviceTenantRule");
			Strings.stringIDs.Add(1450120914U, "ErrorSpBindingWithoutSpWorkload");
			Strings.stringIDs.Add(532439181U, "CannotManipulateDeviceConfigurationRule");
			Strings.stringIDs.Add(3642912594U, "ErrorExBindingWithoutExWorkload");
			Strings.stringIDs.Add(2717805860U, "CanOnlyManipulateDeviceConditionalAccessRule");
			Strings.stringIDs.Add(3278450928U, "DeploymentFailureWithNoImpact");
			Strings.stringIDs.Add(2628803538U, "FailedToGetSpSiteUrlForTenant");
			Strings.stringIDs.Add(1613362912U, "InvalidCombinationOfCompliancePolicyTypeAndWorkload");
			Strings.stringIDs.Add(912843721U, "FailedToGetCredentialsForTenant");
			Strings.stringIDs.Add(3928915344U, "InvalidContentPropertyContainsWordsPredicate");
			Strings.stringIDs.Add(518140498U, "VerboseValidatingAddExchangeBinding");
			Strings.stringIDs.Add(1502130987U, "CanOnlyManipulateAuditConfigurationPolicy");
			Strings.stringIDs.Add(574146044U, "VerboseRetryDistributionNotApplicable");
			Strings.stringIDs.Add(4099235009U, "UnknownErrorMsg");
			Strings.stringIDs.Add(1327345872U, "AuditConfigurationPolicyNotAllowed");
			Strings.stringIDs.Add(445162545U, "CanOnlyManipulateAuditConfigurationRule");
			Strings.stringIDs.Add(4174366101U, "DeviceConfigurationPolicyNotAllowed");
			Strings.stringIDs.Add(1615823280U, "CannotManipulateAuditConfigurationRule");
			Strings.stringIDs.Add(1905427409U, "SiteInReadonlyOrNotAccessibleErrorMsg");
			Strings.stringIDs.Add(2767511991U, "InvalidSensitiveInformationParameterValue");
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00033E80 File Offset: 0x00032080
		public static LocalizedString SpParserInvalidVersionType(string version)
		{
			return new LocalizedString("SpParserInvalidVersionType", Strings.ResourceManager, new object[]
			{
				version
			});
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00033EA8 File Offset: 0x000320A8
		public static LocalizedString ErrorCreateSiteTimeOut(string url)
		{
			return new LocalizedString("ErrorCreateSiteTimeOut", Strings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00033ED0 File Offset: 0x000320D0
		public static LocalizedString VerboseBeginCalculatePolicyDistributionStatus(string name)
		{
			return new LocalizedString("VerboseBeginCalculatePolicyDistributionStatus", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00033EF8 File Offset: 0x000320F8
		public static LocalizedString ErrorMessageForNotificationFailure(string workload, string error)
		{
			return new LocalizedString("ErrorMessageForNotificationFailure", Strings.ResourceManager, new object[]
			{
				workload,
				error
			});
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00033F24 File Offset: 0x00032124
		public static LocalizedString ErrorTaskRuleIsTooAdvancedToModify(string identity)
		{
			return new LocalizedString("ErrorTaskRuleIsTooAdvancedToModify", Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x00033F4C File Offset: 0x0003214C
		public static LocalizedString ErrorOnlyAllowInEop
		{
			get
			{
				return new LocalizedString("ErrorOnlyAllowInEop", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00033F64 File Offset: 0x00032164
		public static LocalizedString ErrorCompliancePolicyHasNoObjectsToRetry(string name)
		{
			return new LocalizedString("ErrorCompliancePolicyHasNoObjectsToRetry", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00033F8C File Offset: 0x0003218C
		public static LocalizedString ErrorCannotCreateRuleUnderPendingDeletionPolicy(string name)
		{
			return new LocalizedString("ErrorCannotCreateRuleUnderPendingDeletionPolicy", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x00033FB4 File Offset: 0x000321B4
		public static LocalizedString InvalidContentDateFromAndContentDateToPredicate
		{
			get
			{
				return new LocalizedString("InvalidContentDateFromAndContentDateToPredicate", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x00033FCB File Offset: 0x000321CB
		public static LocalizedString PolicyNotifyErrorErrorMsg
		{
			get
			{
				return new LocalizedString("PolicyNotifyErrorErrorMsg", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00033FE4 File Offset: 0x000321E4
		public static LocalizedString VerbosePolicyStorageObjectLoadedForCommonRule(string storageObject, string policy)
		{
			return new LocalizedString("VerbosePolicyStorageObjectLoadedForCommonRule", Strings.ResourceManager, new object[]
			{
				storageObject,
				policy
			});
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00034010 File Offset: 0x00032210
		public static LocalizedString VerboseDeletePolicyStorageBaseObject(string name, string typeName)
		{
			return new LocalizedString("VerboseDeletePolicyStorageBaseObject", Strings.ResourceManager, new object[]
			{
				name,
				typeName
			});
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x0003403C File Offset: 0x0003223C
		public static LocalizedString CannotChangeDeviceConfigurationPolicyWorkload
		{
			get
			{
				return new LocalizedString("CannotChangeDeviceConfigurationPolicyWorkload", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x00034053 File Offset: 0x00032253
		public static LocalizedString CannotChangeDeviceConditionalAccessRuleName
		{
			get
			{
				return new LocalizedString("CannotChangeDeviceConditionalAccessRuleName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x0003406A File Offset: 0x0003226A
		public static LocalizedString VerboseValidatingAddSharepointBinding
		{
			get
			{
				return new LocalizedString("VerboseValidatingAddSharepointBinding", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x00034081 File Offset: 0x00032281
		public static LocalizedString UnexpectedConditionOrActionDetected
		{
			get
			{
				return new LocalizedString("UnexpectedConditionOrActionDetected", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x00034098 File Offset: 0x00032298
		public static LocalizedString SiteOutOfQuotaErrorMsg
		{
			get
			{
				return new LocalizedString("SiteOutOfQuotaErrorMsg", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x000340AF File Offset: 0x000322AF
		public static LocalizedString CanOnlyChangeDeviceConditionalAccessPolicy
		{
			get
			{
				return new LocalizedString("CanOnlyChangeDeviceConditionalAccessPolicy", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x000340C8 File Offset: 0x000322C8
		public static LocalizedString RemoveDeviceConditionalAccessRuleConfirmation(string ruleName)
		{
			return new LocalizedString("RemoveDeviceConditionalAccessRuleConfirmation", Strings.ResourceManager, new object[]
			{
				ruleName
			});
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x000340F0 File Offset: 0x000322F0
		public static LocalizedString WarningTaskPolicyIsTooAdvancedToRead(string identity)
		{
			return new LocalizedString("WarningTaskPolicyIsTooAdvancedToRead", Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x00034118 File Offset: 0x00032318
		public static LocalizedString InvalidCompliancePolicyWorkload
		{
			get
			{
				return new LocalizedString("InvalidCompliancePolicyWorkload", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00034130 File Offset: 0x00032330
		public static LocalizedString DisplayBindingName(string workload)
		{
			return new LocalizedString("DisplayBindingName", Strings.ResourceManager, new object[]
			{
				workload
			});
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00034158 File Offset: 0x00032358
		public static LocalizedString VerboseTreatAsWarning(string endPoint, string objectType, string workload)
		{
			return new LocalizedString("VerboseTreatAsWarning", Strings.ResourceManager, new object[]
			{
				endPoint,
				objectType,
				workload
			});
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00034188 File Offset: 0x00032388
		public static LocalizedString SpParserInvalidSiteId(string siteId)
		{
			return new LocalizedString("SpParserInvalidSiteId", Strings.ResourceManager, new object[]
			{
				siteId
			});
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x000341B0 File Offset: 0x000323B0
		public static LocalizedString ErrorMultipleObjectTypeForObjectLevelSync(string types)
		{
			return new LocalizedString("ErrorMultipleObjectTypeForObjectLevelSync", Strings.ResourceManager, new object[]
			{
				types
			});
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x000341D8 File Offset: 0x000323D8
		public static LocalizedString VerboseValidatingExchangeBinding
		{
			get
			{
				return new LocalizedString("VerboseValidatingExchangeBinding", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x000341F0 File Offset: 0x000323F0
		public static LocalizedString WarningDisabledRuleInEnabledPolicy(string ruleName)
		{
			return new LocalizedString("WarningDisabledRuleInEnabledPolicy", Strings.ResourceManager, new object[]
			{
				ruleName
			});
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x00034218 File Offset: 0x00032418
		public static LocalizedString ErrorRuleContainsNoActions(string ruleName)
		{
			return new LocalizedString("ErrorRuleContainsNoActions", Strings.ResourceManager, new object[]
			{
				ruleName
			});
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x00034240 File Offset: 0x00032440
		public static LocalizedString InvalidAuditRuleWorkload
		{
			get
			{
				return new LocalizedString("InvalidAuditRuleWorkload", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x00034258 File Offset: 0x00032458
		public static LocalizedString ErrorCompliancePolicySyncNotificationClient(string workLoad, string reason)
		{
			return new LocalizedString("ErrorCompliancePolicySyncNotificationClient", Strings.ResourceManager, new object[]
			{
				workLoad,
				reason
			});
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00034284 File Offset: 0x00032484
		public static LocalizedString VerboseTrytoCheckSiteDeletedState(Uri siteUrl, string error)
		{
			return new LocalizedString("VerboseTrytoCheckSiteDeletedState", Strings.ResourceManager, new object[]
			{
				siteUrl,
				error
			});
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x000342B0 File Offset: 0x000324B0
		public static LocalizedString CanOnlyChangeDeviceConfigurationPolicy
		{
			get
			{
				return new LocalizedString("CanOnlyChangeDeviceConfigurationPolicy", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x000342C7 File Offset: 0x000324C7
		public static LocalizedString VerboseValidatingRemoveSharepointBinding
		{
			get
			{
				return new LocalizedString("VerboseValidatingRemoveSharepointBinding", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x000342DE File Offset: 0x000324DE
		public static LocalizedString CannotChangeDeviceConfigurationPolicyScenario
		{
			get
			{
				return new LocalizedString("CannotChangeDeviceConfigurationPolicyScenario", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x000342F8 File Offset: 0x000324F8
		public static LocalizedString WarningInvalidTenant(string tenantId)
		{
			return new LocalizedString("WarningInvalidTenant", Strings.ResourceManager, new object[]
			{
				tenantId
			});
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00034320 File Offset: 0x00032520
		public static LocalizedString VerboseNotifyWorkloadWithChangesSuccess(string workload, string notificationIdentifier)
		{
			return new LocalizedString("VerboseNotifyWorkloadWithChangesSuccess", Strings.ResourceManager, new object[]
			{
				workload,
				notificationIdentifier
			});
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0003434C File Offset: 0x0003254C
		public static LocalizedString ErrorRulesInPolicyIsTooAdvancedToModify(string policy, string rule)
		{
			return new LocalizedString("ErrorRulesInPolicyIsTooAdvancedToModify", Strings.ResourceManager, new object[]
			{
				policy,
				rule
			});
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00034378 File Offset: 0x00032578
		public static LocalizedString VerbosePolicyCenterSiteOwner(string address)
		{
			return new LocalizedString("VerbosePolicyCenterSiteOwner", Strings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x000343A0 File Offset: 0x000325A0
		public static LocalizedString ShouldExpandGroups
		{
			get
			{
				return new LocalizedString("ShouldExpandGroups", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x000343B8 File Offset: 0x000325B8
		public static LocalizedString ErrorCannotRemovePendingDeletionRule(string name)
		{
			return new LocalizedString("ErrorCannotRemovePendingDeletionRule", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x000343E0 File Offset: 0x000325E0
		public static LocalizedString ErrorSavingPolicyToWorkload(string policyName, string workloadName)
		{
			return new LocalizedString("ErrorSavingPolicyToWorkload", Strings.ResourceManager, new object[]
			{
				policyName,
				workloadName
			});
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x0003440C File Offset: 0x0003260C
		public static LocalizedString InvalidDeviceRuleWorkload
		{
			get
			{
				return new LocalizedString("InvalidDeviceRuleWorkload", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00034424 File Offset: 0x00032624
		public static LocalizedString VerbosePolicyStorageObjectLoaded(string storageObject)
		{
			return new LocalizedString("VerbosePolicyStorageObjectLoaded", Strings.ResourceManager, new object[]
			{
				storageObject
			});
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x0003444C File Offset: 0x0003264C
		public static LocalizedString ErrorInvalidPolicyCenterSiteOwner
		{
			get
			{
				return new LocalizedString("ErrorInvalidPolicyCenterSiteOwner", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x00034463 File Offset: 0x00032663
		public static LocalizedString CannotChangeAuditConfigurationRuleWorkload
		{
			get
			{
				return new LocalizedString("CannotChangeAuditConfigurationRuleWorkload", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0003447C File Offset: 0x0003267C
		public static LocalizedString ErrorCannotRemovePendingDeletionPolicy(string name)
		{
			return new LocalizedString("ErrorCannotRemovePendingDeletionPolicy", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x000344A4 File Offset: 0x000326A4
		public static LocalizedString FailedToOpenContainerErrorMsg
		{
			get
			{
				return new LocalizedString("FailedToOpenContainerErrorMsg", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x000344BC File Offset: 0x000326BC
		public static LocalizedString InvalidSensitiveInformationParameterName(string invalidParameter)
		{
			return new LocalizedString("InvalidSensitiveInformationParameterName", Strings.ResourceManager, new object[]
			{
				invalidParameter
			});
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x000344E4 File Offset: 0x000326E4
		public static LocalizedString CannotChangeDeviceTenantRuleName
		{
			get
			{
				return new LocalizedString("CannotChangeDeviceTenantRuleName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x000344FC File Offset: 0x000326FC
		public static LocalizedString VerboseSpNotificationClientInfo(Uri spSiteUrl, Uri syncSvcUrl, string credentialType)
		{
			return new LocalizedString("VerboseSpNotificationClientInfo", Strings.ResourceManager, new object[]
			{
				spSiteUrl,
				syncSvcUrl,
				credentialType
			});
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x0003452C File Offset: 0x0003272C
		public static LocalizedString CannotChangeDeviceConfigurationRuleName
		{
			get
			{
				return new LocalizedString("CannotChangeDeviceConfigurationRuleName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x00034543 File Offset: 0x00032743
		public static LocalizedString InvalidAccessScopeIsPredicate
		{
			get
			{
				return new LocalizedString("InvalidAccessScopeIsPredicate", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x0003455C File Offset: 0x0003275C
		public static LocalizedString VerboseTrytoCreatePolicyCenterSite(Uri siteUrl)
		{
			return new LocalizedString("VerboseTrytoCreatePolicyCenterSite", Strings.ResourceManager, new object[]
			{
				siteUrl
			});
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00034584 File Offset: 0x00032784
		public static LocalizedString ErrorPolicyNotFound(string policyParameter)
		{
			return new LocalizedString("ErrorPolicyNotFound", Strings.ResourceManager, new object[]
			{
				policyParameter
			});
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x000345AC File Offset: 0x000327AC
		public static LocalizedString SpParserVersionNotSpecified
		{
			get
			{
				return new LocalizedString("SpParserVersionNotSpecified", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x000345C4 File Offset: 0x000327C4
		public static LocalizedString ErrorRuleNotUnique(string ruleParameter)
		{
			return new LocalizedString("ErrorRuleNotUnique", Strings.ResourceManager, new object[]
			{
				ruleParameter
			});
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x000345EC File Offset: 0x000327EC
		public static LocalizedString DeviceConfigurationRuleAlreadyExists(string ruleName)
		{
			return new LocalizedString("DeviceConfigurationRuleAlreadyExists", Strings.ResourceManager, new object[]
			{
				ruleName
			});
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x00034614 File Offset: 0x00032814
		public static LocalizedString VerboseValidatingRemoveExchangeBinding
		{
			get
			{
				return new LocalizedString("VerboseValidatingRemoveExchangeBinding", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x0003462B File Offset: 0x0003282B
		public static LocalizedString InvalidHoldContentAction
		{
			get
			{
				return new LocalizedString("InvalidHoldContentAction", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00034644 File Offset: 0x00032844
		public static LocalizedString CompliancePolicyCountExceedsLimit(int limit)
		{
			return new LocalizedString("CompliancePolicyCountExceedsLimit", Strings.ResourceManager, new object[]
			{
				limit
			});
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00034674 File Offset: 0x00032874
		public static LocalizedString ExCannotContainWideScopeBindings(string binding)
		{
			return new LocalizedString("ExCannotContainWideScopeBindings", Strings.ResourceManager, new object[]
			{
				binding
			});
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0003469C File Offset: 0x0003289C
		public static LocalizedString RemoveDeviceTenantRuleConfirmation(string ruleName)
		{
			return new LocalizedString("RemoveDeviceTenantRuleConfirmation", Strings.ResourceManager, new object[]
			{
				ruleName
			});
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x000346C4 File Offset: 0x000328C4
		public static LocalizedString ErrorCommonComplianceRuleIsDeleted(string name)
		{
			return new LocalizedString("ErrorCommonComplianceRuleIsDeleted", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x000346EC File Offset: 0x000328EC
		public static LocalizedString CannotSetDeviceConfigurationPolicyWorkload
		{
			get
			{
				return new LocalizedString("CannotSetDeviceConfigurationPolicyWorkload", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00034704 File Offset: 0x00032904
		public static LocalizedString ErrorRuleNotFound(string ruleId)
		{
			return new LocalizedString("ErrorRuleNotFound", Strings.ResourceManager, new object[]
			{
				ruleId
			});
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x0003472C File Offset: 0x0003292C
		public static LocalizedString CanOnlyManipulateDeviceConfigurationRule
		{
			get
			{
				return new LocalizedString("CanOnlyManipulateDeviceConfigurationRule", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00034744 File Offset: 0x00032944
		public static LocalizedString VerboseValidatingSharepointBinding(string workload)
		{
			return new LocalizedString("VerboseValidatingSharepointBinding", Strings.ResourceManager, new object[]
			{
				workload
			});
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x0003476C File Offset: 0x0003296C
		public static LocalizedString MulipleSpBindingObjectDetected
		{
			get
			{
				return new LocalizedString("MulipleSpBindingObjectDetected", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00034784 File Offset: 0x00032984
		public static LocalizedString RemoveCompliancePolicyConfirmation(string policyName)
		{
			return new LocalizedString("RemoveCompliancePolicyConfirmation", Strings.ResourceManager, new object[]
			{
				policyName
			});
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x000347AC File Offset: 0x000329AC
		public static LocalizedString ErrorInvalidDeltaSyncAndFullSyncType(string types)
		{
			return new LocalizedString("ErrorInvalidDeltaSyncAndFullSyncType", Strings.ResourceManager, new object[]
			{
				types
			});
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x000347D4 File Offset: 0x000329D4
		public static LocalizedString ErrorNeedOrganizationId
		{
			get
			{
				return new LocalizedString("ErrorNeedOrganizationId", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x000347EC File Offset: 0x000329EC
		public static LocalizedString ErrorCannotInitializeNotificationClientToSharePoint(Uri spSiteUrl, Uri spAdminSiteUrl, Uri syncSvcUrl)
		{
			return new LocalizedString("ErrorCannotInitializeNotificationClientToSharePoint", Strings.ResourceManager, new object[]
			{
				spSiteUrl,
				spAdminSiteUrl,
				syncSvcUrl
			});
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x0003481C File Offset: 0x00032A1C
		public static LocalizedString CanOnlyChangeDeviceTenantPolicy
		{
			get
			{
				return new LocalizedString("CanOnlyChangeDeviceTenantPolicy", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x00034833 File Offset: 0x00032A33
		public static LocalizedString SkippingInvalidTypeInGroupExpansion
		{
			get
			{
				return new LocalizedString("SkippingInvalidTypeInGroupExpansion", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x0003484A File Offset: 0x00032A4A
		public static LocalizedString FailedToGetExecutingUser
		{
			get
			{
				return new LocalizedString("FailedToGetExecutingUser", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x00034861 File Offset: 0x00032A61
		public static LocalizedString SensitiveInformationDoesNotContainId
		{
			get
			{
				return new LocalizedString("SensitiveInformationDoesNotContainId", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x00034878 File Offset: 0x00032A78
		public static LocalizedString CannotChangeAuditConfigurationRuleName
		{
			get
			{
				return new LocalizedString("CannotChangeAuditConfigurationRuleName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00034890 File Offset: 0x00032A90
		public static LocalizedString ErrorAddRemoveExBindingsOverlapped(string bindings)
		{
			return new LocalizedString("ErrorAddRemoveExBindingsOverlapped", Strings.ResourceManager, new object[]
			{
				bindings
			});
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x000348B8 File Offset: 0x00032AB8
		public static LocalizedString WarningTaskRuleIsTooAdvancedToRead(string identity)
		{
			return new LocalizedString("WarningTaskRuleIsTooAdvancedToRead", Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x000348E0 File Offset: 0x00032AE0
		public static LocalizedString VerboseNotifyWorkloadWithChanges(string workload, string changes)
		{
			return new LocalizedString("VerboseNotifyWorkloadWithChanges", Strings.ResourceManager, new object[]
			{
				workload,
				changes
			});
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x0003490C File Offset: 0x00032B0C
		public static LocalizedString PolicySyncTimeoutErrorMsg
		{
			get
			{
				return new LocalizedString("PolicySyncTimeoutErrorMsg", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00034924 File Offset: 0x00032B24
		public static LocalizedString DiagnoseMissingStatusForScope(DateTime whenChanged)
		{
			return new LocalizedString("DiagnoseMissingStatusForScope", Strings.ResourceManager, new object[]
			{
				whenChanged
			});
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00034954 File Offset: 0x00032B54
		public static LocalizedString ErrorCannotInitializeNotificationClientToExchange(Uri pswsHostUrl, Uri syncSvcUrl)
		{
			return new LocalizedString("ErrorCannotInitializeNotificationClientToExchange", Strings.ResourceManager, new object[]
			{
				pswsHostUrl,
				syncSvcUrl
			});
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00034980 File Offset: 0x00032B80
		public static LocalizedString SpLocationValidationFailed(string url)
		{
			return new LocalizedString("SpLocationValidationFailed", Strings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x000349A8 File Offset: 0x00032BA8
		public static LocalizedString VerboseEndCalculatePolicyDistributionStatus(string name, string state, int errorCount, int timeoutErrorCount)
		{
			return new LocalizedString("VerboseEndCalculatePolicyDistributionStatus", Strings.ResourceManager, new object[]
			{
				name,
				state,
				errorCount,
				timeoutErrorCount
			});
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x000349E8 File Offset: 0x00032BE8
		public static LocalizedString DiagnosePendingStatusTimeout(DateTime whenChanged, TimeSpan timeout)
		{
			return new LocalizedString("DiagnosePendingStatusTimeout", Strings.ResourceManager, new object[]
			{
				whenChanged,
				timeout
			});
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00034A20 File Offset: 0x00032C20
		public static LocalizedString WarningNotificationClientIsMissing(string workload)
		{
			return new LocalizedString("WarningNotificationClientIsMissing", Strings.ResourceManager, new object[]
			{
				workload
			});
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x00034A48 File Offset: 0x00032C48
		public static LocalizedString SensitiveInformationNotFound(string identity)
		{
			return new LocalizedString("SensitiveInformationNotFound", Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x00034A70 File Offset: 0x00032C70
		public static LocalizedString MulipleExBindingObjectDetected
		{
			get
			{
				return new LocalizedString("MulipleExBindingObjectDetected", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x00034A88 File Offset: 0x00032C88
		public static LocalizedString GroupsIsNotAllowedForHold(string group)
		{
			return new LocalizedString("GroupsIsNotAllowedForHold", Strings.ResourceManager, new object[]
			{
				group
			});
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x00034AB0 File Offset: 0x00032CB0
		public static LocalizedString RemoveDeviceConfiguationRuleConfirmation(string ruleName)
		{
			return new LocalizedString("RemoveDeviceConfiguationRuleConfirmation", Strings.ResourceManager, new object[]
			{
				ruleName
			});
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x00034AD8 File Offset: 0x00032CD8
		public static LocalizedString CannotManipulateAuditConfigurationPolicy
		{
			get
			{
				return new LocalizedString("CannotManipulateAuditConfigurationPolicy", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00034AF0 File Offset: 0x00032CF0
		public static LocalizedString DisplayRuleName(string name)
		{
			return new LocalizedString("DisplayRuleName", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00034B18 File Offset: 0x00032D18
		public static LocalizedString RemoveComplianceRuleConfirmation(string ruleName)
		{
			return new LocalizedString("RemoveComplianceRuleConfirmation", Strings.ResourceManager, new object[]
			{
				ruleName
			});
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00034B40 File Offset: 0x00032D40
		public static LocalizedString VerboseLoadBindingStorageObjects(string bindingObject, string policy)
		{
			return new LocalizedString("VerboseLoadBindingStorageObjects", Strings.ResourceManager, new object[]
			{
				bindingObject,
				policy
			});
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00034B6C File Offset: 0x00032D6C
		public static LocalizedString DeviceConditionalAccessRuleAlreadyExists(string ruleName)
		{
			return new LocalizedString("DeviceConditionalAccessRuleAlreadyExists", Strings.ResourceManager, new object[]
			{
				ruleName
			});
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00034B94 File Offset: 0x00032D94
		public static LocalizedString SpParserUnexpectedNumberOfTokens(int version, int expected, int actual)
		{
			return new LocalizedString("SpParserUnexpectedNumberOfTokens", Strings.ResourceManager, new object[]
			{
				version,
				expected,
				actual
			});
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00034BD4 File Offset: 0x00032DD4
		public static LocalizedString BindingCannotCombineAllWithIndividualBindings(string workLoad)
		{
			return new LocalizedString("BindingCannotCombineAllWithIndividualBindings", Strings.ResourceManager, new object[]
			{
				workLoad
			});
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00034BFC File Offset: 0x00032DFC
		public static LocalizedString CompliancePolicyAlreadyExists(string policyName)
		{
			return new LocalizedString("CompliancePolicyAlreadyExists", Strings.ResourceManager, new object[]
			{
				policyName
			});
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x00034C24 File Offset: 0x00032E24
		public static LocalizedString CanOnlyManipulateDeviceTenantRule
		{
			get
			{
				return new LocalizedString("CanOnlyManipulateDeviceTenantRule", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00034C3C File Offset: 0x00032E3C
		public static LocalizedString MulipleComplianceRulesFoundInPolicy(string policy)
		{
			return new LocalizedString("MulipleComplianceRulesFoundInPolicy", Strings.ResourceManager, new object[]
			{
				policy
			});
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x00034C64 File Offset: 0x00032E64
		public static LocalizedString ErrorPolicyNotUnique(string policyParameter)
		{
			return new LocalizedString("ErrorPolicyNotUnique", Strings.ResourceManager, new object[]
			{
				policyParameter
			});
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00034C8C File Offset: 0x00032E8C
		public static LocalizedString VerboseExNotificationClientInfo(Uri pswsHostUrl, Uri syncSvcUrl, string credentialType)
		{
			return new LocalizedString("VerboseExNotificationClientInfo", Strings.ResourceManager, new object[]
			{
				pswsHostUrl,
				syncSvcUrl,
				credentialType
			});
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x00034CBC File Offset: 0x00032EBC
		public static LocalizedString ErrorSpBindingWithoutSpWorkload
		{
			get
			{
				return new LocalizedString("ErrorSpBindingWithoutSpWorkload", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x00034CD3 File Offset: 0x00032ED3
		public static LocalizedString CannotManipulateDeviceConfigurationRule
		{
			get
			{
				return new LocalizedString("CannotManipulateDeviceConfigurationRule", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00034CEC File Offset: 0x00032EEC
		public static LocalizedString WarningNotifyWorkloadFailed(string workload)
		{
			return new LocalizedString("WarningNotifyWorkloadFailed", Strings.ResourceManager, new object[]
			{
				workload
			});
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00034D14 File Offset: 0x00032F14
		public static LocalizedString ErrorInvalidRecipientType(string recipientName, string recipientType)
		{
			return new LocalizedString("ErrorInvalidRecipientType", Strings.ResourceManager, new object[]
			{
				recipientName,
				recipientType
			});
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00034D40 File Offset: 0x00032F40
		public static LocalizedString ErrorInvalidPolicyCenterSiteUrl(string policyCenterSiteUrlStr)
		{
			return new LocalizedString("ErrorInvalidPolicyCenterSiteUrl", Strings.ResourceManager, new object[]
			{
				policyCenterSiteUrlStr
			});
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00034D68 File Offset: 0x00032F68
		public static LocalizedString ErrorInvalidObjectSyncType(string types)
		{
			return new LocalizedString("ErrorInvalidObjectSyncType", Strings.ResourceManager, new object[]
			{
				types
			});
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x00034D90 File Offset: 0x00032F90
		public static LocalizedString ErrorExBindingWithoutExWorkload
		{
			get
			{
				return new LocalizedString("ErrorExBindingWithoutExWorkload", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00034DA8 File Offset: 0x00032FA8
		public static LocalizedString BindingCountExceedsLimit(string workLoad, int limit)
		{
			return new LocalizedString("BindingCountExceedsLimit", Strings.ResourceManager, new object[]
			{
				workLoad,
				limit
			});
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x00034DD9 File Offset: 0x00032FD9
		public static LocalizedString CanOnlyManipulateDeviceConditionalAccessRule
		{
			get
			{
				return new LocalizedString("CanOnlyManipulateDeviceConditionalAccessRule", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00034DF0 File Offset: 0x00032FF0
		public static LocalizedString VerboseRetryDistributionNotificationDetails(string id, string objectType, string changeType)
		{
			return new LocalizedString("VerboseRetryDistributionNotificationDetails", Strings.ResourceManager, new object[]
			{
				id,
				objectType,
				changeType
			});
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00034E20 File Offset: 0x00033020
		public static LocalizedString SpLocationHasMultipleSites(string url)
		{
			return new LocalizedString("SpLocationHasMultipleSites", Strings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x00034E48 File Offset: 0x00033048
		public static LocalizedString DeploymentFailureWithNoImpact
		{
			get
			{
				return new LocalizedString("DeploymentFailureWithNoImpact", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00034E60 File Offset: 0x00033060
		public static LocalizedString PolicyAndIdentityParameterUsedTogether(string policy, string identity)
		{
			return new LocalizedString("PolicyAndIdentityParameterUsedTogether", Strings.ResourceManager, new object[]
			{
				policy,
				identity
			});
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x00034E8C File Offset: 0x0003308C
		public static LocalizedString FailedToGetSpSiteUrlForTenant
		{
			get
			{
				return new LocalizedString("FailedToGetSpSiteUrlForTenant", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00034EA4 File Offset: 0x000330A4
		public static LocalizedString VerboseTryLoadPolicyCenterSite(Uri siteUrl)
		{
			return new LocalizedString("VerboseTryLoadPolicyCenterSite", Strings.ResourceManager, new object[]
			{
				siteUrl
			});
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00034ECC File Offset: 0x000330CC
		public static LocalizedString ErrorMaxSiteLimit(int limit, int actual)
		{
			return new LocalizedString("ErrorMaxSiteLimit", Strings.ResourceManager, new object[]
			{
				limit,
				actual
			});
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x00034F02 File Offset: 0x00033102
		public static LocalizedString InvalidCombinationOfCompliancePolicyTypeAndWorkload
		{
			get
			{
				return new LocalizedString("InvalidCombinationOfCompliancePolicyTypeAndWorkload", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x00034F1C File Offset: 0x0003311C
		public static LocalizedString SpParserVersionNotSupported(int version)
		{
			return new LocalizedString("SpParserVersionNotSupported", Strings.ResourceManager, new object[]
			{
				version
			});
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00034F4C File Offset: 0x0003314C
		public static LocalizedString WarningFailurePublishingStatus(string error)
		{
			return new LocalizedString("WarningFailurePublishingStatus", Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x00034F74 File Offset: 0x00033174
		public static LocalizedString SpParserInvalidSiteUrl(string siteUrl)
		{
			return new LocalizedString("SpParserInvalidSiteUrl", Strings.ResourceManager, new object[]
			{
				siteUrl
			});
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x00034F9C File Offset: 0x0003319C
		public static LocalizedString FailedToGetCredentialsForTenant
		{
			get
			{
				return new LocalizedString("FailedToGetCredentialsForTenant", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00034FB4 File Offset: 0x000331B4
		public static LocalizedString WarningPolicyContainsDisabledRules(string policyName)
		{
			return new LocalizedString("WarningPolicyContainsDisabledRules", Strings.ResourceManager, new object[]
			{
				policyName
			});
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x00034FDC File Offset: 0x000331DC
		public static LocalizedString InvalidContentPropertyContainsWordsPredicate
		{
			get
			{
				return new LocalizedString("InvalidContentPropertyContainsWordsPredicate", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00034FF4 File Offset: 0x000331F4
		public static LocalizedString ErrorAddRemoveSpBindingsOverlapped(string bindings)
		{
			return new LocalizedString("ErrorAddRemoveSpBindingsOverlapped", Strings.ResourceManager, new object[]
			{
				bindings
			});
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0003501C File Offset: 0x0003321C
		public static LocalizedString VerboseCreateNotificationClient(string endPoint)
		{
			return new LocalizedString("VerboseCreateNotificationClient", Strings.ResourceManager, new object[]
			{
				endPoint
			});
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00035044 File Offset: 0x00033244
		public static LocalizedString ErrorUserObjectNotFound(string id)
		{
			return new LocalizedString("ErrorUserObjectNotFound", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0003506C File Offset: 0x0003326C
		public static LocalizedString ErrorMaxMailboxLimit(int limit, int actual)
		{
			return new LocalizedString("ErrorMaxMailboxLimit", Strings.ResourceManager, new object[]
			{
				limit,
				actual
			});
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x000350A4 File Offset: 0x000332A4
		public static LocalizedString SpParserInvalidWebId(string webId)
		{
			return new LocalizedString("SpParserInvalidWebId", Strings.ResourceManager, new object[]
			{
				webId
			});
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x000350CC File Offset: 0x000332CC
		public static LocalizedString VerboseValidatingAddExchangeBinding
		{
			get
			{
				return new LocalizedString("VerboseValidatingAddExchangeBinding", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x000350E4 File Offset: 0x000332E4
		public static LocalizedString VerboseDumpStatusObject(string workload, string objectType, string objectId, string objectVersion, string statusErrorCode, string statusVersion)
		{
			return new LocalizedString("VerboseDumpStatusObject", Strings.ResourceManager, new object[]
			{
				workload,
				objectType,
				objectId,
				objectVersion,
				statusErrorCode,
				statusVersion
			});
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x00035122 File Offset: 0x00033322
		public static LocalizedString CanOnlyManipulateAuditConfigurationPolicy
		{
			get
			{
				return new LocalizedString("CanOnlyManipulateAuditConfigurationPolicy", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0003513C File Offset: 0x0003333C
		public static LocalizedString ErrorMaxMailboxLimitReachedInGroupExpansion(int limit)
		{
			return new LocalizedString("ErrorMaxMailboxLimitReachedInGroupExpansion", Strings.ResourceManager, new object[]
			{
				limit
			});
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0003516C File Offset: 0x0003336C
		public static LocalizedString ComplianceRuleAlreadyExists(string ruleName)
		{
			return new LocalizedString("ComplianceRuleAlreadyExists", Strings.ResourceManager, new object[]
			{
				ruleName
			});
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x00035194 File Offset: 0x00033394
		public static LocalizedString VerboseRetryDistributionNotApplicable
		{
			get
			{
				return new LocalizedString("VerboseRetryDistributionNotApplicable", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x000351AB File Offset: 0x000333AB
		public static LocalizedString UnknownErrorMsg
		{
			get
			{
				return new LocalizedString("UnknownErrorMsg", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x000351C2 File Offset: 0x000333C2
		public static LocalizedString AuditConfigurationPolicyNotAllowed
		{
			get
			{
				return new LocalizedString("AuditConfigurationPolicyNotAllowed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x000351D9 File Offset: 0x000333D9
		public static LocalizedString CanOnlyManipulateAuditConfigurationRule
		{
			get
			{
				return new LocalizedString("CanOnlyManipulateAuditConfigurationRule", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x000351F0 File Offset: 0x000333F0
		public static LocalizedString VerboseSaveBindingStorageObjects(string bindingObject, string policy)
		{
			return new LocalizedString("VerboseSaveBindingStorageObjects", Strings.ResourceManager, new object[]
			{
				bindingObject,
				policy
			});
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0003521C File Offset: 0x0003341C
		public static LocalizedString DeviceTenantRuleAlreadyExists(string ruleName)
		{
			return new LocalizedString("DeviceTenantRuleAlreadyExists", Strings.ResourceManager, new object[]
			{
				ruleName
			});
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x00035244 File Offset: 0x00033444
		public static LocalizedString DeviceConfigurationPolicyNotAllowed
		{
			get
			{
				return new LocalizedString("DeviceConfigurationPolicyNotAllowed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0003525C File Offset: 0x0003345C
		public static LocalizedString DisplayPolicyName(string name)
		{
			return new LocalizedString("DisplayPolicyName", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x00035284 File Offset: 0x00033484
		public static LocalizedString ErrorUserObjectAmbiguous(string id)
		{
			return new LocalizedString("ErrorUserObjectAmbiguous", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x000352AC File Offset: 0x000334AC
		public static LocalizedString SpParserUnexpectedContainerType(string expected, string actual)
		{
			return new LocalizedString("SpParserUnexpectedContainerType", Strings.ResourceManager, new object[]
			{
				expected,
				actual
			});
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x000352D8 File Offset: 0x000334D8
		public static LocalizedString CannotManipulateAuditConfigurationRule
		{
			get
			{
				return new LocalizedString("CannotManipulateAuditConfigurationRule", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x000352F0 File Offset: 0x000334F0
		public static LocalizedString ErrorSharePointCallFailed(string reason)
		{
			return new LocalizedString("ErrorSharePointCallFailed", Strings.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x00035318 File Offset: 0x00033518
		public static LocalizedString ErrorTaskPolicyIsTooAdvancedToModify(string identity)
		{
			return new LocalizedString("ErrorTaskPolicyIsTooAdvancedToModify", Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00035340 File Offset: 0x00033540
		public static LocalizedString VerboseTrytoCheckSiteExistence(Uri siteUrl, string error)
		{
			return new LocalizedString("VerboseTrytoCheckSiteExistence", Strings.ResourceManager, new object[]
			{
				siteUrl,
				error
			});
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x0003536C File Offset: 0x0003356C
		public static LocalizedString SiteInReadonlyOrNotAccessibleErrorMsg
		{
			get
			{
				return new LocalizedString("SiteInReadonlyOrNotAccessibleErrorMsg", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00035384 File Offset: 0x00033584
		public static LocalizedString VerboseDeleteBindingStorageObjects(string bindingObject, string policy)
		{
			return new LocalizedString("VerboseDeleteBindingStorageObjects", Strings.ResourceManager, new object[]
			{
				bindingObject,
				policy
			});
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x000353B0 File Offset: 0x000335B0
		public static LocalizedString ErrorCompliancePolicyIsDeleted(string name)
		{
			return new LocalizedString("ErrorCompliancePolicyIsDeleted", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x000353D8 File Offset: 0x000335D8
		public static LocalizedString VerboseRetryDistributionNotifyingWorkload(string workload, string syncLevel)
		{
			return new LocalizedString("VerboseRetryDistributionNotifyingWorkload", Strings.ResourceManager, new object[]
			{
				workload,
				syncLevel
			});
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00035404 File Offset: 0x00033604
		public static LocalizedString VerboseLoadRuleStorageObjectsForPolicy(string ruleObject, string policy)
		{
			return new LocalizedString("VerboseLoadRuleStorageObjectsForPolicy", Strings.ResourceManager, new object[]
			{
				ruleObject,
				policy
			});
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06000EAB RID: 3755 RVA: 0x00035430 File Offset: 0x00033630
		public static LocalizedString InvalidSensitiveInformationParameterValue
		{
			get
			{
				return new LocalizedString("InvalidSensitiveInformationParameterValue", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x00035447 File Offset: 0x00033647
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x040005C5 RID: 1477
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(58);

		// Token: 0x040005C6 RID: 1478
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Management.Transport.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000153 RID: 339
		public enum IDs : uint
		{
			// Token: 0x040005C8 RID: 1480
			ErrorOnlyAllowInEop = 2376986056U,
			// Token: 0x040005C9 RID: 1481
			InvalidContentDateFromAndContentDateToPredicate = 2845151070U,
			// Token: 0x040005CA RID: 1482
			PolicyNotifyErrorErrorMsg = 3231761178U,
			// Token: 0x040005CB RID: 1483
			CannotChangeDeviceConfigurationPolicyWorkload = 1183239240U,
			// Token: 0x040005CC RID: 1484
			CannotChangeDeviceConditionalAccessRuleName = 2160431466U,
			// Token: 0x040005CD RID: 1485
			VerboseValidatingAddSharepointBinding = 857423920U,
			// Token: 0x040005CE RID: 1486
			UnexpectedConditionOrActionDetected = 629789999U,
			// Token: 0x040005CF RID: 1487
			SiteOutOfQuotaErrorMsg = 107430575U,
			// Token: 0x040005D0 RID: 1488
			CanOnlyChangeDeviceConditionalAccessPolicy = 464180248U,
			// Token: 0x040005D1 RID: 1489
			InvalidCompliancePolicyWorkload = 3201126371U,
			// Token: 0x040005D2 RID: 1490
			VerboseValidatingExchangeBinding = 178440487U,
			// Token: 0x040005D3 RID: 1491
			InvalidAuditRuleWorkload = 1412691609U,
			// Token: 0x040005D4 RID: 1492
			CanOnlyChangeDeviceConfigurationPolicy = 4074170900U,
			// Token: 0x040005D5 RID: 1493
			VerboseValidatingRemoveSharepointBinding = 1306124383U,
			// Token: 0x040005D6 RID: 1494
			CannotChangeDeviceConfigurationPolicyScenario = 1508525573U,
			// Token: 0x040005D7 RID: 1495
			ShouldExpandGroups = 3499233439U,
			// Token: 0x040005D8 RID: 1496
			InvalidDeviceRuleWorkload = 1830331352U,
			// Token: 0x040005D9 RID: 1497
			ErrorInvalidPolicyCenterSiteOwner = 1930430118U,
			// Token: 0x040005DA RID: 1498
			CannotChangeAuditConfigurationRuleWorkload = 1713451535U,
			// Token: 0x040005DB RID: 1499
			FailedToOpenContainerErrorMsg = 1005978434U,
			// Token: 0x040005DC RID: 1500
			CannotChangeDeviceTenantRuleName = 2981356586U,
			// Token: 0x040005DD RID: 1501
			CannotChangeDeviceConfigurationRuleName = 228813078U,
			// Token: 0x040005DE RID: 1502
			InvalidAccessScopeIsPredicate = 4091585218U,
			// Token: 0x040005DF RID: 1503
			SpParserVersionNotSpecified = 2039724013U,
			// Token: 0x040005E0 RID: 1504
			VerboseValidatingRemoveExchangeBinding = 1066947153U,
			// Token: 0x040005E1 RID: 1505
			InvalidHoldContentAction = 317700209U,
			// Token: 0x040005E2 RID: 1506
			CannotSetDeviceConfigurationPolicyWorkload = 2522696186U,
			// Token: 0x040005E3 RID: 1507
			CanOnlyManipulateDeviceConfigurationRule = 167859032U,
			// Token: 0x040005E4 RID: 1508
			MulipleSpBindingObjectDetected = 82360181U,
			// Token: 0x040005E5 RID: 1509
			ErrorNeedOrganizationId = 605330666U,
			// Token: 0x040005E6 RID: 1510
			CanOnlyChangeDeviceTenantPolicy = 2976502162U,
			// Token: 0x040005E7 RID: 1511
			SkippingInvalidTypeInGroupExpansion = 631692221U,
			// Token: 0x040005E8 RID: 1512
			FailedToGetExecutingUser = 1853496069U,
			// Token: 0x040005E9 RID: 1513
			SensitiveInformationDoesNotContainId = 1582525285U,
			// Token: 0x040005EA RID: 1514
			CannotChangeAuditConfigurationRuleName = 639724051U,
			// Token: 0x040005EB RID: 1515
			PolicySyncTimeoutErrorMsg = 1614982109U,
			// Token: 0x040005EC RID: 1516
			MulipleExBindingObjectDetected = 3781430831U,
			// Token: 0x040005ED RID: 1517
			CannotManipulateAuditConfigurationPolicy = 766476708U,
			// Token: 0x040005EE RID: 1518
			CanOnlyManipulateDeviceTenantRule = 2985777218U,
			// Token: 0x040005EF RID: 1519
			ErrorSpBindingWithoutSpWorkload = 1450120914U,
			// Token: 0x040005F0 RID: 1520
			CannotManipulateDeviceConfigurationRule = 532439181U,
			// Token: 0x040005F1 RID: 1521
			ErrorExBindingWithoutExWorkload = 3642912594U,
			// Token: 0x040005F2 RID: 1522
			CanOnlyManipulateDeviceConditionalAccessRule = 2717805860U,
			// Token: 0x040005F3 RID: 1523
			DeploymentFailureWithNoImpact = 3278450928U,
			// Token: 0x040005F4 RID: 1524
			FailedToGetSpSiteUrlForTenant = 2628803538U,
			// Token: 0x040005F5 RID: 1525
			InvalidCombinationOfCompliancePolicyTypeAndWorkload = 1613362912U,
			// Token: 0x040005F6 RID: 1526
			FailedToGetCredentialsForTenant = 912843721U,
			// Token: 0x040005F7 RID: 1527
			InvalidContentPropertyContainsWordsPredicate = 3928915344U,
			// Token: 0x040005F8 RID: 1528
			VerboseValidatingAddExchangeBinding = 518140498U,
			// Token: 0x040005F9 RID: 1529
			CanOnlyManipulateAuditConfigurationPolicy = 1502130987U,
			// Token: 0x040005FA RID: 1530
			VerboseRetryDistributionNotApplicable = 574146044U,
			// Token: 0x040005FB RID: 1531
			UnknownErrorMsg = 4099235009U,
			// Token: 0x040005FC RID: 1532
			AuditConfigurationPolicyNotAllowed = 1327345872U,
			// Token: 0x040005FD RID: 1533
			CanOnlyManipulateAuditConfigurationRule = 445162545U,
			// Token: 0x040005FE RID: 1534
			DeviceConfigurationPolicyNotAllowed = 4174366101U,
			// Token: 0x040005FF RID: 1535
			CannotManipulateAuditConfigurationRule = 1615823280U,
			// Token: 0x04000600 RID: 1536
			SiteInReadonlyOrNotAccessibleErrorMsg = 1905427409U,
			// Token: 0x04000601 RID: 1537
			InvalidSensitiveInformationParameterValue = 2767511991U
		}

		// Token: 0x02000154 RID: 340
		private enum ParamIDs
		{
			// Token: 0x04000603 RID: 1539
			SpParserInvalidVersionType,
			// Token: 0x04000604 RID: 1540
			ErrorCreateSiteTimeOut,
			// Token: 0x04000605 RID: 1541
			VerboseBeginCalculatePolicyDistributionStatus,
			// Token: 0x04000606 RID: 1542
			ErrorMessageForNotificationFailure,
			// Token: 0x04000607 RID: 1543
			ErrorTaskRuleIsTooAdvancedToModify,
			// Token: 0x04000608 RID: 1544
			ErrorCompliancePolicyHasNoObjectsToRetry,
			// Token: 0x04000609 RID: 1545
			ErrorCannotCreateRuleUnderPendingDeletionPolicy,
			// Token: 0x0400060A RID: 1546
			VerbosePolicyStorageObjectLoadedForCommonRule,
			// Token: 0x0400060B RID: 1547
			VerboseDeletePolicyStorageBaseObject,
			// Token: 0x0400060C RID: 1548
			RemoveDeviceConditionalAccessRuleConfirmation,
			// Token: 0x0400060D RID: 1549
			WarningTaskPolicyIsTooAdvancedToRead,
			// Token: 0x0400060E RID: 1550
			DisplayBindingName,
			// Token: 0x0400060F RID: 1551
			VerboseTreatAsWarning,
			// Token: 0x04000610 RID: 1552
			SpParserInvalidSiteId,
			// Token: 0x04000611 RID: 1553
			ErrorMultipleObjectTypeForObjectLevelSync,
			// Token: 0x04000612 RID: 1554
			WarningDisabledRuleInEnabledPolicy,
			// Token: 0x04000613 RID: 1555
			ErrorRuleContainsNoActions,
			// Token: 0x04000614 RID: 1556
			ErrorCompliancePolicySyncNotificationClient,
			// Token: 0x04000615 RID: 1557
			VerboseTrytoCheckSiteDeletedState,
			// Token: 0x04000616 RID: 1558
			WarningInvalidTenant,
			// Token: 0x04000617 RID: 1559
			VerboseNotifyWorkloadWithChangesSuccess,
			// Token: 0x04000618 RID: 1560
			ErrorRulesInPolicyIsTooAdvancedToModify,
			// Token: 0x04000619 RID: 1561
			VerbosePolicyCenterSiteOwner,
			// Token: 0x0400061A RID: 1562
			ErrorCannotRemovePendingDeletionRule,
			// Token: 0x0400061B RID: 1563
			ErrorSavingPolicyToWorkload,
			// Token: 0x0400061C RID: 1564
			VerbosePolicyStorageObjectLoaded,
			// Token: 0x0400061D RID: 1565
			ErrorCannotRemovePendingDeletionPolicy,
			// Token: 0x0400061E RID: 1566
			InvalidSensitiveInformationParameterName,
			// Token: 0x0400061F RID: 1567
			VerboseSpNotificationClientInfo,
			// Token: 0x04000620 RID: 1568
			VerboseTrytoCreatePolicyCenterSite,
			// Token: 0x04000621 RID: 1569
			ErrorPolicyNotFound,
			// Token: 0x04000622 RID: 1570
			ErrorRuleNotUnique,
			// Token: 0x04000623 RID: 1571
			DeviceConfigurationRuleAlreadyExists,
			// Token: 0x04000624 RID: 1572
			CompliancePolicyCountExceedsLimit,
			// Token: 0x04000625 RID: 1573
			ExCannotContainWideScopeBindings,
			// Token: 0x04000626 RID: 1574
			RemoveDeviceTenantRuleConfirmation,
			// Token: 0x04000627 RID: 1575
			ErrorCommonComplianceRuleIsDeleted,
			// Token: 0x04000628 RID: 1576
			ErrorRuleNotFound,
			// Token: 0x04000629 RID: 1577
			VerboseValidatingSharepointBinding,
			// Token: 0x0400062A RID: 1578
			RemoveCompliancePolicyConfirmation,
			// Token: 0x0400062B RID: 1579
			ErrorInvalidDeltaSyncAndFullSyncType,
			// Token: 0x0400062C RID: 1580
			ErrorCannotInitializeNotificationClientToSharePoint,
			// Token: 0x0400062D RID: 1581
			ErrorAddRemoveExBindingsOverlapped,
			// Token: 0x0400062E RID: 1582
			WarningTaskRuleIsTooAdvancedToRead,
			// Token: 0x0400062F RID: 1583
			VerboseNotifyWorkloadWithChanges,
			// Token: 0x04000630 RID: 1584
			DiagnoseMissingStatusForScope,
			// Token: 0x04000631 RID: 1585
			ErrorCannotInitializeNotificationClientToExchange,
			// Token: 0x04000632 RID: 1586
			SpLocationValidationFailed,
			// Token: 0x04000633 RID: 1587
			VerboseEndCalculatePolicyDistributionStatus,
			// Token: 0x04000634 RID: 1588
			DiagnosePendingStatusTimeout,
			// Token: 0x04000635 RID: 1589
			WarningNotificationClientIsMissing,
			// Token: 0x04000636 RID: 1590
			SensitiveInformationNotFound,
			// Token: 0x04000637 RID: 1591
			GroupsIsNotAllowedForHold,
			// Token: 0x04000638 RID: 1592
			RemoveDeviceConfiguationRuleConfirmation,
			// Token: 0x04000639 RID: 1593
			DisplayRuleName,
			// Token: 0x0400063A RID: 1594
			RemoveComplianceRuleConfirmation,
			// Token: 0x0400063B RID: 1595
			VerboseLoadBindingStorageObjects,
			// Token: 0x0400063C RID: 1596
			DeviceConditionalAccessRuleAlreadyExists,
			// Token: 0x0400063D RID: 1597
			SpParserUnexpectedNumberOfTokens,
			// Token: 0x0400063E RID: 1598
			BindingCannotCombineAllWithIndividualBindings,
			// Token: 0x0400063F RID: 1599
			CompliancePolicyAlreadyExists,
			// Token: 0x04000640 RID: 1600
			MulipleComplianceRulesFoundInPolicy,
			// Token: 0x04000641 RID: 1601
			ErrorPolicyNotUnique,
			// Token: 0x04000642 RID: 1602
			VerboseExNotificationClientInfo,
			// Token: 0x04000643 RID: 1603
			WarningNotifyWorkloadFailed,
			// Token: 0x04000644 RID: 1604
			ErrorInvalidRecipientType,
			// Token: 0x04000645 RID: 1605
			ErrorInvalidPolicyCenterSiteUrl,
			// Token: 0x04000646 RID: 1606
			ErrorInvalidObjectSyncType,
			// Token: 0x04000647 RID: 1607
			BindingCountExceedsLimit,
			// Token: 0x04000648 RID: 1608
			VerboseRetryDistributionNotificationDetails,
			// Token: 0x04000649 RID: 1609
			SpLocationHasMultipleSites,
			// Token: 0x0400064A RID: 1610
			PolicyAndIdentityParameterUsedTogether,
			// Token: 0x0400064B RID: 1611
			VerboseTryLoadPolicyCenterSite,
			// Token: 0x0400064C RID: 1612
			ErrorMaxSiteLimit,
			// Token: 0x0400064D RID: 1613
			SpParserVersionNotSupported,
			// Token: 0x0400064E RID: 1614
			WarningFailurePublishingStatus,
			// Token: 0x0400064F RID: 1615
			SpParserInvalidSiteUrl,
			// Token: 0x04000650 RID: 1616
			WarningPolicyContainsDisabledRules,
			// Token: 0x04000651 RID: 1617
			ErrorAddRemoveSpBindingsOverlapped,
			// Token: 0x04000652 RID: 1618
			VerboseCreateNotificationClient,
			// Token: 0x04000653 RID: 1619
			ErrorUserObjectNotFound,
			// Token: 0x04000654 RID: 1620
			ErrorMaxMailboxLimit,
			// Token: 0x04000655 RID: 1621
			SpParserInvalidWebId,
			// Token: 0x04000656 RID: 1622
			VerboseDumpStatusObject,
			// Token: 0x04000657 RID: 1623
			ErrorMaxMailboxLimitReachedInGroupExpansion,
			// Token: 0x04000658 RID: 1624
			ComplianceRuleAlreadyExists,
			// Token: 0x04000659 RID: 1625
			VerboseSaveBindingStorageObjects,
			// Token: 0x0400065A RID: 1626
			DeviceTenantRuleAlreadyExists,
			// Token: 0x0400065B RID: 1627
			DisplayPolicyName,
			// Token: 0x0400065C RID: 1628
			ErrorUserObjectAmbiguous,
			// Token: 0x0400065D RID: 1629
			SpParserUnexpectedContainerType,
			// Token: 0x0400065E RID: 1630
			ErrorSharePointCallFailed,
			// Token: 0x0400065F RID: 1631
			ErrorTaskPolicyIsTooAdvancedToModify,
			// Token: 0x04000660 RID: 1632
			VerboseTrytoCheckSiteExistence,
			// Token: 0x04000661 RID: 1633
			VerboseDeleteBindingStorageObjects,
			// Token: 0x04000662 RID: 1634
			ErrorCompliancePolicyIsDeleted,
			// Token: 0x04000663 RID: 1635
			VerboseRetryDistributionNotifyingWorkload,
			// Token: 0x04000664 RID: 1636
			VerboseLoadRuleStorageObjectsForPolicy
		}
	}
}
