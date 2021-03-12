using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007F5 RID: 2037
	public class MoveOfflineAddressBookCommand : SyntheticCommandWithPipelineInput<OfflineAddressBook, OfflineAddressBook>
	{
		// Token: 0x06006528 RID: 25896 RVA: 0x0009A9E9 File Offset: 0x00098BE9
		private MoveOfflineAddressBookCommand() : base("Move-OfflineAddressBook")
		{
		}

		// Token: 0x06006529 RID: 25897 RVA: 0x0009A9F6 File Offset: 0x00098BF6
		public MoveOfflineAddressBookCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600652A RID: 25898 RVA: 0x0009AA05 File Offset: 0x00098C05
		public virtual MoveOfflineAddressBookCommand SetParameters(MoveOfflineAddressBookCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600652B RID: 25899 RVA: 0x0009AA0F File Offset: 0x00098C0F
		public virtual MoveOfflineAddressBookCommand SetParameters(MoveOfflineAddressBookCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007F6 RID: 2038
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170040F7 RID: 16631
			// (set) Token: 0x0600652C RID: 25900 RVA: 0x0009AA19 File Offset: 0x00098C19
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170040F8 RID: 16632
			// (set) Token: 0x0600652D RID: 25901 RVA: 0x0009AA2C File Offset: 0x00098C2C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170040F9 RID: 16633
			// (set) Token: 0x0600652E RID: 25902 RVA: 0x0009AA3F File Offset: 0x00098C3F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170040FA RID: 16634
			// (set) Token: 0x0600652F RID: 25903 RVA: 0x0009AA57 File Offset: 0x00098C57
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170040FB RID: 16635
			// (set) Token: 0x06006530 RID: 25904 RVA: 0x0009AA6F File Offset: 0x00098C6F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170040FC RID: 16636
			// (set) Token: 0x06006531 RID: 25905 RVA: 0x0009AA87 File Offset: 0x00098C87
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170040FD RID: 16637
			// (set) Token: 0x06006532 RID: 25906 RVA: 0x0009AA9F File Offset: 0x00098C9F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170040FE RID: 16638
			// (set) Token: 0x06006533 RID: 25907 RVA: 0x0009AAB7 File Offset: 0x00098CB7
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020007F7 RID: 2039
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170040FF RID: 16639
			// (set) Token: 0x06006535 RID: 25909 RVA: 0x0009AAD7 File Offset: 0x00098CD7
			public virtual OfflineAddressBookIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004100 RID: 16640
			// (set) Token: 0x06006536 RID: 25910 RVA: 0x0009AAEA File Offset: 0x00098CEA
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17004101 RID: 16641
			// (set) Token: 0x06006537 RID: 25911 RVA: 0x0009AAFD File Offset: 0x00098CFD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004102 RID: 16642
			// (set) Token: 0x06006538 RID: 25912 RVA: 0x0009AB10 File Offset: 0x00098D10
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004103 RID: 16643
			// (set) Token: 0x06006539 RID: 25913 RVA: 0x0009AB28 File Offset: 0x00098D28
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004104 RID: 16644
			// (set) Token: 0x0600653A RID: 25914 RVA: 0x0009AB40 File Offset: 0x00098D40
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004105 RID: 16645
			// (set) Token: 0x0600653B RID: 25915 RVA: 0x0009AB58 File Offset: 0x00098D58
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004106 RID: 16646
			// (set) Token: 0x0600653C RID: 25916 RVA: 0x0009AB70 File Offset: 0x00098D70
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004107 RID: 16647
			// (set) Token: 0x0600653D RID: 25917 RVA: 0x0009AB88 File Offset: 0x00098D88
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
