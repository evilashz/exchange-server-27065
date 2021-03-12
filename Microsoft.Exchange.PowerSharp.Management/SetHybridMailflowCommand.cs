using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000522 RID: 1314
	public class SetHybridMailflowCommand : SyntheticCommand<object>
	{
		// Token: 0x060046AD RID: 18093 RVA: 0x00073341 File Offset: 0x00071541
		private SetHybridMailflowCommand() : base("Set-HybridMailflow")
		{
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x0007334E File Offset: 0x0007154E
		public SetHybridMailflowCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x0007335D File Offset: 0x0007155D
		public virtual SetHybridMailflowCommand SetParameters(SetHybridMailflowCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000523 RID: 1315
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002822 RID: 10274
			// (set) Token: 0x060046B0 RID: 18096 RVA: 0x00073367 File Offset: 0x00071567
			public virtual SmtpDomainWithSubdomains OutboundDomains
			{
				set
				{
					base.PowerSharpParameters["OutboundDomains"] = value;
				}
			}

			// Token: 0x17002823 RID: 10275
			// (set) Token: 0x060046B1 RID: 18097 RVA: 0x0007337A File Offset: 0x0007157A
			public virtual IPRange InboundIPs
			{
				set
				{
					base.PowerSharpParameters["InboundIPs"] = value;
				}
			}

			// Token: 0x17002824 RID: 10276
			// (set) Token: 0x060046B2 RID: 18098 RVA: 0x0007338D File Offset: 0x0007158D
			public virtual Fqdn OnPremisesFQDN
			{
				set
				{
					base.PowerSharpParameters["OnPremisesFQDN"] = value;
				}
			}

			// Token: 0x17002825 RID: 10277
			// (set) Token: 0x060046B3 RID: 18099 RVA: 0x000733A0 File Offset: 0x000715A0
			public virtual string CertificateSubject
			{
				set
				{
					base.PowerSharpParameters["CertificateSubject"] = value;
				}
			}

			// Token: 0x17002826 RID: 10278
			// (set) Token: 0x060046B4 RID: 18100 RVA: 0x000733B3 File Offset: 0x000715B3
			public virtual bool? SecureMailEnabled
			{
				set
				{
					base.PowerSharpParameters["SecureMailEnabled"] = value;
				}
			}

			// Token: 0x17002827 RID: 10279
			// (set) Token: 0x060046B5 RID: 18101 RVA: 0x000733CB File Offset: 0x000715CB
			public virtual bool? CentralizedTransportEnabled
			{
				set
				{
					base.PowerSharpParameters["CentralizedTransportEnabled"] = value;
				}
			}

			// Token: 0x17002828 RID: 10280
			// (set) Token: 0x060046B6 RID: 18102 RVA: 0x000733E3 File Offset: 0x000715E3
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002829 RID: 10281
			// (set) Token: 0x060046B7 RID: 18103 RVA: 0x00073401 File Offset: 0x00071601
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700282A RID: 10282
			// (set) Token: 0x060046B8 RID: 18104 RVA: 0x00073419 File Offset: 0x00071619
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700282B RID: 10283
			// (set) Token: 0x060046B9 RID: 18105 RVA: 0x00073431 File Offset: 0x00071631
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700282C RID: 10284
			// (set) Token: 0x060046BA RID: 18106 RVA: 0x00073449 File Offset: 0x00071649
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700282D RID: 10285
			// (set) Token: 0x060046BB RID: 18107 RVA: 0x00073461 File Offset: 0x00071661
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
