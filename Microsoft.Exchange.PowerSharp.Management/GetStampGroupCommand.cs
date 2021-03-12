using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000548 RID: 1352
	public class GetStampGroupCommand : SyntheticCommandWithPipelineInput<StampGroup, StampGroup>
	{
		// Token: 0x060047EC RID: 18412 RVA: 0x00074BAE File Offset: 0x00072DAE
		private GetStampGroupCommand() : base("Get-StampGroup")
		{
		}

		// Token: 0x060047ED RID: 18413 RVA: 0x00074BBB File Offset: 0x00072DBB
		public GetStampGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060047EE RID: 18414 RVA: 0x00074BCA File Offset: 0x00072DCA
		public virtual GetStampGroupCommand SetParameters(GetStampGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060047EF RID: 18415 RVA: 0x00074BD4 File Offset: 0x00072DD4
		public virtual GetStampGroupCommand SetParameters(GetStampGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000549 RID: 1353
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002915 RID: 10517
			// (set) Token: 0x060047F0 RID: 18416 RVA: 0x00074BDE File Offset: 0x00072DDE
			public virtual SwitchParameter Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x17002916 RID: 10518
			// (set) Token: 0x060047F1 RID: 18417 RVA: 0x00074BF6 File Offset: 0x00072DF6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002917 RID: 10519
			// (set) Token: 0x060047F2 RID: 18418 RVA: 0x00074C09 File Offset: 0x00072E09
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002918 RID: 10520
			// (set) Token: 0x060047F3 RID: 18419 RVA: 0x00074C21 File Offset: 0x00072E21
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002919 RID: 10521
			// (set) Token: 0x060047F4 RID: 18420 RVA: 0x00074C39 File Offset: 0x00072E39
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700291A RID: 10522
			// (set) Token: 0x060047F5 RID: 18421 RVA: 0x00074C51 File Offset: 0x00072E51
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200054A RID: 1354
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700291B RID: 10523
			// (set) Token: 0x060047F7 RID: 18423 RVA: 0x00074C71 File Offset: 0x00072E71
			public virtual StampGroupIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700291C RID: 10524
			// (set) Token: 0x060047F8 RID: 18424 RVA: 0x00074C84 File Offset: 0x00072E84
			public virtual SwitchParameter Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x1700291D RID: 10525
			// (set) Token: 0x060047F9 RID: 18425 RVA: 0x00074C9C File Offset: 0x00072E9C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700291E RID: 10526
			// (set) Token: 0x060047FA RID: 18426 RVA: 0x00074CAF File Offset: 0x00072EAF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700291F RID: 10527
			// (set) Token: 0x060047FB RID: 18427 RVA: 0x00074CC7 File Offset: 0x00072EC7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002920 RID: 10528
			// (set) Token: 0x060047FC RID: 18428 RVA: 0x00074CDF File Offset: 0x00072EDF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002921 RID: 10529
			// (set) Token: 0x060047FD RID: 18429 RVA: 0x00074CF7 File Offset: 0x00072EF7
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
