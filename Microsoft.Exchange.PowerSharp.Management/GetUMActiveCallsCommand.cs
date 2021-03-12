using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B3F RID: 2879
	public class GetUMActiveCallsCommand : SyntheticCommandWithPipelineInputNoOutput<UMServer>
	{
		// Token: 0x06008C79 RID: 35961 RVA: 0x000CE152 File Offset: 0x000CC352
		private GetUMActiveCallsCommand() : base("Get-UMActiveCalls")
		{
		}

		// Token: 0x06008C7A RID: 35962 RVA: 0x000CE15F File Offset: 0x000CC35F
		public GetUMActiveCallsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008C7B RID: 35963 RVA: 0x000CE16E File Offset: 0x000CC36E
		public virtual GetUMActiveCallsCommand SetParameters(GetUMActiveCallsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008C7C RID: 35964 RVA: 0x000CE178 File Offset: 0x000CC378
		public virtual GetUMActiveCallsCommand SetParameters(GetUMActiveCallsCommand.ServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008C7D RID: 35965 RVA: 0x000CE182 File Offset: 0x000CC382
		public virtual GetUMActiveCallsCommand SetParameters(GetUMActiveCallsCommand.ServerInstanceParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008C7E RID: 35966 RVA: 0x000CE18C File Offset: 0x000CC38C
		public virtual GetUMActiveCallsCommand SetParameters(GetUMActiveCallsCommand.DialPlanParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008C7F RID: 35967 RVA: 0x000CE196 File Offset: 0x000CC396
		public virtual GetUMActiveCallsCommand SetParameters(GetUMActiveCallsCommand.UMIPGatewayParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B40 RID: 2880
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170061B4 RID: 25012
			// (set) Token: 0x06008C80 RID: 35968 RVA: 0x000CE1A0 File Offset: 0x000CC3A0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170061B5 RID: 25013
			// (set) Token: 0x06008C81 RID: 35969 RVA: 0x000CE1B3 File Offset: 0x000CC3B3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170061B6 RID: 25014
			// (set) Token: 0x06008C82 RID: 35970 RVA: 0x000CE1CB File Offset: 0x000CC3CB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170061B7 RID: 25015
			// (set) Token: 0x06008C83 RID: 35971 RVA: 0x000CE1E3 File Offset: 0x000CC3E3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170061B8 RID: 25016
			// (set) Token: 0x06008C84 RID: 35972 RVA: 0x000CE1FB File Offset: 0x000CC3FB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000B41 RID: 2881
		public class ServerParameters : ParametersBase
		{
			// Token: 0x170061B9 RID: 25017
			// (set) Token: 0x06008C86 RID: 35974 RVA: 0x000CE21B File Offset: 0x000CC41B
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170061BA RID: 25018
			// (set) Token: 0x06008C87 RID: 35975 RVA: 0x000CE22E File Offset: 0x000CC42E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170061BB RID: 25019
			// (set) Token: 0x06008C88 RID: 35976 RVA: 0x000CE241 File Offset: 0x000CC441
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170061BC RID: 25020
			// (set) Token: 0x06008C89 RID: 35977 RVA: 0x000CE259 File Offset: 0x000CC459
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170061BD RID: 25021
			// (set) Token: 0x06008C8A RID: 35978 RVA: 0x000CE271 File Offset: 0x000CC471
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170061BE RID: 25022
			// (set) Token: 0x06008C8B RID: 35979 RVA: 0x000CE289 File Offset: 0x000CC489
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000B42 RID: 2882
		public class ServerInstanceParameters : ParametersBase
		{
			// Token: 0x170061BF RID: 25023
			// (set) Token: 0x06008C8D RID: 35981 RVA: 0x000CE2A9 File Offset: 0x000CC4A9
			public virtual UMServer InstanceServer
			{
				set
				{
					base.PowerSharpParameters["InstanceServer"] = value;
				}
			}

			// Token: 0x170061C0 RID: 25024
			// (set) Token: 0x06008C8E RID: 35982 RVA: 0x000CE2BC File Offset: 0x000CC4BC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170061C1 RID: 25025
			// (set) Token: 0x06008C8F RID: 35983 RVA: 0x000CE2CF File Offset: 0x000CC4CF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170061C2 RID: 25026
			// (set) Token: 0x06008C90 RID: 35984 RVA: 0x000CE2E7 File Offset: 0x000CC4E7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170061C3 RID: 25027
			// (set) Token: 0x06008C91 RID: 35985 RVA: 0x000CE2FF File Offset: 0x000CC4FF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170061C4 RID: 25028
			// (set) Token: 0x06008C92 RID: 35986 RVA: 0x000CE317 File Offset: 0x000CC517
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000B43 RID: 2883
		public class DialPlanParameters : ParametersBase
		{
			// Token: 0x170061C5 RID: 25029
			// (set) Token: 0x06008C94 RID: 35988 RVA: 0x000CE337 File Offset: 0x000CC537
			public virtual string DialPlan
			{
				set
				{
					base.PowerSharpParameters["DialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170061C6 RID: 25030
			// (set) Token: 0x06008C95 RID: 35989 RVA: 0x000CE355 File Offset: 0x000CC555
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170061C7 RID: 25031
			// (set) Token: 0x06008C96 RID: 35990 RVA: 0x000CE368 File Offset: 0x000CC568
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170061C8 RID: 25032
			// (set) Token: 0x06008C97 RID: 35991 RVA: 0x000CE380 File Offset: 0x000CC580
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170061C9 RID: 25033
			// (set) Token: 0x06008C98 RID: 35992 RVA: 0x000CE398 File Offset: 0x000CC598
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170061CA RID: 25034
			// (set) Token: 0x06008C99 RID: 35993 RVA: 0x000CE3B0 File Offset: 0x000CC5B0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000B44 RID: 2884
		public class UMIPGatewayParameters : ParametersBase
		{
			// Token: 0x170061CB RID: 25035
			// (set) Token: 0x06008C9B RID: 35995 RVA: 0x000CE3D0 File Offset: 0x000CC5D0
			public virtual string IPGateway
			{
				set
				{
					base.PowerSharpParameters["IPGateway"] = ((value != null) ? new UMIPGatewayIdParameter(value) : null);
				}
			}

			// Token: 0x170061CC RID: 25036
			// (set) Token: 0x06008C9C RID: 35996 RVA: 0x000CE3EE File Offset: 0x000CC5EE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170061CD RID: 25037
			// (set) Token: 0x06008C9D RID: 35997 RVA: 0x000CE401 File Offset: 0x000CC601
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170061CE RID: 25038
			// (set) Token: 0x06008C9E RID: 35998 RVA: 0x000CE419 File Offset: 0x000CC619
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170061CF RID: 25039
			// (set) Token: 0x06008C9F RID: 35999 RVA: 0x000CE431 File Offset: 0x000CC631
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170061D0 RID: 25040
			// (set) Token: 0x06008CA0 RID: 36000 RVA: 0x000CE449 File Offset: 0x000CC649
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
