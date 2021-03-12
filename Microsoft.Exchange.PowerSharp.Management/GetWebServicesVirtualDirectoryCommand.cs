using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000938 RID: 2360
	public class GetWebServicesVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADWebServicesVirtualDirectory, ADWebServicesVirtualDirectory>
	{
		// Token: 0x060076DC RID: 30428 RVA: 0x000B21E2 File Offset: 0x000B03E2
		private GetWebServicesVirtualDirectoryCommand() : base("Get-WebServicesVirtualDirectory")
		{
		}

		// Token: 0x060076DD RID: 30429 RVA: 0x000B21EF File Offset: 0x000B03EF
		public GetWebServicesVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060076DE RID: 30430 RVA: 0x000B21FE File Offset: 0x000B03FE
		public virtual GetWebServicesVirtualDirectoryCommand SetParameters(GetWebServicesVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060076DF RID: 30431 RVA: 0x000B2208 File Offset: 0x000B0408
		public virtual GetWebServicesVirtualDirectoryCommand SetParameters(GetWebServicesVirtualDirectoryCommand.ServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060076E0 RID: 30432 RVA: 0x000B2212 File Offset: 0x000B0412
		public virtual GetWebServicesVirtualDirectoryCommand SetParameters(GetWebServicesVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000939 RID: 2361
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005025 RID: 20517
			// (set) Token: 0x060076E1 RID: 30433 RVA: 0x000B221C File Offset: 0x000B041C
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17005026 RID: 20518
			// (set) Token: 0x060076E2 RID: 30434 RVA: 0x000B2234 File Offset: 0x000B0434
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17005027 RID: 20519
			// (set) Token: 0x060076E3 RID: 30435 RVA: 0x000B224C File Offset: 0x000B044C
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17005028 RID: 20520
			// (set) Token: 0x060076E4 RID: 30436 RVA: 0x000B2264 File Offset: 0x000B0464
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005029 RID: 20521
			// (set) Token: 0x060076E5 RID: 30437 RVA: 0x000B2277 File Offset: 0x000B0477
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700502A RID: 20522
			// (set) Token: 0x060076E6 RID: 30438 RVA: 0x000B228F File Offset: 0x000B048F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700502B RID: 20523
			// (set) Token: 0x060076E7 RID: 30439 RVA: 0x000B22A7 File Offset: 0x000B04A7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700502C RID: 20524
			// (set) Token: 0x060076E8 RID: 30440 RVA: 0x000B22BF File Offset: 0x000B04BF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200093A RID: 2362
		public class ServerParameters : ParametersBase
		{
			// Token: 0x1700502D RID: 20525
			// (set) Token: 0x060076EA RID: 30442 RVA: 0x000B22DF File Offset: 0x000B04DF
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x1700502E RID: 20526
			// (set) Token: 0x060076EB RID: 30443 RVA: 0x000B22F2 File Offset: 0x000B04F2
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x1700502F RID: 20527
			// (set) Token: 0x060076EC RID: 30444 RVA: 0x000B230A File Offset: 0x000B050A
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17005030 RID: 20528
			// (set) Token: 0x060076ED RID: 30445 RVA: 0x000B2322 File Offset: 0x000B0522
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17005031 RID: 20529
			// (set) Token: 0x060076EE RID: 30446 RVA: 0x000B233A File Offset: 0x000B053A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005032 RID: 20530
			// (set) Token: 0x060076EF RID: 30447 RVA: 0x000B234D File Offset: 0x000B054D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005033 RID: 20531
			// (set) Token: 0x060076F0 RID: 30448 RVA: 0x000B2365 File Offset: 0x000B0565
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005034 RID: 20532
			// (set) Token: 0x060076F1 RID: 30449 RVA: 0x000B237D File Offset: 0x000B057D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005035 RID: 20533
			// (set) Token: 0x060076F2 RID: 30450 RVA: 0x000B2395 File Offset: 0x000B0595
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200093B RID: 2363
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005036 RID: 20534
			// (set) Token: 0x060076F4 RID: 30452 RVA: 0x000B23B5 File Offset: 0x000B05B5
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17005037 RID: 20535
			// (set) Token: 0x060076F5 RID: 30453 RVA: 0x000B23C8 File Offset: 0x000B05C8
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17005038 RID: 20536
			// (set) Token: 0x060076F6 RID: 30454 RVA: 0x000B23E0 File Offset: 0x000B05E0
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17005039 RID: 20537
			// (set) Token: 0x060076F7 RID: 30455 RVA: 0x000B23F8 File Offset: 0x000B05F8
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x1700503A RID: 20538
			// (set) Token: 0x060076F8 RID: 30456 RVA: 0x000B2410 File Offset: 0x000B0610
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700503B RID: 20539
			// (set) Token: 0x060076F9 RID: 30457 RVA: 0x000B2423 File Offset: 0x000B0623
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700503C RID: 20540
			// (set) Token: 0x060076FA RID: 30458 RVA: 0x000B243B File Offset: 0x000B063B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700503D RID: 20541
			// (set) Token: 0x060076FB RID: 30459 RVA: 0x000B2453 File Offset: 0x000B0653
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700503E RID: 20542
			// (set) Token: 0x060076FC RID: 30460 RVA: 0x000B246B File Offset: 0x000B066B
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
