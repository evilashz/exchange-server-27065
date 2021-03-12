using System;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000742 RID: 1858
	public class GetIPBlockListEntryCommand : SyntheticCommandWithPipelineInput<IPBlockListEntry, IPBlockListEntry>
	{
		// Token: 0x06005F41 RID: 24385 RVA: 0x00093323 File Offset: 0x00091523
		private GetIPBlockListEntryCommand() : base("Get-IPBlockListEntry")
		{
		}

		// Token: 0x06005F42 RID: 24386 RVA: 0x00093330 File Offset: 0x00091530
		public GetIPBlockListEntryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005F43 RID: 24387 RVA: 0x0009333F File Offset: 0x0009153F
		public virtual GetIPBlockListEntryCommand SetParameters(GetIPBlockListEntryCommand.IPAddressParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005F44 RID: 24388 RVA: 0x00093349 File Offset: 0x00091549
		public virtual GetIPBlockListEntryCommand SetParameters(GetIPBlockListEntryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005F45 RID: 24389 RVA: 0x00093353 File Offset: 0x00091553
		public virtual GetIPBlockListEntryCommand SetParameters(GetIPBlockListEntryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000743 RID: 1859
		public class IPAddressParameters : ParametersBase
		{
			// Token: 0x17003C76 RID: 15478
			// (set) Token: 0x06005F46 RID: 24390 RVA: 0x0009335D File Offset: 0x0009155D
			public virtual IPAddress IPAddress
			{
				set
				{
					base.PowerSharpParameters["IPAddress"] = value;
				}
			}

			// Token: 0x17003C77 RID: 15479
			// (set) Token: 0x06005F47 RID: 24391 RVA: 0x00093370 File Offset: 0x00091570
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003C78 RID: 15480
			// (set) Token: 0x06005F48 RID: 24392 RVA: 0x00093383 File Offset: 0x00091583
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17003C79 RID: 15481
			// (set) Token: 0x06005F49 RID: 24393 RVA: 0x0009339B File Offset: 0x0009159B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003C7A RID: 15482
			// (set) Token: 0x06005F4A RID: 24394 RVA: 0x000933B3 File Offset: 0x000915B3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003C7B RID: 15483
			// (set) Token: 0x06005F4B RID: 24395 RVA: 0x000933CB File Offset: 0x000915CB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003C7C RID: 15484
			// (set) Token: 0x06005F4C RID: 24396 RVA: 0x000933E3 File Offset: 0x000915E3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000744 RID: 1860
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003C7D RID: 15485
			// (set) Token: 0x06005F4E RID: 24398 RVA: 0x00093403 File Offset: 0x00091603
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003C7E RID: 15486
			// (set) Token: 0x06005F4F RID: 24399 RVA: 0x00093416 File Offset: 0x00091616
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17003C7F RID: 15487
			// (set) Token: 0x06005F50 RID: 24400 RVA: 0x0009342E File Offset: 0x0009162E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003C80 RID: 15488
			// (set) Token: 0x06005F51 RID: 24401 RVA: 0x00093446 File Offset: 0x00091646
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003C81 RID: 15489
			// (set) Token: 0x06005F52 RID: 24402 RVA: 0x0009345E File Offset: 0x0009165E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003C82 RID: 15490
			// (set) Token: 0x06005F53 RID: 24403 RVA: 0x00093476 File Offset: 0x00091676
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000745 RID: 1861
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003C83 RID: 15491
			// (set) Token: 0x06005F55 RID: 24405 RVA: 0x00093496 File Offset: 0x00091696
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new IPListEntryIdentity(value) : null);
				}
			}

			// Token: 0x17003C84 RID: 15492
			// (set) Token: 0x06005F56 RID: 24406 RVA: 0x000934B4 File Offset: 0x000916B4
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003C85 RID: 15493
			// (set) Token: 0x06005F57 RID: 24407 RVA: 0x000934C7 File Offset: 0x000916C7
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17003C86 RID: 15494
			// (set) Token: 0x06005F58 RID: 24408 RVA: 0x000934DF File Offset: 0x000916DF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003C87 RID: 15495
			// (set) Token: 0x06005F59 RID: 24409 RVA: 0x000934F7 File Offset: 0x000916F7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003C88 RID: 15496
			// (set) Token: 0x06005F5A RID: 24410 RVA: 0x0009350F File Offset: 0x0009170F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003C89 RID: 15497
			// (set) Token: 0x06005F5B RID: 24411 RVA: 0x00093527 File Offset: 0x00091727
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
