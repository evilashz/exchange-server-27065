using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x0200001C RID: 28
	internal class AsyncQueueProbeResult : ConfigurablePropertyBag
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003F2C File Offset: 0x0000212C
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.RequestId.ToString());
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003F52 File Offset: 0x00002152
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00003F64 File Offset: 0x00002164
		public int InprogressBatchSize
		{
			get
			{
				return (int)this[AsyncQueueProbeSchema.InprogressBatchSize];
			}
			set
			{
				this[AsyncQueueProbeSchema.InprogressBatchSize] = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003F77 File Offset: 0x00002177
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00003F89 File Offset: 0x00002189
		public int BatchSize
		{
			get
			{
				return (int)this[AsyncQueueProbeSchema.BatchSize];
			}
			set
			{
				this[AsyncQueueProbeSchema.BatchSize] = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00003F9C File Offset: 0x0000219C
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00003FAE File Offset: 0x000021AE
		public int ProocessInprogressBackInSeconds
		{
			get
			{
				return (int)this[AsyncQueueProbeSchema.ProocessInprogressBackInSeconds];
			}
			set
			{
				this[AsyncQueueProbeSchema.ProocessInprogressBackInSeconds] = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00003FC1 File Offset: 0x000021C1
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00003FD3 File Offset: 0x000021D3
		public int ProocessBackInSeconds
		{
			get
			{
				return (int)this[AsyncQueueProbeSchema.ProocessBackInSeconds];
			}
			set
			{
				this[AsyncQueueProbeSchema.ProocessBackInSeconds] = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00003FE6 File Offset: 0x000021E6
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00003FF8 File Offset: 0x000021F8
		public string OwnerID
		{
			get
			{
				return (string)this[AsyncQueueProbeSchema.OwnerID];
			}
			set
			{
				this[AsyncQueueProbeSchema.OwnerID] = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004006 File Offset: 0x00002206
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00004018 File Offset: 0x00002218
		public byte Priority
		{
			get
			{
				return (byte)this[AsyncQueueProbeSchema.Priority];
			}
			set
			{
				this[AsyncQueueProbeSchema.Priority] = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x0000402B File Offset: 0x0000222B
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x0000403D File Offset: 0x0000223D
		public Guid RequestId
		{
			get
			{
				return (Guid)this[AsyncQueueProbeSchema.RequestId];
			}
			set
			{
				this[AsyncQueueProbeSchema.RequestId] = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00004050 File Offset: 0x00002250
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00004062 File Offset: 0x00002262
		public string StepName
		{
			get
			{
				return (string)this[AsyncQueueProbeSchema.StepName];
			}
			set
			{
				this[AsyncQueueProbeSchema.StepName] = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004070 File Offset: 0x00002270
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00004082 File Offset: 0x00002282
		public short StepNumber
		{
			get
			{
				return (short)this[AsyncQueueProbeSchema.StepNumber];
			}
			set
			{
				this[AsyncQueueProbeSchema.StepNumber] = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004095 File Offset: 0x00002295
		// (set) Token: 0x060000DA RID: 218 RVA: 0x000040A7 File Offset: 0x000022A7
		public int BitFlags
		{
			get
			{
				return (int)this[AsyncQueueProbeSchema.BitFlags];
			}
			set
			{
				this[AsyncQueueProbeSchema.BitFlags] = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000040BA File Offset: 0x000022BA
		// (set) Token: 0x060000DC RID: 220 RVA: 0x000040CC File Offset: 0x000022CC
		public short Ordinal
		{
			get
			{
				return (short)this[AsyncQueueProbeSchema.Ordinal];
			}
			set
			{
				this[AsyncQueueProbeSchema.Ordinal] = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000040DF File Offset: 0x000022DF
		// (set) Token: 0x060000DE RID: 222 RVA: 0x000040F1 File Offset: 0x000022F1
		public short Status
		{
			get
			{
				return (short)this[AsyncQueueProbeSchema.Status];
			}
			set
			{
				this[AsyncQueueProbeSchema.Status] = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00004104 File Offset: 0x00002304
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00004116 File Offset: 0x00002316
		public int FetchCount
		{
			get
			{
				return (int)this[AsyncQueueProbeSchema.FetchCount];
			}
			set
			{
				this[AsyncQueueProbeSchema.FetchCount] = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004129 File Offset: 0x00002329
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x0000413B File Offset: 0x0000233B
		public int ErrorCount
		{
			get
			{
				return (int)this[AsyncQueueProbeSchema.ErrorCount];
			}
			set
			{
				this[AsyncQueueProbeSchema.ErrorCount] = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x0000414E File Offset: 0x0000234E
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00004160 File Offset: 0x00002360
		public DateTime NextFetchDatetime
		{
			get
			{
				return (DateTime)this[AsyncQueueProbeSchema.NextFetchDatetime];
			}
			set
			{
				this[AsyncQueueProbeSchema.NextFetchDatetime] = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00004173 File Offset: 0x00002373
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00004185 File Offset: 0x00002385
		public DateTime CreatedDatetime
		{
			get
			{
				return (DateTime)this[AsyncQueueProbeSchema.CreatedDatetime];
			}
			set
			{
				this[AsyncQueueProbeSchema.CreatedDatetime] = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00004198 File Offset: 0x00002398
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x000041AA File Offset: 0x000023AA
		public DateTime ChangedDatetime
		{
			get
			{
				return (DateTime)this[AsyncQueueProbeSchema.ChangedDatetime];
			}
			set
			{
				this[AsyncQueueProbeSchema.ChangedDatetime] = value;
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000041BD File Offset: 0x000023BD
		public override Type GetSchemaType()
		{
			return typeof(AsyncQueueProbeSchema);
		}

		// Token: 0x04000076 RID: 118
		public static readonly PropertyDefinition[] Propertydefinitions = new PropertyDefinition[]
		{
			AsyncQueueProbeSchema.OwnerID,
			AsyncQueueProbeSchema.Priority,
			AsyncQueueProbeSchema.RequestId,
			AsyncQueueProbeSchema.StepName,
			AsyncQueueProbeSchema.StepNumber,
			AsyncQueueProbeSchema.BitFlags,
			AsyncQueueProbeSchema.Ordinal,
			AsyncQueueProbeSchema.Status,
			AsyncQueueProbeSchema.FetchCount,
			AsyncQueueProbeSchema.ErrorCount,
			AsyncQueueProbeSchema.NextFetchDatetime,
			AsyncQueueProbeSchema.CreatedDatetime,
			AsyncQueueProbeSchema.ChangedDatetime,
			AsyncQueueProbeSchema.InprogressBatchSize,
			AsyncQueueProbeSchema.BatchSize,
			AsyncQueueProbeSchema.ProocessInprogressBackInSeconds,
			AsyncQueueProbeSchema.ProocessBackInSeconds
		};
	}
}
