using System;
using System.Data;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200019E RID: 414
	internal sealed class TransportQueueLogSaveDataSet : ConfigurablePropertyBag
	{
		// Token: 0x0600117A RID: 4474 RVA: 0x000358AC File Offset: 0x00033AAC
		public TransportQueueLogSaveDataSet()
		{
			this.identity = new ConfigObjectId(CombGuidGenerator.NewGuid().ToString());
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x000358DD File Offset: 0x00033ADD
		public override ObjectId Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x000358E8 File Offset: 0x00033AE8
		public static TransportQueueLogSaveDataSet CreateDataSet(TransportQueueLogBatch batch)
		{
			TransportQueueLogSaveDataSet transportQueueLogSaveDataSet = new TransportQueueLogSaveDataSet();
			transportQueueLogSaveDataSet[TransportQueueLogSaveDataSetSchema.ForestIdProperty] = batch.ForestId;
			transportQueueLogSaveDataSet[TransportQueueLogSaveDataSetSchema.ServerIdProperty] = batch.ServerId;
			transportQueueLogSaveDataSet[TransportQueueLogSaveDataSetSchema.SnapshotDatetimeProperty] = batch.SnapshotDatetime;
			transportQueueLogSaveDataSet[TransportQueueLogSaveDataSetSchema.ServerPropertiesTableProperty] = TransportQueueLogSaveDataSet.GetServerProperties(batch);
			transportQueueLogSaveDataSet[TransportQueueLogSaveDataSetSchema.QueueLogPropertiesTableProperty] = TransportQueueLogSaveDataSet.GetQueueLogProperties(batch);
			return transportQueueLogSaveDataSet;
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00035960 File Offset: 0x00033B60
		public override Type GetSchemaType()
		{
			return typeof(TransportQueueLogSaveDataSetSchema);
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0003596C File Offset: 0x00033B6C
		private static DataTable GetServerProperties(TransportQueueLogBatch batch)
		{
			PropertyTable propertyTable = new PropertyTable();
			foreach (PropertyDefinition propertyDefinition in batch.GetPropertyDefinitions(false))
			{
				if (string.Compare(propertyDefinition.Name, TransportQueueLogSaveDataSetSchema.QueueLogPropertiesTableProperty.Name, StringComparison.OrdinalIgnoreCase) != 0)
				{
					propertyTable.AddPropertyValue(propertyDefinition, batch[propertyDefinition]);
				}
			}
			return propertyTable;
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x000359E0 File Offset: 0x00033BE0
		private static DataTable GetQueueLogProperties(TransportQueueLogBatch batch)
		{
			BatchPropertyTable batchPropertyTable = new BatchPropertyTable();
			if (batch.QueueLogs != null)
			{
				foreach (TransportQueueLog transportQueueLog in batch.QueueLogs)
				{
					foreach (PropertyDefinition propertyDefinition in DalHelper.GetPropertyDefinitions(transportQueueLog, true))
					{
						if (!propertyDefinition.Type.Equals(typeof(DataTable)))
						{
							batchPropertyTable.AddPropertyValue(transportQueueLog.QueueId, propertyDefinition, transportQueueLog[propertyDefinition]);
						}
					}
				}
			}
			return batchPropertyTable;
		}

		// Token: 0x04000853 RID: 2131
		private ObjectId identity;
	}
}
