using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000642 RID: 1602
	public class GetTransportServerCommand : SyntheticCommand<object>
	{
		// Token: 0x060050FC RID: 20732 RVA: 0x00080249 File Offset: 0x0007E449
		private GetTransportServerCommand() : base("Get-TransportServer")
		{
		}

		// Token: 0x060050FD RID: 20733 RVA: 0x00080256 File Offset: 0x0007E456
		public GetTransportServerCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060050FE RID: 20734 RVA: 0x00080265 File Offset: 0x0007E465
		public virtual GetTransportServerCommand SetParameters(GetTransportServerCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060050FF RID: 20735 RVA: 0x0008026F File Offset: 0x0007E46F
		public virtual GetTransportServerCommand SetParameters(GetTransportServerCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000643 RID: 1603
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003031 RID: 12337
			// (set) Token: 0x06005100 RID: 20736 RVA: 0x00080279 File Offset: 0x0007E479
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003032 RID: 12338
			// (set) Token: 0x06005101 RID: 20737 RVA: 0x0008028C File Offset: 0x0007E48C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003033 RID: 12339
			// (set) Token: 0x06005102 RID: 20738 RVA: 0x000802A4 File Offset: 0x0007E4A4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003034 RID: 12340
			// (set) Token: 0x06005103 RID: 20739 RVA: 0x000802BC File Offset: 0x0007E4BC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003035 RID: 12341
			// (set) Token: 0x06005104 RID: 20740 RVA: 0x000802D4 File Offset: 0x0007E4D4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000644 RID: 1604
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003036 RID: 12342
			// (set) Token: 0x06005106 RID: 20742 RVA: 0x000802F4 File Offset: 0x0007E4F4
			public virtual TransportServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17003037 RID: 12343
			// (set) Token: 0x06005107 RID: 20743 RVA: 0x00080307 File Offset: 0x0007E507
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003038 RID: 12344
			// (set) Token: 0x06005108 RID: 20744 RVA: 0x0008031A File Offset: 0x0007E51A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003039 RID: 12345
			// (set) Token: 0x06005109 RID: 20745 RVA: 0x00080332 File Offset: 0x0007E532
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700303A RID: 12346
			// (set) Token: 0x0600510A RID: 20746 RVA: 0x0008034A File Offset: 0x0007E54A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700303B RID: 12347
			// (set) Token: 0x0600510B RID: 20747 RVA: 0x00080362 File Offset: 0x0007E562
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
