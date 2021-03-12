using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Directory.TopologyService.EventLog
{
	// Token: 0x02000031 RID: 49
	public static class MSExchangeADTopologyEventLogConstants
	{
		// Token: 0x04000119 RID: 281
		public const string EventSource = "MSExchangeADTopology";

		// Token: 0x0400011A RID: 282
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurationDataLoadingException = new ExEventLog.EventTuple(3221488706U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400011B RID: 283
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WCFFaultedState = new ExEventLog.EventTuple(3221488707U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400011C RID: 284
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ServerNotInSiteFatal = new ExEventLog.EventTuple(3221488708U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400011D RID: 285
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ServerNotInSite = new ExEventLog.EventTuple(3221488709U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400011E RID: 286
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_NO_LOCAL_GC_FOUND = new ExEventLog.EventTuple(1074006028U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400011F RID: 287
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_NO_GC_FOUND = new ExEventLog.EventTuple(1074006037U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000120 RID: 288
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_NO_DC_FOUND = new ExEventLog.EventTuple(1074006045U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000121 RID: 289
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_SUITABLE_SERVERS = new ExEventLog.EventTuple(1074006049U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000122 RID: 290
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_GOING_OUT_OF_SITE_DC = new ExEventLog.EventTuple(1074006052U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000123 RID: 291
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_GOING_OUT_OF_SITE_GC = new ExEventLog.EventTuple(1074006053U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000124 RID: 292
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_NO_LOCAL_DC_FOUND = new ExEventLog.EventTuple(1074006054U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000125 RID: 293
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_CDC_CHANGED = new ExEventLog.EventTuple(1074006063U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000126 RID: 294
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_ALL_DOMAIN_DS_DOWN = new ExEventLog.EventTuple(3221489720U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000127 RID: 295
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_SET_CDC_BAD = new ExEventLog.EventTuple(2147747892U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000128 RID: 296
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_SET_CDC_DOWN = new ExEventLog.EventTuple(2147747893U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000129 RID: 297
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_ALL_DC_DOWN = new ExEventLog.EventTuple(3221489718U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400012A RID: 298
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_DISCOVERY_FAILED2 = new ExEventLog.EventTuple(3221489758U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400012B RID: 299
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStoppedWithException = new ExEventLog.EventTuple(3221490420U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400012C RID: 300
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceFailedToStart = new ExEventLog.EventTuple(3221490421U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400012D RID: 301
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStarting = new ExEventLog.EventTuple(1074006778U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400012E RID: 302
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkItemUnhandledException = new ExEventLog.EventTuple(2147748603U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400012F RID: 303
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkerFatalException = new ExEventLog.EventTuple(3221490428U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000130 RID: 304
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkItemFatalException = new ExEventLog.EventTuple(3221490429U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000131 RID: 305
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CurruptedServiceConfiguration = new ExEventLog.EventTuple(3221490430U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000132 RID: 306
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_SECURE_CHANNEL_DC = new ExEventLog.EventTuple(1074006988U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000133 RID: 307
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_DCMMON_PRIMARY_SERVERS_STATE = new ExEventLog.EventTuple(1074007993U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000134 RID: 308
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_DISCOVERED_SERVERS_DATACENTER = new ExEventLog.EventTuple(1074007994U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000135 RID: 309
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_DCMMMON_RESET_SC_CHANNEL = new ExEventLog.EventTuple(1074007997U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000136 RID: 310
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SecureChannelDCIsUnknown = new ExEventLog.EventTuple(2147749822U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000137 RID: 311
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_DCMMMON_RESET_SC_CHANNEL_TO_MM_DC = new ExEventLog.EventTuple(2147749823U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000138 RID: 312
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_DCMMMON_RESET_SC_CHANNEL_TO_MM_DC_FINALLY = new ExEventLog.EventTuple(3221491648U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000139 RID: 313
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_DCMMMON_RESET_DC_LOCATOR_TO_MM_DC = new ExEventLog.EventTuple(2147749825U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400013A RID: 314
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DSC_EVENT_DCMMMON_RESET_DC_LOCATOR_TO_MM_DC_FINALLY = new ExEventLog.EventTuple(3221491650U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000032 RID: 50
		private enum Category : short
		{
			// Token: 0x0400013C RID: 316
			General = 1,
			// Token: 0x0400013D RID: 317
			Configuration,
			// Token: 0x0400013E RID: 318
			Topology
		}

		// Token: 0x02000033 RID: 51
		internal enum Message : uint
		{
			// Token: 0x04000140 RID: 320
			ConfigurationDataLoadingException = 3221488706U,
			// Token: 0x04000141 RID: 321
			WCFFaultedState,
			// Token: 0x04000142 RID: 322
			ServerNotInSiteFatal,
			// Token: 0x04000143 RID: 323
			ServerNotInSite,
			// Token: 0x04000144 RID: 324
			DSC_EVENT_NO_LOCAL_GC_FOUND = 1074006028U,
			// Token: 0x04000145 RID: 325
			DSC_EVENT_NO_GC_FOUND = 1074006037U,
			// Token: 0x04000146 RID: 326
			DSC_EVENT_NO_DC_FOUND = 1074006045U,
			// Token: 0x04000147 RID: 327
			DSC_EVENT_SUITABLE_SERVERS = 1074006049U,
			// Token: 0x04000148 RID: 328
			DSC_EVENT_GOING_OUT_OF_SITE_DC = 1074006052U,
			// Token: 0x04000149 RID: 329
			DSC_EVENT_GOING_OUT_OF_SITE_GC,
			// Token: 0x0400014A RID: 330
			DSC_EVENT_NO_LOCAL_DC_FOUND,
			// Token: 0x0400014B RID: 331
			DSC_EVENT_CDC_CHANGED = 1074006063U,
			// Token: 0x0400014C RID: 332
			DSC_EVENT_ALL_DOMAIN_DS_DOWN = 3221489720U,
			// Token: 0x0400014D RID: 333
			DSC_EVENT_SET_CDC_BAD = 2147747892U,
			// Token: 0x0400014E RID: 334
			DSC_EVENT_SET_CDC_DOWN,
			// Token: 0x0400014F RID: 335
			DSC_EVENT_ALL_DC_DOWN = 3221489718U,
			// Token: 0x04000150 RID: 336
			DSC_EVENT_DISCOVERY_FAILED2 = 3221489758U,
			// Token: 0x04000151 RID: 337
			ServiceStoppedWithException = 3221490420U,
			// Token: 0x04000152 RID: 338
			ServiceFailedToStart,
			// Token: 0x04000153 RID: 339
			ServiceStarting = 1074006778U,
			// Token: 0x04000154 RID: 340
			WorkItemUnhandledException = 2147748603U,
			// Token: 0x04000155 RID: 341
			WorkerFatalException = 3221490428U,
			// Token: 0x04000156 RID: 342
			WorkItemFatalException,
			// Token: 0x04000157 RID: 343
			CurruptedServiceConfiguration,
			// Token: 0x04000158 RID: 344
			DSC_EVENT_SECURE_CHANNEL_DC = 1074006988U,
			// Token: 0x04000159 RID: 345
			DSC_EVENT_DCMMON_PRIMARY_SERVERS_STATE = 1074007993U,
			// Token: 0x0400015A RID: 346
			DSC_EVENT_DISCOVERED_SERVERS_DATACENTER,
			// Token: 0x0400015B RID: 347
			DSC_EVENT_DCMMMON_RESET_SC_CHANNEL = 1074007997U,
			// Token: 0x0400015C RID: 348
			SecureChannelDCIsUnknown = 2147749822U,
			// Token: 0x0400015D RID: 349
			DSC_EVENT_DCMMMON_RESET_SC_CHANNEL_TO_MM_DC,
			// Token: 0x0400015E RID: 350
			DSC_EVENT_DCMMMON_RESET_SC_CHANNEL_TO_MM_DC_FINALLY = 3221491648U,
			// Token: 0x0400015F RID: 351
			DSC_EVENT_DCMMMON_RESET_DC_LOCATOR_TO_MM_DC = 2147749825U,
			// Token: 0x04000160 RID: 352
			DSC_EVENT_DCMMMON_RESET_DC_LOCATOR_TO_MM_DC_FINALLY = 3221491650U
		}
	}
}
