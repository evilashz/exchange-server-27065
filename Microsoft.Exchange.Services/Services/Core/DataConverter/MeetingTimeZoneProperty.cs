using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000E8 RID: 232
	internal sealed class MeetingTimeZoneProperty : ComplexPropertyBase, IToXmlCommand, IToServiceObjectCommand, ISetCommand, ISetUpdateCommand, IUpdateCommand, IPropertyCommand
	{
		// Token: 0x06000654 RID: 1620 RVA: 0x00020F86 File Offset: 0x0001F186
		private MeetingTimeZoneProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00020F8F File Offset: 0x0001F18F
		public static MeetingTimeZoneProperty CreateCommand(CommandContext commandContext)
		{
			return new MeetingTimeZoneProperty(commandContext);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00020F97 File Offset: 0x0001F197
		public void ToXml()
		{
			throw new InvalidOperationException("MeetingTimeZoneProperty.ToXml should not be called.");
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00020FA4 File Offset: 0x0001F1A4
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010))
			{
				if (commandSettings.ResponseShape != null && commandSettings.ResponseShape.AdditionalProperties != null)
				{
					foreach (PropertyPath propertyPath in commandSettings.ResponseShape.AdditionalProperties)
					{
						PropertyUri propertyUri = propertyPath as PropertyUri;
						if (propertyUri != null && propertyUri.Uri == PropertyUriEnum.MeetingTimeZone)
						{
							throw new InvalidPropertySetException((CoreResources.IDs)3384523424U, this.commandContext.PropertyInformation.PropertyPath);
						}
					}
				}
				ExTraceGlobals.CalendarDataTracer.TraceError((long)this.GetHashCode(), "Property " + PropertyUriEnum.MeetingTimeZone.ToString() + " is deprecated in this mode and is not returned in AllProperties shape");
				return;
			}
			ServiceObject serviceObject = commandSettings.ServiceObject;
			StoreObject storeObject = commandSettings.StoreObject;
			MeetingRequest meetingRequest = storeObject as MeetingRequest;
			if (meetingRequest != null)
			{
				CalendarItemBase cachedEmbeddedItem = ((MeetingRequest)storeObject).GetCachedEmbeddedItem();
				CalendarItem calendarItem = cachedEmbeddedItem as CalendarItem;
				if (calendarItem != null)
				{
					serviceObject[CalendarItemSchema.MeetingTimeZone] = RecurrenceHelper.MeetingTimeZone.Render(calendarItem);
					return;
				}
			}
			else
			{
				serviceObject[CalendarItemSchema.MeetingTimeZone] = RecurrenceHelper.MeetingTimeZone.Render(storeObject as CalendarItem);
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x000210CF File Offset: 0x0001F2CF
		public void Set()
		{
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010))
			{
				throw new InvalidPropertySetException((CoreResources.IDs)3384523424U, this.commandContext.PropertyInformation.PropertyPath);
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00021104 File Offset: 0x0001F304
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010))
			{
				throw new InvalidPropertySetException((CoreResources.IDs)3384523424U, this.commandContext.PropertyInformation.PropertyPath);
			}
			CalendarItem calendarItem = updateCommandSettings.StoreObject as CalendarItem;
			if (calendarItem == null)
			{
				return;
			}
			if (calendarItem.Recurrence != null)
			{
				RecurrenceHelper.Recurrence.CreateAndAssignRecurrence(calendarItem.Recurrence.Pattern, calendarItem.Recurrence.Range, calendarItem.Session.ExTimeZone, calendarItem.Recurrence.ReadExTimeZone, calendarItem);
			}
		}
	}
}
