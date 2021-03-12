using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008FE RID: 2302
	public class DisableOutlookAnywhereCommand : SyntheticCommandWithPipelineInput<ADRpcHttpVirtualDirectory, ADRpcHttpVirtualDirectory>
	{
		// Token: 0x060074EA RID: 29930 RVA: 0x000AFAEE File Offset: 0x000ADCEE
		private DisableOutlookAnywhereCommand() : base("Disable-OutlookAnywhere")
		{
		}

		// Token: 0x060074EB RID: 29931 RVA: 0x000AFAFB File Offset: 0x000ADCFB
		public DisableOutlookAnywhereCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060074EC RID: 29932 RVA: 0x000AFB0A File Offset: 0x000ADD0A
		public virtual DisableOutlookAnywhereCommand SetParameters(DisableOutlookAnywhereCommand.ServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060074ED RID: 29933 RVA: 0x000AFB14 File Offset: 0x000ADD14
		public virtual DisableOutlookAnywhereCommand SetParameters(DisableOutlookAnywhereCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060074EE RID: 29934 RVA: 0x000AFB1E File Offset: 0x000ADD1E
		public virtual DisableOutlookAnywhereCommand SetParameters(DisableOutlookAnywhereCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008FF RID: 2303
		public class ServerParameters : ParametersBase
		{
			// Token: 0x17004EA7 RID: 20135
			// (set) Token: 0x060074EF RID: 29935 RVA: 0x000AFB28 File Offset: 0x000ADD28
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17004EA8 RID: 20136
			// (set) Token: 0x060074F0 RID: 29936 RVA: 0x000AFB3B File Offset: 0x000ADD3B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004EA9 RID: 20137
			// (set) Token: 0x060074F1 RID: 29937 RVA: 0x000AFB4E File Offset: 0x000ADD4E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004EAA RID: 20138
			// (set) Token: 0x060074F2 RID: 29938 RVA: 0x000AFB66 File Offset: 0x000ADD66
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004EAB RID: 20139
			// (set) Token: 0x060074F3 RID: 29939 RVA: 0x000AFB7E File Offset: 0x000ADD7E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004EAC RID: 20140
			// (set) Token: 0x060074F4 RID: 29940 RVA: 0x000AFB96 File Offset: 0x000ADD96
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004EAD RID: 20141
			// (set) Token: 0x060074F5 RID: 29941 RVA: 0x000AFBAE File Offset: 0x000ADDAE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004EAE RID: 20142
			// (set) Token: 0x060074F6 RID: 29942 RVA: 0x000AFBC6 File Offset: 0x000ADDC6
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000900 RID: 2304
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004EAF RID: 20143
			// (set) Token: 0x060074F8 RID: 29944 RVA: 0x000AFBE6 File Offset: 0x000ADDE6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004EB0 RID: 20144
			// (set) Token: 0x060074F9 RID: 29945 RVA: 0x000AFBF9 File Offset: 0x000ADDF9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004EB1 RID: 20145
			// (set) Token: 0x060074FA RID: 29946 RVA: 0x000AFC11 File Offset: 0x000ADE11
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004EB2 RID: 20146
			// (set) Token: 0x060074FB RID: 29947 RVA: 0x000AFC29 File Offset: 0x000ADE29
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004EB3 RID: 20147
			// (set) Token: 0x060074FC RID: 29948 RVA: 0x000AFC41 File Offset: 0x000ADE41
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004EB4 RID: 20148
			// (set) Token: 0x060074FD RID: 29949 RVA: 0x000AFC59 File Offset: 0x000ADE59
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004EB5 RID: 20149
			// (set) Token: 0x060074FE RID: 29950 RVA: 0x000AFC71 File Offset: 0x000ADE71
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000901 RID: 2305
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004EB6 RID: 20150
			// (set) Token: 0x06007500 RID: 29952 RVA: 0x000AFC91 File Offset: 0x000ADE91
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004EB7 RID: 20151
			// (set) Token: 0x06007501 RID: 29953 RVA: 0x000AFCA4 File Offset: 0x000ADEA4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004EB8 RID: 20152
			// (set) Token: 0x06007502 RID: 29954 RVA: 0x000AFCB7 File Offset: 0x000ADEB7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004EB9 RID: 20153
			// (set) Token: 0x06007503 RID: 29955 RVA: 0x000AFCCF File Offset: 0x000ADECF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004EBA RID: 20154
			// (set) Token: 0x06007504 RID: 29956 RVA: 0x000AFCE7 File Offset: 0x000ADEE7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004EBB RID: 20155
			// (set) Token: 0x06007505 RID: 29957 RVA: 0x000AFCFF File Offset: 0x000ADEFF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004EBC RID: 20156
			// (set) Token: 0x06007506 RID: 29958 RVA: 0x000AFD17 File Offset: 0x000ADF17
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004EBD RID: 20157
			// (set) Token: 0x06007507 RID: 29959 RVA: 0x000AFD2F File Offset: 0x000ADF2F
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
