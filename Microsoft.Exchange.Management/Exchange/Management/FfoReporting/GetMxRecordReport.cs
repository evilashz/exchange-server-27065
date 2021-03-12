using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting.Common;
using Microsoft.Exchange.Management.FfoReporting.Data;
using Microsoft.Exchange.Management.FfoReporting.Providers;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003B0 RID: 944
	[Cmdlet("Get", "MxRecordReport")]
	[OutputType(new Type[]
	{
		typeof(MxRecordReport)
	})]
	public sealed class GetMxRecordReport : FfoReportingTask<MxRecordReport>
	{
		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06002132 RID: 8498 RVA: 0x0008C300 File Offset: 0x0008A500
		// (set) Token: 0x06002133 RID: 8499 RVA: 0x0008C308 File Offset: 0x0008A508
		[Parameter(Mandatory = false, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[CmdletValidator("ValidateRequiredField", new object[]
		{

		})]
		public Fqdn Domain { get; set; }

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06002134 RID: 8500 RVA: 0x0008C311 File Offset: 0x0008A511
		public override string ComponentName
		{
			get
			{
				return ExchangeComponent.FfoRws.Name;
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06002135 RID: 8501 RVA: 0x0008C31D File Offset: 0x0008A51D
		public override string MonitorEventName
		{
			get
			{
				return "FfoReporting.SmtpChecker";
			}
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x0008C324 File Offset: 0x0008A524
		protected override IReadOnlyList<MxRecordReport> AggregateOutput()
		{
			return DataProcessorDriver.Process<MxRecordReport>(ServiceLocator.Current.GetService<ISmtpCheckerProvider>().GetMxRecords(this.Domain, base.ConfigSession), ConversionProcessor.Create<MxRecordReport>(this));
		}
	}
}
