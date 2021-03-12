using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200019C RID: 412
	internal class TransportQueueLogBatch : ConfigurablePropertyBag
	{
		// Token: 0x06001160 RID: 4448 RVA: 0x0003564C File Offset: 0x0003384C
		public TransportQueueLogBatch()
		{
			this.QueueLogs = new MultiValuedProperty<TransportQueueLog>();
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x00035660 File Offset: 0x00033860
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.ServerId.ToString());
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x00035686 File Offset: 0x00033886
		// (set) Token: 0x06001163 RID: 4451 RVA: 0x00035698 File Offset: 0x00033898
		public Guid ServerId
		{
			get
			{
				return (Guid)this[TransportQueueLogBatchSchema.ServerIdProperty];
			}
			set
			{
				this[TransportQueueLogBatchSchema.ServerIdProperty] = value;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x000356AB File Offset: 0x000338AB
		// (set) Token: 0x06001165 RID: 4453 RVA: 0x000356BD File Offset: 0x000338BD
		public string ServerName
		{
			get
			{
				return (string)this[TransportQueueLogBatchSchema.ServerNameProperty];
			}
			set
			{
				this[TransportQueueLogBatchSchema.ServerNameProperty] = value;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x000356CB File Offset: 0x000338CB
		// (set) Token: 0x06001167 RID: 4455 RVA: 0x000356DD File Offset: 0x000338DD
		public Guid DagId
		{
			get
			{
				return (Guid)this[TransportQueueLogBatchSchema.DagIdProperty];
			}
			set
			{
				this[TransportQueueLogBatchSchema.DagIdProperty] = value;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x000356F0 File Offset: 0x000338F0
		// (set) Token: 0x06001169 RID: 4457 RVA: 0x00035702 File Offset: 0x00033902
		public string DagName
		{
			get
			{
				return (string)this[TransportQueueLogBatchSchema.DagNameProperty];
			}
			set
			{
				this[TransportQueueLogBatchSchema.DagNameProperty] = value;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x00035710 File Offset: 0x00033910
		// (set) Token: 0x0600116B RID: 4459 RVA: 0x00035722 File Offset: 0x00033922
		public Guid ADSiteId
		{
			get
			{
				return (Guid)this[TransportQueueLogBatchSchema.ADSiteIdProperty];
			}
			set
			{
				this[TransportQueueLogBatchSchema.ADSiteIdProperty] = value;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x0600116C RID: 4460 RVA: 0x00035735 File Offset: 0x00033935
		// (set) Token: 0x0600116D RID: 4461 RVA: 0x00035747 File Offset: 0x00033947
		public string ADSiteName
		{
			get
			{
				return (string)this[TransportQueueLogBatchSchema.ADSiteNameProperty];
			}
			set
			{
				this[TransportQueueLogBatchSchema.ADSiteNameProperty] = value;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x0600116E RID: 4462 RVA: 0x00035755 File Offset: 0x00033955
		// (set) Token: 0x0600116F RID: 4463 RVA: 0x00035767 File Offset: 0x00033967
		public Guid ForestId
		{
			get
			{
				return (Guid)this[TransportQueueLogBatchSchema.ForestIdProperty];
			}
			set
			{
				this[TransportQueueLogBatchSchema.ForestIdProperty] = value;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x0003577A File Offset: 0x0003397A
		// (set) Token: 0x06001171 RID: 4465 RVA: 0x0003578C File Offset: 0x0003398C
		public string ForestName
		{
			get
			{
				return (string)this[TransportQueueLogBatchSchema.ForestNameProperty];
			}
			set
			{
				this[TransportQueueLogBatchSchema.ForestNameProperty] = value;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x0003579A File Offset: 0x0003399A
		// (set) Token: 0x06001173 RID: 4467 RVA: 0x000357AC File Offset: 0x000339AC
		public DateTime SnapshotDatetime
		{
			get
			{
				return (DateTime)this[TransportQueueLogBatchSchema.SnapshotDatetimeProperty];
			}
			set
			{
				this[TransportQueueLogBatchSchema.SnapshotDatetimeProperty] = value;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x000357BF File Offset: 0x000339BF
		// (set) Token: 0x06001175 RID: 4469 RVA: 0x000357D1 File Offset: 0x000339D1
		public MultiValuedProperty<TransportQueueLog> QueueLogs
		{
			get
			{
				return (MultiValuedProperty<TransportQueueLog>)this[TransportQueueLogBatchSchema.QueueLogProperty];
			}
			private set
			{
				this[TransportQueueLogBatchSchema.QueueLogProperty] = value;
			}
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x000357DF File Offset: 0x000339DF
		public void Add(TransportQueueLog queueLog)
		{
			if (queueLog == null)
			{
				throw new ArgumentNullException("queueLog object is null");
			}
			if (string.IsNullOrWhiteSpace(queueLog.QueueName))
			{
				throw new ArgumentNullException("queueLog.QueueName is empty/null");
			}
			this.QueueLogs.Add(queueLog);
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00035813 File Offset: 0x00033A13
		public override Type GetSchemaType()
		{
			return typeof(TransportQueueLogBatchSchema);
		}
	}
}
