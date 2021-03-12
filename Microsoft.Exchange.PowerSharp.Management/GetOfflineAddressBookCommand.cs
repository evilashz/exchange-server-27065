using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007F1 RID: 2033
	public class GetOfflineAddressBookCommand : SyntheticCommandWithPipelineInput<OfflineAddressBook, OfflineAddressBook>
	{
		// Token: 0x0600650C RID: 25868 RVA: 0x0009A7BE File Offset: 0x000989BE
		private GetOfflineAddressBookCommand() : base("Get-OfflineAddressBook")
		{
		}

		// Token: 0x0600650D RID: 25869 RVA: 0x0009A7CB File Offset: 0x000989CB
		public GetOfflineAddressBookCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600650E RID: 25870 RVA: 0x0009A7DA File Offset: 0x000989DA
		public virtual GetOfflineAddressBookCommand SetParameters(GetOfflineAddressBookCommand.ServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600650F RID: 25871 RVA: 0x0009A7E4 File Offset: 0x000989E4
		public virtual GetOfflineAddressBookCommand SetParameters(GetOfflineAddressBookCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006510 RID: 25872 RVA: 0x0009A7EE File Offset: 0x000989EE
		public virtual GetOfflineAddressBookCommand SetParameters(GetOfflineAddressBookCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007F2 RID: 2034
		public class ServerParameters : ParametersBase
		{
			// Token: 0x170040E3 RID: 16611
			// (set) Token: 0x06006511 RID: 25873 RVA: 0x0009A7F8 File Offset: 0x000989F8
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170040E4 RID: 16612
			// (set) Token: 0x06006512 RID: 25874 RVA: 0x0009A80B File Offset: 0x00098A0B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170040E5 RID: 16613
			// (set) Token: 0x06006513 RID: 25875 RVA: 0x0009A829 File Offset: 0x00098A29
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170040E6 RID: 16614
			// (set) Token: 0x06006514 RID: 25876 RVA: 0x0009A83C File Offset: 0x00098A3C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170040E7 RID: 16615
			// (set) Token: 0x06006515 RID: 25877 RVA: 0x0009A854 File Offset: 0x00098A54
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170040E8 RID: 16616
			// (set) Token: 0x06006516 RID: 25878 RVA: 0x0009A86C File Offset: 0x00098A6C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170040E9 RID: 16617
			// (set) Token: 0x06006517 RID: 25879 RVA: 0x0009A884 File Offset: 0x00098A84
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020007F3 RID: 2035
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170040EA RID: 16618
			// (set) Token: 0x06006519 RID: 25881 RVA: 0x0009A8A4 File Offset: 0x00098AA4
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170040EB RID: 16619
			// (set) Token: 0x0600651A RID: 25882 RVA: 0x0009A8C2 File Offset: 0x00098AC2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170040EC RID: 16620
			// (set) Token: 0x0600651B RID: 25883 RVA: 0x0009A8D5 File Offset: 0x00098AD5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170040ED RID: 16621
			// (set) Token: 0x0600651C RID: 25884 RVA: 0x0009A8ED File Offset: 0x00098AED
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170040EE RID: 16622
			// (set) Token: 0x0600651D RID: 25885 RVA: 0x0009A905 File Offset: 0x00098B05
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170040EF RID: 16623
			// (set) Token: 0x0600651E RID: 25886 RVA: 0x0009A91D File Offset: 0x00098B1D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020007F4 RID: 2036
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170040F0 RID: 16624
			// (set) Token: 0x06006520 RID: 25888 RVA: 0x0009A93D File Offset: 0x00098B3D
			public virtual OfflineAddressBookIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170040F1 RID: 16625
			// (set) Token: 0x06006521 RID: 25889 RVA: 0x0009A950 File Offset: 0x00098B50
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170040F2 RID: 16626
			// (set) Token: 0x06006522 RID: 25890 RVA: 0x0009A96E File Offset: 0x00098B6E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170040F3 RID: 16627
			// (set) Token: 0x06006523 RID: 25891 RVA: 0x0009A981 File Offset: 0x00098B81
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170040F4 RID: 16628
			// (set) Token: 0x06006524 RID: 25892 RVA: 0x0009A999 File Offset: 0x00098B99
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170040F5 RID: 16629
			// (set) Token: 0x06006525 RID: 25893 RVA: 0x0009A9B1 File Offset: 0x00098BB1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170040F6 RID: 16630
			// (set) Token: 0x06006526 RID: 25894 RVA: 0x0009A9C9 File Offset: 0x00098BC9
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
