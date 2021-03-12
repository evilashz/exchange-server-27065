using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.StoreTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000438 RID: 1080
	public class GetCalendarDiagnosticAnalysisCommand : SyntheticCommandWithPipelineInputNoOutput<CalendarLog>
	{
		// Token: 0x06003EAE RID: 16046 RVA: 0x000691A6 File Offset: 0x000673A6
		private GetCalendarDiagnosticAnalysisCommand() : base("Get-CalendarDiagnosticAnalysis")
		{
		}

		// Token: 0x06003EAF RID: 16047 RVA: 0x000691B3 File Offset: 0x000673B3
		public GetCalendarDiagnosticAnalysisCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003EB0 RID: 16048 RVA: 0x000691C2 File Offset: 0x000673C2
		public virtual GetCalendarDiagnosticAnalysisCommand SetParameters(GetCalendarDiagnosticAnalysisCommand.DefaultSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003EB1 RID: 16049 RVA: 0x000691CC File Offset: 0x000673CC
		public virtual GetCalendarDiagnosticAnalysisCommand SetParameters(GetCalendarDiagnosticAnalysisCommand.LocationSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003EB2 RID: 16050 RVA: 0x000691D6 File Offset: 0x000673D6
		public virtual GetCalendarDiagnosticAnalysisCommand SetParameters(GetCalendarDiagnosticAnalysisCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000439 RID: 1081
		public class DefaultSetParameters : ParametersBase
		{
			// Token: 0x170021F7 RID: 8695
			// (set) Token: 0x06003EB3 RID: 16051 RVA: 0x000691E0 File Offset: 0x000673E0
			public virtual CalendarLog CalendarLogs
			{
				set
				{
					base.PowerSharpParameters["CalendarLogs"] = value;
				}
			}

			// Token: 0x170021F8 RID: 8696
			// (set) Token: 0x06003EB4 RID: 16052 RVA: 0x000691F3 File Offset: 0x000673F3
			public virtual string GlobalObjectId
			{
				set
				{
					base.PowerSharpParameters["GlobalObjectId"] = value;
				}
			}

			// Token: 0x170021F9 RID: 8697
			// (set) Token: 0x06003EB5 RID: 16053 RVA: 0x00069206 File Offset: 0x00067406
			public virtual AnalysisDetailLevel DetailLevel
			{
				set
				{
					base.PowerSharpParameters["DetailLevel"] = value;
				}
			}

			// Token: 0x170021FA RID: 8698
			// (set) Token: 0x06003EB6 RID: 16054 RVA: 0x0006921E File Offset: 0x0006741E
			public virtual OutputType OutputAs
			{
				set
				{
					base.PowerSharpParameters["OutputAs"] = value;
				}
			}

			// Token: 0x170021FB RID: 8699
			// (set) Token: 0x06003EB7 RID: 16055 RVA: 0x00069236 File Offset: 0x00067436
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170021FC RID: 8700
			// (set) Token: 0x06003EB8 RID: 16056 RVA: 0x0006924E File Offset: 0x0006744E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170021FD RID: 8701
			// (set) Token: 0x06003EB9 RID: 16057 RVA: 0x00069266 File Offset: 0x00067466
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170021FE RID: 8702
			// (set) Token: 0x06003EBA RID: 16058 RVA: 0x0006927E File Offset: 0x0006747E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200043A RID: 1082
		public class LocationSetParameters : ParametersBase
		{
			// Token: 0x170021FF RID: 8703
			// (set) Token: 0x06003EBC RID: 16060 RVA: 0x0006929E File Offset: 0x0006749E
			public virtual string LogLocation
			{
				set
				{
					base.PowerSharpParameters["LogLocation"] = value;
				}
			}

			// Token: 0x17002200 RID: 8704
			// (set) Token: 0x06003EBD RID: 16061 RVA: 0x000692B1 File Offset: 0x000674B1
			public virtual string GlobalObjectId
			{
				set
				{
					base.PowerSharpParameters["GlobalObjectId"] = value;
				}
			}

			// Token: 0x17002201 RID: 8705
			// (set) Token: 0x06003EBE RID: 16062 RVA: 0x000692C4 File Offset: 0x000674C4
			public virtual AnalysisDetailLevel DetailLevel
			{
				set
				{
					base.PowerSharpParameters["DetailLevel"] = value;
				}
			}

			// Token: 0x17002202 RID: 8706
			// (set) Token: 0x06003EBF RID: 16063 RVA: 0x000692DC File Offset: 0x000674DC
			public virtual OutputType OutputAs
			{
				set
				{
					base.PowerSharpParameters["OutputAs"] = value;
				}
			}

			// Token: 0x17002203 RID: 8707
			// (set) Token: 0x06003EC0 RID: 16064 RVA: 0x000692F4 File Offset: 0x000674F4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002204 RID: 8708
			// (set) Token: 0x06003EC1 RID: 16065 RVA: 0x0006930C File Offset: 0x0006750C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002205 RID: 8709
			// (set) Token: 0x06003EC2 RID: 16066 RVA: 0x00069324 File Offset: 0x00067524
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002206 RID: 8710
			// (set) Token: 0x06003EC3 RID: 16067 RVA: 0x0006933C File Offset: 0x0006753C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200043B RID: 1083
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002207 RID: 8711
			// (set) Token: 0x06003EC5 RID: 16069 RVA: 0x0006935C File Offset: 0x0006755C
			public virtual string GlobalObjectId
			{
				set
				{
					base.PowerSharpParameters["GlobalObjectId"] = value;
				}
			}

			// Token: 0x17002208 RID: 8712
			// (set) Token: 0x06003EC6 RID: 16070 RVA: 0x0006936F File Offset: 0x0006756F
			public virtual AnalysisDetailLevel DetailLevel
			{
				set
				{
					base.PowerSharpParameters["DetailLevel"] = value;
				}
			}

			// Token: 0x17002209 RID: 8713
			// (set) Token: 0x06003EC7 RID: 16071 RVA: 0x00069387 File Offset: 0x00067587
			public virtual OutputType OutputAs
			{
				set
				{
					base.PowerSharpParameters["OutputAs"] = value;
				}
			}

			// Token: 0x1700220A RID: 8714
			// (set) Token: 0x06003EC8 RID: 16072 RVA: 0x0006939F File Offset: 0x0006759F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700220B RID: 8715
			// (set) Token: 0x06003EC9 RID: 16073 RVA: 0x000693B7 File Offset: 0x000675B7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700220C RID: 8716
			// (set) Token: 0x06003ECA RID: 16074 RVA: 0x000693CF File Offset: 0x000675CF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700220D RID: 8717
			// (set) Token: 0x06003ECB RID: 16075 RVA: 0x000693E7 File Offset: 0x000675E7
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
