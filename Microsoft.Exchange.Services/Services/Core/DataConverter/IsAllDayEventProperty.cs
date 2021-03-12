using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000E2 RID: 226
	internal sealed class IsAllDayEventProperty : ComplexPropertyBase, IToXmlCommand, IToXmlForPropertyBagCommand, ISetCommand, ISetUpdateCommand, IUpdateCommand, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, IPropertyCommand
	{
		// Token: 0x06000630 RID: 1584 RVA: 0x00020A32 File Offset: 0x0001EC32
		private IsAllDayEventProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00020A3B File Offset: 0x0001EC3B
		public static IsAllDayEventProperty CreateCommand(CommandContext commandContext)
		{
			return new IsAllDayEventProperty(commandContext);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00020A44 File Offset: 0x0001EC44
		public void Set()
		{
			SetCommandSettings commandSettings = base.GetCommandSettings<SetCommandSettings>();
			bool value = (bool)commandSettings.ServiceObject[this.commandContext.PropertyInformation];
			this.SetProperty(commandSettings.StoreObject, value);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00020A84 File Offset: 0x0001EC84
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			bool value = (bool)setPropertyUpdate.ServiceObject[this.commandContext.PropertyInformation];
			StoreObject storeObject = updateCommandSettings.StoreObject;
			this.SetProperty(storeObject, value);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00020ABC File Offset: 0x0001ECBC
		public void ToXml()
		{
			throw new InvalidOperationException("IsAllDayEventProperty.ToXml should not be called");
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00020AC8 File Offset: 0x0001ECC8
		public void ToXmlForPropertyBag()
		{
			throw new InvalidOperationException("IsAllDayEventProperty.ToXmlForPropertyBag should not be called");
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00020AD4 File Offset: 0x0001ECD4
		private void SetProperty(StoreObject storeObject, bool value)
		{
			storeObject[IsAllDayEventProperty.StorageIsAllDayEventPropDef] = value;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00020AE8 File Offset: 0x0001ECE8
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			StoreObject storeObject = commandSettings.StoreObject;
			if (PropertyCommand.StorePropertyExists(storeObject, IsAllDayEventProperty.MapiIsAllDayEventPropDef))
			{
				bool flag = (bool)storeObject[IsAllDayEventProperty.MapiIsAllDayEventPropDef];
				serviceObject[propertyInformation] = flag;
			}
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00020B44 File Offset: 0x0001ED44
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			bool flag = false;
			if (PropertyCommand.TryGetValueFromPropertyBag<bool>(propertyBag, IsAllDayEventProperty.MapiIsAllDayEventPropDef, out flag))
			{
				serviceObject[propertyInformation] = flag;
			}
		}

		// Token: 0x040006AB RID: 1707
		private static readonly PropertyDefinition MapiIsAllDayEventPropDef = CalendarItemBaseSchema.MapiIsAllDayEvent;

		// Token: 0x040006AC RID: 1708
		private static readonly PropertyDefinition StorageIsAllDayEventPropDef = CalendarItemBaseSchema.IsAllDayEvent;
	}
}
