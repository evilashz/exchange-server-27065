using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000402 RID: 1026
	public class RemoveMailboxSearchCommand : SyntheticCommandWithPipelineInput<MailboxDiscoverySearch, MailboxDiscoverySearch>
	{
		// Token: 0x06003CC1 RID: 15553 RVA: 0x00066A3D File Offset: 0x00064C3D
		private RemoveMailboxSearchCommand() : base("Remove-MailboxSearch")
		{
		}

		// Token: 0x06003CC2 RID: 15554 RVA: 0x00066A4A File Offset: 0x00064C4A
		public RemoveMailboxSearchCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003CC3 RID: 15555 RVA: 0x00066A59 File Offset: 0x00064C59
		public virtual RemoveMailboxSearchCommand SetParameters(RemoveMailboxSearchCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003CC4 RID: 15556 RVA: 0x00066A63 File Offset: 0x00064C63
		public virtual RemoveMailboxSearchCommand SetParameters(RemoveMailboxSearchCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000403 RID: 1027
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002076 RID: 8310
			// (set) Token: 0x06003CC5 RID: 15557 RVA: 0x00066A6D File Offset: 0x00064C6D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002077 RID: 8311
			// (set) Token: 0x06003CC6 RID: 15558 RVA: 0x00066A80 File Offset: 0x00064C80
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002078 RID: 8312
			// (set) Token: 0x06003CC7 RID: 15559 RVA: 0x00066A98 File Offset: 0x00064C98
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002079 RID: 8313
			// (set) Token: 0x06003CC8 RID: 15560 RVA: 0x00066AB0 File Offset: 0x00064CB0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700207A RID: 8314
			// (set) Token: 0x06003CC9 RID: 15561 RVA: 0x00066AC8 File Offset: 0x00064CC8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700207B RID: 8315
			// (set) Token: 0x06003CCA RID: 15562 RVA: 0x00066AE0 File Offset: 0x00064CE0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700207C RID: 8316
			// (set) Token: 0x06003CCB RID: 15563 RVA: 0x00066AF8 File Offset: 0x00064CF8
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000404 RID: 1028
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700207D RID: 8317
			// (set) Token: 0x06003CCD RID: 15565 RVA: 0x00066B18 File Offset: 0x00064D18
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new EwsStoreObjectIdParameter(value) : null);
				}
			}

			// Token: 0x1700207E RID: 8318
			// (set) Token: 0x06003CCE RID: 15566 RVA: 0x00066B36 File Offset: 0x00064D36
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700207F RID: 8319
			// (set) Token: 0x06003CCF RID: 15567 RVA: 0x00066B49 File Offset: 0x00064D49
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002080 RID: 8320
			// (set) Token: 0x06003CD0 RID: 15568 RVA: 0x00066B61 File Offset: 0x00064D61
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002081 RID: 8321
			// (set) Token: 0x06003CD1 RID: 15569 RVA: 0x00066B79 File Offset: 0x00064D79
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002082 RID: 8322
			// (set) Token: 0x06003CD2 RID: 15570 RVA: 0x00066B91 File Offset: 0x00064D91
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002083 RID: 8323
			// (set) Token: 0x06003CD3 RID: 15571 RVA: 0x00066BA9 File Offset: 0x00064DA9
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002084 RID: 8324
			// (set) Token: 0x06003CD4 RID: 15572 RVA: 0x00066BC1 File Offset: 0x00064DC1
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
