using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000625 RID: 1573
	public class GetClientAccessServerCommand : SyntheticCommandWithPipelineInput<Server, Server>
	{
		// Token: 0x0600504F RID: 20559 RVA: 0x0007F5A4 File Offset: 0x0007D7A4
		private GetClientAccessServerCommand() : base("Get-ClientAccessServer")
		{
		}

		// Token: 0x06005050 RID: 20560 RVA: 0x0007F5B1 File Offset: 0x0007D7B1
		public GetClientAccessServerCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005051 RID: 20561 RVA: 0x0007F5C0 File Offset: 0x0007D7C0
		public virtual GetClientAccessServerCommand SetParameters(GetClientAccessServerCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005052 RID: 20562 RVA: 0x0007F5CA File Offset: 0x0007D7CA
		public virtual GetClientAccessServerCommand SetParameters(GetClientAccessServerCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000626 RID: 1574
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002FBE RID: 12222
			// (set) Token: 0x06005053 RID: 20563 RVA: 0x0007F5D4 File Offset: 0x0007D7D4
			public virtual SwitchParameter IncludeAlternateServiceAccountCredentialStatus
			{
				set
				{
					base.PowerSharpParameters["IncludeAlternateServiceAccountCredentialStatus"] = value;
				}
			}

			// Token: 0x17002FBF RID: 12223
			// (set) Token: 0x06005054 RID: 20564 RVA: 0x0007F5EC File Offset: 0x0007D7EC
			public virtual SwitchParameter IncludeAlternateServiceAccountCredentialPassword
			{
				set
				{
					base.PowerSharpParameters["IncludeAlternateServiceAccountCredentialPassword"] = value;
				}
			}

			// Token: 0x17002FC0 RID: 12224
			// (set) Token: 0x06005055 RID: 20565 RVA: 0x0007F604 File Offset: 0x0007D804
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002FC1 RID: 12225
			// (set) Token: 0x06005056 RID: 20566 RVA: 0x0007F617 File Offset: 0x0007D817
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002FC2 RID: 12226
			// (set) Token: 0x06005057 RID: 20567 RVA: 0x0007F62F File Offset: 0x0007D82F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002FC3 RID: 12227
			// (set) Token: 0x06005058 RID: 20568 RVA: 0x0007F647 File Offset: 0x0007D847
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002FC4 RID: 12228
			// (set) Token: 0x06005059 RID: 20569 RVA: 0x0007F65F File Offset: 0x0007D85F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000627 RID: 1575
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002FC5 RID: 12229
			// (set) Token: 0x0600505B RID: 20571 RVA: 0x0007F67F File Offset: 0x0007D87F
			public virtual ClientAccessServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002FC6 RID: 12230
			// (set) Token: 0x0600505C RID: 20572 RVA: 0x0007F692 File Offset: 0x0007D892
			public virtual SwitchParameter IncludeAlternateServiceAccountCredentialStatus
			{
				set
				{
					base.PowerSharpParameters["IncludeAlternateServiceAccountCredentialStatus"] = value;
				}
			}

			// Token: 0x17002FC7 RID: 12231
			// (set) Token: 0x0600505D RID: 20573 RVA: 0x0007F6AA File Offset: 0x0007D8AA
			public virtual SwitchParameter IncludeAlternateServiceAccountCredentialPassword
			{
				set
				{
					base.PowerSharpParameters["IncludeAlternateServiceAccountCredentialPassword"] = value;
				}
			}

			// Token: 0x17002FC8 RID: 12232
			// (set) Token: 0x0600505E RID: 20574 RVA: 0x0007F6C2 File Offset: 0x0007D8C2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002FC9 RID: 12233
			// (set) Token: 0x0600505F RID: 20575 RVA: 0x0007F6D5 File Offset: 0x0007D8D5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002FCA RID: 12234
			// (set) Token: 0x06005060 RID: 20576 RVA: 0x0007F6ED File Offset: 0x0007D8ED
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002FCB RID: 12235
			// (set) Token: 0x06005061 RID: 20577 RVA: 0x0007F705 File Offset: 0x0007D905
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002FCC RID: 12236
			// (set) Token: 0x06005062 RID: 20578 RVA: 0x0007F71D File Offset: 0x0007D91D
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
