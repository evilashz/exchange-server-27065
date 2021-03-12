using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000E9 RID: 233
	internal sealed class OriginalStartProperty : ComplexPropertyBase, IToXmlCommand, IToServiceObjectCommand, IPropertyCommand
	{
		// Token: 0x0600065A RID: 1626 RVA: 0x0002118D File Offset: 0x0001F38D
		private OriginalStartProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00021196 File Offset: 0x0001F396
		public static OriginalStartProperty CreateCommand(CommandContext commandContext)
		{
			return new OriginalStartProperty(commandContext);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0002119E File Offset: 0x0001F39E
		public void ToXml()
		{
			throw new InvalidOperationException("OriginalStartProperty.ToXml should not be called");
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x000211AC File Offset: 0x0001F3AC
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			ServiceObject serviceObject = commandSettings.ServiceObject;
			StoreObject storeObject = commandSettings.StoreObject;
			MeetingRequest meetingRequest = storeObject as MeetingRequest;
			if (meetingRequest != null)
			{
				CalendarItemBase cachedEmbeddedItem = meetingRequest.GetCachedEmbeddedItem();
				CalendarItemOccurrence calendarItemOccurrence = cachedEmbeddedItem as CalendarItemOccurrence;
				this.ToServiceObject(serviceObject, calendarItemOccurrence);
				return;
			}
			this.ToServiceObject(serviceObject, storeObject as CalendarItemOccurrence);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00021200 File Offset: 0x0001F400
		private void ToServiceObject(ServiceObject serviceObject, CalendarItemOccurrence calendarItemOccurrence)
		{
			if (calendarItemOccurrence == null)
			{
				return;
			}
			serviceObject.PropertyBag[CalendarItemSchema.OriginalStart] = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(calendarItemOccurrence.OriginalStartTime);
		}
	}
}
