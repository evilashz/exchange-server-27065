using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000171 RID: 369
	internal sealed class ReminderNextTimeProperty : ComplexPropertyBase, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, ISetCommand, ISetUpdateCommand, IUpdateCommand, IPropertyCommand
	{
		// Token: 0x06000A91 RID: 2705 RVA: 0x000334B6 File Offset: 0x000316B6
		private ReminderNextTimeProperty(CommandContext commandContext) : base(commandContext)
		{
			this.propertyDefinition = ItemSchema.ReminderNextTime;
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x000334CA File Offset: 0x000316CA
		public static ReminderNextTimeProperty CreateCommand(CommandContext commandContext)
		{
			return new ReminderNextTimeProperty(commandContext);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x000334D2 File Offset: 0x000316D2
		public void Set()
		{
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x000334D4 File Offset: 0x000316D4
		void ISetCommand.SetPhase3()
		{
			this.SetPhase3();
			SetCommandSettings commandSettings = base.GetCommandSettings<SetCommandSettings>();
			string valueOrDefault = commandSettings.ServiceObject.GetValueOrDefault<string>(this.commandContext.PropertyInformation);
			Item item = (Item)commandSettings.StoreObject;
			this.SetProperty(item, valueOrDefault);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0003351C File Offset: 0x0003171C
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			Item item = (Item)updateCommandSettings.StoreObject;
			string valueOrDefault = setPropertyUpdate.ServiceObject.GetValueOrDefault<string>(this.commandContext.PropertyInformation);
			this.SetProperty(item, valueOrDefault);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00033554 File Offset: 0x00031754
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

		// Token: 0x06000A97 RID: 2711 RVA: 0x000335AC File Offset: 0x000317AC
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			ExDateTime systemDateTime;
			if (PropertyCommand.TryGetValueFromPropertyBag<ExDateTime>(commandSettings.PropertyBag, this.propertyDefinition, out systemDateTime))
			{
				commandSettings.ServiceObject[this.commandContext.PropertyInformation] = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(systemDateTime);
			}
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x000335F4 File Offset: 0x000317F4
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

		// Token: 0x06000A99 RID: 2713 RVA: 0x00033650 File Offset: 0x00031850
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

		// Token: 0x06000A9A RID: 2714 RVA: 0x000336A4 File Offset: 0x000318A4
		public void ToXmlForPropertyBag()
		{
			ToXmlForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToXmlForPropertyBagCommandSettings>();
			ExDateTime systemDateTime;
			if (PropertyCommand.TryGetValueFromPropertyBag<ExDateTime>(commandSettings.PropertyBag, this.propertyDefinition, out systemDateTime))
			{
				base.CreateXmlTextElement(commandSettings.ServiceItem, this.xmlLocalName, ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(systemDateTime));
			}
		}

		// Token: 0x040007CD RID: 1997
		private PropertyDefinition propertyDefinition;
	}
}
