using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001E6 RID: 486
	internal class ProcessorBatch : ConfigurableBatch
	{
		// Token: 0x06001477 RID: 5239 RVA: 0x00041135 File Offset: 0x0003F335
		public ProcessorBatch()
		{
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x00041140 File Offset: 0x0003F340
		public ProcessorBatch(IEnumerable<Processor> items)
		{
			BatchPropertyTable batchPropertyTable = new BatchPropertyTable();
			foreach (Processor processor in items)
			{
				Guid identity = CombGuidGenerator.NewGuid();
				batchPropertyTable.AddPropertyValue(identity, Processor.ProcessorIDProperty, processor.ProcessorID);
				if (processor.PropertyName != null)
				{
					batchPropertyTable.AddPropertyValue(identity, ConfigurablePropertyTable.NameProperty, processor.PropertyName);
					if (processor.IntValue != null)
					{
						batchPropertyTable.AddPropertyValue(identity, ConfigurablePropertyTable.IntValueProperty, processor.IntValue);
					}
					if (processor.LongValue != null)
					{
						batchPropertyTable.AddPropertyValue(identity, ConfigurablePropertyTable.LongValueProperty, processor.LongValue);
					}
					if (processor.StringValue != null)
					{
						batchPropertyTable.AddPropertyValue(identity, ConfigurablePropertyTable.StringValueProperty, processor.StringValue);
					}
					if (processor.BlobValue != null)
					{
						batchPropertyTable.AddPropertyValue(identity, ConfigurablePropertyTable.BlobValueProperty, processor.BlobValue);
					}
					if (processor.DatetimeValue != null)
					{
						batchPropertyTable.AddPropertyValue(identity, ConfigurablePropertyTable.DatetimeValueProperty, processor.DatetimeValue);
					}
					if (processor.DecimalValue != null)
					{
						batchPropertyTable.AddPropertyValue(identity, ConfigurablePropertyTable.DecimalValueProperty, processor.DecimalValue);
					}
					if (processor.BoolValue != null)
					{
						batchPropertyTable.AddPropertyValue(identity, ConfigurablePropertyTable.BoolValueProperty, processor.BoolValue);
					}
					if (processor.GuidValue != null)
					{
						batchPropertyTable.AddPropertyValue(identity, ConfigurablePropertyTable.GuidValueProperty, processor.GuidValue);
					}
				}
			}
			base.Batch = batchPropertyTable;
		}
	}
}
