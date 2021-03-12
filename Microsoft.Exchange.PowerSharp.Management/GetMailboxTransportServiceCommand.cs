using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200063C RID: 1596
	public class GetMailboxTransportServiceCommand : SyntheticCommandWithPipelineInput<MailboxTransportServer, MailboxTransportServer>
	{
		// Token: 0x060050DA RID: 20698 RVA: 0x0007FFD7 File Offset: 0x0007E1D7
		private GetMailboxTransportServiceCommand() : base("Get-MailboxTransportService")
		{
		}

		// Token: 0x060050DB RID: 20699 RVA: 0x0007FFE4 File Offset: 0x0007E1E4
		public GetMailboxTransportServiceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060050DC RID: 20700 RVA: 0x0007FFF3 File Offset: 0x0007E1F3
		public virtual GetMailboxTransportServiceCommand SetParameters(GetMailboxTransportServiceCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060050DD RID: 20701 RVA: 0x0007FFFD File Offset: 0x0007E1FD
		public virtual GetMailboxTransportServiceCommand SetParameters(GetMailboxTransportServiceCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200063D RID: 1597
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700301B RID: 12315
			// (set) Token: 0x060050DE RID: 20702 RVA: 0x00080007 File Offset: 0x0007E207
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700301C RID: 12316
			// (set) Token: 0x060050DF RID: 20703 RVA: 0x0008001A File Offset: 0x0007E21A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700301D RID: 12317
			// (set) Token: 0x060050E0 RID: 20704 RVA: 0x00080032 File Offset: 0x0007E232
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700301E RID: 12318
			// (set) Token: 0x060050E1 RID: 20705 RVA: 0x0008004A File Offset: 0x0007E24A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700301F RID: 12319
			// (set) Token: 0x060050E2 RID: 20706 RVA: 0x00080062 File Offset: 0x0007E262
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200063E RID: 1598
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003020 RID: 12320
			// (set) Token: 0x060050E4 RID: 20708 RVA: 0x00080082 File Offset: 0x0007E282
			public virtual MailboxTransportServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17003021 RID: 12321
			// (set) Token: 0x060050E5 RID: 20709 RVA: 0x00080095 File Offset: 0x0007E295
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003022 RID: 12322
			// (set) Token: 0x060050E6 RID: 20710 RVA: 0x000800A8 File Offset: 0x0007E2A8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003023 RID: 12323
			// (set) Token: 0x060050E7 RID: 20711 RVA: 0x000800C0 File Offset: 0x0007E2C0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003024 RID: 12324
			// (set) Token: 0x060050E8 RID: 20712 RVA: 0x000800D8 File Offset: 0x0007E2D8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003025 RID: 12325
			// (set) Token: 0x060050E9 RID: 20713 RVA: 0x000800F0 File Offset: 0x0007E2F0
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
