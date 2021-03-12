using System;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000819 RID: 2073
	[Serializable]
	public class WebServicesTestOutcome
	{
		// Token: 0x170015B5 RID: 5557
		// (get) Token: 0x060047DE RID: 18398 RVA: 0x001278AB File Offset: 0x00125AAB
		// (set) Token: 0x060047DF RID: 18399 RVA: 0x001278B3 File Offset: 0x00125AB3
		public string Source { get; internal set; }

		// Token: 0x170015B6 RID: 5558
		// (get) Token: 0x060047E0 RID: 18400 RVA: 0x001278BC File Offset: 0x00125ABC
		// (set) Token: 0x060047E1 RID: 18401 RVA: 0x001278C4 File Offset: 0x00125AC4
		public string ServiceEndpoint { get; internal set; }

		// Token: 0x170015B7 RID: 5559
		// (get) Token: 0x060047E2 RID: 18402 RVA: 0x001278CD File Offset: 0x00125ACD
		// (set) Token: 0x060047E3 RID: 18403 RVA: 0x001278D5 File Offset: 0x00125AD5
		public WebServicesTestOutcome.TestScenario Scenario { get; internal set; }

		// Token: 0x170015B8 RID: 5560
		// (get) Token: 0x060047E4 RID: 18404 RVA: 0x001278DE File Offset: 0x00125ADE
		// (set) Token: 0x060047E5 RID: 18405 RVA: 0x001278E6 File Offset: 0x00125AE6
		public string ScenarioDescription { get; internal set; }

		// Token: 0x170015B9 RID: 5561
		// (get) Token: 0x060047E6 RID: 18406 RVA: 0x001278EF File Offset: 0x00125AEF
		// (set) Token: 0x060047E7 RID: 18407 RVA: 0x001278F7 File Offset: 0x00125AF7
		public CasTransactionResultEnum Result { get; internal set; }

		// Token: 0x170015BA RID: 5562
		// (get) Token: 0x060047E8 RID: 18408 RVA: 0x00127900 File Offset: 0x00125B00
		// (set) Token: 0x060047E9 RID: 18409 RVA: 0x00127908 File Offset: 0x00125B08
		public long Latency { get; internal set; }

		// Token: 0x170015BB RID: 5563
		// (get) Token: 0x060047EA RID: 18410 RVA: 0x00127911 File Offset: 0x00125B11
		// (set) Token: 0x060047EB RID: 18411 RVA: 0x00127919 File Offset: 0x00125B19
		public string Error { get; internal set; }

		// Token: 0x170015BC RID: 5564
		// (get) Token: 0x060047EC RID: 18412 RVA: 0x00127922 File Offset: 0x00125B22
		// (set) Token: 0x060047ED RID: 18413 RVA: 0x0012792A File Offset: 0x00125B2A
		public string Verbose { get; internal set; }

		// Token: 0x170015BD RID: 5565
		// (get) Token: 0x060047EE RID: 18414 RVA: 0x00127933 File Offset: 0x00125B33
		public int MonitoringEventId
		{
			get
			{
				if (this.Result == CasTransactionResultEnum.Failure)
				{
					return (int)(this.Scenario + 1000);
				}
				return (int)this.Scenario;
			}
		}

		// Token: 0x060047EF RID: 18415 RVA: 0x00127954 File Offset: 0x00125B54
		public override string ToString()
		{
			return Strings.WebServicesTestOutcomeToString(this.Source, this.ServiceEndpoint, this.Scenario.ToString(), this.Result.ToString(), this.Latency.ToString(), this.Error, this.Verbose);
		}

		// Token: 0x04002B7E RID: 11134
		public const int FailedEventIdBase = 1000;

		// Token: 0x0200081A RID: 2074
		public enum TestScenario
		{
			// Token: 0x04002B88 RID: 11144
			[LocDescription(Strings.IDs.ScenarioAutoDiscoverOutlookProvider)]
			AutoDiscoverOutlookProvider = 5001,
			// Token: 0x04002B89 RID: 11145
			[LocDescription(Strings.IDs.ScenarioExchangeWebServices)]
			ExchangeWebServices,
			// Token: 0x04002B8A RID: 11146
			[LocDescription(Strings.IDs.ScenarioAvailabilityService)]
			AvailabilityService,
			// Token: 0x04002B8B RID: 11147
			[LocDescription(Strings.IDs.ScenarioOfflineAddressBook)]
			OfflineAddressBook,
			// Token: 0x04002B8C RID: 11148
			[LocDescription(Strings.IDs.ScenarioAutoDiscoverSoapProvider)]
			AutoDiscoverSoapProvider = 5051,
			// Token: 0x04002B8D RID: 11149
			[LocDescription(Strings.IDs.ScenarioEwsConvertId)]
			EwsConvertId,
			// Token: 0x04002B8E RID: 11150
			[LocDescription(Strings.IDs.ScenarioEwsGetFolder)]
			EwsGetFolder
		}
	}
}
