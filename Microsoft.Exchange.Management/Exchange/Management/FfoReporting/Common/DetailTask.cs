using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Management.FfoReporting.Common
{
	// Token: 0x02000392 RID: 914
	public abstract class DetailTask<TOutputObject> : FfoReportingDalTask<TOutputObject> where TOutputObject : new()
	{
		// Token: 0x06001FE0 RID: 8160 RVA: 0x00088364 File Offset: 0x00086564
		public DetailTask(string dalTypeName) : base(dalTypeName)
		{
			this.MessageTraceId = new MultiValuedProperty<Guid>();
			this.Domain = new MultiValuedProperty<Fqdn>();
			this.Direction = new MultiValuedProperty<string>();
			this.Direction.Add(Schema.DirectionValues.Inbound.ToString());
			this.Direction.Add(Schema.DirectionValues.Outbound.ToString());
			this.MessageId = new MultiValuedProperty<string>();
			this.SenderAddress = new MultiValuedProperty<string>();
			this.RecipientAddress = new MultiValuedProperty<string>();
			this.Action = new MultiValuedProperty<string>();
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x000883F1 File Offset: 0x000865F1
		// (set) Token: 0x06001FE2 RID: 8162 RVA: 0x000883F9 File Offset: 0x000865F9
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<Guid> MessageTraceId { get; set; }

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x00088402 File Offset: 0x00086602
		// (set) Token: 0x06001FE4 RID: 8164 RVA: 0x0008840A File Offset: 0x0008660A
		[Parameter(Mandatory = false, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[CmdletValidator("ValidateDomain", new object[]
		{

		}, ErrorMessage = Strings.IDs.InvalidDomain, ValidatorType = CmdletValidator.ValidatorTypes.PostprocessingWithConfigSession)]
		[QueryParameter("DomainListQueryDefinition", new string[]
		{

		})]
		public MultiValuedProperty<Fqdn> Domain { get; set; }

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06001FE5 RID: 8165 RVA: 0x00088413 File Offset: 0x00086613
		// (set) Token: 0x06001FE6 RID: 8166 RVA: 0x0008841B File Offset: 0x0008661B
		[Parameter(Mandatory = false)]
		[QueryParameter("StartDateKeyQueryDefinition", new string[]
		{
			"StartHourKeyQueryDefinition"
		}, MethodName = "AddDateFilter")]
		public DateTime? StartDate { get; set; }

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06001FE7 RID: 8167 RVA: 0x00088424 File Offset: 0x00086624
		// (set) Token: 0x06001FE8 RID: 8168 RVA: 0x0008842C File Offset: 0x0008662C
		[QueryParameter("EndDateKeyQueryDefinition", new string[]
		{
			"EndHourKeyQueryDefinition"
		}, MethodName = "AddDateFilter")]
		[Parameter(Mandatory = false)]
		public DateTime? EndDate { get; set; }

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x00088435 File Offset: 0x00086635
		// (set) Token: 0x06001FEA RID: 8170 RVA: 0x0008843D File Offset: 0x0008663D
		[QueryParameter("DirectionListQueryDefinition", new string[]
		{

		})]
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(Schema.DirectionValues)
		}, ErrorMessage = Strings.IDs.InvalidDirection)]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> Direction { get; set; }

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06001FEB RID: 8171 RVA: 0x00088446 File Offset: 0x00086646
		// (set) Token: 0x06001FEC RID: 8172 RVA: 0x0008844E File Offset: 0x0008664E
		[QueryParameter("MessageIdListQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> MessageId { get; set; }

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06001FED RID: 8173 RVA: 0x00088457 File Offset: 0x00086657
		// (set) Token: 0x06001FEE RID: 8174 RVA: 0x0008845F File Offset: 0x0008665F
		[QueryParameter("SenderAddressListQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> SenderAddress { get; set; }

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06001FEF RID: 8175 RVA: 0x00088468 File Offset: 0x00086668
		// (set) Token: 0x06001FF0 RID: 8176 RVA: 0x00088470 File Offset: 0x00086670
		[QueryParameter("RecipientAddressListQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> RecipientAddress { get; set; }

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06001FF1 RID: 8177 RVA: 0x00088479 File Offset: 0x00086679
		// (set) Token: 0x06001FF2 RID: 8178 RVA: 0x00088481 File Offset: 0x00086681
		[Parameter(Mandatory = false)]
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(Schema.Actions)
		}, ErrorMessage = Strings.IDs.InvalidActionParameter)]
		[QueryParameter("ActionListQueryDefinition", new string[]
		{

		})]
		public MultiValuedProperty<string> Action { get; set; }

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06001FF3 RID: 8179 RVA: 0x0008848A File Offset: 0x0008668A
		public override string ComponentName
		{
			get
			{
				return ExchangeComponent.FfoRws.Name;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06001FF4 RID: 8180 RVA: 0x00088496 File Offset: 0x00086696
		public override string MonitorEventName
		{
			get
			{
				return "FFO Reporting Task Status Monitor";
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06001FF5 RID: 8181 RVA: 0x0008849D File Offset: 0x0008669D
		public override string DalMonitorEventName
		{
			get
			{
				return "FFO DAL Retrieval Status Monitor";
			}
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x000884A4 File Offset: 0x000866A4
		protected override void CustomInternalValidate()
		{
			base.CustomInternalValidate();
			Schema.Utilities.CheckDates(this.StartDate, this.EndDate, new Schema.Utilities.NotifyNeedDefaultDatesDelegate(this.SetDefaultDates), new Schema.Utilities.ValidateDatesDelegate(Schema.Utilities.VerifyDateRange));
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x000884D8 File Offset: 0x000866D8
		private void SetDefaultDates()
		{
			DateTime value = (DateTime)ExDateTime.UtcNow;
			this.EndDate = new DateTime?(value);
			this.StartDate = new DateTime?(value.AddDays(-14.0));
		}

		// Token: 0x0400199C RID: 6556
		private const int DefaultDateOffset = -14;
	}
}
