using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000077 RID: 119
	internal class DataLatencyDetail : ConfigurablePropertyBag
	{
		// Token: 0x06000496 RID: 1174 RVA: 0x0000F7A8 File Offset: 0x0000D9A8
		public DataLatencyDetail()
		{
			this[DataLatencyDetailSchema.Identity] = new ConfigObjectId(Guid.NewGuid().ToString());
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0000F7DE File Offset: 0x0000D9DE
		public override ObjectId Identity
		{
			get
			{
				return this[DataLatencyDetailSchema.Identity] as ObjectId;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0000F7F0 File Offset: 0x0000D9F0
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x0000F802 File Offset: 0x0000DA02
		public int TemporalPartition
		{
			get
			{
				return (int)this[DataLatencyDetailSchema.TemporalPartition];
			}
			set
			{
				this[DataLatencyDetailSchema.TemporalPartition] = value;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0000F815 File Offset: 0x0000DA15
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x0000F827 File Offset: 0x0000DA27
		public long RowCount
		{
			get
			{
				return (long)this[DataLatencyDetailSchema.RowCount];
			}
			set
			{
				this[DataLatencyDetailSchema.RowCount] = value;
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000F83A File Offset: 0x0000DA3A
		public override Type GetSchemaType()
		{
			return typeof(DataLatencyDetailSchema);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000F848 File Offset: 0x0000DA48
		public override bool Equals(object obj)
		{
			DataLatencyDetail dataLatencyDetail = obj as DataLatencyDetail;
			return dataLatencyDetail != null && this.Equals(dataLatencyDetail);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000F868 File Offset: 0x0000DA68
		public bool Equals(DataLatencyDetail detail)
		{
			return this.TemporalPartition == detail.TemporalPartition && this.RowCount == detail.RowCount;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000F889 File Offset: 0x0000DA89
		public override int GetHashCode()
		{
			return this.TemporalPartition;
		}
	}
}
