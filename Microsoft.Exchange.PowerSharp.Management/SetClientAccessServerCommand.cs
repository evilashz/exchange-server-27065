using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000653 RID: 1619
	public class SetClientAccessServerCommand : SyntheticCommandWithPipelineInputNoOutput<ClientAccessServer>
	{
		// Token: 0x06005162 RID: 20834 RVA: 0x000809DC File Offset: 0x0007EBDC
		private SetClientAccessServerCommand() : base("Set-ClientAccessServer")
		{
		}

		// Token: 0x06005163 RID: 20835 RVA: 0x000809E9 File Offset: 0x0007EBE9
		public SetClientAccessServerCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005164 RID: 20836 RVA: 0x000809F8 File Offset: 0x0007EBF8
		public virtual SetClientAccessServerCommand SetParameters(SetClientAccessServerCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005165 RID: 20837 RVA: 0x00080A02 File Offset: 0x0007EC02
		public virtual SetClientAccessServerCommand SetParameters(SetClientAccessServerCommand.AlternateServiceAccountParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005166 RID: 20838 RVA: 0x00080A0C File Offset: 0x0007EC0C
		public virtual SetClientAccessServerCommand SetParameters(SetClientAccessServerCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000654 RID: 1620
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003075 RID: 12405
			// (set) Token: 0x06005167 RID: 20839 RVA: 0x00080A16 File Offset: 0x0007EC16
			public virtual Uri AutoDiscoverServiceInternalUri
			{
				set
				{
					base.PowerSharpParameters["AutoDiscoverServiceInternalUri"] = value;
				}
			}

			// Token: 0x17003076 RID: 12406
			// (set) Token: 0x06005168 RID: 20840 RVA: 0x00080A29 File Offset: 0x0007EC29
			public virtual MultiValuedProperty<string> AutoDiscoverSiteScope
			{
				set
				{
					base.PowerSharpParameters["AutoDiscoverSiteScope"] = value;
				}
			}

			// Token: 0x17003077 RID: 12407
			// (set) Token: 0x06005169 RID: 20841 RVA: 0x00080A3C File Offset: 0x0007EC3C
			public virtual ClientAccessArrayIdParameter Array
			{
				set
				{
					base.PowerSharpParameters["Array"] = value;
				}
			}

			// Token: 0x17003078 RID: 12408
			// (set) Token: 0x0600516A RID: 20842 RVA: 0x00080A4F File Offset: 0x0007EC4F
			public virtual ClientAccessServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17003079 RID: 12409
			// (set) Token: 0x0600516B RID: 20843 RVA: 0x00080A62 File Offset: 0x0007EC62
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700307A RID: 12410
			// (set) Token: 0x0600516C RID: 20844 RVA: 0x00080A75 File Offset: 0x0007EC75
			public virtual bool IsOutOfService
			{
				set
				{
					base.PowerSharpParameters["IsOutOfService"] = value;
				}
			}

			// Token: 0x1700307B RID: 12411
			// (set) Token: 0x0600516D RID: 20845 RVA: 0x00080A8D File Offset: 0x0007EC8D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700307C RID: 12412
			// (set) Token: 0x0600516E RID: 20846 RVA: 0x00080AA5 File Offset: 0x0007ECA5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700307D RID: 12413
			// (set) Token: 0x0600516F RID: 20847 RVA: 0x00080ABD File Offset: 0x0007ECBD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700307E RID: 12414
			// (set) Token: 0x06005170 RID: 20848 RVA: 0x00080AD5 File Offset: 0x0007ECD5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700307F RID: 12415
			// (set) Token: 0x06005171 RID: 20849 RVA: 0x00080AED File Offset: 0x0007ECED
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000655 RID: 1621
		public class AlternateServiceAccountParameters : ParametersBase
		{
			// Token: 0x17003080 RID: 12416
			// (set) Token: 0x06005173 RID: 20851 RVA: 0x00080B0D File Offset: 0x0007ED0D
			public virtual PSCredential AlternateServiceAccountCredential
			{
				set
				{
					base.PowerSharpParameters["AlternateServiceAccountCredential"] = value;
				}
			}

			// Token: 0x17003081 RID: 12417
			// (set) Token: 0x06005174 RID: 20852 RVA: 0x00080B20 File Offset: 0x0007ED20
			public virtual SwitchParameter CleanUpInvalidAlternateServiceAccountCredentials
			{
				set
				{
					base.PowerSharpParameters["CleanUpInvalidAlternateServiceAccountCredentials"] = value;
				}
			}

			// Token: 0x17003082 RID: 12418
			// (set) Token: 0x06005175 RID: 20853 RVA: 0x00080B38 File Offset: 0x0007ED38
			public virtual SwitchParameter RemoveAlternateServiceAccountCredentials
			{
				set
				{
					base.PowerSharpParameters["RemoveAlternateServiceAccountCredentials"] = value;
				}
			}

			// Token: 0x17003083 RID: 12419
			// (set) Token: 0x06005176 RID: 20854 RVA: 0x00080B50 File Offset: 0x0007ED50
			public virtual ClientAccessServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17003084 RID: 12420
			// (set) Token: 0x06005177 RID: 20855 RVA: 0x00080B63 File Offset: 0x0007ED63
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003085 RID: 12421
			// (set) Token: 0x06005178 RID: 20856 RVA: 0x00080B76 File Offset: 0x0007ED76
			public virtual bool IsOutOfService
			{
				set
				{
					base.PowerSharpParameters["IsOutOfService"] = value;
				}
			}

			// Token: 0x17003086 RID: 12422
			// (set) Token: 0x06005179 RID: 20857 RVA: 0x00080B8E File Offset: 0x0007ED8E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003087 RID: 12423
			// (set) Token: 0x0600517A RID: 20858 RVA: 0x00080BA6 File Offset: 0x0007EDA6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003088 RID: 12424
			// (set) Token: 0x0600517B RID: 20859 RVA: 0x00080BBE File Offset: 0x0007EDBE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003089 RID: 12425
			// (set) Token: 0x0600517C RID: 20860 RVA: 0x00080BD6 File Offset: 0x0007EDD6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700308A RID: 12426
			// (set) Token: 0x0600517D RID: 20861 RVA: 0x00080BEE File Offset: 0x0007EDEE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000656 RID: 1622
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700308B RID: 12427
			// (set) Token: 0x0600517F RID: 20863 RVA: 0x00080C0E File Offset: 0x0007EE0E
			public virtual ClientAccessServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700308C RID: 12428
			// (set) Token: 0x06005180 RID: 20864 RVA: 0x00080C21 File Offset: 0x0007EE21
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700308D RID: 12429
			// (set) Token: 0x06005181 RID: 20865 RVA: 0x00080C34 File Offset: 0x0007EE34
			public virtual bool IsOutOfService
			{
				set
				{
					base.PowerSharpParameters["IsOutOfService"] = value;
				}
			}

			// Token: 0x1700308E RID: 12430
			// (set) Token: 0x06005182 RID: 20866 RVA: 0x00080C4C File Offset: 0x0007EE4C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700308F RID: 12431
			// (set) Token: 0x06005183 RID: 20867 RVA: 0x00080C64 File Offset: 0x0007EE64
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003090 RID: 12432
			// (set) Token: 0x06005184 RID: 20868 RVA: 0x00080C7C File Offset: 0x0007EE7C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003091 RID: 12433
			// (set) Token: 0x06005185 RID: 20869 RVA: 0x00080C94 File Offset: 0x0007EE94
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003092 RID: 12434
			// (set) Token: 0x06005186 RID: 20870 RVA: 0x00080CAC File Offset: 0x0007EEAC
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
