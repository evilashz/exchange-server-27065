using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006A5 RID: 1701
	public class SetOrganizationRelationshipCommand : SyntheticCommandWithPipelineInputNoOutput<OrganizationRelationship>
	{
		// Token: 0x060059DF RID: 23007 RVA: 0x0008C58A File Offset: 0x0008A78A
		private SetOrganizationRelationshipCommand() : base("Set-OrganizationRelationship")
		{
		}

		// Token: 0x060059E0 RID: 23008 RVA: 0x0008C597 File Offset: 0x0008A797
		public SetOrganizationRelationshipCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060059E1 RID: 23009 RVA: 0x0008C5A6 File Offset: 0x0008A7A6
		public virtual SetOrganizationRelationshipCommand SetParameters(SetOrganizationRelationshipCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060059E2 RID: 23010 RVA: 0x0008C5B0 File Offset: 0x0008A7B0
		public virtual SetOrganizationRelationshipCommand SetParameters(SetOrganizationRelationshipCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006A6 RID: 1702
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700384E RID: 14414
			// (set) Token: 0x060059E3 RID: 23011 RVA: 0x0008C5BA File Offset: 0x0008A7BA
			public virtual MultiValuedProperty<SmtpDomain> DomainNames
			{
				set
				{
					base.PowerSharpParameters["DomainNames"] = value;
				}
			}

			// Token: 0x1700384F RID: 14415
			// (set) Token: 0x060059E4 RID: 23012 RVA: 0x0008C5CD File Offset: 0x0008A7CD
			public virtual bool FreeBusyAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["FreeBusyAccessEnabled"] = value;
				}
			}

			// Token: 0x17003850 RID: 14416
			// (set) Token: 0x060059E5 RID: 23013 RVA: 0x0008C5E5 File Offset: 0x0008A7E5
			public virtual FreeBusyAccessLevel FreeBusyAccessLevel
			{
				set
				{
					base.PowerSharpParameters["FreeBusyAccessLevel"] = value;
				}
			}

			// Token: 0x17003851 RID: 14417
			// (set) Token: 0x060059E6 RID: 23014 RVA: 0x0008C5FD File Offset: 0x0008A7FD
			public virtual string FreeBusyAccessScope
			{
				set
				{
					base.PowerSharpParameters["FreeBusyAccessScope"] = ((value != null) ? new GroupIdParameter(value) : null);
				}
			}

			// Token: 0x17003852 RID: 14418
			// (set) Token: 0x060059E7 RID: 23015 RVA: 0x0008C61B File Offset: 0x0008A81B
			public virtual bool MailboxMoveEnabled
			{
				set
				{
					base.PowerSharpParameters["MailboxMoveEnabled"] = value;
				}
			}

			// Token: 0x17003853 RID: 14419
			// (set) Token: 0x060059E8 RID: 23016 RVA: 0x0008C633 File Offset: 0x0008A833
			public virtual bool DeliveryReportEnabled
			{
				set
				{
					base.PowerSharpParameters["DeliveryReportEnabled"] = value;
				}
			}

			// Token: 0x17003854 RID: 14420
			// (set) Token: 0x060059E9 RID: 23017 RVA: 0x0008C64B File Offset: 0x0008A84B
			public virtual bool MailTipsAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["MailTipsAccessEnabled"] = value;
				}
			}

			// Token: 0x17003855 RID: 14421
			// (set) Token: 0x060059EA RID: 23018 RVA: 0x0008C663 File Offset: 0x0008A863
			public virtual MailTipsAccessLevel MailTipsAccessLevel
			{
				set
				{
					base.PowerSharpParameters["MailTipsAccessLevel"] = value;
				}
			}

			// Token: 0x17003856 RID: 14422
			// (set) Token: 0x060059EB RID: 23019 RVA: 0x0008C67B File Offset: 0x0008A87B
			public virtual string MailTipsAccessScope
			{
				set
				{
					base.PowerSharpParameters["MailTipsAccessScope"] = ((value != null) ? new GroupIdParameter(value) : null);
				}
			}

			// Token: 0x17003857 RID: 14423
			// (set) Token: 0x060059EC RID: 23020 RVA: 0x0008C699 File Offset: 0x0008A899
			public virtual bool ArchiveAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["ArchiveAccessEnabled"] = value;
				}
			}

			// Token: 0x17003858 RID: 14424
			// (set) Token: 0x060059ED RID: 23021 RVA: 0x0008C6B1 File Offset: 0x0008A8B1
			public virtual bool PhotosEnabled
			{
				set
				{
					base.PowerSharpParameters["PhotosEnabled"] = value;
				}
			}

			// Token: 0x17003859 RID: 14425
			// (set) Token: 0x060059EE RID: 23022 RVA: 0x0008C6C9 File Offset: 0x0008A8C9
			public virtual Uri TargetApplicationUri
			{
				set
				{
					base.PowerSharpParameters["TargetApplicationUri"] = value;
				}
			}

			// Token: 0x1700385A RID: 14426
			// (set) Token: 0x060059EF RID: 23023 RVA: 0x0008C6DC File Offset: 0x0008A8DC
			public virtual Uri TargetSharingEpr
			{
				set
				{
					base.PowerSharpParameters["TargetSharingEpr"] = value;
				}
			}

			// Token: 0x1700385B RID: 14427
			// (set) Token: 0x060059F0 RID: 23024 RVA: 0x0008C6EF File Offset: 0x0008A8EF
			public virtual Uri TargetAutodiscoverEpr
			{
				set
				{
					base.PowerSharpParameters["TargetAutodiscoverEpr"] = value;
				}
			}

			// Token: 0x1700385C RID: 14428
			// (set) Token: 0x060059F1 RID: 23025 RVA: 0x0008C702 File Offset: 0x0008A902
			public virtual Uri TargetOwaURL
			{
				set
				{
					base.PowerSharpParameters["TargetOwaURL"] = value;
				}
			}

			// Token: 0x1700385D RID: 14429
			// (set) Token: 0x060059F2 RID: 23026 RVA: 0x0008C715 File Offset: 0x0008A915
			public virtual SmtpAddress OrganizationContact
			{
				set
				{
					base.PowerSharpParameters["OrganizationContact"] = value;
				}
			}

			// Token: 0x1700385E RID: 14430
			// (set) Token: 0x060059F3 RID: 23027 RVA: 0x0008C72D File Offset: 0x0008A92D
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700385F RID: 14431
			// (set) Token: 0x060059F4 RID: 23028 RVA: 0x0008C745 File Offset: 0x0008A945
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17003860 RID: 14432
			// (set) Token: 0x060059F5 RID: 23029 RVA: 0x0008C75D File Offset: 0x0008A95D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003861 RID: 14433
			// (set) Token: 0x060059F6 RID: 23030 RVA: 0x0008C770 File Offset: 0x0008A970
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003862 RID: 14434
			// (set) Token: 0x060059F7 RID: 23031 RVA: 0x0008C783 File Offset: 0x0008A983
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003863 RID: 14435
			// (set) Token: 0x060059F8 RID: 23032 RVA: 0x0008C79B File Offset: 0x0008A99B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003864 RID: 14436
			// (set) Token: 0x060059F9 RID: 23033 RVA: 0x0008C7B3 File Offset: 0x0008A9B3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003865 RID: 14437
			// (set) Token: 0x060059FA RID: 23034 RVA: 0x0008C7CB File Offset: 0x0008A9CB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003866 RID: 14438
			// (set) Token: 0x060059FB RID: 23035 RVA: 0x0008C7E3 File Offset: 0x0008A9E3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020006A7 RID: 1703
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003867 RID: 14439
			// (set) Token: 0x060059FD RID: 23037 RVA: 0x0008C803 File Offset: 0x0008AA03
			public virtual OrganizationRelationshipIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17003868 RID: 14440
			// (set) Token: 0x060059FE RID: 23038 RVA: 0x0008C816 File Offset: 0x0008AA16
			public virtual MultiValuedProperty<SmtpDomain> DomainNames
			{
				set
				{
					base.PowerSharpParameters["DomainNames"] = value;
				}
			}

			// Token: 0x17003869 RID: 14441
			// (set) Token: 0x060059FF RID: 23039 RVA: 0x0008C829 File Offset: 0x0008AA29
			public virtual bool FreeBusyAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["FreeBusyAccessEnabled"] = value;
				}
			}

			// Token: 0x1700386A RID: 14442
			// (set) Token: 0x06005A00 RID: 23040 RVA: 0x0008C841 File Offset: 0x0008AA41
			public virtual FreeBusyAccessLevel FreeBusyAccessLevel
			{
				set
				{
					base.PowerSharpParameters["FreeBusyAccessLevel"] = value;
				}
			}

			// Token: 0x1700386B RID: 14443
			// (set) Token: 0x06005A01 RID: 23041 RVA: 0x0008C859 File Offset: 0x0008AA59
			public virtual string FreeBusyAccessScope
			{
				set
				{
					base.PowerSharpParameters["FreeBusyAccessScope"] = ((value != null) ? new GroupIdParameter(value) : null);
				}
			}

			// Token: 0x1700386C RID: 14444
			// (set) Token: 0x06005A02 RID: 23042 RVA: 0x0008C877 File Offset: 0x0008AA77
			public virtual bool MailboxMoveEnabled
			{
				set
				{
					base.PowerSharpParameters["MailboxMoveEnabled"] = value;
				}
			}

			// Token: 0x1700386D RID: 14445
			// (set) Token: 0x06005A03 RID: 23043 RVA: 0x0008C88F File Offset: 0x0008AA8F
			public virtual bool DeliveryReportEnabled
			{
				set
				{
					base.PowerSharpParameters["DeliveryReportEnabled"] = value;
				}
			}

			// Token: 0x1700386E RID: 14446
			// (set) Token: 0x06005A04 RID: 23044 RVA: 0x0008C8A7 File Offset: 0x0008AAA7
			public virtual bool MailTipsAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["MailTipsAccessEnabled"] = value;
				}
			}

			// Token: 0x1700386F RID: 14447
			// (set) Token: 0x06005A05 RID: 23045 RVA: 0x0008C8BF File Offset: 0x0008AABF
			public virtual MailTipsAccessLevel MailTipsAccessLevel
			{
				set
				{
					base.PowerSharpParameters["MailTipsAccessLevel"] = value;
				}
			}

			// Token: 0x17003870 RID: 14448
			// (set) Token: 0x06005A06 RID: 23046 RVA: 0x0008C8D7 File Offset: 0x0008AAD7
			public virtual string MailTipsAccessScope
			{
				set
				{
					base.PowerSharpParameters["MailTipsAccessScope"] = ((value != null) ? new GroupIdParameter(value) : null);
				}
			}

			// Token: 0x17003871 RID: 14449
			// (set) Token: 0x06005A07 RID: 23047 RVA: 0x0008C8F5 File Offset: 0x0008AAF5
			public virtual bool ArchiveAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["ArchiveAccessEnabled"] = value;
				}
			}

			// Token: 0x17003872 RID: 14450
			// (set) Token: 0x06005A08 RID: 23048 RVA: 0x0008C90D File Offset: 0x0008AB0D
			public virtual bool PhotosEnabled
			{
				set
				{
					base.PowerSharpParameters["PhotosEnabled"] = value;
				}
			}

			// Token: 0x17003873 RID: 14451
			// (set) Token: 0x06005A09 RID: 23049 RVA: 0x0008C925 File Offset: 0x0008AB25
			public virtual Uri TargetApplicationUri
			{
				set
				{
					base.PowerSharpParameters["TargetApplicationUri"] = value;
				}
			}

			// Token: 0x17003874 RID: 14452
			// (set) Token: 0x06005A0A RID: 23050 RVA: 0x0008C938 File Offset: 0x0008AB38
			public virtual Uri TargetSharingEpr
			{
				set
				{
					base.PowerSharpParameters["TargetSharingEpr"] = value;
				}
			}

			// Token: 0x17003875 RID: 14453
			// (set) Token: 0x06005A0B RID: 23051 RVA: 0x0008C94B File Offset: 0x0008AB4B
			public virtual Uri TargetAutodiscoverEpr
			{
				set
				{
					base.PowerSharpParameters["TargetAutodiscoverEpr"] = value;
				}
			}

			// Token: 0x17003876 RID: 14454
			// (set) Token: 0x06005A0C RID: 23052 RVA: 0x0008C95E File Offset: 0x0008AB5E
			public virtual Uri TargetOwaURL
			{
				set
				{
					base.PowerSharpParameters["TargetOwaURL"] = value;
				}
			}

			// Token: 0x17003877 RID: 14455
			// (set) Token: 0x06005A0D RID: 23053 RVA: 0x0008C971 File Offset: 0x0008AB71
			public virtual SmtpAddress OrganizationContact
			{
				set
				{
					base.PowerSharpParameters["OrganizationContact"] = value;
				}
			}

			// Token: 0x17003878 RID: 14456
			// (set) Token: 0x06005A0E RID: 23054 RVA: 0x0008C989 File Offset: 0x0008AB89
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17003879 RID: 14457
			// (set) Token: 0x06005A0F RID: 23055 RVA: 0x0008C9A1 File Offset: 0x0008ABA1
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700387A RID: 14458
			// (set) Token: 0x06005A10 RID: 23056 RVA: 0x0008C9B9 File Offset: 0x0008ABB9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700387B RID: 14459
			// (set) Token: 0x06005A11 RID: 23057 RVA: 0x0008C9CC File Offset: 0x0008ABCC
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700387C RID: 14460
			// (set) Token: 0x06005A12 RID: 23058 RVA: 0x0008C9DF File Offset: 0x0008ABDF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700387D RID: 14461
			// (set) Token: 0x06005A13 RID: 23059 RVA: 0x0008C9F7 File Offset: 0x0008ABF7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700387E RID: 14462
			// (set) Token: 0x06005A14 RID: 23060 RVA: 0x0008CA0F File Offset: 0x0008AC0F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700387F RID: 14463
			// (set) Token: 0x06005A15 RID: 23061 RVA: 0x0008CA27 File Offset: 0x0008AC27
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003880 RID: 14464
			// (set) Token: 0x06005A16 RID: 23062 RVA: 0x0008CA3F File Offset: 0x0008AC3F
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
