using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200019A RID: 410
	public class NewActiveSyncMailboxPolicyCommand : SyntheticCommandWithPipelineInput<ActiveSyncMailboxPolicy, ActiveSyncMailboxPolicy>
	{
		// Token: 0x060023AC RID: 9132 RVA: 0x00045C6B File Offset: 0x00043E6B
		private NewActiveSyncMailboxPolicyCommand() : base("New-ActiveSyncMailboxPolicy")
		{
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x00045C78 File Offset: 0x00043E78
		public NewActiveSyncMailboxPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x00045C87 File Offset: 0x00043E87
		public virtual NewActiveSyncMailboxPolicyCommand SetParameters(NewActiveSyncMailboxPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200019B RID: 411
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000C31 RID: 3121
			// (set) Token: 0x060023AF RID: 9135 RVA: 0x00045C91 File Offset: 0x00043E91
			public virtual bool AttachmentsEnabled
			{
				set
				{
					base.PowerSharpParameters["AttachmentsEnabled"] = value;
				}
			}

			// Token: 0x17000C32 RID: 3122
			// (set) Token: 0x060023B0 RID: 9136 RVA: 0x00045CA9 File Offset: 0x00043EA9
			public virtual bool DevicePasswordEnabled
			{
				set
				{
					base.PowerSharpParameters["DevicePasswordEnabled"] = value;
				}
			}

			// Token: 0x17000C33 RID: 3123
			// (set) Token: 0x060023B1 RID: 9137 RVA: 0x00045CC1 File Offset: 0x00043EC1
			public virtual bool AlphanumericDevicePasswordRequired
			{
				set
				{
					base.PowerSharpParameters["AlphanumericDevicePasswordRequired"] = value;
				}
			}

			// Token: 0x17000C34 RID: 3124
			// (set) Token: 0x060023B2 RID: 9138 RVA: 0x00045CD9 File Offset: 0x00043ED9
			public virtual bool PasswordRecoveryEnabled
			{
				set
				{
					base.PowerSharpParameters["PasswordRecoveryEnabled"] = value;
				}
			}

			// Token: 0x17000C35 RID: 3125
			// (set) Token: 0x060023B3 RID: 9139 RVA: 0x00045CF1 File Offset: 0x00043EF1
			public virtual bool DeviceEncryptionEnabled
			{
				set
				{
					base.PowerSharpParameters["DeviceEncryptionEnabled"] = value;
				}
			}

			// Token: 0x17000C36 RID: 3126
			// (set) Token: 0x060023B4 RID: 9140 RVA: 0x00045D09 File Offset: 0x00043F09
			public virtual int? MinDevicePasswordLength
			{
				set
				{
					base.PowerSharpParameters["MinDevicePasswordLength"] = value;
				}
			}

			// Token: 0x17000C37 RID: 3127
			// (set) Token: 0x060023B5 RID: 9141 RVA: 0x00045D21 File Offset: 0x00043F21
			public virtual Unlimited<EnhancedTimeSpan> MaxInactivityTimeDeviceLock
			{
				set
				{
					base.PowerSharpParameters["MaxInactivityTimeDeviceLock"] = value;
				}
			}

			// Token: 0x17000C38 RID: 3128
			// (set) Token: 0x060023B6 RID: 9142 RVA: 0x00045D39 File Offset: 0x00043F39
			public virtual Unlimited<int> MaxDevicePasswordFailedAttempts
			{
				set
				{
					base.PowerSharpParameters["MaxDevicePasswordFailedAttempts"] = value;
				}
			}

			// Token: 0x17000C39 RID: 3129
			// (set) Token: 0x060023B7 RID: 9143 RVA: 0x00045D51 File Offset: 0x00043F51
			public virtual bool AllowNonProvisionableDevices
			{
				set
				{
					base.PowerSharpParameters["AllowNonProvisionableDevices"] = value;
				}
			}

			// Token: 0x17000C3A RID: 3130
			// (set) Token: 0x060023B8 RID: 9144 RVA: 0x00045D69 File Offset: 0x00043F69
			public virtual Unlimited<ByteQuantifiedSize> MaxAttachmentSize
			{
				set
				{
					base.PowerSharpParameters["MaxAttachmentSize"] = value;
				}
			}

			// Token: 0x17000C3B RID: 3131
			// (set) Token: 0x060023B9 RID: 9145 RVA: 0x00045D81 File Offset: 0x00043F81
			public virtual bool AllowSimpleDevicePassword
			{
				set
				{
					base.PowerSharpParameters["AllowSimpleDevicePassword"] = value;
				}
			}

			// Token: 0x17000C3C RID: 3132
			// (set) Token: 0x060023BA RID: 9146 RVA: 0x00045D99 File Offset: 0x00043F99
			public virtual Unlimited<EnhancedTimeSpan> DevicePasswordExpiration
			{
				set
				{
					base.PowerSharpParameters["DevicePasswordExpiration"] = value;
				}
			}

			// Token: 0x17000C3D RID: 3133
			// (set) Token: 0x060023BB RID: 9147 RVA: 0x00045DB1 File Offset: 0x00043FB1
			public virtual int DevicePasswordHistory
			{
				set
				{
					base.PowerSharpParameters["DevicePasswordHistory"] = value;
				}
			}

			// Token: 0x17000C3E RID: 3134
			// (set) Token: 0x060023BC RID: 9148 RVA: 0x00045DC9 File Offset: 0x00043FC9
			public virtual Unlimited<EnhancedTimeSpan> DevicePolicyRefreshInterval
			{
				set
				{
					base.PowerSharpParameters["DevicePolicyRefreshInterval"] = value;
				}
			}

			// Token: 0x17000C3F RID: 3135
			// (set) Token: 0x060023BD RID: 9149 RVA: 0x00045DE1 File Offset: 0x00043FE1
			public virtual bool WSSAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["WSSAccessEnabled"] = value;
				}
			}

			// Token: 0x17000C40 RID: 3136
			// (set) Token: 0x060023BE RID: 9150 RVA: 0x00045DF9 File Offset: 0x00043FF9
			public virtual bool UNCAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["UNCAccessEnabled"] = value;
				}
			}

			// Token: 0x17000C41 RID: 3137
			// (set) Token: 0x060023BF RID: 9151 RVA: 0x00045E11 File Offset: 0x00044011
			public virtual bool IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x17000C42 RID: 3138
			// (set) Token: 0x060023C0 RID: 9152 RVA: 0x00045E29 File Offset: 0x00044029
			public virtual bool IsDefaultPolicy
			{
				set
				{
					base.PowerSharpParameters["IsDefaultPolicy"] = value;
				}
			}

			// Token: 0x17000C43 RID: 3139
			// (set) Token: 0x060023C1 RID: 9153 RVA: 0x00045E41 File Offset: 0x00044041
			public virtual bool AllowStorageCard
			{
				set
				{
					base.PowerSharpParameters["AllowStorageCard"] = value;
				}
			}

			// Token: 0x17000C44 RID: 3140
			// (set) Token: 0x060023C2 RID: 9154 RVA: 0x00045E59 File Offset: 0x00044059
			public virtual bool AllowCamera
			{
				set
				{
					base.PowerSharpParameters["AllowCamera"] = value;
				}
			}

			// Token: 0x17000C45 RID: 3141
			// (set) Token: 0x060023C3 RID: 9155 RVA: 0x00045E71 File Offset: 0x00044071
			public virtual bool RequireStorageCardEncryption
			{
				set
				{
					base.PowerSharpParameters["RequireStorageCardEncryption"] = value;
				}
			}

			// Token: 0x17000C46 RID: 3142
			// (set) Token: 0x060023C4 RID: 9156 RVA: 0x00045E89 File Offset: 0x00044089
			public virtual bool RequireDeviceEncryption
			{
				set
				{
					base.PowerSharpParameters["RequireDeviceEncryption"] = value;
				}
			}

			// Token: 0x17000C47 RID: 3143
			// (set) Token: 0x060023C5 RID: 9157 RVA: 0x00045EA1 File Offset: 0x000440A1
			public virtual bool AllowUnsignedApplications
			{
				set
				{
					base.PowerSharpParameters["AllowUnsignedApplications"] = value;
				}
			}

			// Token: 0x17000C48 RID: 3144
			// (set) Token: 0x060023C6 RID: 9158 RVA: 0x00045EB9 File Offset: 0x000440B9
			public virtual bool AllowUnsignedInstallationPackages
			{
				set
				{
					base.PowerSharpParameters["AllowUnsignedInstallationPackages"] = value;
				}
			}

			// Token: 0x17000C49 RID: 3145
			// (set) Token: 0x060023C7 RID: 9159 RVA: 0x00045ED1 File Offset: 0x000440D1
			public virtual int MinDevicePasswordComplexCharacters
			{
				set
				{
					base.PowerSharpParameters["MinDevicePasswordComplexCharacters"] = value;
				}
			}

			// Token: 0x17000C4A RID: 3146
			// (set) Token: 0x060023C8 RID: 9160 RVA: 0x00045EE9 File Offset: 0x000440E9
			public virtual bool AllowWiFi
			{
				set
				{
					base.PowerSharpParameters["AllowWiFi"] = value;
				}
			}

			// Token: 0x17000C4B RID: 3147
			// (set) Token: 0x060023C9 RID: 9161 RVA: 0x00045F01 File Offset: 0x00044101
			public virtual bool AllowTextMessaging
			{
				set
				{
					base.PowerSharpParameters["AllowTextMessaging"] = value;
				}
			}

			// Token: 0x17000C4C RID: 3148
			// (set) Token: 0x060023CA RID: 9162 RVA: 0x00045F19 File Offset: 0x00044119
			public virtual bool AllowPOPIMAPEmail
			{
				set
				{
					base.PowerSharpParameters["AllowPOPIMAPEmail"] = value;
				}
			}

			// Token: 0x17000C4D RID: 3149
			// (set) Token: 0x060023CB RID: 9163 RVA: 0x00045F31 File Offset: 0x00044131
			public virtual BluetoothType AllowBluetooth
			{
				set
				{
					base.PowerSharpParameters["AllowBluetooth"] = value;
				}
			}

			// Token: 0x17000C4E RID: 3150
			// (set) Token: 0x060023CC RID: 9164 RVA: 0x00045F49 File Offset: 0x00044149
			public virtual bool AllowIrDA
			{
				set
				{
					base.PowerSharpParameters["AllowIrDA"] = value;
				}
			}

			// Token: 0x17000C4F RID: 3151
			// (set) Token: 0x060023CD RID: 9165 RVA: 0x00045F61 File Offset: 0x00044161
			public virtual bool RequireManualSyncWhenRoaming
			{
				set
				{
					base.PowerSharpParameters["RequireManualSyncWhenRoaming"] = value;
				}
			}

			// Token: 0x17000C50 RID: 3152
			// (set) Token: 0x060023CE RID: 9166 RVA: 0x00045F79 File Offset: 0x00044179
			public virtual bool AllowDesktopSync
			{
				set
				{
					base.PowerSharpParameters["AllowDesktopSync"] = value;
				}
			}

			// Token: 0x17000C51 RID: 3153
			// (set) Token: 0x060023CF RID: 9167 RVA: 0x00045F91 File Offset: 0x00044191
			public virtual CalendarAgeFilterType MaxCalendarAgeFilter
			{
				set
				{
					base.PowerSharpParameters["MaxCalendarAgeFilter"] = value;
				}
			}

			// Token: 0x17000C52 RID: 3154
			// (set) Token: 0x060023D0 RID: 9168 RVA: 0x00045FA9 File Offset: 0x000441A9
			public virtual bool AllowHTMLEmail
			{
				set
				{
					base.PowerSharpParameters["AllowHTMLEmail"] = value;
				}
			}

			// Token: 0x17000C53 RID: 3155
			// (set) Token: 0x060023D1 RID: 9169 RVA: 0x00045FC1 File Offset: 0x000441C1
			public virtual EmailAgeFilterType MaxEmailAgeFilter
			{
				set
				{
					base.PowerSharpParameters["MaxEmailAgeFilter"] = value;
				}
			}

			// Token: 0x17000C54 RID: 3156
			// (set) Token: 0x060023D2 RID: 9170 RVA: 0x00045FD9 File Offset: 0x000441D9
			public virtual Unlimited<int> MaxEmailBodyTruncationSize
			{
				set
				{
					base.PowerSharpParameters["MaxEmailBodyTruncationSize"] = value;
				}
			}

			// Token: 0x17000C55 RID: 3157
			// (set) Token: 0x060023D3 RID: 9171 RVA: 0x00045FF1 File Offset: 0x000441F1
			public virtual Unlimited<int> MaxEmailHTMLBodyTruncationSize
			{
				set
				{
					base.PowerSharpParameters["MaxEmailHTMLBodyTruncationSize"] = value;
				}
			}

			// Token: 0x17000C56 RID: 3158
			// (set) Token: 0x060023D4 RID: 9172 RVA: 0x00046009 File Offset: 0x00044209
			public virtual bool RequireSignedSMIMEMessages
			{
				set
				{
					base.PowerSharpParameters["RequireSignedSMIMEMessages"] = value;
				}
			}

			// Token: 0x17000C57 RID: 3159
			// (set) Token: 0x060023D5 RID: 9173 RVA: 0x00046021 File Offset: 0x00044221
			public virtual bool RequireEncryptedSMIMEMessages
			{
				set
				{
					base.PowerSharpParameters["RequireEncryptedSMIMEMessages"] = value;
				}
			}

			// Token: 0x17000C58 RID: 3160
			// (set) Token: 0x060023D6 RID: 9174 RVA: 0x00046039 File Offset: 0x00044239
			public virtual SignedSMIMEAlgorithmType RequireSignedSMIMEAlgorithm
			{
				set
				{
					base.PowerSharpParameters["RequireSignedSMIMEAlgorithm"] = value;
				}
			}

			// Token: 0x17000C59 RID: 3161
			// (set) Token: 0x060023D7 RID: 9175 RVA: 0x00046051 File Offset: 0x00044251
			public virtual EncryptionSMIMEAlgorithmType RequireEncryptionSMIMEAlgorithm
			{
				set
				{
					base.PowerSharpParameters["RequireEncryptionSMIMEAlgorithm"] = value;
				}
			}

			// Token: 0x17000C5A RID: 3162
			// (set) Token: 0x060023D8 RID: 9176 RVA: 0x00046069 File Offset: 0x00044269
			public virtual SMIMEEncryptionAlgorithmNegotiationType AllowSMIMEEncryptionAlgorithmNegotiation
			{
				set
				{
					base.PowerSharpParameters["AllowSMIMEEncryptionAlgorithmNegotiation"] = value;
				}
			}

			// Token: 0x17000C5B RID: 3163
			// (set) Token: 0x060023D9 RID: 9177 RVA: 0x00046081 File Offset: 0x00044281
			public virtual bool AllowSMIMESoftCerts
			{
				set
				{
					base.PowerSharpParameters["AllowSMIMESoftCerts"] = value;
				}
			}

			// Token: 0x17000C5C RID: 3164
			// (set) Token: 0x060023DA RID: 9178 RVA: 0x00046099 File Offset: 0x00044299
			public virtual bool AllowBrowser
			{
				set
				{
					base.PowerSharpParameters["AllowBrowser"] = value;
				}
			}

			// Token: 0x17000C5D RID: 3165
			// (set) Token: 0x060023DB RID: 9179 RVA: 0x000460B1 File Offset: 0x000442B1
			public virtual bool AllowConsumerEmail
			{
				set
				{
					base.PowerSharpParameters["AllowConsumerEmail"] = value;
				}
			}

			// Token: 0x17000C5E RID: 3166
			// (set) Token: 0x060023DC RID: 9180 RVA: 0x000460C9 File Offset: 0x000442C9
			public virtual bool AllowRemoteDesktop
			{
				set
				{
					base.PowerSharpParameters["AllowRemoteDesktop"] = value;
				}
			}

			// Token: 0x17000C5F RID: 3167
			// (set) Token: 0x060023DD RID: 9181 RVA: 0x000460E1 File Offset: 0x000442E1
			public virtual bool AllowInternetSharing
			{
				set
				{
					base.PowerSharpParameters["AllowInternetSharing"] = value;
				}
			}

			// Token: 0x17000C60 RID: 3168
			// (set) Token: 0x060023DE RID: 9182 RVA: 0x000460F9 File Offset: 0x000442F9
			public virtual MultiValuedProperty<string> UnapprovedInROMApplicationList
			{
				set
				{
					base.PowerSharpParameters["UnapprovedInROMApplicationList"] = value;
				}
			}

			// Token: 0x17000C61 RID: 3169
			// (set) Token: 0x060023DF RID: 9183 RVA: 0x0004610C File Offset: 0x0004430C
			public virtual ApprovedApplicationCollection ApprovedApplicationList
			{
				set
				{
					base.PowerSharpParameters["ApprovedApplicationList"] = value;
				}
			}

			// Token: 0x17000C62 RID: 3170
			// (set) Token: 0x060023E0 RID: 9184 RVA: 0x0004611F File Offset: 0x0004431F
			public virtual bool AllowExternalDeviceManagement
			{
				set
				{
					base.PowerSharpParameters["AllowExternalDeviceManagement"] = value;
				}
			}

			// Token: 0x17000C63 RID: 3171
			// (set) Token: 0x060023E1 RID: 9185 RVA: 0x00046137 File Offset: 0x00044337
			public virtual MobileOTAUpdateModeType MobileOTAUpdateMode
			{
				set
				{
					base.PowerSharpParameters["MobileOTAUpdateMode"] = value;
				}
			}

			// Token: 0x17000C64 RID: 3172
			// (set) Token: 0x060023E2 RID: 9186 RVA: 0x0004614F File Offset: 0x0004434F
			public virtual bool AllowMobileOTAUpdate
			{
				set
				{
					base.PowerSharpParameters["AllowMobileOTAUpdate"] = value;
				}
			}

			// Token: 0x17000C65 RID: 3173
			// (set) Token: 0x060023E3 RID: 9187 RVA: 0x00046167 File Offset: 0x00044367
			public virtual bool IrmEnabled
			{
				set
				{
					base.PowerSharpParameters["IrmEnabled"] = value;
				}
			}

			// Token: 0x17000C66 RID: 3174
			// (set) Token: 0x060023E4 RID: 9188 RVA: 0x0004617F File Offset: 0x0004437F
			public virtual bool AllowApplePushNotifications
			{
				set
				{
					base.PowerSharpParameters["AllowApplePushNotifications"] = value;
				}
			}

			// Token: 0x17000C67 RID: 3175
			// (set) Token: 0x060023E5 RID: 9189 RVA: 0x00046197 File Offset: 0x00044397
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000C68 RID: 3176
			// (set) Token: 0x060023E6 RID: 9190 RVA: 0x000461B5 File Offset: 0x000443B5
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000C69 RID: 3177
			// (set) Token: 0x060023E7 RID: 9191 RVA: 0x000461C8 File Offset: 0x000443C8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000C6A RID: 3178
			// (set) Token: 0x060023E8 RID: 9192 RVA: 0x000461DB File Offset: 0x000443DB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000C6B RID: 3179
			// (set) Token: 0x060023E9 RID: 9193 RVA: 0x000461F3 File Offset: 0x000443F3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000C6C RID: 3180
			// (set) Token: 0x060023EA RID: 9194 RVA: 0x0004620B File Offset: 0x0004440B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000C6D RID: 3181
			// (set) Token: 0x060023EB RID: 9195 RVA: 0x00046223 File Offset: 0x00044423
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000C6E RID: 3182
			// (set) Token: 0x060023EC RID: 9196 RVA: 0x0004623B File Offset: 0x0004443B
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
