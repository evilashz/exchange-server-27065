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
	// Token: 0x020003B1 RID: 945
	[OutputType(new Type[]
	{
		typeof(OutboundConnectorReport)
	})]
	[Cmdlet("Get", "OutboundConnectorReport")]
	public sealed class GetOutboundConnectorReport : FfoReportingTask<OutboundConnectorReport>
	{
		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06002138 RID: 8504 RVA: 0x0008C354 File Offset: 0x0008A554
		// (set) Token: 0x06002139 RID: 8505 RVA: 0x0008C35C File Offset: 0x0008A55C
		[Parameter(Mandatory = false, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[CmdletValidator("ValidateRequiredField", new object[]
		{

		})]
		public Fqdn Domain { get; set; }

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x0600213A RID: 8506 RVA: 0x0008C365 File Offset: 0x0008A565
		public override string ComponentName
		{
			get
			{
				return ExchangeComponent.FfoRws.Name;
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x0600213B RID: 8507 RVA: 0x0008C371 File Offset: 0x0008A571
		public override string MonitorEventName
		{
			get
			{
				return "FfoReporting.SmtpChecker";
			}
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x0008C378 File Offset: 0x0008A578
		protected override IReadOnlyList<OutboundConnectorReport> AggregateOutput()
		{
			return DataProcessorDriver.Process<OutboundConnectorReport>(ServiceLocator.Current.GetService<ISmtpCheckerProvider>().GetOutboundConnectors(this.Domain, base.ConfigSession), ConversionProcessor.Create<OutboundConnectorReport>(this));
		}
	}
}
