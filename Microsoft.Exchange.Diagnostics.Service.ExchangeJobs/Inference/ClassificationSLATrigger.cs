using System;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Inference
{
	// Token: 0x02000027 RID: 39
	public class ClassificationSLATrigger : PerfLogCounterTrigger
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00006A9C File Offset: 0x00004C9C
		public ClassificationSLATrigger(IJob job) : base(job, Regex.Escape(string.Format("{0}({1})\\{2}", "MSExchangeInference Pipeline", "classificationpipeline", "Classification SLA")), new PerfLogCounterTrigger.TriggerConfiguration("ClassificationSLATrigger", 98.5, double.MinValue, TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(15.0), TimeSpan.FromMinutes(30.0), 1))
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("The SLA is calculated from the 'MSExchange Delivery Store Driver\\SucessfulDeliveries', 'MSExchangeInference Pipeline\\Number Of Failed Documents\\classificationpipeline', 'MSExchangeInference Classification Processing\\Items Skipped' and 'MSExchange Delivery Store Driver Agents\\StoreDriverDelivery Agent Failure\\inference classification agent' counters over the past hour.");
			stringBuilder.AppendLine("Please visit https://eds.outlook.com/ for more historical data.");
			this.additionalInfo = stringBuilder.ToString();
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00006B3F File Offset: 0x00004D3F
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00006B46 File Offset: 0x00004D46
		internal static string LastFailureCounterData { get; set; }

		// Token: 0x060000BF RID: 191 RVA: 0x00006B4E File Offset: 0x00004D4E
		protected override void OnThresholdEvent(PerfLogLine line, PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00006B50 File Offset: 0x00004D50
		protected override string CollectAdditionalInformation(PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			return (ClassificationSLATrigger.LastFailureCounterData ?? string.Empty) + this.additionalInfo;
		}

		// Token: 0x04000117 RID: 279
		internal const double TriggerThreshold = 98.5;

		// Token: 0x04000118 RID: 280
		internal const double IgnorableExceptionTriggerThreshold = 97.0;

		// Token: 0x04000119 RID: 281
		private readonly string additionalInfo;
	}
}
