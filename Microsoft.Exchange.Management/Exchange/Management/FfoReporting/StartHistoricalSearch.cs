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
	// Token: 0x020003EF RID: 1007
	[Cmdlet("Start", "HistoricalSearch")]
	[OutputType(new Type[]
	{
		typeof(HistoricalSearch)
	})]
	public sealed class StartHistoricalSearch : HistoricalSearchBaseTask
	{
		// Token: 0x06002381 RID: 9089 RVA: 0x0008F5B6 File Offset: 0x0008D7B6
		public StartHistoricalSearch() : base("StartHistoricalSearch", "Microsoft.Exchange.Hygiene.ManagementHelper.HistoricalSearch.StartHistoricalSearchHelper")
		{
			this.Locale = Thread.CurrentThread.CurrentCulture;
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06002382 RID: 9090 RVA: 0x0008F5D8 File Offset: 0x0008D7D8
		// (set) Token: 0x06002383 RID: 9091 RVA: 0x0008F5E0 File Offset: 0x0008D7E0
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = false)]
		public HistoricalSearchReportType ReportType { get; set; }

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06002384 RID: 9092 RVA: 0x0008F5E9 File Offset: 0x0008D7E9
		// (set) Token: 0x06002385 RID: 9093 RVA: 0x0008F5F1 File Offset: 0x0008D7F1
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = false)]
		public string ReportTitle { get; set; }

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06002386 RID: 9094 RVA: 0x0008F5FA File Offset: 0x0008D7FA
		// (set) Token: 0x06002387 RID: 9095 RVA: 0x0008F602 File Offset: 0x0008D802
		[CmdletValidator("ValidateEmailAddressWithDomain", new object[]
		{
			CmdletValidator.EmailAddress.Recipient,
			CmdletValidator.WildcardValidationOptions.Disallow,
			CmdletValidator.EmailAcceptedDomainOptions.Verify
		}, ErrorMessage = Strings.IDs.InvalidNotifyAddress)]
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public MultiValuedProperty<string> NotifyAddress { get; set; }

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06002388 RID: 9096 RVA: 0x0008F60B File Offset: 0x0008D80B
		// (set) Token: 0x06002389 RID: 9097 RVA: 0x0008F613 File Offset: 0x0008D813
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
		public DateTime StartDate { get; set; }

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x0600238A RID: 9098 RVA: 0x0008F61C File Offset: 0x0008D81C
		// (set) Token: 0x0600238B RID: 9099 RVA: 0x0008F624 File Offset: 0x0008D824
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
		public DateTime EndDate { get; set; }

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x0600238C RID: 9100 RVA: 0x0008F62D File Offset: 0x0008D82D
		// (set) Token: 0x0600238D RID: 9101 RVA: 0x0008F635 File Offset: 0x0008D835
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(MessageDeliveryStatus)
		}, ErrorMessage = Strings.IDs.InvalidDeliveryStatus)]
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false)]
		public string DeliveryStatus { get; set; }

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x0600238E RID: 9102 RVA: 0x0008F63E File Offset: 0x0008D83E
		// (set) Token: 0x0600238F RID: 9103 RVA: 0x0008F646 File Offset: 0x0008D846
		[CmdletValidator("ValidateEmailAddress", new object[]
		{
			CmdletValidator.EmailAddress.Sender,
			CmdletValidator.WildcardValidationOptions.Allow
		})]
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false)]
		public MultiValuedProperty<string> SenderAddress { get; set; }

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06002390 RID: 9104 RVA: 0x0008F64F File Offset: 0x0008D84F
		// (set) Token: 0x06002391 RID: 9105 RVA: 0x0008F657 File Offset: 0x0008D857
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false)]
		[CmdletValidator("ValidateEmailAddress", new object[]
		{
			CmdletValidator.EmailAddress.Recipient,
			CmdletValidator.WildcardValidationOptions.Allow
		})]
		public MultiValuedProperty<string> RecipientAddress { get; set; }

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06002392 RID: 9106 RVA: 0x0008F660 File Offset: 0x0008D860
		// (set) Token: 0x06002393 RID: 9107 RVA: 0x0008F668 File Offset: 0x0008D868
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false)]
		public string OriginalClientIP { get; set; }

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06002394 RID: 9108 RVA: 0x0008F671 File Offset: 0x0008D871
		// (set) Token: 0x06002395 RID: 9109 RVA: 0x0008F679 File Offset: 0x0008D879
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false)]
		public MultiValuedProperty<string> MessageID { get; set; }

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06002396 RID: 9110 RVA: 0x0008F682 File Offset: 0x0008D882
		// (set) Token: 0x06002397 RID: 9111 RVA: 0x0008F68A File Offset: 0x0008D88A
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false)]
		public MultiValuedProperty<Guid> DLPPolicy { get; set; }

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06002398 RID: 9112 RVA: 0x0008F693 File Offset: 0x0008D893
		// (set) Token: 0x06002399 RID: 9113 RVA: 0x0008F69B File Offset: 0x0008D89B
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false)]
		public MultiValuedProperty<Guid> TransportRule { get; set; }

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x0600239A RID: 9114 RVA: 0x0008F6A4 File Offset: 0x0008D8A4
		// (set) Token: 0x0600239B RID: 9115 RVA: 0x0008F6AC File Offset: 0x0008D8AC
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false)]
		public CultureInfo Locale { get; set; }

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x0600239C RID: 9116 RVA: 0x0008F6B5 File Offset: 0x0008D8B5
		// (set) Token: 0x0600239D RID: 9117 RVA: 0x0008F6BD File Offset: 0x0008D8BD
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false)]
		public MessageDirection Direction { get; set; }

		// Token: 0x0600239E RID: 9118 RVA: 0x0008F6D0 File Offset: 0x0008D8D0
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
				if ((this.ReportType == HistoricalSearchReportType.MessageTrace || this.ReportType == HistoricalSearchReportType.MessageTraceDetail) && this.MessageID == null && this.RecipientAddress == null && this.SenderAddress == null)
				{
					base.WriteError(new ArgumentException(Strings.MessageTraceMinimumCriteriaFieldsInErrorDeliveryStatus), ErrorCategory.InvalidArgument, null);
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

		// Token: 0x04001C60 RID: 7264
		private const int MaxReportDays = 90;

		// Token: 0x04001C61 RID: 7265
		private const string ComponentName = "StartHistoricalSearch";
	}
}
