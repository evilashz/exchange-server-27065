using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	public abstract class TransactionOutcomeBase : ConfigurableObject
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00003090 File Offset: 0x00001290
		internal TransactionOutcomeBase(string clientAccessServer, string scenarioName, string scenarioDescription, string performanceCounterName, string userName) : base(new CasTransactionPropertyBag())
		{
			this.ClientAccessServer = clientAccessServer;
			this.Scenario = scenarioName;
			this.ScenarioDescription = scenarioDescription;
			this.PerformanceCounterName = performanceCounterName;
			this.Result = new CasTransactionResult(CasTransactionResultEnum.Undefined);
			this.Error = null;
			this.UserName = userName;
			this.StartTime = ExDateTime.Now;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000030EB File Offset: 0x000012EB
		internal virtual void Update(CasTransactionResultEnum result)
		{
			this.Update(result, null);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000030F5 File Offset: 0x000012F5
		internal virtual void Update(CasTransactionResultEnum result, string additionalInformation)
		{
			this.Update(result, (result == CasTransactionResultEnum.Success) ? TransactionOutcomeBase.ComputeLatency(this.StartTime) : TimeSpan.FromMilliseconds(-1.0), additionalInformation);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000311E File Offset: 0x0000131E
		internal virtual void Update(CasTransactionResultEnum result, string additionalInformation, EventTypeEnumeration eventType)
		{
			this.Update(result, (result == CasTransactionResultEnum.Success) ? TransactionOutcomeBase.ComputeLatency(this.StartTime) : TimeSpan.FromMilliseconds(-1.0), additionalInformation, eventType);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003148 File Offset: 0x00001348
		internal virtual void Update(CasTransactionResultEnum result, TimeSpan latency, string additionalInformation)
		{
			EventTypeEnumeration eventType;
			if (result == CasTransactionResultEnum.Failure)
			{
				eventType = EventTypeEnumeration.Error;
			}
			else if (result == CasTransactionResultEnum.Skipped)
			{
				eventType = EventTypeEnumeration.Warning;
			}
			else
			{
				if (result != CasTransactionResultEnum.Success)
				{
					throw new ArgumentException("Unhandled CasTransactionResultEnum type.");
				}
				eventType = EventTypeEnumeration.Success;
			}
			this.Update(result, latency, additionalInformation, eventType);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003182 File Offset: 0x00001382
		internal virtual void Update(CasTransactionResultEnum result, TimeSpan latency, string additionalInformation, EventTypeEnumeration eventType)
		{
			this.Result = new CasTransactionResult(result);
			this.Latency = latency;
			this.Error = additionalInformation;
			this.EventType = eventType;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000031AB File Offset: 0x000013AB
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000031B2 File Offset: 0x000013B2
		internal void UpdateScenario(string scenarioName, string scenarioDescription)
		{
			this.Scenario = scenarioName;
			this.ScenarioDescription = scenarioDescription;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000031C2 File Offset: 0x000013C2
		internal void UpdateLatency(TimeSpan latency)
		{
			this.Latency = latency;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000031D0 File Offset: 0x000013D0
		private static TimeSpan ComputeLatency(ExDateTime startTime)
		{
			ExDateTime now = ExDateTime.Now;
			TimeSpan result = now - startTime;
			if (result.Ticks == 0L)
			{
				return new TimeSpan(1L);
			}
			return result;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000031FF File Offset: 0x000013FF
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00003216 File Offset: 0x00001416
		public string ClientAccessServer
		{
			get
			{
				return (string)this.propertyBag[TransactionOutcomeBaseSchema.ClientAccessServer];
			}
			protected set
			{
				this.propertyBag[TransactionOutcomeBaseSchema.ClientAccessServer] = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003229 File Offset: 0x00001429
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00003240 File Offset: 0x00001440
		public string Scenario
		{
			get
			{
				return (string)this.propertyBag[TransactionOutcomeBaseSchema.ScenarioName];
			}
			private set
			{
				this.propertyBag[TransactionOutcomeBaseSchema.ScenarioName] = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003253 File Offset: 0x00001453
		// (set) Token: 0x0600005C RID: 92 RVA: 0x0000326A File Offset: 0x0000146A
		public string ScenarioDescription
		{
			get
			{
				return (string)this.propertyBag[TransactionOutcomeBaseSchema.ScenarioDescription];
			}
			private set
			{
				this.propertyBag[TransactionOutcomeBaseSchema.ScenarioDescription] = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000327D File Offset: 0x0000147D
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00003294 File Offset: 0x00001494
		public string PerformanceCounterName
		{
			get
			{
				return (string)this.propertyBag[TransactionOutcomeBaseSchema.PerformanceCounterName];
			}
			internal set
			{
				this.propertyBag[TransactionOutcomeBaseSchema.PerformanceCounterName] = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000032A7 File Offset: 0x000014A7
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000032BE File Offset: 0x000014BE
		public CasTransactionResult Result
		{
			get
			{
				return (CasTransactionResult)this.propertyBag[TransactionOutcomeBaseSchema.Result];
			}
			private set
			{
				this.propertyBag[TransactionOutcomeBaseSchema.Result] = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000032D1 File Offset: 0x000014D1
		// (set) Token: 0x06000062 RID: 98 RVA: 0x000032E8 File Offset: 0x000014E8
		public string Error
		{
			get
			{
				return (string)this.propertyBag[TransactionOutcomeBaseSchema.AdditionalInformation];
			}
			private set
			{
				this.propertyBag[TransactionOutcomeBaseSchema.AdditionalInformation] = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000032FB File Offset: 0x000014FB
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00003312 File Offset: 0x00001512
		public string UserName
		{
			get
			{
				return (string)this.propertyBag[TransactionOutcomeBaseSchema.UserName];
			}
			private set
			{
				this.propertyBag[TransactionOutcomeBaseSchema.UserName] = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003325 File Offset: 0x00001525
		// (set) Token: 0x06000066 RID: 102 RVA: 0x0000333C File Offset: 0x0000153C
		public ExDateTime StartTime
		{
			get
			{
				return (ExDateTime)this.propertyBag[TransactionOutcomeBaseSchema.StartTime];
			}
			internal set
			{
				this.propertyBag[TransactionOutcomeBaseSchema.StartTime] = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003354 File Offset: 0x00001554
		// (set) Token: 0x06000068 RID: 104 RVA: 0x0000336B File Offset: 0x0000156B
		public EnhancedTimeSpan Latency
		{
			get
			{
				return (EnhancedTimeSpan)this.propertyBag[TransactionOutcomeBaseSchema.Latency];
			}
			private set
			{
				this.propertyBag[TransactionOutcomeBaseSchema.Latency] = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003383 File Offset: 0x00001583
		// (set) Token: 0x0600006A RID: 106 RVA: 0x0000339A File Offset: 0x0000159A
		public EventTypeEnumeration EventType
		{
			get
			{
				return (EventTypeEnumeration)this.propertyBag[TransactionOutcomeBaseSchema.EventType];
			}
			private set
			{
				this.propertyBag[TransactionOutcomeBaseSchema.EventType] = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000033B4 File Offset: 0x000015B4
		public string LatencyInMillisecondsString
		{
			get
			{
				if (string.IsNullOrEmpty(this.Error))
				{
					return Math.Round(this.Latency.TotalMilliseconds, 2).ToString("F2", CultureInfo.InvariantCulture);
				}
				return string.Empty;
			}
		}
	}
}
