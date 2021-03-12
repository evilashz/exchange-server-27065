using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000636 RID: 1590
	public class GetFrontendTransportServiceCommand : SyntheticCommandWithPipelineInput<FrontendTransportServer, FrontendTransportServer>
	{
		// Token: 0x060050B6 RID: 20662 RVA: 0x0007FD35 File Offset: 0x0007DF35
		private GetFrontendTransportServiceCommand() : base("Get-FrontendTransportService")
		{
		}

		// Token: 0x060050B7 RID: 20663 RVA: 0x0007FD42 File Offset: 0x0007DF42
		public GetFrontendTransportServiceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060050B8 RID: 20664 RVA: 0x0007FD51 File Offset: 0x0007DF51
		public virtual GetFrontendTransportServiceCommand SetParameters(GetFrontendTransportServiceCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060050B9 RID: 20665 RVA: 0x0007FD5B File Offset: 0x0007DF5B
		public virtual GetFrontendTransportServiceCommand SetParameters(GetFrontendTransportServiceCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000637 RID: 1591
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003003 RID: 12291
			// (set) Token: 0x060050BA RID: 20666 RVA: 0x0007FD65 File Offset: 0x0007DF65
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003004 RID: 12292
			// (set) Token: 0x060050BB RID: 20667 RVA: 0x0007FD78 File Offset: 0x0007DF78
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003005 RID: 12293
			// (set) Token: 0x060050BC RID: 20668 RVA: 0x0007FD90 File Offset: 0x0007DF90
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003006 RID: 12294
			// (set) Token: 0x060050BD RID: 20669 RVA: 0x0007FDA8 File Offset: 0x0007DFA8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003007 RID: 12295
			// (set) Token: 0x060050BE RID: 20670 RVA: 0x0007FDC0 File Offset: 0x0007DFC0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000638 RID: 1592
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003008 RID: 12296
			// (set) Token: 0x060050C0 RID: 20672 RVA: 0x0007FDE0 File Offset: 0x0007DFE0
			public virtual FrontendTransportServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17003009 RID: 12297
			// (set) Token: 0x060050C1 RID: 20673 RVA: 0x0007FDF3 File Offset: 0x0007DFF3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700300A RID: 12298
			// (set) Token: 0x060050C2 RID: 20674 RVA: 0x0007FE06 File Offset: 0x0007E006
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700300B RID: 12299
			// (set) Token: 0x060050C3 RID: 20675 RVA: 0x0007FE1E File Offset: 0x0007E01E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700300C RID: 12300
			// (set) Token: 0x060050C4 RID: 20676 RVA: 0x0007FE36 File Offset: 0x0007E036
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700300D RID: 12301
			// (set) Token: 0x060050C5 RID: 20677 RVA: 0x0007FE4E File Offset: 0x0007E04E
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
