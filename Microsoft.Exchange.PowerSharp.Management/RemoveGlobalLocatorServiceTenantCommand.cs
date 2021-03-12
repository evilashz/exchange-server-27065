using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006C0 RID: 1728
	public class RemoveGlobalLocatorServiceTenantCommand : SyntheticCommandWithPipelineInputNoOutput<SmtpDomain>
	{
		// Token: 0x06005ACF RID: 23247 RVA: 0x0008D86E File Offset: 0x0008BA6E
		private RemoveGlobalLocatorServiceTenantCommand() : base("Remove-GlobalLocatorServiceTenant")
		{
		}

		// Token: 0x06005AD0 RID: 23248 RVA: 0x0008D87B File Offset: 0x0008BA7B
		public RemoveGlobalLocatorServiceTenantCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005AD1 RID: 23249 RVA: 0x0008D88A File Offset: 0x0008BA8A
		public virtual RemoveGlobalLocatorServiceTenantCommand SetParameters(RemoveGlobalLocatorServiceTenantCommand.DomainNameParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005AD2 RID: 23250 RVA: 0x0008D894 File Offset: 0x0008BA94
		public virtual RemoveGlobalLocatorServiceTenantCommand SetParameters(RemoveGlobalLocatorServiceTenantCommand.ExternalDirectoryOrganizationIdParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005AD3 RID: 23251 RVA: 0x0008D89E File Offset: 0x0008BA9E
		public virtual RemoveGlobalLocatorServiceTenantCommand SetParameters(RemoveGlobalLocatorServiceTenantCommand.MsaUserNetIDParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006C1 RID: 1729
		public class DomainNameParameterSetParameters : ParametersBase
		{
			// Token: 0x17003908 RID: 14600
			// (set) Token: 0x06005AD4 RID: 23252 RVA: 0x0008D8A8 File Offset: 0x0008BAA8
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x17003909 RID: 14601
			// (set) Token: 0x06005AD5 RID: 23253 RVA: 0x0008D8BB File Offset: 0x0008BABB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700390A RID: 14602
			// (set) Token: 0x06005AD6 RID: 23254 RVA: 0x0008D8D3 File Offset: 0x0008BAD3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700390B RID: 14603
			// (set) Token: 0x06005AD7 RID: 23255 RVA: 0x0008D8EB File Offset: 0x0008BAEB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700390C RID: 14604
			// (set) Token: 0x06005AD8 RID: 23256 RVA: 0x0008D903 File Offset: 0x0008BB03
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700390D RID: 14605
			// (set) Token: 0x06005AD9 RID: 23257 RVA: 0x0008D91B File Offset: 0x0008BB1B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700390E RID: 14606
			// (set) Token: 0x06005ADA RID: 23258 RVA: 0x0008D933 File Offset: 0x0008BB33
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020006C2 RID: 1730
		public class ExternalDirectoryOrganizationIdParameterSetParameters : ParametersBase
		{
			// Token: 0x1700390F RID: 14607
			// (set) Token: 0x06005ADC RID: 23260 RVA: 0x0008D953 File Offset: 0x0008BB53
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x17003910 RID: 14608
			// (set) Token: 0x06005ADD RID: 23261 RVA: 0x0008D96B File Offset: 0x0008BB6B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003911 RID: 14609
			// (set) Token: 0x06005ADE RID: 23262 RVA: 0x0008D983 File Offset: 0x0008BB83
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003912 RID: 14610
			// (set) Token: 0x06005ADF RID: 23263 RVA: 0x0008D99B File Offset: 0x0008BB9B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003913 RID: 14611
			// (set) Token: 0x06005AE0 RID: 23264 RVA: 0x0008D9B3 File Offset: 0x0008BBB3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003914 RID: 14612
			// (set) Token: 0x06005AE1 RID: 23265 RVA: 0x0008D9CB File Offset: 0x0008BBCB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003915 RID: 14613
			// (set) Token: 0x06005AE2 RID: 23266 RVA: 0x0008D9E3 File Offset: 0x0008BBE3
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020006C3 RID: 1731
		public class MsaUserNetIDParameterSetParameters : ParametersBase
		{
			// Token: 0x17003916 RID: 14614
			// (set) Token: 0x06005AE4 RID: 23268 RVA: 0x0008DA03 File Offset: 0x0008BC03
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x17003917 RID: 14615
			// (set) Token: 0x06005AE5 RID: 23269 RVA: 0x0008DA1B File Offset: 0x0008BC1B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003918 RID: 14616
			// (set) Token: 0x06005AE6 RID: 23270 RVA: 0x0008DA33 File Offset: 0x0008BC33
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003919 RID: 14617
			// (set) Token: 0x06005AE7 RID: 23271 RVA: 0x0008DA4B File Offset: 0x0008BC4B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700391A RID: 14618
			// (set) Token: 0x06005AE8 RID: 23272 RVA: 0x0008DA63 File Offset: 0x0008BC63
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700391B RID: 14619
			// (set) Token: 0x06005AE9 RID: 23273 RVA: 0x0008DA7B File Offset: 0x0008BC7B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700391C RID: 14620
			// (set) Token: 0x06005AEA RID: 23274 RVA: 0x0008DA93 File Offset: 0x0008BC93
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
