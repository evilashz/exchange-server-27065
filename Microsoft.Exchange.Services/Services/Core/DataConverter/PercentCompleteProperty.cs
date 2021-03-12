using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200018A RID: 394
	internal class PercentCompleteProperty : SimpleProperty
	{
		// Token: 0x06000B27 RID: 2855 RVA: 0x00035200 File Offset: 0x00033400
		private PercentCompleteProperty(CommandContext commandContext, BaseConverter converter) : base(commandContext, converter)
		{
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0003520A File Offset: 0x0003340A
		public new static PercentCompleteProperty CreateCommand(CommandContext commandContext)
		{
			return new PercentCompleteProperty(commandContext, new DoubleConverter());
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00035218 File Offset: 0x00033418
		protected override object GetPropertyValue(StoreObject storeObject)
		{
			double decimalValue = (double)PropertyCommand.GetPropertyValueFromStoreObject(storeObject, this.propertyDefinition);
			return this.DecimalToPercent(decimalValue);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00035240 File Offset: 0x00033440
		protected override object PreparePropertyBagValue(object propertyValue)
		{
			if (propertyValue is double)
			{
				double decimalValue = (double)propertyValue;
				return this.DecimalToPercent(decimalValue);
			}
			return propertyValue;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00035268 File Offset: 0x00033468
		protected override object Parse(string propertyString)
		{
			object result;
			try
			{
				result = double.Parse(propertyString, CultureInfo.InvariantCulture) / 100.0;
			}
			catch (OverflowException innerException)
			{
				throw new InvalidValueForPropertyException(CoreResources.IDs.ErrorInvalidValueForProperty, innerException);
			}
			return result;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x000352B8 File Offset: 0x000334B8
		protected override void SetStoreObjectProperty(StoreObject storeObject, PropertyDefinition propertyDefinition, object value)
		{
			Task task = storeObject as Task;
			double num = (double)value;
			if (num < 0.0 || num > 1.0)
			{
				throw new InvalidPercentCompleteValueException();
			}
			if (num == 1.0)
			{
				CompleteDateProperty.SetStatusCompleted(task, ExDateTime.GetNow(task.Session.ExTimeZone));
				return;
			}
			task.PercentComplete = num;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0003531C File Offset: 0x0003351C
		private string DecimalToPercent(double decimalValue)
		{
			return (decimalValue * 100.0).ToString(CultureInfo.InvariantCulture);
		}
	}
}
