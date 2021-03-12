using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000485 RID: 1157
	public class SetMailboxCalendarFolderCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxCalendarFolder>
	{
		// Token: 0x0600416B RID: 16747 RVA: 0x0006CA1E File Offset: 0x0006AC1E
		private SetMailboxCalendarFolderCommand() : base("Set-MailboxCalendarFolder")
		{
		}

		// Token: 0x0600416C RID: 16748 RVA: 0x0006CA2B File Offset: 0x0006AC2B
		public SetMailboxCalendarFolderCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600416D RID: 16749 RVA: 0x0006CA3A File Offset: 0x0006AC3A
		public virtual SetMailboxCalendarFolderCommand SetParameters(SetMailboxCalendarFolderCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600416E RID: 16750 RVA: 0x0006CA44 File Offset: 0x0006AC44
		public virtual SetMailboxCalendarFolderCommand SetParameters(SetMailboxCalendarFolderCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000486 RID: 1158
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700241A RID: 9242
			// (set) Token: 0x0600416F RID: 16751 RVA: 0x0006CA4E File Offset: 0x0006AC4E
			public virtual SwitchParameter ResetUrl
			{
				set
				{
					base.PowerSharpParameters["ResetUrl"] = value;
				}
			}

			// Token: 0x1700241B RID: 9243
			// (set) Token: 0x06004170 RID: 16752 RVA: 0x0006CA66 File Offset: 0x0006AC66
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700241C RID: 9244
			// (set) Token: 0x06004171 RID: 16753 RVA: 0x0006CA79 File Offset: 0x0006AC79
			public virtual bool PublishEnabled
			{
				set
				{
					base.PowerSharpParameters["PublishEnabled"] = value;
				}
			}

			// Token: 0x1700241D RID: 9245
			// (set) Token: 0x06004172 RID: 16754 RVA: 0x0006CA91 File Offset: 0x0006AC91
			public virtual DateRangeEnumType PublishDateRangeFrom
			{
				set
				{
					base.PowerSharpParameters["PublishDateRangeFrom"] = value;
				}
			}

			// Token: 0x1700241E RID: 9246
			// (set) Token: 0x06004173 RID: 16755 RVA: 0x0006CAA9 File Offset: 0x0006ACA9
			public virtual DateRangeEnumType PublishDateRangeTo
			{
				set
				{
					base.PowerSharpParameters["PublishDateRangeTo"] = value;
				}
			}

			// Token: 0x1700241F RID: 9247
			// (set) Token: 0x06004174 RID: 16756 RVA: 0x0006CAC1 File Offset: 0x0006ACC1
			public virtual DetailLevelEnumType DetailLevel
			{
				set
				{
					base.PowerSharpParameters["DetailLevel"] = value;
				}
			}

			// Token: 0x17002420 RID: 9248
			// (set) Token: 0x06004175 RID: 16757 RVA: 0x0006CAD9 File Offset: 0x0006ACD9
			public virtual bool SearchableUrlEnabled
			{
				set
				{
					base.PowerSharpParameters["SearchableUrlEnabled"] = value;
				}
			}

			// Token: 0x17002421 RID: 9249
			// (set) Token: 0x06004176 RID: 16758 RVA: 0x0006CAF1 File Offset: 0x0006ACF1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002422 RID: 9250
			// (set) Token: 0x06004177 RID: 16759 RVA: 0x0006CB09 File Offset: 0x0006AD09
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002423 RID: 9251
			// (set) Token: 0x06004178 RID: 16760 RVA: 0x0006CB21 File Offset: 0x0006AD21
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002424 RID: 9252
			// (set) Token: 0x06004179 RID: 16761 RVA: 0x0006CB39 File Offset: 0x0006AD39
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002425 RID: 9253
			// (set) Token: 0x0600417A RID: 16762 RVA: 0x0006CB51 File Offset: 0x0006AD51
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000487 RID: 1159
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002426 RID: 9254
			// (set) Token: 0x0600417C RID: 16764 RVA: 0x0006CB71 File Offset: 0x0006AD71
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17002427 RID: 9255
			// (set) Token: 0x0600417D RID: 16765 RVA: 0x0006CB8F File Offset: 0x0006AD8F
			public virtual SwitchParameter ResetUrl
			{
				set
				{
					base.PowerSharpParameters["ResetUrl"] = value;
				}
			}

			// Token: 0x17002428 RID: 9256
			// (set) Token: 0x0600417E RID: 16766 RVA: 0x0006CBA7 File Offset: 0x0006ADA7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002429 RID: 9257
			// (set) Token: 0x0600417F RID: 16767 RVA: 0x0006CBBA File Offset: 0x0006ADBA
			public virtual bool PublishEnabled
			{
				set
				{
					base.PowerSharpParameters["PublishEnabled"] = value;
				}
			}

			// Token: 0x1700242A RID: 9258
			// (set) Token: 0x06004180 RID: 16768 RVA: 0x0006CBD2 File Offset: 0x0006ADD2
			public virtual DateRangeEnumType PublishDateRangeFrom
			{
				set
				{
					base.PowerSharpParameters["PublishDateRangeFrom"] = value;
				}
			}

			// Token: 0x1700242B RID: 9259
			// (set) Token: 0x06004181 RID: 16769 RVA: 0x0006CBEA File Offset: 0x0006ADEA
			public virtual DateRangeEnumType PublishDateRangeTo
			{
				set
				{
					base.PowerSharpParameters["PublishDateRangeTo"] = value;
				}
			}

			// Token: 0x1700242C RID: 9260
			// (set) Token: 0x06004182 RID: 16770 RVA: 0x0006CC02 File Offset: 0x0006AE02
			public virtual DetailLevelEnumType DetailLevel
			{
				set
				{
					base.PowerSharpParameters["DetailLevel"] = value;
				}
			}

			// Token: 0x1700242D RID: 9261
			// (set) Token: 0x06004183 RID: 16771 RVA: 0x0006CC1A File Offset: 0x0006AE1A
			public virtual bool SearchableUrlEnabled
			{
				set
				{
					base.PowerSharpParameters["SearchableUrlEnabled"] = value;
				}
			}

			// Token: 0x1700242E RID: 9262
			// (set) Token: 0x06004184 RID: 16772 RVA: 0x0006CC32 File Offset: 0x0006AE32
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700242F RID: 9263
			// (set) Token: 0x06004185 RID: 16773 RVA: 0x0006CC4A File Offset: 0x0006AE4A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002430 RID: 9264
			// (set) Token: 0x06004186 RID: 16774 RVA: 0x0006CC62 File Offset: 0x0006AE62
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002431 RID: 9265
			// (set) Token: 0x06004187 RID: 16775 RVA: 0x0006CC7A File Offset: 0x0006AE7A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002432 RID: 9266
			// (set) Token: 0x06004188 RID: 16776 RVA: 0x0006CC92 File Offset: 0x0006AE92
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
