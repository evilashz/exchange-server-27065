using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000F2 RID: 242
	internal sealed class TimeZoneProperty : ComplexPropertyBase, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, ISetCommand, ISetUpdateCommand, IUpdateCommand, IPropertyCommand
	{
		// Token: 0x060006BA RID: 1722 RVA: 0x0002262C File Offset: 0x0002082C
		private TimeZoneProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00022635 File Offset: 0x00020835
		private TimeZoneProperty(CommandContext commandContext, bool startProperty) : base(commandContext)
		{
			this.propertyIsStartTimeZone = startProperty;
			this.propertyInfo = (startProperty ? CalendarItemSchema.OrganizerSpecific.StartTimeZone : CalendarItemSchema.OrganizerSpecific.EndTimeZone);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0002265A File Offset: 0x0002085A
		public static TimeZoneProperty CreateCommandForStart(CommandContext commandContext)
		{
			return new TimeZoneProperty(commandContext, true);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00022663 File Offset: 0x00020863
		public static TimeZoneProperty CreateCommandForEnd(CommandContext commandContext)
		{
			return new TimeZoneProperty(commandContext, false);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0002266C File Offset: 0x0002086C
		public void ToXml()
		{
			throw new InvalidOperationException("TimeZoneProperty.ToXml should not be called.");
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00022678 File Offset: 0x00020878
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			MeetingRequest meetingRequest = storeObject as MeetingRequest;
			ExTimeZone timeZoneToRender;
			if (meetingRequest != null)
			{
				CalendarItemBase cachedEmbeddedItem = ((MeetingRequest)storeObject).GetCachedEmbeddedItem();
				timeZoneToRender = this.GetTimeZoneToRender(cachedEmbeddedItem);
			}
			else
			{
				CalendarItemBase calendarItemBase = storeObject as CalendarItemBase;
				timeZoneToRender = this.GetTimeZoneToRender(calendarItemBase);
			}
			if (timeZoneToRender != null)
			{
				TimeZoneDefinitionType timeZoneDefinitionType = new TimeZoneDefinitionType(timeZoneToRender);
				timeZoneDefinitionType.Render(true, EWSSettings.ClientCulture);
				commandSettings.ServiceObject.PropertyBag[this.propertyInfo] = timeZoneDefinitionType;
			}
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x000226F8 File Offset: 0x000208F8
		public void ToXmlForPropertyBag()
		{
			throw new InvalidOperationException("TimeZoneProperty.ToXmlForPropertyBag should not be called.");
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00022704 File Offset: 0x00020904
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			bool flag;
			if (this.propertyIsStartTimeZone && PropertyCommand.TryGetValueFromPropertyBag<bool>(propertyBag, CalendarItemBaseSchema.IsRecurring, out flag) && flag)
			{
				return;
			}
			byte[] bytes;
			if (PropertyCommand.TryGetValueFromPropertyBag<byte[]>(propertyBag, this.propertyIsStartTimeZone ? TimeZoneProperty.StartTimeZonePropertyDefinition : TimeZoneProperty.EndTimeZonePropertyDefinition, out bytes))
			{
				ExTimeZone utcTimeZone;
				if (!O12TimeZoneFormatter.TryParseTimeZoneBlob(bytes, string.Empty, out utcTimeZone))
				{
					utcTimeZone = ExTimeZone.UtcTimeZone;
				}
				TimeZoneDefinitionType timeZoneDefinitionType = new TimeZoneDefinitionType(utcTimeZone);
				timeZoneDefinitionType.Render(true, EWSSettings.ClientCulture);
				commandSettings.ServiceObject.PropertyBag[this.propertyInfo] = timeZoneDefinitionType;
			}
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0002279C File Offset: 0x0002099C
		public void Set()
		{
			SetCommandSettings commandSettings = base.GetCommandSettings<SetCommandSettings>();
			TimeZoneDefinitionType timeZone = (TimeZoneDefinitionType)commandSettings.ServiceObject.PropertyBag[this.propertyInfo];
			CalendarItemBase calendarItemBase = (CalendarItemBase)commandSettings.StoreObject;
			this.SetProperty(calendarItemBase, timeZone);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x000227E0 File Offset: 0x000209E0
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			TimeZoneDefinitionType timeZone = (TimeZoneDefinitionType)setPropertyUpdate.ServiceObject.PropertyBag[this.propertyInfo];
			CalendarItemBase calendarItemBase = (CalendarItemBase)updateCommandSettings.StoreObject;
			this.SetProperty(calendarItemBase, timeZone);
			CalendarItem calendarItem = updateCommandSettings.StoreObject as CalendarItem;
			if (calendarItem != null && this.propertyIsStartTimeZone && calendarItem.Recurrence != null)
			{
				RecurrenceHelper.Recurrence.CreateAndAssignRecurrence(calendarItem.Recurrence.Pattern, calendarItem.Recurrence.Range, calendarItem.StartTimeZone, calendarItem.Recurrence.ReadExTimeZone, calendarItem);
			}
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0002286C File Offset: 0x00020A6C
		private void SetProperty(CalendarItemBase calendarItemBase, TimeZoneDefinitionType timeZone)
		{
			if (this.propertyIsStartTimeZone)
			{
				if (PropertyCommand.StorePropertyExists(calendarItemBase, CalendarItemInstanceSchema.StartTime))
				{
					ExDateTime exDateTime = calendarItemBase.StartTimeZone.ConvertDateTime(calendarItemBase.StartTime);
					calendarItemBase.StartTime = timeZone.ExTimeZone.Assign(exDateTime);
				}
				else
				{
					calendarItemBase.StartTimeZone = timeZone.ExTimeZone;
				}
				calendarItemBase.Session.ExTimeZone = calendarItemBase.StartTimeZone;
				return;
			}
			if (PropertyCommand.StorePropertyExists(calendarItemBase, CalendarItemInstanceSchema.EndTime))
			{
				ExDateTime exDateTime = calendarItemBase.EndTimeZone.ConvertDateTime(calendarItemBase.EndTime);
				calendarItemBase.EndTime = timeZone.ExTimeZone.Assign(exDateTime);
				return;
			}
			calendarItemBase.EndTimeZone = timeZone.ExTimeZone;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00022910 File Offset: 0x00020B10
		private ExTimeZone GetTimeZoneToRender(CalendarItemBase calendarItemBase)
		{
			if (calendarItemBase == null)
			{
				return null;
			}
			if (!this.propertyIsStartTimeZone)
			{
				return calendarItemBase.EndTimeZone;
			}
			CalendarItem calendarItem = calendarItemBase as CalendarItem;
			if (calendarItem != null && calendarItem.Recurrence != null && calendarItem.Recurrence.HasTimeZone)
			{
				return calendarItem.Recurrence.CreatedExTimeZone;
			}
			return calendarItemBase.StartTimeZone;
		}

		// Token: 0x040006C6 RID: 1734
		public static readonly PropertyDefinition StartTimeZonePropertyDefinition = ItemSchema.TimeZoneDefinitionStart;

		// Token: 0x040006C7 RID: 1735
		public static readonly PropertyDefinition EndTimeZonePropertyDefinition = CalendarItemBaseSchema.TimeZoneDefinitionEnd;

		// Token: 0x040006C8 RID: 1736
		private bool propertyIsStartTimeZone;

		// Token: 0x040006C9 RID: 1737
		private PropertyInformation propertyInfo;
	}
}
