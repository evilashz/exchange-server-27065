using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200002A RID: 42
	public class GetActiveSyncDeviceCommand : SyntheticCommandWithPipelineInput<ActiveSyncDevice, ActiveSyncDevice>
	{
		// Token: 0x0600158A RID: 5514 RVA: 0x00033AEE File Offset: 0x00031CEE
		private GetActiveSyncDeviceCommand() : base("Get-ActiveSyncDevice")
		{
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x00033AFB File Offset: 0x00031CFB
		public GetActiveSyncDeviceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x00033B0A File Offset: 0x00031D0A
		public virtual GetActiveSyncDeviceCommand SetParameters(GetActiveSyncDeviceCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x00033B14 File Offset: 0x00031D14
		public virtual GetActiveSyncDeviceCommand SetParameters(GetActiveSyncDeviceCommand.MailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x00033B1E File Offset: 0x00031D1E
		public virtual GetActiveSyncDeviceCommand SetParameters(GetActiveSyncDeviceCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200002B RID: 43
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170000EF RID: 239
			// (set) Token: 0x0600158F RID: 5519 RVA: 0x00033B28 File Offset: 0x00031D28
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170000F0 RID: 240
			// (set) Token: 0x06001590 RID: 5520 RVA: 0x00033B40 File Offset: 0x00031D40
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170000F1 RID: 241
			// (set) Token: 0x06001591 RID: 5521 RVA: 0x00033B5E File Offset: 0x00031D5E
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170000F2 RID: 242
			// (set) Token: 0x06001592 RID: 5522 RVA: 0x00033B71 File Offset: 0x00031D71
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170000F3 RID: 243
			// (set) Token: 0x06001593 RID: 5523 RVA: 0x00033B84 File Offset: 0x00031D84
			public virtual SwitchParameter Monitoring
			{
				set
				{
					base.PowerSharpParameters["Monitoring"] = value;
				}
			}

			// Token: 0x170000F4 RID: 244
			// (set) Token: 0x06001594 RID: 5524 RVA: 0x00033B9C File Offset: 0x00031D9C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170000F5 RID: 245
			// (set) Token: 0x06001595 RID: 5525 RVA: 0x00033BBA File Offset: 0x00031DBA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170000F6 RID: 246
			// (set) Token: 0x06001596 RID: 5526 RVA: 0x00033BCD File Offset: 0x00031DCD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170000F7 RID: 247
			// (set) Token: 0x06001597 RID: 5527 RVA: 0x00033BE5 File Offset: 0x00031DE5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170000F8 RID: 248
			// (set) Token: 0x06001598 RID: 5528 RVA: 0x00033BFD File Offset: 0x00031DFD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170000F9 RID: 249
			// (set) Token: 0x06001599 RID: 5529 RVA: 0x00033C15 File Offset: 0x00031E15
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200002C RID: 44
		public class MailboxParameters : ParametersBase
		{
			// Token: 0x170000FA RID: 250
			// (set) Token: 0x0600159B RID: 5531 RVA: 0x00033C35 File Offset: 0x00031E35
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170000FB RID: 251
			// (set) Token: 0x0600159C RID: 5532 RVA: 0x00033C53 File Offset: 0x00031E53
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170000FC RID: 252
			// (set) Token: 0x0600159D RID: 5533 RVA: 0x00033C6B File Offset: 0x00031E6B
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170000FD RID: 253
			// (set) Token: 0x0600159E RID: 5534 RVA: 0x00033C89 File Offset: 0x00031E89
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170000FE RID: 254
			// (set) Token: 0x0600159F RID: 5535 RVA: 0x00033C9C File Offset: 0x00031E9C
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170000FF RID: 255
			// (set) Token: 0x060015A0 RID: 5536 RVA: 0x00033CAF File Offset: 0x00031EAF
			public virtual SwitchParameter Monitoring
			{
				set
				{
					base.PowerSharpParameters["Monitoring"] = value;
				}
			}

			// Token: 0x17000100 RID: 256
			// (set) Token: 0x060015A1 RID: 5537 RVA: 0x00033CC7 File Offset: 0x00031EC7
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000101 RID: 257
			// (set) Token: 0x060015A2 RID: 5538 RVA: 0x00033CE5 File Offset: 0x00031EE5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000102 RID: 258
			// (set) Token: 0x060015A3 RID: 5539 RVA: 0x00033CF8 File Offset: 0x00031EF8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000103 RID: 259
			// (set) Token: 0x060015A4 RID: 5540 RVA: 0x00033D10 File Offset: 0x00031F10
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000104 RID: 260
			// (set) Token: 0x060015A5 RID: 5541 RVA: 0x00033D28 File Offset: 0x00031F28
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000105 RID: 261
			// (set) Token: 0x060015A6 RID: 5542 RVA: 0x00033D40 File Offset: 0x00031F40
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200002D RID: 45
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000106 RID: 262
			// (set) Token: 0x060015A8 RID: 5544 RVA: 0x00033D60 File Offset: 0x00031F60
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ActiveSyncDeviceIdParameter(value) : null);
				}
			}

			// Token: 0x17000107 RID: 263
			// (set) Token: 0x060015A9 RID: 5545 RVA: 0x00033D7E File Offset: 0x00031F7E
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17000108 RID: 264
			// (set) Token: 0x060015AA RID: 5546 RVA: 0x00033D96 File Offset: 0x00031F96
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17000109 RID: 265
			// (set) Token: 0x060015AB RID: 5547 RVA: 0x00033DB4 File Offset: 0x00031FB4
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700010A RID: 266
			// (set) Token: 0x060015AC RID: 5548 RVA: 0x00033DC7 File Offset: 0x00031FC7
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700010B RID: 267
			// (set) Token: 0x060015AD RID: 5549 RVA: 0x00033DDA File Offset: 0x00031FDA
			public virtual SwitchParameter Monitoring
			{
				set
				{
					base.PowerSharpParameters["Monitoring"] = value;
				}
			}

			// Token: 0x1700010C RID: 268
			// (set) Token: 0x060015AE RID: 5550 RVA: 0x00033DF2 File Offset: 0x00031FF2
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700010D RID: 269
			// (set) Token: 0x060015AF RID: 5551 RVA: 0x00033E10 File Offset: 0x00032010
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700010E RID: 270
			// (set) Token: 0x060015B0 RID: 5552 RVA: 0x00033E23 File Offset: 0x00032023
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700010F RID: 271
			// (set) Token: 0x060015B1 RID: 5553 RVA: 0x00033E3B File Offset: 0x0003203B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000110 RID: 272
			// (set) Token: 0x060015B2 RID: 5554 RVA: 0x00033E53 File Offset: 0x00032053
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000111 RID: 273
			// (set) Token: 0x060015B3 RID: 5555 RVA: 0x00033E6B File Offset: 0x0003206B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
