using System;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000768 RID: 1896
	public class AddIPBlockListProviderCommand : SyntheticCommandWithPipelineInput<IPBlockListProvider, IPBlockListProvider>
	{
		// Token: 0x0600603D RID: 24637 RVA: 0x00094651 File Offset: 0x00092851
		private AddIPBlockListProviderCommand() : base("Add-IPBlockListProvider")
		{
		}

		// Token: 0x0600603E RID: 24638 RVA: 0x0009465E File Offset: 0x0009285E
		public AddIPBlockListProviderCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600603F RID: 24639 RVA: 0x0009466D File Offset: 0x0009286D
		public virtual AddIPBlockListProviderCommand SetParameters(AddIPBlockListProviderCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000769 RID: 1897
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003D26 RID: 15654
			// (set) Token: 0x06006040 RID: 24640 RVA: 0x00094677 File Offset: 0x00092877
			public virtual AsciiString RejectionResponse
			{
				set
				{
					base.PowerSharpParameters["RejectionResponse"] = value;
				}
			}

			// Token: 0x17003D27 RID: 15655
			// (set) Token: 0x06006041 RID: 24641 RVA: 0x0009468A File Offset: 0x0009288A
			public virtual SmtpDomain LookupDomain
			{
				set
				{
					base.PowerSharpParameters["LookupDomain"] = value;
				}
			}

			// Token: 0x17003D28 RID: 15656
			// (set) Token: 0x06006042 RID: 24642 RVA: 0x0009469D File Offset: 0x0009289D
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17003D29 RID: 15657
			// (set) Token: 0x06006043 RID: 24643 RVA: 0x000946B5 File Offset: 0x000928B5
			public virtual bool AnyMatch
			{
				set
				{
					base.PowerSharpParameters["AnyMatch"] = value;
				}
			}

			// Token: 0x17003D2A RID: 15658
			// (set) Token: 0x06006044 RID: 24644 RVA: 0x000946CD File Offset: 0x000928CD
			public virtual IPAddress BitmaskMatch
			{
				set
				{
					base.PowerSharpParameters["BitmaskMatch"] = value;
				}
			}

			// Token: 0x17003D2B RID: 15659
			// (set) Token: 0x06006045 RID: 24645 RVA: 0x000946E0 File Offset: 0x000928E0
			public virtual MultiValuedProperty<IPAddress> IPAddressesMatch
			{
				set
				{
					base.PowerSharpParameters["IPAddressesMatch"] = value;
				}
			}

			// Token: 0x17003D2C RID: 15660
			// (set) Token: 0x06006046 RID: 24646 RVA: 0x000946F3 File Offset: 0x000928F3
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17003D2D RID: 15661
			// (set) Token: 0x06006047 RID: 24647 RVA: 0x0009470B File Offset: 0x0009290B
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003D2E RID: 15662
			// (set) Token: 0x06006048 RID: 24648 RVA: 0x0009471E File Offset: 0x0009291E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003D2F RID: 15663
			// (set) Token: 0x06006049 RID: 24649 RVA: 0x00094731 File Offset: 0x00092931
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003D30 RID: 15664
			// (set) Token: 0x0600604A RID: 24650 RVA: 0x00094749 File Offset: 0x00092949
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003D31 RID: 15665
			// (set) Token: 0x0600604B RID: 24651 RVA: 0x00094761 File Offset: 0x00092961
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003D32 RID: 15666
			// (set) Token: 0x0600604C RID: 24652 RVA: 0x00094779 File Offset: 0x00092979
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003D33 RID: 15667
			// (set) Token: 0x0600604D RID: 24653 RVA: 0x00094791 File Offset: 0x00092991
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
