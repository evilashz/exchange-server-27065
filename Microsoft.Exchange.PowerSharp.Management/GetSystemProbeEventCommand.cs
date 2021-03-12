using System;
using System.Management.Automation;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BC7 RID: 3015
	public class GetSystemProbeEventCommand : SyntheticCommandWithPipelineInputNoOutput<Guid>
	{
		// Token: 0x0600918C RID: 37260 RVA: 0x000D49D8 File Offset: 0x000D2BD8
		private GetSystemProbeEventCommand() : base("Get-SystemProbeEvent")
		{
		}

		// Token: 0x0600918D RID: 37261 RVA: 0x000D49E5 File Offset: 0x000D2BE5
		public GetSystemProbeEventCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600918E RID: 37262 RVA: 0x000D49F4 File Offset: 0x000D2BF4
		public virtual GetSystemProbeEventCommand SetParameters(GetSystemProbeEventCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BC8 RID: 3016
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170065B7 RID: 26039
			// (set) Token: 0x0600918F RID: 37263 RVA: 0x000D49FE File Offset: 0x000D2BFE
			public virtual Guid Guid
			{
				set
				{
					base.PowerSharpParameters["Guid"] = value;
				}
			}

			// Token: 0x170065B8 RID: 26040
			// (set) Token: 0x06009190 RID: 37264 RVA: 0x000D4A16 File Offset: 0x000D2C16
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170065B9 RID: 26041
			// (set) Token: 0x06009191 RID: 37265 RVA: 0x000D4A2E File Offset: 0x000D2C2E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170065BA RID: 26042
			// (set) Token: 0x06009192 RID: 37266 RVA: 0x000D4A46 File Offset: 0x000D2C46
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170065BB RID: 26043
			// (set) Token: 0x06009193 RID: 37267 RVA: 0x000D4A5E File Offset: 0x000D2C5E
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
