using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x02000015 RID: 21
	internal class AsyncQueueLogProperty : ConfigurablePropertyBag
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003A86 File Offset: 0x00001C86
		public override ObjectId Identity
		{
			get
			{
				return new ADObjectId(this.identity);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003A93 File Offset: 0x00001C93
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00003AA5 File Offset: 0x00001CA5
		public DateTime LogTime
		{
			get
			{
				return (DateTime)this[AsyncQueueLogPropertySchema.LogTimeProperty];
			}
			set
			{
				this[AsyncQueueLogPropertySchema.LogTimeProperty] = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003AB8 File Offset: 0x00001CB8
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00003ACA File Offset: 0x00001CCA
		public string LogType
		{
			get
			{
				return (string)this[AsyncQueueLogPropertySchema.LogTypeProperty];
			}
			set
			{
				this[AsyncQueueLogPropertySchema.LogTypeProperty] = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003AD8 File Offset: 0x00001CD8
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00003AEA File Offset: 0x00001CEA
		public int LogIndex
		{
			get
			{
				return (int)this[AsyncQueueLogPropertySchema.LogIndexProperty];
			}
			set
			{
				this[AsyncQueueLogPropertySchema.LogIndexProperty] = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003AFD File Offset: 0x00001CFD
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00003B0F File Offset: 0x00001D0F
		public string LogData
		{
			get
			{
				return (string)this[AsyncQueueLogPropertySchema.LogDataProperty];
			}
			set
			{
				this[AsyncQueueLogPropertySchema.LogDataProperty] = value;
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003B1D File Offset: 0x00001D1D
		public override Type GetSchemaType()
		{
			return typeof(AsyncQueueLogPropertySchema);
		}

		// Token: 0x04000044 RID: 68
		private readonly Guid identity = CombGuidGenerator.NewGuid();
	}
}
