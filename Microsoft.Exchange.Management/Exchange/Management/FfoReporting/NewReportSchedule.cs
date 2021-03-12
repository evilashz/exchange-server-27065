using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.FfoReporting.Common;
using Microsoft.Exchange.Management.ReportingTask;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003F5 RID: 1013
	[Cmdlet("New", "ReportSchedule")]
	[OutputType(new Type[]
	{
		typeof(ReportSchedule)
	})]
	public sealed class NewReportSchedule : ReportScheduleBaseTask
	{
		// Token: 0x060023B7 RID: 9143 RVA: 0x0008FC30 File Offset: 0x0008DE30
		public NewReportSchedule() : base("NewReportSchedule", "Microsoft.Exchange.Hygiene.ManagementHelper.ReportSchedule.NewReportScheduleHelper")
		{
			this.Locale = Thread.CurrentThread.CurrentCulture;
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x060023B8 RID: 9144 RVA: 0x0008FC52 File Offset: 0x0008DE52
		// (set) Token: 0x060023B9 RID: 9145 RVA: 0x0008FC5A File Offset: 0x0008DE5A
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(MessageDeliveryStatus)
		}, ErrorMessage = Strings.IDs.InvalidDeliveryStatus)]
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false)]
		public string DeliveryStatus { get; set; }

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x060023BA RID: 9146 RVA: 0x0008FC63 File Offset: 0x0008DE63
		// (set) Token: 0x060023BB RID: 9147 RVA: 0x0008FC6B File Offset: 0x0008DE6B
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false)]
		public ReportDirection Direction { get; set; }

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x060023BC RID: 9148 RVA: 0x0008FC74 File Offset: 0x0008DE74
		// (set) Token: 0x060023BD RID: 9149 RVA: 0x0008FC7C File Offset: 0x0008DE7C
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false)]
		public MultiValuedProperty<Guid> DLPPolicy { get; set; }

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x060023BE RID: 9150 RVA: 0x0008FC85 File Offset: 0x0008DE85
		// (set) Token: 0x060023BF RID: 9151 RVA: 0x0008FC8D File Offset: 0x0008DE8D
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false)]
		public string Domain { get; set; }

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x060023C0 RID: 9152 RVA: 0x0008FC96 File Offset: 0x0008DE96
		// (set) Token: 0x060023C1 RID: 9153 RVA: 0x0008FC9E File Offset: 0x0008DE9E
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
		public DateTime EndDate { get; set; }

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x060023C2 RID: 9154 RVA: 0x0008FCA7 File Offset: 0x0008DEA7
		// (set) Token: 0x060023C3 RID: 9155 RVA: 0x0008FCAF File Offset: 0x0008DEAF
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false)]
		public CultureInfo Locale { get; set; }

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x060023C4 RID: 9156 RVA: 0x0008FCB8 File Offset: 0x0008DEB8
		// (set) Token: 0x060023C5 RID: 9157 RVA: 0x0008FCC0 File Offset: 0x0008DEC0
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false)]
		public MultiValuedProperty<Guid> MalwareName { get; set; }

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x060023C6 RID: 9158 RVA: 0x0008FCC9 File Offset: 0x0008DEC9
		// (set) Token: 0x060023C7 RID: 9159 RVA: 0x0008FCD1 File Offset: 0x0008DED1
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false)]
		public MultiValuedProperty<string> MessageID { get; set; }

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x060023C8 RID: 9160 RVA: 0x0008FCDA File Offset: 0x0008DEDA
		// (set) Token: 0x060023C9 RID: 9161 RVA: 0x0008FCE2 File Offset: 0x0008DEE2
		[CmdletValidator("ValidateEmailAddressWithDomain", new object[]
		{
			CmdletValidator.EmailAddress.Recipient,
			CmdletValidator.WildcardValidationOptions.Disallow,
			CmdletValidator.EmailAcceptedDomainOptions.Verify
		}, ErrorMessage = Strings.IDs.InvalidNotifyAddress)]
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public MultiValuedProperty<string> NotifyAddress { get; set; }

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x060023CA RID: 9162 RVA: 0x0008FCEB File Offset: 0x0008DEEB
		// (set) Token: 0x060023CB RID: 9163 RVA: 0x0008FCF3 File Offset: 0x0008DEF3
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false)]
		public string OriginalClientIP { get; set; }

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x060023CC RID: 9164 RVA: 0x0008FCFC File Offset: 0x0008DEFC
		// (set) Token: 0x060023CD RID: 9165 RVA: 0x0008FD04 File Offset: 0x0008DF04
		[CmdletValidator("ValidateEmailAddress", new object[]
		{
			CmdletValidator.EmailAddress.Recipient,
			CmdletValidator.WildcardValidationOptions.Allow
		})]
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false)]
		public MultiValuedProperty<string> RecipientAddress { get; set; }

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x060023CE RID: 9166 RVA: 0x0008FD0D File Offset: 0x0008DF0D
		// (set) Token: 0x060023CF RID: 9167 RVA: 0x0008FD15 File Offset: 0x0008DF15
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false)]
		public ReportRecurrence Recurrence { get; set; }

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x060023D0 RID: 9168 RVA: 0x0008FD1E File Offset: 0x0008DF1E
		// (set) Token: 0x060023D1 RID: 9169 RVA: 0x0008FD26 File Offset: 0x0008DF26
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = false)]
		public string ReportTitle { get; set; }

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x060023D2 RID: 9170 RVA: 0x0008FD2F File Offset: 0x0008DF2F
		// (set) Token: 0x060023D3 RID: 9171 RVA: 0x0008FD37 File Offset: 0x0008DF37
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = false)]
		public ScheduleReportType ReportType { get; set; }

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x060023D4 RID: 9172 RVA: 0x0008FD40 File Offset: 0x0008DF40
		// (set) Token: 0x060023D5 RID: 9173 RVA: 0x0008FD48 File Offset: 0x0008DF48
		[CmdletValidator("ValidateEmailAddress", new object[]
		{
			CmdletValidator.EmailAddress.Sender,
			CmdletValidator.WildcardValidationOptions.Allow
		})]
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false)]
		public MultiValuedProperty<string> SenderAddress { get; set; }

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x060023D6 RID: 9174 RVA: 0x0008FD51 File Offset: 0x0008DF51
		// (set) Token: 0x060023D7 RID: 9175 RVA: 0x0008FD59 File Offset: 0x0008DF59
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false)]
		public ReportSeverity Severity { get; set; }

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x060023D8 RID: 9176 RVA: 0x0008FD62 File Offset: 0x0008DF62
		// (set) Token: 0x060023D9 RID: 9177 RVA: 0x0008FD6A File Offset: 0x0008DF6A
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
		public DateTime StartDate { get; set; }

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x060023DA RID: 9178 RVA: 0x0008FD73 File Offset: 0x0008DF73
		// (set) Token: 0x060023DB RID: 9179 RVA: 0x0008FD7B File Offset: 0x0008DF7B
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false)]
		public MultiValuedProperty<Guid> TransportRule { get; set; }

		// Token: 0x060023DC RID: 9180 RVA: 0x0008FD8C File Offset: 0x0008DF8C
		protected override void InternalValidate()
		{
			try
			{
				base.InternalValidate();
				Schema.Utilities.ValidateParameters(this, () => base.ConfigSession, new HashSet<CmdletValidator.ValidatorTypes>
				{
					CmdletValidator.ValidatorTypes.Preprocessing
				});
				Schema.Utilities.VerifyDateRange(this.StartDate, this.EndDate);
				DateTime dateTime = (DateTime)ExDateTime.UtcNow;
				int days = dateTime.Subtract(this.StartDate).Days;
				if (days > 90)
				{
					base.WriteError(new ArgumentException(Strings.InvalidStartDate(90)), ErrorCategory.InvalidArgument, null);
				}
				if (this.EndDate > dateTime)
				{
					this.EndDate = dateTime;
				}
				if (!string.IsNullOrEmpty(this.OriginalClientIP))
				{
					IPvxAddress none = IPvxAddress.None;
					if (!IPvxAddress.TryParse(this.OriginalClientIP, out none))
					{
						base.WriteError(new ArgumentException(Strings.InvalidIPAddress(this.OriginalClientIP)), ErrorCategory.InvalidArgument, null);
					}
				}
			}
			catch (InvalidExpressionException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, null);
			}
			catch (Exception exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x04001C7C RID: 7292
		private const int MaxReportDays = 90;

		// Token: 0x04001C7D RID: 7293
		private const string ComponentName = "NewReportSchedule";
	}
}
