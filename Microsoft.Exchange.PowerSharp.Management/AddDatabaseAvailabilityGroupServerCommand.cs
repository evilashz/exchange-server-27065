using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200052F RID: 1327
	public class AddDatabaseAvailabilityGroupServerCommand : SyntheticCommandWithPipelineInput<DatabaseAvailabilityGroup, DatabaseAvailabilityGroup>
	{
		// Token: 0x0600473D RID: 18237 RVA: 0x00073E8E File Offset: 0x0007208E
		private AddDatabaseAvailabilityGroupServerCommand() : base("Add-DatabaseAvailabilityGroupServer")
		{
		}

		// Token: 0x0600473E RID: 18238 RVA: 0x00073E9B File Offset: 0x0007209B
		public AddDatabaseAvailabilityGroupServerCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600473F RID: 18239 RVA: 0x00073EAA File Offset: 0x000720AA
		public virtual AddDatabaseAvailabilityGroupServerCommand SetParameters(AddDatabaseAvailabilityGroupServerCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004740 RID: 18240 RVA: 0x00073EB4 File Offset: 0x000720B4
		public virtual AddDatabaseAvailabilityGroupServerCommand SetParameters(AddDatabaseAvailabilityGroupServerCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000530 RID: 1328
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002898 RID: 10392
			// (set) Token: 0x06004741 RID: 18241 RVA: 0x00073EBE File Offset: 0x000720BE
			public virtual ServerIdParameter MailboxServer
			{
				set
				{
					base.PowerSharpParameters["MailboxServer"] = value;
				}
			}

			// Token: 0x17002899 RID: 10393
			// (set) Token: 0x06004742 RID: 18242 RVA: 0x00073ED1 File Offset: 0x000720D1
			public virtual DatabaseAvailabilityGroupIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700289A RID: 10394
			// (set) Token: 0x06004743 RID: 18243 RVA: 0x00073EE4 File Offset: 0x000720E4
			public virtual SwitchParameter SkipDagValidation
			{
				set
				{
					base.PowerSharpParameters["SkipDagValidation"] = value;
				}
			}

			// Token: 0x1700289B RID: 10395
			// (set) Token: 0x06004744 RID: 18244 RVA: 0x00073EFC File Offset: 0x000720FC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700289C RID: 10396
			// (set) Token: 0x06004745 RID: 18245 RVA: 0x00073F0F File Offset: 0x0007210F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700289D RID: 10397
			// (set) Token: 0x06004746 RID: 18246 RVA: 0x00073F27 File Offset: 0x00072127
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700289E RID: 10398
			// (set) Token: 0x06004747 RID: 18247 RVA: 0x00073F3F File Offset: 0x0007213F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700289F RID: 10399
			// (set) Token: 0x06004748 RID: 18248 RVA: 0x00073F57 File Offset: 0x00072157
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170028A0 RID: 10400
			// (set) Token: 0x06004749 RID: 18249 RVA: 0x00073F6F File Offset: 0x0007216F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000531 RID: 1329
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170028A1 RID: 10401
			// (set) Token: 0x0600474B RID: 18251 RVA: 0x00073F8F File Offset: 0x0007218F
			public virtual SwitchParameter SkipDagValidation
			{
				set
				{
					base.PowerSharpParameters["SkipDagValidation"] = value;
				}
			}

			// Token: 0x170028A2 RID: 10402
			// (set) Token: 0x0600474C RID: 18252 RVA: 0x00073FA7 File Offset: 0x000721A7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170028A3 RID: 10403
			// (set) Token: 0x0600474D RID: 18253 RVA: 0x00073FBA File Offset: 0x000721BA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170028A4 RID: 10404
			// (set) Token: 0x0600474E RID: 18254 RVA: 0x00073FD2 File Offset: 0x000721D2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170028A5 RID: 10405
			// (set) Token: 0x0600474F RID: 18255 RVA: 0x00073FEA File Offset: 0x000721EA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170028A6 RID: 10406
			// (set) Token: 0x06004750 RID: 18256 RVA: 0x00074002 File Offset: 0x00072202
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170028A7 RID: 10407
			// (set) Token: 0x06004751 RID: 18257 RVA: 0x0007401A File Offset: 0x0007221A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
