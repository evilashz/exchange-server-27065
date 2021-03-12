using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007D6 RID: 2006
	public class GetRemoteDomainCommand : SyntheticCommandWithPipelineInput<DomainContentConfig, DomainContentConfig>
	{
		// Token: 0x0600641F RID: 25631 RVA: 0x000994D0 File Offset: 0x000976D0
		private GetRemoteDomainCommand() : base("Get-RemoteDomain")
		{
		}

		// Token: 0x06006420 RID: 25632 RVA: 0x000994DD File Offset: 0x000976DD
		public GetRemoteDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006421 RID: 25633 RVA: 0x000994EC File Offset: 0x000976EC
		public virtual GetRemoteDomainCommand SetParameters(GetRemoteDomainCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006422 RID: 25634 RVA: 0x000994F6 File Offset: 0x000976F6
		public virtual GetRemoteDomainCommand SetParameters(GetRemoteDomainCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007D7 RID: 2007
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700402C RID: 16428
			// (set) Token: 0x06006423 RID: 25635 RVA: 0x00099500 File Offset: 0x00097700
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700402D RID: 16429
			// (set) Token: 0x06006424 RID: 25636 RVA: 0x0009951E File Offset: 0x0009771E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700402E RID: 16430
			// (set) Token: 0x06006425 RID: 25637 RVA: 0x00099531 File Offset: 0x00097731
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700402F RID: 16431
			// (set) Token: 0x06006426 RID: 25638 RVA: 0x00099549 File Offset: 0x00097749
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004030 RID: 16432
			// (set) Token: 0x06006427 RID: 25639 RVA: 0x00099561 File Offset: 0x00097761
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004031 RID: 16433
			// (set) Token: 0x06006428 RID: 25640 RVA: 0x00099579 File Offset: 0x00097779
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020007D8 RID: 2008
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004032 RID: 16434
			// (set) Token: 0x0600642A RID: 25642 RVA: 0x00099599 File Offset: 0x00097799
			public virtual RemoteDomainIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004033 RID: 16435
			// (set) Token: 0x0600642B RID: 25643 RVA: 0x000995AC File Offset: 0x000977AC
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17004034 RID: 16436
			// (set) Token: 0x0600642C RID: 25644 RVA: 0x000995CA File Offset: 0x000977CA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004035 RID: 16437
			// (set) Token: 0x0600642D RID: 25645 RVA: 0x000995DD File Offset: 0x000977DD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004036 RID: 16438
			// (set) Token: 0x0600642E RID: 25646 RVA: 0x000995F5 File Offset: 0x000977F5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004037 RID: 16439
			// (set) Token: 0x0600642F RID: 25647 RVA: 0x0009960D File Offset: 0x0009780D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004038 RID: 16440
			// (set) Token: 0x06006430 RID: 25648 RVA: 0x00099625 File Offset: 0x00097825
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
