using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Management.FfoReporting.Common
{
	// Token: 0x02000394 RID: 916
	public class TrafficTask<TOutputObject> : FfoReportingDalTask<TOutputObject> where TOutputObject : new()
	{
		// Token: 0x06002004 RID: 8196 RVA: 0x000885D4 File Offset: 0x000867D4
		public TrafficTask(string dalTypeName) : base(dalTypeName)
		{
			this.Domain = new MultiValuedProperty<Fqdn>();
			this.Direction = new MultiValuedProperty<string>();
			this.Direction.Add(Schema.DirectionValues.Inbound.ToString());
			this.Direction.Add(Schema.DirectionValues.Outbound.ToString());
			this.AggregateBy = Schema.AggregateByValues.Day.ToString();
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06002005 RID: 8197 RVA: 0x0008863B File Offset: 0x0008683B
		// (set) Token: 0x06002006 RID: 8198 RVA: 0x00088643 File Offset: 0x00086843
		[QueryParameter("DomainListQueryDefinition", new string[]
		{

		})]
		[CmdletValidator("ValidateDomain", new object[]
		{

		}, ErrorMessage = Strings.IDs.InvalidDomain, ValidatorType = CmdletValidator.ValidatorTypes.PostprocessingWithConfigSession)]
		[Parameter(Mandatory = false, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public MultiValuedProperty<Fqdn> Domain { get; set; }

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06002007 RID: 8199 RVA: 0x0008864C File Offset: 0x0008684C
		// (set) Token: 0x06002008 RID: 8200 RVA: 0x00088654 File Offset: 0x00086854
		[Parameter(Mandatory = false)]
		[QueryParameter("StartDateKeyQueryDefinition", new string[]
		{
			"StartHourKeyQueryDefinition"
		}, MethodName = "AddDateFilter")]
		public DateTime? StartDate { get; set; }

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06002009 RID: 8201 RVA: 0x0008865D File Offset: 0x0008685D
		// (set) Token: 0x0600200A RID: 8202 RVA: 0x00088665 File Offset: 0x00086865
		[Parameter(Mandatory = false)]
		[QueryParameter("EndDateKeyQueryDefinition", new string[]
		{
			"EndHourKeyQueryDefinition"
		}, MethodName = "AddDateFilter")]
		public DateTime? EndDate { get; set; }

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x0600200B RID: 8203 RVA: 0x0008866E File Offset: 0x0008686E
		// (set) Token: 0x0600200C RID: 8204 RVA: 0x00088676 File Offset: 0x00086876
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(Schema.DirectionValues)
		}, ErrorMessage = Strings.IDs.InvalidDirection)]
		[Parameter(Mandatory = false)]
		[QueryParameter("DirectionListQueryDefinition", new string[]
		{

		})]
		public MultiValuedProperty<string> Direction { get; set; }

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x0600200D RID: 8205 RVA: 0x0008867F File Offset: 0x0008687F
		// (set) Token: 0x0600200E RID: 8206 RVA: 0x00088687 File Offset: 0x00086887
		[QueryParameter("AggregateByQueryDefinition", new string[]
		{

		})]
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(Schema.AggregateByValues)
		}, ErrorMessage = Strings.IDs.InvalidAggregateBy)]
		[Parameter(Mandatory = false)]
		public string AggregateBy { get; set; }

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x0600200F RID: 8207 RVA: 0x00088690 File Offset: 0x00086890
		public override string ComponentName
		{
			get
			{
				return ExchangeComponent.FfoRws.Name;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06002010 RID: 8208 RVA: 0x0008869C File Offset: 0x0008689C
		public override string MonitorEventName
		{
			get
			{
				return "FFO Reporting Task Status Monitor";
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06002011 RID: 8209 RVA: 0x000886A3 File Offset: 0x000868A3
		public override string DalMonitorEventName
		{
			get
			{
				return "FFO DAL Retrieval Status Monitor";
			}
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x000886AA File Offset: 0x000868AA
		protected override void CustomInternalValidate()
		{
			base.CustomInternalValidate();
			Schema.Utilities.CheckDates(this.StartDate, this.EndDate, new Schema.Utilities.NotifyNeedDefaultDatesDelegate(this.SetDefaultDates), new Schema.Utilities.ValidateDatesDelegate(Schema.Utilities.VerifyDateRange));
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x000886DC File Offset: 0x000868DC
		private void SetDefaultDates()
		{
			DateTime value = (DateTime)ExDateTime.UtcNow;
			this.EndDate = new DateTime?(value);
			this.StartDate = new DateTime?(value.AddDays(-14.0));
		}

		// Token: 0x040019AE RID: 6574
		private const int DefaultDateOffset = -14;
	}
}
