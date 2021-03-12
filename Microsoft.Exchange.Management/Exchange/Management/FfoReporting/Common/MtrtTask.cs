using System;
using System.Management.Automation;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.ReportingTask;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Management.FfoReporting.Common
{
	// Token: 0x020003A5 RID: 933
	public class MtrtTask<TOutputObject> : FfoReportingDalTask<TOutputObject> where TOutputObject : new()
	{
		// Token: 0x060020C4 RID: 8388 RVA: 0x0008AF32 File Offset: 0x00089132
		public MtrtTask(string dalTypeName) : base(dalTypeName)
		{
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x060020C5 RID: 8389 RVA: 0x0008AF3B File Offset: 0x0008913B
		// (set) Token: 0x060020C6 RID: 8390 RVA: 0x0008AF43 File Offset: 0x00089143
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		[QueryParameter("StartDateQueryDefinition", new string[]
		{

		})]
		public DateTime? StartDate { get; set; }

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x060020C7 RID: 8391 RVA: 0x0008AF4C File Offset: 0x0008914C
		// (set) Token: 0x060020C8 RID: 8392 RVA: 0x0008AF54 File Offset: 0x00089154
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		[QueryParameter("EndDateQueryDefinition", new string[]
		{

		})]
		public DateTime? EndDate { get; set; }

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x060020C9 RID: 8393 RVA: 0x0008AF5D File Offset: 0x0008915D
		public override string ComponentName
		{
			get
			{
				return ExchangeComponent.FfoRws.Name;
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x060020CA RID: 8394 RVA: 0x0008AF69 File Offset: 0x00089169
		public override string MonitorEventName
		{
			get
			{
				return "FFO Reporting Task Status Monitor";
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x060020CB RID: 8395 RVA: 0x0008AF70 File Offset: 0x00089170
		public override string DalMonitorEventName
		{
			get
			{
				return "FFO DAL Retrieval Status Monitor";
			}
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x0008AF77 File Offset: 0x00089177
		protected override void CustomInternalValidate()
		{
			base.CustomInternalValidate();
			Schema.Utilities.CheckDates(this.StartDate, this.EndDate, new Schema.Utilities.NotifyNeedDefaultDatesDelegate(this.SetDefaultDates), new Schema.Utilities.ValidateDatesDelegate(this.ValidateDateRange));
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x0008AFA8 File Offset: 0x000891A8
		private void SetDefaultDates()
		{
			DateTime value = (DateTime)ExDateTime.UtcNow;
			this.EndDate = new DateTime?(value);
			this.StartDate = new DateTime?(value.AddHours(-48.0));
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x0008AFE8 File Offset: 0x000891E8
		private void ValidateDateRange(DateTime startTime, DateTime endTime)
		{
			Schema.Utilities.VerifyDateRange(startTime, endTime);
			int days = ((DateTime)ExDateTime.UtcNow).Subtract(startTime).Days;
			if (days > 30)
			{
				throw new InvalidExpressionException(Strings.InvalidStartDateOffset);
			}
		}

		// Token: 0x04001A27 RID: 6695
		private const int DefaultHourOffset = -48;

		// Token: 0x04001A28 RID: 6696
		private const int MaxDateOffset = 30;
	}
}
