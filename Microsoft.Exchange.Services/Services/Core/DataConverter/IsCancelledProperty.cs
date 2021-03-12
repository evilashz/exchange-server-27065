using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000E3 RID: 227
	internal sealed class IsCancelledProperty : ComplexPropertyBase, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, IPropertyCommand
	{
		// Token: 0x0600063A RID: 1594 RVA: 0x00020BA8 File Offset: 0x0001EDA8
		private IsCancelledProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00020BB1 File Offset: 0x0001EDB1
		public static IsCancelledProperty CreateCommand(CommandContext commandContext)
		{
			return new IsCancelledProperty(commandContext);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00020BB9 File Offset: 0x0001EDB9
		public void ToXml()
		{
			throw new InvalidOperationException("IsCancelledProperty.ToXml should not be called.");
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00020BC5 File Offset: 0x0001EDC5
		public void ToXmlForPropertyBag()
		{
			throw new InvalidOperationException("IsCancelledProperty.ToXmlForPropertyBag should not be called.");
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00020BD4 File Offset: 0x0001EDD4
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			StoreObject storeObject = commandSettings.StoreObject;
			CalendarItemBase calendarItemBase = commandSettings.StoreObject as CalendarItemBase;
			bool isCancelled;
			if (calendarItemBase == null)
			{
				calendarItemBase = ((MeetingRequest)commandSettings.StoreObject).GetCachedEmbeddedItem();
				isCancelled = calendarItemBase.IsCancelled;
			}
			else
			{
				isCancelled = calendarItemBase.IsCancelled;
			}
			serviceObject[propertyInformation] = isCancelled;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00020C44 File Offset: 0x0001EE44
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			int num;
			if (PropertyCommand.TryGetValueFromPropertyBag<int>(propertyBag, CalendarItemBaseSchema.AppointmentState, out num))
			{
				bool flag = (num & 4) != 0;
				serviceObject[propertyInformation] = flag;
			}
		}
	}
}
