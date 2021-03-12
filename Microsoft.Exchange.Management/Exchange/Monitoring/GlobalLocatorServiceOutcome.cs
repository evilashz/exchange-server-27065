using System;
using System.Globalization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200001C RID: 28
	[Serializable]
	public class GlobalLocatorServiceOutcome : ConfigurableObject
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004C1C File Offset: 0x00002E1C
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return GlobalLocatorServiceOutcome.schema;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004C24 File Offset: 0x00002E24
		public string LatencyInMillisecondsString
		{
			get
			{
				return Math.Round(this.Latency.TotalMilliseconds, 2).ToString("F2", CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004C57 File Offset: 0x00002E57
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00004C69 File Offset: 0x00002E69
		public string Server
		{
			get
			{
				return (string)this[GlobalLocatorServiceOutcomeSchema.Server];
			}
			internal set
			{
				this[GlobalLocatorServiceOutcomeSchema.Server] = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004C77 File Offset: 0x00002E77
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00004C89 File Offset: 0x00002E89
		public GlobalLocatorServiceResult Result
		{
			get
			{
				return (GlobalLocatorServiceResult)this[GlobalLocatorServiceOutcomeSchema.Result];
			}
			internal set
			{
				this[GlobalLocatorServiceOutcomeSchema.Result] = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00004C97 File Offset: 0x00002E97
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00004CB7 File Offset: 0x00002EB7
		public TimeSpan Latency
		{
			get
			{
				return (TimeSpan)(this[GlobalLocatorServiceOutcomeSchema.Latency] ?? TimeSpan.Zero);
			}
			internal set
			{
				this[GlobalLocatorServiceOutcomeSchema.Latency] = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004CCA File Offset: 0x00002ECA
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00004CE1 File Offset: 0x00002EE1
		public string Error
		{
			get
			{
				return (string)this.propertyBag[GlobalLocatorServiceOutcomeSchema.Error];
			}
			internal set
			{
				this.propertyBag[GlobalLocatorServiceOutcomeSchema.Error] = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004CF4 File Offset: 0x00002EF4
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00004D0B File Offset: 0x00002F0B
		public string Output
		{
			get
			{
				return (string)this.propertyBag[GlobalLocatorServiceOutcomeSchema.Output];
			}
			internal set
			{
				this.propertyBag[GlobalLocatorServiceOutcomeSchema.Output] = value;
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004D1E File Offset: 0x00002F1E
		public GlobalLocatorServiceOutcome(string server) : base(new SimpleProviderPropertyBag())
		{
			this.Server = server;
			this.Result = new GlobalLocatorServiceResult(GlobalLocatorServiceResultEnum.Undefined);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004D4C File Offset: 0x00002F4C
		internal void Update(GlobalLocatorServiceResultEnum resultEnum, TimeSpan latency, string error, string output)
		{
			lock (this.thisLock)
			{
				this.Result = new GlobalLocatorServiceResult(resultEnum);
				this.Latency = latency;
				this.Error = (error ?? string.Empty);
				this.Output = (output ?? string.Empty);
			}
		}

		// Token: 0x04000082 RID: 130
		[NonSerialized]
		private object thisLock = new object();

		// Token: 0x04000083 RID: 131
		private static GlobalLocatorServiceOutcomeSchema schema = ObjectSchema.GetInstance<GlobalLocatorServiceOutcomeSchema>();
	}
}
