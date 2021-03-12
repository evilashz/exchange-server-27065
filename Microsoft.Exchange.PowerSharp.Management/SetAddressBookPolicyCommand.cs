using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004D5 RID: 1237
	public class SetAddressBookPolicyCommand : SyntheticCommandWithPipelineInputNoOutput<AddressBookMailboxPolicy>
	{
		// Token: 0x06004487 RID: 17543 RVA: 0x000708BC File Offset: 0x0006EABC
		private SetAddressBookPolicyCommand() : base("Set-AddressBookPolicy")
		{
		}

		// Token: 0x06004488 RID: 17544 RVA: 0x000708C9 File Offset: 0x0006EAC9
		public SetAddressBookPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004489 RID: 17545 RVA: 0x000708D8 File Offset: 0x0006EAD8
		public virtual SetAddressBookPolicyCommand SetParameters(SetAddressBookPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600448A RID: 17546 RVA: 0x000708E2 File Offset: 0x0006EAE2
		public virtual SetAddressBookPolicyCommand SetParameters(SetAddressBookPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004D6 RID: 1238
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002696 RID: 9878
			// (set) Token: 0x0600448B RID: 17547 RVA: 0x000708EC File Offset: 0x0006EAEC
			public virtual AddressListIdParameter AddressLists
			{
				set
				{
					base.PowerSharpParameters["AddressLists"] = value;
				}
			}

			// Token: 0x17002697 RID: 9879
			// (set) Token: 0x0600448C RID: 17548 RVA: 0x000708FF File Offset: 0x0006EAFF
			public virtual GlobalAddressListIdParameter GlobalAddressList
			{
				set
				{
					base.PowerSharpParameters["GlobalAddressList"] = value;
				}
			}

			// Token: 0x17002698 RID: 9880
			// (set) Token: 0x0600448D RID: 17549 RVA: 0x00070912 File Offset: 0x0006EB12
			public virtual AddressListIdParameter RoomList
			{
				set
				{
					base.PowerSharpParameters["RoomList"] = value;
				}
			}

			// Token: 0x17002699 RID: 9881
			// (set) Token: 0x0600448E RID: 17550 RVA: 0x00070925 File Offset: 0x0006EB25
			public virtual OfflineAddressBookIdParameter OfflineAddressBook
			{
				set
				{
					base.PowerSharpParameters["OfflineAddressBook"] = value;
				}
			}

			// Token: 0x1700269A RID: 9882
			// (set) Token: 0x0600448F RID: 17551 RVA: 0x00070938 File Offset: 0x0006EB38
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700269B RID: 9883
			// (set) Token: 0x06004490 RID: 17552 RVA: 0x0007094B File Offset: 0x0006EB4B
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700269C RID: 9884
			// (set) Token: 0x06004491 RID: 17553 RVA: 0x0007095E File Offset: 0x0006EB5E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700269D RID: 9885
			// (set) Token: 0x06004492 RID: 17554 RVA: 0x00070976 File Offset: 0x0006EB76
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700269E RID: 9886
			// (set) Token: 0x06004493 RID: 17555 RVA: 0x0007098E File Offset: 0x0006EB8E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700269F RID: 9887
			// (set) Token: 0x06004494 RID: 17556 RVA: 0x000709A6 File Offset: 0x0006EBA6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170026A0 RID: 9888
			// (set) Token: 0x06004495 RID: 17557 RVA: 0x000709BE File Offset: 0x0006EBBE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020004D7 RID: 1239
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170026A1 RID: 9889
			// (set) Token: 0x06004497 RID: 17559 RVA: 0x000709DE File Offset: 0x0006EBDE
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170026A2 RID: 9890
			// (set) Token: 0x06004498 RID: 17560 RVA: 0x000709FC File Offset: 0x0006EBFC
			public virtual AddressListIdParameter AddressLists
			{
				set
				{
					base.PowerSharpParameters["AddressLists"] = value;
				}
			}

			// Token: 0x170026A3 RID: 9891
			// (set) Token: 0x06004499 RID: 17561 RVA: 0x00070A0F File Offset: 0x0006EC0F
			public virtual GlobalAddressListIdParameter GlobalAddressList
			{
				set
				{
					base.PowerSharpParameters["GlobalAddressList"] = value;
				}
			}

			// Token: 0x170026A4 RID: 9892
			// (set) Token: 0x0600449A RID: 17562 RVA: 0x00070A22 File Offset: 0x0006EC22
			public virtual AddressListIdParameter RoomList
			{
				set
				{
					base.PowerSharpParameters["RoomList"] = value;
				}
			}

			// Token: 0x170026A5 RID: 9893
			// (set) Token: 0x0600449B RID: 17563 RVA: 0x00070A35 File Offset: 0x0006EC35
			public virtual OfflineAddressBookIdParameter OfflineAddressBook
			{
				set
				{
					base.PowerSharpParameters["OfflineAddressBook"] = value;
				}
			}

			// Token: 0x170026A6 RID: 9894
			// (set) Token: 0x0600449C RID: 17564 RVA: 0x00070A48 File Offset: 0x0006EC48
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170026A7 RID: 9895
			// (set) Token: 0x0600449D RID: 17565 RVA: 0x00070A5B File Offset: 0x0006EC5B
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170026A8 RID: 9896
			// (set) Token: 0x0600449E RID: 17566 RVA: 0x00070A6E File Offset: 0x0006EC6E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170026A9 RID: 9897
			// (set) Token: 0x0600449F RID: 17567 RVA: 0x00070A86 File Offset: 0x0006EC86
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170026AA RID: 9898
			// (set) Token: 0x060044A0 RID: 17568 RVA: 0x00070A9E File Offset: 0x0006EC9E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170026AB RID: 9899
			// (set) Token: 0x060044A1 RID: 17569 RVA: 0x00070AB6 File Offset: 0x0006ECB6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170026AC RID: 9900
			// (set) Token: 0x060044A2 RID: 17570 RVA: 0x00070ACE File Offset: 0x0006ECCE
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
