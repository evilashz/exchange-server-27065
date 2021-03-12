using System;
using System.IO;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000D9 RID: 217
	internal sealed class CalendarItemRecurrenceProperty : ComplexPropertyBase, IToXmlCommand, IToServiceObjectCommand, ISetCommand, ISetUpdateCommand, IDeleteUpdateCommand, IUpdateCommand, IPropertyCommand
	{
		// Token: 0x060005E0 RID: 1504 RVA: 0x0001EC72 File Offset: 0x0001CE72
		private CalendarItemRecurrenceProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001EC7B File Offset: 0x0001CE7B
		public static CalendarItemRecurrenceProperty CreateCommand(CommandContext commandContext)
		{
			return new CalendarItemRecurrenceProperty(commandContext);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001EC83 File Offset: 0x0001CE83
		public void ToXml()
		{
			throw new InvalidOperationException("CalendarItemRecurrenceProperty.ToXml should not be called.");
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001EC90 File Offset: 0x0001CE90
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			ServiceObject serviceObject = commandSettings.ServiceObject;
			StoreObject storeObject = commandSettings.StoreObject;
			MeetingMessage meetingMessage = storeObject as MeetingMessage;
			if (meetingMessage != null)
			{
				CalendarItem calendarItem = ((MeetingMessage)storeObject).GetCachedEmbeddedItem() as CalendarItem;
				serviceObject.PropertyBag[CalendarItemSchema.OrganizerSpecific.Recurrence] = RecurrenceHelper.Recurrence.Render(calendarItem.Recurrence);
				return;
			}
			CalendarItem calendarItem2 = storeObject as CalendarItem;
			if (calendarItem2 != null)
			{
				serviceObject.PropertyBag[CalendarItemSchema.OrganizerSpecific.Recurrence] = RecurrenceHelper.Recurrence.Render(calendarItem2.Recurrence);
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001ED14 File Offset: 0x0001CF14
		public void Set()
		{
			SetCommandSettings commandSettings = base.GetCommandSettings<SetCommandSettings>();
			CalendarItem calendarItem = commandSettings.StoreObject as CalendarItem;
			if (calendarItem != null && calendarItem.Body.Format == BodyFormat.TextHtml)
			{
				string value;
				using (TextReader textReader = calendarItem.Body.OpenTextReader(calendarItem.Body.Format))
				{
					value = textReader.ReadToEnd();
				}
				BodyWriteConfiguration bodyWriteConfiguration = new BodyWriteConfiguration(calendarItem.Body.Format);
				bodyWriteConfiguration.SetTargetFormat(BodyFormat.ApplicationRtf, calendarItem.Body.Charset);
				try
				{
					using (TextWriter textWriter = calendarItem.Body.OpenTextWriter(bodyWriteConfiguration))
					{
						textWriter.Write(value);
					}
				}
				catch (InvalidCharsetException)
				{
					throw new CalendarExceptionInvalidRecurrence();
				}
				catch (TextConvertersException)
				{
					throw new CalendarExceptionInvalidRecurrence();
				}
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001EE04 File Offset: 0x0001D004
		void ISetCommand.SetPhase2()
		{
			this.SetPhase2();
			SetCommandSettings commandSettings = base.GetCommandSettings<SetCommandSettings>();
			CalendarItem calendarItem = commandSettings.StoreObject as CalendarItem;
			if (calendarItem == null)
			{
				throw new InvalidPropertySetException(this.commandContext.PropertyInformation.PropertyPath);
			}
			CalendarItemRecurrenceProperty.SetProperty(calendarItem, (RecurrenceType)commandSettings.ServiceObject[CalendarItemSchema.OrganizerSpecific.Recurrence], false);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001EE60 File Offset: 0x0001D060
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			CalendarItem calendarItem = updateCommandSettings.StoreObject as CalendarItem;
			if (calendarItem == null)
			{
				throw new InvalidPropertySetException(setPropertyUpdate.PropertyPath);
			}
			CalendarItemRecurrenceProperty.SetProperty(calendarItem, (RecurrenceType)setPropertyUpdate.ServiceObject.PropertyBag[CalendarItemSchema.OrganizerSpecific.Recurrence], true);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001EEAC File Offset: 0x0001D0AC
		public override void DeleteUpdate(DeletePropertyUpdate deletePropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			CalendarItem calendarItem = updateCommandSettings.StoreObject as CalendarItem;
			if (calendarItem == null)
			{
				throw new InvalidPropertyDeleteException(deletePropertyUpdate.PropertyPath);
			}
			calendarItem.Recurrence = null;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001EEDB File Offset: 0x0001D0DB
		private static ExTimeZone GetSafeTimeZoneForRecurrence(ExTimeZone timeZone)
		{
			if (timeZone != ExTimeZone.UtcTimeZone)
			{
				return timeZone;
			}
			return EWSSettings.DefaultGmtTimeZone;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001EEEC File Offset: 0x0001D0EC
		private static void SetProperty(CalendarItem calendarItem, RecurrenceType recurrenceType, bool performUpdate)
		{
			calendarItem.Session.ExTimeZone = CalendarItemRecurrenceProperty.GetSafeTimeZoneForRecurrence(calendarItem.Session.ExTimeZone);
			calendarItem.ExTimeZone = calendarItem.Session.ExTimeZone;
			ExTimeZone exTimeZone;
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2007SP1))
			{
				if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010) && performUpdate)
				{
					if (calendarItem.Recurrence == null)
					{
						exTimeZone = calendarItem.StartTimeZone;
					}
					else
					{
						exTimeZone = calendarItem.Recurrence.CreatedExTimeZone;
					}
					exTimeZone = CalendarItemRecurrenceProperty.GetSafeTimeZoneForRecurrence(exTimeZone);
				}
				else
				{
					exTimeZone = calendarItem.Session.ExTimeZone;
				}
			}
			else
			{
				exTimeZone = null;
			}
			Recurrence recurrence;
			if (RecurrenceHelper.Recurrence.Parse(exTimeZone, recurrenceType, out recurrence))
			{
				try
				{
					calendarItem.Recurrence = recurrence;
				}
				catch (InvalidOperationException)
				{
					throw new CalendarExceptionInvalidRecurrence();
				}
				calendarItem[CalendarItemBaseSchema.IsRecurring] = true;
			}
		}
	}
}
