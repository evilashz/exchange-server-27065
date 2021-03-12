using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004D2 RID: 1234
	public class RemoveAddressBookPolicyCommand : SyntheticCommandWithPipelineInput<AddressBookMailboxPolicy, AddressBookMailboxPolicy>
	{
		// Token: 0x06004472 RID: 17522 RVA: 0x00070718 File Offset: 0x0006E918
		private RemoveAddressBookPolicyCommand() : base("Remove-AddressBookPolicy")
		{
		}

		// Token: 0x06004473 RID: 17523 RVA: 0x00070725 File Offset: 0x0006E925
		public RemoveAddressBookPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004474 RID: 17524 RVA: 0x00070734 File Offset: 0x0006E934
		public virtual RemoveAddressBookPolicyCommand SetParameters(RemoveAddressBookPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004475 RID: 17525 RVA: 0x0007073E File Offset: 0x0006E93E
		public virtual RemoveAddressBookPolicyCommand SetParameters(RemoveAddressBookPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004D3 RID: 1235
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002687 RID: 9863
			// (set) Token: 0x06004476 RID: 17526 RVA: 0x00070748 File Offset: 0x0006E948
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002688 RID: 9864
			// (set) Token: 0x06004477 RID: 17527 RVA: 0x0007075B File Offset: 0x0006E95B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002689 RID: 9865
			// (set) Token: 0x06004478 RID: 17528 RVA: 0x00070773 File Offset: 0x0006E973
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700268A RID: 9866
			// (set) Token: 0x06004479 RID: 17529 RVA: 0x0007078B File Offset: 0x0006E98B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700268B RID: 9867
			// (set) Token: 0x0600447A RID: 17530 RVA: 0x000707A3 File Offset: 0x0006E9A3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700268C RID: 9868
			// (set) Token: 0x0600447B RID: 17531 RVA: 0x000707BB File Offset: 0x0006E9BB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700268D RID: 9869
			// (set) Token: 0x0600447C RID: 17532 RVA: 0x000707D3 File Offset: 0x0006E9D3
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020004D4 RID: 1236
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700268E RID: 9870
			// (set) Token: 0x0600447E RID: 17534 RVA: 0x000707F3 File Offset: 0x0006E9F3
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700268F RID: 9871
			// (set) Token: 0x0600447F RID: 17535 RVA: 0x00070811 File Offset: 0x0006EA11
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002690 RID: 9872
			// (set) Token: 0x06004480 RID: 17536 RVA: 0x00070824 File Offset: 0x0006EA24
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002691 RID: 9873
			// (set) Token: 0x06004481 RID: 17537 RVA: 0x0007083C File Offset: 0x0006EA3C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002692 RID: 9874
			// (set) Token: 0x06004482 RID: 17538 RVA: 0x00070854 File Offset: 0x0006EA54
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002693 RID: 9875
			// (set) Token: 0x06004483 RID: 17539 RVA: 0x0007086C File Offset: 0x0006EA6C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002694 RID: 9876
			// (set) Token: 0x06004484 RID: 17540 RVA: 0x00070884 File Offset: 0x0006EA84
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002695 RID: 9877
			// (set) Token: 0x06004485 RID: 17541 RVA: 0x0007089C File Offset: 0x0006EA9C
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
