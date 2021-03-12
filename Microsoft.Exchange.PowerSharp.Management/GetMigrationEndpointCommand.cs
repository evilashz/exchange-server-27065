using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Migration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200028B RID: 651
	public class GetMigrationEndpointCommand : SyntheticCommandWithPipelineInput<MigrationEndpoint, MigrationEndpoint>
	{
		// Token: 0x06002EFF RID: 12031 RVA: 0x00054EB4 File Offset: 0x000530B4
		private GetMigrationEndpointCommand() : base("Get-MigrationEndpoint")
		{
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x00054EC1 File Offset: 0x000530C1
		public GetMigrationEndpointCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x00054ED0 File Offset: 0x000530D0
		public virtual GetMigrationEndpointCommand SetParameters(GetMigrationEndpointCommand.TypeFilterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x00054EDA File Offset: 0x000530DA
		public virtual GetMigrationEndpointCommand SetParameters(GetMigrationEndpointCommand.ConnectionSettingsFilterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x00054EE4 File Offset: 0x000530E4
		public virtual GetMigrationEndpointCommand SetParameters(GetMigrationEndpointCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x00054EEE File Offset: 0x000530EE
		public virtual GetMigrationEndpointCommand SetParameters(GetMigrationEndpointCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200028C RID: 652
		public class TypeFilterParameters : ParametersBase
		{
			// Token: 0x170015A2 RID: 5538
			// (set) Token: 0x06002F05 RID: 12037 RVA: 0x00054EF8 File Offset: 0x000530F8
			public virtual MigrationType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x170015A3 RID: 5539
			// (set) Token: 0x06002F06 RID: 12038 RVA: 0x00054F10 File Offset: 0x00053110
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170015A4 RID: 5540
			// (set) Token: 0x06002F07 RID: 12039 RVA: 0x00054F2E File Offset: 0x0005312E
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170015A5 RID: 5541
			// (set) Token: 0x06002F08 RID: 12040 RVA: 0x00054F4C File Offset: 0x0005314C
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x170015A6 RID: 5542
			// (set) Token: 0x06002F09 RID: 12041 RVA: 0x00054F64 File Offset: 0x00053164
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x170015A7 RID: 5543
			// (set) Token: 0x06002F0A RID: 12042 RVA: 0x00054F77 File Offset: 0x00053177
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170015A8 RID: 5544
			// (set) Token: 0x06002F0B RID: 12043 RVA: 0x00054F8A File Offset: 0x0005318A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170015A9 RID: 5545
			// (set) Token: 0x06002F0C RID: 12044 RVA: 0x00054FA2 File Offset: 0x000531A2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170015AA RID: 5546
			// (set) Token: 0x06002F0D RID: 12045 RVA: 0x00054FBA File Offset: 0x000531BA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170015AB RID: 5547
			// (set) Token: 0x06002F0E RID: 12046 RVA: 0x00054FD2 File Offset: 0x000531D2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200028D RID: 653
		public class ConnectionSettingsFilterParameters : ParametersBase
		{
			// Token: 0x170015AC RID: 5548
			// (set) Token: 0x06002F10 RID: 12048 RVA: 0x00054FF2 File Offset: 0x000531F2
			public virtual ExchangeConnectionSettings ConnectionSettings
			{
				set
				{
					base.PowerSharpParameters["ConnectionSettings"] = value;
				}
			}

			// Token: 0x170015AD RID: 5549
			// (set) Token: 0x06002F11 RID: 12049 RVA: 0x00055005 File Offset: 0x00053205
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170015AE RID: 5550
			// (set) Token: 0x06002F12 RID: 12050 RVA: 0x00055023 File Offset: 0x00053223
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170015AF RID: 5551
			// (set) Token: 0x06002F13 RID: 12051 RVA: 0x00055041 File Offset: 0x00053241
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x170015B0 RID: 5552
			// (set) Token: 0x06002F14 RID: 12052 RVA: 0x00055059 File Offset: 0x00053259
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x170015B1 RID: 5553
			// (set) Token: 0x06002F15 RID: 12053 RVA: 0x0005506C File Offset: 0x0005326C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170015B2 RID: 5554
			// (set) Token: 0x06002F16 RID: 12054 RVA: 0x0005507F File Offset: 0x0005327F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170015B3 RID: 5555
			// (set) Token: 0x06002F17 RID: 12055 RVA: 0x00055097 File Offset: 0x00053297
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170015B4 RID: 5556
			// (set) Token: 0x06002F18 RID: 12056 RVA: 0x000550AF File Offset: 0x000532AF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170015B5 RID: 5557
			// (set) Token: 0x06002F19 RID: 12057 RVA: 0x000550C7 File Offset: 0x000532C7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200028E RID: 654
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170015B6 RID: 5558
			// (set) Token: 0x06002F1B RID: 12059 RVA: 0x000550E7 File Offset: 0x000532E7
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170015B7 RID: 5559
			// (set) Token: 0x06002F1C RID: 12060 RVA: 0x00055105 File Offset: 0x00053305
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170015B8 RID: 5560
			// (set) Token: 0x06002F1D RID: 12061 RVA: 0x00055123 File Offset: 0x00053323
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x170015B9 RID: 5561
			// (set) Token: 0x06002F1E RID: 12062 RVA: 0x0005513B File Offset: 0x0005333B
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x170015BA RID: 5562
			// (set) Token: 0x06002F1F RID: 12063 RVA: 0x0005514E File Offset: 0x0005334E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170015BB RID: 5563
			// (set) Token: 0x06002F20 RID: 12064 RVA: 0x00055161 File Offset: 0x00053361
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170015BC RID: 5564
			// (set) Token: 0x06002F21 RID: 12065 RVA: 0x00055179 File Offset: 0x00053379
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170015BD RID: 5565
			// (set) Token: 0x06002F22 RID: 12066 RVA: 0x00055191 File Offset: 0x00053391
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170015BE RID: 5566
			// (set) Token: 0x06002F23 RID: 12067 RVA: 0x000551A9 File Offset: 0x000533A9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200028F RID: 655
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170015BF RID: 5567
			// (set) Token: 0x06002F25 RID: 12069 RVA: 0x000551C9 File Offset: 0x000533C9
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MigrationEndpointIdParameter(value) : null);
				}
			}

			// Token: 0x170015C0 RID: 5568
			// (set) Token: 0x06002F26 RID: 12070 RVA: 0x000551E7 File Offset: 0x000533E7
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170015C1 RID: 5569
			// (set) Token: 0x06002F27 RID: 12071 RVA: 0x00055205 File Offset: 0x00053405
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170015C2 RID: 5570
			// (set) Token: 0x06002F28 RID: 12072 RVA: 0x00055223 File Offset: 0x00053423
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x170015C3 RID: 5571
			// (set) Token: 0x06002F29 RID: 12073 RVA: 0x0005523B File Offset: 0x0005343B
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x170015C4 RID: 5572
			// (set) Token: 0x06002F2A RID: 12074 RVA: 0x0005524E File Offset: 0x0005344E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170015C5 RID: 5573
			// (set) Token: 0x06002F2B RID: 12075 RVA: 0x00055261 File Offset: 0x00053461
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170015C6 RID: 5574
			// (set) Token: 0x06002F2C RID: 12076 RVA: 0x00055279 File Offset: 0x00053479
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170015C7 RID: 5575
			// (set) Token: 0x06002F2D RID: 12077 RVA: 0x00055291 File Offset: 0x00053491
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170015C8 RID: 5576
			// (set) Token: 0x06002F2E RID: 12078 RVA: 0x000552A9 File Offset: 0x000534A9
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
