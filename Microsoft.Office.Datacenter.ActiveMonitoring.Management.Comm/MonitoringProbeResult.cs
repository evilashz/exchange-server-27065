using System;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Management.Common
{
	// Token: 0x0200000C RID: 12
	[Serializable]
	public class MonitoringProbeResult : ConfigurableObject
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00003B14 File Offset: 0x00001D14
		public MonitoringProbeResult() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003B24 File Offset: 0x00001D24
		internal MonitoringProbeResult(string server, RpcInvokeMonitoringProbe.RpcMonitorProbeResult rpcResult) : this()
		{
			this.Server = server;
			this.MonitorIdentity = rpcResult.MonitorIdentity;
			this.Error = rpcResult.Error;
			this.Exception = rpcResult.Exception;
			this.ExecutionContext = rpcResult.ExecutionContext;
			this.ExecutionEndTime = rpcResult.ExecutionEndTime;
			this.ExecutionId = rpcResult.ExecutionId;
			this.ExecutionStartTime = rpcResult.ExecutionStartTime;
			this.ExtensionXml = rpcResult.ExtensionXml;
			this.FailureContext = rpcResult.FailureContext;
			this.IsNotified = rpcResult.IsNotified;
			this.PoisonedCount = (int)rpcResult.PoisonedCount;
			this.RequestId = rpcResult.RequestId;
			this.ResultId = rpcResult.ResultId;
			this.ResultName = rpcResult.ResultName;
			this.ResultType = rpcResult.ResultType;
			this.RetryCount = (int)rpcResult.RetryCount;
			this.SampleValue = rpcResult.SampleValue;
			this.ServiceName = rpcResult.ServiceName;
			this.StateAttribute1 = rpcResult.StateAttribute1;
			this.StateAttribute2 = rpcResult.StateAttribute2;
			this.StateAttribute3 = rpcResult.StateAttribute3;
			this.StateAttribute4 = rpcResult.StateAttribute4;
			this.StateAttribute5 = rpcResult.StateAttribute5;
			this.StateAttribute6 = rpcResult.StateAttribute6;
			this.StateAttribute7 = rpcResult.StateAttribute7;
			this.StateAttribute8 = rpcResult.StateAttribute8;
			this.StateAttribute9 = rpcResult.StateAttribute9;
			this.StateAttribute10 = rpcResult.StateAttribute10;
			this.StateAttribute11 = rpcResult.StateAttribute11;
			this.StateAttribute12 = rpcResult.StateAttribute12;
			this.StateAttribute13 = rpcResult.StateAttribute13;
			this.StateAttribute14 = rpcResult.StateAttribute14;
			this.StateAttribute15 = rpcResult.StateAttribute15;
			this.StateAttribute16 = rpcResult.StateAttribute16;
			this.StateAttribute17 = rpcResult.StateAttribute17;
			this.StateAttribute18 = rpcResult.StateAttribute18;
			this.StateAttribute19 = rpcResult.StateAttribute19;
			this.StateAttribute20 = rpcResult.StateAttribute20;
			this.StateAttribute21 = rpcResult.StateAttribute21;
			this.StateAttribute22 = rpcResult.StateAttribute22;
			this.StateAttribute23 = rpcResult.StateAttribute23;
			this.StateAttribute24 = rpcResult.StateAttribute24;
			this.StateAttribute25 = rpcResult.StateAttribute25;
			this[SimpleProviderObjectSchema.Identity] = new MonitoringProbeResult.MonitoringProbeResultId(Guid.NewGuid());
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003D57 File Offset: 0x00001F57
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00003D69 File Offset: 0x00001F69
		public string Server
		{
			get
			{
				return (string)this[MonitoringProbeResultSchema.Server];
			}
			private set
			{
				this[MonitoringProbeResultSchema.Server] = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003D77 File Offset: 0x00001F77
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00003D89 File Offset: 0x00001F89
		public string MonitorIdentity
		{
			get
			{
				return (string)this[MonitoringProbeResultSchema.MonitorIdentity];
			}
			private set
			{
				this[MonitoringProbeResultSchema.MonitorIdentity] = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003D97 File Offset: 0x00001F97
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00003DA9 File Offset: 0x00001FA9
		public Guid RequestId
		{
			get
			{
				return (Guid)this[MonitoringProbeResultSchema.RequestId];
			}
			private set
			{
				this[MonitoringProbeResultSchema.RequestId] = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003DBC File Offset: 0x00001FBC
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00003DCE File Offset: 0x00001FCE
		public DateTime ExecutionStartTime
		{
			get
			{
				return (DateTime)this[MonitoringProbeResultSchema.ExecutionStartTime];
			}
			private set
			{
				this[MonitoringProbeResultSchema.ExecutionStartTime] = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003DE1 File Offset: 0x00001FE1
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00003DF3 File Offset: 0x00001FF3
		public DateTime ExecutionEndTime
		{
			get
			{
				return (DateTime)this[MonitoringProbeResultSchema.ExecutionEndTime];
			}
			private set
			{
				this[MonitoringProbeResultSchema.ExecutionEndTime] = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003E06 File Offset: 0x00002006
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00003E18 File Offset: 0x00002018
		public string Error
		{
			get
			{
				return (string)this[MonitoringProbeResultSchema.Error];
			}
			private set
			{
				this[MonitoringProbeResultSchema.Error] = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003E26 File Offset: 0x00002026
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00003E38 File Offset: 0x00002038
		public string Exception
		{
			get
			{
				return (string)this[MonitoringProbeResultSchema.Exception];
			}
			private set
			{
				this[MonitoringProbeResultSchema.Exception] = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003E46 File Offset: 0x00002046
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00003E58 File Offset: 0x00002058
		public int PoisonedCount
		{
			get
			{
				return (int)this[MonitoringProbeResultSchema.PoisonedCount];
			}
			private set
			{
				this[MonitoringProbeResultSchema.PoisonedCount] = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003E6B File Offset: 0x0000206B
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00003E7D File Offset: 0x0000207D
		public int ExecutionId
		{
			get
			{
				return (int)this[MonitoringProbeResultSchema.ExecutionId];
			}
			private set
			{
				this[MonitoringProbeResultSchema.ExecutionId] = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003E90 File Offset: 0x00002090
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00003EA2 File Offset: 0x000020A2
		public double SampleValue
		{
			get
			{
				return (double)this[MonitoringProbeResultSchema.SampleValue];
			}
			private set
			{
				this[MonitoringProbeResultSchema.SampleValue] = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003EB5 File Offset: 0x000020B5
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00003EC7 File Offset: 0x000020C7
		public string ExecutionContext
		{
			get
			{
				return (string)this[MonitoringProbeResultSchema.ExecutionContext];
			}
			private set
			{
				this[MonitoringProbeResultSchema.ExecutionContext] = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003ED5 File Offset: 0x000020D5
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00003EE7 File Offset: 0x000020E7
		public string FailureContext
		{
			get
			{
				return (string)this[MonitoringProbeResultSchema.FailureContext];
			}
			private set
			{
				this[MonitoringProbeResultSchema.FailureContext] = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003EF5 File Offset: 0x000020F5
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00003F07 File Offset: 0x00002107
		public string ExtensionXml
		{
			get
			{
				return (string)this[MonitoringProbeResultSchema.ExtensionXml];
			}
			private set
			{
				this[MonitoringProbeResultSchema.ExtensionXml] = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003F15 File Offset: 0x00002115
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00003F27 File Offset: 0x00002127
		public ResultType ResultType
		{
			get
			{
				return (ResultType)this[MonitoringProbeResultSchema.ResultType];
			}
			private set
			{
				this[MonitoringProbeResultSchema.ResultType] = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003F3A File Offset: 0x0000213A
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00003F4C File Offset: 0x0000214C
		public int RetryCount
		{
			get
			{
				return (int)this[MonitoringProbeResultSchema.RetryCount];
			}
			private set
			{
				this[MonitoringProbeResultSchema.RetryCount] = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003F5F File Offset: 0x0000215F
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00003F71 File Offset: 0x00002171
		public string ResultName
		{
			get
			{
				return (string)this[MonitoringProbeResultSchema.ResultName];
			}
			private set
			{
				this[MonitoringProbeResultSchema.ResultName] = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003F7F File Offset: 0x0000217F
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00003F91 File Offset: 0x00002191
		public bool IsNotified
		{
			get
			{
				return (bool)this[MonitoringProbeResultSchema.IsNotified];
			}
			private set
			{
				this[MonitoringProbeResultSchema.IsNotified] = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003FA4 File Offset: 0x000021A4
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00003FB6 File Offset: 0x000021B6
		public int ResultId
		{
			get
			{
				return (int)this[MonitoringProbeResultSchema.ResultId];
			}
			private set
			{
				this[MonitoringProbeResultSchema.ResultId] = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003FC9 File Offset: 0x000021C9
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00003FDB File Offset: 0x000021DB
		public string ServiceName
		{
			get
			{
				return (string)this[MonitoringProbeResultSchema.ServiceName];
			}
			private set
			{
				this[MonitoringProbeResultSchema.ServiceName] = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003FE9 File Offset: 0x000021E9
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00003FFB File Offset: 0x000021FB
		public string StateAttribute1
		{
			get
			{
				return (string)this[MonitoringProbeResultSchema.StateAttribute1];
			}
			private set
			{
				this[MonitoringProbeResultSchema.StateAttribute1] = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00004009 File Offset: 0x00002209
		// (set) Token: 0x060000BB RID: 187 RVA: 0x0000401B File Offset: 0x0000221B
		public string StateAttribute2
		{
			get
			{
				return (string)this[MonitoringProbeResultSchema.StateAttribute2];
			}
			private set
			{
				this[MonitoringProbeResultSchema.StateAttribute2] = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00004029 File Offset: 0x00002229
		// (set) Token: 0x060000BD RID: 189 RVA: 0x0000403B File Offset: 0x0000223B
		public string StateAttribute3
		{
			get
			{
				return (string)this[MonitoringProbeResultSchema.StateAttribute3];
			}
			private set
			{
				this[MonitoringProbeResultSchema.StateAttribute3] = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004049 File Offset: 0x00002249
		// (set) Token: 0x060000BF RID: 191 RVA: 0x0000405B File Offset: 0x0000225B
		public string StateAttribute4
		{
			get
			{
				return (string)this[MonitoringProbeResultSchema.StateAttribute4];
			}
			private set
			{
				this[MonitoringProbeResultSchema.StateAttribute4] = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00004069 File Offset: 0x00002269
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x0000407B File Offset: 0x0000227B
		public string StateAttribute5
		{
			get
			{
				return (string)this[MonitoringProbeResultSchema.StateAttribute5];
			}
			private set
			{
				this[MonitoringProbeResultSchema.StateAttribute5] = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004089 File Offset: 0x00002289
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x0000409B File Offset: 0x0000229B
		public double StateAttribute6
		{
			get
			{
				return (double)this[MonitoringProbeResultSchema.StateAttribute6];
			}
			private set
			{
				this[MonitoringProbeResultSchema.StateAttribute6] = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000040AE File Offset: 0x000022AE
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x000040C0 File Offset: 0x000022C0
		public double StateAttribute7
		{
			get
			{
				return (double)this[MonitoringProbeResultSchema.StateAttribute7];
			}
			private set
			{
				this[MonitoringProbeResultSchema.StateAttribute7] = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000040D3 File Offset: 0x000022D3
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x000040E5 File Offset: 0x000022E5
		public double StateAttribute8
		{
			get
			{
				return (double)this[MonitoringProbeResultSchema.StateAttribute8];
			}
			private set
			{
				this[MonitoringProbeResultSchema.StateAttribute8] = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000040F8 File Offset: 0x000022F8
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x0000410A File Offset: 0x0000230A
		public double StateAttribute9
		{
			get
			{
				return (double)this[MonitoringProbeResultSchema.StateAttribute9];
			}
			private set
			{
				this[MonitoringProbeResultSchema.StateAttribute9] = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000411D File Offset: 0x0000231D
		// (set) Token: 0x060000CB RID: 203 RVA: 0x0000412F File Offset: 0x0000232F
		public double StateAttribute10
		{
			get
			{
				return (double)this[MonitoringProbeResultSchema.StateAttribute10];
			}
			private set
			{
				this[MonitoringProbeResultSchema.StateAttribute10] = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00004142 File Offset: 0x00002342
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00004154 File Offset: 0x00002354
		public string StateAttribute11
		{
			get
			{
				return (string)this[MonitoringProbeResultSchema.StateAttribute11];
			}
			private set
			{
				this[MonitoringProbeResultSchema.StateAttribute11] = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00004162 File Offset: 0x00002362
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00004174 File Offset: 0x00002374
		public string StateAttribute12
		{
			get
			{
				return (string)this[MonitoringProbeResultSchema.StateAttribute12];
			}
			private set
			{
				this[MonitoringProbeResultSchema.StateAttribute12] = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004182 File Offset: 0x00002382
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00004194 File Offset: 0x00002394
		public string StateAttribute13
		{
			get
			{
				return (string)this[MonitoringProbeResultSchema.StateAttribute13];
			}
			private set
			{
				this[MonitoringProbeResultSchema.StateAttribute13] = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000041A2 File Offset: 0x000023A2
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x000041B4 File Offset: 0x000023B4
		public string StateAttribute14
		{
			get
			{
				return (string)this[MonitoringProbeResultSchema.StateAttribute14];
			}
			private set
			{
				this[MonitoringProbeResultSchema.StateAttribute14] = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000041C2 File Offset: 0x000023C2
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x000041D4 File Offset: 0x000023D4
		public string StateAttribute15
		{
			get
			{
				return (string)this[MonitoringProbeResultSchema.StateAttribute15];
			}
			private set
			{
				this[MonitoringProbeResultSchema.StateAttribute15] = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000041E2 File Offset: 0x000023E2
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x000041F4 File Offset: 0x000023F4
		public double StateAttribute16
		{
			get
			{
				return (double)this[MonitoringProbeResultSchema.StateAttribute16];
			}
			private set
			{
				this[MonitoringProbeResultSchema.StateAttribute16] = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004207 File Offset: 0x00002407
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00004219 File Offset: 0x00002419
		public double StateAttribute17
		{
			get
			{
				return (double)this[MonitoringProbeResultSchema.StateAttribute17];
			}
			private set
			{
				this[MonitoringProbeResultSchema.StateAttribute17] = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000DA RID: 218 RVA: 0x0000422C File Offset: 0x0000242C
		// (set) Token: 0x060000DB RID: 219 RVA: 0x0000423E File Offset: 0x0000243E
		public double StateAttribute18
		{
			get
			{
				return (double)this[MonitoringProbeResultSchema.StateAttribute18];
			}
			private set
			{
				this[MonitoringProbeResultSchema.StateAttribute18] = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004251 File Offset: 0x00002451
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00004263 File Offset: 0x00002463
		public double StateAttribute19
		{
			get
			{
				return (double)this[MonitoringProbeResultSchema.StateAttribute19];
			}
			private set
			{
				this[MonitoringProbeResultSchema.StateAttribute19] = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00004276 File Offset: 0x00002476
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00004288 File Offset: 0x00002488
		public double StateAttribute20
		{
			get
			{
				return (double)this[MonitoringProbeResultSchema.StateAttribute20];
			}
			private set
			{
				this[MonitoringProbeResultSchema.StateAttribute20] = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000429B File Offset: 0x0000249B
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x000042AD File Offset: 0x000024AD
		public string StateAttribute21
		{
			get
			{
				return (string)this[MonitoringProbeResultSchema.StateAttribute21];
			}
			private set
			{
				this[MonitoringProbeResultSchema.StateAttribute21] = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000042BB File Offset: 0x000024BB
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x000042CD File Offset: 0x000024CD
		public string StateAttribute22
		{
			get
			{
				return (string)this[MonitoringProbeResultSchema.StateAttribute22];
			}
			private set
			{
				this[MonitoringProbeResultSchema.StateAttribute22] = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000042DB File Offset: 0x000024DB
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x000042ED File Offset: 0x000024ED
		public string StateAttribute23
		{
			get
			{
				return (string)this[MonitoringProbeResultSchema.StateAttribute23];
			}
			private set
			{
				this[MonitoringProbeResultSchema.StateAttribute23] = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x000042FB File Offset: 0x000024FB
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x0000430D File Offset: 0x0000250D
		public string StateAttribute24
		{
			get
			{
				return (string)this[MonitoringProbeResultSchema.StateAttribute24];
			}
			private set
			{
				this[MonitoringProbeResultSchema.StateAttribute24] = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x0000431B File Offset: 0x0000251B
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x0000432D File Offset: 0x0000252D
		public string StateAttribute25
		{
			get
			{
				return (string)this[MonitoringProbeResultSchema.StateAttribute25];
			}
			private set
			{
				this[MonitoringProbeResultSchema.StateAttribute25] = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000EA RID: 234 RVA: 0x0000433B File Offset: 0x0000253B
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004342 File Offset: 0x00002542
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MonitoringProbeResult.schema;
			}
		}

		// Token: 0x04000043 RID: 67
		private static MonitoringProbeResultSchema schema = ObjectSchema.GetInstance<MonitoringProbeResultSchema>();

		// Token: 0x0200000D RID: 13
		[Serializable]
		public class MonitoringProbeResultId : ObjectId
		{
			// Token: 0x060000ED RID: 237 RVA: 0x00004355 File Offset: 0x00002555
			public MonitoringProbeResultId(Guid requestId)
			{
				this.identity = requestId.ToString("N");
			}

			// Token: 0x060000EE RID: 238 RVA: 0x0000436F File Offset: 0x0000256F
			public override string ToString()
			{
				return this.identity;
			}

			// Token: 0x060000EF RID: 239 RVA: 0x00004377 File Offset: 0x00002577
			public override byte[] GetBytes()
			{
				return Encoding.Unicode.GetBytes(this.ToString());
			}

			// Token: 0x04000044 RID: 68
			private readonly string identity;
		}
	}
}
