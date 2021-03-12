using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007C4 RID: 1988
	[Cmdlet("Set", "MailboxCalendarConfiguration", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMailboxCalendarConfiguration : SetMailboxConfigurationTaskBase<MailboxCalendarConfiguration>
	{
		// Token: 0x17001514 RID: 5396
		// (get) Token: 0x060045CB RID: 17867 RVA: 0x0011EE05 File Offset: 0x0011D005
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageMailboxCalendarConfiguration(this.Identity.ToString());
			}
		}

		// Token: 0x060045CC RID: 17868 RVA: 0x0011EE18 File Offset: 0x0011D018
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.Instance.IsModified(MailboxCalendarConfigurationSchema.FirstWeekOfYear) && this.Instance.FirstWeekOfYear == FirstWeekRules.LegacyNotSet)
			{
				base.WriteError(new InvalidParamException(Strings.ErrorMailboxCalendarConfigurationNotAllowedParameterValue("FirstWeekOfYear", "LegacyNotSet", "FirstDay, FirstFourDayWeek, FirstFullWeek")), ExchangeErrorCategory.Client, this.Identity);
			}
		}

		// Token: 0x060045CD RID: 17869 RVA: 0x0011EE74 File Offset: 0x0011D074
		protected override void InternalProcessRecord()
		{
			if (this.DataObject.FirstWeekOfYear == FirstWeekRules.LegacyNotSet)
			{
				this.DataObject.FirstWeekOfYear = FirstWeekRules.FirstDay;
			}
			base.InternalProcessRecord();
		}
	}
}
