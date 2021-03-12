using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000CE7 RID: 3303
	public class EnableMailContactCommand : SyntheticCommandWithPipelineInput<ADContact, ADContact>
	{
		// Token: 0x0600AD85 RID: 44421 RVA: 0x000FAC89 File Offset: 0x000F8E89
		private EnableMailContactCommand() : base("Enable-MailContact")
		{
		}

		// Token: 0x0600AD86 RID: 44422 RVA: 0x000FAC96 File Offset: 0x000F8E96
		public EnableMailContactCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600AD87 RID: 44423 RVA: 0x000FACA5 File Offset: 0x000F8EA5
		public virtual EnableMailContactCommand SetParameters(EnableMailContactCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AD88 RID: 44424 RVA: 0x000FACAF File Offset: 0x000F8EAF
		public virtual EnableMailContactCommand SetParameters(EnableMailContactCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000CE8 RID: 3304
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17007F70 RID: 32624
			// (set) Token: 0x0600AD89 RID: 44425 RVA: 0x000FACB9 File Offset: 0x000F8EB9
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x17007F71 RID: 32625
			// (set) Token: 0x0600AD8A RID: 44426 RVA: 0x000FACCC File Offset: 0x000F8ECC
			public virtual bool UsePreferMessageFormat
			{
				set
				{
					base.PowerSharpParameters["UsePreferMessageFormat"] = value;
				}
			}

			// Token: 0x17007F72 RID: 32626
			// (set) Token: 0x0600AD8B RID: 44427 RVA: 0x000FACE4 File Offset: 0x000F8EE4
			public virtual MessageFormat MessageFormat
			{
				set
				{
					base.PowerSharpParameters["MessageFormat"] = value;
				}
			}

			// Token: 0x17007F73 RID: 32627
			// (set) Token: 0x0600AD8C RID: 44428 RVA: 0x000FACFC File Offset: 0x000F8EFC
			public virtual MessageBodyFormat MessageBodyFormat
			{
				set
				{
					base.PowerSharpParameters["MessageBodyFormat"] = value;
				}
			}

			// Token: 0x17007F74 RID: 32628
			// (set) Token: 0x0600AD8D RID: 44429 RVA: 0x000FAD14 File Offset: 0x000F8F14
			public virtual MacAttachmentFormat MacAttachmentFormat
			{
				set
				{
					base.PowerSharpParameters["MacAttachmentFormat"] = value;
				}
			}

			// Token: 0x17007F75 RID: 32629
			// (set) Token: 0x0600AD8E RID: 44430 RVA: 0x000FAD2C File Offset: 0x000F8F2C
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007F76 RID: 32630
			// (set) Token: 0x0600AD8F RID: 44431 RVA: 0x000FAD3F File Offset: 0x000F8F3F
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007F77 RID: 32631
			// (set) Token: 0x0600AD90 RID: 44432 RVA: 0x000FAD52 File Offset: 0x000F8F52
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007F78 RID: 32632
			// (set) Token: 0x0600AD91 RID: 44433 RVA: 0x000FAD6A File Offset: 0x000F8F6A
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007F79 RID: 32633
			// (set) Token: 0x0600AD92 RID: 44434 RVA: 0x000FAD82 File Offset: 0x000F8F82
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007F7A RID: 32634
			// (set) Token: 0x0600AD93 RID: 44435 RVA: 0x000FAD95 File Offset: 0x000F8F95
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007F7B RID: 32635
			// (set) Token: 0x0600AD94 RID: 44436 RVA: 0x000FADAD File Offset: 0x000F8FAD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007F7C RID: 32636
			// (set) Token: 0x0600AD95 RID: 44437 RVA: 0x000FADC5 File Offset: 0x000F8FC5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007F7D RID: 32637
			// (set) Token: 0x0600AD96 RID: 44438 RVA: 0x000FADDD File Offset: 0x000F8FDD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007F7E RID: 32638
			// (set) Token: 0x0600AD97 RID: 44439 RVA: 0x000FADF5 File Offset: 0x000F8FF5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CE9 RID: 3305
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17007F7F RID: 32639
			// (set) Token: 0x0600AD99 RID: 44441 RVA: 0x000FAE15 File Offset: 0x000F9015
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ContactIdParameter(value) : null);
				}
			}

			// Token: 0x17007F80 RID: 32640
			// (set) Token: 0x0600AD9A RID: 44442 RVA: 0x000FAE33 File Offset: 0x000F9033
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x17007F81 RID: 32641
			// (set) Token: 0x0600AD9B RID: 44443 RVA: 0x000FAE46 File Offset: 0x000F9046
			public virtual bool UsePreferMessageFormat
			{
				set
				{
					base.PowerSharpParameters["UsePreferMessageFormat"] = value;
				}
			}

			// Token: 0x17007F82 RID: 32642
			// (set) Token: 0x0600AD9C RID: 44444 RVA: 0x000FAE5E File Offset: 0x000F905E
			public virtual MessageFormat MessageFormat
			{
				set
				{
					base.PowerSharpParameters["MessageFormat"] = value;
				}
			}

			// Token: 0x17007F83 RID: 32643
			// (set) Token: 0x0600AD9D RID: 44445 RVA: 0x000FAE76 File Offset: 0x000F9076
			public virtual MessageBodyFormat MessageBodyFormat
			{
				set
				{
					base.PowerSharpParameters["MessageBodyFormat"] = value;
				}
			}

			// Token: 0x17007F84 RID: 32644
			// (set) Token: 0x0600AD9E RID: 44446 RVA: 0x000FAE8E File Offset: 0x000F908E
			public virtual MacAttachmentFormat MacAttachmentFormat
			{
				set
				{
					base.PowerSharpParameters["MacAttachmentFormat"] = value;
				}
			}

			// Token: 0x17007F85 RID: 32645
			// (set) Token: 0x0600AD9F RID: 44447 RVA: 0x000FAEA6 File Offset: 0x000F90A6
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007F86 RID: 32646
			// (set) Token: 0x0600ADA0 RID: 44448 RVA: 0x000FAEB9 File Offset: 0x000F90B9
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007F87 RID: 32647
			// (set) Token: 0x0600ADA1 RID: 44449 RVA: 0x000FAECC File Offset: 0x000F90CC
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007F88 RID: 32648
			// (set) Token: 0x0600ADA2 RID: 44450 RVA: 0x000FAEE4 File Offset: 0x000F90E4
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007F89 RID: 32649
			// (set) Token: 0x0600ADA3 RID: 44451 RVA: 0x000FAEFC File Offset: 0x000F90FC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007F8A RID: 32650
			// (set) Token: 0x0600ADA4 RID: 44452 RVA: 0x000FAF0F File Offset: 0x000F910F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007F8B RID: 32651
			// (set) Token: 0x0600ADA5 RID: 44453 RVA: 0x000FAF27 File Offset: 0x000F9127
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007F8C RID: 32652
			// (set) Token: 0x0600ADA6 RID: 44454 RVA: 0x000FAF3F File Offset: 0x000F913F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007F8D RID: 32653
			// (set) Token: 0x0600ADA7 RID: 44455 RVA: 0x000FAF57 File Offset: 0x000F9157
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007F8E RID: 32654
			// (set) Token: 0x0600ADA8 RID: 44456 RVA: 0x000FAF6F File Offset: 0x000F916F
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
