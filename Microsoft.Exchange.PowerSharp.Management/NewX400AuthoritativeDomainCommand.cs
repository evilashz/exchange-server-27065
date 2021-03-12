using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007B1 RID: 1969
	public class NewX400AuthoritativeDomainCommand : SyntheticCommandWithPipelineInput<X400AuthoritativeDomain, X400AuthoritativeDomain>
	{
		// Token: 0x060062BF RID: 25279 RVA: 0x00097967 File Offset: 0x00095B67
		private NewX400AuthoritativeDomainCommand() : base("New-X400AuthoritativeDomain")
		{
		}

		// Token: 0x060062C0 RID: 25280 RVA: 0x00097974 File Offset: 0x00095B74
		public NewX400AuthoritativeDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060062C1 RID: 25281 RVA: 0x00097983 File Offset: 0x00095B83
		public virtual NewX400AuthoritativeDomainCommand SetParameters(NewX400AuthoritativeDomainCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007B2 RID: 1970
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003F16 RID: 16150
			// (set) Token: 0x060062C2 RID: 25282 RVA: 0x0009798D File Offset: 0x00095B8D
			public virtual X400Domain X400DomainName
			{
				set
				{
					base.PowerSharpParameters["X400DomainName"] = value;
				}
			}

			// Token: 0x17003F17 RID: 16151
			// (set) Token: 0x060062C3 RID: 25283 RVA: 0x000979A0 File Offset: 0x00095BA0
			public virtual bool X400ExternalRelay
			{
				set
				{
					base.PowerSharpParameters["X400ExternalRelay"] = value;
				}
			}

			// Token: 0x17003F18 RID: 16152
			// (set) Token: 0x060062C4 RID: 25284 RVA: 0x000979B8 File Offset: 0x00095BB8
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003F19 RID: 16153
			// (set) Token: 0x060062C5 RID: 25285 RVA: 0x000979CB File Offset: 0x00095BCB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003F1A RID: 16154
			// (set) Token: 0x060062C6 RID: 25286 RVA: 0x000979DE File Offset: 0x00095BDE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003F1B RID: 16155
			// (set) Token: 0x060062C7 RID: 25287 RVA: 0x000979F6 File Offset: 0x00095BF6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003F1C RID: 16156
			// (set) Token: 0x060062C8 RID: 25288 RVA: 0x00097A0E File Offset: 0x00095C0E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003F1D RID: 16157
			// (set) Token: 0x060062C9 RID: 25289 RVA: 0x00097A26 File Offset: 0x00095C26
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003F1E RID: 16158
			// (set) Token: 0x060062CA RID: 25290 RVA: 0x00097A3E File Offset: 0x00095C3E
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
