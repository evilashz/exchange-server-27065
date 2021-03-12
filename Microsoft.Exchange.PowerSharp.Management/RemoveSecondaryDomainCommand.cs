using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000BB RID: 187
	public class RemoveSecondaryDomainCommand : SyntheticCommandWithPipelineInputNoOutput<AcceptedDomainIdParameter>
	{
		// Token: 0x06001AD5 RID: 6869 RVA: 0x0003A674 File Offset: 0x00038874
		private RemoveSecondaryDomainCommand() : base("Remove-SecondaryDomain")
		{
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0003A681 File Offset: 0x00038881
		public RemoveSecondaryDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x0003A690 File Offset: 0x00038890
		public virtual RemoveSecondaryDomainCommand SetParameters(RemoveSecondaryDomainCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x0003A69A File Offset: 0x0003889A
		public virtual RemoveSecondaryDomainCommand SetParameters(RemoveSecondaryDomainCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000BC RID: 188
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000518 RID: 1304
			// (set) Token: 0x06001AD9 RID: 6873 RVA: 0x0003A6A4 File Offset: 0x000388A4
			public virtual AcceptedDomainIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17000519 RID: 1305
			// (set) Token: 0x06001ADA RID: 6874 RVA: 0x0003A6B7 File Offset: 0x000388B7
			public virtual SwitchParameter SkipRecipients
			{
				set
				{
					base.PowerSharpParameters["SkipRecipients"] = value;
				}
			}

			// Token: 0x1700051A RID: 1306
			// (set) Token: 0x06001ADB RID: 6875 RVA: 0x0003A6CF File Offset: 0x000388CF
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700051B RID: 1307
			// (set) Token: 0x06001ADC RID: 6876 RVA: 0x0003A6E7 File Offset: 0x000388E7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700051C RID: 1308
			// (set) Token: 0x06001ADD RID: 6877 RVA: 0x0003A6FA File Offset: 0x000388FA
			public virtual SwitchParameter IsDatacenter
			{
				set
				{
					base.PowerSharpParameters["IsDatacenter"] = value;
				}
			}

			// Token: 0x1700051D RID: 1309
			// (set) Token: 0x06001ADE RID: 6878 RVA: 0x0003A712 File Offset: 0x00038912
			public virtual SwitchParameter IsDatacenterDedicated
			{
				set
				{
					base.PowerSharpParameters["IsDatacenterDedicated"] = value;
				}
			}

			// Token: 0x1700051E RID: 1310
			// (set) Token: 0x06001ADF RID: 6879 RVA: 0x0003A72A File Offset: 0x0003892A
			public virtual SwitchParameter IsPartnerHosted
			{
				set
				{
					base.PowerSharpParameters["IsPartnerHosted"] = value;
				}
			}

			// Token: 0x1700051F RID: 1311
			// (set) Token: 0x06001AE0 RID: 6880 RVA: 0x0003A742 File Offset: 0x00038942
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000520 RID: 1312
			// (set) Token: 0x06001AE1 RID: 6881 RVA: 0x0003A75A File Offset: 0x0003895A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000521 RID: 1313
			// (set) Token: 0x06001AE2 RID: 6882 RVA: 0x0003A772 File Offset: 0x00038972
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000522 RID: 1314
			// (set) Token: 0x06001AE3 RID: 6883 RVA: 0x0003A78A File Offset: 0x0003898A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000523 RID: 1315
			// (set) Token: 0x06001AE4 RID: 6884 RVA: 0x0003A7A2 File Offset: 0x000389A2
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000524 RID: 1316
			// (set) Token: 0x06001AE5 RID: 6885 RVA: 0x0003A7BA File Offset: 0x000389BA
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020000BD RID: 189
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000525 RID: 1317
			// (set) Token: 0x06001AE7 RID: 6887 RVA: 0x0003A7DA File Offset: 0x000389DA
			public virtual SwitchParameter SkipRecipients
			{
				set
				{
					base.PowerSharpParameters["SkipRecipients"] = value;
				}
			}

			// Token: 0x17000526 RID: 1318
			// (set) Token: 0x06001AE8 RID: 6888 RVA: 0x0003A7F2 File Offset: 0x000389F2
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17000527 RID: 1319
			// (set) Token: 0x06001AE9 RID: 6889 RVA: 0x0003A80A File Offset: 0x00038A0A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000528 RID: 1320
			// (set) Token: 0x06001AEA RID: 6890 RVA: 0x0003A81D File Offset: 0x00038A1D
			public virtual SwitchParameter IsDatacenter
			{
				set
				{
					base.PowerSharpParameters["IsDatacenter"] = value;
				}
			}

			// Token: 0x17000529 RID: 1321
			// (set) Token: 0x06001AEB RID: 6891 RVA: 0x0003A835 File Offset: 0x00038A35
			public virtual SwitchParameter IsDatacenterDedicated
			{
				set
				{
					base.PowerSharpParameters["IsDatacenterDedicated"] = value;
				}
			}

			// Token: 0x1700052A RID: 1322
			// (set) Token: 0x06001AEC RID: 6892 RVA: 0x0003A84D File Offset: 0x00038A4D
			public virtual SwitchParameter IsPartnerHosted
			{
				set
				{
					base.PowerSharpParameters["IsPartnerHosted"] = value;
				}
			}

			// Token: 0x1700052B RID: 1323
			// (set) Token: 0x06001AED RID: 6893 RVA: 0x0003A865 File Offset: 0x00038A65
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700052C RID: 1324
			// (set) Token: 0x06001AEE RID: 6894 RVA: 0x0003A87D File Offset: 0x00038A7D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700052D RID: 1325
			// (set) Token: 0x06001AEF RID: 6895 RVA: 0x0003A895 File Offset: 0x00038A95
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700052E RID: 1326
			// (set) Token: 0x06001AF0 RID: 6896 RVA: 0x0003A8AD File Offset: 0x00038AAD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700052F RID: 1327
			// (set) Token: 0x06001AF1 RID: 6897 RVA: 0x0003A8C5 File Offset: 0x00038AC5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000530 RID: 1328
			// (set) Token: 0x06001AF2 RID: 6898 RVA: 0x0003A8DD File Offset: 0x00038ADD
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
