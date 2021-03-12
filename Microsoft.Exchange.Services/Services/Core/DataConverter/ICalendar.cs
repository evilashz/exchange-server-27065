using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000DD RID: 221
	internal sealed class ICalendar
	{
		// Token: 0x0400069E RID: 1694
		private static readonly PropertyDefinition GlobalObjectId = CalendarItemBaseSchema.GlobalObjectId;

		// Token: 0x0400069F RID: 1695
		private static readonly PropertyDefinition CleanGlobalObjectId = CalendarItemBaseSchema.CleanGlobalObjectId;

		// Token: 0x040006A0 RID: 1696
		private static readonly PropertyDefinition StartRecurTime = CalendarItemBaseSchema.StartRecurTime;

		// Token: 0x040006A1 RID: 1697
		private static readonly PropertyDefinition TimeZoneBlob = CalendarItemBaseSchema.TimeZoneBlob;

		// Token: 0x040006A2 RID: 1698
		private static readonly PropertyDefinition TimeZoneDefinitionRecurring = CalendarItemBaseSchema.TimeZoneDefinitionRecurring;

		// Token: 0x040006A3 RID: 1699
		private static readonly PropertyDefinition TimeZoneDefinitionStart = ItemSchema.TimeZoneDefinitionStart;

		// Token: 0x040006A4 RID: 1700
		private static readonly PropertyDefinition OwnerCriticalChangeTime = CalendarItemBaseSchema.OwnerCriticalChangeTime;

		// Token: 0x040006A5 RID: 1701
		private static readonly PropertyDefinition AttendeeCriticalChangeTime = CalendarItemBaseSchema.AttendeeCriticalChangeTime;

		// Token: 0x040006A6 RID: 1702
		private static readonly PropertyDefinition LastModifiedTime = StoreObjectSchema.LastModifiedTime;

		// Token: 0x040006A7 RID: 1703
		private static readonly PropertyDefinition ItemClass = StoreObjectSchema.ItemClass;

		// Token: 0x020000DE RID: 222
		internal sealed class UidProperty : ComplexPropertyBase, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, ISetCommand, ISetUpdateCommand, IUpdateCommand, IPropertyCommand
		{
			// Token: 0x06000608 RID: 1544 RVA: 0x00020071 File Offset: 0x0001E271
			private UidProperty(CommandContext commandContext) : base(commandContext)
			{
			}

			// Token: 0x06000609 RID: 1545 RVA: 0x0002007A File Offset: 0x0001E27A
			public static ICalendar.UidProperty CreateCommand(CommandContext commandContext)
			{
				return new ICalendar.UidProperty(commandContext);
			}

			// Token: 0x0600060A RID: 1546 RVA: 0x00020084 File Offset: 0x0001E284
			public static bool TryGetUidFromStoreObject(StoreObject storeObject, PropertyPath uidPropertyPath, out string uid)
			{
				if (PropertyCommand.StorePropertyExists(storeObject, ICalendar.CleanGlobalObjectId))
				{
					byte[] globalObjectIdBytes = storeObject.TryGetProperty(ICalendar.CleanGlobalObjectId) as byte[];
					uid = ICalendar.UidProperty.GetUidFromGoidBytes(globalObjectIdBytes, uidPropertyPath);
					return true;
				}
				uid = null;
				return false;
			}

			// Token: 0x0600060B RID: 1547 RVA: 0x000200C0 File Offset: 0x0001E2C0
			public static bool TryGetUidFromPropertyBag(IStorePropertyBag propertyBag, out string uid)
			{
				uid = null;
				byte[] array = propertyBag.TryGetProperty(CalendarItemBaseSchema.CleanGlobalObjectId) as byte[];
				if (array == null)
				{
					return false;
				}
				uid = ICalendar.UidProperty.GetUidFromGoidBytes(array, CalendarItemSchema.ICalendarUid.PropertyPath);
				return true;
			}

			// Token: 0x0600060C RID: 1548 RVA: 0x000200FC File Offset: 0x0001E2FC
			public void Set()
			{
				SetCommandSettings commandSettings = base.GetCommandSettings<SetCommandSettings>();
				CalendarItem calendarItem = commandSettings.StoreObject as CalendarItem;
				this.SetProperty(calendarItem, commandSettings.ServiceObject);
			}

			// Token: 0x0600060D RID: 1549 RVA: 0x0002012C File Offset: 0x0001E32C
			public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
			{
				ServiceObject serviceObject = setPropertyUpdate.ServiceObject;
				CalendarItem calendarItem = updateCommandSettings.StoreObject as CalendarItem;
				if (calendarItem == null)
				{
					throw new InvalidPropertySetException((CoreResources.IDs)4292861306U, updateCommandSettings.PropertyUpdate.PropertyPath);
				}
				this.SetProperty(calendarItem, serviceObject);
			}

			// Token: 0x0600060E RID: 1550 RVA: 0x00020172 File Offset: 0x0001E372
			public void ToXml()
			{
				throw new InvalidOperationException("UidProperty.ToXml should not be called.");
			}

			// Token: 0x0600060F RID: 1551 RVA: 0x0002017E File Offset: 0x0001E37E
			public void ToXmlForPropertyBag()
			{
				throw new InvalidOperationException("UidProperty.ToXmlForPropertyBag should not be called.");
			}

			// Token: 0x06000610 RID: 1552 RVA: 0x0002018C File Offset: 0x0001E38C
			public void ToServiceObject()
			{
				ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
				StoreObject storeObject = commandSettings.StoreObject;
				ServiceObject serviceObject = commandSettings.ServiceObject;
				string value;
				if (ICalendar.UidProperty.TryGetUidFromStoreObject(storeObject, this.commandContext.PropertyInformation.PropertyPath, out value))
				{
					serviceObject[this.commandContext.PropertyInformation] = value;
				}
			}

			// Token: 0x06000611 RID: 1553 RVA: 0x000201DC File Offset: 0x0001E3DC
			public void ToServiceObjectForPropertyBag()
			{
				ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
				IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
				ServiceObject serviceObject = commandSettings.ServiceObject;
				byte[] globalObjectIdBytes;
				if (PropertyCommand.TryGetValueFromPropertyBag<byte[]>(propertyBag, ICalendar.CleanGlobalObjectId, out globalObjectIdBytes))
				{
					serviceObject[this.commandContext.PropertyInformation] = ICalendar.UidProperty.GetUidFromGoidBytes(globalObjectIdBytes, this.commandContext.PropertyInformation.PropertyPath);
				}
			}

			// Token: 0x06000612 RID: 1554 RVA: 0x00020234 File Offset: 0x0001E434
			private void SetProperty(CalendarItem calendarItem, ServiceObject serviceObject)
			{
				string valueOrDefault = serviceObject.GetValueOrDefault<string>(this.commandContext.PropertyInformation);
				try
				{
					GlobalObjectId globalObjectId = new GlobalObjectId(valueOrDefault);
					calendarItem[ICalendar.GlobalObjectId] = globalObjectId.Bytes;
					calendarItem[ICalendar.CleanGlobalObjectId] = globalObjectId.Bytes;
				}
				catch (Exception innerException)
				{
					throw new CalendarExceptionInvalidPropertyValue(this.commandContext.PropertyInformation.PropertyPath, innerException);
				}
			}

			// Token: 0x06000613 RID: 1555 RVA: 0x000202A8 File Offset: 0x0001E4A8
			private static string GetUidFromGoidBytes(byte[] globalObjectIdBytes, PropertyPath uidPropertyPath)
			{
				string uid;
				try
				{
					GlobalObjectId globalObjectId = new GlobalObjectId(globalObjectIdBytes);
					uid = globalObjectId.Uid;
				}
				catch (Exception innerException)
				{
					throw new CalendarExceptionInvalidPropertyValue(uidPropertyPath, innerException);
				}
				return uid;
			}

			// Token: 0x040006A8 RID: 1704
			public static readonly PropertyDefinition PropertyToLoad = ICalendar.CleanGlobalObjectId;
		}

		// Token: 0x020000DF RID: 223
		internal sealed class RecurrenceIdProperty : ComplexPropertyBase, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, IPropertyCommand
		{
			// Token: 0x06000615 RID: 1557 RVA: 0x000202EC File Offset: 0x0001E4EC
			private RecurrenceIdProperty(CommandContext commandContext) : base(commandContext)
			{
			}

			// Token: 0x06000616 RID: 1558 RVA: 0x000202F5 File Offset: 0x0001E4F5
			public static ICalendar.RecurrenceIdProperty CreateCommand(CommandContext commandContext)
			{
				return new ICalendar.RecurrenceIdProperty(commandContext);
			}

			// Token: 0x06000617 RID: 1559 RVA: 0x000202FD File Offset: 0x0001E4FD
			public void ToXml()
			{
				throw new InvalidOperationException("RecurrenceIdProperty.ToXml should not be called.");
			}

			// Token: 0x06000618 RID: 1560 RVA: 0x00020309 File Offset: 0x0001E509
			public void ToXmlForPropertyBag()
			{
				throw new InvalidOperationException("RecurrenceIdProperty.ToXmlForPropertyBag should not be called.");
			}

			// Token: 0x06000619 RID: 1561 RVA: 0x00020318 File Offset: 0x0001E518
			public void ToServiceObject()
			{
				ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
				StoreObject storeObject = commandSettings.StoreObject;
				ServiceObject serviceObject = commandSettings.ServiceObject;
				ExTimeZone recurringTimeZoneFromPropertyBag = TimeZoneHelper.GetRecurringTimeZoneFromPropertyBag(storeObject.PropertyBag);
				if (recurringTimeZoneFromPropertyBag == null)
				{
					byte[] valueOrDefault = storeObject.PropertyBag.GetValueOrDefault<byte[]>(ICalendar.TimeZoneDefinitionStart);
					if (!O12TimeZoneFormatter.TryParseTimeZoneBlob(valueOrDefault, string.Empty, out recurringTimeZoneFromPropertyBag))
					{
						return;
					}
				}
				if (PropertyCommand.StorePropertyExists(storeObject, ICalendar.GlobalObjectId) && PropertyCommand.StorePropertyExists(storeObject, ICalendar.StartRecurTime))
				{
					byte[] globalObjectIdBytes = storeObject.TryGetProperty(ICalendar.GlobalObjectId) as byte[];
					int occurrenceScdTime = (int)storeObject.TryGetProperty(ICalendar.StartRecurTime);
					this.RenderToServiceObject(recurringTimeZoneFromPropertyBag, serviceObject, globalObjectIdBytes, occurrenceScdTime);
				}
			}

			// Token: 0x0600061A RID: 1562 RVA: 0x000203B8 File Offset: 0x0001E5B8
			public void ToServiceObjectForPropertyBag()
			{
				ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
				ServiceObject serviceObject = commandSettings.ServiceObject;
				IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
				ExTimeZone exTimeZone = null;
				byte[] bytes;
				if (PropertyCommand.TryGetValueFromPropertyBag<byte[]>(propertyBag, ICalendar.TimeZoneDefinitionRecurring, out bytes))
				{
					O12TimeZoneFormatter.TryParseTimeZoneBlob(bytes, string.Empty, out exTimeZone);
				}
				else if (PropertyCommand.TryGetValueFromPropertyBag<byte[]>(propertyBag, ICalendar.TimeZoneBlob, out bytes))
				{
					O11TimeZoneFormatter.TryParseTimeZoneBlob(bytes, string.Empty, out exTimeZone);
				}
				else if (PropertyCommand.TryGetValueFromPropertyBag<byte[]>(propertyBag, ICalendar.TimeZoneDefinitionStart, out bytes))
				{
					O12TimeZoneFormatter.TryParseTimeZoneBlob(bytes, string.Empty, out exTimeZone);
				}
				byte[] globalObjectIdBytes;
				int occurrenceScdTime;
				if (exTimeZone != null && PropertyCommand.TryGetValueFromPropertyBag<byte[]>(propertyBag, ICalendar.GlobalObjectId, out globalObjectIdBytes) && PropertyCommand.TryGetValueFromPropertyBag<int>(propertyBag, ICalendar.StartRecurTime, out occurrenceScdTime))
				{
					this.RenderToServiceObject(exTimeZone, serviceObject, globalObjectIdBytes, occurrenceScdTime);
				}
			}

			// Token: 0x0600061B RID: 1563 RVA: 0x00020467 File Offset: 0x0001E667
			private static TimeSpan ConvertSCDTimeToTimeSpan(int scdTime)
			{
				return new TimeSpan(scdTime >> 12 & 31, scdTime >> 6 & 63, scdTime & 63);
			}

			// Token: 0x0600061C RID: 1564 RVA: 0x00020480 File Offset: 0x0001E680
			private void RenderToServiceObject(ExTimeZone timeZone, ServiceObject serviceObject, byte[] globalObjectIdBytes, int occurrenceScdTime)
			{
				string value = null;
				try
				{
					GlobalObjectId globalObjectId = new GlobalObjectId(globalObjectIdBytes);
					ExDateTime exDateTime = globalObjectId.Date;
					if (exDateTime != ExDateTime.MinValue)
					{
						exDateTime += ICalendar.RecurrenceIdProperty.ConvertSCDTimeToTimeSpan(occurrenceScdTime);
						if (timeZone != null)
						{
							exDateTime = new ExDateTime(timeZone, exDateTime.Year, exDateTime.Month, exDateTime.Day, exDateTime.Hour, exDateTime.Minute, exDateTime.Second, exDateTime.Millisecond);
						}
						value = ExDateTimeConverter.ToUtcXsdDateTime(exDateTime);
					}
				}
				catch (Exception innerException)
				{
					throw new CalendarExceptionInvalidPropertyValue(this.commandContext.PropertyInformation.PropertyPath, innerException);
				}
				serviceObject[this.commandContext.PropertyInformation] = value;
			}

			// Token: 0x040006A9 RID: 1705
			public static readonly PropertyDefinition[] PropertiesToLoad = new PropertyDefinition[]
			{
				ICalendar.GlobalObjectId,
				ICalendar.StartRecurTime,
				ICalendar.TimeZoneBlob,
				ICalendar.TimeZoneDefinitionRecurring,
				ICalendar.TimeZoneDefinitionStart
			};
		}

		// Token: 0x020000E0 RID: 224
		internal sealed class DateTimeStampProperty : ComplexPropertyBase, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, IPropertyCommand
		{
			// Token: 0x0600061E RID: 1566 RVA: 0x0002057A File Offset: 0x0001E77A
			private DateTimeStampProperty(CommandContext commandContext) : base(commandContext)
			{
			}

			// Token: 0x0600061F RID: 1567 RVA: 0x00020583 File Offset: 0x0001E783
			public static ICalendar.DateTimeStampProperty CreateCommand(CommandContext commandContext)
			{
				return new ICalendar.DateTimeStampProperty(commandContext);
			}

			// Token: 0x06000620 RID: 1568 RVA: 0x0002058B File Offset: 0x0001E78B
			public void ToXml()
			{
				throw new InvalidOperationException("DateTimeStampProperty.ToXml should not be called.");
			}

			// Token: 0x06000621 RID: 1569 RVA: 0x00020597 File Offset: 0x0001E797
			public void ToXmlForPropertyBag()
			{
				throw new InvalidOperationException("DateTimeStampProperty.ToXmlForPropertyBag should not be called.");
			}

			// Token: 0x06000622 RID: 1570 RVA: 0x000205A4 File Offset: 0x0001E7A4
			public void ToServiceObject()
			{
				ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
				StoreObject storeObject = commandSettings.StoreObject;
				ExDateTime? valueAsNullable;
				if (ObjectClass.IsMeetingResponse(storeObject.ClassName))
				{
					valueAsNullable = storeObject.GetValueAsNullable<ExDateTime>(ICalendar.AttendeeCriticalChangeTime);
				}
				else
				{
					valueAsNullable = storeObject.GetValueAsNullable<ExDateTime>(ICalendar.OwnerCriticalChangeTime);
				}
				if (valueAsNullable == null)
				{
					valueAsNullable = storeObject.GetValueAsNullable<ExDateTime>(ICalendar.LastModifiedTime);
				}
				if (valueAsNullable == null)
				{
					valueAsNullable = new ExDateTime?(ExDateTime.UtcNow);
				}
				this.RenderToServiceObject(commandSettings.ServiceObject, valueAsNullable.Value);
			}

			// Token: 0x06000623 RID: 1571 RVA: 0x00020624 File Offset: 0x0001E824
			public void ToServiceObjectForPropertyBag()
			{
				ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
				IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
				string itemClass;
				if (PropertyCommand.TryGetValueFromPropertyBag<string>(propertyBag, ICalendar.ItemClass, out itemClass))
				{
					ExDateTime? exDateTime = null;
					if (ObjectClass.IsMeetingResponse(itemClass))
					{
						PropertyCommand.TryGetValueFromPropertyBag<ExDateTime?>(propertyBag, ICalendar.AttendeeCriticalChangeTime, out exDateTime);
					}
					else
					{
						PropertyCommand.TryGetValueFromPropertyBag<ExDateTime?>(propertyBag, ICalendar.OwnerCriticalChangeTime, out exDateTime);
					}
					if (exDateTime == null)
					{
						PropertyCommand.TryGetValueFromPropertyBag<ExDateTime?>(propertyBag, ICalendar.LastModifiedTime, out exDateTime);
					}
					if (exDateTime == null)
					{
						exDateTime = new ExDateTime?(ExDateTime.UtcNow);
					}
					this.RenderToServiceObject(commandSettings.ServiceObject, exDateTime.Value);
				}
			}

			// Token: 0x06000624 RID: 1572 RVA: 0x000206C0 File Offset: 0x0001E8C0
			private void RenderToServiceObject(ServiceObject serviceObject, ExDateTime dateTime)
			{
				serviceObject[this.commandContext.PropertyInformation] = ExDateTimeConverter.ToUtcXsdDateTime(dateTime);
			}

			// Token: 0x040006AA RID: 1706
			public static readonly PropertyDefinition[] PropertiesToLoad = new PropertyDefinition[]
			{
				ICalendar.OwnerCriticalChangeTime,
				ICalendar.AttendeeCriticalChangeTime,
				ICalendar.LastModifiedTime,
				ICalendar.ItemClass
			};
		}
	}
}
