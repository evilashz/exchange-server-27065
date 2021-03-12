using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004B8 RID: 1208
	public class RemoveAddressListCommand : SyntheticCommandWithPipelineInput<AddressListIdParameter, AddressListIdParameter>
	{
		// Token: 0x06004362 RID: 17250 RVA: 0x0006F24F File Offset: 0x0006D44F
		private RemoveAddressListCommand() : base("Remove-AddressList")
		{
		}

		// Token: 0x06004363 RID: 17251 RVA: 0x0006F25C File Offset: 0x0006D45C
		public RemoveAddressListCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004364 RID: 17252 RVA: 0x0006F26B File Offset: 0x0006D46B
		public virtual RemoveAddressListCommand SetParameters(RemoveAddressListCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004365 RID: 17253 RVA: 0x0006F275 File Offset: 0x0006D475
		public virtual RemoveAddressListCommand SetParameters(RemoveAddressListCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004B9 RID: 1209
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170025AB RID: 9643
			// (set) Token: 0x06004366 RID: 17254 RVA: 0x0006F27F File Offset: 0x0006D47F
			public virtual SwitchParameter Recursive
			{
				set
				{
					base.PowerSharpParameters["Recursive"] = value;
				}
			}

			// Token: 0x170025AC RID: 9644
			// (set) Token: 0x06004367 RID: 17255 RVA: 0x0006F297 File Offset: 0x0006D497
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170025AD RID: 9645
			// (set) Token: 0x06004368 RID: 17256 RVA: 0x0006F2AA File Offset: 0x0006D4AA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170025AE RID: 9646
			// (set) Token: 0x06004369 RID: 17257 RVA: 0x0006F2C2 File Offset: 0x0006D4C2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170025AF RID: 9647
			// (set) Token: 0x0600436A RID: 17258 RVA: 0x0006F2DA File Offset: 0x0006D4DA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170025B0 RID: 9648
			// (set) Token: 0x0600436B RID: 17259 RVA: 0x0006F2F2 File Offset: 0x0006D4F2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170025B1 RID: 9649
			// (set) Token: 0x0600436C RID: 17260 RVA: 0x0006F30A File Offset: 0x0006D50A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170025B2 RID: 9650
			// (set) Token: 0x0600436D RID: 17261 RVA: 0x0006F322 File Offset: 0x0006D522
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020004BA RID: 1210
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170025B3 RID: 9651
			// (set) Token: 0x0600436F RID: 17263 RVA: 0x0006F342 File Offset: 0x0006D542
			public virtual AddressListIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170025B4 RID: 9652
			// (set) Token: 0x06004370 RID: 17264 RVA: 0x0006F355 File Offset: 0x0006D555
			public virtual SwitchParameter Recursive
			{
				set
				{
					base.PowerSharpParameters["Recursive"] = value;
				}
			}

			// Token: 0x170025B5 RID: 9653
			// (set) Token: 0x06004371 RID: 17265 RVA: 0x0006F36D File Offset: 0x0006D56D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170025B6 RID: 9654
			// (set) Token: 0x06004372 RID: 17266 RVA: 0x0006F380 File Offset: 0x0006D580
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170025B7 RID: 9655
			// (set) Token: 0x06004373 RID: 17267 RVA: 0x0006F398 File Offset: 0x0006D598
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170025B8 RID: 9656
			// (set) Token: 0x06004374 RID: 17268 RVA: 0x0006F3B0 File Offset: 0x0006D5B0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170025B9 RID: 9657
			// (set) Token: 0x06004375 RID: 17269 RVA: 0x0006F3C8 File Offset: 0x0006D5C8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170025BA RID: 9658
			// (set) Token: 0x06004376 RID: 17270 RVA: 0x0006F3E0 File Offset: 0x0006D5E0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170025BB RID: 9659
			// (set) Token: 0x06004377 RID: 17271 RVA: 0x0006F3F8 File Offset: 0x0006D5F8
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
