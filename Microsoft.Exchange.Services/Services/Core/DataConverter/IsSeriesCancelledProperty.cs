using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000E4 RID: 228
	internal sealed class IsSeriesCancelledProperty : ComplexPropertyBase, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, IPropertyCommand
	{
		// Token: 0x06000640 RID: 1600 RVA: 0x00020C9B File Offset: 0x0001EE9B
		private IsSeriesCancelledProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00020CA4 File Offset: 0x0001EEA4
		public static IsSeriesCancelledProperty CreateCommand(CommandContext commandContext)
		{
			return new IsSeriesCancelledProperty(commandContext);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00020CAC File Offset: 0x0001EEAC
		public void ToXml()
		{
			throw new InvalidOperationException("IsSeriesCancelledProperty.ToXml should not be called.");
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00020CB8 File Offset: 0x0001EEB8
		public void ToXmlForPropertyBag()
		{
			throw new InvalidOperationException("IsSeriesCancelledProperty.ToXmlForPropertyBag should not be called.");
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00020CC4 File Offset: 0x0001EEC4
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			StoreObject storeObject = commandSettings.StoreObject;
			CalendarItemBase calendarItemBase = commandSettings.StoreObject as CalendarItemBase;
			if (calendarItemBase != null && calendarItemBase.CalendarItemType != CalendarItemType.Single)
			{
				serviceObject[propertyInformation] = calendarItemBase.GetValueOrDefault<bool>(CalendarItemOccurrenceSchema.IsSeriesCancelled);
			}
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00020D20 File Offset: 0x0001EF20
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			bool flag;
			if (PropertyCommand.TryGetValueFromPropertyBag<bool>(propertyBag, CalendarItemOccurrenceSchema.IsSeriesCancelled, out flag))
			{
				serviceObject[propertyInformation] = flag;
			}
		}
	}
}
