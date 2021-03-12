using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000D2 RID: 210
	internal sealed class AllowNewTimeProposalProperty : ComplexPropertyBase, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, ISetCommand, ISetUpdateCommand, IUpdateCommand, IPropertyCommand
	{
		// Token: 0x060005BA RID: 1466 RVA: 0x0001E268 File Offset: 0x0001C468
		private AllowNewTimeProposalProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0001E271 File Offset: 0x0001C471
		public static AllowNewTimeProposalProperty CreateCommand(CommandContext commandContext)
		{
			return new AllowNewTimeProposalProperty(commandContext);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001E27C File Offset: 0x0001C47C
		public void Set()
		{
			SetCommandSettings commandSettings = base.GetCommandSettings<SetCommandSettings>();
			ServiceObject serviceObject = commandSettings.ServiceObject;
			bool allowNewTimeProposal = (bool)serviceObject[this.commandContext.PropertyInformation];
			CalendarItemBase calendarItemBase = commandSettings.StoreObject as CalendarItemBase;
			if (calendarItemBase != null)
			{
				AllowNewTimeProposalProperty.InvertAndSetProperty(calendarItemBase, allowNewTimeProposal);
				return;
			}
			throw new InvalidPropertySetException(this.commandContext.PropertyInformation.PropertyPath);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001E2DC File Offset: 0x0001C4DC
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			ServiceObject serviceObject = setPropertyUpdate.ServiceObject;
			CalendarItemBase calendarItemBase = updateCommandSettings.StoreObject as CalendarItemBase;
			if (calendarItemBase != null)
			{
				bool allowNewTimeProposal = (bool)serviceObject[this.commandContext.PropertyInformation];
				AllowNewTimeProposalProperty.InvertAndSetProperty(calendarItemBase, allowNewTimeProposal);
				return;
			}
			throw new InvalidPropertySetException(setPropertyUpdate.PropertyPath);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0001E329 File Offset: 0x0001C529
		public void ToXml()
		{
			throw new InvalidOperationException("AllowNewTimeZoneProposalProperty.ToXml should not be called.");
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001E338 File Offset: 0x0001C538
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			PropertyDefinition[] propertyDefinitions = this.commandContext.GetPropertyDefinitions();
			if (PropertyCommand.StorePropertyExists(storeObject, propertyDefinitions[0]))
			{
				bool flag = (bool)storeObject[CalendarItemBaseSchema.DisallowNewTimeProposal];
				commandSettings.ServiceObject[CalendarItemSchema.OrganizerSpecific.AllowNewTimeProposal] = !flag;
			}
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001E394 File Offset: 0x0001C594
		public void ToXmlForPropertyBag()
		{
			throw new InvalidOperationException("AllowNewTimeZoneProposalProperty.ToXmlForPropertyBag should not be called.");
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001E3A0 File Offset: 0x0001C5A0
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			ServiceObject serviceObject = commandSettings.ServiceObject;
			bool flag;
			if (PropertyCommand.TryGetValueFromPropertyBag<bool>(commandSettings.PropertyBag, CalendarItemBaseSchema.DisallowNewTimeProposal, out flag))
			{
				serviceObject.PropertyBag[CalendarItemSchema.OrganizerSpecific.AllowNewTimeProposal] = !flag;
			}
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001E3E8 File Offset: 0x0001C5E8
		private static void InvertAndSetProperty(CalendarItemBase calendarItemBase, bool allowNewTimeProposal)
		{
			calendarItemBase[CalendarItemBaseSchema.DisallowNewTimeProposal] = !allowNewTimeProposal;
		}
	}
}
