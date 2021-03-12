using System;
using System.Management.Automation;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002F9 RID: 761
	public class RemovePerfCountersCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x06003329 RID: 13097 RVA: 0x0005A3FB File Offset: 0x000585FB
		private RemovePerfCountersCommand() : base("Remove-PerfCounters")
		{
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x0005A408 File Offset: 0x00058608
		public RemovePerfCountersCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x0005A417 File Offset: 0x00058617
		public virtual RemovePerfCountersCommand SetParameters(RemovePerfCountersCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002FA RID: 762
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170018F0 RID: 6384
			// (set) Token: 0x0600332C RID: 13100 RVA: 0x0005A421 File Offset: 0x00058621
			public virtual string DefinitionFileName
			{
				set
				{
					base.PowerSharpParameters["DefinitionFileName"] = value;
				}
			}

			// Token: 0x170018F1 RID: 6385
			// (set) Token: 0x0600332D RID: 13101 RVA: 0x0005A434 File Offset: 0x00058634
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170018F2 RID: 6386
			// (set) Token: 0x0600332E RID: 13102 RVA: 0x0005A44C File Offset: 0x0005864C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170018F3 RID: 6387
			// (set) Token: 0x0600332F RID: 13103 RVA: 0x0005A464 File Offset: 0x00058664
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170018F4 RID: 6388
			// (set) Token: 0x06003330 RID: 13104 RVA: 0x0005A47C File Offset: 0x0005867C
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
