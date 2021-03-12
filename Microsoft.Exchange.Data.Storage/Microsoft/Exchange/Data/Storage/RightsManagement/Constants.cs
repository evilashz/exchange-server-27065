using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B39 RID: 2873
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class Constants
	{
		// Token: 0x04003AE5 RID: 15077
		public const string SamlTokenType11 = "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1";

		// Token: 0x04003AE6 RID: 15078
		public const long MaxSizeOfMexData = 51200L;

		// Token: 0x04003AE7 RID: 15079
		public const long MaxReceivedMessageSizeInBytes = 2097152L;

		// Token: 0x04003AE8 RID: 15080
		public const int MaxStringContentLengthInBytes = 2097152;

		// Token: 0x04003AE9 RID: 15081
		public static readonly TimeSpan BindingTimeout = TimeSpan.FromSeconds(30.0);

		// Token: 0x04003AEA RID: 15082
		public static readonly byte[] EmptyByteArray = Array<byte>.Empty;

		// Token: 0x02000B3A RID: 2874
		public enum State
		{
			// Token: 0x04003AEC RID: 15084
			None,
			// Token: 0x04003AED RID: 15085
			BeginAcquireRMSTemplate,
			// Token: 0x04003AEE RID: 15086
			BeginAcquireRMSTemplateFirstRequest,
			// Token: 0x04003AEF RID: 15087
			BeginAcquireRMSTemplatePendingRequest,
			// Token: 0x04003AF0 RID: 15088
			AcquireRmsTemplatesCallback,
			// Token: 0x04003AF1 RID: 15089
			EndAcquireRMSTemplate,
			// Token: 0x04003AF2 RID: 15090
			PerTenantQueryControllerInvokeCallback,
			// Token: 0x04003AF3 RID: 15091
			BeginAcquirePreLicense,
			// Token: 0x04003AF4 RID: 15092
			AcquirePreLicenseCallback,
			// Token: 0x04003AF5 RID: 15093
			EndAcquirePreLicense,
			// Token: 0x04003AF6 RID: 15094
			BeginAcquireInternalOrganizationRACAndCLC,
			// Token: 0x04003AF7 RID: 15095
			BeginAcquireInternalOrganizationRACAndCLCFirstRequest,
			// Token: 0x04003AF8 RID: 15096
			BeginAcquireInternalOrganizationRACAndCLCPendingRequest,
			// Token: 0x04003AF9 RID: 15097
			AcquireServerRacCallback,
			// Token: 0x04003AFA RID: 15098
			BeginAcquireClc,
			// Token: 0x04003AFB RID: 15099
			AcquireClcCallback,
			// Token: 0x04003AFC RID: 15100
			EndAcquireInternalOrganizationRACAndCLC,
			// Token: 0x04003AFD RID: 15101
			BeginAcquireSuperUserUseLicense,
			// Token: 0x04003AFE RID: 15102
			AcquireTenantLicenseCallback,
			// Token: 0x04003AFF RID: 15103
			BeginAcquireUseLicense,
			// Token: 0x04003B00 RID: 15104
			AcquireUseLicenseCallback,
			// Token: 0x04003B01 RID: 15105
			BeginAcquireFederationRAC,
			// Token: 0x04003B02 RID: 15106
			BeginAcquireFederationServerLicense,
			// Token: 0x04003B03 RID: 15107
			AcquireExternalRMSInfoCertificationCallback,
			// Token: 0x04003B04 RID: 15108
			WCFBeginCertify,
			// Token: 0x04003B05 RID: 15109
			AcquireTenantExternalRacCallback,
			// Token: 0x04003B06 RID: 15110
			AcquireFederationRacCallback,
			// Token: 0x04003B07 RID: 15111
			AcquireExternalRMSInfoLicensingCallback,
			// Token: 0x04003B08 RID: 15112
			WCFBeginAcquireLicense,
			// Token: 0x04003B09 RID: 15113
			AcquireFederationLicenseCallback,
			// Token: 0x04003B0A RID: 15114
			BeginAcquireServerInfo,
			// Token: 0x04003B0B RID: 15115
			BeginFindServiceLocationsFirstRequest,
			// Token: 0x04003B0C RID: 15116
			BeginFindServiceLocationsPendingRequest,
			// Token: 0x04003B0D RID: 15117
			EndAcquireServerInfo,
			// Token: 0x04003B0E RID: 15118
			AcquireServiceLocationCallback,
			// Token: 0x04003B0F RID: 15119
			BeginDownloadCertificationMexData,
			// Token: 0x04003B10 RID: 15120
			AcquireCertificationMexCallback,
			// Token: 0x04003B11 RID: 15121
			AcquireServerLicensingMexCallback,
			// Token: 0x04003B12 RID: 15122
			BeginDownloadServerLicensingMexData,
			// Token: 0x04003B13 RID: 15123
			EndAcquireFederationRAC,
			// Token: 0x04003B14 RID: 15124
			EndAcquireUseLicense,
			// Token: 0x04003B15 RID: 15125
			BeginAcquireUseLicenseAndUsageRights,
			// Token: 0x04003B16 RID: 15126
			AcquireUseLicenseAndUsageRightsCallbackForUseLicense,
			// Token: 0x04003B17 RID: 15127
			AcquireUseLicenseAndUsageRightsCallbackForPreLicense,
			// Token: 0x04003B18 RID: 15128
			EndAcquireUseLicenseAndUsageRights
		}
	}
}
