using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Search.Core.Diagnostics
{
	// Token: 0x020000A2 RID: 162
	internal class TransportFlowOperatorTimings
	{
		// Token: 0x060004C4 RID: 1220 RVA: 0x0000F065 File Offset: 0x0000D265
		public TransportFlowOperatorTimings(string timingString)
		{
			this.ProcessOperatorTimings(timingString);
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0000F074 File Offset: 0x0000D274
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x0000F07C File Offset: 0x0000D27C
		public long TimeInQueueInMsec { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0000F085 File Offset: 0x0000D285
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x0000F08D File Offset: 0x0000D28D
		public long TimeInTransportRetrieverInMsec { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000F096 File Offset: 0x0000D296
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x0000F09E File Offset: 0x0000D29E
		public long TimeInDocParserInMsec { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000F0A7 File Offset: 0x0000D2A7
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x0000F0AF File Offset: 0x0000D2AF
		public long TimeInWordbreakerInMsec { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0000F0B8 File Offset: 0x0000D2B8
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x0000F0C0 File Offset: 0x0000D2C0
		public long TimeInNLGSubflowInMsec { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0000F0C9 File Offset: 0x0000D2C9
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x0000F0D1 File Offset: 0x0000D2D1
		public long TimeSpentWaitingForConnectInMsec { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0000F0DA File Offset: 0x0000D2DA
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x0000F0E2 File Offset: 0x0000D2E2
		public long TimeSpentMessageSendInMsec { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0000F0EB File Offset: 0x0000D2EB
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x0000F0F3 File Offset: 0x0000D2F3
		public string OperatorTimingString { get; private set; }

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000F0FC File Offset: 0x0000D2FC
		private void ProcessOperatorTimings(string timingString)
		{
			this.OperatorTimingString = timingString;
			if (string.IsNullOrWhiteSpace(this.OperatorTimingString))
			{
				return;
			}
			foreach (OperatorTimingEntry operatorTimingEntry in OperatorTimingEntry.DeserializeList(timingString))
			{
				string name;
				if ((name = operatorTimingEntry.Name) != null)
				{
					if (!(name == "RetrieverOperator"))
					{
						if (!(name == "PostDocParserOperator"))
						{
							if (!(name == "PostWordBreakerDiagnosticOperator"))
							{
								if (name == "TransportWriterProducer")
								{
									if (operatorTimingEntry.Location == OperatorLocation.BeginWrite)
									{
										this.TimeInNLGSubflowInMsec = operatorTimingEntry.Elapsed;
									}
								}
							}
							else if (operatorTimingEntry.Location == OperatorLocation.BeginProcessRecord)
							{
								this.TimeInWordbreakerInMsec = operatorTimingEntry.Elapsed;
							}
						}
						else if (operatorTimingEntry.Location == OperatorLocation.BeginProcessRecord)
						{
							this.TimeInDocParserInMsec = operatorTimingEntry.Elapsed;
						}
					}
					else if (operatorTimingEntry.Location == OperatorLocation.BeginProcessRecord)
					{
						this.TimeInQueueInMsec = operatorTimingEntry.Elapsed;
					}
					else if (operatorTimingEntry.Location == OperatorLocation.EndProcessRecord)
					{
						this.TimeInTransportRetrieverInMsec = operatorTimingEntry.Elapsed;
					}
				}
			}
		}

		// Token: 0x04000234 RID: 564
		private const string TransportRetrieverOperatorName = "RetrieverOperator";

		// Token: 0x04000235 RID: 565
		private const string PostDocParserProducerName = "PostDocParserOperator";

		// Token: 0x04000236 RID: 566
		private const string TransportWriterProducerName = "TransportWriterProducer";

		// Token: 0x04000237 RID: 567
		private const string PostWordBreakerDiagnosticOperatorName = "PostWordBreakerDiagnosticOperator";

		// Token: 0x04000238 RID: 568
		internal static readonly List<string> TimingEntryNames = new List<string>
		{
			"RetrieverOperator",
			"PostDocParserOperator",
			"PostWordBreakerDiagnosticOperator",
			"TransportWriterProducer"
		};
	}
}
