using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200024A RID: 586
	public class RemovePublicFolderCommand : SyntheticCommandWithPipelineInput<PublicFolder, PublicFolder>
	{
		// Token: 0x06002BA4 RID: 11172 RVA: 0x00050673 File Offset: 0x0004E873
		private RemovePublicFolderCommand() : base("Remove-PublicFolder")
		{
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x00050680 File Offset: 0x0004E880
		public RemovePublicFolderCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x0005068F File Offset: 0x0004E88F
		public virtual RemovePublicFolderCommand SetParameters(RemovePublicFolderCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x00050699 File Offset: 0x0004E899
		public virtual RemovePublicFolderCommand SetParameters(RemovePublicFolderCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200024B RID: 587
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170012C9 RID: 4809
			// (set) Token: 0x06002BA8 RID: 11176 RVA: 0x000506A3 File Offset: 0x0004E8A3
			public virtual SwitchParameter Recurse
			{
				set
				{
					base.PowerSharpParameters["Recurse"] = value;
				}
			}

			// Token: 0x170012CA RID: 4810
			// (set) Token: 0x06002BA9 RID: 11177 RVA: 0x000506BB File Offset: 0x0004E8BB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170012CB RID: 4811
			// (set) Token: 0x06002BAA RID: 11178 RVA: 0x000506CE File Offset: 0x0004E8CE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170012CC RID: 4812
			// (set) Token: 0x06002BAB RID: 11179 RVA: 0x000506E6 File Offset: 0x0004E8E6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170012CD RID: 4813
			// (set) Token: 0x06002BAC RID: 11180 RVA: 0x000506FE File Offset: 0x0004E8FE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170012CE RID: 4814
			// (set) Token: 0x06002BAD RID: 11181 RVA: 0x00050716 File Offset: 0x0004E916
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170012CF RID: 4815
			// (set) Token: 0x06002BAE RID: 11182 RVA: 0x0005072E File Offset: 0x0004E92E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170012D0 RID: 4816
			// (set) Token: 0x06002BAF RID: 11183 RVA: 0x00050746 File Offset: 0x0004E946
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200024C RID: 588
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170012D1 RID: 4817
			// (set) Token: 0x06002BB1 RID: 11185 RVA: 0x00050766 File Offset: 0x0004E966
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderIdParameter(value) : null);
				}
			}

			// Token: 0x170012D2 RID: 4818
			// (set) Token: 0x06002BB2 RID: 11186 RVA: 0x00050784 File Offset: 0x0004E984
			public virtual SwitchParameter Recurse
			{
				set
				{
					base.PowerSharpParameters["Recurse"] = value;
				}
			}

			// Token: 0x170012D3 RID: 4819
			// (set) Token: 0x06002BB3 RID: 11187 RVA: 0x0005079C File Offset: 0x0004E99C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170012D4 RID: 4820
			// (set) Token: 0x06002BB4 RID: 11188 RVA: 0x000507AF File Offset: 0x0004E9AF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170012D5 RID: 4821
			// (set) Token: 0x06002BB5 RID: 11189 RVA: 0x000507C7 File Offset: 0x0004E9C7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170012D6 RID: 4822
			// (set) Token: 0x06002BB6 RID: 11190 RVA: 0x000507DF File Offset: 0x0004E9DF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170012D7 RID: 4823
			// (set) Token: 0x06002BB7 RID: 11191 RVA: 0x000507F7 File Offset: 0x0004E9F7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170012D8 RID: 4824
			// (set) Token: 0x06002BB8 RID: 11192 RVA: 0x0005080F File Offset: 0x0004EA0F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170012D9 RID: 4825
			// (set) Token: 0x06002BB9 RID: 11193 RVA: 0x00050827 File Offset: 0x0004EA27
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
