using System;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000628 RID: 1576
	public class GetDomainControllerCommand : SyntheticCommandWithPipelineInput<ADServer, ADServer>
	{
		// Token: 0x06005064 RID: 20580 RVA: 0x0007F73D File Offset: 0x0007D93D
		private GetDomainControllerCommand() : base("Get-DomainController")
		{
		}

		// Token: 0x06005065 RID: 20581 RVA: 0x0007F74A File Offset: 0x0007D94A
		public GetDomainControllerCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005066 RID: 20582 RVA: 0x0007F759 File Offset: 0x0007D959
		public virtual GetDomainControllerCommand SetParameters(GetDomainControllerCommand.GlobalCatalogParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005067 RID: 20583 RVA: 0x0007F763 File Offset: 0x0007D963
		public virtual GetDomainControllerCommand SetParameters(GetDomainControllerCommand.DomainControllerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005068 RID: 20584 RVA: 0x0007F76D File Offset: 0x0007D96D
		public virtual GetDomainControllerCommand SetParameters(GetDomainControllerCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000629 RID: 1577
		public class GlobalCatalogParameters : ParametersBase
		{
			// Token: 0x17002FCD RID: 12237
			// (set) Token: 0x06005069 RID: 20585 RVA: 0x0007F777 File Offset: 0x0007D977
			public virtual SwitchParameter GlobalCatalog
			{
				set
				{
					base.PowerSharpParameters["GlobalCatalog"] = value;
				}
			}

			// Token: 0x17002FCE RID: 12238
			// (set) Token: 0x0600506A RID: 20586 RVA: 0x0007F78F File Offset: 0x0007D98F
			public virtual Fqdn Forest
			{
				set
				{
					base.PowerSharpParameters["Forest"] = value;
				}
			}

			// Token: 0x17002FCF RID: 12239
			// (set) Token: 0x0600506B RID: 20587 RVA: 0x0007F7A2 File Offset: 0x0007D9A2
			public virtual NetworkCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17002FD0 RID: 12240
			// (set) Token: 0x0600506C RID: 20588 RVA: 0x0007F7B5 File Offset: 0x0007D9B5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002FD1 RID: 12241
			// (set) Token: 0x0600506D RID: 20589 RVA: 0x0007F7CD File Offset: 0x0007D9CD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002FD2 RID: 12242
			// (set) Token: 0x0600506E RID: 20590 RVA: 0x0007F7E5 File Offset: 0x0007D9E5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002FD3 RID: 12243
			// (set) Token: 0x0600506F RID: 20591 RVA: 0x0007F7FD File Offset: 0x0007D9FD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200062A RID: 1578
		public class DomainControllerParameters : ParametersBase
		{
			// Token: 0x17002FD4 RID: 12244
			// (set) Token: 0x06005071 RID: 20593 RVA: 0x0007F81D File Offset: 0x0007DA1D
			public virtual Fqdn DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x17002FD5 RID: 12245
			// (set) Token: 0x06005072 RID: 20594 RVA: 0x0007F830 File Offset: 0x0007DA30
			public virtual NetworkCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17002FD6 RID: 12246
			// (set) Token: 0x06005073 RID: 20595 RVA: 0x0007F843 File Offset: 0x0007DA43
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002FD7 RID: 12247
			// (set) Token: 0x06005074 RID: 20596 RVA: 0x0007F85B File Offset: 0x0007DA5B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002FD8 RID: 12248
			// (set) Token: 0x06005075 RID: 20597 RVA: 0x0007F873 File Offset: 0x0007DA73
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002FD9 RID: 12249
			// (set) Token: 0x06005076 RID: 20598 RVA: 0x0007F88B File Offset: 0x0007DA8B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200062B RID: 1579
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002FDA RID: 12250
			// (set) Token: 0x06005078 RID: 20600 RVA: 0x0007F8AB File Offset: 0x0007DAAB
			public virtual NetworkCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17002FDB RID: 12251
			// (set) Token: 0x06005079 RID: 20601 RVA: 0x0007F8BE File Offset: 0x0007DABE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002FDC RID: 12252
			// (set) Token: 0x0600507A RID: 20602 RVA: 0x0007F8D6 File Offset: 0x0007DAD6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002FDD RID: 12253
			// (set) Token: 0x0600507B RID: 20603 RVA: 0x0007F8EE File Offset: 0x0007DAEE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002FDE RID: 12254
			// (set) Token: 0x0600507C RID: 20604 RVA: 0x0007F906 File Offset: 0x0007DB06
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
