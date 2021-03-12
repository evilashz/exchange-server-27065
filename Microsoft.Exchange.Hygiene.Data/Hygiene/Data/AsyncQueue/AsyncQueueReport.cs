using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x0200001D RID: 29
	internal class AsyncQueueReport : ConfigurablePropertyBag
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000042A9 File Offset: 0x000024A9
		public override ObjectId Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000ED RID: 237 RVA: 0x000042B1 File Offset: 0x000024B1
		// (set) Token: 0x060000EE RID: 238 RVA: 0x000042C3 File Offset: 0x000024C3
		public string Report
		{
			get
			{
				return (string)this[AsyncQueueReportSchema.ReportProperty];
			}
			set
			{
				this[AsyncQueueReportSchema.ReportProperty] = value;
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000042D1 File Offset: 0x000024D1
		public override Type GetSchemaType()
		{
			return typeof(AsyncQueueReportSchema);
		}

		// Token: 0x04000077 RID: 119
		private ConfigObjectId identity = new ConfigObjectId(Guid.NewGuid().ToString());
	}
}
