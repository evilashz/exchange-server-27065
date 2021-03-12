using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006F8 RID: 1784
	public class GetContentFilterConfigCommand : SyntheticCommandWithPipelineInput<ContentFilterConfig, ContentFilterConfig>
	{
		// Token: 0x06005C7E RID: 23678 RVA: 0x0008FA6A File Offset: 0x0008DC6A
		private GetContentFilterConfigCommand() : base("Get-ContentFilterConfig")
		{
		}

		// Token: 0x06005C7F RID: 23679 RVA: 0x0008FA77 File Offset: 0x0008DC77
		public GetContentFilterConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005C80 RID: 23680 RVA: 0x0008FA86 File Offset: 0x0008DC86
		public virtual GetContentFilterConfigCommand SetParameters(GetContentFilterConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006F9 RID: 1785
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003A47 RID: 14919
			// (set) Token: 0x06005C81 RID: 23681 RVA: 0x0008FA90 File Offset: 0x0008DC90
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003A48 RID: 14920
			// (set) Token: 0x06005C82 RID: 23682 RVA: 0x0008FAA3 File Offset: 0x0008DCA3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003A49 RID: 14921
			// (set) Token: 0x06005C83 RID: 23683 RVA: 0x0008FABB File Offset: 0x0008DCBB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003A4A RID: 14922
			// (set) Token: 0x06005C84 RID: 23684 RVA: 0x0008FAD3 File Offset: 0x0008DCD3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003A4B RID: 14923
			// (set) Token: 0x06005C85 RID: 23685 RVA: 0x0008FAEB File Offset: 0x0008DCEB
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
