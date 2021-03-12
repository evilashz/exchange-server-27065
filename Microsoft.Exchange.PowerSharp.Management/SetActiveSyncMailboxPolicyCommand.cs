using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001A2 RID: 418
	public class SetActiveSyncMailboxPolicyCommand : SyntheticCommandWithPipelineInputNoOutput<ActiveSyncMailboxPolicy>
	{
		// Token: 0x06002418 RID: 9240 RVA: 0x000465AF File Offset: 0x000447AF
		private SetActiveSyncMailboxPolicyCommand() : base("Set-ActiveSyncMailboxPolicy")
		{
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x000465BC File Offset: 0x000447BC
		public SetActiveSyncMailboxPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600241A RID: 9242 RVA: 0x000465CB File Offset: 0x000447CB
		public virtual SetActiveSyncMailboxPolicyCommand SetParameters(SetActiveSyncMailboxPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x000465D5 File Offset: 0x000447D5
		public virtual SetActiveSyncMailboxPolicyCommand SetParameters(SetActiveSyncMailboxPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001A3 RID: 419
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000C8D RID: 3213
			// (set) Token: 0x0600241C RID: 9244 RVA: 0x000465DF File Offset: 0x000447DF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000C8E RID: 3214
			// (set) Token: 0x0600241D RID: 9245 RVA: 0x000465F2 File Offset: 0x000447F2
			public virtual bool AlphanumericDevicePasswordRequired
			{
				set
				{
					base.PowerSharpParameters["AlphanumericDevicePasswordRequired"] = value;
				}
			}

			// Token: 0x17000C8F RID: 3215
			// (set) Token: 0x0600241E RID: 9246 RVA: 0x0004660A File Offset: 0x0004480A
			public virtual bool DevicePasswordEnabled
			{
				set
				{
					base.PowerSharpParameters["DevicePasswordEnabled"] = value;
				}
			}

			// Token: 0x17000C90 RID: 3216
			// (set) Token: 0x0600241F RID: 9247 RVA: 0x00046622 File Offset: 0x00044822
			public virtual bool AllowSimpleDevicePassword
			{
				set
				{
					base.PowerSharpParameters["AllowSimpleDevicePassword"] = value;
				}
			}

			// Token: 0x17000C91 RID: 3217
			// (set) Token: 0x06002420 RID: 9248 RVA: 0x0004663A File Offset: 0x0004483A
			public virtual int? MinDevicePasswordLength
			{
				set
				{
					base.PowerSharpParameters["MinDevicePasswordLength"] = value;
				}
			}

			// Token: 0x17000C92 RID: 3218
			// (set) Token: 0x06002421 RID: 9249 RVA: 0x00046652 File Offset: 0x00044852
			public virtual bool IsDefaultPolicy
			{
				set
				{
					base.PowerSharpParameters["IsDefaultPolicy"] = value;
				}
			}

			// Token: 0x17000C93 RID: 3219
			// (set) Token: 0x06002422 RID: 9250 RVA: 0x0004666A File Offset: 0x0004486A
			public virtual Unlimited<EnhancedTimeSpan> MaxInactivityTimeDeviceLock
			{
				set
				{
					base.PowerSharpParameters["MaxInactivityTimeDeviceLock"] = value;
				}
			}

			// Token: 0x17000C94 RID: 3220
			// (set) Token: 0x06002423 RID: 9251 RVA: 0x00046682 File Offset: 0x00044882
			public virtual Unlimited<int> MaxDevicePasswordFailedAttempts
			{
				set
				{
					base.PowerSharpParameters["MaxDevicePasswordFailedAttempts"] = value;
				}
			}

			// Token: 0x17000C95 RID: 3221
			// (set) Token: 0x06002424 RID: 9252 RVA: 0x0004669A File Offset: 0x0004489A
			public virtual Unlimited<EnhancedTimeSpan> DevicePasswordExpiration
			{
				set
				{
					base.PowerSharpParameters["DevicePasswordExpiration"] = value;
				}
			}

			// Token: 0x17000C96 RID: 3222
			// (set) Token: 0x06002425 RID: 9253 RVA: 0x000466B2 File Offset: 0x000448B2
			public virtual int DevicePasswordHistory
			{
				set
				{
					base.PowerSharpParameters["DevicePasswordHistory"] = value;
				}
			}

			// Token: 0x17000C97 RID: 3223
			// (set) Token: 0x06002426 RID: 9254 RVA: 0x000466CA File Offset: 0x000448CA
			public virtual int MinDevicePasswordComplexCharacters
			{
				set
				{
					base.PowerSharpParameters["MinDevicePasswordComplexCharacters"] = value;
				}
			}

			// Token: 0x17000C98 RID: 3224
			// (set) Token: 0x06002427 RID: 9255 RVA: 0x000466E2 File Offset: 0x000448E2
			public virtual bool AllowNonProvisionableDevices
			{
				set
				{
					base.PowerSharpParameters["AllowNonProvisionableDevices"] = value;
				}
			}

			// Token: 0x17000C99 RID: 3225
			// (set) Token: 0x06002428 RID: 9256 RVA: 0x000466FA File Offset: 0x000448FA
			public virtual bool AttachmentsEnabled
			{
				set
				{
					base.PowerSharpParameters["AttachmentsEnabled"] = value;
				}
			}

			// Token: 0x17000C9A RID: 3226
			// (set) Token: 0x06002429 RID: 9257 RVA: 0x00046712 File Offset: 0x00044912
			public virtual bool DeviceEncryptionEnabled
			{
				set
				{
					base.PowerSharpParameters["DeviceEncryptionEnabled"] = value;
				}
			}

			// Token: 0x17000C9B RID: 3227
			// (set) Token: 0x0600242A RID: 9258 RVA: 0x0004672A File Offset: 0x0004492A
			public virtual bool RequireStorageCardEncryption
			{
				set
				{
					base.PowerSharpParameters["RequireStorageCardEncryption"] = value;
				}
			}

			// Token: 0x17000C9C RID: 3228
			// (set) Token: 0x0600242B RID: 9259 RVA: 0x00046742 File Offset: 0x00044942
			public virtual bool PasswordRecoveryEnabled
			{
				set
				{
					base.PowerSharpParameters["PasswordRecoveryEnabled"] = value;
				}
			}

			// Token: 0x17000C9D RID: 3229
			// (set) Token: 0x0600242C RID: 9260 RVA: 0x0004675A File Offset: 0x0004495A
			public virtual Unlimited<EnhancedTimeSpan> DevicePolicyRefreshInterval
			{
				set
				{
					base.PowerSharpParameters["DevicePolicyRefreshInterval"] = value;
				}
			}

			// Token: 0x17000C9E RID: 3230
			// (set) Token: 0x0600242D RID: 9261 RVA: 0x00046772 File Offset: 0x00044972
			public virtual Unlimited<ByteQuantifiedSize> MaxAttachmentSize
			{
				set
				{
					base.PowerSharpParameters["MaxAttachmentSize"] = value;
				}
			}

			// Token: 0x17000C9F RID: 3231
			// (set) Token: 0x0600242E RID: 9262 RVA: 0x0004678A File Offset: 0x0004498A
			public virtual bool WSSAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["WSSAccessEnabled"] = value;
				}
			}

			// Token: 0x17000CA0 RID: 3232
			// (set) Token: 0x0600242F RID: 9263 RVA: 0x000467A2 File Offset: 0x000449A2
			public virtual bool UNCAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["UNCAccessEnabled"] = value;
				}
			}

			// Token: 0x17000CA1 RID: 3233
			// (set) Token: 0x06002430 RID: 9264 RVA: 0x000467BA File Offset: 0x000449BA
			public virtual bool IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x17000CA2 RID: 3234
			// (set) Token: 0x06002431 RID: 9265 RVA: 0x000467D2 File Offset: 0x000449D2
			public virtual bool AllowApplePushNotifications
			{
				set
				{
					base.PowerSharpParameters["AllowApplePushNotifications"] = value;
				}
			}

			// Token: 0x17000CA3 RID: 3235
			// (set) Token: 0x06002432 RID: 9266 RVA: 0x000467EA File Offset: 0x000449EA
			public virtual bool AllowStorageCard
			{
				set
				{
					base.PowerSharpParameters["AllowStorageCard"] = value;
				}
			}

			// Token: 0x17000CA4 RID: 3236
			// (set) Token: 0x06002433 RID: 9267 RVA: 0x00046802 File Offset: 0x00044A02
			public virtual bool AllowCamera
			{
				set
				{
					base.PowerSharpParameters["AllowCamera"] = value;
				}
			}

			// Token: 0x17000CA5 RID: 3237
			// (set) Token: 0x06002434 RID: 9268 RVA: 0x0004681A File Offset: 0x00044A1A
			public virtual bool RequireDeviceEncryption
			{
				set
				{
					base.PowerSharpParameters["RequireDeviceEncryption"] = value;
				}
			}

			// Token: 0x17000CA6 RID: 3238
			// (set) Token: 0x06002435 RID: 9269 RVA: 0x00046832 File Offset: 0x00044A32
			public virtual bool AllowUnsignedApplications
			{
				set
				{
					base.PowerSharpParameters["AllowUnsignedApplications"] = value;
				}
			}

			// Token: 0x17000CA7 RID: 3239
			// (set) Token: 0x06002436 RID: 9270 RVA: 0x0004684A File Offset: 0x00044A4A
			public virtual bool AllowUnsignedInstallationPackages
			{
				set
				{
					base.PowerSharpParameters["AllowUnsignedInstallationPackages"] = value;
				}
			}

			// Token: 0x17000CA8 RID: 3240
			// (set) Token: 0x06002437 RID: 9271 RVA: 0x00046862 File Offset: 0x00044A62
			public virtual bool AllowWiFi
			{
				set
				{
					base.PowerSharpParameters["AllowWiFi"] = value;
				}
			}

			// Token: 0x17000CA9 RID: 3241
			// (set) Token: 0x06002438 RID: 9272 RVA: 0x0004687A File Offset: 0x00044A7A
			public virtual bool AllowTextMessaging
			{
				set
				{
					base.PowerSharpParameters["AllowTextMessaging"] = value;
				}
			}

			// Token: 0x17000CAA RID: 3242
			// (set) Token: 0x06002439 RID: 9273 RVA: 0x00046892 File Offset: 0x00044A92
			public virtual bool AllowPOPIMAPEmail
			{
				set
				{
					base.PowerSharpParameters["AllowPOPIMAPEmail"] = value;
				}
			}

			// Token: 0x17000CAB RID: 3243
			// (set) Token: 0x0600243A RID: 9274 RVA: 0x000468AA File Offset: 0x00044AAA
			public virtual bool AllowIrDA
			{
				set
				{
					base.PowerSharpParameters["AllowIrDA"] = value;
				}
			}

			// Token: 0x17000CAC RID: 3244
			// (set) Token: 0x0600243B RID: 9275 RVA: 0x000468C2 File Offset: 0x00044AC2
			public virtual bool RequireManualSyncWhenRoaming
			{
				set
				{
					base.PowerSharpParameters["RequireManualSyncWhenRoaming"] = value;
				}
			}

			// Token: 0x17000CAD RID: 3245
			// (set) Token: 0x0600243C RID: 9276 RVA: 0x000468DA File Offset: 0x00044ADA
			public virtual bool AllowDesktopSync
			{
				set
				{
					base.PowerSharpParameters["AllowDesktopSync"] = value;
				}
			}

			// Token: 0x17000CAE RID: 3246
			// (set) Token: 0x0600243D RID: 9277 RVA: 0x000468F2 File Offset: 0x00044AF2
			public virtual bool AllowHTMLEmail
			{
				set
				{
					base.PowerSharpParameters["AllowHTMLEmail"] = value;
				}
			}

			// Token: 0x17000CAF RID: 3247
			// (set) Token: 0x0600243E RID: 9278 RVA: 0x0004690A File Offset: 0x00044B0A
			public virtual bool RequireSignedSMIMEMessages
			{
				set
				{
					base.PowerSharpParameters["RequireSignedSMIMEMessages"] = value;
				}
			}

			// Token: 0x17000CB0 RID: 3248
			// (set) Token: 0x0600243F RID: 9279 RVA: 0x00046922 File Offset: 0x00044B22
			public virtual bool RequireEncryptedSMIMEMessages
			{
				set
				{
					base.PowerSharpParameters["RequireEncryptedSMIMEMessages"] = value;
				}
			}

			// Token: 0x17000CB1 RID: 3249
			// (set) Token: 0x06002440 RID: 9280 RVA: 0x0004693A File Offset: 0x00044B3A
			public virtual bool AllowSMIMESoftCerts
			{
				set
				{
					base.PowerSharpParameters["AllowSMIMESoftCerts"] = value;
				}
			}

			// Token: 0x17000CB2 RID: 3250
			// (set) Token: 0x06002441 RID: 9281 RVA: 0x00046952 File Offset: 0x00044B52
			public virtual bool AllowBrowser
			{
				set
				{
					base.PowerSharpParameters["AllowBrowser"] = value;
				}
			}

			// Token: 0x17000CB3 RID: 3251
			// (set) Token: 0x06002442 RID: 9282 RVA: 0x0004696A File Offset: 0x00044B6A
			public virtual bool AllowConsumerEmail
			{
				set
				{
					base.PowerSharpParameters["AllowConsumerEmail"] = value;
				}
			}

			// Token: 0x17000CB4 RID: 3252
			// (set) Token: 0x06002443 RID: 9283 RVA: 0x00046982 File Offset: 0x00044B82
			public virtual bool AllowRemoteDesktop
			{
				set
				{
					base.PowerSharpParameters["AllowRemoteDesktop"] = value;
				}
			}

			// Token: 0x17000CB5 RID: 3253
			// (set) Token: 0x06002444 RID: 9284 RVA: 0x0004699A File Offset: 0x00044B9A
			public virtual bool AllowInternetSharing
			{
				set
				{
					base.PowerSharpParameters["AllowInternetSharing"] = value;
				}
			}

			// Token: 0x17000CB6 RID: 3254
			// (set) Token: 0x06002445 RID: 9285 RVA: 0x000469B2 File Offset: 0x00044BB2
			public virtual BluetoothType AllowBluetooth
			{
				set
				{
					base.PowerSharpParameters["AllowBluetooth"] = value;
				}
			}

			// Token: 0x17000CB7 RID: 3255
			// (set) Token: 0x06002446 RID: 9286 RVA: 0x000469CA File Offset: 0x00044BCA
			public virtual CalendarAgeFilterType MaxCalendarAgeFilter
			{
				set
				{
					base.PowerSharpParameters["MaxCalendarAgeFilter"] = value;
				}
			}

			// Token: 0x17000CB8 RID: 3256
			// (set) Token: 0x06002447 RID: 9287 RVA: 0x000469E2 File Offset: 0x00044BE2
			public virtual EmailAgeFilterType MaxEmailAgeFilter
			{
				set
				{
					base.PowerSharpParameters["MaxEmailAgeFilter"] = value;
				}
			}

			// Token: 0x17000CB9 RID: 3257
			// (set) Token: 0x06002448 RID: 9288 RVA: 0x000469FA File Offset: 0x00044BFA
			public virtual SignedSMIMEAlgorithmType RequireSignedSMIMEAlgorithm
			{
				set
				{
					base.PowerSharpParameters["RequireSignedSMIMEAlgorithm"] = value;
				}
			}

			// Token: 0x17000CBA RID: 3258
			// (set) Token: 0x06002449 RID: 9289 RVA: 0x00046A12 File Offset: 0x00044C12
			public virtual EncryptionSMIMEAlgorithmType RequireEncryptionSMIMEAlgorithm
			{
				set
				{
					base.PowerSharpParameters["RequireEncryptionSMIMEAlgorithm"] = value;
				}
			}

			// Token: 0x17000CBB RID: 3259
			// (set) Token: 0x0600244A RID: 9290 RVA: 0x00046A2A File Offset: 0x00044C2A
			public virtual SMIMEEncryptionAlgorithmNegotiationType AllowSMIMEEncryptionAlgorithmNegotiation
			{
				set
				{
					base.PowerSharpParameters["AllowSMIMEEncryptionAlgorithmNegotiation"] = value;
				}
			}

			// Token: 0x17000CBC RID: 3260
			// (set) Token: 0x0600244B RID: 9291 RVA: 0x00046A42 File Offset: 0x00044C42
			public virtual Unlimited<int> MaxEmailBodyTruncationSize
			{
				set
				{
					base.PowerSharpParameters["MaxEmailBodyTruncationSize"] = value;
				}
			}

			// Token: 0x17000CBD RID: 3261
			// (set) Token: 0x0600244C RID: 9292 RVA: 0x00046A5A File Offset: 0x00044C5A
			public virtual Unlimited<int> MaxEmailHTMLBodyTruncationSize
			{
				set
				{
					base.PowerSharpParameters["MaxEmailHTMLBodyTruncationSize"] = value;
				}
			}

			// Token: 0x17000CBE RID: 3262
			// (set) Token: 0x0600244D RID: 9293 RVA: 0x00046A72 File Offset: 0x00044C72
			public virtual MultiValuedProperty<string> UnapprovedInROMApplicationList
			{
				set
				{
					base.PowerSharpParameters["UnapprovedInROMApplicationList"] = value;
				}
			}

			// Token: 0x17000CBF RID: 3263
			// (set) Token: 0x0600244E RID: 9294 RVA: 0x00046A85 File Offset: 0x00044C85
			public virtual ApprovedApplicationCollection ApprovedApplicationList
			{
				set
				{
					base.PowerSharpParameters["ApprovedApplicationList"] = value;
				}
			}

			// Token: 0x17000CC0 RID: 3264
			// (set) Token: 0x0600244F RID: 9295 RVA: 0x00046A98 File Offset: 0x00044C98
			public virtual bool AllowExternalDeviceManagement
			{
				set
				{
					base.PowerSharpParameters["AllowExternalDeviceManagement"] = value;
				}
			}

			// Token: 0x17000CC1 RID: 3265
			// (set) Token: 0x06002450 RID: 9296 RVA: 0x00046AB0 File Offset: 0x00044CB0
			public virtual MobileOTAUpdateModeType MobileOTAUpdateMode
			{
				set
				{
					base.PowerSharpParameters["MobileOTAUpdateMode"] = value;
				}
			}

			// Token: 0x17000CC2 RID: 3266
			// (set) Token: 0x06002451 RID: 9297 RVA: 0x00046AC8 File Offset: 0x00044CC8
			public virtual bool AllowMobileOTAUpdate
			{
				set
				{
					base.PowerSharpParameters["AllowMobileOTAUpdate"] = value;
				}
			}

			// Token: 0x17000CC3 RID: 3267
			// (set) Token: 0x06002452 RID: 9298 RVA: 0x00046AE0 File Offset: 0x00044CE0
			public virtual bool IrmEnabled
			{
				set
				{
					base.PowerSharpParameters["IrmEnabled"] = value;
				}
			}

			// Token: 0x17000CC4 RID: 3268
			// (set) Token: 0x06002453 RID: 9299 RVA: 0x00046AF8 File Offset: 0x00044CF8
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000CC5 RID: 3269
			// (set) Token: 0x06002454 RID: 9300 RVA: 0x00046B0B File Offset: 0x00044D0B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000CC6 RID: 3270
			// (set) Token: 0x06002455 RID: 9301 RVA: 0x00046B23 File Offset: 0x00044D23
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000CC7 RID: 3271
			// (set) Token: 0x06002456 RID: 9302 RVA: 0x00046B3B File Offset: 0x00044D3B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000CC8 RID: 3272
			// (set) Token: 0x06002457 RID: 9303 RVA: 0x00046B53 File Offset: 0x00044D53
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000CC9 RID: 3273
			// (set) Token: 0x06002458 RID: 9304 RVA: 0x00046B6B File Offset: 0x00044D6B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020001A4 RID: 420
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000CCA RID: 3274
			// (set) Token: 0x0600245A RID: 9306 RVA: 0x00046B8B File Offset: 0x00044D8B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17000CCB RID: 3275
			// (set) Token: 0x0600245B RID: 9307 RVA: 0x00046BA9 File Offset: 0x00044DA9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000CCC RID: 3276
			// (set) Token: 0x0600245C RID: 9308 RVA: 0x00046BBC File Offset: 0x00044DBC
			public virtual bool AlphanumericDevicePasswordRequired
			{
				set
				{
					base.PowerSharpParameters["AlphanumericDevicePasswordRequired"] = value;
				}
			}

			// Token: 0x17000CCD RID: 3277
			// (set) Token: 0x0600245D RID: 9309 RVA: 0x00046BD4 File Offset: 0x00044DD4
			public virtual bool DevicePasswordEnabled
			{
				set
				{
					base.PowerSharpParameters["DevicePasswordEnabled"] = value;
				}
			}

			// Token: 0x17000CCE RID: 3278
			// (set) Token: 0x0600245E RID: 9310 RVA: 0x00046BEC File Offset: 0x00044DEC
			public virtual bool AllowSimpleDevicePassword
			{
				set
				{
					base.PowerSharpParameters["AllowSimpleDevicePassword"] = value;
				}
			}

			// Token: 0x17000CCF RID: 3279
			// (set) Token: 0x0600245F RID: 9311 RVA: 0x00046C04 File Offset: 0x00044E04
			public virtual int? MinDevicePasswordLength
			{
				set
				{
					base.PowerSharpParameters["MinDevicePasswordLength"] = value;
				}
			}

			// Token: 0x17000CD0 RID: 3280
			// (set) Token: 0x06002460 RID: 9312 RVA: 0x00046C1C File Offset: 0x00044E1C
			public virtual bool IsDefaultPolicy
			{
				set
				{
					base.PowerSharpParameters["IsDefaultPolicy"] = value;
				}
			}

			// Token: 0x17000CD1 RID: 3281
			// (set) Token: 0x06002461 RID: 9313 RVA: 0x00046C34 File Offset: 0x00044E34
			public virtual Unlimited<EnhancedTimeSpan> MaxInactivityTimeDeviceLock
			{
				set
				{
					base.PowerSharpParameters["MaxInactivityTimeDeviceLock"] = value;
				}
			}

			// Token: 0x17000CD2 RID: 3282
			// (set) Token: 0x06002462 RID: 9314 RVA: 0x00046C4C File Offset: 0x00044E4C
			public virtual Unlimited<int> MaxDevicePasswordFailedAttempts
			{
				set
				{
					base.PowerSharpParameters["MaxDevicePasswordFailedAttempts"] = value;
				}
			}

			// Token: 0x17000CD3 RID: 3283
			// (set) Token: 0x06002463 RID: 9315 RVA: 0x00046C64 File Offset: 0x00044E64
			public virtual Unlimited<EnhancedTimeSpan> DevicePasswordExpiration
			{
				set
				{
					base.PowerSharpParameters["DevicePasswordExpiration"] = value;
				}
			}

			// Token: 0x17000CD4 RID: 3284
			// (set) Token: 0x06002464 RID: 9316 RVA: 0x00046C7C File Offset: 0x00044E7C
			public virtual int DevicePasswordHistory
			{
				set
				{
					base.PowerSharpParameters["DevicePasswordHistory"] = value;
				}
			}

			// Token: 0x17000CD5 RID: 3285
			// (set) Token: 0x06002465 RID: 9317 RVA: 0x00046C94 File Offset: 0x00044E94
			public virtual int MinDevicePasswordComplexCharacters
			{
				set
				{
					base.PowerSharpParameters["MinDevicePasswordComplexCharacters"] = value;
				}
			}

			// Token: 0x17000CD6 RID: 3286
			// (set) Token: 0x06002466 RID: 9318 RVA: 0x00046CAC File Offset: 0x00044EAC
			public virtual bool AllowNonProvisionableDevices
			{
				set
				{
					base.PowerSharpParameters["AllowNonProvisionableDevices"] = value;
				}
			}

			// Token: 0x17000CD7 RID: 3287
			// (set) Token: 0x06002467 RID: 9319 RVA: 0x00046CC4 File Offset: 0x00044EC4
			public virtual bool AttachmentsEnabled
			{
				set
				{
					base.PowerSharpParameters["AttachmentsEnabled"] = value;
				}
			}

			// Token: 0x17000CD8 RID: 3288
			// (set) Token: 0x06002468 RID: 9320 RVA: 0x00046CDC File Offset: 0x00044EDC
			public virtual bool DeviceEncryptionEnabled
			{
				set
				{
					base.PowerSharpParameters["DeviceEncryptionEnabled"] = value;
				}
			}

			// Token: 0x17000CD9 RID: 3289
			// (set) Token: 0x06002469 RID: 9321 RVA: 0x00046CF4 File Offset: 0x00044EF4
			public virtual bool RequireStorageCardEncryption
			{
				set
				{
					base.PowerSharpParameters["RequireStorageCardEncryption"] = value;
				}
			}

			// Token: 0x17000CDA RID: 3290
			// (set) Token: 0x0600246A RID: 9322 RVA: 0x00046D0C File Offset: 0x00044F0C
			public virtual bool PasswordRecoveryEnabled
			{
				set
				{
					base.PowerSharpParameters["PasswordRecoveryEnabled"] = value;
				}
			}

			// Token: 0x17000CDB RID: 3291
			// (set) Token: 0x0600246B RID: 9323 RVA: 0x00046D24 File Offset: 0x00044F24
			public virtual Unlimited<EnhancedTimeSpan> DevicePolicyRefreshInterval
			{
				set
				{
					base.PowerSharpParameters["DevicePolicyRefreshInterval"] = value;
				}
			}

			// Token: 0x17000CDC RID: 3292
			// (set) Token: 0x0600246C RID: 9324 RVA: 0x00046D3C File Offset: 0x00044F3C
			public virtual Unlimited<ByteQuantifiedSize> MaxAttachmentSize
			{
				set
				{
					base.PowerSharpParameters["MaxAttachmentSize"] = value;
				}
			}

			// Token: 0x17000CDD RID: 3293
			// (set) Token: 0x0600246D RID: 9325 RVA: 0x00046D54 File Offset: 0x00044F54
			public virtual bool WSSAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["WSSAccessEnabled"] = value;
				}
			}

			// Token: 0x17000CDE RID: 3294
			// (set) Token: 0x0600246E RID: 9326 RVA: 0x00046D6C File Offset: 0x00044F6C
			public virtual bool UNCAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["UNCAccessEnabled"] = value;
				}
			}

			// Token: 0x17000CDF RID: 3295
			// (set) Token: 0x0600246F RID: 9327 RVA: 0x00046D84 File Offset: 0x00044F84
			public virtual bool IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x17000CE0 RID: 3296
			// (set) Token: 0x06002470 RID: 9328 RVA: 0x00046D9C File Offset: 0x00044F9C
			public virtual bool AllowApplePushNotifications
			{
				set
				{
					base.PowerSharpParameters["AllowApplePushNotifications"] = value;
				}
			}

			// Token: 0x17000CE1 RID: 3297
			// (set) Token: 0x06002471 RID: 9329 RVA: 0x00046DB4 File Offset: 0x00044FB4
			public virtual bool AllowStorageCard
			{
				set
				{
					base.PowerSharpParameters["AllowStorageCard"] = value;
				}
			}

			// Token: 0x17000CE2 RID: 3298
			// (set) Token: 0x06002472 RID: 9330 RVA: 0x00046DCC File Offset: 0x00044FCC
			public virtual bool AllowCamera
			{
				set
				{
					base.PowerSharpParameters["AllowCamera"] = value;
				}
			}

			// Token: 0x17000CE3 RID: 3299
			// (set) Token: 0x06002473 RID: 9331 RVA: 0x00046DE4 File Offset: 0x00044FE4
			public virtual bool RequireDeviceEncryption
			{
				set
				{
					base.PowerSharpParameters["RequireDeviceEncryption"] = value;
				}
			}

			// Token: 0x17000CE4 RID: 3300
			// (set) Token: 0x06002474 RID: 9332 RVA: 0x00046DFC File Offset: 0x00044FFC
			public virtual bool AllowUnsignedApplications
			{
				set
				{
					base.PowerSharpParameters["AllowUnsignedApplications"] = value;
				}
			}

			// Token: 0x17000CE5 RID: 3301
			// (set) Token: 0x06002475 RID: 9333 RVA: 0x00046E14 File Offset: 0x00045014
			public virtual bool AllowUnsignedInstallationPackages
			{
				set
				{
					base.PowerSharpParameters["AllowUnsignedInstallationPackages"] = value;
				}
			}

			// Token: 0x17000CE6 RID: 3302
			// (set) Token: 0x06002476 RID: 9334 RVA: 0x00046E2C File Offset: 0x0004502C
			public virtual bool AllowWiFi
			{
				set
				{
					base.PowerSharpParameters["AllowWiFi"] = value;
				}
			}

			// Token: 0x17000CE7 RID: 3303
			// (set) Token: 0x06002477 RID: 9335 RVA: 0x00046E44 File Offset: 0x00045044
			public virtual bool AllowTextMessaging
			{
				set
				{
					base.PowerSharpParameters["AllowTextMessaging"] = value;
				}
			}

			// Token: 0x17000CE8 RID: 3304
			// (set) Token: 0x06002478 RID: 9336 RVA: 0x00046E5C File Offset: 0x0004505C
			public virtual bool AllowPOPIMAPEmail
			{
				set
				{
					base.PowerSharpParameters["AllowPOPIMAPEmail"] = value;
				}
			}

			// Token: 0x17000CE9 RID: 3305
			// (set) Token: 0x06002479 RID: 9337 RVA: 0x00046E74 File Offset: 0x00045074
			public virtual bool AllowIrDA
			{
				set
				{
					base.PowerSharpParameters["AllowIrDA"] = value;
				}
			}

			// Token: 0x17000CEA RID: 3306
			// (set) Token: 0x0600247A RID: 9338 RVA: 0x00046E8C File Offset: 0x0004508C
			public virtual bool RequireManualSyncWhenRoaming
			{
				set
				{
					base.PowerSharpParameters["RequireManualSyncWhenRoaming"] = value;
				}
			}

			// Token: 0x17000CEB RID: 3307
			// (set) Token: 0x0600247B RID: 9339 RVA: 0x00046EA4 File Offset: 0x000450A4
			public virtual bool AllowDesktopSync
			{
				set
				{
					base.PowerSharpParameters["AllowDesktopSync"] = value;
				}
			}

			// Token: 0x17000CEC RID: 3308
			// (set) Token: 0x0600247C RID: 9340 RVA: 0x00046EBC File Offset: 0x000450BC
			public virtual bool AllowHTMLEmail
			{
				set
				{
					base.PowerSharpParameters["AllowHTMLEmail"] = value;
				}
			}

			// Token: 0x17000CED RID: 3309
			// (set) Token: 0x0600247D RID: 9341 RVA: 0x00046ED4 File Offset: 0x000450D4
			public virtual bool RequireSignedSMIMEMessages
			{
				set
				{
					base.PowerSharpParameters["RequireSignedSMIMEMessages"] = value;
				}
			}

			// Token: 0x17000CEE RID: 3310
			// (set) Token: 0x0600247E RID: 9342 RVA: 0x00046EEC File Offset: 0x000450EC
			public virtual bool RequireEncryptedSMIMEMessages
			{
				set
				{
					base.PowerSharpParameters["RequireEncryptedSMIMEMessages"] = value;
				}
			}

			// Token: 0x17000CEF RID: 3311
			// (set) Token: 0x0600247F RID: 9343 RVA: 0x00046F04 File Offset: 0x00045104
			public virtual bool AllowSMIMESoftCerts
			{
				set
				{
					base.PowerSharpParameters["AllowSMIMESoftCerts"] = value;
				}
			}

			// Token: 0x17000CF0 RID: 3312
			// (set) Token: 0x06002480 RID: 9344 RVA: 0x00046F1C File Offset: 0x0004511C
			public virtual bool AllowBrowser
			{
				set
				{
					base.PowerSharpParameters["AllowBrowser"] = value;
				}
			}

			// Token: 0x17000CF1 RID: 3313
			// (set) Token: 0x06002481 RID: 9345 RVA: 0x00046F34 File Offset: 0x00045134
			public virtual bool AllowConsumerEmail
			{
				set
				{
					base.PowerSharpParameters["AllowConsumerEmail"] = value;
				}
			}

			// Token: 0x17000CF2 RID: 3314
			// (set) Token: 0x06002482 RID: 9346 RVA: 0x00046F4C File Offset: 0x0004514C
			public virtual bool AllowRemoteDesktop
			{
				set
				{
					base.PowerSharpParameters["AllowRemoteDesktop"] = value;
				}
			}

			// Token: 0x17000CF3 RID: 3315
			// (set) Token: 0x06002483 RID: 9347 RVA: 0x00046F64 File Offset: 0x00045164
			public virtual bool AllowInternetSharing
			{
				set
				{
					base.PowerSharpParameters["AllowInternetSharing"] = value;
				}
			}

			// Token: 0x17000CF4 RID: 3316
			// (set) Token: 0x06002484 RID: 9348 RVA: 0x00046F7C File Offset: 0x0004517C
			public virtual BluetoothType AllowBluetooth
			{
				set
				{
					base.PowerSharpParameters["AllowBluetooth"] = value;
				}
			}

			// Token: 0x17000CF5 RID: 3317
			// (set) Token: 0x06002485 RID: 9349 RVA: 0x00046F94 File Offset: 0x00045194
			public virtual CalendarAgeFilterType MaxCalendarAgeFilter
			{
				set
				{
					base.PowerSharpParameters["MaxCalendarAgeFilter"] = value;
				}
			}

			// Token: 0x17000CF6 RID: 3318
			// (set) Token: 0x06002486 RID: 9350 RVA: 0x00046FAC File Offset: 0x000451AC
			public virtual EmailAgeFilterType MaxEmailAgeFilter
			{
				set
				{
					base.PowerSharpParameters["MaxEmailAgeFilter"] = value;
				}
			}

			// Token: 0x17000CF7 RID: 3319
			// (set) Token: 0x06002487 RID: 9351 RVA: 0x00046FC4 File Offset: 0x000451C4
			public virtual SignedSMIMEAlgorithmType RequireSignedSMIMEAlgorithm
			{
				set
				{
					base.PowerSharpParameters["RequireSignedSMIMEAlgorithm"] = value;
				}
			}

			// Token: 0x17000CF8 RID: 3320
			// (set) Token: 0x06002488 RID: 9352 RVA: 0x00046FDC File Offset: 0x000451DC
			public virtual EncryptionSMIMEAlgorithmType RequireEncryptionSMIMEAlgorithm
			{
				set
				{
					base.PowerSharpParameters["RequireEncryptionSMIMEAlgorithm"] = value;
				}
			}

			// Token: 0x17000CF9 RID: 3321
			// (set) Token: 0x06002489 RID: 9353 RVA: 0x00046FF4 File Offset: 0x000451F4
			public virtual SMIMEEncryptionAlgorithmNegotiationType AllowSMIMEEncryptionAlgorithmNegotiation
			{
				set
				{
					base.PowerSharpParameters["AllowSMIMEEncryptionAlgorithmNegotiation"] = value;
				}
			}

			// Token: 0x17000CFA RID: 3322
			// (set) Token: 0x0600248A RID: 9354 RVA: 0x0004700C File Offset: 0x0004520C
			public virtual Unlimited<int> MaxEmailBodyTruncationSize
			{
				set
				{
					base.PowerSharpParameters["MaxEmailBodyTruncationSize"] = value;
				}
			}

			// Token: 0x17000CFB RID: 3323
			// (set) Token: 0x0600248B RID: 9355 RVA: 0x00047024 File Offset: 0x00045224
			public virtual Unlimited<int> MaxEmailHTMLBodyTruncationSize
			{
				set
				{
					base.PowerSharpParameters["MaxEmailHTMLBodyTruncationSize"] = value;
				}
			}

			// Token: 0x17000CFC RID: 3324
			// (set) Token: 0x0600248C RID: 9356 RVA: 0x0004703C File Offset: 0x0004523C
			public virtual MultiValuedProperty<string> UnapprovedInROMApplicationList
			{
				set
				{
					base.PowerSharpParameters["UnapprovedInROMApplicationList"] = value;
				}
			}

			// Token: 0x17000CFD RID: 3325
			// (set) Token: 0x0600248D RID: 9357 RVA: 0x0004704F File Offset: 0x0004524F
			public virtual ApprovedApplicationCollection ApprovedApplicationList
			{
				set
				{
					base.PowerSharpParameters["ApprovedApplicationList"] = value;
				}
			}

			// Token: 0x17000CFE RID: 3326
			// (set) Token: 0x0600248E RID: 9358 RVA: 0x00047062 File Offset: 0x00045262
			public virtual bool AllowExternalDeviceManagement
			{
				set
				{
					base.PowerSharpParameters["AllowExternalDeviceManagement"] = value;
				}
			}

			// Token: 0x17000CFF RID: 3327
			// (set) Token: 0x0600248F RID: 9359 RVA: 0x0004707A File Offset: 0x0004527A
			public virtual MobileOTAUpdateModeType MobileOTAUpdateMode
			{
				set
				{
					base.PowerSharpParameters["MobileOTAUpdateMode"] = value;
				}
			}

			// Token: 0x17000D00 RID: 3328
			// (set) Token: 0x06002490 RID: 9360 RVA: 0x00047092 File Offset: 0x00045292
			public virtual bool AllowMobileOTAUpdate
			{
				set
				{
					base.PowerSharpParameters["AllowMobileOTAUpdate"] = value;
				}
			}

			// Token: 0x17000D01 RID: 3329
			// (set) Token: 0x06002491 RID: 9361 RVA: 0x000470AA File Offset: 0x000452AA
			public virtual bool IrmEnabled
			{
				set
				{
					base.PowerSharpParameters["IrmEnabled"] = value;
				}
			}

			// Token: 0x17000D02 RID: 3330
			// (set) Token: 0x06002492 RID: 9362 RVA: 0x000470C2 File Offset: 0x000452C2
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000D03 RID: 3331
			// (set) Token: 0x06002493 RID: 9363 RVA: 0x000470D5 File Offset: 0x000452D5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000D04 RID: 3332
			// (set) Token: 0x06002494 RID: 9364 RVA: 0x000470ED File Offset: 0x000452ED
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000D05 RID: 3333
			// (set) Token: 0x06002495 RID: 9365 RVA: 0x00047105 File Offset: 0x00045305
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000D06 RID: 3334
			// (set) Token: 0x06002496 RID: 9366 RVA: 0x0004711D File Offset: 0x0004531D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000D07 RID: 3335
			// (set) Token: 0x06002497 RID: 9367 RVA: 0x00047135 File Offset: 0x00045335
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
