using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007FA RID: 2042
	public class RemoveOfflineAddressBookCommand : SyntheticCommandWithPipelineInput<OfflineAddressBook, OfflineAddressBook>
	{
		// Token: 0x06006552 RID: 25938 RVA: 0x0009AD36 File Offset: 0x00098F36
		private RemoveOfflineAddressBookCommand() : base("Remove-OfflineAddressBook")
		{
		}

		// Token: 0x06006553 RID: 25939 RVA: 0x0009AD43 File Offset: 0x00098F43
		public RemoveOfflineAddressBookCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006554 RID: 25940 RVA: 0x0009AD52 File Offset: 0x00098F52
		public virtual RemoveOfflineAddressBookCommand SetParameters(RemoveOfflineAddressBookCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006555 RID: 25941 RVA: 0x0009AD5C File Offset: 0x00098F5C
		public virtual RemoveOfflineAddressBookCommand SetParameters(RemoveOfflineAddressBookCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007FB RID: 2043
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004117 RID: 16663
			// (set) Token: 0x06006556 RID: 25942 RVA: 0x0009AD66 File Offset: 0x00098F66
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17004118 RID: 16664
			// (set) Token: 0x06006557 RID: 25943 RVA: 0x0009AD7E File Offset: 0x00098F7E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004119 RID: 16665
			// (set) Token: 0x06006558 RID: 25944 RVA: 0x0009AD91 File Offset: 0x00098F91
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700411A RID: 16666
			// (set) Token: 0x06006559 RID: 25945 RVA: 0x0009ADA9 File Offset: 0x00098FA9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700411B RID: 16667
			// (set) Token: 0x0600655A RID: 25946 RVA: 0x0009ADC1 File Offset: 0x00098FC1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700411C RID: 16668
			// (set) Token: 0x0600655B RID: 25947 RVA: 0x0009ADD9 File Offset: 0x00098FD9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700411D RID: 16669
			// (set) Token: 0x0600655C RID: 25948 RVA: 0x0009ADF1 File Offset: 0x00098FF1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700411E RID: 16670
			// (set) Token: 0x0600655D RID: 25949 RVA: 0x0009AE09 File Offset: 0x00099009
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020007FC RID: 2044
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700411F RID: 16671
			// (set) Token: 0x0600655F RID: 25951 RVA: 0x0009AE29 File Offset: 0x00099029
			public virtual OfflineAddressBookIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004120 RID: 16672
			// (set) Token: 0x06006560 RID: 25952 RVA: 0x0009AE3C File Offset: 0x0009903C
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17004121 RID: 16673
			// (set) Token: 0x06006561 RID: 25953 RVA: 0x0009AE54 File Offset: 0x00099054
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004122 RID: 16674
			// (set) Token: 0x06006562 RID: 25954 RVA: 0x0009AE67 File Offset: 0x00099067
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004123 RID: 16675
			// (set) Token: 0x06006563 RID: 25955 RVA: 0x0009AE7F File Offset: 0x0009907F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004124 RID: 16676
			// (set) Token: 0x06006564 RID: 25956 RVA: 0x0009AE97 File Offset: 0x00099097
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004125 RID: 16677
			// (set) Token: 0x06006565 RID: 25957 RVA: 0x0009AEAF File Offset: 0x000990AF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004126 RID: 16678
			// (set) Token: 0x06006566 RID: 25958 RVA: 0x0009AEC7 File Offset: 0x000990C7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004127 RID: 16679
			// (set) Token: 0x06006567 RID: 25959 RVA: 0x0009AEDF File Offset: 0x000990DF
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
