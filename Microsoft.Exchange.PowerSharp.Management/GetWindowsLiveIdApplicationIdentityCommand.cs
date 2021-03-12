using System;
using System.Management.Automation;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BB6 RID: 2998
	public class GetWindowsLiveIdApplicationIdentityCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x06009113 RID: 37139 RVA: 0x000D4084 File Offset: 0x000D2284
		private GetWindowsLiveIdApplicationIdentityCommand() : base("Get-WindowsLiveIdApplicationIdentity")
		{
		}

		// Token: 0x06009114 RID: 37140 RVA: 0x000D4091 File Offset: 0x000D2291
		public GetWindowsLiveIdApplicationIdentityCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009115 RID: 37141 RVA: 0x000D40A0 File Offset: 0x000D22A0
		public virtual GetWindowsLiveIdApplicationIdentityCommand SetParameters(GetWindowsLiveIdApplicationIdentityCommand.UriParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009116 RID: 37142 RVA: 0x000D40AA File Offset: 0x000D22AA
		public virtual GetWindowsLiveIdApplicationIdentityCommand SetParameters(GetWindowsLiveIdApplicationIdentityCommand.AppIDParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BB7 RID: 2999
		public class UriParameters : ParametersBase
		{
			// Token: 0x17006560 RID: 25952
			// (set) Token: 0x06009117 RID: 37143 RVA: 0x000D40B4 File Offset: 0x000D22B4
			public virtual string Uri
			{
				set
				{
					base.PowerSharpParameters["Uri"] = value;
				}
			}

			// Token: 0x17006561 RID: 25953
			// (set) Token: 0x06009118 RID: 37144 RVA: 0x000D40C7 File Offset: 0x000D22C7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006562 RID: 25954
			// (set) Token: 0x06009119 RID: 37145 RVA: 0x000D40DF File Offset: 0x000D22DF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006563 RID: 25955
			// (set) Token: 0x0600911A RID: 37146 RVA: 0x000D40F7 File Offset: 0x000D22F7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006564 RID: 25956
			// (set) Token: 0x0600911B RID: 37147 RVA: 0x000D410F File Offset: 0x000D230F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000BB8 RID: 3000
		public class AppIDParameters : ParametersBase
		{
			// Token: 0x17006565 RID: 25957
			// (set) Token: 0x0600911D RID: 37149 RVA: 0x000D412F File Offset: 0x000D232F
			public virtual string AppId
			{
				set
				{
					base.PowerSharpParameters["AppId"] = value;
				}
			}

			// Token: 0x17006566 RID: 25958
			// (set) Token: 0x0600911E RID: 37150 RVA: 0x000D4142 File Offset: 0x000D2342
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006567 RID: 25959
			// (set) Token: 0x0600911F RID: 37151 RVA: 0x000D415A File Offset: 0x000D235A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006568 RID: 25960
			// (set) Token: 0x06009120 RID: 37152 RVA: 0x000D4172 File Offset: 0x000D2372
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006569 RID: 25961
			// (set) Token: 0x06009121 RID: 37153 RVA: 0x000D418A File Offset: 0x000D238A
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
