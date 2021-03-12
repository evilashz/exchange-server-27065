using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200037E RID: 894
	public class GetRecipientPermissionCommand : SyntheticCommandWithPipelineInput<ReducedRecipient, ReducedRecipient>
	{
		// Token: 0x0600385B RID: 14427 RVA: 0x00060F63 File Offset: 0x0005F163
		private GetRecipientPermissionCommand() : base("Get-RecipientPermission")
		{
		}

		// Token: 0x0600385C RID: 14428 RVA: 0x00060F70 File Offset: 0x0005F170
		public GetRecipientPermissionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600385D RID: 14429 RVA: 0x00060F7F File Offset: 0x0005F17F
		public virtual GetRecipientPermissionCommand SetParameters(GetRecipientPermissionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600385E RID: 14430 RVA: 0x00060F89 File Offset: 0x0005F189
		public virtual GetRecipientPermissionCommand SetParameters(GetRecipientPermissionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200037F RID: 895
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001D18 RID: 7448
			// (set) Token: 0x0600385F RID: 14431 RVA: 0x00060F93 File Offset: 0x0005F193
			public virtual string Trustee
			{
				set
				{
					base.PowerSharpParameters["Trustee"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x17001D19 RID: 7449
			// (set) Token: 0x06003860 RID: 14432 RVA: 0x00060FB1 File Offset: 0x0005F1B1
			public virtual MultiValuedProperty<RecipientAccessRight> AccessRights
			{
				set
				{
					base.PowerSharpParameters["AccessRights"] = value;
				}
			}

			// Token: 0x17001D1A RID: 7450
			// (set) Token: 0x06003861 RID: 14433 RVA: 0x00060FC4 File Offset: 0x0005F1C4
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001D1B RID: 7451
			// (set) Token: 0x06003862 RID: 14434 RVA: 0x00060FDC File Offset: 0x0005F1DC
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17001D1C RID: 7452
			// (set) Token: 0x06003863 RID: 14435 RVA: 0x00060FF4 File Offset: 0x0005F1F4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001D1D RID: 7453
			// (set) Token: 0x06003864 RID: 14436 RVA: 0x00061007 File Offset: 0x0005F207
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001D1E RID: 7454
			// (set) Token: 0x06003865 RID: 14437 RVA: 0x0006101F File Offset: 0x0005F21F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001D1F RID: 7455
			// (set) Token: 0x06003866 RID: 14438 RVA: 0x00061037 File Offset: 0x0005F237
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001D20 RID: 7456
			// (set) Token: 0x06003867 RID: 14439 RVA: 0x0006104F File Offset: 0x0005F24F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000380 RID: 896
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001D21 RID: 7457
			// (set) Token: 0x06003869 RID: 14441 RVA: 0x0006106F File Offset: 0x0005F26F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17001D22 RID: 7458
			// (set) Token: 0x0600386A RID: 14442 RVA: 0x0006108D File Offset: 0x0005F28D
			public virtual string Trustee
			{
				set
				{
					base.PowerSharpParameters["Trustee"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x17001D23 RID: 7459
			// (set) Token: 0x0600386B RID: 14443 RVA: 0x000610AB File Offset: 0x0005F2AB
			public virtual MultiValuedProperty<RecipientAccessRight> AccessRights
			{
				set
				{
					base.PowerSharpParameters["AccessRights"] = value;
				}
			}

			// Token: 0x17001D24 RID: 7460
			// (set) Token: 0x0600386C RID: 14444 RVA: 0x000610BE File Offset: 0x0005F2BE
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001D25 RID: 7461
			// (set) Token: 0x0600386D RID: 14445 RVA: 0x000610D6 File Offset: 0x0005F2D6
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17001D26 RID: 7462
			// (set) Token: 0x0600386E RID: 14446 RVA: 0x000610EE File Offset: 0x0005F2EE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001D27 RID: 7463
			// (set) Token: 0x0600386F RID: 14447 RVA: 0x00061101 File Offset: 0x0005F301
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001D28 RID: 7464
			// (set) Token: 0x06003870 RID: 14448 RVA: 0x00061119 File Offset: 0x0005F319
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001D29 RID: 7465
			// (set) Token: 0x06003871 RID: 14449 RVA: 0x00061131 File Offset: 0x0005F331
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001D2A RID: 7466
			// (set) Token: 0x06003872 RID: 14450 RVA: 0x00061149 File Offset: 0x0005F349
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
