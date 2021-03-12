using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Config;
using Microsoft.Exchange.MailboxLoadBalance.Data;

namespace Microsoft.Exchange.MailboxLoadBalance.Logging
{
	// Token: 0x0200009D RID: 157
	internal class BandRebalanceLog : ObjectLog<BandRebalanceLogEntry>
	{
		// Token: 0x0600059D RID: 1437 RVA: 0x0000EF49 File Offset: 0x0000D149
		private BandRebalanceLog() : base(new BandRebalanceLog.BandRebalanceLogSchema(), new LoadBalanceLoggingConfig("BandBalance"))
		{
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0000EF60 File Offset: 0x0000D160
		public static void Write(BandMailboxRebalanceData request)
		{
			foreach (LoadMetric loadMetric in request.RebalanceInformation.Metrics)
			{
				BandRebalanceLogEntry bandRebalanceLogEntry = new BandRebalanceLogEntry();
				bandRebalanceLogEntry.SourceDatabase = request.SourceDatabase.Name;
				bandRebalanceLogEntry.TargetDatabase = request.TargetDatabase.Name;
				bandRebalanceLogEntry.BatchName = request.RebalanceBatchName;
				bandRebalanceLogEntry.Metric = loadMetric.ToString();
				bandRebalanceLogEntry.RebalanceUnits = request.RebalanceInformation[loadMetric];
				BandRebalanceLog.Instance.LogObject(bandRebalanceLogEntry);
			}
		}

		// Token: 0x040001CA RID: 458
		private static readonly BandRebalanceLog Instance = new BandRebalanceLog();

		// Token: 0x0200009E RID: 158
		private class BandRebalanceLogData : ConfigurableObject
		{
			// Token: 0x060005A0 RID: 1440 RVA: 0x0000F014 File Offset: 0x0000D214
			public BandRebalanceLogData(PropertyBag propertyBag) : base(propertyBag)
			{
			}

			// Token: 0x170001E1 RID: 481
			// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0000F01D File Offset: 0x0000D21D
			internal override ObjectSchema ObjectSchema
			{
				get
				{
					return new DummyObjectSchema();
				}
			}
		}

		// Token: 0x0200009F RID: 159
		private class BandRebalanceLogSchema : ConfigurableObjectLogSchema<BandRebalanceLog.BandRebalanceLogData, DummyObjectSchema>
		{
			// Token: 0x170001E2 RID: 482
			// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0000F024 File Offset: 0x0000D224
			public override string LogType
			{
				get
				{
					return "Band Rebalance Requests";
				}
			}

			// Token: 0x170001E3 RID: 483
			// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0000F02B File Offset: 0x0000D22B
			public override string Software
			{
				get
				{
					return "Mailbox Load Balancing";
				}
			}

			// Token: 0x040001CB RID: 459
			public static readonly ObjectLogSimplePropertyDefinition<BandRebalanceLogEntry> SourceDatabase = new ObjectLogSimplePropertyDefinition<BandRebalanceLogEntry>("SourceDatabase", (BandRebalanceLogEntry r) => r.SourceDatabase);

			// Token: 0x040001CC RID: 460
			public static readonly ObjectLogSimplePropertyDefinition<BandRebalanceLogEntry> TargetDatabase = new ObjectLogSimplePropertyDefinition<BandRebalanceLogEntry>("TargetDatabase", (BandRebalanceLogEntry r) => r.TargetDatabase);

			// Token: 0x040001CD RID: 461
			public static readonly ObjectLogSimplePropertyDefinition<BandRebalanceLogEntry> Metric = new ObjectLogSimplePropertyDefinition<BandRebalanceLogEntry>("Metric", (BandRebalanceLogEntry r) => r.Metric);

			// Token: 0x040001CE RID: 462
			public static readonly ObjectLogSimplePropertyDefinition<BandRebalanceLogEntry> Units = new ObjectLogSimplePropertyDefinition<BandRebalanceLogEntry>("Units", (BandRebalanceLogEntry r) => r.RebalanceUnits);

			// Token: 0x040001CF RID: 463
			public static readonly ObjectLogSimplePropertyDefinition<BandRebalanceLogEntry> BatchName = new ObjectLogSimplePropertyDefinition<BandRebalanceLogEntry>("BatchName", (BandRebalanceLogEntry r) => r.BatchName);
		}
	}
}
