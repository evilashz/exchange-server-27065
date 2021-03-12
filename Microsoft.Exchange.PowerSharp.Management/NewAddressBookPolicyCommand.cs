using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004D0 RID: 1232
	public class NewAddressBookPolicyCommand : SyntheticCommandWithPipelineInput<AddressBookMailboxPolicy, AddressBookMailboxPolicy>
	{
		// Token: 0x06004462 RID: 17506 RVA: 0x000705E2 File Offset: 0x0006E7E2
		private NewAddressBookPolicyCommand() : base("New-AddressBookPolicy")
		{
		}

		// Token: 0x06004463 RID: 17507 RVA: 0x000705EF File Offset: 0x0006E7EF
		public NewAddressBookPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004464 RID: 17508 RVA: 0x000705FE File Offset: 0x0006E7FE
		public virtual NewAddressBookPolicyCommand SetParameters(NewAddressBookPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004D1 RID: 1233
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700267B RID: 9851
			// (set) Token: 0x06004465 RID: 17509 RVA: 0x00070608 File Offset: 0x0006E808
			public virtual AddressListIdParameter AddressLists
			{
				set
				{
					base.PowerSharpParameters["AddressLists"] = value;
				}
			}

			// Token: 0x1700267C RID: 9852
			// (set) Token: 0x06004466 RID: 17510 RVA: 0x0007061B File Offset: 0x0006E81B
			public virtual GlobalAddressListIdParameter GlobalAddressList
			{
				set
				{
					base.PowerSharpParameters["GlobalAddressList"] = value;
				}
			}

			// Token: 0x1700267D RID: 9853
			// (set) Token: 0x06004467 RID: 17511 RVA: 0x0007062E File Offset: 0x0006E82E
			public virtual AddressListIdParameter RoomList
			{
				set
				{
					base.PowerSharpParameters["RoomList"] = value;
				}
			}

			// Token: 0x1700267E RID: 9854
			// (set) Token: 0x06004468 RID: 17512 RVA: 0x00070641 File Offset: 0x0006E841
			public virtual OfflineAddressBookIdParameter OfflineAddressBook
			{
				set
				{
					base.PowerSharpParameters["OfflineAddressBook"] = value;
				}
			}

			// Token: 0x1700267F RID: 9855
			// (set) Token: 0x06004469 RID: 17513 RVA: 0x00070654 File Offset: 0x0006E854
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002680 RID: 9856
			// (set) Token: 0x0600446A RID: 17514 RVA: 0x00070672 File Offset: 0x0006E872
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002681 RID: 9857
			// (set) Token: 0x0600446B RID: 17515 RVA: 0x00070685 File Offset: 0x0006E885
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002682 RID: 9858
			// (set) Token: 0x0600446C RID: 17516 RVA: 0x00070698 File Offset: 0x0006E898
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002683 RID: 9859
			// (set) Token: 0x0600446D RID: 17517 RVA: 0x000706B0 File Offset: 0x0006E8B0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002684 RID: 9860
			// (set) Token: 0x0600446E RID: 17518 RVA: 0x000706C8 File Offset: 0x0006E8C8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002685 RID: 9861
			// (set) Token: 0x0600446F RID: 17519 RVA: 0x000706E0 File Offset: 0x0006E8E0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002686 RID: 9862
			// (set) Token: 0x06004470 RID: 17520 RVA: 0x000706F8 File Offset: 0x0006E8F8
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
