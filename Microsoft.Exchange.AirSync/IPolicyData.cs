using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000CD RID: 205
	internal interface IPolicyData
	{
		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000BCC RID: 3020
		ADObjectId Identity { get; }

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000BCD RID: 3021
		bool AllowNonProvisionableDevices { get; }

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000BCE RID: 3022
		bool AlphanumericDevicePasswordRequired { get; }

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000BCF RID: 3023
		bool AttachmentsEnabled { get; }

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000BD0 RID: 3024
		bool RequireStorageCardEncryption { get; }

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000BD1 RID: 3025
		bool DevicePasswordEnabled { get; }

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000BD2 RID: 3026
		bool PasswordRecoveryEnabled { get; }

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000BD3 RID: 3027
		Unlimited<EnhancedTimeSpan> DevicePolicyRefreshInterval { get; }

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000BD4 RID: 3028
		bool AllowSimpleDevicePassword { get; }

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000BD5 RID: 3029
		Unlimited<ByteQuantifiedSize> MaxAttachmentSize { get; }

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000BD6 RID: 3030
		bool WSSAccessEnabled { get; }

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000BD7 RID: 3031
		bool UNCAccessEnabled { get; }

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000BD8 RID: 3032
		int? MinDevicePasswordLength { get; }

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000BD9 RID: 3033
		Unlimited<EnhancedTimeSpan> MaxInactivityTimeDeviceLock { get; }

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000BDA RID: 3034
		Unlimited<int> MaxDevicePasswordFailedAttempts { get; }

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000BDB RID: 3035
		Unlimited<EnhancedTimeSpan> DevicePasswordExpiration { get; }

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000BDC RID: 3036
		int DevicePasswordHistory { get; }

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000BDD RID: 3037
		bool IsDefault { get; }

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000BDE RID: 3038
		bool AllowStorageCard { get; }

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000BDF RID: 3039
		bool AllowCamera { get; }

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000BE0 RID: 3040
		bool RequireDeviceEncryption { get; }

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000BE1 RID: 3041
		bool AllowUnsignedApplications { get; }

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000BE2 RID: 3042
		bool AllowUnsignedInstallationPackages { get; }

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000BE3 RID: 3043
		bool AllowWiFi { get; }

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000BE4 RID: 3044
		bool AllowTextMessaging { get; }

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000BE5 RID: 3045
		bool AllowPOPIMAPEmail { get; }

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000BE6 RID: 3046
		bool AllowIrDA { get; }

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000BE7 RID: 3047
		bool RequireManualSyncWhenRoaming { get; }

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000BE8 RID: 3048
		bool AllowDesktopSync { get; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000BE9 RID: 3049
		bool AllowHTMLEmail { get; }

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000BEA RID: 3050
		bool RequireSignedSMIMEMessages { get; }

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000BEB RID: 3051
		bool RequireEncryptedSMIMEMessages { get; }

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000BEC RID: 3052
		bool AllowSMIMESoftCerts { get; }

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000BED RID: 3053
		bool AllowBrowser { get; }

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000BEE RID: 3054
		bool AllowConsumerEmail { get; }

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000BEF RID: 3055
		bool AllowRemoteDesktop { get; }

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000BF0 RID: 3056
		bool AllowInternetSharing { get; }

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000BF1 RID: 3057
		BluetoothType AllowBluetooth { get; }

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000BF2 RID: 3058
		CalendarAgeFilterType MaxCalendarAgeFilter { get; }

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000BF3 RID: 3059
		EmailAgeFilterType MaxEmailAgeFilter { get; }

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000BF4 RID: 3060
		SignedSMIMEAlgorithmType RequireSignedSMIMEAlgorithm { get; }

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000BF5 RID: 3061
		EncryptionSMIMEAlgorithmType RequireEncryptionSMIMEAlgorithm { get; }

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000BF6 RID: 3062
		SMIMEEncryptionAlgorithmNegotiationType AllowSMIMEEncryptionAlgorithmNegotiation { get; }

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000BF7 RID: 3063
		int MinDevicePasswordComplexCharacters { get; }

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000BF8 RID: 3064
		Unlimited<int> MaxEmailBodyTruncationSize { get; }

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000BF9 RID: 3065
		Unlimited<int> MaxEmailHTMLBodyTruncationSize { get; }

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000BFA RID: 3066
		MultiValuedProperty<string> UnapprovedInROMApplicationList { get; }

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000BFB RID: 3067
		ApprovedApplicationCollection ApprovedApplicationList { get; }

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000BFC RID: 3068
		bool AllowExternalDeviceManagement { get; }

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06000BFD RID: 3069
		bool IsIrmEnabled { get; }

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06000BFE RID: 3070
		MobileOTAUpdateModeType MobileOTAUpdateMode { get; }

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06000BFF RID: 3071
		bool AllowMobileOTAUpdate { get; }

		// Token: 0x06000C00 RID: 3072
		bool GetVersionCompatibility(int version);

		// Token: 0x06000C01 RID: 3073
		int GetHashCode(int version);
	}
}
