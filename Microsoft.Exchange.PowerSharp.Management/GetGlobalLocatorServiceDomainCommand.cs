using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006C8 RID: 1736
	public class GetGlobalLocatorServiceDomainCommand : SyntheticCommandWithPipelineInputNoOutput<SmtpDomain>
	{
		// Token: 0x06005B15 RID: 23317 RVA: 0x0008DDE6 File Offset: 0x0008BFE6
		private GetGlobalLocatorServiceDomainCommand() : base("Get-GlobalLocatorServiceDomain")
		{
		}

		// Token: 0x06005B16 RID: 23318 RVA: 0x0008DDF3 File Offset: 0x0008BFF3
		public GetGlobalLocatorServiceDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005B17 RID: 23319 RVA: 0x0008DE02 File Offset: 0x0008C002
		public virtual GetGlobalLocatorServiceDomainCommand SetParameters(GetGlobalLocatorServiceDomainCommand.DomainNameParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006C9 RID: 1737
		public class DomainNameParameterSetParameters : ParametersBase
		{
			// Token: 0x1700393E RID: 14654
			// (set) Token: 0x06005B18 RID: 23320 RVA: 0x0008DE0C File Offset: 0x0008C00C
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x1700393F RID: 14655
			// (set) Token: 0x06005B19 RID: 23321 RVA: 0x0008DE1F File Offset: 0x0008C01F
			public virtual SwitchParameter UseOfflineGLS
			{
				set
				{
					base.PowerSharpParameters["UseOfflineGLS"] = value;
				}
			}

			// Token: 0x17003940 RID: 14656
			// (set) Token: 0x06005B1A RID: 23322 RVA: 0x0008DE37 File Offset: 0x0008C037
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003941 RID: 14657
			// (set) Token: 0x06005B1B RID: 23323 RVA: 0x0008DE4F File Offset: 0x0008C04F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003942 RID: 14658
			// (set) Token: 0x06005B1C RID: 23324 RVA: 0x0008DE67 File Offset: 0x0008C067
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003943 RID: 14659
			// (set) Token: 0x06005B1D RID: 23325 RVA: 0x0008DE7F File Offset: 0x0008C07F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003944 RID: 14660
			// (set) Token: 0x06005B1E RID: 23326 RVA: 0x0008DE97 File Offset: 0x0008C097
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003945 RID: 14661
			// (set) Token: 0x06005B1F RID: 23327 RVA: 0x0008DEAF File Offset: 0x0008C0AF
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
