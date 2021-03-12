using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x02000013 RID: 19
	internal class AsyncQueueLogBatch : ConfigurablePropertyBag
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00003970 File Offset: 0x00001B70
		public AsyncQueueLogBatch(Guid tenantId)
		{
			this.OrganizationalUnitRoot = tenantId;
			this.Logs = new MultiValuedProperty<AsyncQueueLog>();
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600009C RID: 156 RVA: 0x0000398C File Offset: 0x00001B8C
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.OrganizationalUnitRoot.ToString());
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000039B2 File Offset: 0x00001BB2
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000039C4 File Offset: 0x00001BC4
		public Guid OrganizationalUnitRoot
		{
			get
			{
				return (Guid)this[AsyncQueueLogBatchSchema.OrganizationalUnitRootProperty];
			}
			set
			{
				this[AsyncQueueLogBatchSchema.OrganizationalUnitRootProperty] = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000039D7 File Offset: 0x00001BD7
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x000039E4 File Offset: 0x00001BE4
		public object PersistentStoreCopyId
		{
			get
			{
				return this[AsyncQueueLogBatchSchema.FssCopyIdProp];
			}
			set
			{
				this[AsyncQueueLogBatchSchema.FssCopyIdProp] = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000039F2 File Offset: 0x00001BF2
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00003A04 File Offset: 0x00001C04
		public MultiValuedProperty<AsyncQueueLog> Logs
		{
			get
			{
				return (MultiValuedProperty<AsyncQueueLog>)this[AsyncQueueLogBatchSchema.LogProperty];
			}
			set
			{
				this[AsyncQueueLogBatchSchema.LogProperty] = value;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003A12 File Offset: 0x00001C12
		public void Add(AsyncQueueLog log)
		{
			if (log == null)
			{
				throw new ArgumentNullException("log object is NULL");
			}
			this.Logs.Add(log);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003A2E File Offset: 0x00001C2E
		public override Type GetSchemaType()
		{
			return typeof(AsyncQueueLogBatchSchema);
		}
	}
}
