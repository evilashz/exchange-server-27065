using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200068D RID: 1677
	public class NewOrganizationRelationshipCommand : SyntheticCommandWithPipelineInput<OrganizationRelationship, OrganizationRelationship>
	{
		// Token: 0x06005910 RID: 22800 RVA: 0x0008B554 File Offset: 0x00089754
		private NewOrganizationRelationshipCommand() : base("New-OrganizationRelationship")
		{
		}

		// Token: 0x06005911 RID: 22801 RVA: 0x0008B561 File Offset: 0x00089761
		public NewOrganizationRelationshipCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005912 RID: 22802 RVA: 0x0008B570 File Offset: 0x00089770
		public virtual NewOrganizationRelationshipCommand SetParameters(NewOrganizationRelationshipCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200068E RID: 1678
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170037AF RID: 14255
			// (set) Token: 0x06005913 RID: 22803 RVA: 0x0008B57A File Offset: 0x0008977A
			public virtual MultiValuedProperty<SmtpDomain> DomainNames
			{
				set
				{
					base.PowerSharpParameters["DomainNames"] = value;
				}
			}

			// Token: 0x170037B0 RID: 14256
			// (set) Token: 0x06005914 RID: 22804 RVA: 0x0008B58D File Offset: 0x0008978D
			public virtual bool FreeBusyAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["FreeBusyAccessEnabled"] = value;
				}
			}

			// Token: 0x170037B1 RID: 14257
			// (set) Token: 0x06005915 RID: 22805 RVA: 0x0008B5A5 File Offset: 0x000897A5
			public virtual FreeBusyAccessLevel FreeBusyAccessLevel
			{
				set
				{
					base.PowerSharpParameters["FreeBusyAccessLevel"] = value;
				}
			}

			// Token: 0x170037B2 RID: 14258
			// (set) Token: 0x06005916 RID: 22806 RVA: 0x0008B5BD File Offset: 0x000897BD
			public virtual string FreeBusyAccessScope
			{
				set
				{
					base.PowerSharpParameters["FreeBusyAccessScope"] = ((value != null) ? new GroupIdParameter(value) : null);
				}
			}

			// Token: 0x170037B3 RID: 14259
			// (set) Token: 0x06005917 RID: 22807 RVA: 0x0008B5DB File Offset: 0x000897DB
			public virtual bool MailboxMoveEnabled
			{
				set
				{
					base.PowerSharpParameters["MailboxMoveEnabled"] = value;
				}
			}

			// Token: 0x170037B4 RID: 14260
			// (set) Token: 0x06005918 RID: 22808 RVA: 0x0008B5F3 File Offset: 0x000897F3
			public virtual bool DeliveryReportEnabled
			{
				set
				{
					base.PowerSharpParameters["DeliveryReportEnabled"] = value;
				}
			}

			// Token: 0x170037B5 RID: 14261
			// (set) Token: 0x06005919 RID: 22809 RVA: 0x0008B60B File Offset: 0x0008980B
			public virtual bool MailTipsAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["MailTipsAccessEnabled"] = value;
				}
			}

			// Token: 0x170037B6 RID: 14262
			// (set) Token: 0x0600591A RID: 22810 RVA: 0x0008B623 File Offset: 0x00089823
			public virtual MailTipsAccessLevel MailTipsAccessLevel
			{
				set
				{
					base.PowerSharpParameters["MailTipsAccessLevel"] = value;
				}
			}

			// Token: 0x170037B7 RID: 14263
			// (set) Token: 0x0600591B RID: 22811 RVA: 0x0008B63B File Offset: 0x0008983B
			public virtual string MailTipsAccessScope
			{
				set
				{
					base.PowerSharpParameters["MailTipsAccessScope"] = ((value != null) ? new GroupIdParameter(value) : null);
				}
			}

			// Token: 0x170037B8 RID: 14264
			// (set) Token: 0x0600591C RID: 22812 RVA: 0x0008B659 File Offset: 0x00089859
			public virtual bool ArchiveAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["ArchiveAccessEnabled"] = value;
				}
			}

			// Token: 0x170037B9 RID: 14265
			// (set) Token: 0x0600591D RID: 22813 RVA: 0x0008B671 File Offset: 0x00089871
			public virtual bool PhotosEnabled
			{
				set
				{
					base.PowerSharpParameters["PhotosEnabled"] = value;
				}
			}

			// Token: 0x170037BA RID: 14266
			// (set) Token: 0x0600591E RID: 22814 RVA: 0x0008B689 File Offset: 0x00089889
			public virtual Uri TargetApplicationUri
			{
				set
				{
					base.PowerSharpParameters["TargetApplicationUri"] = value;
				}
			}

			// Token: 0x170037BB RID: 14267
			// (set) Token: 0x0600591F RID: 22815 RVA: 0x0008B69C File Offset: 0x0008989C
			public virtual Uri TargetSharingEpr
			{
				set
				{
					base.PowerSharpParameters["TargetSharingEpr"] = value;
				}
			}

			// Token: 0x170037BC RID: 14268
			// (set) Token: 0x06005920 RID: 22816 RVA: 0x0008B6AF File Offset: 0x000898AF
			public virtual Uri TargetAutodiscoverEpr
			{
				set
				{
					base.PowerSharpParameters["TargetAutodiscoverEpr"] = value;
				}
			}

			// Token: 0x170037BD RID: 14269
			// (set) Token: 0x06005921 RID: 22817 RVA: 0x0008B6C2 File Offset: 0x000898C2
			public virtual SmtpAddress OrganizationContact
			{
				set
				{
					base.PowerSharpParameters["OrganizationContact"] = value;
				}
			}

			// Token: 0x170037BE RID: 14270
			// (set) Token: 0x06005922 RID: 22818 RVA: 0x0008B6DA File Offset: 0x000898DA
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170037BF RID: 14271
			// (set) Token: 0x06005923 RID: 22819 RVA: 0x0008B6F2 File Offset: 0x000898F2
			public virtual Uri TargetOwaURL
			{
				set
				{
					base.PowerSharpParameters["TargetOwaURL"] = value;
				}
			}

			// Token: 0x170037C0 RID: 14272
			// (set) Token: 0x06005924 RID: 22820 RVA: 0x0008B705 File Offset: 0x00089905
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170037C1 RID: 14273
			// (set) Token: 0x06005925 RID: 22821 RVA: 0x0008B723 File Offset: 0x00089923
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170037C2 RID: 14274
			// (set) Token: 0x06005926 RID: 22822 RVA: 0x0008B736 File Offset: 0x00089936
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170037C3 RID: 14275
			// (set) Token: 0x06005927 RID: 22823 RVA: 0x0008B749 File Offset: 0x00089949
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170037C4 RID: 14276
			// (set) Token: 0x06005928 RID: 22824 RVA: 0x0008B761 File Offset: 0x00089961
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170037C5 RID: 14277
			// (set) Token: 0x06005929 RID: 22825 RVA: 0x0008B779 File Offset: 0x00089979
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170037C6 RID: 14278
			// (set) Token: 0x0600592A RID: 22826 RVA: 0x0008B791 File Offset: 0x00089991
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170037C7 RID: 14279
			// (set) Token: 0x0600592B RID: 22827 RVA: 0x0008B7A9 File Offset: 0x000899A9
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
