using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C1B RID: 3099
	public class SetGroupCommand : SyntheticCommandWithPipelineInputNoOutput<WindowsGroup>
	{
		// Token: 0x0600976E RID: 38766 RVA: 0x000DC496 File Offset: 0x000DA696
		private SetGroupCommand() : base("Set-Group")
		{
		}

		// Token: 0x0600976F RID: 38767 RVA: 0x000DC4A3 File Offset: 0x000DA6A3
		public SetGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009770 RID: 38768 RVA: 0x000DC4B2 File Offset: 0x000DA6B2
		public virtual SetGroupCommand SetParameters(SetGroupCommand.UniversalParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009771 RID: 38769 RVA: 0x000DC4BC File Offset: 0x000DA6BC
		public virtual SetGroupCommand SetParameters(SetGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009772 RID: 38770 RVA: 0x000DC4C6 File Offset: 0x000DA6C6
		public virtual SetGroupCommand SetParameters(SetGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C1C RID: 3100
		public class UniversalParameters : ParametersBase
		{
			// Token: 0x17006AF1 RID: 27377
			// (set) Token: 0x06009773 RID: 38771 RVA: 0x000DC4D0 File Offset: 0x000DA6D0
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new GroupIdParameter(value) : null);
				}
			}

			// Token: 0x17006AF2 RID: 27378
			// (set) Token: 0x06009774 RID: 38772 RVA: 0x000DC4EE File Offset: 0x000DA6EE
			public virtual SwitchParameter Universal
			{
				set
				{
					base.PowerSharpParameters["Universal"] = value;
				}
			}

			// Token: 0x17006AF3 RID: 27379
			// (set) Token: 0x06009775 RID: 38773 RVA: 0x000DC506 File Offset: 0x000DA706
			public virtual GeneralRecipientIdParameter ManagedBy
			{
				set
				{
					base.PowerSharpParameters["ManagedBy"] = value;
				}
			}

			// Token: 0x17006AF4 RID: 27380
			// (set) Token: 0x06009776 RID: 38774 RVA: 0x000DC519 File Offset: 0x000DA719
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x17006AF5 RID: 27381
			// (set) Token: 0x06009777 RID: 38775 RVA: 0x000DC531 File Offset: 0x000DA731
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006AF6 RID: 27382
			// (set) Token: 0x06009778 RID: 38776 RVA: 0x000DC549 File Offset: 0x000DA749
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006AF7 RID: 27383
			// (set) Token: 0x06009779 RID: 38777 RVA: 0x000DC55C File Offset: 0x000DA75C
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006AF8 RID: 27384
			// (set) Token: 0x0600977A RID: 38778 RVA: 0x000DC56F File Offset: 0x000DA76F
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x17006AF9 RID: 27385
			// (set) Token: 0x0600977B RID: 38779 RVA: 0x000DC582 File Offset: 0x000DA782
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x17006AFA RID: 27386
			// (set) Token: 0x0600977C RID: 38780 RVA: 0x000DC59A File Offset: 0x000DA79A
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17006AFB RID: 27387
			// (set) Token: 0x0600977D RID: 38781 RVA: 0x000DC5AD File Offset: 0x000DA7AD
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17006AFC RID: 27388
			// (set) Token: 0x0600977E RID: 38782 RVA: 0x000DC5C0 File Offset: 0x000DA7C0
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17006AFD RID: 27389
			// (set) Token: 0x0600977F RID: 38783 RVA: 0x000DC5D8 File Offset: 0x000DA7D8
			public virtual bool IsHierarchicalGroup
			{
				set
				{
					base.PowerSharpParameters["IsHierarchicalGroup"] = value;
				}
			}

			// Token: 0x17006AFE RID: 27390
			// (set) Token: 0x06009780 RID: 38784 RVA: 0x000DC5F0 File Offset: 0x000DA7F0
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006AFF RID: 27391
			// (set) Token: 0x06009781 RID: 38785 RVA: 0x000DC603 File Offset: 0x000DA803
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006B00 RID: 27392
			// (set) Token: 0x06009782 RID: 38786 RVA: 0x000DC61B File Offset: 0x000DA81B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006B01 RID: 27393
			// (set) Token: 0x06009783 RID: 38787 RVA: 0x000DC633 File Offset: 0x000DA833
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006B02 RID: 27394
			// (set) Token: 0x06009784 RID: 38788 RVA: 0x000DC64B File Offset: 0x000DA84B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006B03 RID: 27395
			// (set) Token: 0x06009785 RID: 38789 RVA: 0x000DC663 File Offset: 0x000DA863
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C1D RID: 3101
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006B04 RID: 27396
			// (set) Token: 0x06009787 RID: 38791 RVA: 0x000DC683 File Offset: 0x000DA883
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new GroupIdParameter(value) : null);
				}
			}

			// Token: 0x17006B05 RID: 27397
			// (set) Token: 0x06009788 RID: 38792 RVA: 0x000DC6A1 File Offset: 0x000DA8A1
			public virtual GeneralRecipientIdParameter ManagedBy
			{
				set
				{
					base.PowerSharpParameters["ManagedBy"] = value;
				}
			}

			// Token: 0x17006B06 RID: 27398
			// (set) Token: 0x06009789 RID: 38793 RVA: 0x000DC6B4 File Offset: 0x000DA8B4
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x17006B07 RID: 27399
			// (set) Token: 0x0600978A RID: 38794 RVA: 0x000DC6CC File Offset: 0x000DA8CC
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006B08 RID: 27400
			// (set) Token: 0x0600978B RID: 38795 RVA: 0x000DC6E4 File Offset: 0x000DA8E4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006B09 RID: 27401
			// (set) Token: 0x0600978C RID: 38796 RVA: 0x000DC6F7 File Offset: 0x000DA8F7
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006B0A RID: 27402
			// (set) Token: 0x0600978D RID: 38797 RVA: 0x000DC70A File Offset: 0x000DA90A
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x17006B0B RID: 27403
			// (set) Token: 0x0600978E RID: 38798 RVA: 0x000DC71D File Offset: 0x000DA91D
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x17006B0C RID: 27404
			// (set) Token: 0x0600978F RID: 38799 RVA: 0x000DC735 File Offset: 0x000DA935
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17006B0D RID: 27405
			// (set) Token: 0x06009790 RID: 38800 RVA: 0x000DC748 File Offset: 0x000DA948
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17006B0E RID: 27406
			// (set) Token: 0x06009791 RID: 38801 RVA: 0x000DC75B File Offset: 0x000DA95B
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17006B0F RID: 27407
			// (set) Token: 0x06009792 RID: 38802 RVA: 0x000DC773 File Offset: 0x000DA973
			public virtual bool IsHierarchicalGroup
			{
				set
				{
					base.PowerSharpParameters["IsHierarchicalGroup"] = value;
				}
			}

			// Token: 0x17006B10 RID: 27408
			// (set) Token: 0x06009793 RID: 38803 RVA: 0x000DC78B File Offset: 0x000DA98B
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006B11 RID: 27409
			// (set) Token: 0x06009794 RID: 38804 RVA: 0x000DC79E File Offset: 0x000DA99E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006B12 RID: 27410
			// (set) Token: 0x06009795 RID: 38805 RVA: 0x000DC7B6 File Offset: 0x000DA9B6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006B13 RID: 27411
			// (set) Token: 0x06009796 RID: 38806 RVA: 0x000DC7CE File Offset: 0x000DA9CE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006B14 RID: 27412
			// (set) Token: 0x06009797 RID: 38807 RVA: 0x000DC7E6 File Offset: 0x000DA9E6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006B15 RID: 27413
			// (set) Token: 0x06009798 RID: 38808 RVA: 0x000DC7FE File Offset: 0x000DA9FE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C1E RID: 3102
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006B16 RID: 27414
			// (set) Token: 0x0600979A RID: 38810 RVA: 0x000DC81E File Offset: 0x000DAA1E
			public virtual GeneralRecipientIdParameter ManagedBy
			{
				set
				{
					base.PowerSharpParameters["ManagedBy"] = value;
				}
			}

			// Token: 0x17006B17 RID: 27415
			// (set) Token: 0x0600979B RID: 38811 RVA: 0x000DC831 File Offset: 0x000DAA31
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x17006B18 RID: 27416
			// (set) Token: 0x0600979C RID: 38812 RVA: 0x000DC849 File Offset: 0x000DAA49
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006B19 RID: 27417
			// (set) Token: 0x0600979D RID: 38813 RVA: 0x000DC861 File Offset: 0x000DAA61
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006B1A RID: 27418
			// (set) Token: 0x0600979E RID: 38814 RVA: 0x000DC874 File Offset: 0x000DAA74
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006B1B RID: 27419
			// (set) Token: 0x0600979F RID: 38815 RVA: 0x000DC887 File Offset: 0x000DAA87
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x17006B1C RID: 27420
			// (set) Token: 0x060097A0 RID: 38816 RVA: 0x000DC89A File Offset: 0x000DAA9A
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x17006B1D RID: 27421
			// (set) Token: 0x060097A1 RID: 38817 RVA: 0x000DC8B2 File Offset: 0x000DAAB2
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17006B1E RID: 27422
			// (set) Token: 0x060097A2 RID: 38818 RVA: 0x000DC8C5 File Offset: 0x000DAAC5
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17006B1F RID: 27423
			// (set) Token: 0x060097A3 RID: 38819 RVA: 0x000DC8D8 File Offset: 0x000DAAD8
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x17006B20 RID: 27424
			// (set) Token: 0x060097A4 RID: 38820 RVA: 0x000DC8F0 File Offset: 0x000DAAF0
			public virtual bool IsHierarchicalGroup
			{
				set
				{
					base.PowerSharpParameters["IsHierarchicalGroup"] = value;
				}
			}

			// Token: 0x17006B21 RID: 27425
			// (set) Token: 0x060097A5 RID: 38821 RVA: 0x000DC908 File Offset: 0x000DAB08
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006B22 RID: 27426
			// (set) Token: 0x060097A6 RID: 38822 RVA: 0x000DC91B File Offset: 0x000DAB1B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006B23 RID: 27427
			// (set) Token: 0x060097A7 RID: 38823 RVA: 0x000DC933 File Offset: 0x000DAB33
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006B24 RID: 27428
			// (set) Token: 0x060097A8 RID: 38824 RVA: 0x000DC94B File Offset: 0x000DAB4B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006B25 RID: 27429
			// (set) Token: 0x060097A9 RID: 38825 RVA: 0x000DC963 File Offset: 0x000DAB63
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006B26 RID: 27430
			// (set) Token: 0x060097AA RID: 38826 RVA: 0x000DC97B File Offset: 0x000DAB7B
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
