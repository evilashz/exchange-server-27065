using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007A6 RID: 1958
	public class GetX400AuthoritativeDomainCommand : SyntheticCommandWithPipelineInput<X400AuthoritativeDomain, X400AuthoritativeDomain>
	{
		// Token: 0x0600624D RID: 25165 RVA: 0x00096FF5 File Offset: 0x000951F5
		private GetX400AuthoritativeDomainCommand() : base("Get-X400AuthoritativeDomain")
		{
		}

		// Token: 0x0600624E RID: 25166 RVA: 0x00097002 File Offset: 0x00095202
		public GetX400AuthoritativeDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600624F RID: 25167 RVA: 0x00097011 File Offset: 0x00095211
		public virtual GetX400AuthoritativeDomainCommand SetParameters(GetX400AuthoritativeDomainCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006250 RID: 25168 RVA: 0x0009701B File Offset: 0x0009521B
		public virtual GetX400AuthoritativeDomainCommand SetParameters(GetX400AuthoritativeDomainCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007A7 RID: 1959
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003EBA RID: 16058
			// (set) Token: 0x06006251 RID: 25169 RVA: 0x00097025 File Offset: 0x00095225
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003EBB RID: 16059
			// (set) Token: 0x06006252 RID: 25170 RVA: 0x00097038 File Offset: 0x00095238
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003EBC RID: 16060
			// (set) Token: 0x06006253 RID: 25171 RVA: 0x00097050 File Offset: 0x00095250
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003EBD RID: 16061
			// (set) Token: 0x06006254 RID: 25172 RVA: 0x00097068 File Offset: 0x00095268
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003EBE RID: 16062
			// (set) Token: 0x06006255 RID: 25173 RVA: 0x00097080 File Offset: 0x00095280
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020007A8 RID: 1960
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003EBF RID: 16063
			// (set) Token: 0x06006257 RID: 25175 RVA: 0x000970A0 File Offset: 0x000952A0
			public virtual X400AuthoritativeDomainIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17003EC0 RID: 16064
			// (set) Token: 0x06006258 RID: 25176 RVA: 0x000970B3 File Offset: 0x000952B3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003EC1 RID: 16065
			// (set) Token: 0x06006259 RID: 25177 RVA: 0x000970C6 File Offset: 0x000952C6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003EC2 RID: 16066
			// (set) Token: 0x0600625A RID: 25178 RVA: 0x000970DE File Offset: 0x000952DE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003EC3 RID: 16067
			// (set) Token: 0x0600625B RID: 25179 RVA: 0x000970F6 File Offset: 0x000952F6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003EC4 RID: 16068
			// (set) Token: 0x0600625C RID: 25180 RVA: 0x0009710E File Offset: 0x0009530E
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
