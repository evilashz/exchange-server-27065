using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.SystemProbeTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BCB RID: 3019
	public class NewEncryptedSystemProbeGuidCommand : SyntheticCommandWithPipelineInput<SystemProbeData, SystemProbeData>
	{
		// Token: 0x0600919F RID: 37279 RVA: 0x000D4B3C File Offset: 0x000D2D3C
		private NewEncryptedSystemProbeGuidCommand() : base("New-EncryptedSystemProbeGuid")
		{
		}

		// Token: 0x060091A0 RID: 37280 RVA: 0x000D4B49 File Offset: 0x000D2D49
		public NewEncryptedSystemProbeGuidCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060091A1 RID: 37281 RVA: 0x000D4B58 File Offset: 0x000D2D58
		public virtual NewEncryptedSystemProbeGuidCommand SetParameters(NewEncryptedSystemProbeGuidCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BCC RID: 3020
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170065C2 RID: 26050
			// (set) Token: 0x060091A2 RID: 37282 RVA: 0x000D4B62 File Offset: 0x000D2D62
			public virtual Guid? Guid
			{
				set
				{
					base.PowerSharpParameters["Guid"] = value;
				}
			}

			// Token: 0x170065C3 RID: 26051
			// (set) Token: 0x060091A3 RID: 37283 RVA: 0x000D4B7A File Offset: 0x000D2D7A
			public virtual DateTime TimeStamp
			{
				set
				{
					base.PowerSharpParameters["TimeStamp"] = value;
				}
			}

			// Token: 0x170065C4 RID: 26052
			// (set) Token: 0x060091A4 RID: 37284 RVA: 0x000D4B92 File Offset: 0x000D2D92
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170065C5 RID: 26053
			// (set) Token: 0x060091A5 RID: 37285 RVA: 0x000D4BAA File Offset: 0x000D2DAA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170065C6 RID: 26054
			// (set) Token: 0x060091A6 RID: 37286 RVA: 0x000D4BC2 File Offset: 0x000D2DC2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170065C7 RID: 26055
			// (set) Token: 0x060091A7 RID: 37287 RVA: 0x000D4BDA File Offset: 0x000D2DDA
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
