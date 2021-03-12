using System;
using System.Globalization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000025 RID: 37
	[Serializable]
	public class LiveIdAuthenticationOutcome : ConfigurableObject
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00005DAC File Offset: 0x00003FAC
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return LiveIdAuthenticationOutcome.schema;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00005DB4 File Offset: 0x00003FB4
		public string LatencyInMillisecondsString
		{
			get
			{
				return Math.Round(this.Latency.TotalMilliseconds, 2).ToString("F2", CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00005DE7 File Offset: 0x00003FE7
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00005DF9 File Offset: 0x00003FF9
		public string Server
		{
			get
			{
				return (string)this[LiveIdAuthenticationOutcomeSchema.Server];
			}
			internal set
			{
				this[LiveIdAuthenticationOutcomeSchema.Server] = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00005E07 File Offset: 0x00004007
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00005E19 File Offset: 0x00004019
		public string Mailbox
		{
			get
			{
				return (string)this[LiveIdAuthenticationOutcomeSchema.Mailbox];
			}
			internal set
			{
				this[LiveIdAuthenticationOutcomeSchema.Mailbox] = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00005E27 File Offset: 0x00004027
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00005E39 File Offset: 0x00004039
		public LiveIdAuthenticationResult Result
		{
			get
			{
				return (LiveIdAuthenticationResult)this[LiveIdAuthenticationOutcomeSchema.Result];
			}
			internal set
			{
				this[LiveIdAuthenticationOutcomeSchema.Result] = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00005E47 File Offset: 0x00004047
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00005E67 File Offset: 0x00004067
		public TimeSpan Latency
		{
			get
			{
				return (TimeSpan)(this[LiveIdAuthenticationOutcomeSchema.Latency] ?? TimeSpan.Zero);
			}
			internal set
			{
				this[LiveIdAuthenticationOutcomeSchema.Latency] = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00005E7A File Offset: 0x0000407A
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00005E91 File Offset: 0x00004091
		public string Error
		{
			get
			{
				return (string)this.propertyBag[LiveIdAuthenticationOutcomeSchema.Error];
			}
			internal set
			{
				this.propertyBag[LiveIdAuthenticationOutcomeSchema.Error] = value;
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005EA4 File Offset: 0x000040A4
		public LiveIdAuthenticationOutcome(string server, string username) : base(new SimpleProviderPropertyBag())
		{
			this.Server = server;
			this.Mailbox = username;
			this.Result = new LiveIdAuthenticationResult(LiveIdAuthenticationResultEnum.Undefined);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005ED8 File Offset: 0x000040D8
		internal void Update(LiveIdAuthenticationResultEnum resultEnum, TimeSpan latency, string error)
		{
			lock (this.thisLock)
			{
				this.Result = new LiveIdAuthenticationResult(resultEnum);
				this.Latency = latency;
				this.Error = (error ?? string.Empty);
			}
		}

		// Token: 0x040000B6 RID: 182
		[NonSerialized]
		private object thisLock = new object();

		// Token: 0x040000B7 RID: 183
		private static LiveIdAuthenticationOutcomeSchema schema = ObjectSchema.GetInstance<LiveIdAuthenticationOutcomeSchema>();
	}
}
