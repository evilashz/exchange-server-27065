using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D50 RID: 3408
	public class EnableRemoteMailboxCommand : SyntheticCommandWithPipelineInputNoOutput<SwitchParameter>
	{
		// Token: 0x0600B430 RID: 46128 RVA: 0x001038D6 File Offset: 0x00101AD6
		private EnableRemoteMailboxCommand() : base("Enable-RemoteMailbox")
		{
		}

		// Token: 0x0600B431 RID: 46129 RVA: 0x001038E3 File Offset: 0x00101AE3
		public EnableRemoteMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B432 RID: 46130 RVA: 0x001038F2 File Offset: 0x00101AF2
		public virtual EnableRemoteMailboxCommand SetParameters(EnableRemoteMailboxCommand.SharedParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B433 RID: 46131 RVA: 0x001038FC File Offset: 0x00101AFC
		public virtual EnableRemoteMailboxCommand SetParameters(EnableRemoteMailboxCommand.RoomParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B434 RID: 46132 RVA: 0x00103906 File Offset: 0x00101B06
		public virtual EnableRemoteMailboxCommand SetParameters(EnableRemoteMailboxCommand.EnabledUserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B435 RID: 46133 RVA: 0x00103910 File Offset: 0x00101B10
		public virtual EnableRemoteMailboxCommand SetParameters(EnableRemoteMailboxCommand.EquipmentParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B436 RID: 46134 RVA: 0x0010391A File Offset: 0x00101B1A
		public virtual EnableRemoteMailboxCommand SetParameters(EnableRemoteMailboxCommand.ArchiveParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B437 RID: 46135 RVA: 0x00103924 File Offset: 0x00101B24
		public virtual EnableRemoteMailboxCommand SetParameters(EnableRemoteMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D51 RID: 3409
		public class SharedParameters : ParametersBase
		{
			// Token: 0x17008549 RID: 34121
			// (set) Token: 0x0600B438 RID: 46136 RVA: 0x0010392E File Offset: 0x00101B2E
			public virtual ProxyAddress RemoteRoutingAddress
			{
				set
				{
					base.PowerSharpParameters["RemoteRoutingAddress"] = value;
				}
			}

			// Token: 0x1700854A RID: 34122
			// (set) Token: 0x0600B439 RID: 46137 RVA: 0x00103941 File Offset: 0x00101B41
			public virtual SwitchParameter Shared
			{
				set
				{
					base.PowerSharpParameters["Shared"] = value;
				}
			}

			// Token: 0x1700854B RID: 34123
			// (set) Token: 0x0600B43A RID: 46138 RVA: 0x00103959 File Offset: 0x00101B59
			public virtual SwitchParameter ACLableSyncedObjectEnabled
			{
				set
				{
					base.PowerSharpParameters["ACLableSyncedObjectEnabled"] = value;
				}
			}

			// Token: 0x1700854C RID: 34124
			// (set) Token: 0x0600B43B RID: 46139 RVA: 0x00103971 File Offset: 0x00101B71
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x1700854D RID: 34125
			// (set) Token: 0x0600B43C RID: 46140 RVA: 0x0010398F File Offset: 0x00101B8F
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700854E RID: 34126
			// (set) Token: 0x0600B43D RID: 46141 RVA: 0x001039A2 File Offset: 0x00101BA2
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700854F RID: 34127
			// (set) Token: 0x0600B43E RID: 46142 RVA: 0x001039B5 File Offset: 0x00101BB5
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008550 RID: 34128
			// (set) Token: 0x0600B43F RID: 46143 RVA: 0x001039CD File Offset: 0x00101BCD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008551 RID: 34129
			// (set) Token: 0x0600B440 RID: 46144 RVA: 0x001039E0 File Offset: 0x00101BE0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008552 RID: 34130
			// (set) Token: 0x0600B441 RID: 46145 RVA: 0x001039F8 File Offset: 0x00101BF8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008553 RID: 34131
			// (set) Token: 0x0600B442 RID: 46146 RVA: 0x00103A10 File Offset: 0x00101C10
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008554 RID: 34132
			// (set) Token: 0x0600B443 RID: 46147 RVA: 0x00103A28 File Offset: 0x00101C28
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008555 RID: 34133
			// (set) Token: 0x0600B444 RID: 46148 RVA: 0x00103A40 File Offset: 0x00101C40
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D52 RID: 3410
		public class RoomParameters : ParametersBase
		{
			// Token: 0x17008556 RID: 34134
			// (set) Token: 0x0600B446 RID: 46150 RVA: 0x00103A60 File Offset: 0x00101C60
			public virtual ProxyAddress RemoteRoutingAddress
			{
				set
				{
					base.PowerSharpParameters["RemoteRoutingAddress"] = value;
				}
			}

			// Token: 0x17008557 RID: 34135
			// (set) Token: 0x0600B447 RID: 46151 RVA: 0x00103A73 File Offset: 0x00101C73
			public virtual SwitchParameter Room
			{
				set
				{
					base.PowerSharpParameters["Room"] = value;
				}
			}

			// Token: 0x17008558 RID: 34136
			// (set) Token: 0x0600B448 RID: 46152 RVA: 0x00103A8B File Offset: 0x00101C8B
			public virtual SwitchParameter ACLableSyncedObjectEnabled
			{
				set
				{
					base.PowerSharpParameters["ACLableSyncedObjectEnabled"] = value;
				}
			}

			// Token: 0x17008559 RID: 34137
			// (set) Token: 0x0600B449 RID: 46153 RVA: 0x00103AA3 File Offset: 0x00101CA3
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x1700855A RID: 34138
			// (set) Token: 0x0600B44A RID: 46154 RVA: 0x00103AC1 File Offset: 0x00101CC1
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700855B RID: 34139
			// (set) Token: 0x0600B44B RID: 46155 RVA: 0x00103AD4 File Offset: 0x00101CD4
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700855C RID: 34140
			// (set) Token: 0x0600B44C RID: 46156 RVA: 0x00103AE7 File Offset: 0x00101CE7
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700855D RID: 34141
			// (set) Token: 0x0600B44D RID: 46157 RVA: 0x00103AFF File Offset: 0x00101CFF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700855E RID: 34142
			// (set) Token: 0x0600B44E RID: 46158 RVA: 0x00103B12 File Offset: 0x00101D12
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700855F RID: 34143
			// (set) Token: 0x0600B44F RID: 46159 RVA: 0x00103B2A File Offset: 0x00101D2A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008560 RID: 34144
			// (set) Token: 0x0600B450 RID: 46160 RVA: 0x00103B42 File Offset: 0x00101D42
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008561 RID: 34145
			// (set) Token: 0x0600B451 RID: 46161 RVA: 0x00103B5A File Offset: 0x00101D5A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008562 RID: 34146
			// (set) Token: 0x0600B452 RID: 46162 RVA: 0x00103B72 File Offset: 0x00101D72
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D53 RID: 3411
		public class EnabledUserParameters : ParametersBase
		{
			// Token: 0x17008563 RID: 34147
			// (set) Token: 0x0600B454 RID: 46164 RVA: 0x00103B92 File Offset: 0x00101D92
			public virtual ProxyAddress RemoteRoutingAddress
			{
				set
				{
					base.PowerSharpParameters["RemoteRoutingAddress"] = value;
				}
			}

			// Token: 0x17008564 RID: 34148
			// (set) Token: 0x0600B455 RID: 46165 RVA: 0x00103BA5 File Offset: 0x00101DA5
			public virtual SwitchParameter ACLableSyncedObjectEnabled
			{
				set
				{
					base.PowerSharpParameters["ACLableSyncedObjectEnabled"] = value;
				}
			}

			// Token: 0x17008565 RID: 34149
			// (set) Token: 0x0600B456 RID: 46166 RVA: 0x00103BBD File Offset: 0x00101DBD
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17008566 RID: 34150
			// (set) Token: 0x0600B457 RID: 46167 RVA: 0x00103BDB File Offset: 0x00101DDB
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008567 RID: 34151
			// (set) Token: 0x0600B458 RID: 46168 RVA: 0x00103BEE File Offset: 0x00101DEE
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008568 RID: 34152
			// (set) Token: 0x0600B459 RID: 46169 RVA: 0x00103C01 File Offset: 0x00101E01
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008569 RID: 34153
			// (set) Token: 0x0600B45A RID: 46170 RVA: 0x00103C19 File Offset: 0x00101E19
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700856A RID: 34154
			// (set) Token: 0x0600B45B RID: 46171 RVA: 0x00103C2C File Offset: 0x00101E2C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700856B RID: 34155
			// (set) Token: 0x0600B45C RID: 46172 RVA: 0x00103C44 File Offset: 0x00101E44
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700856C RID: 34156
			// (set) Token: 0x0600B45D RID: 46173 RVA: 0x00103C5C File Offset: 0x00101E5C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700856D RID: 34157
			// (set) Token: 0x0600B45E RID: 46174 RVA: 0x00103C74 File Offset: 0x00101E74
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700856E RID: 34158
			// (set) Token: 0x0600B45F RID: 46175 RVA: 0x00103C8C File Offset: 0x00101E8C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D54 RID: 3412
		public class EquipmentParameters : ParametersBase
		{
			// Token: 0x1700856F RID: 34159
			// (set) Token: 0x0600B461 RID: 46177 RVA: 0x00103CAC File Offset: 0x00101EAC
			public virtual ProxyAddress RemoteRoutingAddress
			{
				set
				{
					base.PowerSharpParameters["RemoteRoutingAddress"] = value;
				}
			}

			// Token: 0x17008570 RID: 34160
			// (set) Token: 0x0600B462 RID: 46178 RVA: 0x00103CBF File Offset: 0x00101EBF
			public virtual SwitchParameter Equipment
			{
				set
				{
					base.PowerSharpParameters["Equipment"] = value;
				}
			}

			// Token: 0x17008571 RID: 34161
			// (set) Token: 0x0600B463 RID: 46179 RVA: 0x00103CD7 File Offset: 0x00101ED7
			public virtual SwitchParameter ACLableSyncedObjectEnabled
			{
				set
				{
					base.PowerSharpParameters["ACLableSyncedObjectEnabled"] = value;
				}
			}

			// Token: 0x17008572 RID: 34162
			// (set) Token: 0x0600B464 RID: 46180 RVA: 0x00103CEF File Offset: 0x00101EEF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17008573 RID: 34163
			// (set) Token: 0x0600B465 RID: 46181 RVA: 0x00103D0D File Offset: 0x00101F0D
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008574 RID: 34164
			// (set) Token: 0x0600B466 RID: 46182 RVA: 0x00103D20 File Offset: 0x00101F20
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008575 RID: 34165
			// (set) Token: 0x0600B467 RID: 46183 RVA: 0x00103D33 File Offset: 0x00101F33
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008576 RID: 34166
			// (set) Token: 0x0600B468 RID: 46184 RVA: 0x00103D4B File Offset: 0x00101F4B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008577 RID: 34167
			// (set) Token: 0x0600B469 RID: 46185 RVA: 0x00103D5E File Offset: 0x00101F5E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008578 RID: 34168
			// (set) Token: 0x0600B46A RID: 46186 RVA: 0x00103D76 File Offset: 0x00101F76
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008579 RID: 34169
			// (set) Token: 0x0600B46B RID: 46187 RVA: 0x00103D8E File Offset: 0x00101F8E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700857A RID: 34170
			// (set) Token: 0x0600B46C RID: 46188 RVA: 0x00103DA6 File Offset: 0x00101FA6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700857B RID: 34171
			// (set) Token: 0x0600B46D RID: 46189 RVA: 0x00103DBE File Offset: 0x00101FBE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D55 RID: 3413
		public class ArchiveParameters : ParametersBase
		{
			// Token: 0x1700857C RID: 34172
			// (set) Token: 0x0600B46F RID: 46191 RVA: 0x00103DDE File Offset: 0x00101FDE
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x1700857D RID: 34173
			// (set) Token: 0x0600B470 RID: 46192 RVA: 0x00103DF6 File Offset: 0x00101FF6
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x1700857E RID: 34174
			// (set) Token: 0x0600B471 RID: 46193 RVA: 0x00103E09 File Offset: 0x00102009
			public virtual SwitchParameter ACLableSyncedObjectEnabled
			{
				set
				{
					base.PowerSharpParameters["ACLableSyncedObjectEnabled"] = value;
				}
			}

			// Token: 0x1700857F RID: 34175
			// (set) Token: 0x0600B472 RID: 46194 RVA: 0x00103E21 File Offset: 0x00102021
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17008580 RID: 34176
			// (set) Token: 0x0600B473 RID: 46195 RVA: 0x00103E3F File Offset: 0x0010203F
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008581 RID: 34177
			// (set) Token: 0x0600B474 RID: 46196 RVA: 0x00103E52 File Offset: 0x00102052
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008582 RID: 34178
			// (set) Token: 0x0600B475 RID: 46197 RVA: 0x00103E65 File Offset: 0x00102065
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008583 RID: 34179
			// (set) Token: 0x0600B476 RID: 46198 RVA: 0x00103E7D File Offset: 0x0010207D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008584 RID: 34180
			// (set) Token: 0x0600B477 RID: 46199 RVA: 0x00103E90 File Offset: 0x00102090
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008585 RID: 34181
			// (set) Token: 0x0600B478 RID: 46200 RVA: 0x00103EA8 File Offset: 0x001020A8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008586 RID: 34182
			// (set) Token: 0x0600B479 RID: 46201 RVA: 0x00103EC0 File Offset: 0x001020C0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008587 RID: 34183
			// (set) Token: 0x0600B47A RID: 46202 RVA: 0x00103ED8 File Offset: 0x001020D8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008588 RID: 34184
			// (set) Token: 0x0600B47B RID: 46203 RVA: 0x00103EF0 File Offset: 0x001020F0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D56 RID: 3414
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17008589 RID: 34185
			// (set) Token: 0x0600B47D RID: 46205 RVA: 0x00103F10 File Offset: 0x00102110
			public virtual SwitchParameter ACLableSyncedObjectEnabled
			{
				set
				{
					base.PowerSharpParameters["ACLableSyncedObjectEnabled"] = value;
				}
			}

			// Token: 0x1700858A RID: 34186
			// (set) Token: 0x0600B47E RID: 46206 RVA: 0x00103F28 File Offset: 0x00102128
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x1700858B RID: 34187
			// (set) Token: 0x0600B47F RID: 46207 RVA: 0x00103F46 File Offset: 0x00102146
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700858C RID: 34188
			// (set) Token: 0x0600B480 RID: 46208 RVA: 0x00103F59 File Offset: 0x00102159
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700858D RID: 34189
			// (set) Token: 0x0600B481 RID: 46209 RVA: 0x00103F6C File Offset: 0x0010216C
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700858E RID: 34190
			// (set) Token: 0x0600B482 RID: 46210 RVA: 0x00103F84 File Offset: 0x00102184
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700858F RID: 34191
			// (set) Token: 0x0600B483 RID: 46211 RVA: 0x00103F97 File Offset: 0x00102197
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008590 RID: 34192
			// (set) Token: 0x0600B484 RID: 46212 RVA: 0x00103FAF File Offset: 0x001021AF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008591 RID: 34193
			// (set) Token: 0x0600B485 RID: 46213 RVA: 0x00103FC7 File Offset: 0x001021C7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008592 RID: 34194
			// (set) Token: 0x0600B486 RID: 46214 RVA: 0x00103FDF File Offset: 0x001021DF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008593 RID: 34195
			// (set) Token: 0x0600B487 RID: 46215 RVA: 0x00103FF7 File Offset: 0x001021F7
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
