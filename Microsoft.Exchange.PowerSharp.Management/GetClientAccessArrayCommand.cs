using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000612 RID: 1554
	public class GetClientAccessArrayCommand : SyntheticCommandWithPipelineInput<ClientAccessArray, ClientAccessArray>
	{
		// Token: 0x06004FCB RID: 20427 RVA: 0x0007EB97 File Offset: 0x0007CD97
		private GetClientAccessArrayCommand() : base("Get-ClientAccessArray")
		{
		}

		// Token: 0x06004FCC RID: 20428 RVA: 0x0007EBA4 File Offset: 0x0007CDA4
		public GetClientAccessArrayCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004FCD RID: 20429 RVA: 0x0007EBB3 File Offset: 0x0007CDB3
		public virtual GetClientAccessArrayCommand SetParameters(GetClientAccessArrayCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004FCE RID: 20430 RVA: 0x0007EBBD File Offset: 0x0007CDBD
		public virtual GetClientAccessArrayCommand SetParameters(GetClientAccessArrayCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000613 RID: 1555
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002F60 RID: 12128
			// (set) Token: 0x06004FCF RID: 20431 RVA: 0x0007EBC7 File Offset: 0x0007CDC7
			public virtual string Site
			{
				set
				{
					base.PowerSharpParameters["Site"] = ((value != null) ? new AdSiteIdParameter(value) : null);
				}
			}

			// Token: 0x17002F61 RID: 12129
			// (set) Token: 0x06004FD0 RID: 20432 RVA: 0x0007EBE5 File Offset: 0x0007CDE5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002F62 RID: 12130
			// (set) Token: 0x06004FD1 RID: 20433 RVA: 0x0007EBF8 File Offset: 0x0007CDF8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002F63 RID: 12131
			// (set) Token: 0x06004FD2 RID: 20434 RVA: 0x0007EC10 File Offset: 0x0007CE10
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002F64 RID: 12132
			// (set) Token: 0x06004FD3 RID: 20435 RVA: 0x0007EC28 File Offset: 0x0007CE28
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002F65 RID: 12133
			// (set) Token: 0x06004FD4 RID: 20436 RVA: 0x0007EC40 File Offset: 0x0007CE40
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000614 RID: 1556
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002F66 RID: 12134
			// (set) Token: 0x06004FD6 RID: 20438 RVA: 0x0007EC60 File Offset: 0x0007CE60
			public virtual ClientAccessArrayIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002F67 RID: 12135
			// (set) Token: 0x06004FD7 RID: 20439 RVA: 0x0007EC73 File Offset: 0x0007CE73
			public virtual string Site
			{
				set
				{
					base.PowerSharpParameters["Site"] = ((value != null) ? new AdSiteIdParameter(value) : null);
				}
			}

			// Token: 0x17002F68 RID: 12136
			// (set) Token: 0x06004FD8 RID: 20440 RVA: 0x0007EC91 File Offset: 0x0007CE91
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002F69 RID: 12137
			// (set) Token: 0x06004FD9 RID: 20441 RVA: 0x0007ECA4 File Offset: 0x0007CEA4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002F6A RID: 12138
			// (set) Token: 0x06004FDA RID: 20442 RVA: 0x0007ECBC File Offset: 0x0007CEBC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002F6B RID: 12139
			// (set) Token: 0x06004FDB RID: 20443 RVA: 0x0007ECD4 File Offset: 0x0007CED4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002F6C RID: 12140
			// (set) Token: 0x06004FDC RID: 20444 RVA: 0x0007ECEC File Offset: 0x0007CEEC
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
