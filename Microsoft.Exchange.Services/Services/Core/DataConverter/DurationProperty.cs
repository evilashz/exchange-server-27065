using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000DB RID: 219
	internal sealed class DurationProperty : ComplexPropertyBase, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, IPropertyCommand
	{
		// Token: 0x060005F0 RID: 1520 RVA: 0x0001F219 File Offset: 0x0001D419
		private DurationProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001F222 File Offset: 0x0001D422
		public static DurationProperty CreateCommand(CommandContext commandContext)
		{
			return new DurationProperty(commandContext);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001F22A File Offset: 0x0001D42A
		public void ToXml()
		{
			throw new InvalidOperationException("DurationProperty.ToXml should not be called.");
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001F238 File Offset: 0x0001D438
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			ServiceObject serviceObject = commandSettings.ServiceObject;
			StoreObject storeObject = commandSettings.StoreObject;
			if (PropertyCommand.StorePropertyExists(storeObject, CalendarItemInstanceSchema.StartTime) && PropertyCommand.StorePropertyExists(storeObject, CalendarItemInstanceSchema.EndTime))
			{
				string value = DurationProperty.ToString((ExDateTime)storeObject[CalendarItemInstanceSchema.StartTime], (ExDateTime)storeObject[CalendarItemInstanceSchema.EndTime]);
				serviceObject[this.commandContext.PropertyInformation] = value;
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001F2AC File Offset: 0x0001D4AC
		public void ToXmlForPropertyBag()
		{
			throw new InvalidOperationException("DurationProperty.ToXmlForPropertyBag should not be called.");
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001F2B8 File Offset: 0x0001D4B8
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			ExDateTime end;
			ExDateTime start;
			if (PropertyCommand.TryGetValueFromPropertyBag<ExDateTime>(propertyBag, CalendarItemInstanceSchema.EndTime, out end) && PropertyCommand.TryGetValueFromPropertyBag<ExDateTime>(propertyBag, CalendarItemInstanceSchema.StartTime, out start))
			{
				serviceObject[this.commandContext.PropertyInformation] = DurationProperty.ToString(start, end);
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001F31D File Offset: 0x0001D51D
		private static string ToString(ExDateTime start, ExDateTime end)
		{
			return DurationProperty.ToString(end - start);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001F32C File Offset: 0x0001D52C
		private static string ToString(TimeSpan timeSpan)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (timeSpan.Ticks < 0L)
			{
				stringBuilder.Append("-P");
				if (timeSpan == TimeSpan.MinValue)
				{
					timeSpan = TimeSpan.MaxValue;
				}
				else
				{
					timeSpan = timeSpan.Negate();
				}
			}
			else
			{
				stringBuilder.Append("P");
			}
			bool flag = timeSpan.Hours != 0 || timeSpan.Minutes != 0 || timeSpan.Seconds != 0;
			if (timeSpan.Days != 0 || !flag)
			{
				stringBuilder.Append(timeSpan.Days);
				stringBuilder.Append("D");
			}
			if (flag)
			{
				stringBuilder.Append("T");
				if (timeSpan.Hours != 0)
				{
					stringBuilder.Append(timeSpan.Hours);
					stringBuilder.Append("H");
				}
				if (timeSpan.Minutes != 0)
				{
					stringBuilder.Append(timeSpan.Minutes);
					stringBuilder.Append("M");
				}
				if (timeSpan.Seconds != 0)
				{
					stringBuilder.Append(timeSpan.Seconds);
					stringBuilder.Append("S");
				}
			}
			return stringBuilder.ToString();
		}
	}
}
