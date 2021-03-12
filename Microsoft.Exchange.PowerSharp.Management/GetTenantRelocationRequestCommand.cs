using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000384 RID: 900
	public class GetTenantRelocationRequestCommand : SyntheticCommandWithPipelineInput<TenantRelocationRequest, TenantRelocationRequest>
	{
		// Token: 0x0600388D RID: 14477 RVA: 0x0006136F File Offset: 0x0005F56F
		private GetTenantRelocationRequestCommand() : base("Get-TenantRelocationRequest")
		{
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x0006137C File Offset: 0x0005F57C
		public GetTenantRelocationRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x0006138B File Offset: 0x0005F58B
		public virtual GetTenantRelocationRequestCommand SetParameters(GetTenantRelocationRequestCommand.PartitionWideParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003890 RID: 14480 RVA: 0x00061395 File Offset: 0x0005F595
		public virtual GetTenantRelocationRequestCommand SetParameters(GetTenantRelocationRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x0006139F File Offset: 0x0005F59F
		public virtual GetTenantRelocationRequestCommand SetParameters(GetTenantRelocationRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000385 RID: 901
		public class PartitionWideParameters : ParametersBase
		{
			// Token: 0x17001D3E RID: 7486
			// (set) Token: 0x06003892 RID: 14482 RVA: 0x000613A9 File Offset: 0x0005F5A9
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17001D3F RID: 7487
			// (set) Token: 0x06003893 RID: 14483 RVA: 0x000613BC File Offset: 0x0005F5BC
			public virtual SwitchParameter SourceStateOnly
			{
				set
				{
					base.PowerSharpParameters["SourceStateOnly"] = value;
				}
			}

			// Token: 0x17001D40 RID: 7488
			// (set) Token: 0x06003894 RID: 14484 RVA: 0x000613D4 File Offset: 0x0005F5D4
			public virtual RelocationStateRequested RelocationStateRequested
			{
				set
				{
					base.PowerSharpParameters["RelocationStateRequested"] = value;
				}
			}

			// Token: 0x17001D41 RID: 7489
			// (set) Token: 0x06003895 RID: 14485 RVA: 0x000613EC File Offset: 0x0005F5EC
			public virtual RelocationStatusDetailsSource RelocationStatusDetailsSource
			{
				set
				{
					base.PowerSharpParameters["RelocationStatusDetailsSource"] = value;
				}
			}

			// Token: 0x17001D42 RID: 7490
			// (set) Token: 0x06003896 RID: 14486 RVA: 0x00061404 File Offset: 0x0005F604
			public virtual RelocationError RelocationLastError
			{
				set
				{
					base.PowerSharpParameters["RelocationLastError"] = value;
				}
			}

			// Token: 0x17001D43 RID: 7491
			// (set) Token: 0x06003897 RID: 14487 RVA: 0x0006141C File Offset: 0x0005F61C
			public virtual SwitchParameter Suspended
			{
				set
				{
					base.PowerSharpParameters["Suspended"] = value;
				}
			}

			// Token: 0x17001D44 RID: 7492
			// (set) Token: 0x06003898 RID: 14488 RVA: 0x00061434 File Offset: 0x0005F634
			public virtual SwitchParameter Lockdown
			{
				set
				{
					base.PowerSharpParameters["Lockdown"] = value;
				}
			}

			// Token: 0x17001D45 RID: 7493
			// (set) Token: 0x06003899 RID: 14489 RVA: 0x0006144C File Offset: 0x0005F64C
			public virtual SwitchParameter StaleLockdown
			{
				set
				{
					base.PowerSharpParameters["StaleLockdown"] = value;
				}
			}

			// Token: 0x17001D46 RID: 7494
			// (set) Token: 0x0600389A RID: 14490 RVA: 0x00061464 File Offset: 0x0005F664
			public virtual SwitchParameter HasPermanentError
			{
				set
				{
					base.PowerSharpParameters["HasPermanentError"] = value;
				}
			}

			// Token: 0x17001D47 RID: 7495
			// (set) Token: 0x0600389B RID: 14491 RVA: 0x0006147C File Offset: 0x0005F67C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001D48 RID: 7496
			// (set) Token: 0x0600389C RID: 14492 RVA: 0x0006148F File Offset: 0x0005F68F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001D49 RID: 7497
			// (set) Token: 0x0600389D RID: 14493 RVA: 0x000614A7 File Offset: 0x0005F6A7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001D4A RID: 7498
			// (set) Token: 0x0600389E RID: 14494 RVA: 0x000614BF File Offset: 0x0005F6BF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001D4B RID: 7499
			// (set) Token: 0x0600389F RID: 14495 RVA: 0x000614D7 File Offset: 0x0005F6D7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000386 RID: 902
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001D4C RID: 7500
			// (set) Token: 0x060038A1 RID: 14497 RVA: 0x000614F7 File Offset: 0x0005F6F7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001D4D RID: 7501
			// (set) Token: 0x060038A2 RID: 14498 RVA: 0x0006150A File Offset: 0x0005F70A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001D4E RID: 7502
			// (set) Token: 0x060038A3 RID: 14499 RVA: 0x00061522 File Offset: 0x0005F722
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001D4F RID: 7503
			// (set) Token: 0x060038A4 RID: 14500 RVA: 0x0006153A File Offset: 0x0005F73A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001D50 RID: 7504
			// (set) Token: 0x060038A5 RID: 14501 RVA: 0x00061552 File Offset: 0x0005F752
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000387 RID: 903
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001D51 RID: 7505
			// (set) Token: 0x060038A7 RID: 14503 RVA: 0x00061572 File Offset: 0x0005F772
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new TenantRelocationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17001D52 RID: 7506
			// (set) Token: 0x060038A8 RID: 14504 RVA: 0x00061590 File Offset: 0x0005F790
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001D53 RID: 7507
			// (set) Token: 0x060038A9 RID: 14505 RVA: 0x000615A3 File Offset: 0x0005F7A3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001D54 RID: 7508
			// (set) Token: 0x060038AA RID: 14506 RVA: 0x000615BB File Offset: 0x0005F7BB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001D55 RID: 7509
			// (set) Token: 0x060038AB RID: 14507 RVA: 0x000615D3 File Offset: 0x0005F7D3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001D56 RID: 7510
			// (set) Token: 0x060038AC RID: 14508 RVA: 0x000615EB File Offset: 0x0005F7EB
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
