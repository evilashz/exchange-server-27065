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
	// Token: 0x020003B2 RID: 946
	[OutputType(new Type[]
	{
		typeof(ServiceDeliveryReport)
	})]
	[Cmdlet("Get", "ServiceDeliveryReport")]
	public sealed class GetServiceDeliveryReport : FfoReportingTask<ServiceDeliveryReport>
	{
		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x0600213E RID: 8510 RVA: 0x0008C3A8 File Offset: 0x0008A5A8
		// (set) Token: 0x0600213F RID: 8511 RVA: 0x0008C3B0 File Offset: 0x0008A5B0
		[CmdletValidator("ValidateRequiredField", new object[]
		{

		})]
		[Parameter(Mandatory = false, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public SmtpAddress Recipient { get; set; }

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06002140 RID: 8512 RVA: 0x0008C3B9 File Offset: 0x0008A5B9
		public override string ComponentName
		{
			get
			{
				return ExchangeComponent.FfoRws.Name;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06002141 RID: 8513 RVA: 0x0008C3C5 File Offset: 0x0008A5C5
		public override string MonitorEventName
		{
			get
			{
				return "FfoReporting.SmtpChecker";
			}
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x0008C3CC File Offset: 0x0008A5CC
		protected override IReadOnlyList<ServiceDeliveryReport> AggregateOutput()
		{
			return DataProcessorDriver.Process<ServiceDeliveryReport>(ServiceLocator.Current.GetService<ISmtpCheckerProvider>().GetServiceDeliveries(this.Recipient, base.ConfigSession), ConversionProcessor.Create<ServiceDeliveryReport>(this));
		}
	}
}
