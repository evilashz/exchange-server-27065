using System;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000755 RID: 1877
	public class AddIPAllowListProviderCommand : SyntheticCommandWithPipelineInput<IPAllowListProvider, IPAllowListProvider>
	{
		// Token: 0x06005FB7 RID: 24503 RVA: 0x00093C16 File Offset: 0x00091E16
		private AddIPAllowListProviderCommand() : base("Add-IPAllowListProvider")
		{
		}

		// Token: 0x06005FB8 RID: 24504 RVA: 0x00093C23 File Offset: 0x00091E23
		public AddIPAllowListProviderCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005FB9 RID: 24505 RVA: 0x00093C32 File Offset: 0x00091E32
		public virtual AddIPAllowListProviderCommand SetParameters(AddIPAllowListProviderCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000756 RID: 1878
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003CC6 RID: 15558
			// (set) Token: 0x06005FBA RID: 24506 RVA: 0x00093C3C File Offset: 0x00091E3C
			public virtual SmtpDomain LookupDomain
			{
				set
				{
					base.PowerSharpParameters["LookupDomain"] = value;
				}
			}

			// Token: 0x17003CC7 RID: 15559
			// (set) Token: 0x06005FBB RID: 24507 RVA: 0x00093C4F File Offset: 0x00091E4F
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17003CC8 RID: 15560
			// (set) Token: 0x06005FBC RID: 24508 RVA: 0x00093C67 File Offset: 0x00091E67
			public virtual bool AnyMatch
			{
				set
				{
					base.PowerSharpParameters["AnyMatch"] = value;
				}
			}

			// Token: 0x17003CC9 RID: 15561
			// (set) Token: 0x06005FBD RID: 24509 RVA: 0x00093C7F File Offset: 0x00091E7F
			public virtual IPAddress BitmaskMatch
			{
				set
				{
					base.PowerSharpParameters["BitmaskMatch"] = value;
				}
			}

			// Token: 0x17003CCA RID: 15562
			// (set) Token: 0x06005FBE RID: 24510 RVA: 0x00093C92 File Offset: 0x00091E92
			public virtual MultiValuedProperty<IPAddress> IPAddressesMatch
			{
				set
				{
					base.PowerSharpParameters["IPAddressesMatch"] = value;
				}
			}

			// Token: 0x17003CCB RID: 15563
			// (set) Token: 0x06005FBF RID: 24511 RVA: 0x00093CA5 File Offset: 0x00091EA5
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17003CCC RID: 15564
			// (set) Token: 0x06005FC0 RID: 24512 RVA: 0x00093CBD File Offset: 0x00091EBD
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003CCD RID: 15565
			// (set) Token: 0x06005FC1 RID: 24513 RVA: 0x00093CD0 File Offset: 0x00091ED0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003CCE RID: 15566
			// (set) Token: 0x06005FC2 RID: 24514 RVA: 0x00093CE3 File Offset: 0x00091EE3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003CCF RID: 15567
			// (set) Token: 0x06005FC3 RID: 24515 RVA: 0x00093CFB File Offset: 0x00091EFB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003CD0 RID: 15568
			// (set) Token: 0x06005FC4 RID: 24516 RVA: 0x00093D13 File Offset: 0x00091F13
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003CD1 RID: 15569
			// (set) Token: 0x06005FC5 RID: 24517 RVA: 0x00093D2B File Offset: 0x00091F2B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003CD2 RID: 15570
			// (set) Token: 0x06005FC6 RID: 24518 RVA: 0x00093D43 File Offset: 0x00091F43
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
