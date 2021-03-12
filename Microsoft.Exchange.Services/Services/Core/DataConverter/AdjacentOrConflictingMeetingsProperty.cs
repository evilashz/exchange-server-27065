using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000CC RID: 204
	internal sealed class AdjacentOrConflictingMeetingsProperty : ComplexPropertyBase, IToXmlCommand, IToServiceObjectCommand, IPropertyCommand
	{
		// Token: 0x060005A5 RID: 1445 RVA: 0x0001DE45 File Offset: 0x0001C045
		private AdjacentOrConflictingMeetingsProperty(CommandContext commandContext, bool propertyIsAdjacentMeetings, bool propertyIsMeetingCount) : base(commandContext)
		{
			this.propertyIsAdjacentMeetings = propertyIsAdjacentMeetings;
			this.propertyIsMeetingCount = propertyIsMeetingCount;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001DE5C File Offset: 0x0001C05C
		public static AdjacentOrConflictingMeetingsProperty CreateCommandForAdjacentMeetings(CommandContext commandContext)
		{
			return new AdjacentOrConflictingMeetingsProperty(commandContext, true, false);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001DE66 File Offset: 0x0001C066
		public static AdjacentOrConflictingMeetingsProperty CreateCommandForConflictingMeetings(CommandContext commandContext)
		{
			return new AdjacentOrConflictingMeetingsProperty(commandContext, false, false);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001DE70 File Offset: 0x0001C070
		public static AdjacentOrConflictingMeetingsProperty CreateCommandForAdjacentMeetingCount(CommandContext commandContext)
		{
			return new AdjacentOrConflictingMeetingsProperty(commandContext, true, true);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001DE7A File Offset: 0x0001C07A
		public static AdjacentOrConflictingMeetingsProperty CreateCommandForConflictingMeetingCount(CommandContext commandContext)
		{
			return new AdjacentOrConflictingMeetingsProperty(commandContext, false, true);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001DE84 File Offset: 0x0001C084
		public void ToXml()
		{
			throw new InvalidOperationException("AdjacentOrConflictingMeetingsProperty.ToXml should not be called.");
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0001DE90 File Offset: 0x0001C090
		public override bool ToServiceObjectRequiresMailboxAccess
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001DE94 File Offset: 0x0001C094
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			ServiceObject serviceObject = commandSettings.ServiceObject;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			MailboxSession mailboxSession = commandSettings.StoreObject.Session as MailboxSession;
			if (mailboxSession == null)
			{
				return;
			}
			if (commandSettings.StoreObject.Id == null || commandSettings.StoreObject.Id.ObjectId.ProviderLevelItemId.Length == 0)
			{
				return;
			}
			MailboxId calendarItemMailboxOwnerMailboxId = null;
			CalendarItemBase calendarItemBase = commandSettings.StoreObject as CalendarItemBase;
			AdjacencyOrConflictInfo[] adjacentOrConflictingItems;
			if (calendarItemBase == null)
			{
				MeetingRequest meetingRequest = (MeetingRequest)commandSettings.StoreObject;
				adjacentOrConflictingItems = AdjacentOrConflictingMeetingsProperty.GetAdjacentOrConflictingItems(mailboxSession, meetingRequest, out calendarItemMailboxOwnerMailboxId);
			}
			else
			{
				adjacentOrConflictingItems = AdjacentOrConflictingMeetingsProperty.GetAdjacentOrConflictingItems(mailboxSession, calendarItemBase, out calendarItemMailboxOwnerMailboxId);
			}
			int num = 0;
			if (adjacentOrConflictingItems != null)
			{
				foreach (AdjacencyOrConflictInfo adjacencyOrConflictInfo in adjacentOrConflictingItems)
				{
					if (this.propertyIsAdjacentMeetings == (adjacencyOrConflictInfo.AdjacencyOrConflictType == AdjacencyOrConflictType.Precedes || adjacencyOrConflictInfo.AdjacencyOrConflictType == AdjacencyOrConflictType.Follows))
					{
						if (this.propertyIsMeetingCount)
						{
							num++;
						}
						else
						{
							EwsCalendarItemType item = AdjacentOrConflictingMeetingsProperty.CreateCalendarItemType(adjacencyOrConflictInfo, calendarItemMailboxOwnerMailboxId);
							if (!serviceObject.PropertyBag.Contains(propertyInformation))
							{
								serviceObject[propertyInformation] = new NonEmptyArrayOfAllItemsType();
							}
							((NonEmptyArrayOfAllItemsType)serviceObject[propertyInformation]).Add(item);
						}
					}
				}
				if (this.propertyIsMeetingCount)
				{
					serviceObject[propertyInformation] = num;
				}
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0001DFDC File Offset: 0x0001C1DC
		private static AdjacencyOrConflictInfo[] GetAdjacentOrConflictingItems(MailboxSession mailboxSession, MeetingRequest meetingRequest, out MailboxId calendarItemMailboxOwnerMailboxId)
		{
			AdjacencyOrConflictInfo[] result = null;
			calendarItemMailboxOwnerMailboxId = null;
			CalendarItemBase cachedEmbeddedItem = meetingRequest.GetCachedEmbeddedItem();
			if (meetingRequest.IsDelegated())
			{
				using (DelegateSessionHandleWrapper delegateSessionHandle = AdjacentOrConflictingMeetingsProperty.GetDelegateSessionHandle(meetingRequest))
				{
					if (delegateSessionHandle != null)
					{
						result = AdjacentOrConflictingMeetingsProperty.GetAdjacentOrConflictingItemsFromDefaultCalendarFolder(delegateSessionHandle.Handle.MailboxSession, cachedEmbeddedItem);
						calendarItemMailboxOwnerMailboxId = new MailboxId(delegateSessionHandle.Handle.MailboxSession);
					}
					return result;
				}
			}
			result = AdjacentOrConflictingMeetingsProperty.GetAdjacentOrConflictingItemsFromDefaultCalendarFolder(mailboxSession, cachedEmbeddedItem);
			calendarItemMailboxOwnerMailboxId = new MailboxId(mailboxSession);
			return result;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001E05C File Offset: 0x0001C25C
		private static AdjacencyOrConflictInfo[] GetAdjacentOrConflictingItems(MailboxSession mailboxSession, CalendarItemBase calendarItemBase, out MailboxId calendarItemMailboxOwnerMailboxId)
		{
			AdjacencyOrConflictInfo[] result = null;
			using (Folder folder = Folder.Bind(mailboxSession, calendarItemBase.ParentId, null))
			{
				CalendarFolder calendarFolder = folder as CalendarFolder;
				if (calendarFolder != null)
				{
					result = calendarFolder.GetAdjacentOrConflictingItems(calendarItemBase);
				}
				else
				{
					result = AdjacentOrConflictingMeetingsProperty.GetAdjacentOrConflictingItemsFromDefaultCalendarFolder(mailboxSession, calendarItemBase);
				}
			}
			calendarItemMailboxOwnerMailboxId = new MailboxId(mailboxSession);
			return result;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001E0BC File Offset: 0x0001C2BC
		private static EwsCalendarItemType CreateCalendarItemType(AdjacencyOrConflictInfo itemInfo, MailboxId calendarItemMailboxOwnerMailboxId)
		{
			ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(itemInfo.OccurrenceInfo.VersionedId, calendarItemMailboxOwnerMailboxId, null);
			return new EwsCalendarItemType
			{
				ItemId = new ItemId(concatenatedId.Id, concatenatedId.ChangeKey),
				Subject = itemInfo.Subject,
				Start = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(itemInfo.OccurrenceInfo.StartTime),
				End = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(itemInfo.OccurrenceInfo.EndTime),
				LegacyFreeBusyStatusString = BusyTypeConverter.ToString(itemInfo.FreeBusyStatus),
				Location = itemInfo.Location,
				IsAllDayEvent = new bool?(itemInfo.IsAllDayEvent)
			};
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001E164 File Offset: 0x0001C364
		private static AdjacencyOrConflictInfo[] GetAdjacentOrConflictingItemsFromDefaultCalendarFolder(MailboxSession mailboxSession, CalendarItemBase calendarItemBase)
		{
			AdjacencyOrConflictInfo[] result;
			try
			{
				using (CalendarFolder calendarFolder = CalendarFolder.Bind(mailboxSession, DefaultFolderType.Calendar, null))
				{
					result = calendarFolder.GetAdjacentOrConflictingItems(calendarItemBase);
				}
			}
			catch (ObjectNotFoundException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0001E1B0 File Offset: 0x0001C3B0
		private static DelegateSessionHandleWrapper GetDelegateSessionHandle(MeetingMessage meetingMessage)
		{
			MailboxSession mailboxSession = meetingMessage.Session as MailboxSession;
			if (mailboxSession.LogonType != LogonType.Owner)
			{
				return null;
			}
			Participant participant = MailboxHelper.TryConvertTo(meetingMessage.ReceivedRepresenting, "EX");
			if (participant != null && participant.RoutingType == "EX")
			{
				try
				{
					ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromLegacyDN(mailboxSession.MailboxOwner.MailboxInfo.OrganizationId.ToADSessionSettings(), participant.EmailAddress, RemotingOptions.AllowCrossSite);
					return new DelegateSessionHandleWrapper(mailboxSession.GetDelegateSessionHandleForEWS(exchangePrincipal));
				}
				catch (ObjectNotFoundException arg)
				{
					ExTraceGlobals.CalendarDataTracer.TraceWarning<string, ObjectNotFoundException>((long)meetingMessage.GetHashCode(), "Unable to get exchange principal for receivedRepresenting '{0}' . Exception '{1}'.", participant.EmailAddress, arg);
				}
			}
			return null;
		}

		// Token: 0x04000698 RID: 1688
		private const bool IsAdjacentMeetingProperty = true;

		// Token: 0x04000699 RID: 1689
		private const bool IsMeetingCountProperty = true;

		// Token: 0x0400069A RID: 1690
		private bool propertyIsAdjacentMeetings;

		// Token: 0x0400069B RID: 1691
		private bool propertyIsMeetingCount;
	}
}
