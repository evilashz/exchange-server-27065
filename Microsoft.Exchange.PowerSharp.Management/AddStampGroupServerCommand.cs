using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000532 RID: 1330
	public class AddStampGroupServerCommand : SyntheticCommandWithPipelineInput<StampGroup, StampGroup>
	{
		// Token: 0x06004753 RID: 18259 RVA: 0x0007403A File Offset: 0x0007223A
		private AddStampGroupServerCommand() : base("Add-StampGroupServer")
		{
		}

		// Token: 0x06004754 RID: 18260 RVA: 0x00074047 File Offset: 0x00072247
		public AddStampGroupServerCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004755 RID: 18261 RVA: 0x00074056 File Offset: 0x00072256
		public virtual AddStampGroupServerCommand SetParameters(AddStampGroupServerCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004756 RID: 18262 RVA: 0x00074060 File Offset: 0x00072260
		public virtual AddStampGroupServerCommand SetParameters(AddStampGroupServerCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000533 RID: 1331
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170028A8 RID: 10408
			// (set) Token: 0x06004757 RID: 18263 RVA: 0x0007406A File Offset: 0x0007226A
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170028A9 RID: 10409
			// (set) Token: 0x06004758 RID: 18264 RVA: 0x0007407D File Offset: 0x0007227D
			public virtual StampGroupIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170028AA RID: 10410
			// (set) Token: 0x06004759 RID: 18265 RVA: 0x00074090 File Offset: 0x00072290
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170028AB RID: 10411
			// (set) Token: 0x0600475A RID: 18266 RVA: 0x000740A3 File Offset: 0x000722A3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170028AC RID: 10412
			// (set) Token: 0x0600475B RID: 18267 RVA: 0x000740BB File Offset: 0x000722BB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170028AD RID: 10413
			// (set) Token: 0x0600475C RID: 18268 RVA: 0x000740D3 File Offset: 0x000722D3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170028AE RID: 10414
			// (set) Token: 0x0600475D RID: 18269 RVA: 0x000740EB File Offset: 0x000722EB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170028AF RID: 10415
			// (set) Token: 0x0600475E RID: 18270 RVA: 0x00074103 File Offset: 0x00072303
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000534 RID: 1332
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170028B0 RID: 10416
			// (set) Token: 0x06004760 RID: 18272 RVA: 0x00074123 File Offset: 0x00072323
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170028B1 RID: 10417
			// (set) Token: 0x06004761 RID: 18273 RVA: 0x00074136 File Offset: 0x00072336
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170028B2 RID: 10418
			// (set) Token: 0x06004762 RID: 18274 RVA: 0x0007414E File Offset: 0x0007234E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170028B3 RID: 10419
			// (set) Token: 0x06004763 RID: 18275 RVA: 0x00074166 File Offset: 0x00072366
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170028B4 RID: 10420
			// (set) Token: 0x06004764 RID: 18276 RVA: 0x0007417E File Offset: 0x0007237E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170028B5 RID: 10421
			// (set) Token: 0x06004765 RID: 18277 RVA: 0x00074196 File Offset: 0x00072396
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
