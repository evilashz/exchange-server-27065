using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001F5 RID: 501
	internal class RuleExtendedPropertyBatch : ConfigurableBatch
	{
		// Token: 0x0600150D RID: 5389 RVA: 0x0004200A File Offset: 0x0004020A
		public RuleExtendedPropertyBatch()
		{
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x00042014 File Offset: 0x00040214
		public RuleExtendedPropertyBatch(IEnumerable<RuleExtendedProperty> items)
		{
			BatchPropertyTable batchPropertyTable = new BatchPropertyTable();
			foreach (RuleExtendedProperty ruleExtendedProperty in items)
			{
				Guid identity = CombGuidGenerator.NewGuid();
				batchPropertyTable.AddPropertyValue(identity, RuleExtendedProperty.IDProperty, ruleExtendedProperty.ID);
				if (ruleExtendedProperty.PropertyName != null)
				{
					batchPropertyTable.AddPropertyValue(identity, ConfigurablePropertyTable.NameProperty, ruleExtendedProperty.PropertyName);
					if (ruleExtendedProperty.IntValue != null)
					{
						batchPropertyTable.AddPropertyValue(identity, ConfigurablePropertyTable.IntValueProperty, ruleExtendedProperty.IntValue);
					}
					if (ruleExtendedProperty.LongValue != null)
					{
						batchPropertyTable.AddPropertyValue(identity, ConfigurablePropertyTable.LongValueProperty, ruleExtendedProperty.LongValue);
					}
					if (ruleExtendedProperty.StringValue != null)
					{
						batchPropertyTable.AddPropertyValue(identity, ConfigurablePropertyTable.StringValueProperty, ruleExtendedProperty.StringValue);
					}
					if (ruleExtendedProperty.BlobValue != null)
					{
						batchPropertyTable.AddPropertyValue(identity, ConfigurablePropertyTable.BlobValueProperty, ruleExtendedProperty.BlobValue);
					}
					if (ruleExtendedProperty.DatetimeValue != null)
					{
						batchPropertyTable.AddPropertyValue(identity, ConfigurablePropertyTable.DatetimeValueProperty, ruleExtendedProperty.DatetimeValue);
					}
					if (ruleExtendedProperty.DecimalValue != null)
					{
						batchPropertyTable.AddPropertyValue(identity, ConfigurablePropertyTable.DecimalValueProperty, ruleExtendedProperty.DecimalValue);
					}
					if (ruleExtendedProperty.BoolValue != null)
					{
						batchPropertyTable.AddPropertyValue(identity, ConfigurablePropertyTable.BoolValueProperty, ruleExtendedProperty.BoolValue);
					}
					if (ruleExtendedProperty.GuidValue != null)
					{
						batchPropertyTable.AddPropertyValue(identity, ConfigurablePropertyTable.GuidValueProperty, ruleExtendedProperty.GuidValue);
					}
				}
			}
			base.Batch = batchPropertyTable;
		}
	}
}
