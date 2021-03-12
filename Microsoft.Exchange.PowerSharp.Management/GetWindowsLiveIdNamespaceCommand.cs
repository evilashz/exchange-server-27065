using System;
using System.Management.Automation;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BB9 RID: 3001
	public class GetWindowsLiveIdNamespaceCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x06009123 RID: 37155 RVA: 0x000D41AA File Offset: 0x000D23AA
		private GetWindowsLiveIdNamespaceCommand() : base("Get-WindowsLiveIdNamespace")
		{
		}

		// Token: 0x06009124 RID: 37156 RVA: 0x000D41B7 File Offset: 0x000D23B7
		public GetWindowsLiveIdNamespaceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009125 RID: 37157 RVA: 0x000D41C6 File Offset: 0x000D23C6
		public virtual GetWindowsLiveIdNamespaceCommand SetParameters(GetWindowsLiveIdNamespaceCommand.NamespaceParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BBA RID: 3002
		public class NamespaceParameters : ParametersBase
		{
			// Token: 0x1700656A RID: 25962
			// (set) Token: 0x06009126 RID: 37158 RVA: 0x000D41D0 File Offset: 0x000D23D0
			public virtual string Namespace
			{
				set
				{
					base.PowerSharpParameters["Namespace"] = value;
				}
			}

			// Token: 0x1700656B RID: 25963
			// (set) Token: 0x06009127 RID: 37159 RVA: 0x000D41E3 File Offset: 0x000D23E3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700656C RID: 25964
			// (set) Token: 0x06009128 RID: 37160 RVA: 0x000D41FB File Offset: 0x000D23FB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700656D RID: 25965
			// (set) Token: 0x06009129 RID: 37161 RVA: 0x000D4213 File Offset: 0x000D2413
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700656E RID: 25966
			// (set) Token: 0x0600912A RID: 37162 RVA: 0x000D422B File Offset: 0x000D242B
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
