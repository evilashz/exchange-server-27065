using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001F6 RID: 502
	public class SetMailboxAssociationCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxAssociationPresentationObject>
	{
		// Token: 0x0600289C RID: 10396 RVA: 0x0004C7C4 File Offset: 0x0004A9C4
		private SetMailboxAssociationCommand() : base("Set-MailboxAssociation")
		{
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x0004C7D1 File Offset: 0x0004A9D1
		public SetMailboxAssociationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x0004C7E0 File Offset: 0x0004A9E0
		public virtual SetMailboxAssociationCommand SetParameters(SetMailboxAssociationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x0004C7EA File Offset: 0x0004A9EA
		public virtual SetMailboxAssociationCommand SetParameters(SetMailboxAssociationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001F7 RID: 503
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001069 RID: 4201
			// (set) Token: 0x060028A0 RID: 10400 RVA: 0x0004C7F4 File Offset: 0x0004A9F4
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxAssociationIdParameter(value) : null);
				}
			}

			// Token: 0x1700106A RID: 4202
			// (set) Token: 0x060028A1 RID: 10401 RVA: 0x0004C812 File Offset: 0x0004AA12
			public virtual SwitchParameter UpdateSlavedData
			{
				set
				{
					base.PowerSharpParameters["UpdateSlavedData"] = value;
				}
			}

			// Token: 0x1700106B RID: 4203
			// (set) Token: 0x060028A2 RID: 10402 RVA: 0x0004C82A File Offset: 0x0004AA2A
			public virtual SwitchParameter ReplicateMasteredData
			{
				set
				{
					base.PowerSharpParameters["ReplicateMasteredData"] = value;
				}
			}

			// Token: 0x1700106C RID: 4204
			// (set) Token: 0x060028A3 RID: 10403 RVA: 0x0004C842 File Offset: 0x0004AA42
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700106D RID: 4205
			// (set) Token: 0x060028A4 RID: 10404 RVA: 0x0004C85A File Offset: 0x0004AA5A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700106E RID: 4206
			// (set) Token: 0x060028A5 RID: 10405 RVA: 0x0004C86D File Offset: 0x0004AA6D
			public virtual string ExternalId
			{
				set
				{
					base.PowerSharpParameters["ExternalId"] = value;
				}
			}

			// Token: 0x1700106F RID: 4207
			// (set) Token: 0x060028A6 RID: 10406 RVA: 0x0004C880 File Offset: 0x0004AA80
			public virtual string LegacyDn
			{
				set
				{
					base.PowerSharpParameters["LegacyDn"] = value;
				}
			}

			// Token: 0x17001070 RID: 4208
			// (set) Token: 0x060028A7 RID: 10407 RVA: 0x0004C893 File Offset: 0x0004AA93
			public virtual bool IsMember
			{
				set
				{
					base.PowerSharpParameters["IsMember"] = value;
				}
			}

			// Token: 0x17001071 RID: 4209
			// (set) Token: 0x060028A8 RID: 10408 RVA: 0x0004C8AB File Offset: 0x0004AAAB
			public virtual string JoinedBy
			{
				set
				{
					base.PowerSharpParameters["JoinedBy"] = value;
				}
			}

			// Token: 0x17001072 RID: 4210
			// (set) Token: 0x060028A9 RID: 10409 RVA: 0x0004C8BE File Offset: 0x0004AABE
			public virtual SmtpAddress GroupSmtpAddress
			{
				set
				{
					base.PowerSharpParameters["GroupSmtpAddress"] = value;
				}
			}

			// Token: 0x17001073 RID: 4211
			// (set) Token: 0x060028AA RID: 10410 RVA: 0x0004C8D6 File Offset: 0x0004AAD6
			public virtual SmtpAddress UserSmtpAddress
			{
				set
				{
					base.PowerSharpParameters["UserSmtpAddress"] = value;
				}
			}

			// Token: 0x17001074 RID: 4212
			// (set) Token: 0x060028AB RID: 10411 RVA: 0x0004C8EE File Offset: 0x0004AAEE
			public virtual bool IsPin
			{
				set
				{
					base.PowerSharpParameters["IsPin"] = value;
				}
			}

			// Token: 0x17001075 RID: 4213
			// (set) Token: 0x060028AC RID: 10412 RVA: 0x0004C906 File Offset: 0x0004AB06
			public virtual bool ShouldEscalate
			{
				set
				{
					base.PowerSharpParameters["ShouldEscalate"] = value;
				}
			}

			// Token: 0x17001076 RID: 4214
			// (set) Token: 0x060028AD RID: 10413 RVA: 0x0004C91E File Offset: 0x0004AB1E
			public virtual bool IsAutoSubscribed
			{
				set
				{
					base.PowerSharpParameters["IsAutoSubscribed"] = value;
				}
			}

			// Token: 0x17001077 RID: 4215
			// (set) Token: 0x060028AE RID: 10414 RVA: 0x0004C936 File Offset: 0x0004AB36
			public virtual ExDateTime JoinDate
			{
				set
				{
					base.PowerSharpParameters["JoinDate"] = value;
				}
			}

			// Token: 0x17001078 RID: 4216
			// (set) Token: 0x060028AF RID: 10415 RVA: 0x0004C94E File Offset: 0x0004AB4E
			public virtual ExDateTime LastVisitedDate
			{
				set
				{
					base.PowerSharpParameters["LastVisitedDate"] = value;
				}
			}

			// Token: 0x17001079 RID: 4217
			// (set) Token: 0x060028B0 RID: 10416 RVA: 0x0004C966 File Offset: 0x0004AB66
			public virtual ExDateTime PinDate
			{
				set
				{
					base.PowerSharpParameters["PinDate"] = value;
				}
			}

			// Token: 0x1700107A RID: 4218
			// (set) Token: 0x060028B1 RID: 10417 RVA: 0x0004C97E File Offset: 0x0004AB7E
			public virtual int CurrentVersion
			{
				set
				{
					base.PowerSharpParameters["CurrentVersion"] = value;
				}
			}

			// Token: 0x1700107B RID: 4219
			// (set) Token: 0x060028B2 RID: 10418 RVA: 0x0004C996 File Offset: 0x0004AB96
			public virtual int SyncedVersion
			{
				set
				{
					base.PowerSharpParameters["SyncedVersion"] = value;
				}
			}

			// Token: 0x1700107C RID: 4220
			// (set) Token: 0x060028B3 RID: 10419 RVA: 0x0004C9AE File Offset: 0x0004ABAE
			public virtual string LastSyncError
			{
				set
				{
					base.PowerSharpParameters["LastSyncError"] = value;
				}
			}

			// Token: 0x1700107D RID: 4221
			// (set) Token: 0x060028B4 RID: 10420 RVA: 0x0004C9C1 File Offset: 0x0004ABC1
			public virtual int SyncAttempts
			{
				set
				{
					base.PowerSharpParameters["SyncAttempts"] = value;
				}
			}

			// Token: 0x1700107E RID: 4222
			// (set) Token: 0x060028B5 RID: 10421 RVA: 0x0004C9D9 File Offset: 0x0004ABD9
			public virtual string SyncedSchemaVersion
			{
				set
				{
					base.PowerSharpParameters["SyncedSchemaVersion"] = value;
				}
			}

			// Token: 0x1700107F RID: 4223
			// (set) Token: 0x060028B6 RID: 10422 RVA: 0x0004C9EC File Offset: 0x0004ABEC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001080 RID: 4224
			// (set) Token: 0x060028B7 RID: 10423 RVA: 0x0004CA04 File Offset: 0x0004AC04
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001081 RID: 4225
			// (set) Token: 0x060028B8 RID: 10424 RVA: 0x0004CA1C File Offset: 0x0004AC1C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001082 RID: 4226
			// (set) Token: 0x060028B9 RID: 10425 RVA: 0x0004CA34 File Offset: 0x0004AC34
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001083 RID: 4227
			// (set) Token: 0x060028BA RID: 10426 RVA: 0x0004CA4C File Offset: 0x0004AC4C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020001F8 RID: 504
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001084 RID: 4228
			// (set) Token: 0x060028BC RID: 10428 RVA: 0x0004CA6C File Offset: 0x0004AC6C
			public virtual SwitchParameter UpdateSlavedData
			{
				set
				{
					base.PowerSharpParameters["UpdateSlavedData"] = value;
				}
			}

			// Token: 0x17001085 RID: 4229
			// (set) Token: 0x060028BD RID: 10429 RVA: 0x0004CA84 File Offset: 0x0004AC84
			public virtual SwitchParameter ReplicateMasteredData
			{
				set
				{
					base.PowerSharpParameters["ReplicateMasteredData"] = value;
				}
			}

			// Token: 0x17001086 RID: 4230
			// (set) Token: 0x060028BE RID: 10430 RVA: 0x0004CA9C File Offset: 0x0004AC9C
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17001087 RID: 4231
			// (set) Token: 0x060028BF RID: 10431 RVA: 0x0004CAB4 File Offset: 0x0004ACB4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001088 RID: 4232
			// (set) Token: 0x060028C0 RID: 10432 RVA: 0x0004CAC7 File Offset: 0x0004ACC7
			public virtual string ExternalId
			{
				set
				{
					base.PowerSharpParameters["ExternalId"] = value;
				}
			}

			// Token: 0x17001089 RID: 4233
			// (set) Token: 0x060028C1 RID: 10433 RVA: 0x0004CADA File Offset: 0x0004ACDA
			public virtual string LegacyDn
			{
				set
				{
					base.PowerSharpParameters["LegacyDn"] = value;
				}
			}

			// Token: 0x1700108A RID: 4234
			// (set) Token: 0x060028C2 RID: 10434 RVA: 0x0004CAED File Offset: 0x0004ACED
			public virtual bool IsMember
			{
				set
				{
					base.PowerSharpParameters["IsMember"] = value;
				}
			}

			// Token: 0x1700108B RID: 4235
			// (set) Token: 0x060028C3 RID: 10435 RVA: 0x0004CB05 File Offset: 0x0004AD05
			public virtual string JoinedBy
			{
				set
				{
					base.PowerSharpParameters["JoinedBy"] = value;
				}
			}

			// Token: 0x1700108C RID: 4236
			// (set) Token: 0x060028C4 RID: 10436 RVA: 0x0004CB18 File Offset: 0x0004AD18
			public virtual SmtpAddress GroupSmtpAddress
			{
				set
				{
					base.PowerSharpParameters["GroupSmtpAddress"] = value;
				}
			}

			// Token: 0x1700108D RID: 4237
			// (set) Token: 0x060028C5 RID: 10437 RVA: 0x0004CB30 File Offset: 0x0004AD30
			public virtual SmtpAddress UserSmtpAddress
			{
				set
				{
					base.PowerSharpParameters["UserSmtpAddress"] = value;
				}
			}

			// Token: 0x1700108E RID: 4238
			// (set) Token: 0x060028C6 RID: 10438 RVA: 0x0004CB48 File Offset: 0x0004AD48
			public virtual bool IsPin
			{
				set
				{
					base.PowerSharpParameters["IsPin"] = value;
				}
			}

			// Token: 0x1700108F RID: 4239
			// (set) Token: 0x060028C7 RID: 10439 RVA: 0x0004CB60 File Offset: 0x0004AD60
			public virtual bool ShouldEscalate
			{
				set
				{
					base.PowerSharpParameters["ShouldEscalate"] = value;
				}
			}

			// Token: 0x17001090 RID: 4240
			// (set) Token: 0x060028C8 RID: 10440 RVA: 0x0004CB78 File Offset: 0x0004AD78
			public virtual bool IsAutoSubscribed
			{
				set
				{
					base.PowerSharpParameters["IsAutoSubscribed"] = value;
				}
			}

			// Token: 0x17001091 RID: 4241
			// (set) Token: 0x060028C9 RID: 10441 RVA: 0x0004CB90 File Offset: 0x0004AD90
			public virtual ExDateTime JoinDate
			{
				set
				{
					base.PowerSharpParameters["JoinDate"] = value;
				}
			}

			// Token: 0x17001092 RID: 4242
			// (set) Token: 0x060028CA RID: 10442 RVA: 0x0004CBA8 File Offset: 0x0004ADA8
			public virtual ExDateTime LastVisitedDate
			{
				set
				{
					base.PowerSharpParameters["LastVisitedDate"] = value;
				}
			}

			// Token: 0x17001093 RID: 4243
			// (set) Token: 0x060028CB RID: 10443 RVA: 0x0004CBC0 File Offset: 0x0004ADC0
			public virtual ExDateTime PinDate
			{
				set
				{
					base.PowerSharpParameters["PinDate"] = value;
				}
			}

			// Token: 0x17001094 RID: 4244
			// (set) Token: 0x060028CC RID: 10444 RVA: 0x0004CBD8 File Offset: 0x0004ADD8
			public virtual int CurrentVersion
			{
				set
				{
					base.PowerSharpParameters["CurrentVersion"] = value;
				}
			}

			// Token: 0x17001095 RID: 4245
			// (set) Token: 0x060028CD RID: 10445 RVA: 0x0004CBF0 File Offset: 0x0004ADF0
			public virtual int SyncedVersion
			{
				set
				{
					base.PowerSharpParameters["SyncedVersion"] = value;
				}
			}

			// Token: 0x17001096 RID: 4246
			// (set) Token: 0x060028CE RID: 10446 RVA: 0x0004CC08 File Offset: 0x0004AE08
			public virtual string LastSyncError
			{
				set
				{
					base.PowerSharpParameters["LastSyncError"] = value;
				}
			}

			// Token: 0x17001097 RID: 4247
			// (set) Token: 0x060028CF RID: 10447 RVA: 0x0004CC1B File Offset: 0x0004AE1B
			public virtual int SyncAttempts
			{
				set
				{
					base.PowerSharpParameters["SyncAttempts"] = value;
				}
			}

			// Token: 0x17001098 RID: 4248
			// (set) Token: 0x060028D0 RID: 10448 RVA: 0x0004CC33 File Offset: 0x0004AE33
			public virtual string SyncedSchemaVersion
			{
				set
				{
					base.PowerSharpParameters["SyncedSchemaVersion"] = value;
				}
			}

			// Token: 0x17001099 RID: 4249
			// (set) Token: 0x060028D1 RID: 10449 RVA: 0x0004CC46 File Offset: 0x0004AE46
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700109A RID: 4250
			// (set) Token: 0x060028D2 RID: 10450 RVA: 0x0004CC5E File Offset: 0x0004AE5E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700109B RID: 4251
			// (set) Token: 0x060028D3 RID: 10451 RVA: 0x0004CC76 File Offset: 0x0004AE76
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700109C RID: 4252
			// (set) Token: 0x060028D4 RID: 10452 RVA: 0x0004CC8E File Offset: 0x0004AE8E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700109D RID: 4253
			// (set) Token: 0x060028D5 RID: 10453 RVA: 0x0004CCA6 File Offset: 0x0004AEA6
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
