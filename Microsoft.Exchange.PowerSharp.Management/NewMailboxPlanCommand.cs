using System;
using System.Globalization;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000CB4 RID: 3252
	public class NewMailboxPlanCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x0600A7D7 RID: 42967 RVA: 0x000F2E2E File Offset: 0x000F102E
		private NewMailboxPlanCommand() : base("New-MailboxPlan")
		{
		}

		// Token: 0x0600A7D8 RID: 42968 RVA: 0x000F2E3B File Offset: 0x000F103B
		public NewMailboxPlanCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600A7D9 RID: 42969 RVA: 0x000F2E4A File Offset: 0x000F104A
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.MailboxPlanParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7DA RID: 42970 RVA: 0x000F2E54 File Offset: 0x000F1054
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7DB RID: 42971 RVA: 0x000F2E5E File Offset: 0x000F105E
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.WindowsLiveCustomDomainsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7DC RID: 42972 RVA: 0x000F2E68 File Offset: 0x000F1068
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.ImportLiveIdParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7DD RID: 42973 RVA: 0x000F2E72 File Offset: 0x000F1072
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.RemovedMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7DE RID: 42974 RVA: 0x000F2E7C File Offset: 0x000F107C
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.FederatedUserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7DF RID: 42975 RVA: 0x000F2E86 File Offset: 0x000F1086
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.MicrosoftOnlineServicesFederatedUserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7E0 RID: 42976 RVA: 0x000F2E90 File Offset: 0x000F1090
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.RemoteArchiveParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7E1 RID: 42977 RVA: 0x000F2E9A File Offset: 0x000F109A
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.MicrosoftOnlineServicesIDParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7E2 RID: 42978 RVA: 0x000F2EA4 File Offset: 0x000F10A4
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.DisabledUserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7E3 RID: 42979 RVA: 0x000F2EAE File Offset: 0x000F10AE
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.UserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7E4 RID: 42980 RVA: 0x000F2EB8 File Offset: 0x000F10B8
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.WindowsLiveIDParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7E5 RID: 42981 RVA: 0x000F2EC2 File Offset: 0x000F10C2
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.DiscoveryParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7E6 RID: 42982 RVA: 0x000F2ECC File Offset: 0x000F10CC
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.TeamMailboxIWParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7E7 RID: 42983 RVA: 0x000F2ED6 File Offset: 0x000F10D6
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.ArbitrationParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7E8 RID: 42984 RVA: 0x000F2EE0 File Offset: 0x000F10E0
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.EquipmentParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7E9 RID: 42985 RVA: 0x000F2EEA File Offset: 0x000F10EA
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.AuxMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7EA RID: 42986 RVA: 0x000F2EF4 File Offset: 0x000F10F4
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.TeamMailboxITProParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7EB RID: 42987 RVA: 0x000F2EFE File Offset: 0x000F10FE
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.LinkedParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7EC RID: 42988 RVA: 0x000F2F08 File Offset: 0x000F1108
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.RoomParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7ED RID: 42989 RVA: 0x000F2F12 File Offset: 0x000F1112
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.SharedParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7EE RID: 42990 RVA: 0x000F2F1C File Offset: 0x000F111C
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.LinkedWithSyncMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7EF RID: 42991 RVA: 0x000F2F26 File Offset: 0x000F1126
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.LinkedRoomMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7F0 RID: 42992 RVA: 0x000F2F30 File Offset: 0x000F1130
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.EnableRoomMailboxAccountParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7F1 RID: 42993 RVA: 0x000F2F3A File Offset: 0x000F113A
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.AuditLogParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7F2 RID: 42994 RVA: 0x000F2F44 File Offset: 0x000F1144
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.GroupMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A7F3 RID: 42995 RVA: 0x000F2F4E File Offset: 0x000F114E
		public virtual NewMailboxPlanCommand SetParameters(NewMailboxPlanCommand.PublicFolderParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000CB5 RID: 3253
		public class MailboxPlanParameters : ParametersBase
		{
			// Token: 0x17007A28 RID: 31272
			// (set) Token: 0x0600A7F4 RID: 42996 RVA: 0x000F2F58 File Offset: 0x000F1158
			public virtual SwitchParameter IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x17007A29 RID: 31273
			// (set) Token: 0x0600A7F5 RID: 42997 RVA: 0x000F2F70 File Offset: 0x000F1170
			public virtual string MailboxPlanIndex
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanIndex"] = value;
				}
			}

			// Token: 0x17007A2A RID: 31274
			// (set) Token: 0x0600A7F6 RID: 42998 RVA: 0x000F2F83 File Offset: 0x000F1183
			public virtual MailboxPlanRelease MailboxPlanRelease
			{
				set
				{
					base.PowerSharpParameters["MailboxPlanRelease"] = value;
				}
			}

			// Token: 0x17007A2B RID: 31275
			// (set) Token: 0x0600A7F7 RID: 42999 RVA: 0x000F2F9B File Offset: 0x000F119B
			public virtual bool IsPilotMailboxPlan
			{
				set
				{
					base.PowerSharpParameters["IsPilotMailboxPlan"] = value;
				}
			}

			// Token: 0x17007A2C RID: 31276
			// (set) Token: 0x0600A7F8 RID: 43000 RVA: 0x000F2FB3 File Offset: 0x000F11B3
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17007A2D RID: 31277
			// (set) Token: 0x0600A7F9 RID: 43001 RVA: 0x000F2FD1 File Offset: 0x000F11D1
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17007A2E RID: 31278
			// (set) Token: 0x0600A7FA RID: 43002 RVA: 0x000F2FE4 File Offset: 0x000F11E4
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007A2F RID: 31279
			// (set) Token: 0x0600A7FB RID: 43003 RVA: 0x000F3002 File Offset: 0x000F1202
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007A30 RID: 31280
			// (set) Token: 0x0600A7FC RID: 43004 RVA: 0x000F3015 File Offset: 0x000F1215
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17007A31 RID: 31281
			// (set) Token: 0x0600A7FD RID: 43005 RVA: 0x000F302D File Offset: 0x000F122D
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17007A32 RID: 31282
			// (set) Token: 0x0600A7FE RID: 43006 RVA: 0x000F3045 File Offset: 0x000F1245
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007A33 RID: 31283
			// (set) Token: 0x0600A7FF RID: 43007 RVA: 0x000F3058 File Offset: 0x000F1258
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007A34 RID: 31284
			// (set) Token: 0x0600A800 RID: 43008 RVA: 0x000F3070 File Offset: 0x000F1270
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007A35 RID: 31285
			// (set) Token: 0x0600A801 RID: 43009 RVA: 0x000F3083 File Offset: 0x000F1283
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007A36 RID: 31286
			// (set) Token: 0x0600A802 RID: 43010 RVA: 0x000F3096 File Offset: 0x000F1296
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007A37 RID: 31287
			// (set) Token: 0x0600A803 RID: 43011 RVA: 0x000F30B4 File Offset: 0x000F12B4
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007A38 RID: 31288
			// (set) Token: 0x0600A804 RID: 43012 RVA: 0x000F30D2 File Offset: 0x000F12D2
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007A39 RID: 31289
			// (set) Token: 0x0600A805 RID: 43013 RVA: 0x000F30F0 File Offset: 0x000F12F0
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007A3A RID: 31290
			// (set) Token: 0x0600A806 RID: 43014 RVA: 0x000F310E File Offset: 0x000F130E
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007A3B RID: 31291
			// (set) Token: 0x0600A807 RID: 43015 RVA: 0x000F3121 File Offset: 0x000F1321
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007A3C RID: 31292
			// (set) Token: 0x0600A808 RID: 43016 RVA: 0x000F3139 File Offset: 0x000F1339
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007A3D RID: 31293
			// (set) Token: 0x0600A809 RID: 43017 RVA: 0x000F3157 File Offset: 0x000F1357
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007A3E RID: 31294
			// (set) Token: 0x0600A80A RID: 43018 RVA: 0x000F316F File Offset: 0x000F136F
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007A3F RID: 31295
			// (set) Token: 0x0600A80B RID: 43019 RVA: 0x000F3187 File Offset: 0x000F1387
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007A40 RID: 31296
			// (set) Token: 0x0600A80C RID: 43020 RVA: 0x000F319A File Offset: 0x000F139A
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007A41 RID: 31297
			// (set) Token: 0x0600A80D RID: 43021 RVA: 0x000F31B2 File Offset: 0x000F13B2
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007A42 RID: 31298
			// (set) Token: 0x0600A80E RID: 43022 RVA: 0x000F31C5 File Offset: 0x000F13C5
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007A43 RID: 31299
			// (set) Token: 0x0600A80F RID: 43023 RVA: 0x000F31D8 File Offset: 0x000F13D8
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007A44 RID: 31300
			// (set) Token: 0x0600A810 RID: 43024 RVA: 0x000F31EB File Offset: 0x000F13EB
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007A45 RID: 31301
			// (set) Token: 0x0600A811 RID: 43025 RVA: 0x000F31FE File Offset: 0x000F13FE
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007A46 RID: 31302
			// (set) Token: 0x0600A812 RID: 43026 RVA: 0x000F3216 File Offset: 0x000F1416
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007A47 RID: 31303
			// (set) Token: 0x0600A813 RID: 43027 RVA: 0x000F3229 File Offset: 0x000F1429
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007A48 RID: 31304
			// (set) Token: 0x0600A814 RID: 43028 RVA: 0x000F323C File Offset: 0x000F143C
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007A49 RID: 31305
			// (set) Token: 0x0600A815 RID: 43029 RVA: 0x000F3254 File Offset: 0x000F1454
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007A4A RID: 31306
			// (set) Token: 0x0600A816 RID: 43030 RVA: 0x000F3267 File Offset: 0x000F1467
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007A4B RID: 31307
			// (set) Token: 0x0600A817 RID: 43031 RVA: 0x000F327F File Offset: 0x000F147F
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007A4C RID: 31308
			// (set) Token: 0x0600A818 RID: 43032 RVA: 0x000F3292 File Offset: 0x000F1492
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007A4D RID: 31309
			// (set) Token: 0x0600A819 RID: 43033 RVA: 0x000F32B0 File Offset: 0x000F14B0
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007A4E RID: 31310
			// (set) Token: 0x0600A81A RID: 43034 RVA: 0x000F32CE File Offset: 0x000F14CE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007A4F RID: 31311
			// (set) Token: 0x0600A81B RID: 43035 RVA: 0x000F32E1 File Offset: 0x000F14E1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007A50 RID: 31312
			// (set) Token: 0x0600A81C RID: 43036 RVA: 0x000F32F9 File Offset: 0x000F14F9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007A51 RID: 31313
			// (set) Token: 0x0600A81D RID: 43037 RVA: 0x000F3311 File Offset: 0x000F1511
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007A52 RID: 31314
			// (set) Token: 0x0600A81E RID: 43038 RVA: 0x000F3329 File Offset: 0x000F1529
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007A53 RID: 31315
			// (set) Token: 0x0600A81F RID: 43039 RVA: 0x000F3341 File Offset: 0x000F1541
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CB6 RID: 3254
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17007A54 RID: 31316
			// (set) Token: 0x0600A821 RID: 43041 RVA: 0x000F3361 File Offset: 0x000F1561
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007A55 RID: 31317
			// (set) Token: 0x0600A822 RID: 43042 RVA: 0x000F3374 File Offset: 0x000F1574
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007A56 RID: 31318
			// (set) Token: 0x0600A823 RID: 43043 RVA: 0x000F338C File Offset: 0x000F158C
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007A57 RID: 31319
			// (set) Token: 0x0600A824 RID: 43044 RVA: 0x000F339F File Offset: 0x000F159F
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007A58 RID: 31320
			// (set) Token: 0x0600A825 RID: 43045 RVA: 0x000F33B2 File Offset: 0x000F15B2
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007A59 RID: 31321
			// (set) Token: 0x0600A826 RID: 43046 RVA: 0x000F33D0 File Offset: 0x000F15D0
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007A5A RID: 31322
			// (set) Token: 0x0600A827 RID: 43047 RVA: 0x000F33EE File Offset: 0x000F15EE
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007A5B RID: 31323
			// (set) Token: 0x0600A828 RID: 43048 RVA: 0x000F340C File Offset: 0x000F160C
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007A5C RID: 31324
			// (set) Token: 0x0600A829 RID: 43049 RVA: 0x000F342A File Offset: 0x000F162A
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007A5D RID: 31325
			// (set) Token: 0x0600A82A RID: 43050 RVA: 0x000F343D File Offset: 0x000F163D
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007A5E RID: 31326
			// (set) Token: 0x0600A82B RID: 43051 RVA: 0x000F3455 File Offset: 0x000F1655
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007A5F RID: 31327
			// (set) Token: 0x0600A82C RID: 43052 RVA: 0x000F3473 File Offset: 0x000F1673
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007A60 RID: 31328
			// (set) Token: 0x0600A82D RID: 43053 RVA: 0x000F348B File Offset: 0x000F168B
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007A61 RID: 31329
			// (set) Token: 0x0600A82E RID: 43054 RVA: 0x000F34A3 File Offset: 0x000F16A3
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007A62 RID: 31330
			// (set) Token: 0x0600A82F RID: 43055 RVA: 0x000F34B6 File Offset: 0x000F16B6
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007A63 RID: 31331
			// (set) Token: 0x0600A830 RID: 43056 RVA: 0x000F34CE File Offset: 0x000F16CE
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007A64 RID: 31332
			// (set) Token: 0x0600A831 RID: 43057 RVA: 0x000F34E1 File Offset: 0x000F16E1
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007A65 RID: 31333
			// (set) Token: 0x0600A832 RID: 43058 RVA: 0x000F34F4 File Offset: 0x000F16F4
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007A66 RID: 31334
			// (set) Token: 0x0600A833 RID: 43059 RVA: 0x000F3507 File Offset: 0x000F1707
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007A67 RID: 31335
			// (set) Token: 0x0600A834 RID: 43060 RVA: 0x000F351A File Offset: 0x000F171A
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007A68 RID: 31336
			// (set) Token: 0x0600A835 RID: 43061 RVA: 0x000F3532 File Offset: 0x000F1732
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007A69 RID: 31337
			// (set) Token: 0x0600A836 RID: 43062 RVA: 0x000F3545 File Offset: 0x000F1745
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007A6A RID: 31338
			// (set) Token: 0x0600A837 RID: 43063 RVA: 0x000F3558 File Offset: 0x000F1758
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007A6B RID: 31339
			// (set) Token: 0x0600A838 RID: 43064 RVA: 0x000F3570 File Offset: 0x000F1770
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007A6C RID: 31340
			// (set) Token: 0x0600A839 RID: 43065 RVA: 0x000F3583 File Offset: 0x000F1783
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007A6D RID: 31341
			// (set) Token: 0x0600A83A RID: 43066 RVA: 0x000F359B File Offset: 0x000F179B
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007A6E RID: 31342
			// (set) Token: 0x0600A83B RID: 43067 RVA: 0x000F35AE File Offset: 0x000F17AE
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007A6F RID: 31343
			// (set) Token: 0x0600A83C RID: 43068 RVA: 0x000F35CC File Offset: 0x000F17CC
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007A70 RID: 31344
			// (set) Token: 0x0600A83D RID: 43069 RVA: 0x000F35EA File Offset: 0x000F17EA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007A71 RID: 31345
			// (set) Token: 0x0600A83E RID: 43070 RVA: 0x000F35FD File Offset: 0x000F17FD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007A72 RID: 31346
			// (set) Token: 0x0600A83F RID: 43071 RVA: 0x000F3615 File Offset: 0x000F1815
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007A73 RID: 31347
			// (set) Token: 0x0600A840 RID: 43072 RVA: 0x000F362D File Offset: 0x000F182D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007A74 RID: 31348
			// (set) Token: 0x0600A841 RID: 43073 RVA: 0x000F3645 File Offset: 0x000F1845
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007A75 RID: 31349
			// (set) Token: 0x0600A842 RID: 43074 RVA: 0x000F365D File Offset: 0x000F185D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CB7 RID: 3255
		public class WindowsLiveCustomDomainsParameters : ParametersBase
		{
			// Token: 0x17007A76 RID: 31350
			// (set) Token: 0x0600A844 RID: 43076 RVA: 0x000F367D File Offset: 0x000F187D
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17007A77 RID: 31351
			// (set) Token: 0x0600A845 RID: 43077 RVA: 0x000F369B File Offset: 0x000F189B
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007A78 RID: 31352
			// (set) Token: 0x0600A846 RID: 43078 RVA: 0x000F36B9 File Offset: 0x000F18B9
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007A79 RID: 31353
			// (set) Token: 0x0600A847 RID: 43079 RVA: 0x000F36CC File Offset: 0x000F18CC
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17007A7A RID: 31354
			// (set) Token: 0x0600A848 RID: 43080 RVA: 0x000F36E4 File Offset: 0x000F18E4
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17007A7B RID: 31355
			// (set) Token: 0x0600A849 RID: 43081 RVA: 0x000F36FC File Offset: 0x000F18FC
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007A7C RID: 31356
			// (set) Token: 0x0600A84A RID: 43082 RVA: 0x000F371A File Offset: 0x000F191A
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x17007A7D RID: 31357
			// (set) Token: 0x0600A84B RID: 43083 RVA: 0x000F372D File Offset: 0x000F192D
			public virtual SwitchParameter UseExistingLiveId
			{
				set
				{
					base.PowerSharpParameters["UseExistingLiveId"] = value;
				}
			}

			// Token: 0x17007A7E RID: 31358
			// (set) Token: 0x0600A84C RID: 43084 RVA: 0x000F3745 File Offset: 0x000F1945
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x17007A7F RID: 31359
			// (set) Token: 0x0600A84D RID: 43085 RVA: 0x000F3758 File Offset: 0x000F1958
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007A80 RID: 31360
			// (set) Token: 0x0600A84E RID: 43086 RVA: 0x000F376B File Offset: 0x000F196B
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007A81 RID: 31361
			// (set) Token: 0x0600A84F RID: 43087 RVA: 0x000F3783 File Offset: 0x000F1983
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007A82 RID: 31362
			// (set) Token: 0x0600A850 RID: 43088 RVA: 0x000F3796 File Offset: 0x000F1996
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007A83 RID: 31363
			// (set) Token: 0x0600A851 RID: 43089 RVA: 0x000F37A9 File Offset: 0x000F19A9
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007A84 RID: 31364
			// (set) Token: 0x0600A852 RID: 43090 RVA: 0x000F37C7 File Offset: 0x000F19C7
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007A85 RID: 31365
			// (set) Token: 0x0600A853 RID: 43091 RVA: 0x000F37E5 File Offset: 0x000F19E5
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007A86 RID: 31366
			// (set) Token: 0x0600A854 RID: 43092 RVA: 0x000F3803 File Offset: 0x000F1A03
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007A87 RID: 31367
			// (set) Token: 0x0600A855 RID: 43093 RVA: 0x000F3821 File Offset: 0x000F1A21
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007A88 RID: 31368
			// (set) Token: 0x0600A856 RID: 43094 RVA: 0x000F3834 File Offset: 0x000F1A34
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007A89 RID: 31369
			// (set) Token: 0x0600A857 RID: 43095 RVA: 0x000F384C File Offset: 0x000F1A4C
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007A8A RID: 31370
			// (set) Token: 0x0600A858 RID: 43096 RVA: 0x000F386A File Offset: 0x000F1A6A
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007A8B RID: 31371
			// (set) Token: 0x0600A859 RID: 43097 RVA: 0x000F3882 File Offset: 0x000F1A82
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007A8C RID: 31372
			// (set) Token: 0x0600A85A RID: 43098 RVA: 0x000F389A File Offset: 0x000F1A9A
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007A8D RID: 31373
			// (set) Token: 0x0600A85B RID: 43099 RVA: 0x000F38AD File Offset: 0x000F1AAD
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007A8E RID: 31374
			// (set) Token: 0x0600A85C RID: 43100 RVA: 0x000F38C5 File Offset: 0x000F1AC5
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007A8F RID: 31375
			// (set) Token: 0x0600A85D RID: 43101 RVA: 0x000F38D8 File Offset: 0x000F1AD8
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007A90 RID: 31376
			// (set) Token: 0x0600A85E RID: 43102 RVA: 0x000F38EB File Offset: 0x000F1AEB
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007A91 RID: 31377
			// (set) Token: 0x0600A85F RID: 43103 RVA: 0x000F38FE File Offset: 0x000F1AFE
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007A92 RID: 31378
			// (set) Token: 0x0600A860 RID: 43104 RVA: 0x000F3911 File Offset: 0x000F1B11
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007A93 RID: 31379
			// (set) Token: 0x0600A861 RID: 43105 RVA: 0x000F3929 File Offset: 0x000F1B29
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007A94 RID: 31380
			// (set) Token: 0x0600A862 RID: 43106 RVA: 0x000F393C File Offset: 0x000F1B3C
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007A95 RID: 31381
			// (set) Token: 0x0600A863 RID: 43107 RVA: 0x000F394F File Offset: 0x000F1B4F
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007A96 RID: 31382
			// (set) Token: 0x0600A864 RID: 43108 RVA: 0x000F3967 File Offset: 0x000F1B67
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007A97 RID: 31383
			// (set) Token: 0x0600A865 RID: 43109 RVA: 0x000F397A File Offset: 0x000F1B7A
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007A98 RID: 31384
			// (set) Token: 0x0600A866 RID: 43110 RVA: 0x000F3992 File Offset: 0x000F1B92
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007A99 RID: 31385
			// (set) Token: 0x0600A867 RID: 43111 RVA: 0x000F39A5 File Offset: 0x000F1BA5
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007A9A RID: 31386
			// (set) Token: 0x0600A868 RID: 43112 RVA: 0x000F39C3 File Offset: 0x000F1BC3
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007A9B RID: 31387
			// (set) Token: 0x0600A869 RID: 43113 RVA: 0x000F39E1 File Offset: 0x000F1BE1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007A9C RID: 31388
			// (set) Token: 0x0600A86A RID: 43114 RVA: 0x000F39F4 File Offset: 0x000F1BF4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007A9D RID: 31389
			// (set) Token: 0x0600A86B RID: 43115 RVA: 0x000F3A0C File Offset: 0x000F1C0C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007A9E RID: 31390
			// (set) Token: 0x0600A86C RID: 43116 RVA: 0x000F3A24 File Offset: 0x000F1C24
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007A9F RID: 31391
			// (set) Token: 0x0600A86D RID: 43117 RVA: 0x000F3A3C File Offset: 0x000F1C3C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007AA0 RID: 31392
			// (set) Token: 0x0600A86E RID: 43118 RVA: 0x000F3A54 File Offset: 0x000F1C54
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CB8 RID: 3256
		public class ImportLiveIdParameters : ParametersBase
		{
			// Token: 0x17007AA1 RID: 31393
			// (set) Token: 0x0600A870 RID: 43120 RVA: 0x000F3A74 File Offset: 0x000F1C74
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17007AA2 RID: 31394
			// (set) Token: 0x0600A871 RID: 43121 RVA: 0x000F3A92 File Offset: 0x000F1C92
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007AA3 RID: 31395
			// (set) Token: 0x0600A872 RID: 43122 RVA: 0x000F3AB0 File Offset: 0x000F1CB0
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007AA4 RID: 31396
			// (set) Token: 0x0600A873 RID: 43123 RVA: 0x000F3AC3 File Offset: 0x000F1CC3
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17007AA5 RID: 31397
			// (set) Token: 0x0600A874 RID: 43124 RVA: 0x000F3ADB File Offset: 0x000F1CDB
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17007AA6 RID: 31398
			// (set) Token: 0x0600A875 RID: 43125 RVA: 0x000F3AF3 File Offset: 0x000F1CF3
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007AA7 RID: 31399
			// (set) Token: 0x0600A876 RID: 43126 RVA: 0x000F3B11 File Offset: 0x000F1D11
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x17007AA8 RID: 31400
			// (set) Token: 0x0600A877 RID: 43127 RVA: 0x000F3B24 File Offset: 0x000F1D24
			public virtual SwitchParameter ImportLiveId
			{
				set
				{
					base.PowerSharpParameters["ImportLiveId"] = value;
				}
			}

			// Token: 0x17007AA9 RID: 31401
			// (set) Token: 0x0600A878 RID: 43128 RVA: 0x000F3B3C File Offset: 0x000F1D3C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007AAA RID: 31402
			// (set) Token: 0x0600A879 RID: 43129 RVA: 0x000F3B4F File Offset: 0x000F1D4F
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007AAB RID: 31403
			// (set) Token: 0x0600A87A RID: 43130 RVA: 0x000F3B67 File Offset: 0x000F1D67
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007AAC RID: 31404
			// (set) Token: 0x0600A87B RID: 43131 RVA: 0x000F3B7A File Offset: 0x000F1D7A
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007AAD RID: 31405
			// (set) Token: 0x0600A87C RID: 43132 RVA: 0x000F3B8D File Offset: 0x000F1D8D
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007AAE RID: 31406
			// (set) Token: 0x0600A87D RID: 43133 RVA: 0x000F3BAB File Offset: 0x000F1DAB
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007AAF RID: 31407
			// (set) Token: 0x0600A87E RID: 43134 RVA: 0x000F3BC9 File Offset: 0x000F1DC9
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007AB0 RID: 31408
			// (set) Token: 0x0600A87F RID: 43135 RVA: 0x000F3BE7 File Offset: 0x000F1DE7
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007AB1 RID: 31409
			// (set) Token: 0x0600A880 RID: 43136 RVA: 0x000F3C05 File Offset: 0x000F1E05
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007AB2 RID: 31410
			// (set) Token: 0x0600A881 RID: 43137 RVA: 0x000F3C18 File Offset: 0x000F1E18
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007AB3 RID: 31411
			// (set) Token: 0x0600A882 RID: 43138 RVA: 0x000F3C30 File Offset: 0x000F1E30
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007AB4 RID: 31412
			// (set) Token: 0x0600A883 RID: 43139 RVA: 0x000F3C4E File Offset: 0x000F1E4E
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007AB5 RID: 31413
			// (set) Token: 0x0600A884 RID: 43140 RVA: 0x000F3C66 File Offset: 0x000F1E66
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007AB6 RID: 31414
			// (set) Token: 0x0600A885 RID: 43141 RVA: 0x000F3C7E File Offset: 0x000F1E7E
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007AB7 RID: 31415
			// (set) Token: 0x0600A886 RID: 43142 RVA: 0x000F3C91 File Offset: 0x000F1E91
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007AB8 RID: 31416
			// (set) Token: 0x0600A887 RID: 43143 RVA: 0x000F3CA9 File Offset: 0x000F1EA9
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007AB9 RID: 31417
			// (set) Token: 0x0600A888 RID: 43144 RVA: 0x000F3CBC File Offset: 0x000F1EBC
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007ABA RID: 31418
			// (set) Token: 0x0600A889 RID: 43145 RVA: 0x000F3CCF File Offset: 0x000F1ECF
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007ABB RID: 31419
			// (set) Token: 0x0600A88A RID: 43146 RVA: 0x000F3CE2 File Offset: 0x000F1EE2
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007ABC RID: 31420
			// (set) Token: 0x0600A88B RID: 43147 RVA: 0x000F3CF5 File Offset: 0x000F1EF5
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007ABD RID: 31421
			// (set) Token: 0x0600A88C RID: 43148 RVA: 0x000F3D0D File Offset: 0x000F1F0D
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007ABE RID: 31422
			// (set) Token: 0x0600A88D RID: 43149 RVA: 0x000F3D20 File Offset: 0x000F1F20
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007ABF RID: 31423
			// (set) Token: 0x0600A88E RID: 43150 RVA: 0x000F3D33 File Offset: 0x000F1F33
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007AC0 RID: 31424
			// (set) Token: 0x0600A88F RID: 43151 RVA: 0x000F3D4B File Offset: 0x000F1F4B
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007AC1 RID: 31425
			// (set) Token: 0x0600A890 RID: 43152 RVA: 0x000F3D5E File Offset: 0x000F1F5E
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007AC2 RID: 31426
			// (set) Token: 0x0600A891 RID: 43153 RVA: 0x000F3D76 File Offset: 0x000F1F76
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007AC3 RID: 31427
			// (set) Token: 0x0600A892 RID: 43154 RVA: 0x000F3D89 File Offset: 0x000F1F89
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007AC4 RID: 31428
			// (set) Token: 0x0600A893 RID: 43155 RVA: 0x000F3DA7 File Offset: 0x000F1FA7
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007AC5 RID: 31429
			// (set) Token: 0x0600A894 RID: 43156 RVA: 0x000F3DC5 File Offset: 0x000F1FC5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007AC6 RID: 31430
			// (set) Token: 0x0600A895 RID: 43157 RVA: 0x000F3DD8 File Offset: 0x000F1FD8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007AC7 RID: 31431
			// (set) Token: 0x0600A896 RID: 43158 RVA: 0x000F3DF0 File Offset: 0x000F1FF0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007AC8 RID: 31432
			// (set) Token: 0x0600A897 RID: 43159 RVA: 0x000F3E08 File Offset: 0x000F2008
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007AC9 RID: 31433
			// (set) Token: 0x0600A898 RID: 43160 RVA: 0x000F3E20 File Offset: 0x000F2020
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007ACA RID: 31434
			// (set) Token: 0x0600A899 RID: 43161 RVA: 0x000F3E38 File Offset: 0x000F2038
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CB9 RID: 3257
		public class RemovedMailboxParameters : ParametersBase
		{
			// Token: 0x17007ACB RID: 31435
			// (set) Token: 0x0600A89B RID: 43163 RVA: 0x000F3E58 File Offset: 0x000F2058
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17007ACC RID: 31436
			// (set) Token: 0x0600A89C RID: 43164 RVA: 0x000F3E76 File Offset: 0x000F2076
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17007ACD RID: 31437
			// (set) Token: 0x0600A89D RID: 43165 RVA: 0x000F3E89 File Offset: 0x000F2089
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007ACE RID: 31438
			// (set) Token: 0x0600A89E RID: 43166 RVA: 0x000F3EA7 File Offset: 0x000F20A7
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007ACF RID: 31439
			// (set) Token: 0x0600A89F RID: 43167 RVA: 0x000F3EBA File Offset: 0x000F20BA
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17007AD0 RID: 31440
			// (set) Token: 0x0600A8A0 RID: 43168 RVA: 0x000F3ED2 File Offset: 0x000F20D2
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17007AD1 RID: 31441
			// (set) Token: 0x0600A8A1 RID: 43169 RVA: 0x000F3EEA File Offset: 0x000F20EA
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007AD2 RID: 31442
			// (set) Token: 0x0600A8A2 RID: 43170 RVA: 0x000F3F08 File Offset: 0x000F2108
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007AD3 RID: 31443
			// (set) Token: 0x0600A8A3 RID: 43171 RVA: 0x000F3F1B File Offset: 0x000F211B
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007AD4 RID: 31444
			// (set) Token: 0x0600A8A4 RID: 43172 RVA: 0x000F3F33 File Offset: 0x000F2133
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007AD5 RID: 31445
			// (set) Token: 0x0600A8A5 RID: 43173 RVA: 0x000F3F46 File Offset: 0x000F2146
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007AD6 RID: 31446
			// (set) Token: 0x0600A8A6 RID: 43174 RVA: 0x000F3F59 File Offset: 0x000F2159
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007AD7 RID: 31447
			// (set) Token: 0x0600A8A7 RID: 43175 RVA: 0x000F3F77 File Offset: 0x000F2177
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007AD8 RID: 31448
			// (set) Token: 0x0600A8A8 RID: 43176 RVA: 0x000F3F95 File Offset: 0x000F2195
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007AD9 RID: 31449
			// (set) Token: 0x0600A8A9 RID: 43177 RVA: 0x000F3FB3 File Offset: 0x000F21B3
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007ADA RID: 31450
			// (set) Token: 0x0600A8AA RID: 43178 RVA: 0x000F3FD1 File Offset: 0x000F21D1
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007ADB RID: 31451
			// (set) Token: 0x0600A8AB RID: 43179 RVA: 0x000F3FE4 File Offset: 0x000F21E4
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007ADC RID: 31452
			// (set) Token: 0x0600A8AC RID: 43180 RVA: 0x000F3FFC File Offset: 0x000F21FC
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007ADD RID: 31453
			// (set) Token: 0x0600A8AD RID: 43181 RVA: 0x000F401A File Offset: 0x000F221A
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007ADE RID: 31454
			// (set) Token: 0x0600A8AE RID: 43182 RVA: 0x000F4032 File Offset: 0x000F2232
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007ADF RID: 31455
			// (set) Token: 0x0600A8AF RID: 43183 RVA: 0x000F404A File Offset: 0x000F224A
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007AE0 RID: 31456
			// (set) Token: 0x0600A8B0 RID: 43184 RVA: 0x000F405D File Offset: 0x000F225D
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007AE1 RID: 31457
			// (set) Token: 0x0600A8B1 RID: 43185 RVA: 0x000F4075 File Offset: 0x000F2275
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007AE2 RID: 31458
			// (set) Token: 0x0600A8B2 RID: 43186 RVA: 0x000F4088 File Offset: 0x000F2288
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007AE3 RID: 31459
			// (set) Token: 0x0600A8B3 RID: 43187 RVA: 0x000F409B File Offset: 0x000F229B
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007AE4 RID: 31460
			// (set) Token: 0x0600A8B4 RID: 43188 RVA: 0x000F40AE File Offset: 0x000F22AE
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007AE5 RID: 31461
			// (set) Token: 0x0600A8B5 RID: 43189 RVA: 0x000F40C1 File Offset: 0x000F22C1
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007AE6 RID: 31462
			// (set) Token: 0x0600A8B6 RID: 43190 RVA: 0x000F40D9 File Offset: 0x000F22D9
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007AE7 RID: 31463
			// (set) Token: 0x0600A8B7 RID: 43191 RVA: 0x000F40EC File Offset: 0x000F22EC
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007AE8 RID: 31464
			// (set) Token: 0x0600A8B8 RID: 43192 RVA: 0x000F40FF File Offset: 0x000F22FF
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007AE9 RID: 31465
			// (set) Token: 0x0600A8B9 RID: 43193 RVA: 0x000F4117 File Offset: 0x000F2317
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007AEA RID: 31466
			// (set) Token: 0x0600A8BA RID: 43194 RVA: 0x000F412A File Offset: 0x000F232A
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007AEB RID: 31467
			// (set) Token: 0x0600A8BB RID: 43195 RVA: 0x000F4142 File Offset: 0x000F2342
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007AEC RID: 31468
			// (set) Token: 0x0600A8BC RID: 43196 RVA: 0x000F4155 File Offset: 0x000F2355
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007AED RID: 31469
			// (set) Token: 0x0600A8BD RID: 43197 RVA: 0x000F4173 File Offset: 0x000F2373
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007AEE RID: 31470
			// (set) Token: 0x0600A8BE RID: 43198 RVA: 0x000F4191 File Offset: 0x000F2391
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007AEF RID: 31471
			// (set) Token: 0x0600A8BF RID: 43199 RVA: 0x000F41A4 File Offset: 0x000F23A4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007AF0 RID: 31472
			// (set) Token: 0x0600A8C0 RID: 43200 RVA: 0x000F41BC File Offset: 0x000F23BC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007AF1 RID: 31473
			// (set) Token: 0x0600A8C1 RID: 43201 RVA: 0x000F41D4 File Offset: 0x000F23D4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007AF2 RID: 31474
			// (set) Token: 0x0600A8C2 RID: 43202 RVA: 0x000F41EC File Offset: 0x000F23EC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007AF3 RID: 31475
			// (set) Token: 0x0600A8C3 RID: 43203 RVA: 0x000F4204 File Offset: 0x000F2404
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CBA RID: 3258
		public class FederatedUserParameters : ParametersBase
		{
			// Token: 0x17007AF4 RID: 31476
			// (set) Token: 0x0600A8C5 RID: 43205 RVA: 0x000F4224 File Offset: 0x000F2424
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17007AF5 RID: 31477
			// (set) Token: 0x0600A8C6 RID: 43206 RVA: 0x000F4242 File Offset: 0x000F2442
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007AF6 RID: 31478
			// (set) Token: 0x0600A8C7 RID: 43207 RVA: 0x000F4260 File Offset: 0x000F2460
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x17007AF7 RID: 31479
			// (set) Token: 0x0600A8C8 RID: 43208 RVA: 0x000F4273 File Offset: 0x000F2473
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x17007AF8 RID: 31480
			// (set) Token: 0x0600A8C9 RID: 43209 RVA: 0x000F4286 File Offset: 0x000F2486
			public virtual SwitchParameter EvictLiveId
			{
				set
				{
					base.PowerSharpParameters["EvictLiveId"] = value;
				}
			}

			// Token: 0x17007AF9 RID: 31481
			// (set) Token: 0x0600A8CA RID: 43210 RVA: 0x000F429E File Offset: 0x000F249E
			public virtual string FederatedIdentity
			{
				set
				{
					base.PowerSharpParameters["FederatedIdentity"] = value;
				}
			}

			// Token: 0x17007AFA RID: 31482
			// (set) Token: 0x0600A8CB RID: 43211 RVA: 0x000F42B1 File Offset: 0x000F24B1
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007AFB RID: 31483
			// (set) Token: 0x0600A8CC RID: 43212 RVA: 0x000F42C4 File Offset: 0x000F24C4
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007AFC RID: 31484
			// (set) Token: 0x0600A8CD RID: 43213 RVA: 0x000F42DC File Offset: 0x000F24DC
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007AFD RID: 31485
			// (set) Token: 0x0600A8CE RID: 43214 RVA: 0x000F42EF File Offset: 0x000F24EF
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007AFE RID: 31486
			// (set) Token: 0x0600A8CF RID: 43215 RVA: 0x000F4302 File Offset: 0x000F2502
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007AFF RID: 31487
			// (set) Token: 0x0600A8D0 RID: 43216 RVA: 0x000F4320 File Offset: 0x000F2520
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007B00 RID: 31488
			// (set) Token: 0x0600A8D1 RID: 43217 RVA: 0x000F433E File Offset: 0x000F253E
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007B01 RID: 31489
			// (set) Token: 0x0600A8D2 RID: 43218 RVA: 0x000F435C File Offset: 0x000F255C
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007B02 RID: 31490
			// (set) Token: 0x0600A8D3 RID: 43219 RVA: 0x000F437A File Offset: 0x000F257A
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007B03 RID: 31491
			// (set) Token: 0x0600A8D4 RID: 43220 RVA: 0x000F438D File Offset: 0x000F258D
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007B04 RID: 31492
			// (set) Token: 0x0600A8D5 RID: 43221 RVA: 0x000F43A5 File Offset: 0x000F25A5
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007B05 RID: 31493
			// (set) Token: 0x0600A8D6 RID: 43222 RVA: 0x000F43C3 File Offset: 0x000F25C3
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007B06 RID: 31494
			// (set) Token: 0x0600A8D7 RID: 43223 RVA: 0x000F43DB File Offset: 0x000F25DB
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007B07 RID: 31495
			// (set) Token: 0x0600A8D8 RID: 43224 RVA: 0x000F43F3 File Offset: 0x000F25F3
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007B08 RID: 31496
			// (set) Token: 0x0600A8D9 RID: 43225 RVA: 0x000F4406 File Offset: 0x000F2606
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007B09 RID: 31497
			// (set) Token: 0x0600A8DA RID: 43226 RVA: 0x000F441E File Offset: 0x000F261E
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007B0A RID: 31498
			// (set) Token: 0x0600A8DB RID: 43227 RVA: 0x000F4431 File Offset: 0x000F2631
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007B0B RID: 31499
			// (set) Token: 0x0600A8DC RID: 43228 RVA: 0x000F4444 File Offset: 0x000F2644
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007B0C RID: 31500
			// (set) Token: 0x0600A8DD RID: 43229 RVA: 0x000F4457 File Offset: 0x000F2657
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007B0D RID: 31501
			// (set) Token: 0x0600A8DE RID: 43230 RVA: 0x000F446A File Offset: 0x000F266A
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007B0E RID: 31502
			// (set) Token: 0x0600A8DF RID: 43231 RVA: 0x000F4482 File Offset: 0x000F2682
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007B0F RID: 31503
			// (set) Token: 0x0600A8E0 RID: 43232 RVA: 0x000F4495 File Offset: 0x000F2695
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007B10 RID: 31504
			// (set) Token: 0x0600A8E1 RID: 43233 RVA: 0x000F44A8 File Offset: 0x000F26A8
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007B11 RID: 31505
			// (set) Token: 0x0600A8E2 RID: 43234 RVA: 0x000F44C0 File Offset: 0x000F26C0
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007B12 RID: 31506
			// (set) Token: 0x0600A8E3 RID: 43235 RVA: 0x000F44D3 File Offset: 0x000F26D3
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007B13 RID: 31507
			// (set) Token: 0x0600A8E4 RID: 43236 RVA: 0x000F44EB File Offset: 0x000F26EB
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007B14 RID: 31508
			// (set) Token: 0x0600A8E5 RID: 43237 RVA: 0x000F44FE File Offset: 0x000F26FE
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007B15 RID: 31509
			// (set) Token: 0x0600A8E6 RID: 43238 RVA: 0x000F451C File Offset: 0x000F271C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007B16 RID: 31510
			// (set) Token: 0x0600A8E7 RID: 43239 RVA: 0x000F453A File Offset: 0x000F273A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007B17 RID: 31511
			// (set) Token: 0x0600A8E8 RID: 43240 RVA: 0x000F454D File Offset: 0x000F274D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007B18 RID: 31512
			// (set) Token: 0x0600A8E9 RID: 43241 RVA: 0x000F4565 File Offset: 0x000F2765
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007B19 RID: 31513
			// (set) Token: 0x0600A8EA RID: 43242 RVA: 0x000F457D File Offset: 0x000F277D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007B1A RID: 31514
			// (set) Token: 0x0600A8EB RID: 43243 RVA: 0x000F4595 File Offset: 0x000F2795
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007B1B RID: 31515
			// (set) Token: 0x0600A8EC RID: 43244 RVA: 0x000F45AD File Offset: 0x000F27AD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CBB RID: 3259
		public class MicrosoftOnlineServicesFederatedUserParameters : ParametersBase
		{
			// Token: 0x17007B1C RID: 31516
			// (set) Token: 0x0600A8EE RID: 43246 RVA: 0x000F45CD File Offset: 0x000F27CD
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17007B1D RID: 31517
			// (set) Token: 0x0600A8EF RID: 43247 RVA: 0x000F45EB File Offset: 0x000F27EB
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007B1E RID: 31518
			// (set) Token: 0x0600A8F0 RID: 43248 RVA: 0x000F4609 File Offset: 0x000F2809
			public virtual WindowsLiveId MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x17007B1F RID: 31519
			// (set) Token: 0x0600A8F1 RID: 43249 RVA: 0x000F461C File Offset: 0x000F281C
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x17007B20 RID: 31520
			// (set) Token: 0x0600A8F2 RID: 43250 RVA: 0x000F462F File Offset: 0x000F282F
			public virtual string FederatedIdentity
			{
				set
				{
					base.PowerSharpParameters["FederatedIdentity"] = value;
				}
			}

			// Token: 0x17007B21 RID: 31521
			// (set) Token: 0x0600A8F3 RID: 43251 RVA: 0x000F4642 File Offset: 0x000F2842
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007B22 RID: 31522
			// (set) Token: 0x0600A8F4 RID: 43252 RVA: 0x000F4655 File Offset: 0x000F2855
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007B23 RID: 31523
			// (set) Token: 0x0600A8F5 RID: 43253 RVA: 0x000F466D File Offset: 0x000F286D
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007B24 RID: 31524
			// (set) Token: 0x0600A8F6 RID: 43254 RVA: 0x000F4680 File Offset: 0x000F2880
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007B25 RID: 31525
			// (set) Token: 0x0600A8F7 RID: 43255 RVA: 0x000F4693 File Offset: 0x000F2893
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007B26 RID: 31526
			// (set) Token: 0x0600A8F8 RID: 43256 RVA: 0x000F46B1 File Offset: 0x000F28B1
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007B27 RID: 31527
			// (set) Token: 0x0600A8F9 RID: 43257 RVA: 0x000F46CF File Offset: 0x000F28CF
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007B28 RID: 31528
			// (set) Token: 0x0600A8FA RID: 43258 RVA: 0x000F46ED File Offset: 0x000F28ED
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007B29 RID: 31529
			// (set) Token: 0x0600A8FB RID: 43259 RVA: 0x000F470B File Offset: 0x000F290B
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007B2A RID: 31530
			// (set) Token: 0x0600A8FC RID: 43260 RVA: 0x000F471E File Offset: 0x000F291E
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007B2B RID: 31531
			// (set) Token: 0x0600A8FD RID: 43261 RVA: 0x000F4736 File Offset: 0x000F2936
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007B2C RID: 31532
			// (set) Token: 0x0600A8FE RID: 43262 RVA: 0x000F4754 File Offset: 0x000F2954
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007B2D RID: 31533
			// (set) Token: 0x0600A8FF RID: 43263 RVA: 0x000F476C File Offset: 0x000F296C
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007B2E RID: 31534
			// (set) Token: 0x0600A900 RID: 43264 RVA: 0x000F4784 File Offset: 0x000F2984
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007B2F RID: 31535
			// (set) Token: 0x0600A901 RID: 43265 RVA: 0x000F4797 File Offset: 0x000F2997
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007B30 RID: 31536
			// (set) Token: 0x0600A902 RID: 43266 RVA: 0x000F47AF File Offset: 0x000F29AF
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007B31 RID: 31537
			// (set) Token: 0x0600A903 RID: 43267 RVA: 0x000F47C2 File Offset: 0x000F29C2
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007B32 RID: 31538
			// (set) Token: 0x0600A904 RID: 43268 RVA: 0x000F47D5 File Offset: 0x000F29D5
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007B33 RID: 31539
			// (set) Token: 0x0600A905 RID: 43269 RVA: 0x000F47E8 File Offset: 0x000F29E8
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007B34 RID: 31540
			// (set) Token: 0x0600A906 RID: 43270 RVA: 0x000F47FB File Offset: 0x000F29FB
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007B35 RID: 31541
			// (set) Token: 0x0600A907 RID: 43271 RVA: 0x000F4813 File Offset: 0x000F2A13
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007B36 RID: 31542
			// (set) Token: 0x0600A908 RID: 43272 RVA: 0x000F4826 File Offset: 0x000F2A26
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007B37 RID: 31543
			// (set) Token: 0x0600A909 RID: 43273 RVA: 0x000F4839 File Offset: 0x000F2A39
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007B38 RID: 31544
			// (set) Token: 0x0600A90A RID: 43274 RVA: 0x000F4851 File Offset: 0x000F2A51
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007B39 RID: 31545
			// (set) Token: 0x0600A90B RID: 43275 RVA: 0x000F4864 File Offset: 0x000F2A64
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007B3A RID: 31546
			// (set) Token: 0x0600A90C RID: 43276 RVA: 0x000F487C File Offset: 0x000F2A7C
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007B3B RID: 31547
			// (set) Token: 0x0600A90D RID: 43277 RVA: 0x000F488F File Offset: 0x000F2A8F
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007B3C RID: 31548
			// (set) Token: 0x0600A90E RID: 43278 RVA: 0x000F48AD File Offset: 0x000F2AAD
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007B3D RID: 31549
			// (set) Token: 0x0600A90F RID: 43279 RVA: 0x000F48CB File Offset: 0x000F2ACB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007B3E RID: 31550
			// (set) Token: 0x0600A910 RID: 43280 RVA: 0x000F48DE File Offset: 0x000F2ADE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007B3F RID: 31551
			// (set) Token: 0x0600A911 RID: 43281 RVA: 0x000F48F6 File Offset: 0x000F2AF6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007B40 RID: 31552
			// (set) Token: 0x0600A912 RID: 43282 RVA: 0x000F490E File Offset: 0x000F2B0E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007B41 RID: 31553
			// (set) Token: 0x0600A913 RID: 43283 RVA: 0x000F4926 File Offset: 0x000F2B26
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007B42 RID: 31554
			// (set) Token: 0x0600A914 RID: 43284 RVA: 0x000F493E File Offset: 0x000F2B3E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CBC RID: 3260
		public class RemoteArchiveParameters : ParametersBase
		{
			// Token: 0x17007B43 RID: 31555
			// (set) Token: 0x0600A916 RID: 43286 RVA: 0x000F495E File Offset: 0x000F2B5E
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17007B44 RID: 31556
			// (set) Token: 0x0600A917 RID: 43287 RVA: 0x000F497C File Offset: 0x000F2B7C
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17007B45 RID: 31557
			// (set) Token: 0x0600A918 RID: 43288 RVA: 0x000F498F File Offset: 0x000F2B8F
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17007B46 RID: 31558
			// (set) Token: 0x0600A919 RID: 43289 RVA: 0x000F49A2 File Offset: 0x000F2BA2
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007B47 RID: 31559
			// (set) Token: 0x0600A91A RID: 43290 RVA: 0x000F49C0 File Offset: 0x000F2BC0
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007B48 RID: 31560
			// (set) Token: 0x0600A91B RID: 43291 RVA: 0x000F49D3 File Offset: 0x000F2BD3
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17007B49 RID: 31561
			// (set) Token: 0x0600A91C RID: 43292 RVA: 0x000F49EB File Offset: 0x000F2BEB
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17007B4A RID: 31562
			// (set) Token: 0x0600A91D RID: 43293 RVA: 0x000F4A03 File Offset: 0x000F2C03
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007B4B RID: 31563
			// (set) Token: 0x0600A91E RID: 43294 RVA: 0x000F4A21 File Offset: 0x000F2C21
			public virtual SwitchParameter RemoteArchive
			{
				set
				{
					base.PowerSharpParameters["RemoteArchive"] = value;
				}
			}

			// Token: 0x17007B4C RID: 31564
			// (set) Token: 0x0600A91F RID: 43295 RVA: 0x000F4A39 File Offset: 0x000F2C39
			public virtual SmtpDomain ArchiveDomain
			{
				set
				{
					base.PowerSharpParameters["ArchiveDomain"] = value;
				}
			}

			// Token: 0x17007B4D RID: 31565
			// (set) Token: 0x0600A920 RID: 43296 RVA: 0x000F4A4C File Offset: 0x000F2C4C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007B4E RID: 31566
			// (set) Token: 0x0600A921 RID: 43297 RVA: 0x000F4A5F File Offset: 0x000F2C5F
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007B4F RID: 31567
			// (set) Token: 0x0600A922 RID: 43298 RVA: 0x000F4A77 File Offset: 0x000F2C77
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007B50 RID: 31568
			// (set) Token: 0x0600A923 RID: 43299 RVA: 0x000F4A8A File Offset: 0x000F2C8A
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007B51 RID: 31569
			// (set) Token: 0x0600A924 RID: 43300 RVA: 0x000F4A9D File Offset: 0x000F2C9D
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007B52 RID: 31570
			// (set) Token: 0x0600A925 RID: 43301 RVA: 0x000F4ABB File Offset: 0x000F2CBB
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007B53 RID: 31571
			// (set) Token: 0x0600A926 RID: 43302 RVA: 0x000F4AD9 File Offset: 0x000F2CD9
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007B54 RID: 31572
			// (set) Token: 0x0600A927 RID: 43303 RVA: 0x000F4AF7 File Offset: 0x000F2CF7
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007B55 RID: 31573
			// (set) Token: 0x0600A928 RID: 43304 RVA: 0x000F4B15 File Offset: 0x000F2D15
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007B56 RID: 31574
			// (set) Token: 0x0600A929 RID: 43305 RVA: 0x000F4B28 File Offset: 0x000F2D28
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007B57 RID: 31575
			// (set) Token: 0x0600A92A RID: 43306 RVA: 0x000F4B40 File Offset: 0x000F2D40
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007B58 RID: 31576
			// (set) Token: 0x0600A92B RID: 43307 RVA: 0x000F4B5E File Offset: 0x000F2D5E
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007B59 RID: 31577
			// (set) Token: 0x0600A92C RID: 43308 RVA: 0x000F4B76 File Offset: 0x000F2D76
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007B5A RID: 31578
			// (set) Token: 0x0600A92D RID: 43309 RVA: 0x000F4B8E File Offset: 0x000F2D8E
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007B5B RID: 31579
			// (set) Token: 0x0600A92E RID: 43310 RVA: 0x000F4BA1 File Offset: 0x000F2DA1
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007B5C RID: 31580
			// (set) Token: 0x0600A92F RID: 43311 RVA: 0x000F4BB9 File Offset: 0x000F2DB9
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007B5D RID: 31581
			// (set) Token: 0x0600A930 RID: 43312 RVA: 0x000F4BCC File Offset: 0x000F2DCC
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007B5E RID: 31582
			// (set) Token: 0x0600A931 RID: 43313 RVA: 0x000F4BDF File Offset: 0x000F2DDF
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007B5F RID: 31583
			// (set) Token: 0x0600A932 RID: 43314 RVA: 0x000F4BF2 File Offset: 0x000F2DF2
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007B60 RID: 31584
			// (set) Token: 0x0600A933 RID: 43315 RVA: 0x000F4C05 File Offset: 0x000F2E05
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007B61 RID: 31585
			// (set) Token: 0x0600A934 RID: 43316 RVA: 0x000F4C1D File Offset: 0x000F2E1D
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007B62 RID: 31586
			// (set) Token: 0x0600A935 RID: 43317 RVA: 0x000F4C30 File Offset: 0x000F2E30
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007B63 RID: 31587
			// (set) Token: 0x0600A936 RID: 43318 RVA: 0x000F4C43 File Offset: 0x000F2E43
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007B64 RID: 31588
			// (set) Token: 0x0600A937 RID: 43319 RVA: 0x000F4C5B File Offset: 0x000F2E5B
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007B65 RID: 31589
			// (set) Token: 0x0600A938 RID: 43320 RVA: 0x000F4C6E File Offset: 0x000F2E6E
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007B66 RID: 31590
			// (set) Token: 0x0600A939 RID: 43321 RVA: 0x000F4C86 File Offset: 0x000F2E86
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007B67 RID: 31591
			// (set) Token: 0x0600A93A RID: 43322 RVA: 0x000F4C99 File Offset: 0x000F2E99
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007B68 RID: 31592
			// (set) Token: 0x0600A93B RID: 43323 RVA: 0x000F4CB7 File Offset: 0x000F2EB7
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007B69 RID: 31593
			// (set) Token: 0x0600A93C RID: 43324 RVA: 0x000F4CD5 File Offset: 0x000F2ED5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007B6A RID: 31594
			// (set) Token: 0x0600A93D RID: 43325 RVA: 0x000F4CE8 File Offset: 0x000F2EE8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007B6B RID: 31595
			// (set) Token: 0x0600A93E RID: 43326 RVA: 0x000F4D00 File Offset: 0x000F2F00
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007B6C RID: 31596
			// (set) Token: 0x0600A93F RID: 43327 RVA: 0x000F4D18 File Offset: 0x000F2F18
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007B6D RID: 31597
			// (set) Token: 0x0600A940 RID: 43328 RVA: 0x000F4D30 File Offset: 0x000F2F30
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007B6E RID: 31598
			// (set) Token: 0x0600A941 RID: 43329 RVA: 0x000F4D48 File Offset: 0x000F2F48
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CBD RID: 3261
		public class MicrosoftOnlineServicesIDParameters : ParametersBase
		{
			// Token: 0x17007B6F RID: 31599
			// (set) Token: 0x0600A943 RID: 43331 RVA: 0x000F4D68 File Offset: 0x000F2F68
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17007B70 RID: 31600
			// (set) Token: 0x0600A944 RID: 43332 RVA: 0x000F4D86 File Offset: 0x000F2F86
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17007B71 RID: 31601
			// (set) Token: 0x0600A945 RID: 43333 RVA: 0x000F4D99 File Offset: 0x000F2F99
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007B72 RID: 31602
			// (set) Token: 0x0600A946 RID: 43334 RVA: 0x000F4DB7 File Offset: 0x000F2FB7
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007B73 RID: 31603
			// (set) Token: 0x0600A947 RID: 43335 RVA: 0x000F4DCA File Offset: 0x000F2FCA
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17007B74 RID: 31604
			// (set) Token: 0x0600A948 RID: 43336 RVA: 0x000F4DE2 File Offset: 0x000F2FE2
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17007B75 RID: 31605
			// (set) Token: 0x0600A949 RID: 43337 RVA: 0x000F4DFA File Offset: 0x000F2FFA
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007B76 RID: 31606
			// (set) Token: 0x0600A94A RID: 43338 RVA: 0x000F4E18 File Offset: 0x000F3018
			public virtual WindowsLiveId MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x17007B77 RID: 31607
			// (set) Token: 0x0600A94B RID: 43339 RVA: 0x000F4E2B File Offset: 0x000F302B
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007B78 RID: 31608
			// (set) Token: 0x0600A94C RID: 43340 RVA: 0x000F4E3E File Offset: 0x000F303E
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007B79 RID: 31609
			// (set) Token: 0x0600A94D RID: 43341 RVA: 0x000F4E56 File Offset: 0x000F3056
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007B7A RID: 31610
			// (set) Token: 0x0600A94E RID: 43342 RVA: 0x000F4E69 File Offset: 0x000F3069
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007B7B RID: 31611
			// (set) Token: 0x0600A94F RID: 43343 RVA: 0x000F4E7C File Offset: 0x000F307C
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007B7C RID: 31612
			// (set) Token: 0x0600A950 RID: 43344 RVA: 0x000F4E9A File Offset: 0x000F309A
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007B7D RID: 31613
			// (set) Token: 0x0600A951 RID: 43345 RVA: 0x000F4EB8 File Offset: 0x000F30B8
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007B7E RID: 31614
			// (set) Token: 0x0600A952 RID: 43346 RVA: 0x000F4ED6 File Offset: 0x000F30D6
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007B7F RID: 31615
			// (set) Token: 0x0600A953 RID: 43347 RVA: 0x000F4EF4 File Offset: 0x000F30F4
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007B80 RID: 31616
			// (set) Token: 0x0600A954 RID: 43348 RVA: 0x000F4F07 File Offset: 0x000F3107
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007B81 RID: 31617
			// (set) Token: 0x0600A955 RID: 43349 RVA: 0x000F4F1F File Offset: 0x000F311F
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007B82 RID: 31618
			// (set) Token: 0x0600A956 RID: 43350 RVA: 0x000F4F3D File Offset: 0x000F313D
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007B83 RID: 31619
			// (set) Token: 0x0600A957 RID: 43351 RVA: 0x000F4F55 File Offset: 0x000F3155
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007B84 RID: 31620
			// (set) Token: 0x0600A958 RID: 43352 RVA: 0x000F4F6D File Offset: 0x000F316D
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007B85 RID: 31621
			// (set) Token: 0x0600A959 RID: 43353 RVA: 0x000F4F80 File Offset: 0x000F3180
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007B86 RID: 31622
			// (set) Token: 0x0600A95A RID: 43354 RVA: 0x000F4F98 File Offset: 0x000F3198
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007B87 RID: 31623
			// (set) Token: 0x0600A95B RID: 43355 RVA: 0x000F4FAB File Offset: 0x000F31AB
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007B88 RID: 31624
			// (set) Token: 0x0600A95C RID: 43356 RVA: 0x000F4FBE File Offset: 0x000F31BE
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007B89 RID: 31625
			// (set) Token: 0x0600A95D RID: 43357 RVA: 0x000F4FD1 File Offset: 0x000F31D1
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007B8A RID: 31626
			// (set) Token: 0x0600A95E RID: 43358 RVA: 0x000F4FE4 File Offset: 0x000F31E4
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007B8B RID: 31627
			// (set) Token: 0x0600A95F RID: 43359 RVA: 0x000F4FFC File Offset: 0x000F31FC
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007B8C RID: 31628
			// (set) Token: 0x0600A960 RID: 43360 RVA: 0x000F500F File Offset: 0x000F320F
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007B8D RID: 31629
			// (set) Token: 0x0600A961 RID: 43361 RVA: 0x000F5022 File Offset: 0x000F3222
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007B8E RID: 31630
			// (set) Token: 0x0600A962 RID: 43362 RVA: 0x000F503A File Offset: 0x000F323A
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007B8F RID: 31631
			// (set) Token: 0x0600A963 RID: 43363 RVA: 0x000F504D File Offset: 0x000F324D
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007B90 RID: 31632
			// (set) Token: 0x0600A964 RID: 43364 RVA: 0x000F5065 File Offset: 0x000F3265
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007B91 RID: 31633
			// (set) Token: 0x0600A965 RID: 43365 RVA: 0x000F5078 File Offset: 0x000F3278
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007B92 RID: 31634
			// (set) Token: 0x0600A966 RID: 43366 RVA: 0x000F5096 File Offset: 0x000F3296
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007B93 RID: 31635
			// (set) Token: 0x0600A967 RID: 43367 RVA: 0x000F50B4 File Offset: 0x000F32B4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007B94 RID: 31636
			// (set) Token: 0x0600A968 RID: 43368 RVA: 0x000F50C7 File Offset: 0x000F32C7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007B95 RID: 31637
			// (set) Token: 0x0600A969 RID: 43369 RVA: 0x000F50DF File Offset: 0x000F32DF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007B96 RID: 31638
			// (set) Token: 0x0600A96A RID: 43370 RVA: 0x000F50F7 File Offset: 0x000F32F7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007B97 RID: 31639
			// (set) Token: 0x0600A96B RID: 43371 RVA: 0x000F510F File Offset: 0x000F330F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007B98 RID: 31640
			// (set) Token: 0x0600A96C RID: 43372 RVA: 0x000F5127 File Offset: 0x000F3327
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CBE RID: 3262
		public class DisabledUserParameters : ParametersBase
		{
			// Token: 0x17007B99 RID: 31641
			// (set) Token: 0x0600A96E RID: 43374 RVA: 0x000F5147 File Offset: 0x000F3347
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17007B9A RID: 31642
			// (set) Token: 0x0600A96F RID: 43375 RVA: 0x000F5165 File Offset: 0x000F3365
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17007B9B RID: 31643
			// (set) Token: 0x0600A970 RID: 43376 RVA: 0x000F5178 File Offset: 0x000F3378
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17007B9C RID: 31644
			// (set) Token: 0x0600A971 RID: 43377 RVA: 0x000F518B File Offset: 0x000F338B
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17007B9D RID: 31645
			// (set) Token: 0x0600A972 RID: 43378 RVA: 0x000F51A3 File Offset: 0x000F33A3
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007B9E RID: 31646
			// (set) Token: 0x0600A973 RID: 43379 RVA: 0x000F51C1 File Offset: 0x000F33C1
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007B9F RID: 31647
			// (set) Token: 0x0600A974 RID: 43380 RVA: 0x000F51D4 File Offset: 0x000F33D4
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17007BA0 RID: 31648
			// (set) Token: 0x0600A975 RID: 43381 RVA: 0x000F51EC File Offset: 0x000F33EC
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17007BA1 RID: 31649
			// (set) Token: 0x0600A976 RID: 43382 RVA: 0x000F5204 File Offset: 0x000F3404
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007BA2 RID: 31650
			// (set) Token: 0x0600A977 RID: 43383 RVA: 0x000F5217 File Offset: 0x000F3417
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007BA3 RID: 31651
			// (set) Token: 0x0600A978 RID: 43384 RVA: 0x000F522F File Offset: 0x000F342F
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007BA4 RID: 31652
			// (set) Token: 0x0600A979 RID: 43385 RVA: 0x000F5242 File Offset: 0x000F3442
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007BA5 RID: 31653
			// (set) Token: 0x0600A97A RID: 43386 RVA: 0x000F5255 File Offset: 0x000F3455
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007BA6 RID: 31654
			// (set) Token: 0x0600A97B RID: 43387 RVA: 0x000F5273 File Offset: 0x000F3473
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007BA7 RID: 31655
			// (set) Token: 0x0600A97C RID: 43388 RVA: 0x000F5291 File Offset: 0x000F3491
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007BA8 RID: 31656
			// (set) Token: 0x0600A97D RID: 43389 RVA: 0x000F52AF File Offset: 0x000F34AF
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007BA9 RID: 31657
			// (set) Token: 0x0600A97E RID: 43390 RVA: 0x000F52CD File Offset: 0x000F34CD
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007BAA RID: 31658
			// (set) Token: 0x0600A97F RID: 43391 RVA: 0x000F52E0 File Offset: 0x000F34E0
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007BAB RID: 31659
			// (set) Token: 0x0600A980 RID: 43392 RVA: 0x000F52F8 File Offset: 0x000F34F8
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007BAC RID: 31660
			// (set) Token: 0x0600A981 RID: 43393 RVA: 0x000F5316 File Offset: 0x000F3516
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007BAD RID: 31661
			// (set) Token: 0x0600A982 RID: 43394 RVA: 0x000F532E File Offset: 0x000F352E
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007BAE RID: 31662
			// (set) Token: 0x0600A983 RID: 43395 RVA: 0x000F5346 File Offset: 0x000F3546
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007BAF RID: 31663
			// (set) Token: 0x0600A984 RID: 43396 RVA: 0x000F5359 File Offset: 0x000F3559
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007BB0 RID: 31664
			// (set) Token: 0x0600A985 RID: 43397 RVA: 0x000F5371 File Offset: 0x000F3571
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007BB1 RID: 31665
			// (set) Token: 0x0600A986 RID: 43398 RVA: 0x000F5384 File Offset: 0x000F3584
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007BB2 RID: 31666
			// (set) Token: 0x0600A987 RID: 43399 RVA: 0x000F5397 File Offset: 0x000F3597
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007BB3 RID: 31667
			// (set) Token: 0x0600A988 RID: 43400 RVA: 0x000F53AA File Offset: 0x000F35AA
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007BB4 RID: 31668
			// (set) Token: 0x0600A989 RID: 43401 RVA: 0x000F53BD File Offset: 0x000F35BD
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007BB5 RID: 31669
			// (set) Token: 0x0600A98A RID: 43402 RVA: 0x000F53D5 File Offset: 0x000F35D5
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007BB6 RID: 31670
			// (set) Token: 0x0600A98B RID: 43403 RVA: 0x000F53E8 File Offset: 0x000F35E8
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007BB7 RID: 31671
			// (set) Token: 0x0600A98C RID: 43404 RVA: 0x000F53FB File Offset: 0x000F35FB
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007BB8 RID: 31672
			// (set) Token: 0x0600A98D RID: 43405 RVA: 0x000F5413 File Offset: 0x000F3613
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007BB9 RID: 31673
			// (set) Token: 0x0600A98E RID: 43406 RVA: 0x000F5426 File Offset: 0x000F3626
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007BBA RID: 31674
			// (set) Token: 0x0600A98F RID: 43407 RVA: 0x000F543E File Offset: 0x000F363E
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007BBB RID: 31675
			// (set) Token: 0x0600A990 RID: 43408 RVA: 0x000F5451 File Offset: 0x000F3651
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007BBC RID: 31676
			// (set) Token: 0x0600A991 RID: 43409 RVA: 0x000F546F File Offset: 0x000F366F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007BBD RID: 31677
			// (set) Token: 0x0600A992 RID: 43410 RVA: 0x000F548D File Offset: 0x000F368D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007BBE RID: 31678
			// (set) Token: 0x0600A993 RID: 43411 RVA: 0x000F54A0 File Offset: 0x000F36A0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007BBF RID: 31679
			// (set) Token: 0x0600A994 RID: 43412 RVA: 0x000F54B8 File Offset: 0x000F36B8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007BC0 RID: 31680
			// (set) Token: 0x0600A995 RID: 43413 RVA: 0x000F54D0 File Offset: 0x000F36D0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007BC1 RID: 31681
			// (set) Token: 0x0600A996 RID: 43414 RVA: 0x000F54E8 File Offset: 0x000F36E8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007BC2 RID: 31682
			// (set) Token: 0x0600A997 RID: 43415 RVA: 0x000F5500 File Offset: 0x000F3700
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CBF RID: 3263
		public class UserParameters : ParametersBase
		{
			// Token: 0x17007BC3 RID: 31683
			// (set) Token: 0x0600A999 RID: 43417 RVA: 0x000F5520 File Offset: 0x000F3720
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17007BC4 RID: 31684
			// (set) Token: 0x0600A99A RID: 43418 RVA: 0x000F553E File Offset: 0x000F373E
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17007BC5 RID: 31685
			// (set) Token: 0x0600A99B RID: 43419 RVA: 0x000F5551 File Offset: 0x000F3751
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17007BC6 RID: 31686
			// (set) Token: 0x0600A99C RID: 43420 RVA: 0x000F5564 File Offset: 0x000F3764
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007BC7 RID: 31687
			// (set) Token: 0x0600A99D RID: 43421 RVA: 0x000F5582 File Offset: 0x000F3782
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007BC8 RID: 31688
			// (set) Token: 0x0600A99E RID: 43422 RVA: 0x000F5595 File Offset: 0x000F3795
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17007BC9 RID: 31689
			// (set) Token: 0x0600A99F RID: 43423 RVA: 0x000F55AD File Offset: 0x000F37AD
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17007BCA RID: 31690
			// (set) Token: 0x0600A9A0 RID: 43424 RVA: 0x000F55C5 File Offset: 0x000F37C5
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007BCB RID: 31691
			// (set) Token: 0x0600A9A1 RID: 43425 RVA: 0x000F55E3 File Offset: 0x000F37E3
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007BCC RID: 31692
			// (set) Token: 0x0600A9A2 RID: 43426 RVA: 0x000F55F6 File Offset: 0x000F37F6
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007BCD RID: 31693
			// (set) Token: 0x0600A9A3 RID: 43427 RVA: 0x000F560E File Offset: 0x000F380E
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007BCE RID: 31694
			// (set) Token: 0x0600A9A4 RID: 43428 RVA: 0x000F5621 File Offset: 0x000F3821
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007BCF RID: 31695
			// (set) Token: 0x0600A9A5 RID: 43429 RVA: 0x000F5634 File Offset: 0x000F3834
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007BD0 RID: 31696
			// (set) Token: 0x0600A9A6 RID: 43430 RVA: 0x000F5652 File Offset: 0x000F3852
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007BD1 RID: 31697
			// (set) Token: 0x0600A9A7 RID: 43431 RVA: 0x000F5670 File Offset: 0x000F3870
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007BD2 RID: 31698
			// (set) Token: 0x0600A9A8 RID: 43432 RVA: 0x000F568E File Offset: 0x000F388E
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007BD3 RID: 31699
			// (set) Token: 0x0600A9A9 RID: 43433 RVA: 0x000F56AC File Offset: 0x000F38AC
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007BD4 RID: 31700
			// (set) Token: 0x0600A9AA RID: 43434 RVA: 0x000F56BF File Offset: 0x000F38BF
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007BD5 RID: 31701
			// (set) Token: 0x0600A9AB RID: 43435 RVA: 0x000F56D7 File Offset: 0x000F38D7
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007BD6 RID: 31702
			// (set) Token: 0x0600A9AC RID: 43436 RVA: 0x000F56F5 File Offset: 0x000F38F5
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007BD7 RID: 31703
			// (set) Token: 0x0600A9AD RID: 43437 RVA: 0x000F570D File Offset: 0x000F390D
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007BD8 RID: 31704
			// (set) Token: 0x0600A9AE RID: 43438 RVA: 0x000F5725 File Offset: 0x000F3925
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007BD9 RID: 31705
			// (set) Token: 0x0600A9AF RID: 43439 RVA: 0x000F5738 File Offset: 0x000F3938
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007BDA RID: 31706
			// (set) Token: 0x0600A9B0 RID: 43440 RVA: 0x000F5750 File Offset: 0x000F3950
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007BDB RID: 31707
			// (set) Token: 0x0600A9B1 RID: 43441 RVA: 0x000F5763 File Offset: 0x000F3963
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007BDC RID: 31708
			// (set) Token: 0x0600A9B2 RID: 43442 RVA: 0x000F5776 File Offset: 0x000F3976
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007BDD RID: 31709
			// (set) Token: 0x0600A9B3 RID: 43443 RVA: 0x000F5789 File Offset: 0x000F3989
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007BDE RID: 31710
			// (set) Token: 0x0600A9B4 RID: 43444 RVA: 0x000F579C File Offset: 0x000F399C
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007BDF RID: 31711
			// (set) Token: 0x0600A9B5 RID: 43445 RVA: 0x000F57B4 File Offset: 0x000F39B4
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007BE0 RID: 31712
			// (set) Token: 0x0600A9B6 RID: 43446 RVA: 0x000F57C7 File Offset: 0x000F39C7
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007BE1 RID: 31713
			// (set) Token: 0x0600A9B7 RID: 43447 RVA: 0x000F57DA File Offset: 0x000F39DA
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007BE2 RID: 31714
			// (set) Token: 0x0600A9B8 RID: 43448 RVA: 0x000F57F2 File Offset: 0x000F39F2
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007BE3 RID: 31715
			// (set) Token: 0x0600A9B9 RID: 43449 RVA: 0x000F5805 File Offset: 0x000F3A05
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007BE4 RID: 31716
			// (set) Token: 0x0600A9BA RID: 43450 RVA: 0x000F581D File Offset: 0x000F3A1D
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007BE5 RID: 31717
			// (set) Token: 0x0600A9BB RID: 43451 RVA: 0x000F5830 File Offset: 0x000F3A30
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007BE6 RID: 31718
			// (set) Token: 0x0600A9BC RID: 43452 RVA: 0x000F584E File Offset: 0x000F3A4E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007BE7 RID: 31719
			// (set) Token: 0x0600A9BD RID: 43453 RVA: 0x000F586C File Offset: 0x000F3A6C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007BE8 RID: 31720
			// (set) Token: 0x0600A9BE RID: 43454 RVA: 0x000F587F File Offset: 0x000F3A7F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007BE9 RID: 31721
			// (set) Token: 0x0600A9BF RID: 43455 RVA: 0x000F5897 File Offset: 0x000F3A97
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007BEA RID: 31722
			// (set) Token: 0x0600A9C0 RID: 43456 RVA: 0x000F58AF File Offset: 0x000F3AAF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007BEB RID: 31723
			// (set) Token: 0x0600A9C1 RID: 43457 RVA: 0x000F58C7 File Offset: 0x000F3AC7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007BEC RID: 31724
			// (set) Token: 0x0600A9C2 RID: 43458 RVA: 0x000F58DF File Offset: 0x000F3ADF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CC0 RID: 3264
		public class WindowsLiveIDParameters : ParametersBase
		{
			// Token: 0x17007BED RID: 31725
			// (set) Token: 0x0600A9C4 RID: 43460 RVA: 0x000F58FF File Offset: 0x000F3AFF
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17007BEE RID: 31726
			// (set) Token: 0x0600A9C5 RID: 43461 RVA: 0x000F591D File Offset: 0x000F3B1D
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17007BEF RID: 31727
			// (set) Token: 0x0600A9C6 RID: 43462 RVA: 0x000F5930 File Offset: 0x000F3B30
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007BF0 RID: 31728
			// (set) Token: 0x0600A9C7 RID: 43463 RVA: 0x000F594E File Offset: 0x000F3B4E
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007BF1 RID: 31729
			// (set) Token: 0x0600A9C8 RID: 43464 RVA: 0x000F5961 File Offset: 0x000F3B61
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17007BF2 RID: 31730
			// (set) Token: 0x0600A9C9 RID: 43465 RVA: 0x000F5979 File Offset: 0x000F3B79
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17007BF3 RID: 31731
			// (set) Token: 0x0600A9CA RID: 43466 RVA: 0x000F5991 File Offset: 0x000F3B91
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007BF4 RID: 31732
			// (set) Token: 0x0600A9CB RID: 43467 RVA: 0x000F59AF File Offset: 0x000F3BAF
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x17007BF5 RID: 31733
			// (set) Token: 0x0600A9CC RID: 43468 RVA: 0x000F59C2 File Offset: 0x000F3BC2
			public virtual SwitchParameter EvictLiveId
			{
				set
				{
					base.PowerSharpParameters["EvictLiveId"] = value;
				}
			}

			// Token: 0x17007BF6 RID: 31734
			// (set) Token: 0x0600A9CD RID: 43469 RVA: 0x000F59DA File Offset: 0x000F3BDA
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007BF7 RID: 31735
			// (set) Token: 0x0600A9CE RID: 43470 RVA: 0x000F59ED File Offset: 0x000F3BED
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007BF8 RID: 31736
			// (set) Token: 0x0600A9CF RID: 43471 RVA: 0x000F5A05 File Offset: 0x000F3C05
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007BF9 RID: 31737
			// (set) Token: 0x0600A9D0 RID: 43472 RVA: 0x000F5A18 File Offset: 0x000F3C18
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007BFA RID: 31738
			// (set) Token: 0x0600A9D1 RID: 43473 RVA: 0x000F5A2B File Offset: 0x000F3C2B
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007BFB RID: 31739
			// (set) Token: 0x0600A9D2 RID: 43474 RVA: 0x000F5A49 File Offset: 0x000F3C49
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007BFC RID: 31740
			// (set) Token: 0x0600A9D3 RID: 43475 RVA: 0x000F5A67 File Offset: 0x000F3C67
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007BFD RID: 31741
			// (set) Token: 0x0600A9D4 RID: 43476 RVA: 0x000F5A85 File Offset: 0x000F3C85
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007BFE RID: 31742
			// (set) Token: 0x0600A9D5 RID: 43477 RVA: 0x000F5AA3 File Offset: 0x000F3CA3
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007BFF RID: 31743
			// (set) Token: 0x0600A9D6 RID: 43478 RVA: 0x000F5AB6 File Offset: 0x000F3CB6
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007C00 RID: 31744
			// (set) Token: 0x0600A9D7 RID: 43479 RVA: 0x000F5ACE File Offset: 0x000F3CCE
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007C01 RID: 31745
			// (set) Token: 0x0600A9D8 RID: 43480 RVA: 0x000F5AEC File Offset: 0x000F3CEC
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007C02 RID: 31746
			// (set) Token: 0x0600A9D9 RID: 43481 RVA: 0x000F5B04 File Offset: 0x000F3D04
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007C03 RID: 31747
			// (set) Token: 0x0600A9DA RID: 43482 RVA: 0x000F5B1C File Offset: 0x000F3D1C
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007C04 RID: 31748
			// (set) Token: 0x0600A9DB RID: 43483 RVA: 0x000F5B2F File Offset: 0x000F3D2F
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007C05 RID: 31749
			// (set) Token: 0x0600A9DC RID: 43484 RVA: 0x000F5B47 File Offset: 0x000F3D47
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007C06 RID: 31750
			// (set) Token: 0x0600A9DD RID: 43485 RVA: 0x000F5B5A File Offset: 0x000F3D5A
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007C07 RID: 31751
			// (set) Token: 0x0600A9DE RID: 43486 RVA: 0x000F5B6D File Offset: 0x000F3D6D
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007C08 RID: 31752
			// (set) Token: 0x0600A9DF RID: 43487 RVA: 0x000F5B80 File Offset: 0x000F3D80
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007C09 RID: 31753
			// (set) Token: 0x0600A9E0 RID: 43488 RVA: 0x000F5B93 File Offset: 0x000F3D93
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007C0A RID: 31754
			// (set) Token: 0x0600A9E1 RID: 43489 RVA: 0x000F5BAB File Offset: 0x000F3DAB
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007C0B RID: 31755
			// (set) Token: 0x0600A9E2 RID: 43490 RVA: 0x000F5BBE File Offset: 0x000F3DBE
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007C0C RID: 31756
			// (set) Token: 0x0600A9E3 RID: 43491 RVA: 0x000F5BD1 File Offset: 0x000F3DD1
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007C0D RID: 31757
			// (set) Token: 0x0600A9E4 RID: 43492 RVA: 0x000F5BE9 File Offset: 0x000F3DE9
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007C0E RID: 31758
			// (set) Token: 0x0600A9E5 RID: 43493 RVA: 0x000F5BFC File Offset: 0x000F3DFC
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007C0F RID: 31759
			// (set) Token: 0x0600A9E6 RID: 43494 RVA: 0x000F5C14 File Offset: 0x000F3E14
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007C10 RID: 31760
			// (set) Token: 0x0600A9E7 RID: 43495 RVA: 0x000F5C27 File Offset: 0x000F3E27
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007C11 RID: 31761
			// (set) Token: 0x0600A9E8 RID: 43496 RVA: 0x000F5C45 File Offset: 0x000F3E45
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007C12 RID: 31762
			// (set) Token: 0x0600A9E9 RID: 43497 RVA: 0x000F5C63 File Offset: 0x000F3E63
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007C13 RID: 31763
			// (set) Token: 0x0600A9EA RID: 43498 RVA: 0x000F5C76 File Offset: 0x000F3E76
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007C14 RID: 31764
			// (set) Token: 0x0600A9EB RID: 43499 RVA: 0x000F5C8E File Offset: 0x000F3E8E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007C15 RID: 31765
			// (set) Token: 0x0600A9EC RID: 43500 RVA: 0x000F5CA6 File Offset: 0x000F3EA6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007C16 RID: 31766
			// (set) Token: 0x0600A9ED RID: 43501 RVA: 0x000F5CBE File Offset: 0x000F3EBE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007C17 RID: 31767
			// (set) Token: 0x0600A9EE RID: 43502 RVA: 0x000F5CD6 File Offset: 0x000F3ED6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CC1 RID: 3265
		public class DiscoveryParameters : ParametersBase
		{
			// Token: 0x17007C18 RID: 31768
			// (set) Token: 0x0600A9F0 RID: 43504 RVA: 0x000F5CF6 File Offset: 0x000F3EF6
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17007C19 RID: 31769
			// (set) Token: 0x0600A9F1 RID: 43505 RVA: 0x000F5D09 File Offset: 0x000F3F09
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17007C1A RID: 31770
			// (set) Token: 0x0600A9F2 RID: 43506 RVA: 0x000F5D1C File Offset: 0x000F3F1C
			public virtual SwitchParameter Discovery
			{
				set
				{
					base.PowerSharpParameters["Discovery"] = value;
				}
			}

			// Token: 0x17007C1B RID: 31771
			// (set) Token: 0x0600A9F3 RID: 43507 RVA: 0x000F5D34 File Offset: 0x000F3F34
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007C1C RID: 31772
			// (set) Token: 0x0600A9F4 RID: 43508 RVA: 0x000F5D47 File Offset: 0x000F3F47
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007C1D RID: 31773
			// (set) Token: 0x0600A9F5 RID: 43509 RVA: 0x000F5D5F File Offset: 0x000F3F5F
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007C1E RID: 31774
			// (set) Token: 0x0600A9F6 RID: 43510 RVA: 0x000F5D72 File Offset: 0x000F3F72
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007C1F RID: 31775
			// (set) Token: 0x0600A9F7 RID: 43511 RVA: 0x000F5D85 File Offset: 0x000F3F85
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007C20 RID: 31776
			// (set) Token: 0x0600A9F8 RID: 43512 RVA: 0x000F5DA3 File Offset: 0x000F3FA3
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007C21 RID: 31777
			// (set) Token: 0x0600A9F9 RID: 43513 RVA: 0x000F5DC1 File Offset: 0x000F3FC1
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007C22 RID: 31778
			// (set) Token: 0x0600A9FA RID: 43514 RVA: 0x000F5DDF File Offset: 0x000F3FDF
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007C23 RID: 31779
			// (set) Token: 0x0600A9FB RID: 43515 RVA: 0x000F5DFD File Offset: 0x000F3FFD
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007C24 RID: 31780
			// (set) Token: 0x0600A9FC RID: 43516 RVA: 0x000F5E10 File Offset: 0x000F4010
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007C25 RID: 31781
			// (set) Token: 0x0600A9FD RID: 43517 RVA: 0x000F5E28 File Offset: 0x000F4028
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007C26 RID: 31782
			// (set) Token: 0x0600A9FE RID: 43518 RVA: 0x000F5E46 File Offset: 0x000F4046
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007C27 RID: 31783
			// (set) Token: 0x0600A9FF RID: 43519 RVA: 0x000F5E5E File Offset: 0x000F405E
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007C28 RID: 31784
			// (set) Token: 0x0600AA00 RID: 43520 RVA: 0x000F5E76 File Offset: 0x000F4076
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007C29 RID: 31785
			// (set) Token: 0x0600AA01 RID: 43521 RVA: 0x000F5E89 File Offset: 0x000F4089
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007C2A RID: 31786
			// (set) Token: 0x0600AA02 RID: 43522 RVA: 0x000F5EA1 File Offset: 0x000F40A1
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007C2B RID: 31787
			// (set) Token: 0x0600AA03 RID: 43523 RVA: 0x000F5EB4 File Offset: 0x000F40B4
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007C2C RID: 31788
			// (set) Token: 0x0600AA04 RID: 43524 RVA: 0x000F5EC7 File Offset: 0x000F40C7
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007C2D RID: 31789
			// (set) Token: 0x0600AA05 RID: 43525 RVA: 0x000F5EDA File Offset: 0x000F40DA
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007C2E RID: 31790
			// (set) Token: 0x0600AA06 RID: 43526 RVA: 0x000F5EED File Offset: 0x000F40ED
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007C2F RID: 31791
			// (set) Token: 0x0600AA07 RID: 43527 RVA: 0x000F5F05 File Offset: 0x000F4105
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007C30 RID: 31792
			// (set) Token: 0x0600AA08 RID: 43528 RVA: 0x000F5F18 File Offset: 0x000F4118
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007C31 RID: 31793
			// (set) Token: 0x0600AA09 RID: 43529 RVA: 0x000F5F2B File Offset: 0x000F412B
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007C32 RID: 31794
			// (set) Token: 0x0600AA0A RID: 43530 RVA: 0x000F5F43 File Offset: 0x000F4143
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007C33 RID: 31795
			// (set) Token: 0x0600AA0B RID: 43531 RVA: 0x000F5F56 File Offset: 0x000F4156
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007C34 RID: 31796
			// (set) Token: 0x0600AA0C RID: 43532 RVA: 0x000F5F6E File Offset: 0x000F416E
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007C35 RID: 31797
			// (set) Token: 0x0600AA0D RID: 43533 RVA: 0x000F5F81 File Offset: 0x000F4181
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007C36 RID: 31798
			// (set) Token: 0x0600AA0E RID: 43534 RVA: 0x000F5F9F File Offset: 0x000F419F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007C37 RID: 31799
			// (set) Token: 0x0600AA0F RID: 43535 RVA: 0x000F5FBD File Offset: 0x000F41BD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007C38 RID: 31800
			// (set) Token: 0x0600AA10 RID: 43536 RVA: 0x000F5FD0 File Offset: 0x000F41D0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007C39 RID: 31801
			// (set) Token: 0x0600AA11 RID: 43537 RVA: 0x000F5FE8 File Offset: 0x000F41E8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007C3A RID: 31802
			// (set) Token: 0x0600AA12 RID: 43538 RVA: 0x000F6000 File Offset: 0x000F4200
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007C3B RID: 31803
			// (set) Token: 0x0600AA13 RID: 43539 RVA: 0x000F6018 File Offset: 0x000F4218
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007C3C RID: 31804
			// (set) Token: 0x0600AA14 RID: 43540 RVA: 0x000F6030 File Offset: 0x000F4230
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CC2 RID: 3266
		public class TeamMailboxIWParameters : ParametersBase
		{
			// Token: 0x17007C3D RID: 31805
			// (set) Token: 0x0600AA16 RID: 43542 RVA: 0x000F6050 File Offset: 0x000F4250
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17007C3E RID: 31806
			// (set) Token: 0x0600AA17 RID: 43543 RVA: 0x000F6063 File Offset: 0x000F4263
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17007C3F RID: 31807
			// (set) Token: 0x0600AA18 RID: 43544 RVA: 0x000F6076 File Offset: 0x000F4276
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007C40 RID: 31808
			// (set) Token: 0x0600AA19 RID: 43545 RVA: 0x000F6094 File Offset: 0x000F4294
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007C41 RID: 31809
			// (set) Token: 0x0600AA1A RID: 43546 RVA: 0x000F60A7 File Offset: 0x000F42A7
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17007C42 RID: 31810
			// (set) Token: 0x0600AA1B RID: 43547 RVA: 0x000F60BF File Offset: 0x000F42BF
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17007C43 RID: 31811
			// (set) Token: 0x0600AA1C RID: 43548 RVA: 0x000F60D7 File Offset: 0x000F42D7
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007C44 RID: 31812
			// (set) Token: 0x0600AA1D RID: 43549 RVA: 0x000F60EA File Offset: 0x000F42EA
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007C45 RID: 31813
			// (set) Token: 0x0600AA1E RID: 43550 RVA: 0x000F6102 File Offset: 0x000F4302
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007C46 RID: 31814
			// (set) Token: 0x0600AA1F RID: 43551 RVA: 0x000F6115 File Offset: 0x000F4315
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007C47 RID: 31815
			// (set) Token: 0x0600AA20 RID: 43552 RVA: 0x000F6128 File Offset: 0x000F4328
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007C48 RID: 31816
			// (set) Token: 0x0600AA21 RID: 43553 RVA: 0x000F6146 File Offset: 0x000F4346
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007C49 RID: 31817
			// (set) Token: 0x0600AA22 RID: 43554 RVA: 0x000F6164 File Offset: 0x000F4364
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007C4A RID: 31818
			// (set) Token: 0x0600AA23 RID: 43555 RVA: 0x000F6182 File Offset: 0x000F4382
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007C4B RID: 31819
			// (set) Token: 0x0600AA24 RID: 43556 RVA: 0x000F61A0 File Offset: 0x000F43A0
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007C4C RID: 31820
			// (set) Token: 0x0600AA25 RID: 43557 RVA: 0x000F61B3 File Offset: 0x000F43B3
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007C4D RID: 31821
			// (set) Token: 0x0600AA26 RID: 43558 RVA: 0x000F61CB File Offset: 0x000F43CB
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007C4E RID: 31822
			// (set) Token: 0x0600AA27 RID: 43559 RVA: 0x000F61E9 File Offset: 0x000F43E9
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007C4F RID: 31823
			// (set) Token: 0x0600AA28 RID: 43560 RVA: 0x000F6201 File Offset: 0x000F4401
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007C50 RID: 31824
			// (set) Token: 0x0600AA29 RID: 43561 RVA: 0x000F6219 File Offset: 0x000F4419
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007C51 RID: 31825
			// (set) Token: 0x0600AA2A RID: 43562 RVA: 0x000F622C File Offset: 0x000F442C
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007C52 RID: 31826
			// (set) Token: 0x0600AA2B RID: 43563 RVA: 0x000F6244 File Offset: 0x000F4444
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007C53 RID: 31827
			// (set) Token: 0x0600AA2C RID: 43564 RVA: 0x000F6257 File Offset: 0x000F4457
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007C54 RID: 31828
			// (set) Token: 0x0600AA2D RID: 43565 RVA: 0x000F626A File Offset: 0x000F446A
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007C55 RID: 31829
			// (set) Token: 0x0600AA2E RID: 43566 RVA: 0x000F627D File Offset: 0x000F447D
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007C56 RID: 31830
			// (set) Token: 0x0600AA2F RID: 43567 RVA: 0x000F6290 File Offset: 0x000F4490
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007C57 RID: 31831
			// (set) Token: 0x0600AA30 RID: 43568 RVA: 0x000F62A8 File Offset: 0x000F44A8
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007C58 RID: 31832
			// (set) Token: 0x0600AA31 RID: 43569 RVA: 0x000F62BB File Offset: 0x000F44BB
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007C59 RID: 31833
			// (set) Token: 0x0600AA32 RID: 43570 RVA: 0x000F62CE File Offset: 0x000F44CE
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007C5A RID: 31834
			// (set) Token: 0x0600AA33 RID: 43571 RVA: 0x000F62E6 File Offset: 0x000F44E6
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007C5B RID: 31835
			// (set) Token: 0x0600AA34 RID: 43572 RVA: 0x000F62F9 File Offset: 0x000F44F9
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007C5C RID: 31836
			// (set) Token: 0x0600AA35 RID: 43573 RVA: 0x000F6311 File Offset: 0x000F4511
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007C5D RID: 31837
			// (set) Token: 0x0600AA36 RID: 43574 RVA: 0x000F6324 File Offset: 0x000F4524
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007C5E RID: 31838
			// (set) Token: 0x0600AA37 RID: 43575 RVA: 0x000F6342 File Offset: 0x000F4542
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007C5F RID: 31839
			// (set) Token: 0x0600AA38 RID: 43576 RVA: 0x000F6360 File Offset: 0x000F4560
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007C60 RID: 31840
			// (set) Token: 0x0600AA39 RID: 43577 RVA: 0x000F6373 File Offset: 0x000F4573
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007C61 RID: 31841
			// (set) Token: 0x0600AA3A RID: 43578 RVA: 0x000F638B File Offset: 0x000F458B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007C62 RID: 31842
			// (set) Token: 0x0600AA3B RID: 43579 RVA: 0x000F63A3 File Offset: 0x000F45A3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007C63 RID: 31843
			// (set) Token: 0x0600AA3C RID: 43580 RVA: 0x000F63BB File Offset: 0x000F45BB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007C64 RID: 31844
			// (set) Token: 0x0600AA3D RID: 43581 RVA: 0x000F63D3 File Offset: 0x000F45D3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CC3 RID: 3267
		public class ArbitrationParameters : ParametersBase
		{
			// Token: 0x17007C65 RID: 31845
			// (set) Token: 0x0600AA3F RID: 43583 RVA: 0x000F63F3 File Offset: 0x000F45F3
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17007C66 RID: 31846
			// (set) Token: 0x0600AA40 RID: 43584 RVA: 0x000F6406 File Offset: 0x000F4606
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17007C67 RID: 31847
			// (set) Token: 0x0600AA41 RID: 43585 RVA: 0x000F6419 File Offset: 0x000F4619
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x17007C68 RID: 31848
			// (set) Token: 0x0600AA42 RID: 43586 RVA: 0x000F6431 File Offset: 0x000F4631
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007C69 RID: 31849
			// (set) Token: 0x0600AA43 RID: 43587 RVA: 0x000F6444 File Offset: 0x000F4644
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007C6A RID: 31850
			// (set) Token: 0x0600AA44 RID: 43588 RVA: 0x000F645C File Offset: 0x000F465C
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007C6B RID: 31851
			// (set) Token: 0x0600AA45 RID: 43589 RVA: 0x000F646F File Offset: 0x000F466F
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007C6C RID: 31852
			// (set) Token: 0x0600AA46 RID: 43590 RVA: 0x000F6482 File Offset: 0x000F4682
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007C6D RID: 31853
			// (set) Token: 0x0600AA47 RID: 43591 RVA: 0x000F64A0 File Offset: 0x000F46A0
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007C6E RID: 31854
			// (set) Token: 0x0600AA48 RID: 43592 RVA: 0x000F64BE File Offset: 0x000F46BE
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007C6F RID: 31855
			// (set) Token: 0x0600AA49 RID: 43593 RVA: 0x000F64DC File Offset: 0x000F46DC
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007C70 RID: 31856
			// (set) Token: 0x0600AA4A RID: 43594 RVA: 0x000F64FA File Offset: 0x000F46FA
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007C71 RID: 31857
			// (set) Token: 0x0600AA4B RID: 43595 RVA: 0x000F650D File Offset: 0x000F470D
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007C72 RID: 31858
			// (set) Token: 0x0600AA4C RID: 43596 RVA: 0x000F6525 File Offset: 0x000F4725
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007C73 RID: 31859
			// (set) Token: 0x0600AA4D RID: 43597 RVA: 0x000F6543 File Offset: 0x000F4743
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007C74 RID: 31860
			// (set) Token: 0x0600AA4E RID: 43598 RVA: 0x000F655B File Offset: 0x000F475B
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007C75 RID: 31861
			// (set) Token: 0x0600AA4F RID: 43599 RVA: 0x000F6573 File Offset: 0x000F4773
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007C76 RID: 31862
			// (set) Token: 0x0600AA50 RID: 43600 RVA: 0x000F6586 File Offset: 0x000F4786
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007C77 RID: 31863
			// (set) Token: 0x0600AA51 RID: 43601 RVA: 0x000F659E File Offset: 0x000F479E
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007C78 RID: 31864
			// (set) Token: 0x0600AA52 RID: 43602 RVA: 0x000F65B1 File Offset: 0x000F47B1
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007C79 RID: 31865
			// (set) Token: 0x0600AA53 RID: 43603 RVA: 0x000F65C4 File Offset: 0x000F47C4
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007C7A RID: 31866
			// (set) Token: 0x0600AA54 RID: 43604 RVA: 0x000F65D7 File Offset: 0x000F47D7
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007C7B RID: 31867
			// (set) Token: 0x0600AA55 RID: 43605 RVA: 0x000F65EA File Offset: 0x000F47EA
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007C7C RID: 31868
			// (set) Token: 0x0600AA56 RID: 43606 RVA: 0x000F6602 File Offset: 0x000F4802
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007C7D RID: 31869
			// (set) Token: 0x0600AA57 RID: 43607 RVA: 0x000F6615 File Offset: 0x000F4815
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007C7E RID: 31870
			// (set) Token: 0x0600AA58 RID: 43608 RVA: 0x000F6628 File Offset: 0x000F4828
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007C7F RID: 31871
			// (set) Token: 0x0600AA59 RID: 43609 RVA: 0x000F6640 File Offset: 0x000F4840
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007C80 RID: 31872
			// (set) Token: 0x0600AA5A RID: 43610 RVA: 0x000F6653 File Offset: 0x000F4853
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007C81 RID: 31873
			// (set) Token: 0x0600AA5B RID: 43611 RVA: 0x000F666B File Offset: 0x000F486B
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007C82 RID: 31874
			// (set) Token: 0x0600AA5C RID: 43612 RVA: 0x000F667E File Offset: 0x000F487E
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007C83 RID: 31875
			// (set) Token: 0x0600AA5D RID: 43613 RVA: 0x000F669C File Offset: 0x000F489C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007C84 RID: 31876
			// (set) Token: 0x0600AA5E RID: 43614 RVA: 0x000F66BA File Offset: 0x000F48BA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007C85 RID: 31877
			// (set) Token: 0x0600AA5F RID: 43615 RVA: 0x000F66CD File Offset: 0x000F48CD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007C86 RID: 31878
			// (set) Token: 0x0600AA60 RID: 43616 RVA: 0x000F66E5 File Offset: 0x000F48E5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007C87 RID: 31879
			// (set) Token: 0x0600AA61 RID: 43617 RVA: 0x000F66FD File Offset: 0x000F48FD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007C88 RID: 31880
			// (set) Token: 0x0600AA62 RID: 43618 RVA: 0x000F6715 File Offset: 0x000F4915
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007C89 RID: 31881
			// (set) Token: 0x0600AA63 RID: 43619 RVA: 0x000F672D File Offset: 0x000F492D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CC4 RID: 3268
		public class EquipmentParameters : ParametersBase
		{
			// Token: 0x17007C8A RID: 31882
			// (set) Token: 0x0600AA65 RID: 43621 RVA: 0x000F674D File Offset: 0x000F494D
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17007C8B RID: 31883
			// (set) Token: 0x0600AA66 RID: 43622 RVA: 0x000F6760 File Offset: 0x000F4960
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17007C8C RID: 31884
			// (set) Token: 0x0600AA67 RID: 43623 RVA: 0x000F6773 File Offset: 0x000F4973
			public virtual SwitchParameter Equipment
			{
				set
				{
					base.PowerSharpParameters["Equipment"] = value;
				}
			}

			// Token: 0x17007C8D RID: 31885
			// (set) Token: 0x0600AA68 RID: 43624 RVA: 0x000F678B File Offset: 0x000F498B
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007C8E RID: 31886
			// (set) Token: 0x0600AA69 RID: 43625 RVA: 0x000F67A9 File Offset: 0x000F49A9
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007C8F RID: 31887
			// (set) Token: 0x0600AA6A RID: 43626 RVA: 0x000F67BC File Offset: 0x000F49BC
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17007C90 RID: 31888
			// (set) Token: 0x0600AA6B RID: 43627 RVA: 0x000F67D4 File Offset: 0x000F49D4
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17007C91 RID: 31889
			// (set) Token: 0x0600AA6C RID: 43628 RVA: 0x000F67EC File Offset: 0x000F49EC
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007C92 RID: 31890
			// (set) Token: 0x0600AA6D RID: 43629 RVA: 0x000F67FF File Offset: 0x000F49FF
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007C93 RID: 31891
			// (set) Token: 0x0600AA6E RID: 43630 RVA: 0x000F6817 File Offset: 0x000F4A17
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007C94 RID: 31892
			// (set) Token: 0x0600AA6F RID: 43631 RVA: 0x000F682A File Offset: 0x000F4A2A
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007C95 RID: 31893
			// (set) Token: 0x0600AA70 RID: 43632 RVA: 0x000F683D File Offset: 0x000F4A3D
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007C96 RID: 31894
			// (set) Token: 0x0600AA71 RID: 43633 RVA: 0x000F685B File Offset: 0x000F4A5B
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007C97 RID: 31895
			// (set) Token: 0x0600AA72 RID: 43634 RVA: 0x000F6879 File Offset: 0x000F4A79
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007C98 RID: 31896
			// (set) Token: 0x0600AA73 RID: 43635 RVA: 0x000F6897 File Offset: 0x000F4A97
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007C99 RID: 31897
			// (set) Token: 0x0600AA74 RID: 43636 RVA: 0x000F68B5 File Offset: 0x000F4AB5
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007C9A RID: 31898
			// (set) Token: 0x0600AA75 RID: 43637 RVA: 0x000F68C8 File Offset: 0x000F4AC8
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007C9B RID: 31899
			// (set) Token: 0x0600AA76 RID: 43638 RVA: 0x000F68E0 File Offset: 0x000F4AE0
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007C9C RID: 31900
			// (set) Token: 0x0600AA77 RID: 43639 RVA: 0x000F68FE File Offset: 0x000F4AFE
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007C9D RID: 31901
			// (set) Token: 0x0600AA78 RID: 43640 RVA: 0x000F6916 File Offset: 0x000F4B16
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007C9E RID: 31902
			// (set) Token: 0x0600AA79 RID: 43641 RVA: 0x000F692E File Offset: 0x000F4B2E
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007C9F RID: 31903
			// (set) Token: 0x0600AA7A RID: 43642 RVA: 0x000F6941 File Offset: 0x000F4B41
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007CA0 RID: 31904
			// (set) Token: 0x0600AA7B RID: 43643 RVA: 0x000F6959 File Offset: 0x000F4B59
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007CA1 RID: 31905
			// (set) Token: 0x0600AA7C RID: 43644 RVA: 0x000F696C File Offset: 0x000F4B6C
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007CA2 RID: 31906
			// (set) Token: 0x0600AA7D RID: 43645 RVA: 0x000F697F File Offset: 0x000F4B7F
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007CA3 RID: 31907
			// (set) Token: 0x0600AA7E RID: 43646 RVA: 0x000F6992 File Offset: 0x000F4B92
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007CA4 RID: 31908
			// (set) Token: 0x0600AA7F RID: 43647 RVA: 0x000F69A5 File Offset: 0x000F4BA5
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007CA5 RID: 31909
			// (set) Token: 0x0600AA80 RID: 43648 RVA: 0x000F69BD File Offset: 0x000F4BBD
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007CA6 RID: 31910
			// (set) Token: 0x0600AA81 RID: 43649 RVA: 0x000F69D0 File Offset: 0x000F4BD0
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007CA7 RID: 31911
			// (set) Token: 0x0600AA82 RID: 43650 RVA: 0x000F69E3 File Offset: 0x000F4BE3
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007CA8 RID: 31912
			// (set) Token: 0x0600AA83 RID: 43651 RVA: 0x000F69FB File Offset: 0x000F4BFB
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007CA9 RID: 31913
			// (set) Token: 0x0600AA84 RID: 43652 RVA: 0x000F6A0E File Offset: 0x000F4C0E
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007CAA RID: 31914
			// (set) Token: 0x0600AA85 RID: 43653 RVA: 0x000F6A26 File Offset: 0x000F4C26
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007CAB RID: 31915
			// (set) Token: 0x0600AA86 RID: 43654 RVA: 0x000F6A39 File Offset: 0x000F4C39
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007CAC RID: 31916
			// (set) Token: 0x0600AA87 RID: 43655 RVA: 0x000F6A57 File Offset: 0x000F4C57
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007CAD RID: 31917
			// (set) Token: 0x0600AA88 RID: 43656 RVA: 0x000F6A75 File Offset: 0x000F4C75
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007CAE RID: 31918
			// (set) Token: 0x0600AA89 RID: 43657 RVA: 0x000F6A88 File Offset: 0x000F4C88
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007CAF RID: 31919
			// (set) Token: 0x0600AA8A RID: 43658 RVA: 0x000F6AA0 File Offset: 0x000F4CA0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007CB0 RID: 31920
			// (set) Token: 0x0600AA8B RID: 43659 RVA: 0x000F6AB8 File Offset: 0x000F4CB8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007CB1 RID: 31921
			// (set) Token: 0x0600AA8C RID: 43660 RVA: 0x000F6AD0 File Offset: 0x000F4CD0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007CB2 RID: 31922
			// (set) Token: 0x0600AA8D RID: 43661 RVA: 0x000F6AE8 File Offset: 0x000F4CE8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CC5 RID: 3269
		public class AuxMailboxParameters : ParametersBase
		{
			// Token: 0x17007CB3 RID: 31923
			// (set) Token: 0x0600AA8F RID: 43663 RVA: 0x000F6B08 File Offset: 0x000F4D08
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17007CB4 RID: 31924
			// (set) Token: 0x0600AA90 RID: 43664 RVA: 0x000F6B1B File Offset: 0x000F4D1B
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17007CB5 RID: 31925
			// (set) Token: 0x0600AA91 RID: 43665 RVA: 0x000F6B2E File Offset: 0x000F4D2E
			public virtual SwitchParameter AuxMailbox
			{
				set
				{
					base.PowerSharpParameters["AuxMailbox"] = value;
				}
			}

			// Token: 0x17007CB6 RID: 31926
			// (set) Token: 0x0600AA92 RID: 43666 RVA: 0x000F6B46 File Offset: 0x000F4D46
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007CB7 RID: 31927
			// (set) Token: 0x0600AA93 RID: 43667 RVA: 0x000F6B59 File Offset: 0x000F4D59
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007CB8 RID: 31928
			// (set) Token: 0x0600AA94 RID: 43668 RVA: 0x000F6B71 File Offset: 0x000F4D71
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007CB9 RID: 31929
			// (set) Token: 0x0600AA95 RID: 43669 RVA: 0x000F6B84 File Offset: 0x000F4D84
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007CBA RID: 31930
			// (set) Token: 0x0600AA96 RID: 43670 RVA: 0x000F6B97 File Offset: 0x000F4D97
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007CBB RID: 31931
			// (set) Token: 0x0600AA97 RID: 43671 RVA: 0x000F6BB5 File Offset: 0x000F4DB5
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007CBC RID: 31932
			// (set) Token: 0x0600AA98 RID: 43672 RVA: 0x000F6BD3 File Offset: 0x000F4DD3
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007CBD RID: 31933
			// (set) Token: 0x0600AA99 RID: 43673 RVA: 0x000F6BF1 File Offset: 0x000F4DF1
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007CBE RID: 31934
			// (set) Token: 0x0600AA9A RID: 43674 RVA: 0x000F6C0F File Offset: 0x000F4E0F
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007CBF RID: 31935
			// (set) Token: 0x0600AA9B RID: 43675 RVA: 0x000F6C22 File Offset: 0x000F4E22
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007CC0 RID: 31936
			// (set) Token: 0x0600AA9C RID: 43676 RVA: 0x000F6C3A File Offset: 0x000F4E3A
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007CC1 RID: 31937
			// (set) Token: 0x0600AA9D RID: 43677 RVA: 0x000F6C58 File Offset: 0x000F4E58
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007CC2 RID: 31938
			// (set) Token: 0x0600AA9E RID: 43678 RVA: 0x000F6C70 File Offset: 0x000F4E70
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007CC3 RID: 31939
			// (set) Token: 0x0600AA9F RID: 43679 RVA: 0x000F6C88 File Offset: 0x000F4E88
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007CC4 RID: 31940
			// (set) Token: 0x0600AAA0 RID: 43680 RVA: 0x000F6C9B File Offset: 0x000F4E9B
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007CC5 RID: 31941
			// (set) Token: 0x0600AAA1 RID: 43681 RVA: 0x000F6CB3 File Offset: 0x000F4EB3
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007CC6 RID: 31942
			// (set) Token: 0x0600AAA2 RID: 43682 RVA: 0x000F6CC6 File Offset: 0x000F4EC6
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007CC7 RID: 31943
			// (set) Token: 0x0600AAA3 RID: 43683 RVA: 0x000F6CD9 File Offset: 0x000F4ED9
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007CC8 RID: 31944
			// (set) Token: 0x0600AAA4 RID: 43684 RVA: 0x000F6CEC File Offset: 0x000F4EEC
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007CC9 RID: 31945
			// (set) Token: 0x0600AAA5 RID: 43685 RVA: 0x000F6CFF File Offset: 0x000F4EFF
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007CCA RID: 31946
			// (set) Token: 0x0600AAA6 RID: 43686 RVA: 0x000F6D17 File Offset: 0x000F4F17
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007CCB RID: 31947
			// (set) Token: 0x0600AAA7 RID: 43687 RVA: 0x000F6D2A File Offset: 0x000F4F2A
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007CCC RID: 31948
			// (set) Token: 0x0600AAA8 RID: 43688 RVA: 0x000F6D3D File Offset: 0x000F4F3D
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007CCD RID: 31949
			// (set) Token: 0x0600AAA9 RID: 43689 RVA: 0x000F6D55 File Offset: 0x000F4F55
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007CCE RID: 31950
			// (set) Token: 0x0600AAAA RID: 43690 RVA: 0x000F6D68 File Offset: 0x000F4F68
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007CCF RID: 31951
			// (set) Token: 0x0600AAAB RID: 43691 RVA: 0x000F6D80 File Offset: 0x000F4F80
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007CD0 RID: 31952
			// (set) Token: 0x0600AAAC RID: 43692 RVA: 0x000F6D93 File Offset: 0x000F4F93
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007CD1 RID: 31953
			// (set) Token: 0x0600AAAD RID: 43693 RVA: 0x000F6DB1 File Offset: 0x000F4FB1
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007CD2 RID: 31954
			// (set) Token: 0x0600AAAE RID: 43694 RVA: 0x000F6DCF File Offset: 0x000F4FCF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007CD3 RID: 31955
			// (set) Token: 0x0600AAAF RID: 43695 RVA: 0x000F6DE2 File Offset: 0x000F4FE2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007CD4 RID: 31956
			// (set) Token: 0x0600AAB0 RID: 43696 RVA: 0x000F6DFA File Offset: 0x000F4FFA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007CD5 RID: 31957
			// (set) Token: 0x0600AAB1 RID: 43697 RVA: 0x000F6E12 File Offset: 0x000F5012
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007CD6 RID: 31958
			// (set) Token: 0x0600AAB2 RID: 43698 RVA: 0x000F6E2A File Offset: 0x000F502A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007CD7 RID: 31959
			// (set) Token: 0x0600AAB3 RID: 43699 RVA: 0x000F6E42 File Offset: 0x000F5042
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CC6 RID: 3270
		public class TeamMailboxITProParameters : ParametersBase
		{
			// Token: 0x17007CD8 RID: 31960
			// (set) Token: 0x0600AAB5 RID: 43701 RVA: 0x000F6E62 File Offset: 0x000F5062
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17007CD9 RID: 31961
			// (set) Token: 0x0600AAB6 RID: 43702 RVA: 0x000F6E75 File Offset: 0x000F5075
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17007CDA RID: 31962
			// (set) Token: 0x0600AAB7 RID: 43703 RVA: 0x000F6E88 File Offset: 0x000F5088
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007CDB RID: 31963
			// (set) Token: 0x0600AAB8 RID: 43704 RVA: 0x000F6EA6 File Offset: 0x000F50A6
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007CDC RID: 31964
			// (set) Token: 0x0600AAB9 RID: 43705 RVA: 0x000F6EB9 File Offset: 0x000F50B9
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17007CDD RID: 31965
			// (set) Token: 0x0600AABA RID: 43706 RVA: 0x000F6ED1 File Offset: 0x000F50D1
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17007CDE RID: 31966
			// (set) Token: 0x0600AABB RID: 43707 RVA: 0x000F6EE9 File Offset: 0x000F50E9
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007CDF RID: 31967
			// (set) Token: 0x0600AABC RID: 43708 RVA: 0x000F6EFC File Offset: 0x000F50FC
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007CE0 RID: 31968
			// (set) Token: 0x0600AABD RID: 43709 RVA: 0x000F6F14 File Offset: 0x000F5114
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007CE1 RID: 31969
			// (set) Token: 0x0600AABE RID: 43710 RVA: 0x000F6F27 File Offset: 0x000F5127
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007CE2 RID: 31970
			// (set) Token: 0x0600AABF RID: 43711 RVA: 0x000F6F3A File Offset: 0x000F513A
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007CE3 RID: 31971
			// (set) Token: 0x0600AAC0 RID: 43712 RVA: 0x000F6F58 File Offset: 0x000F5158
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007CE4 RID: 31972
			// (set) Token: 0x0600AAC1 RID: 43713 RVA: 0x000F6F76 File Offset: 0x000F5176
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007CE5 RID: 31973
			// (set) Token: 0x0600AAC2 RID: 43714 RVA: 0x000F6F94 File Offset: 0x000F5194
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007CE6 RID: 31974
			// (set) Token: 0x0600AAC3 RID: 43715 RVA: 0x000F6FB2 File Offset: 0x000F51B2
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007CE7 RID: 31975
			// (set) Token: 0x0600AAC4 RID: 43716 RVA: 0x000F6FC5 File Offset: 0x000F51C5
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007CE8 RID: 31976
			// (set) Token: 0x0600AAC5 RID: 43717 RVA: 0x000F6FDD File Offset: 0x000F51DD
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007CE9 RID: 31977
			// (set) Token: 0x0600AAC6 RID: 43718 RVA: 0x000F6FFB File Offset: 0x000F51FB
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007CEA RID: 31978
			// (set) Token: 0x0600AAC7 RID: 43719 RVA: 0x000F7013 File Offset: 0x000F5213
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007CEB RID: 31979
			// (set) Token: 0x0600AAC8 RID: 43720 RVA: 0x000F702B File Offset: 0x000F522B
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007CEC RID: 31980
			// (set) Token: 0x0600AAC9 RID: 43721 RVA: 0x000F703E File Offset: 0x000F523E
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007CED RID: 31981
			// (set) Token: 0x0600AACA RID: 43722 RVA: 0x000F7056 File Offset: 0x000F5256
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007CEE RID: 31982
			// (set) Token: 0x0600AACB RID: 43723 RVA: 0x000F7069 File Offset: 0x000F5269
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007CEF RID: 31983
			// (set) Token: 0x0600AACC RID: 43724 RVA: 0x000F707C File Offset: 0x000F527C
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007CF0 RID: 31984
			// (set) Token: 0x0600AACD RID: 43725 RVA: 0x000F708F File Offset: 0x000F528F
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007CF1 RID: 31985
			// (set) Token: 0x0600AACE RID: 43726 RVA: 0x000F70A2 File Offset: 0x000F52A2
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007CF2 RID: 31986
			// (set) Token: 0x0600AACF RID: 43727 RVA: 0x000F70BA File Offset: 0x000F52BA
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007CF3 RID: 31987
			// (set) Token: 0x0600AAD0 RID: 43728 RVA: 0x000F70CD File Offset: 0x000F52CD
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007CF4 RID: 31988
			// (set) Token: 0x0600AAD1 RID: 43729 RVA: 0x000F70E0 File Offset: 0x000F52E0
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007CF5 RID: 31989
			// (set) Token: 0x0600AAD2 RID: 43730 RVA: 0x000F70F8 File Offset: 0x000F52F8
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007CF6 RID: 31990
			// (set) Token: 0x0600AAD3 RID: 43731 RVA: 0x000F710B File Offset: 0x000F530B
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007CF7 RID: 31991
			// (set) Token: 0x0600AAD4 RID: 43732 RVA: 0x000F7123 File Offset: 0x000F5323
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007CF8 RID: 31992
			// (set) Token: 0x0600AAD5 RID: 43733 RVA: 0x000F7136 File Offset: 0x000F5336
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007CF9 RID: 31993
			// (set) Token: 0x0600AAD6 RID: 43734 RVA: 0x000F7154 File Offset: 0x000F5354
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007CFA RID: 31994
			// (set) Token: 0x0600AAD7 RID: 43735 RVA: 0x000F7172 File Offset: 0x000F5372
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007CFB RID: 31995
			// (set) Token: 0x0600AAD8 RID: 43736 RVA: 0x000F7185 File Offset: 0x000F5385
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007CFC RID: 31996
			// (set) Token: 0x0600AAD9 RID: 43737 RVA: 0x000F719D File Offset: 0x000F539D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007CFD RID: 31997
			// (set) Token: 0x0600AADA RID: 43738 RVA: 0x000F71B5 File Offset: 0x000F53B5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007CFE RID: 31998
			// (set) Token: 0x0600AADB RID: 43739 RVA: 0x000F71CD File Offset: 0x000F53CD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007CFF RID: 31999
			// (set) Token: 0x0600AADC RID: 43740 RVA: 0x000F71E5 File Offset: 0x000F53E5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CC7 RID: 3271
		public class LinkedParameters : ParametersBase
		{
			// Token: 0x17007D00 RID: 32000
			// (set) Token: 0x0600AADE RID: 43742 RVA: 0x000F7205 File Offset: 0x000F5405
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17007D01 RID: 32001
			// (set) Token: 0x0600AADF RID: 43743 RVA: 0x000F7218 File Offset: 0x000F5418
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17007D02 RID: 32002
			// (set) Token: 0x0600AAE0 RID: 43744 RVA: 0x000F722B File Offset: 0x000F542B
			public virtual string LinkedMasterAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedMasterAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17007D03 RID: 32003
			// (set) Token: 0x0600AAE1 RID: 43745 RVA: 0x000F7249 File Offset: 0x000F5449
			public virtual string LinkedDomainController
			{
				set
				{
					base.PowerSharpParameters["LinkedDomainController"] = value;
				}
			}

			// Token: 0x17007D04 RID: 32004
			// (set) Token: 0x0600AAE2 RID: 43746 RVA: 0x000F725C File Offset: 0x000F545C
			public virtual PSCredential LinkedCredential
			{
				set
				{
					base.PowerSharpParameters["LinkedCredential"] = value;
				}
			}

			// Token: 0x17007D05 RID: 32005
			// (set) Token: 0x0600AAE3 RID: 43747 RVA: 0x000F726F File Offset: 0x000F546F
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007D06 RID: 32006
			// (set) Token: 0x0600AAE4 RID: 43748 RVA: 0x000F728D File Offset: 0x000F548D
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007D07 RID: 32007
			// (set) Token: 0x0600AAE5 RID: 43749 RVA: 0x000F72A0 File Offset: 0x000F54A0
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17007D08 RID: 32008
			// (set) Token: 0x0600AAE6 RID: 43750 RVA: 0x000F72B8 File Offset: 0x000F54B8
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17007D09 RID: 32009
			// (set) Token: 0x0600AAE7 RID: 43751 RVA: 0x000F72D0 File Offset: 0x000F54D0
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007D0A RID: 32010
			// (set) Token: 0x0600AAE8 RID: 43752 RVA: 0x000F72E3 File Offset: 0x000F54E3
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007D0B RID: 32011
			// (set) Token: 0x0600AAE9 RID: 43753 RVA: 0x000F72FB File Offset: 0x000F54FB
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007D0C RID: 32012
			// (set) Token: 0x0600AAEA RID: 43754 RVA: 0x000F730E File Offset: 0x000F550E
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007D0D RID: 32013
			// (set) Token: 0x0600AAEB RID: 43755 RVA: 0x000F7321 File Offset: 0x000F5521
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007D0E RID: 32014
			// (set) Token: 0x0600AAEC RID: 43756 RVA: 0x000F733F File Offset: 0x000F553F
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007D0F RID: 32015
			// (set) Token: 0x0600AAED RID: 43757 RVA: 0x000F735D File Offset: 0x000F555D
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007D10 RID: 32016
			// (set) Token: 0x0600AAEE RID: 43758 RVA: 0x000F737B File Offset: 0x000F557B
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007D11 RID: 32017
			// (set) Token: 0x0600AAEF RID: 43759 RVA: 0x000F7399 File Offset: 0x000F5599
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007D12 RID: 32018
			// (set) Token: 0x0600AAF0 RID: 43760 RVA: 0x000F73AC File Offset: 0x000F55AC
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007D13 RID: 32019
			// (set) Token: 0x0600AAF1 RID: 43761 RVA: 0x000F73C4 File Offset: 0x000F55C4
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007D14 RID: 32020
			// (set) Token: 0x0600AAF2 RID: 43762 RVA: 0x000F73E2 File Offset: 0x000F55E2
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007D15 RID: 32021
			// (set) Token: 0x0600AAF3 RID: 43763 RVA: 0x000F73FA File Offset: 0x000F55FA
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007D16 RID: 32022
			// (set) Token: 0x0600AAF4 RID: 43764 RVA: 0x000F7412 File Offset: 0x000F5612
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007D17 RID: 32023
			// (set) Token: 0x0600AAF5 RID: 43765 RVA: 0x000F7425 File Offset: 0x000F5625
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007D18 RID: 32024
			// (set) Token: 0x0600AAF6 RID: 43766 RVA: 0x000F743D File Offset: 0x000F563D
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007D19 RID: 32025
			// (set) Token: 0x0600AAF7 RID: 43767 RVA: 0x000F7450 File Offset: 0x000F5650
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007D1A RID: 32026
			// (set) Token: 0x0600AAF8 RID: 43768 RVA: 0x000F7463 File Offset: 0x000F5663
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007D1B RID: 32027
			// (set) Token: 0x0600AAF9 RID: 43769 RVA: 0x000F7476 File Offset: 0x000F5676
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007D1C RID: 32028
			// (set) Token: 0x0600AAFA RID: 43770 RVA: 0x000F7489 File Offset: 0x000F5689
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007D1D RID: 32029
			// (set) Token: 0x0600AAFB RID: 43771 RVA: 0x000F74A1 File Offset: 0x000F56A1
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007D1E RID: 32030
			// (set) Token: 0x0600AAFC RID: 43772 RVA: 0x000F74B4 File Offset: 0x000F56B4
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007D1F RID: 32031
			// (set) Token: 0x0600AAFD RID: 43773 RVA: 0x000F74C7 File Offset: 0x000F56C7
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007D20 RID: 32032
			// (set) Token: 0x0600AAFE RID: 43774 RVA: 0x000F74DF File Offset: 0x000F56DF
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007D21 RID: 32033
			// (set) Token: 0x0600AAFF RID: 43775 RVA: 0x000F74F2 File Offset: 0x000F56F2
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007D22 RID: 32034
			// (set) Token: 0x0600AB00 RID: 43776 RVA: 0x000F750A File Offset: 0x000F570A
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007D23 RID: 32035
			// (set) Token: 0x0600AB01 RID: 43777 RVA: 0x000F751D File Offset: 0x000F571D
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007D24 RID: 32036
			// (set) Token: 0x0600AB02 RID: 43778 RVA: 0x000F753B File Offset: 0x000F573B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007D25 RID: 32037
			// (set) Token: 0x0600AB03 RID: 43779 RVA: 0x000F7559 File Offset: 0x000F5759
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007D26 RID: 32038
			// (set) Token: 0x0600AB04 RID: 43780 RVA: 0x000F756C File Offset: 0x000F576C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007D27 RID: 32039
			// (set) Token: 0x0600AB05 RID: 43781 RVA: 0x000F7584 File Offset: 0x000F5784
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007D28 RID: 32040
			// (set) Token: 0x0600AB06 RID: 43782 RVA: 0x000F759C File Offset: 0x000F579C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007D29 RID: 32041
			// (set) Token: 0x0600AB07 RID: 43783 RVA: 0x000F75B4 File Offset: 0x000F57B4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007D2A RID: 32042
			// (set) Token: 0x0600AB08 RID: 43784 RVA: 0x000F75CC File Offset: 0x000F57CC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CC8 RID: 3272
		public class RoomParameters : ParametersBase
		{
			// Token: 0x17007D2B RID: 32043
			// (set) Token: 0x0600AB0A RID: 43786 RVA: 0x000F75EC File Offset: 0x000F57EC
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17007D2C RID: 32044
			// (set) Token: 0x0600AB0B RID: 43787 RVA: 0x000F75FF File Offset: 0x000F57FF
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17007D2D RID: 32045
			// (set) Token: 0x0600AB0C RID: 43788 RVA: 0x000F7612 File Offset: 0x000F5812
			public virtual SwitchParameter Room
			{
				set
				{
					base.PowerSharpParameters["Room"] = value;
				}
			}

			// Token: 0x17007D2E RID: 32046
			// (set) Token: 0x0600AB0D RID: 43789 RVA: 0x000F762A File Offset: 0x000F582A
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007D2F RID: 32047
			// (set) Token: 0x0600AB0E RID: 43790 RVA: 0x000F7648 File Offset: 0x000F5848
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007D30 RID: 32048
			// (set) Token: 0x0600AB0F RID: 43791 RVA: 0x000F765B File Offset: 0x000F585B
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17007D31 RID: 32049
			// (set) Token: 0x0600AB10 RID: 43792 RVA: 0x000F7673 File Offset: 0x000F5873
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17007D32 RID: 32050
			// (set) Token: 0x0600AB11 RID: 43793 RVA: 0x000F768B File Offset: 0x000F588B
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007D33 RID: 32051
			// (set) Token: 0x0600AB12 RID: 43794 RVA: 0x000F769E File Offset: 0x000F589E
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007D34 RID: 32052
			// (set) Token: 0x0600AB13 RID: 43795 RVA: 0x000F76B6 File Offset: 0x000F58B6
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007D35 RID: 32053
			// (set) Token: 0x0600AB14 RID: 43796 RVA: 0x000F76C9 File Offset: 0x000F58C9
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007D36 RID: 32054
			// (set) Token: 0x0600AB15 RID: 43797 RVA: 0x000F76DC File Offset: 0x000F58DC
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007D37 RID: 32055
			// (set) Token: 0x0600AB16 RID: 43798 RVA: 0x000F76FA File Offset: 0x000F58FA
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007D38 RID: 32056
			// (set) Token: 0x0600AB17 RID: 43799 RVA: 0x000F7718 File Offset: 0x000F5918
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007D39 RID: 32057
			// (set) Token: 0x0600AB18 RID: 43800 RVA: 0x000F7736 File Offset: 0x000F5936
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007D3A RID: 32058
			// (set) Token: 0x0600AB19 RID: 43801 RVA: 0x000F7754 File Offset: 0x000F5954
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007D3B RID: 32059
			// (set) Token: 0x0600AB1A RID: 43802 RVA: 0x000F7767 File Offset: 0x000F5967
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007D3C RID: 32060
			// (set) Token: 0x0600AB1B RID: 43803 RVA: 0x000F777F File Offset: 0x000F597F
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007D3D RID: 32061
			// (set) Token: 0x0600AB1C RID: 43804 RVA: 0x000F779D File Offset: 0x000F599D
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007D3E RID: 32062
			// (set) Token: 0x0600AB1D RID: 43805 RVA: 0x000F77B5 File Offset: 0x000F59B5
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007D3F RID: 32063
			// (set) Token: 0x0600AB1E RID: 43806 RVA: 0x000F77CD File Offset: 0x000F59CD
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007D40 RID: 32064
			// (set) Token: 0x0600AB1F RID: 43807 RVA: 0x000F77E0 File Offset: 0x000F59E0
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007D41 RID: 32065
			// (set) Token: 0x0600AB20 RID: 43808 RVA: 0x000F77F8 File Offset: 0x000F59F8
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007D42 RID: 32066
			// (set) Token: 0x0600AB21 RID: 43809 RVA: 0x000F780B File Offset: 0x000F5A0B
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007D43 RID: 32067
			// (set) Token: 0x0600AB22 RID: 43810 RVA: 0x000F781E File Offset: 0x000F5A1E
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007D44 RID: 32068
			// (set) Token: 0x0600AB23 RID: 43811 RVA: 0x000F7831 File Offset: 0x000F5A31
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007D45 RID: 32069
			// (set) Token: 0x0600AB24 RID: 43812 RVA: 0x000F7844 File Offset: 0x000F5A44
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007D46 RID: 32070
			// (set) Token: 0x0600AB25 RID: 43813 RVA: 0x000F785C File Offset: 0x000F5A5C
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007D47 RID: 32071
			// (set) Token: 0x0600AB26 RID: 43814 RVA: 0x000F786F File Offset: 0x000F5A6F
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007D48 RID: 32072
			// (set) Token: 0x0600AB27 RID: 43815 RVA: 0x000F7882 File Offset: 0x000F5A82
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007D49 RID: 32073
			// (set) Token: 0x0600AB28 RID: 43816 RVA: 0x000F789A File Offset: 0x000F5A9A
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007D4A RID: 32074
			// (set) Token: 0x0600AB29 RID: 43817 RVA: 0x000F78AD File Offset: 0x000F5AAD
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007D4B RID: 32075
			// (set) Token: 0x0600AB2A RID: 43818 RVA: 0x000F78C5 File Offset: 0x000F5AC5
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007D4C RID: 32076
			// (set) Token: 0x0600AB2B RID: 43819 RVA: 0x000F78D8 File Offset: 0x000F5AD8
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007D4D RID: 32077
			// (set) Token: 0x0600AB2C RID: 43820 RVA: 0x000F78F6 File Offset: 0x000F5AF6
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007D4E RID: 32078
			// (set) Token: 0x0600AB2D RID: 43821 RVA: 0x000F7914 File Offset: 0x000F5B14
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007D4F RID: 32079
			// (set) Token: 0x0600AB2E RID: 43822 RVA: 0x000F7927 File Offset: 0x000F5B27
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007D50 RID: 32080
			// (set) Token: 0x0600AB2F RID: 43823 RVA: 0x000F793F File Offset: 0x000F5B3F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007D51 RID: 32081
			// (set) Token: 0x0600AB30 RID: 43824 RVA: 0x000F7957 File Offset: 0x000F5B57
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007D52 RID: 32082
			// (set) Token: 0x0600AB31 RID: 43825 RVA: 0x000F796F File Offset: 0x000F5B6F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007D53 RID: 32083
			// (set) Token: 0x0600AB32 RID: 43826 RVA: 0x000F7987 File Offset: 0x000F5B87
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CC9 RID: 3273
		public class SharedParameters : ParametersBase
		{
			// Token: 0x17007D54 RID: 32084
			// (set) Token: 0x0600AB34 RID: 43828 RVA: 0x000F79A7 File Offset: 0x000F5BA7
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17007D55 RID: 32085
			// (set) Token: 0x0600AB35 RID: 43829 RVA: 0x000F79BA File Offset: 0x000F5BBA
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17007D56 RID: 32086
			// (set) Token: 0x0600AB36 RID: 43830 RVA: 0x000F79CD File Offset: 0x000F5BCD
			public virtual SwitchParameter Shared
			{
				set
				{
					base.PowerSharpParameters["Shared"] = value;
				}
			}

			// Token: 0x17007D57 RID: 32087
			// (set) Token: 0x0600AB37 RID: 43831 RVA: 0x000F79E5 File Offset: 0x000F5BE5
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007D58 RID: 32088
			// (set) Token: 0x0600AB38 RID: 43832 RVA: 0x000F7A03 File Offset: 0x000F5C03
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007D59 RID: 32089
			// (set) Token: 0x0600AB39 RID: 43833 RVA: 0x000F7A16 File Offset: 0x000F5C16
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17007D5A RID: 32090
			// (set) Token: 0x0600AB3A RID: 43834 RVA: 0x000F7A2E File Offset: 0x000F5C2E
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17007D5B RID: 32091
			// (set) Token: 0x0600AB3B RID: 43835 RVA: 0x000F7A46 File Offset: 0x000F5C46
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007D5C RID: 32092
			// (set) Token: 0x0600AB3C RID: 43836 RVA: 0x000F7A59 File Offset: 0x000F5C59
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007D5D RID: 32093
			// (set) Token: 0x0600AB3D RID: 43837 RVA: 0x000F7A71 File Offset: 0x000F5C71
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007D5E RID: 32094
			// (set) Token: 0x0600AB3E RID: 43838 RVA: 0x000F7A84 File Offset: 0x000F5C84
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007D5F RID: 32095
			// (set) Token: 0x0600AB3F RID: 43839 RVA: 0x000F7A97 File Offset: 0x000F5C97
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007D60 RID: 32096
			// (set) Token: 0x0600AB40 RID: 43840 RVA: 0x000F7AB5 File Offset: 0x000F5CB5
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007D61 RID: 32097
			// (set) Token: 0x0600AB41 RID: 43841 RVA: 0x000F7AD3 File Offset: 0x000F5CD3
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007D62 RID: 32098
			// (set) Token: 0x0600AB42 RID: 43842 RVA: 0x000F7AF1 File Offset: 0x000F5CF1
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007D63 RID: 32099
			// (set) Token: 0x0600AB43 RID: 43843 RVA: 0x000F7B0F File Offset: 0x000F5D0F
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007D64 RID: 32100
			// (set) Token: 0x0600AB44 RID: 43844 RVA: 0x000F7B22 File Offset: 0x000F5D22
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007D65 RID: 32101
			// (set) Token: 0x0600AB45 RID: 43845 RVA: 0x000F7B3A File Offset: 0x000F5D3A
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007D66 RID: 32102
			// (set) Token: 0x0600AB46 RID: 43846 RVA: 0x000F7B58 File Offset: 0x000F5D58
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007D67 RID: 32103
			// (set) Token: 0x0600AB47 RID: 43847 RVA: 0x000F7B70 File Offset: 0x000F5D70
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007D68 RID: 32104
			// (set) Token: 0x0600AB48 RID: 43848 RVA: 0x000F7B88 File Offset: 0x000F5D88
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007D69 RID: 32105
			// (set) Token: 0x0600AB49 RID: 43849 RVA: 0x000F7B9B File Offset: 0x000F5D9B
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007D6A RID: 32106
			// (set) Token: 0x0600AB4A RID: 43850 RVA: 0x000F7BB3 File Offset: 0x000F5DB3
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007D6B RID: 32107
			// (set) Token: 0x0600AB4B RID: 43851 RVA: 0x000F7BC6 File Offset: 0x000F5DC6
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007D6C RID: 32108
			// (set) Token: 0x0600AB4C RID: 43852 RVA: 0x000F7BD9 File Offset: 0x000F5DD9
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007D6D RID: 32109
			// (set) Token: 0x0600AB4D RID: 43853 RVA: 0x000F7BEC File Offset: 0x000F5DEC
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007D6E RID: 32110
			// (set) Token: 0x0600AB4E RID: 43854 RVA: 0x000F7BFF File Offset: 0x000F5DFF
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007D6F RID: 32111
			// (set) Token: 0x0600AB4F RID: 43855 RVA: 0x000F7C17 File Offset: 0x000F5E17
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007D70 RID: 32112
			// (set) Token: 0x0600AB50 RID: 43856 RVA: 0x000F7C2A File Offset: 0x000F5E2A
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007D71 RID: 32113
			// (set) Token: 0x0600AB51 RID: 43857 RVA: 0x000F7C3D File Offset: 0x000F5E3D
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007D72 RID: 32114
			// (set) Token: 0x0600AB52 RID: 43858 RVA: 0x000F7C55 File Offset: 0x000F5E55
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007D73 RID: 32115
			// (set) Token: 0x0600AB53 RID: 43859 RVA: 0x000F7C68 File Offset: 0x000F5E68
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007D74 RID: 32116
			// (set) Token: 0x0600AB54 RID: 43860 RVA: 0x000F7C80 File Offset: 0x000F5E80
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007D75 RID: 32117
			// (set) Token: 0x0600AB55 RID: 43861 RVA: 0x000F7C93 File Offset: 0x000F5E93
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007D76 RID: 32118
			// (set) Token: 0x0600AB56 RID: 43862 RVA: 0x000F7CB1 File Offset: 0x000F5EB1
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007D77 RID: 32119
			// (set) Token: 0x0600AB57 RID: 43863 RVA: 0x000F7CCF File Offset: 0x000F5ECF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007D78 RID: 32120
			// (set) Token: 0x0600AB58 RID: 43864 RVA: 0x000F7CE2 File Offset: 0x000F5EE2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007D79 RID: 32121
			// (set) Token: 0x0600AB59 RID: 43865 RVA: 0x000F7CFA File Offset: 0x000F5EFA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007D7A RID: 32122
			// (set) Token: 0x0600AB5A RID: 43866 RVA: 0x000F7D12 File Offset: 0x000F5F12
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007D7B RID: 32123
			// (set) Token: 0x0600AB5B RID: 43867 RVA: 0x000F7D2A File Offset: 0x000F5F2A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007D7C RID: 32124
			// (set) Token: 0x0600AB5C RID: 43868 RVA: 0x000F7D42 File Offset: 0x000F5F42
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CCA RID: 3274
		public class LinkedWithSyncMailboxParameters : ParametersBase
		{
			// Token: 0x17007D7D RID: 32125
			// (set) Token: 0x0600AB5E RID: 43870 RVA: 0x000F7D62 File Offset: 0x000F5F62
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17007D7E RID: 32126
			// (set) Token: 0x0600AB5F RID: 43871 RVA: 0x000F7D75 File Offset: 0x000F5F75
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17007D7F RID: 32127
			// (set) Token: 0x0600AB60 RID: 43872 RVA: 0x000F7D88 File Offset: 0x000F5F88
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007D80 RID: 32128
			// (set) Token: 0x0600AB61 RID: 43873 RVA: 0x000F7DA6 File Offset: 0x000F5FA6
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007D81 RID: 32129
			// (set) Token: 0x0600AB62 RID: 43874 RVA: 0x000F7DB9 File Offset: 0x000F5FB9
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17007D82 RID: 32130
			// (set) Token: 0x0600AB63 RID: 43875 RVA: 0x000F7DD1 File Offset: 0x000F5FD1
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17007D83 RID: 32131
			// (set) Token: 0x0600AB64 RID: 43876 RVA: 0x000F7DE9 File Offset: 0x000F5FE9
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007D84 RID: 32132
			// (set) Token: 0x0600AB65 RID: 43877 RVA: 0x000F7DFC File Offset: 0x000F5FFC
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007D85 RID: 32133
			// (set) Token: 0x0600AB66 RID: 43878 RVA: 0x000F7E14 File Offset: 0x000F6014
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007D86 RID: 32134
			// (set) Token: 0x0600AB67 RID: 43879 RVA: 0x000F7E27 File Offset: 0x000F6027
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007D87 RID: 32135
			// (set) Token: 0x0600AB68 RID: 43880 RVA: 0x000F7E3A File Offset: 0x000F603A
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007D88 RID: 32136
			// (set) Token: 0x0600AB69 RID: 43881 RVA: 0x000F7E58 File Offset: 0x000F6058
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007D89 RID: 32137
			// (set) Token: 0x0600AB6A RID: 43882 RVA: 0x000F7E76 File Offset: 0x000F6076
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007D8A RID: 32138
			// (set) Token: 0x0600AB6B RID: 43883 RVA: 0x000F7E94 File Offset: 0x000F6094
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007D8B RID: 32139
			// (set) Token: 0x0600AB6C RID: 43884 RVA: 0x000F7EB2 File Offset: 0x000F60B2
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007D8C RID: 32140
			// (set) Token: 0x0600AB6D RID: 43885 RVA: 0x000F7EC5 File Offset: 0x000F60C5
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007D8D RID: 32141
			// (set) Token: 0x0600AB6E RID: 43886 RVA: 0x000F7EDD File Offset: 0x000F60DD
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007D8E RID: 32142
			// (set) Token: 0x0600AB6F RID: 43887 RVA: 0x000F7EFB File Offset: 0x000F60FB
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007D8F RID: 32143
			// (set) Token: 0x0600AB70 RID: 43888 RVA: 0x000F7F13 File Offset: 0x000F6113
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007D90 RID: 32144
			// (set) Token: 0x0600AB71 RID: 43889 RVA: 0x000F7F2B File Offset: 0x000F612B
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007D91 RID: 32145
			// (set) Token: 0x0600AB72 RID: 43890 RVA: 0x000F7F3E File Offset: 0x000F613E
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007D92 RID: 32146
			// (set) Token: 0x0600AB73 RID: 43891 RVA: 0x000F7F56 File Offset: 0x000F6156
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007D93 RID: 32147
			// (set) Token: 0x0600AB74 RID: 43892 RVA: 0x000F7F69 File Offset: 0x000F6169
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007D94 RID: 32148
			// (set) Token: 0x0600AB75 RID: 43893 RVA: 0x000F7F7C File Offset: 0x000F617C
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007D95 RID: 32149
			// (set) Token: 0x0600AB76 RID: 43894 RVA: 0x000F7F8F File Offset: 0x000F618F
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007D96 RID: 32150
			// (set) Token: 0x0600AB77 RID: 43895 RVA: 0x000F7FA2 File Offset: 0x000F61A2
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007D97 RID: 32151
			// (set) Token: 0x0600AB78 RID: 43896 RVA: 0x000F7FBA File Offset: 0x000F61BA
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007D98 RID: 32152
			// (set) Token: 0x0600AB79 RID: 43897 RVA: 0x000F7FCD File Offset: 0x000F61CD
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007D99 RID: 32153
			// (set) Token: 0x0600AB7A RID: 43898 RVA: 0x000F7FE0 File Offset: 0x000F61E0
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007D9A RID: 32154
			// (set) Token: 0x0600AB7B RID: 43899 RVA: 0x000F7FF8 File Offset: 0x000F61F8
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007D9B RID: 32155
			// (set) Token: 0x0600AB7C RID: 43900 RVA: 0x000F800B File Offset: 0x000F620B
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007D9C RID: 32156
			// (set) Token: 0x0600AB7D RID: 43901 RVA: 0x000F8023 File Offset: 0x000F6223
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007D9D RID: 32157
			// (set) Token: 0x0600AB7E RID: 43902 RVA: 0x000F8036 File Offset: 0x000F6236
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007D9E RID: 32158
			// (set) Token: 0x0600AB7F RID: 43903 RVA: 0x000F8054 File Offset: 0x000F6254
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007D9F RID: 32159
			// (set) Token: 0x0600AB80 RID: 43904 RVA: 0x000F8072 File Offset: 0x000F6272
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007DA0 RID: 32160
			// (set) Token: 0x0600AB81 RID: 43905 RVA: 0x000F8085 File Offset: 0x000F6285
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007DA1 RID: 32161
			// (set) Token: 0x0600AB82 RID: 43906 RVA: 0x000F809D File Offset: 0x000F629D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007DA2 RID: 32162
			// (set) Token: 0x0600AB83 RID: 43907 RVA: 0x000F80B5 File Offset: 0x000F62B5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007DA3 RID: 32163
			// (set) Token: 0x0600AB84 RID: 43908 RVA: 0x000F80CD File Offset: 0x000F62CD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007DA4 RID: 32164
			// (set) Token: 0x0600AB85 RID: 43909 RVA: 0x000F80E5 File Offset: 0x000F62E5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CCB RID: 3275
		public class LinkedRoomMailboxParameters : ParametersBase
		{
			// Token: 0x17007DA5 RID: 32165
			// (set) Token: 0x0600AB87 RID: 43911 RVA: 0x000F8105 File Offset: 0x000F6305
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17007DA6 RID: 32166
			// (set) Token: 0x0600AB88 RID: 43912 RVA: 0x000F8118 File Offset: 0x000F6318
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17007DA7 RID: 32167
			// (set) Token: 0x0600AB89 RID: 43913 RVA: 0x000F812B File Offset: 0x000F632B
			public virtual SwitchParameter LinkedRoom
			{
				set
				{
					base.PowerSharpParameters["LinkedRoom"] = value;
				}
			}

			// Token: 0x17007DA8 RID: 32168
			// (set) Token: 0x0600AB8A RID: 43914 RVA: 0x000F8143 File Offset: 0x000F6343
			public virtual string LinkedMasterAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedMasterAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17007DA9 RID: 32169
			// (set) Token: 0x0600AB8B RID: 43915 RVA: 0x000F8161 File Offset: 0x000F6361
			public virtual string LinkedDomainController
			{
				set
				{
					base.PowerSharpParameters["LinkedDomainController"] = value;
				}
			}

			// Token: 0x17007DAA RID: 32170
			// (set) Token: 0x0600AB8C RID: 43916 RVA: 0x000F8174 File Offset: 0x000F6374
			public virtual PSCredential LinkedCredential
			{
				set
				{
					base.PowerSharpParameters["LinkedCredential"] = value;
				}
			}

			// Token: 0x17007DAB RID: 32171
			// (set) Token: 0x0600AB8D RID: 43917 RVA: 0x000F8187 File Offset: 0x000F6387
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007DAC RID: 32172
			// (set) Token: 0x0600AB8E RID: 43918 RVA: 0x000F81A5 File Offset: 0x000F63A5
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007DAD RID: 32173
			// (set) Token: 0x0600AB8F RID: 43919 RVA: 0x000F81B8 File Offset: 0x000F63B8
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17007DAE RID: 32174
			// (set) Token: 0x0600AB90 RID: 43920 RVA: 0x000F81D0 File Offset: 0x000F63D0
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17007DAF RID: 32175
			// (set) Token: 0x0600AB91 RID: 43921 RVA: 0x000F81E8 File Offset: 0x000F63E8
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007DB0 RID: 32176
			// (set) Token: 0x0600AB92 RID: 43922 RVA: 0x000F81FB File Offset: 0x000F63FB
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007DB1 RID: 32177
			// (set) Token: 0x0600AB93 RID: 43923 RVA: 0x000F8213 File Offset: 0x000F6413
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007DB2 RID: 32178
			// (set) Token: 0x0600AB94 RID: 43924 RVA: 0x000F8226 File Offset: 0x000F6426
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007DB3 RID: 32179
			// (set) Token: 0x0600AB95 RID: 43925 RVA: 0x000F8239 File Offset: 0x000F6439
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007DB4 RID: 32180
			// (set) Token: 0x0600AB96 RID: 43926 RVA: 0x000F8257 File Offset: 0x000F6457
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007DB5 RID: 32181
			// (set) Token: 0x0600AB97 RID: 43927 RVA: 0x000F8275 File Offset: 0x000F6475
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007DB6 RID: 32182
			// (set) Token: 0x0600AB98 RID: 43928 RVA: 0x000F8293 File Offset: 0x000F6493
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007DB7 RID: 32183
			// (set) Token: 0x0600AB99 RID: 43929 RVA: 0x000F82B1 File Offset: 0x000F64B1
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007DB8 RID: 32184
			// (set) Token: 0x0600AB9A RID: 43930 RVA: 0x000F82C4 File Offset: 0x000F64C4
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007DB9 RID: 32185
			// (set) Token: 0x0600AB9B RID: 43931 RVA: 0x000F82DC File Offset: 0x000F64DC
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007DBA RID: 32186
			// (set) Token: 0x0600AB9C RID: 43932 RVA: 0x000F82FA File Offset: 0x000F64FA
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007DBB RID: 32187
			// (set) Token: 0x0600AB9D RID: 43933 RVA: 0x000F8312 File Offset: 0x000F6512
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007DBC RID: 32188
			// (set) Token: 0x0600AB9E RID: 43934 RVA: 0x000F832A File Offset: 0x000F652A
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007DBD RID: 32189
			// (set) Token: 0x0600AB9F RID: 43935 RVA: 0x000F833D File Offset: 0x000F653D
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007DBE RID: 32190
			// (set) Token: 0x0600ABA0 RID: 43936 RVA: 0x000F8355 File Offset: 0x000F6555
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007DBF RID: 32191
			// (set) Token: 0x0600ABA1 RID: 43937 RVA: 0x000F8368 File Offset: 0x000F6568
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007DC0 RID: 32192
			// (set) Token: 0x0600ABA2 RID: 43938 RVA: 0x000F837B File Offset: 0x000F657B
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007DC1 RID: 32193
			// (set) Token: 0x0600ABA3 RID: 43939 RVA: 0x000F838E File Offset: 0x000F658E
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007DC2 RID: 32194
			// (set) Token: 0x0600ABA4 RID: 43940 RVA: 0x000F83A1 File Offset: 0x000F65A1
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007DC3 RID: 32195
			// (set) Token: 0x0600ABA5 RID: 43941 RVA: 0x000F83B9 File Offset: 0x000F65B9
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007DC4 RID: 32196
			// (set) Token: 0x0600ABA6 RID: 43942 RVA: 0x000F83CC File Offset: 0x000F65CC
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007DC5 RID: 32197
			// (set) Token: 0x0600ABA7 RID: 43943 RVA: 0x000F83DF File Offset: 0x000F65DF
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007DC6 RID: 32198
			// (set) Token: 0x0600ABA8 RID: 43944 RVA: 0x000F83F7 File Offset: 0x000F65F7
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007DC7 RID: 32199
			// (set) Token: 0x0600ABA9 RID: 43945 RVA: 0x000F840A File Offset: 0x000F660A
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007DC8 RID: 32200
			// (set) Token: 0x0600ABAA RID: 43946 RVA: 0x000F8422 File Offset: 0x000F6622
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007DC9 RID: 32201
			// (set) Token: 0x0600ABAB RID: 43947 RVA: 0x000F8435 File Offset: 0x000F6635
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007DCA RID: 32202
			// (set) Token: 0x0600ABAC RID: 43948 RVA: 0x000F8453 File Offset: 0x000F6653
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007DCB RID: 32203
			// (set) Token: 0x0600ABAD RID: 43949 RVA: 0x000F8471 File Offset: 0x000F6671
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007DCC RID: 32204
			// (set) Token: 0x0600ABAE RID: 43950 RVA: 0x000F8484 File Offset: 0x000F6684
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007DCD RID: 32205
			// (set) Token: 0x0600ABAF RID: 43951 RVA: 0x000F849C File Offset: 0x000F669C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007DCE RID: 32206
			// (set) Token: 0x0600ABB0 RID: 43952 RVA: 0x000F84B4 File Offset: 0x000F66B4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007DCF RID: 32207
			// (set) Token: 0x0600ABB1 RID: 43953 RVA: 0x000F84CC File Offset: 0x000F66CC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007DD0 RID: 32208
			// (set) Token: 0x0600ABB2 RID: 43954 RVA: 0x000F84E4 File Offset: 0x000F66E4
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CCC RID: 3276
		public class EnableRoomMailboxAccountParameters : ParametersBase
		{
			// Token: 0x17007DD1 RID: 32209
			// (set) Token: 0x0600ABB4 RID: 43956 RVA: 0x000F8504 File Offset: 0x000F6704
			public virtual SecureString RoomMailboxPassword
			{
				set
				{
					base.PowerSharpParameters["RoomMailboxPassword"] = value;
				}
			}

			// Token: 0x17007DD2 RID: 32210
			// (set) Token: 0x0600ABB5 RID: 43957 RVA: 0x000F8517 File Offset: 0x000F6717
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17007DD3 RID: 32211
			// (set) Token: 0x0600ABB6 RID: 43958 RVA: 0x000F852A File Offset: 0x000F672A
			public virtual SwitchParameter Room
			{
				set
				{
					base.PowerSharpParameters["Room"] = value;
				}
			}

			// Token: 0x17007DD4 RID: 32212
			// (set) Token: 0x0600ABB7 RID: 43959 RVA: 0x000F8542 File Offset: 0x000F6742
			public virtual bool EnableRoomMailboxAccount
			{
				set
				{
					base.PowerSharpParameters["EnableRoomMailboxAccount"] = value;
				}
			}

			// Token: 0x17007DD5 RID: 32213
			// (set) Token: 0x0600ABB8 RID: 43960 RVA: 0x000F855A File Offset: 0x000F675A
			public virtual WindowsLiveId MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x17007DD6 RID: 32214
			// (set) Token: 0x0600ABB9 RID: 43961 RVA: 0x000F856D File Offset: 0x000F676D
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007DD7 RID: 32215
			// (set) Token: 0x0600ABBA RID: 43962 RVA: 0x000F8580 File Offset: 0x000F6780
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007DD8 RID: 32216
			// (set) Token: 0x0600ABBB RID: 43963 RVA: 0x000F8598 File Offset: 0x000F6798
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007DD9 RID: 32217
			// (set) Token: 0x0600ABBC RID: 43964 RVA: 0x000F85AB File Offset: 0x000F67AB
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007DDA RID: 32218
			// (set) Token: 0x0600ABBD RID: 43965 RVA: 0x000F85BE File Offset: 0x000F67BE
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007DDB RID: 32219
			// (set) Token: 0x0600ABBE RID: 43966 RVA: 0x000F85DC File Offset: 0x000F67DC
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007DDC RID: 32220
			// (set) Token: 0x0600ABBF RID: 43967 RVA: 0x000F85FA File Offset: 0x000F67FA
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007DDD RID: 32221
			// (set) Token: 0x0600ABC0 RID: 43968 RVA: 0x000F8618 File Offset: 0x000F6818
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007DDE RID: 32222
			// (set) Token: 0x0600ABC1 RID: 43969 RVA: 0x000F8636 File Offset: 0x000F6836
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007DDF RID: 32223
			// (set) Token: 0x0600ABC2 RID: 43970 RVA: 0x000F8649 File Offset: 0x000F6849
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007DE0 RID: 32224
			// (set) Token: 0x0600ABC3 RID: 43971 RVA: 0x000F8661 File Offset: 0x000F6861
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007DE1 RID: 32225
			// (set) Token: 0x0600ABC4 RID: 43972 RVA: 0x000F867F File Offset: 0x000F687F
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007DE2 RID: 32226
			// (set) Token: 0x0600ABC5 RID: 43973 RVA: 0x000F8697 File Offset: 0x000F6897
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007DE3 RID: 32227
			// (set) Token: 0x0600ABC6 RID: 43974 RVA: 0x000F86AF File Offset: 0x000F68AF
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007DE4 RID: 32228
			// (set) Token: 0x0600ABC7 RID: 43975 RVA: 0x000F86C2 File Offset: 0x000F68C2
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007DE5 RID: 32229
			// (set) Token: 0x0600ABC8 RID: 43976 RVA: 0x000F86DA File Offset: 0x000F68DA
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007DE6 RID: 32230
			// (set) Token: 0x0600ABC9 RID: 43977 RVA: 0x000F86ED File Offset: 0x000F68ED
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007DE7 RID: 32231
			// (set) Token: 0x0600ABCA RID: 43978 RVA: 0x000F8700 File Offset: 0x000F6900
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007DE8 RID: 32232
			// (set) Token: 0x0600ABCB RID: 43979 RVA: 0x000F8713 File Offset: 0x000F6913
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007DE9 RID: 32233
			// (set) Token: 0x0600ABCC RID: 43980 RVA: 0x000F8726 File Offset: 0x000F6926
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007DEA RID: 32234
			// (set) Token: 0x0600ABCD RID: 43981 RVA: 0x000F873E File Offset: 0x000F693E
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007DEB RID: 32235
			// (set) Token: 0x0600ABCE RID: 43982 RVA: 0x000F8751 File Offset: 0x000F6951
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007DEC RID: 32236
			// (set) Token: 0x0600ABCF RID: 43983 RVA: 0x000F8764 File Offset: 0x000F6964
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007DED RID: 32237
			// (set) Token: 0x0600ABD0 RID: 43984 RVA: 0x000F877C File Offset: 0x000F697C
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007DEE RID: 32238
			// (set) Token: 0x0600ABD1 RID: 43985 RVA: 0x000F878F File Offset: 0x000F698F
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007DEF RID: 32239
			// (set) Token: 0x0600ABD2 RID: 43986 RVA: 0x000F87A7 File Offset: 0x000F69A7
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007DF0 RID: 32240
			// (set) Token: 0x0600ABD3 RID: 43987 RVA: 0x000F87BA File Offset: 0x000F69BA
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007DF1 RID: 32241
			// (set) Token: 0x0600ABD4 RID: 43988 RVA: 0x000F87D8 File Offset: 0x000F69D8
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007DF2 RID: 32242
			// (set) Token: 0x0600ABD5 RID: 43989 RVA: 0x000F87F6 File Offset: 0x000F69F6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007DF3 RID: 32243
			// (set) Token: 0x0600ABD6 RID: 43990 RVA: 0x000F8809 File Offset: 0x000F6A09
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007DF4 RID: 32244
			// (set) Token: 0x0600ABD7 RID: 43991 RVA: 0x000F8821 File Offset: 0x000F6A21
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007DF5 RID: 32245
			// (set) Token: 0x0600ABD8 RID: 43992 RVA: 0x000F8839 File Offset: 0x000F6A39
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007DF6 RID: 32246
			// (set) Token: 0x0600ABD9 RID: 43993 RVA: 0x000F8851 File Offset: 0x000F6A51
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007DF7 RID: 32247
			// (set) Token: 0x0600ABDA RID: 43994 RVA: 0x000F8869 File Offset: 0x000F6A69
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CCD RID: 3277
		public class AuditLogParameters : ParametersBase
		{
			// Token: 0x17007DF8 RID: 32248
			// (set) Token: 0x0600ABDC RID: 43996 RVA: 0x000F8889 File Offset: 0x000F6A89
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17007DF9 RID: 32249
			// (set) Token: 0x0600ABDD RID: 43997 RVA: 0x000F889C File Offset: 0x000F6A9C
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17007DFA RID: 32250
			// (set) Token: 0x0600ABDE RID: 43998 RVA: 0x000F88B4 File Offset: 0x000F6AB4
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007DFB RID: 32251
			// (set) Token: 0x0600ABDF RID: 43999 RVA: 0x000F88C7 File Offset: 0x000F6AC7
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007DFC RID: 32252
			// (set) Token: 0x0600ABE0 RID: 44000 RVA: 0x000F88DF File Offset: 0x000F6ADF
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007DFD RID: 32253
			// (set) Token: 0x0600ABE1 RID: 44001 RVA: 0x000F88F2 File Offset: 0x000F6AF2
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007DFE RID: 32254
			// (set) Token: 0x0600ABE2 RID: 44002 RVA: 0x000F8905 File Offset: 0x000F6B05
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007DFF RID: 32255
			// (set) Token: 0x0600ABE3 RID: 44003 RVA: 0x000F8923 File Offset: 0x000F6B23
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007E00 RID: 32256
			// (set) Token: 0x0600ABE4 RID: 44004 RVA: 0x000F8941 File Offset: 0x000F6B41
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007E01 RID: 32257
			// (set) Token: 0x0600ABE5 RID: 44005 RVA: 0x000F895F File Offset: 0x000F6B5F
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007E02 RID: 32258
			// (set) Token: 0x0600ABE6 RID: 44006 RVA: 0x000F897D File Offset: 0x000F6B7D
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007E03 RID: 32259
			// (set) Token: 0x0600ABE7 RID: 44007 RVA: 0x000F8990 File Offset: 0x000F6B90
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007E04 RID: 32260
			// (set) Token: 0x0600ABE8 RID: 44008 RVA: 0x000F89A8 File Offset: 0x000F6BA8
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007E05 RID: 32261
			// (set) Token: 0x0600ABE9 RID: 44009 RVA: 0x000F89C6 File Offset: 0x000F6BC6
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007E06 RID: 32262
			// (set) Token: 0x0600ABEA RID: 44010 RVA: 0x000F89DE File Offset: 0x000F6BDE
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007E07 RID: 32263
			// (set) Token: 0x0600ABEB RID: 44011 RVA: 0x000F89F6 File Offset: 0x000F6BF6
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007E08 RID: 32264
			// (set) Token: 0x0600ABEC RID: 44012 RVA: 0x000F8A09 File Offset: 0x000F6C09
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007E09 RID: 32265
			// (set) Token: 0x0600ABED RID: 44013 RVA: 0x000F8A21 File Offset: 0x000F6C21
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007E0A RID: 32266
			// (set) Token: 0x0600ABEE RID: 44014 RVA: 0x000F8A34 File Offset: 0x000F6C34
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007E0B RID: 32267
			// (set) Token: 0x0600ABEF RID: 44015 RVA: 0x000F8A47 File Offset: 0x000F6C47
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007E0C RID: 32268
			// (set) Token: 0x0600ABF0 RID: 44016 RVA: 0x000F8A5A File Offset: 0x000F6C5A
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007E0D RID: 32269
			// (set) Token: 0x0600ABF1 RID: 44017 RVA: 0x000F8A6D File Offset: 0x000F6C6D
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007E0E RID: 32270
			// (set) Token: 0x0600ABF2 RID: 44018 RVA: 0x000F8A85 File Offset: 0x000F6C85
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007E0F RID: 32271
			// (set) Token: 0x0600ABF3 RID: 44019 RVA: 0x000F8A98 File Offset: 0x000F6C98
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007E10 RID: 32272
			// (set) Token: 0x0600ABF4 RID: 44020 RVA: 0x000F8AAB File Offset: 0x000F6CAB
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007E11 RID: 32273
			// (set) Token: 0x0600ABF5 RID: 44021 RVA: 0x000F8AC3 File Offset: 0x000F6CC3
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007E12 RID: 32274
			// (set) Token: 0x0600ABF6 RID: 44022 RVA: 0x000F8AD6 File Offset: 0x000F6CD6
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007E13 RID: 32275
			// (set) Token: 0x0600ABF7 RID: 44023 RVA: 0x000F8AEE File Offset: 0x000F6CEE
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007E14 RID: 32276
			// (set) Token: 0x0600ABF8 RID: 44024 RVA: 0x000F8B01 File Offset: 0x000F6D01
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007E15 RID: 32277
			// (set) Token: 0x0600ABF9 RID: 44025 RVA: 0x000F8B1F File Offset: 0x000F6D1F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007E16 RID: 32278
			// (set) Token: 0x0600ABFA RID: 44026 RVA: 0x000F8B3D File Offset: 0x000F6D3D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007E17 RID: 32279
			// (set) Token: 0x0600ABFB RID: 44027 RVA: 0x000F8B50 File Offset: 0x000F6D50
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007E18 RID: 32280
			// (set) Token: 0x0600ABFC RID: 44028 RVA: 0x000F8B68 File Offset: 0x000F6D68
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007E19 RID: 32281
			// (set) Token: 0x0600ABFD RID: 44029 RVA: 0x000F8B80 File Offset: 0x000F6D80
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007E1A RID: 32282
			// (set) Token: 0x0600ABFE RID: 44030 RVA: 0x000F8B98 File Offset: 0x000F6D98
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007E1B RID: 32283
			// (set) Token: 0x0600ABFF RID: 44031 RVA: 0x000F8BB0 File Offset: 0x000F6DB0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CCE RID: 3278
		public class GroupMailboxParameters : ParametersBase
		{
			// Token: 0x17007E1C RID: 32284
			// (set) Token: 0x0600AC01 RID: 44033 RVA: 0x000F8BD0 File Offset: 0x000F6DD0
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007E1D RID: 32285
			// (set) Token: 0x0600AC02 RID: 44034 RVA: 0x000F8BEE File Offset: 0x000F6DEE
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007E1E RID: 32286
			// (set) Token: 0x0600AC03 RID: 44035 RVA: 0x000F8C01 File Offset: 0x000F6E01
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17007E1F RID: 32287
			// (set) Token: 0x0600AC04 RID: 44036 RVA: 0x000F8C19 File Offset: 0x000F6E19
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007E20 RID: 32288
			// (set) Token: 0x0600AC05 RID: 44037 RVA: 0x000F8C2C File Offset: 0x000F6E2C
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007E21 RID: 32289
			// (set) Token: 0x0600AC06 RID: 44038 RVA: 0x000F8C44 File Offset: 0x000F6E44
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007E22 RID: 32290
			// (set) Token: 0x0600AC07 RID: 44039 RVA: 0x000F8C57 File Offset: 0x000F6E57
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007E23 RID: 32291
			// (set) Token: 0x0600AC08 RID: 44040 RVA: 0x000F8C6A File Offset: 0x000F6E6A
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007E24 RID: 32292
			// (set) Token: 0x0600AC09 RID: 44041 RVA: 0x000F8C88 File Offset: 0x000F6E88
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007E25 RID: 32293
			// (set) Token: 0x0600AC0A RID: 44042 RVA: 0x000F8CA6 File Offset: 0x000F6EA6
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007E26 RID: 32294
			// (set) Token: 0x0600AC0B RID: 44043 RVA: 0x000F8CC4 File Offset: 0x000F6EC4
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007E27 RID: 32295
			// (set) Token: 0x0600AC0C RID: 44044 RVA: 0x000F8CE2 File Offset: 0x000F6EE2
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007E28 RID: 32296
			// (set) Token: 0x0600AC0D RID: 44045 RVA: 0x000F8CF5 File Offset: 0x000F6EF5
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007E29 RID: 32297
			// (set) Token: 0x0600AC0E RID: 44046 RVA: 0x000F8D0D File Offset: 0x000F6F0D
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007E2A RID: 32298
			// (set) Token: 0x0600AC0F RID: 44047 RVA: 0x000F8D2B File Offset: 0x000F6F2B
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007E2B RID: 32299
			// (set) Token: 0x0600AC10 RID: 44048 RVA: 0x000F8D43 File Offset: 0x000F6F43
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007E2C RID: 32300
			// (set) Token: 0x0600AC11 RID: 44049 RVA: 0x000F8D5B File Offset: 0x000F6F5B
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007E2D RID: 32301
			// (set) Token: 0x0600AC12 RID: 44050 RVA: 0x000F8D6E File Offset: 0x000F6F6E
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007E2E RID: 32302
			// (set) Token: 0x0600AC13 RID: 44051 RVA: 0x000F8D86 File Offset: 0x000F6F86
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007E2F RID: 32303
			// (set) Token: 0x0600AC14 RID: 44052 RVA: 0x000F8D99 File Offset: 0x000F6F99
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007E30 RID: 32304
			// (set) Token: 0x0600AC15 RID: 44053 RVA: 0x000F8DAC File Offset: 0x000F6FAC
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007E31 RID: 32305
			// (set) Token: 0x0600AC16 RID: 44054 RVA: 0x000F8DBF File Offset: 0x000F6FBF
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007E32 RID: 32306
			// (set) Token: 0x0600AC17 RID: 44055 RVA: 0x000F8DD2 File Offset: 0x000F6FD2
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007E33 RID: 32307
			// (set) Token: 0x0600AC18 RID: 44056 RVA: 0x000F8DEA File Offset: 0x000F6FEA
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007E34 RID: 32308
			// (set) Token: 0x0600AC19 RID: 44057 RVA: 0x000F8DFD File Offset: 0x000F6FFD
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007E35 RID: 32309
			// (set) Token: 0x0600AC1A RID: 44058 RVA: 0x000F8E10 File Offset: 0x000F7010
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007E36 RID: 32310
			// (set) Token: 0x0600AC1B RID: 44059 RVA: 0x000F8E28 File Offset: 0x000F7028
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007E37 RID: 32311
			// (set) Token: 0x0600AC1C RID: 44060 RVA: 0x000F8E3B File Offset: 0x000F703B
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007E38 RID: 32312
			// (set) Token: 0x0600AC1D RID: 44061 RVA: 0x000F8E53 File Offset: 0x000F7053
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007E39 RID: 32313
			// (set) Token: 0x0600AC1E RID: 44062 RVA: 0x000F8E66 File Offset: 0x000F7066
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007E3A RID: 32314
			// (set) Token: 0x0600AC1F RID: 44063 RVA: 0x000F8E84 File Offset: 0x000F7084
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007E3B RID: 32315
			// (set) Token: 0x0600AC20 RID: 44064 RVA: 0x000F8EA2 File Offset: 0x000F70A2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007E3C RID: 32316
			// (set) Token: 0x0600AC21 RID: 44065 RVA: 0x000F8EB5 File Offset: 0x000F70B5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007E3D RID: 32317
			// (set) Token: 0x0600AC22 RID: 44066 RVA: 0x000F8ECD File Offset: 0x000F70CD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007E3E RID: 32318
			// (set) Token: 0x0600AC23 RID: 44067 RVA: 0x000F8EE5 File Offset: 0x000F70E5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007E3F RID: 32319
			// (set) Token: 0x0600AC24 RID: 44068 RVA: 0x000F8EFD File Offset: 0x000F70FD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007E40 RID: 32320
			// (set) Token: 0x0600AC25 RID: 44069 RVA: 0x000F8F15 File Offset: 0x000F7115
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CCF RID: 3279
		public class PublicFolderParameters : ParametersBase
		{
			// Token: 0x17007E41 RID: 32321
			// (set) Token: 0x0600AC27 RID: 44071 RVA: 0x000F8F35 File Offset: 0x000F7135
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x17007E42 RID: 32322
			// (set) Token: 0x0600AC28 RID: 44072 RVA: 0x000F8F4D File Offset: 0x000F714D
			public virtual bool IsExcludedFromServingHierarchy
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromServingHierarchy"] = value;
				}
			}

			// Token: 0x17007E43 RID: 32323
			// (set) Token: 0x0600AC29 RID: 44073 RVA: 0x000F8F65 File Offset: 0x000F7165
			public virtual SwitchParameter HoldForMigration
			{
				set
				{
					base.PowerSharpParameters["HoldForMigration"] = value;
				}
			}

			// Token: 0x17007E44 RID: 32324
			// (set) Token: 0x0600AC2A RID: 44074 RVA: 0x000F8F7D File Offset: 0x000F717D
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007E45 RID: 32325
			// (set) Token: 0x0600AC2B RID: 44075 RVA: 0x000F8F90 File Offset: 0x000F7190
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007E46 RID: 32326
			// (set) Token: 0x0600AC2C RID: 44076 RVA: 0x000F8FA8 File Offset: 0x000F71A8
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007E47 RID: 32327
			// (set) Token: 0x0600AC2D RID: 44077 RVA: 0x000F8FBB File Offset: 0x000F71BB
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007E48 RID: 32328
			// (set) Token: 0x0600AC2E RID: 44078 RVA: 0x000F8FCE File Offset: 0x000F71CE
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007E49 RID: 32329
			// (set) Token: 0x0600AC2F RID: 44079 RVA: 0x000F8FEC File Offset: 0x000F71EC
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007E4A RID: 32330
			// (set) Token: 0x0600AC30 RID: 44080 RVA: 0x000F900A File Offset: 0x000F720A
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007E4B RID: 32331
			// (set) Token: 0x0600AC31 RID: 44081 RVA: 0x000F9028 File Offset: 0x000F7228
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007E4C RID: 32332
			// (set) Token: 0x0600AC32 RID: 44082 RVA: 0x000F9046 File Offset: 0x000F7246
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007E4D RID: 32333
			// (set) Token: 0x0600AC33 RID: 44083 RVA: 0x000F9059 File Offset: 0x000F7259
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007E4E RID: 32334
			// (set) Token: 0x0600AC34 RID: 44084 RVA: 0x000F9071 File Offset: 0x000F7271
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007E4F RID: 32335
			// (set) Token: 0x0600AC35 RID: 44085 RVA: 0x000F908F File Offset: 0x000F728F
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007E50 RID: 32336
			// (set) Token: 0x0600AC36 RID: 44086 RVA: 0x000F90A7 File Offset: 0x000F72A7
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007E51 RID: 32337
			// (set) Token: 0x0600AC37 RID: 44087 RVA: 0x000F90BF File Offset: 0x000F72BF
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007E52 RID: 32338
			// (set) Token: 0x0600AC38 RID: 44088 RVA: 0x000F90D2 File Offset: 0x000F72D2
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007E53 RID: 32339
			// (set) Token: 0x0600AC39 RID: 44089 RVA: 0x000F90EA File Offset: 0x000F72EA
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007E54 RID: 32340
			// (set) Token: 0x0600AC3A RID: 44090 RVA: 0x000F90FD File Offset: 0x000F72FD
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007E55 RID: 32341
			// (set) Token: 0x0600AC3B RID: 44091 RVA: 0x000F9110 File Offset: 0x000F7310
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007E56 RID: 32342
			// (set) Token: 0x0600AC3C RID: 44092 RVA: 0x000F9123 File Offset: 0x000F7323
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007E57 RID: 32343
			// (set) Token: 0x0600AC3D RID: 44093 RVA: 0x000F9136 File Offset: 0x000F7336
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007E58 RID: 32344
			// (set) Token: 0x0600AC3E RID: 44094 RVA: 0x000F914E File Offset: 0x000F734E
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007E59 RID: 32345
			// (set) Token: 0x0600AC3F RID: 44095 RVA: 0x000F9161 File Offset: 0x000F7361
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007E5A RID: 32346
			// (set) Token: 0x0600AC40 RID: 44096 RVA: 0x000F9174 File Offset: 0x000F7374
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007E5B RID: 32347
			// (set) Token: 0x0600AC41 RID: 44097 RVA: 0x000F918C File Offset: 0x000F738C
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007E5C RID: 32348
			// (set) Token: 0x0600AC42 RID: 44098 RVA: 0x000F919F File Offset: 0x000F739F
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007E5D RID: 32349
			// (set) Token: 0x0600AC43 RID: 44099 RVA: 0x000F91B7 File Offset: 0x000F73B7
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007E5E RID: 32350
			// (set) Token: 0x0600AC44 RID: 44100 RVA: 0x000F91CA File Offset: 0x000F73CA
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007E5F RID: 32351
			// (set) Token: 0x0600AC45 RID: 44101 RVA: 0x000F91E8 File Offset: 0x000F73E8
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007E60 RID: 32352
			// (set) Token: 0x0600AC46 RID: 44102 RVA: 0x000F9206 File Offset: 0x000F7406
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007E61 RID: 32353
			// (set) Token: 0x0600AC47 RID: 44103 RVA: 0x000F9219 File Offset: 0x000F7419
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007E62 RID: 32354
			// (set) Token: 0x0600AC48 RID: 44104 RVA: 0x000F9231 File Offset: 0x000F7431
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007E63 RID: 32355
			// (set) Token: 0x0600AC49 RID: 44105 RVA: 0x000F9249 File Offset: 0x000F7449
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007E64 RID: 32356
			// (set) Token: 0x0600AC4A RID: 44106 RVA: 0x000F9261 File Offset: 0x000F7461
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007E65 RID: 32357
			// (set) Token: 0x0600AC4B RID: 44107 RVA: 0x000F9279 File Offset: 0x000F7479
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
