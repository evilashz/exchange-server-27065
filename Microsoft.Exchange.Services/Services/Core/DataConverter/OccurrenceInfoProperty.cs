using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000EA RID: 234
	internal class OccurrenceInfoProperty : ComplexPropertyBase, IToXmlCommand, IToServiceObjectCommand, IPropertyCommand
	{
		// Token: 0x0600065F RID: 1631 RVA: 0x00021221 File Offset: 0x0001F421
		private OccurrenceInfoProperty(CommandContext commandContext, OccurrenceInfoProperty.PropertyCommandKind propertyType) : base(commandContext)
		{
			this.propertyCommandKind = propertyType;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00021231 File Offset: 0x0001F431
		public static OccurrenceInfoProperty CreateCommandForFirstOccurrence(CommandContext commandContext)
		{
			return new OccurrenceInfoProperty(commandContext, OccurrenceInfoProperty.PropertyCommandKind.FirstOccurrence);
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0002123A File Offset: 0x0001F43A
		public static OccurrenceInfoProperty CreateCommandForLastOccurrence(CommandContext commandContext)
		{
			return new OccurrenceInfoProperty(commandContext, OccurrenceInfoProperty.PropertyCommandKind.LastOccurrence);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00021243 File Offset: 0x0001F443
		public static OccurrenceInfoProperty CreateCommandForModifiedOccurrences(CommandContext commandContext)
		{
			return new OccurrenceInfoProperty(commandContext, OccurrenceInfoProperty.PropertyCommandKind.ModifiedOccurrences);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0002124C File Offset: 0x0001F44C
		public static OccurrenceInfoProperty CreateCommandForDeletedOccurrences(CommandContext commandContext)
		{
			return new OccurrenceInfoProperty(commandContext, OccurrenceInfoProperty.PropertyCommandKind.DeletedOccurrences);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00021255 File Offset: 0x0001F455
		public void ToXml()
		{
			throw new InvalidOperationException("OccurrenceInfoProperty.ToXml should not be called");
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00021264 File Offset: 0x0001F464
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			ServiceObject serviceObject = commandSettings.ServiceObject;
			IdAndSession idAndSession = commandSettings.IdAndSession;
			StoreObject storeObject = commandSettings.StoreObject;
			MeetingRequest meetingRequest = storeObject as MeetingRequest;
			if (meetingRequest != null)
			{
				CalendarItem calendarItem = meetingRequest.GetCachedEmbeddedItem() as CalendarItem;
				this.Render(serviceObject, idAndSession, calendarItem);
				return;
			}
			this.Render(serviceObject, idAndSession, storeObject as CalendarItem);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x000212C0 File Offset: 0x0001F4C0
		private void Render(ServiceObject serviceObject, IdAndSession storeIdAndSession, CalendarItem calendarItem)
		{
			if (calendarItem == null)
			{
				return;
			}
			if (calendarItem.Recurrence == null)
			{
				return;
			}
			switch (this.propertyCommandKind)
			{
			case OccurrenceInfoProperty.PropertyCommandKind.FirstOccurrence:
				serviceObject.PropertyBag[CalendarItemSchema.FirstOccurrence] = this.RenderOccurrenceInfo(storeIdAndSession, (calendarItem.Recurrence != null) ? calendarItem.Recurrence.GetFirstOccurrence() : null);
				return;
			case OccurrenceInfoProperty.PropertyCommandKind.LastOccurrence:
				if (calendarItem.Recurrence.Range is EndDateRecurrenceRange || calendarItem.Recurrence.Range is NumberedRecurrenceRange)
				{
					serviceObject.PropertyBag[CalendarItemSchema.LastOccurrence] = this.RenderOccurrenceInfo(storeIdAndSession, calendarItem.Recurrence.GetLastOccurrence());
					return;
				}
				break;
			case OccurrenceInfoProperty.PropertyCommandKind.ModifiedOccurrences:
				if (calendarItem.Recurrence != null)
				{
					IList<OccurrenceInfo> modifiedOccurrences = calendarItem.Recurrence.GetModifiedOccurrences();
					if (modifiedOccurrences.Count > 0)
					{
						OccurrenceInfoType[] array = new OccurrenceInfoType[modifiedOccurrences.Count];
						for (int i = 0; i < modifiedOccurrences.Count; i++)
						{
							array[i] = this.RenderOccurrenceInfo(storeIdAndSession, modifiedOccurrences[i]);
						}
						serviceObject.PropertyBag[CalendarItemSchema.ModifiedOccurrences] = array;
					}
				}
				break;
			case OccurrenceInfoProperty.PropertyCommandKind.DeletedOccurrences:
				if (calendarItem.Recurrence != null)
				{
					ExDateTime[] deletedOccurrences = calendarItem.Recurrence.GetDeletedOccurrences();
					if (deletedOccurrences.Length > 0)
					{
						DeletedOccurrenceInfoType[] array2 = new DeletedOccurrenceInfoType[deletedOccurrences.Length];
						for (int j = 0; j < deletedOccurrences.Length; j++)
						{
							array2[j] = new DeletedOccurrenceInfoType
							{
								Start = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(deletedOccurrences[j])
							};
						}
						serviceObject.PropertyBag[CalendarItemSchema.DeletedOccurrences] = array2;
						return;
					}
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00021448 File Offset: 0x0001F648
		private OccurrenceInfoType RenderOccurrenceInfo(IdAndSession storeIdAndSession, OccurrenceInfo occurrenceInfo)
		{
			ItemId itemId;
			if (storeIdAndSession.Session is PublicFolderSession)
			{
				ConcatenatedIdAndChangeKey concatenatedIdForPublicFolderItem = IdConverter.GetConcatenatedIdForPublicFolderItem(occurrenceInfo.VersionedId, StoreId.GetStoreObjectId(storeIdAndSession.ParentFolderId), null);
				itemId = new ItemId(concatenatedIdForPublicFolderItem.Id, concatenatedIdForPublicFolderItem.ChangeKey);
			}
			else
			{
				itemId = IdConverter.ConvertStoreItemIdToItemId(occurrenceInfo.VersionedId, storeIdAndSession.Session);
			}
			return new OccurrenceInfoType
			{
				ItemId = itemId,
				Start = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(occurrenceInfo.StartTime),
				End = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(occurrenceInfo.EndTime),
				OriginalStart = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(occurrenceInfo.OriginalStartTime)
			};
		}

		// Token: 0x040006B4 RID: 1716
		private const string XmlElementNameDeletedOccurrence = "DeletedOccurrence";

		// Token: 0x040006B5 RID: 1717
		private const string XmlElementNameOccurrence = "Occurrence";

		// Token: 0x040006B6 RID: 1718
		private const string XmlElementNameStart = "Start";

		// Token: 0x040006B7 RID: 1719
		private const string XmlElementNameEnd = "End";

		// Token: 0x040006B8 RID: 1720
		private const string XmlElementNameOriginalStart = "OriginalStart";

		// Token: 0x040006B9 RID: 1721
		private OccurrenceInfoProperty.PropertyCommandKind propertyCommandKind;

		// Token: 0x020000EB RID: 235
		private enum PropertyCommandKind
		{
			// Token: 0x040006BB RID: 1723
			FirstOccurrence,
			// Token: 0x040006BC RID: 1724
			LastOccurrence,
			// Token: 0x040006BD RID: 1725
			ModifiedOccurrences,
			// Token: 0x040006BE RID: 1726
			DeletedOccurrences
		}
	}
}
