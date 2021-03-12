using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000FE RID: 254
	internal static class Strings
	{
		// Token: 0x06000834 RID: 2100 RVA: 0x00018F94 File Offset: 0x00017194
		static Strings()
		{
			Strings.stringIDs.Add(2677654617U, "InvalidAppId");
			Strings.stringIDs.Add(3805371211U, "AzureDeviceMissingPayload");
			Strings.stringIDs.Add(1268441430U, "ApnsChannelAuthenticationTimeout");
			Strings.stringIDs.Add(31607964U, "GcmInvalidPayload");
			Strings.stringIDs.Add(735444327U, "InvalidSubscriptionId");
			Strings.stringIDs.Add(121229349U, "ValidationErrorInvalidAuthenticationKey");
			Strings.stringIDs.Add(3217301529U, "AzureChallengeEmptyUriTemplate");
			Strings.stringIDs.Add(2677736001U, "InvalidWnsBadgeValue");
			Strings.stringIDs.Add(3272335588U, "WebAppInvalidPayload");
			Strings.stringIDs.Add(3255907000U, "AzureDeviceEmptyUriTemplate");
			Strings.stringIDs.Add(2799225028U, "AzureChallengeMissingPayload");
			Strings.stringIDs.Add(4047283153U, "AzureChallengeEmptySasKey");
			Strings.stringIDs.Add(734509627U, "CannotResolveProxyAppFromAD");
			Strings.stringIDs.Add(678895536U, "AzureDeviceEmptySasKey");
			Strings.stringIDs.Add(2472023412U, "WebAppInvalidAction");
			Strings.stringIDs.Add(4200803720U, "InvalidProxyNotificationBatch");
			Strings.stringIDs.Add(1133222470U, "InvalidEmailCount");
			Strings.stringIDs.Add(816696259U, "InvalidPayload");
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00019138 File Offset: 0x00017338
		public static LocalizedString InvalidPayloadFormat(string error)
		{
			return new LocalizedString("InvalidPayloadFormat", Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00019160 File Offset: 0x00017360
		public static LocalizedString AzureChallengeInvalidDeviceId(string deviceId)
		{
			return new LocalizedString("AzureChallengeInvalidDeviceId", Strings.ResourceManager, new object[]
			{
				deviceId
			});
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00019188 File Offset: 0x00017388
		public static LocalizedString InvalidWnsUriScheme(string uri)
		{
			return new LocalizedString("InvalidWnsUriScheme", Strings.ResourceManager, new object[]
			{
				uri
			});
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x000191B0 File Offset: 0x000173B0
		public static LocalizedString ValidationErrorRangeVersion(Version minimum, Version maximum)
		{
			return new LocalizedString("ValidationErrorRangeVersion", Strings.ResourceManager, new object[]
			{
				minimum,
				maximum
			});
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x000191DC File Offset: 0x000173DC
		public static LocalizedString InvalidPayloadLength(int length, string jsonPayload)
		{
			return new LocalizedString("InvalidPayloadLength", Strings.ResourceManager, new object[]
			{
				length,
				jsonPayload
			});
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x0001920D File Offset: 0x0001740D
		public static LocalizedString InvalidAppId
		{
			get
			{
				return new LocalizedString("InvalidAppId", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00019224 File Offset: 0x00017424
		public static LocalizedString InvalidReportFromApns(string status)
		{
			return new LocalizedString("InvalidReportFromApns", Strings.ResourceManager, new object[]
			{
				status
			});
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001924C File Offset: 0x0001744C
		public static LocalizedString ApnsFeedbackFileIdInvalidPseudoAppId(string serializedId)
		{
			return new LocalizedString("ApnsFeedbackFileIdInvalidPseudoAppId", Strings.ResourceManager, new object[]
			{
				serializedId
			});
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00019274 File Offset: 0x00017474
		public static LocalizedString AzureDeviceMissingPayload
		{
			get
			{
				return new LocalizedString("AzureDeviceMissingPayload", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0001928C File Offset: 0x0001748C
		public static LocalizedString AzureInvalidRecipientId(string recipientId)
		{
			return new LocalizedString("AzureInvalidRecipientId", Strings.ResourceManager, new object[]
			{
				recipientId
			});
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x000192B4 File Offset: 0x000174B4
		public static LocalizedString ApnsFeedbackFileIdInvalidDate(string serializedId, string error)
		{
			return new LocalizedString("ApnsFeedbackFileIdInvalidDate", Strings.ResourceManager, new object[]
			{
				serializedId,
				error
			});
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x000192E0 File Offset: 0x000174E0
		public static LocalizedString ApnsFeedbackFileIdInvalidCharacters(string serializedId, string error)
		{
			return new LocalizedString("ApnsFeedbackFileIdInvalidCharacters", Strings.ResourceManager, new object[]
			{
				serializedId,
				error
			});
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001930C File Offset: 0x0001750C
		public static LocalizedString ApnsFeedbackPackageFeedbackNotFound(string path)
		{
			return new LocalizedString("ApnsFeedbackPackageFeedbackNotFound", Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x00019334 File Offset: 0x00017534
		public static LocalizedString ApnsChannelAuthenticationTimeout
		{
			get
			{
				return new LocalizedString("ApnsChannelAuthenticationTimeout", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x0001934B File Offset: 0x0001754B
		public static LocalizedString GcmInvalidPayload
		{
			get
			{
				return new LocalizedString("GcmInvalidPayload", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00019364 File Offset: 0x00017564
		public static LocalizedString ApnsCertificatePrivateKeyError(string name, string thumbprint)
		{
			return new LocalizedString("ApnsCertificatePrivateKeyError", Strings.ResourceManager, new object[]
			{
				name,
				thumbprint
			});
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x00019390 File Offset: 0x00017590
		public static LocalizedString InvalidSubscriptionId
		{
			get
			{
				return new LocalizedString("InvalidSubscriptionId", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x000193A8 File Offset: 0x000175A8
		public static LocalizedString InvalidWnsPayloadLength(int maxLen, string payload)
		{
			return new LocalizedString("InvalidWnsPayloadLength", Strings.ResourceManager, new object[]
			{
				maxLen,
				payload
			});
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x000193DC File Offset: 0x000175DC
		public static LocalizedString ApnsFeedbackResponseInvalidLength(int length)
		{
			return new LocalizedString("ApnsFeedbackResponseInvalidLength", Strings.ResourceManager, new object[]
			{
				length
			});
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001940C File Offset: 0x0001760C
		public static LocalizedString ValidationErrorNonNegativeInteger(string propertyName, int value)
		{
			return new LocalizedString("ValidationErrorNonNegativeInteger", Strings.ResourceManager, new object[]
			{
				propertyName,
				value
			});
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00019440 File Offset: 0x00017640
		public static LocalizedString GcmInvalidNotificationReported(string report)
		{
			return new LocalizedString("GcmInvalidNotificationReported", Strings.ResourceManager, new object[]
			{
				report
			});
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00019468 File Offset: 0x00017668
		public static LocalizedString InvalidWnsUri(string uri, string error)
		{
			return new LocalizedString("InvalidWnsUri", Strings.ResourceManager, new object[]
			{
				uri,
				error
			});
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x00019494 File Offset: 0x00017694
		public static LocalizedString ValidationErrorInvalidAuthenticationKey
		{
			get
			{
				return new LocalizedString("ValidationErrorInvalidAuthenticationKey", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x000194AC File Offset: 0x000176AC
		public static LocalizedString ApnsFeedbackFileGetFilesError(string root, string searchPattern, string error)
		{
			return new LocalizedString("ApnsFeedbackFileGetFilesError", Strings.ResourceManager, new object[]
			{
				root,
				searchPattern,
				error
			});
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x000194DC File Offset: 0x000176DC
		public static LocalizedString ApnsFeedbackPackageRemovalFailed(string path, string error)
		{
			return new LocalizedString("ApnsFeedbackPackageRemovalFailed", Strings.ResourceManager, new object[]
			{
				path,
				error
			});
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00019508 File Offset: 0x00017708
		public static LocalizedString DataProtectionDecryptingError(string protectedText, string error)
		{
			return new LocalizedString("DataProtectionDecryptingError", Strings.ResourceManager, new object[]
			{
				protectedText,
				error
			});
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00019534 File Offset: 0x00017734
		public static LocalizedString ValidationErrorDeviceRegistrationAppId(string name, string expected)
		{
			return new LocalizedString("ValidationErrorDeviceRegistrationAppId", Strings.ResourceManager, new object[]
			{
				name,
				expected
			});
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x00019560 File Offset: 0x00017760
		public static LocalizedString AzureChallengeEmptyUriTemplate
		{
			get
			{
				return new LocalizedString("AzureChallengeEmptyUriTemplate", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x00019577 File Offset: 0x00017777
		public static LocalizedString InvalidWnsBadgeValue
		{
			get
			{
				return new LocalizedString("InvalidWnsBadgeValue", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00019590 File Offset: 0x00017790
		public static LocalizedString InvalidExpiration(int expiration)
		{
			return new LocalizedString("InvalidExpiration", Strings.ResourceManager, new object[]
			{
				expiration
			});
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x000195BD File Offset: 0x000177BD
		public static LocalizedString WebAppInvalidPayload
		{
			get
			{
				return new LocalizedString("WebAppInvalidPayload", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x000195D4 File Offset: 0x000177D4
		public static LocalizedString ApnsFeedbackFileIdInsufficientComponents(string serializedId)
		{
			return new LocalizedString("ApnsFeedbackFileIdInsufficientComponents", Strings.ResourceManager, new object[]
			{
				serializedId
			});
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x000195FC File Offset: 0x000177FC
		public static LocalizedString AzureDeviceInvalidTag(string tag)
		{
			return new LocalizedString("AzureDeviceInvalidTag", Strings.ResourceManager, new object[]
			{
				tag
			});
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x00019624 File Offset: 0x00017824
		public static LocalizedString AzureDeviceEmptyUriTemplate
		{
			get
			{
				return new LocalizedString("AzureDeviceEmptyUriTemplate", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001963C File Offset: 0x0001783C
		public static LocalizedString ApnsFeedbackPackageExtractionFailed(string path, string error)
		{
			return new LocalizedString("ApnsFeedbackPackageExtractionFailed", Strings.ResourceManager, new object[]
			{
				path,
				error
			});
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00019668 File Offset: 0x00017868
		public static LocalizedString ApnsFeedbackFileIdInvalidExtension(string extension)
		{
			return new LocalizedString("ApnsFeedbackFileIdInvalidExtension", Strings.ResourceManager, new object[]
			{
				extension
			});
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00019690 File Offset: 0x00017890
		public static LocalizedString ApnsFeedbackFileLoadError(string filePath, string error)
		{
			return new LocalizedString("ApnsFeedbackFileLoadError", Strings.ResourceManager, new object[]
			{
				filePath,
				error
			});
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x000196BC File Offset: 0x000178BC
		public static LocalizedString AzureChallengeMissingPayload
		{
			get
			{
				return new LocalizedString("AzureChallengeMissingPayload", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x000196D4 File Offset: 0x000178D4
		public static LocalizedString CannotCreateValidSasToken(string appId, string sasToken)
		{
			return new LocalizedString("CannotCreateValidSasToken", Strings.ResourceManager, new object[]
			{
				appId,
				sasToken
			});
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00019700 File Offset: 0x00017900
		public static LocalizedString ApnsPublisherExpiredToken(string notification)
		{
			return new LocalizedString("ApnsPublisherExpiredToken", Strings.ResourceManager, new object[]
			{
				notification
			});
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00019728 File Offset: 0x00017928
		public static LocalizedString InvalidWnsTimeToLive(int ttl)
		{
			return new LocalizedString("InvalidWnsTimeToLive", Strings.ResourceManager, new object[]
			{
				ttl
			});
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x00019755 File Offset: 0x00017955
		public static LocalizedString AzureChallengeEmptySasKey
		{
			get
			{
				return new LocalizedString("AzureChallengeEmptySasKey", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0001976C File Offset: 0x0001796C
		public static LocalizedString ValidationErrorEmptyString(string propertyName)
		{
			return new LocalizedString("ValidationErrorEmptyString", Strings.ResourceManager, new object[]
			{
				propertyName
			});
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00019794 File Offset: 0x00017994
		public static LocalizedString ApnsCertificateExternalException(string thumbprint, string errorMessage)
		{
			return new LocalizedString("ApnsCertificateExternalException", Strings.ResourceManager, new object[]
			{
				thumbprint,
				errorMessage
			});
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x000197C0 File Offset: 0x000179C0
		public static LocalizedString AzureDeviceInvalidDeviceId(string deviceId)
		{
			return new LocalizedString("AzureDeviceInvalidDeviceId", Strings.ResourceManager, new object[]
			{
				deviceId
			});
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000197E8 File Offset: 0x000179E8
		public static LocalizedString ValidationErrorInvalidSasToken(string sasToken)
		{
			return new LocalizedString("ValidationErrorInvalidSasToken", Strings.ResourceManager, new object[]
			{
				sasToken
			});
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x00019810 File Offset: 0x00017A10
		public static LocalizedString CannotResolveProxyAppFromAD
		{
			get
			{
				return new LocalizedString("CannotResolveProxyAppFromAD", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00019828 File Offset: 0x00017A28
		public static LocalizedString ValidationErrorRangeInteger(string propertyName, int lowest, int highest, int value)
		{
			return new LocalizedString("ValidationErrorRangeInteger", Strings.ResourceManager, new object[]
			{
				propertyName,
				lowest,
				highest,
				value
			});
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0001986C File Offset: 0x00017A6C
		public static LocalizedString AzureChallengeInvalidPlatformOnPayload(string platform)
		{
			return new LocalizedString("AzureChallengeInvalidPlatformOnPayload", Strings.ResourceManager, new object[]
			{
				platform
			});
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00019894 File Offset: 0x00017A94
		public static LocalizedString ApnsFeedbackError(string appId, string exceptionType, string errorMessage)
		{
			return new LocalizedString("ApnsFeedbackError", Strings.ResourceManager, new object[]
			{
				appId,
				exceptionType,
				errorMessage
			});
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000198C4 File Offset: 0x00017AC4
		public static LocalizedString GcmInvalidTimeToLive(int ttl)
		{
			return new LocalizedString("GcmInvalidTimeToLive", Strings.ResourceManager, new object[]
			{
				ttl
			});
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x000198F4 File Offset: 0x00017AF4
		public static LocalizedString ValidationErrorPositiveInteger(string propertyName, int value)
		{
			return new LocalizedString("ValidationErrorPositiveInteger", Strings.ResourceManager, new object[]
			{
				propertyName,
				value
			});
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00019928 File Offset: 0x00017B28
		public static LocalizedString NotificationDroppedDueToLastUpdateTime(string nextUpdate)
		{
			return new LocalizedString("NotificationDroppedDueToLastUpdateTime", Strings.ResourceManager, new object[]
			{
				nextUpdate
			});
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x00019950 File Offset: 0x00017B50
		public static LocalizedString AzureDeviceEmptySasKey
		{
			get
			{
				return new LocalizedString("AzureDeviceEmptySasKey", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00019968 File Offset: 0x00017B68
		public static LocalizedString ValidationErrorHubCreationAppId(string name, string expected)
		{
			return new LocalizedString("ValidationErrorHubCreationAppId", Strings.ResourceManager, new object[]
			{
				name,
				expected
			});
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00019994 File Offset: 0x00017B94
		public static LocalizedString DataProtectionEncryptingError(string error)
		{
			return new LocalizedString("DataProtectionEncryptingError", Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x000199BC File Offset: 0x00017BBC
		public static LocalizedString ApnsFeedbackResponseInvalidFileLine(string line, string error)
		{
			return new LocalizedString("ApnsFeedbackResponseInvalidFileLine", Strings.ResourceManager, new object[]
			{
				line,
				error
			});
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x000199E8 File Offset: 0x00017BE8
		public static LocalizedString ApnsFeedbackFileIdInvalidDirectory(string folderName)
		{
			return new LocalizedString("ApnsFeedbackFileIdInvalidDirectory", Strings.ResourceManager, new object[]
			{
				folderName
			});
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00019A10 File Offset: 0x00017C10
		public static LocalizedString ApnsCertificateNotFound(string thumbprint)
		{
			return new LocalizedString("ApnsCertificateNotFound", Strings.ResourceManager, new object[]
			{
				thumbprint
			});
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00019A38 File Offset: 0x00017C38
		public static LocalizedString InvalidWnsTemplate(string binding)
		{
			return new LocalizedString("InvalidWnsTemplate", Strings.ResourceManager, new object[]
			{
				binding
			});
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00019A60 File Offset: 0x00017C60
		public static LocalizedString AzureCannotResolveExternalOrgId(string error)
		{
			return new LocalizedString("AzureCannotResolveExternalOrgId", Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00019A88 File Offset: 0x00017C88
		public static LocalizedString ApnsFeedbackPackageUnexpectedMetadataResult(string path, int number)
		{
			return new LocalizedString("ApnsFeedbackPackageUnexpectedMetadataResult", Strings.ResourceManager, new object[]
			{
				path,
				number
			});
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00019ABC File Offset: 0x00017CBC
		public static LocalizedString ValidationErrorChallengeRequestAppId(string name, string expected)
		{
			return new LocalizedString("ValidationErrorChallengeRequestAppId", Strings.ResourceManager, new object[]
			{
				name,
				expected
			});
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00019AE8 File Offset: 0x00017CE8
		public static LocalizedString GcmInvalidPayloadLength(int currentLength, string payloadExtract)
		{
			return new LocalizedString("GcmInvalidPayloadLength", Strings.ResourceManager, new object[]
			{
				currentLength,
				payloadExtract
			});
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x00019B19 File Offset: 0x00017D19
		public static LocalizedString WebAppInvalidAction
		{
			get
			{
				return new LocalizedString("WebAppInvalidAction", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00019B30 File Offset: 0x00017D30
		public static LocalizedString ValidationErrorInvalidUri(string propertyName, string value, string extra)
		{
			return new LocalizedString("ValidationErrorInvalidUri", Strings.ResourceManager, new object[]
			{
				propertyName,
				value,
				extra
			});
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x00019B60 File Offset: 0x00017D60
		public static LocalizedString InvalidProxyNotificationBatch
		{
			get
			{
				return new LocalizedString("InvalidProxyNotificationBatch", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00019B78 File Offset: 0x00017D78
		public static LocalizedString InvalidAzurePayloadLength(int maxLen, string payload)
		{
			return new LocalizedString("InvalidAzurePayloadLength", Strings.ResourceManager, new object[]
			{
				maxLen,
				payload
			});
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00019BAC File Offset: 0x00017DAC
		public static LocalizedString GcmInvalidRegistrationId(string id)
		{
			return new LocalizedString("GcmInvalidRegistrationId", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00019BD4 File Offset: 0x00017DD4
		public static LocalizedString InvalidLastSubscriptionUpdate(string date)
		{
			return new LocalizedString("InvalidLastSubscriptionUpdate", Strings.ResourceManager, new object[]
			{
				date
			});
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00019BFC File Offset: 0x00017DFC
		public static LocalizedString InvalidWnsLanguage(string lang, string error)
		{
			return new LocalizedString("InvalidWnsLanguage", Strings.ResourceManager, new object[]
			{
				lang,
				error
			});
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x00019C28 File Offset: 0x00017E28
		public static LocalizedString InvalidEmailCount
		{
			get
			{
				return new LocalizedString("InvalidEmailCount", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00019C40 File Offset: 0x00017E40
		public static LocalizedString InvalidDeviceToken(string token)
		{
			return new LocalizedString("InvalidDeviceToken", Strings.ResourceManager, new object[]
			{
				token
			});
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00019C68 File Offset: 0x00017E68
		public static LocalizedString InvalidWnsDeviceUri(string uri, string error)
		{
			return new LocalizedString("InvalidWnsDeviceUri", Strings.ResourceManager, new object[]
			{
				uri,
				error
			});
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00019C94 File Offset: 0x00017E94
		public static LocalizedString InvalidWnsAttributeIsMandatory(string attributeName)
		{
			return new LocalizedString("InvalidWnsAttributeIsMandatory", Strings.ResourceManager, new object[]
			{
				attributeName
			});
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x00019CBC File Offset: 0x00017EBC
		public static LocalizedString InvalidPayload
		{
			get
			{
				return new LocalizedString("InvalidPayload", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00019CD4 File Offset: 0x00017ED4
		public static LocalizedString WnsChannelInvalidNotificationReported(string report)
		{
			return new LocalizedString("WnsChannelInvalidNotificationReported", Strings.ResourceManager, new object[]
			{
				report
			});
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00019CFC File Offset: 0x00017EFC
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000472 RID: 1138
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(18);

		// Token: 0x04000473 RID: 1139
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.PushNotifications.Publishers.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x020000FF RID: 255
		public enum IDs : uint
		{
			// Token: 0x04000475 RID: 1141
			InvalidAppId = 2677654617U,
			// Token: 0x04000476 RID: 1142
			AzureDeviceMissingPayload = 3805371211U,
			// Token: 0x04000477 RID: 1143
			ApnsChannelAuthenticationTimeout = 1268441430U,
			// Token: 0x04000478 RID: 1144
			GcmInvalidPayload = 31607964U,
			// Token: 0x04000479 RID: 1145
			InvalidSubscriptionId = 735444327U,
			// Token: 0x0400047A RID: 1146
			ValidationErrorInvalidAuthenticationKey = 121229349U,
			// Token: 0x0400047B RID: 1147
			AzureChallengeEmptyUriTemplate = 3217301529U,
			// Token: 0x0400047C RID: 1148
			InvalidWnsBadgeValue = 2677736001U,
			// Token: 0x0400047D RID: 1149
			WebAppInvalidPayload = 3272335588U,
			// Token: 0x0400047E RID: 1150
			AzureDeviceEmptyUriTemplate = 3255907000U,
			// Token: 0x0400047F RID: 1151
			AzureChallengeMissingPayload = 2799225028U,
			// Token: 0x04000480 RID: 1152
			AzureChallengeEmptySasKey = 4047283153U,
			// Token: 0x04000481 RID: 1153
			CannotResolveProxyAppFromAD = 734509627U,
			// Token: 0x04000482 RID: 1154
			AzureDeviceEmptySasKey = 678895536U,
			// Token: 0x04000483 RID: 1155
			WebAppInvalidAction = 2472023412U,
			// Token: 0x04000484 RID: 1156
			InvalidProxyNotificationBatch = 4200803720U,
			// Token: 0x04000485 RID: 1157
			InvalidEmailCount = 1133222470U,
			// Token: 0x04000486 RID: 1158
			InvalidPayload = 816696259U
		}

		// Token: 0x02000100 RID: 256
		private enum ParamIDs
		{
			// Token: 0x04000488 RID: 1160
			InvalidPayloadFormat,
			// Token: 0x04000489 RID: 1161
			AzureChallengeInvalidDeviceId,
			// Token: 0x0400048A RID: 1162
			InvalidWnsUriScheme,
			// Token: 0x0400048B RID: 1163
			ValidationErrorRangeVersion,
			// Token: 0x0400048C RID: 1164
			InvalidPayloadLength,
			// Token: 0x0400048D RID: 1165
			InvalidReportFromApns,
			// Token: 0x0400048E RID: 1166
			ApnsFeedbackFileIdInvalidPseudoAppId,
			// Token: 0x0400048F RID: 1167
			AzureInvalidRecipientId,
			// Token: 0x04000490 RID: 1168
			ApnsFeedbackFileIdInvalidDate,
			// Token: 0x04000491 RID: 1169
			ApnsFeedbackFileIdInvalidCharacters,
			// Token: 0x04000492 RID: 1170
			ApnsFeedbackPackageFeedbackNotFound,
			// Token: 0x04000493 RID: 1171
			ApnsCertificatePrivateKeyError,
			// Token: 0x04000494 RID: 1172
			InvalidWnsPayloadLength,
			// Token: 0x04000495 RID: 1173
			ApnsFeedbackResponseInvalidLength,
			// Token: 0x04000496 RID: 1174
			ValidationErrorNonNegativeInteger,
			// Token: 0x04000497 RID: 1175
			GcmInvalidNotificationReported,
			// Token: 0x04000498 RID: 1176
			InvalidWnsUri,
			// Token: 0x04000499 RID: 1177
			ApnsFeedbackFileGetFilesError,
			// Token: 0x0400049A RID: 1178
			ApnsFeedbackPackageRemovalFailed,
			// Token: 0x0400049B RID: 1179
			DataProtectionDecryptingError,
			// Token: 0x0400049C RID: 1180
			ValidationErrorDeviceRegistrationAppId,
			// Token: 0x0400049D RID: 1181
			InvalidExpiration,
			// Token: 0x0400049E RID: 1182
			ApnsFeedbackFileIdInsufficientComponents,
			// Token: 0x0400049F RID: 1183
			AzureDeviceInvalidTag,
			// Token: 0x040004A0 RID: 1184
			ApnsFeedbackPackageExtractionFailed,
			// Token: 0x040004A1 RID: 1185
			ApnsFeedbackFileIdInvalidExtension,
			// Token: 0x040004A2 RID: 1186
			ApnsFeedbackFileLoadError,
			// Token: 0x040004A3 RID: 1187
			CannotCreateValidSasToken,
			// Token: 0x040004A4 RID: 1188
			ApnsPublisherExpiredToken,
			// Token: 0x040004A5 RID: 1189
			InvalidWnsTimeToLive,
			// Token: 0x040004A6 RID: 1190
			ValidationErrorEmptyString,
			// Token: 0x040004A7 RID: 1191
			ApnsCertificateExternalException,
			// Token: 0x040004A8 RID: 1192
			AzureDeviceInvalidDeviceId,
			// Token: 0x040004A9 RID: 1193
			ValidationErrorInvalidSasToken,
			// Token: 0x040004AA RID: 1194
			ValidationErrorRangeInteger,
			// Token: 0x040004AB RID: 1195
			AzureChallengeInvalidPlatformOnPayload,
			// Token: 0x040004AC RID: 1196
			ApnsFeedbackError,
			// Token: 0x040004AD RID: 1197
			GcmInvalidTimeToLive,
			// Token: 0x040004AE RID: 1198
			ValidationErrorPositiveInteger,
			// Token: 0x040004AF RID: 1199
			NotificationDroppedDueToLastUpdateTime,
			// Token: 0x040004B0 RID: 1200
			ValidationErrorHubCreationAppId,
			// Token: 0x040004B1 RID: 1201
			DataProtectionEncryptingError,
			// Token: 0x040004B2 RID: 1202
			ApnsFeedbackResponseInvalidFileLine,
			// Token: 0x040004B3 RID: 1203
			ApnsFeedbackFileIdInvalidDirectory,
			// Token: 0x040004B4 RID: 1204
			ApnsCertificateNotFound,
			// Token: 0x040004B5 RID: 1205
			InvalidWnsTemplate,
			// Token: 0x040004B6 RID: 1206
			AzureCannotResolveExternalOrgId,
			// Token: 0x040004B7 RID: 1207
			ApnsFeedbackPackageUnexpectedMetadataResult,
			// Token: 0x040004B8 RID: 1208
			ValidationErrorChallengeRequestAppId,
			// Token: 0x040004B9 RID: 1209
			GcmInvalidPayloadLength,
			// Token: 0x040004BA RID: 1210
			ValidationErrorInvalidUri,
			// Token: 0x040004BB RID: 1211
			InvalidAzurePayloadLength,
			// Token: 0x040004BC RID: 1212
			GcmInvalidRegistrationId,
			// Token: 0x040004BD RID: 1213
			InvalidLastSubscriptionUpdate,
			// Token: 0x040004BE RID: 1214
			InvalidWnsLanguage,
			// Token: 0x040004BF RID: 1215
			InvalidDeviceToken,
			// Token: 0x040004C0 RID: 1216
			InvalidWnsDeviceUri,
			// Token: 0x040004C1 RID: 1217
			InvalidWnsAttributeIsMandatory,
			// Token: 0x040004C2 RID: 1218
			WnsChannelInvalidNotificationReported
		}
	}
}
