using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000170 RID: 368
	internal sealed class ReminderDueByProperty : ComplexPropertyBase, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, ISetCommand, ISetUpdateCommand, IUpdateCommand, IPropertyCommand
	{
		// Token: 0x06000A87 RID: 2695 RVA: 0x00033285 File Offset: 0x00031485
		private ReminderDueByProperty(CommandContext commandContext) : base(commandContext)
		{
			this.propertyDefinition = ItemSchema.ReminderDueBy;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00033299 File Offset: 0x00031499
		public static ReminderDueByProperty CreateCommand(CommandContext commandContext)
		{
			return new ReminderDueByProperty(commandContext);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x000332A1 File Offset: 0x000314A1
		public void Set()
		{
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x000332A4 File Offset: 0x000314A4
		void ISetCommand.SetPhase3()
		{
			this.SetPhase3();
			SetCommandSettings commandSettings = base.GetCommandSettings<SetCommandSettings>();
			string valueOrDefault = commandSettings.ServiceObject.GetValueOrDefault<string>(this.commandContext.PropertyInformation);
			Item item = (Item)commandSettings.StoreObject;
			this.SetProperty(item, valueOrDefault);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x000332EC File Offset: 0x000314EC
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			Item item = (Item)updateCommandSettings.StoreObject;
			string valueOrDefault = setPropertyUpdate.ServiceObject.GetValueOrDefault<string>(this.commandContext.PropertyInformation);
			this.SetProperty(item, valueOrDefault);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00033324 File Offset: 0x00031524
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			if (PropertyCommand.StorePropertyExists(storeObject, this.propertyDefinition))
			{
				ExDateTime systemDateTime = (ExDateTime)storeObject[this.propertyDefinition];
				commandSettings.ServiceObject[this.commandContext.PropertyInformation] = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(systemDateTime);
			}
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0003337C File Offset: 0x0003157C
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			ExDateTime systemDateTime;
			if (PropertyCommand.TryGetValueFromPropertyBag<ExDateTime>(commandSettings.PropertyBag, this.propertyDefinition, out systemDateTime))
			{
				commandSettings.ServiceObject[this.commandContext.PropertyInformation] = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(systemDateTime);
			}
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x000333C4 File Offset: 0x000315C4
		private void SetProperty(Item item, string value)
		{
			CalendarItemBase calendarItemBase = item as CalendarItemBase;
			ExTimeZone timeZone;
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010))
			{
				timeZone = ((calendarItemBase == null) ? item.Session.ExTimeZone : calendarItemBase.StartTimeZone);
			}
			else
			{
				timeZone = ExTimeZone.UtcTimeZone;
			}
			item[this.propertyDefinition] = ExDateTimeConverter.ParseTimeZoneRelated(value, timeZone);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00033420 File Offset: 0x00031620
		public void ToXml()
		{
			ToXmlCommandSettings commandSettings = base.GetCommandSettings<ToXmlCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			if (PropertyCommand.StorePropertyExists(storeObject, this.propertyDefinition))
			{
				ExDateTime systemDateTime = (ExDateTime)storeObject[this.propertyDefinition];
				base.CreateXmlTextElement(commandSettings.ServiceItem, this.xmlLocalName, ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(systemDateTime));
			}
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00033474 File Offset: 0x00031674
		public void ToXmlForPropertyBag()
		{
			ToXmlForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToXmlForPropertyBagCommandSettings>();
			ExDateTime systemDateTime;
			if (PropertyCommand.TryGetValueFromPropertyBag<ExDateTime>(commandSettings.PropertyBag, this.propertyDefinition, out systemDateTime))
			{
				base.CreateXmlTextElement(commandSettings.ServiceItem, this.xmlLocalName, ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(systemDateTime));
			}
		}

		// Token: 0x040007CC RID: 1996
		private PropertyDefinition propertyDefinition;
	}
}
