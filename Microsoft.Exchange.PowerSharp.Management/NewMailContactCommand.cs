using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000CEE RID: 3310
	public class NewMailContactCommand : SyntheticCommandWithPipelineInputNoOutput<ProxyAddress>
	{
		// Token: 0x0600ADE4 RID: 44516 RVA: 0x000FB46B File Offset: 0x000F966B
		private NewMailContactCommand() : base("New-MailContact")
		{
		}

		// Token: 0x0600ADE5 RID: 44517 RVA: 0x000FB478 File Offset: 0x000F9678
		public NewMailContactCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600ADE6 RID: 44518 RVA: 0x000FB487 File Offset: 0x000F9687
		public virtual NewMailContactCommand SetParameters(NewMailContactCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000CEF RID: 3311
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17007FC1 RID: 32705
			// (set) Token: 0x0600ADE7 RID: 44519 RVA: 0x000FB491 File Offset: 0x000F9691
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x17007FC2 RID: 32706
			// (set) Token: 0x0600ADE8 RID: 44520 RVA: 0x000FB4A4 File Offset: 0x000F96A4
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007FC3 RID: 32707
			// (set) Token: 0x0600ADE9 RID: 44521 RVA: 0x000FB4B7 File Offset: 0x000F96B7
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007FC4 RID: 32708
			// (set) Token: 0x0600ADEA RID: 44522 RVA: 0x000FB4CA File Offset: 0x000F96CA
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007FC5 RID: 32709
			// (set) Token: 0x0600ADEB RID: 44523 RVA: 0x000FB4DD File Offset: 0x000F96DD
			public virtual bool UsePreferMessageFormat
			{
				set
				{
					base.PowerSharpParameters["UsePreferMessageFormat"] = value;
				}
			}

			// Token: 0x17007FC6 RID: 32710
			// (set) Token: 0x0600ADEC RID: 44524 RVA: 0x000FB4F5 File Offset: 0x000F96F5
			public virtual MessageFormat MessageFormat
			{
				set
				{
					base.PowerSharpParameters["MessageFormat"] = value;
				}
			}

			// Token: 0x17007FC7 RID: 32711
			// (set) Token: 0x0600ADED RID: 44525 RVA: 0x000FB50D File Offset: 0x000F970D
			public virtual MessageBodyFormat MessageBodyFormat
			{
				set
				{
					base.PowerSharpParameters["MessageBodyFormat"] = value;
				}
			}

			// Token: 0x17007FC8 RID: 32712
			// (set) Token: 0x0600ADEE RID: 44526 RVA: 0x000FB525 File Offset: 0x000F9725
			public virtual MacAttachmentFormat MacAttachmentFormat
			{
				set
				{
					base.PowerSharpParameters["MacAttachmentFormat"] = value;
				}
			}

			// Token: 0x17007FC9 RID: 32713
			// (set) Token: 0x0600ADEF RID: 44527 RVA: 0x000FB53D File Offset: 0x000F973D
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007FCA RID: 32714
			// (set) Token: 0x0600ADF0 RID: 44528 RVA: 0x000FB55B File Offset: 0x000F975B
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007FCB RID: 32715
			// (set) Token: 0x0600ADF1 RID: 44529 RVA: 0x000FB56E File Offset: 0x000F976E
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17007FCC RID: 32716
			// (set) Token: 0x0600ADF2 RID: 44530 RVA: 0x000FB586 File Offset: 0x000F9786
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17007FCD RID: 32717
			// (set) Token: 0x0600ADF3 RID: 44531 RVA: 0x000FB59E File Offset: 0x000F979E
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007FCE RID: 32718
			// (set) Token: 0x0600ADF4 RID: 44532 RVA: 0x000FB5B6 File Offset: 0x000F97B6
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007FCF RID: 32719
			// (set) Token: 0x0600ADF5 RID: 44533 RVA: 0x000FB5C9 File Offset: 0x000F97C9
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007FD0 RID: 32720
			// (set) Token: 0x0600ADF6 RID: 44534 RVA: 0x000FB5E1 File Offset: 0x000F97E1
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007FD1 RID: 32721
			// (set) Token: 0x0600ADF7 RID: 44535 RVA: 0x000FB5F4 File Offset: 0x000F97F4
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007FD2 RID: 32722
			// (set) Token: 0x0600ADF8 RID: 44536 RVA: 0x000FB607 File Offset: 0x000F9807
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007FD3 RID: 32723
			// (set) Token: 0x0600ADF9 RID: 44537 RVA: 0x000FB625 File Offset: 0x000F9825
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17007FD4 RID: 32724
			// (set) Token: 0x0600ADFA RID: 44538 RVA: 0x000FB638 File Offset: 0x000F9838
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007FD5 RID: 32725
			// (set) Token: 0x0600ADFB RID: 44539 RVA: 0x000FB656 File Offset: 0x000F9856
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007FD6 RID: 32726
			// (set) Token: 0x0600ADFC RID: 44540 RVA: 0x000FB669 File Offset: 0x000F9869
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007FD7 RID: 32727
			// (set) Token: 0x0600ADFD RID: 44541 RVA: 0x000FB681 File Offset: 0x000F9881
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007FD8 RID: 32728
			// (set) Token: 0x0600ADFE RID: 44542 RVA: 0x000FB699 File Offset: 0x000F9899
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007FD9 RID: 32729
			// (set) Token: 0x0600ADFF RID: 44543 RVA: 0x000FB6B1 File Offset: 0x000F98B1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007FDA RID: 32730
			// (set) Token: 0x0600AE00 RID: 44544 RVA: 0x000FB6C9 File Offset: 0x000F98C9
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
