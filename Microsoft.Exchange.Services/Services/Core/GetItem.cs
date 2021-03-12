using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000312 RID: 786
	internal sealed class GetItem : MultiStepServiceCommand<GetItemRequest, ItemType[]>, IDisposable
	{
		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600162A RID: 5674 RVA: 0x00072D61 File Offset: 0x00070F61
		internal override bool SupportsExternalUsers
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x00072D64 File Offset: 0x00070F64
		internal override Offer ExpectedOffer
		{
			get
			{
				return Offer.SharingRead;
			}
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x00072D6C File Offset: 0x00070F6C
		public GetItem(CallContext callContext, GetItemRequest request) : base(callContext, request)
		{
			this.itemIds = base.Request.Ids;
			this.responseShape = Global.ResponseShapeResolver.GetResponseShape<ItemResponseShape>(base.Request.ShapeName, base.Request.ItemShape, base.CallContext.FeaturesManager);
			ServiceCommandBase.ThrowIfNullOrEmpty<BaseItemId>(this.itemIds, "itemIds", "GetItem::PreExecuteCommand");
			ServiceCommandBase.ThrowIfNull(this.responseShape, "this.responseShape", "GetItem::PreExecuteCommand");
			RequestDetailsLogger.Current.AppendGenericInfo("Count", this.itemIds.Count);
			if (request.PrefetchItems)
			{
				RequestDetailsLogger.Current.AppendGenericInfo("Prefetch", "1");
			}
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x00072E38 File Offset: 0x00071038
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x00072FC0 File Offset: 0x000711C0
		internal override ServiceResult<ItemType[]> Execute()
		{
			ServiceResult<ItemType[]> serviceResult = null;
			GrayException.MapAndReportGrayExceptions(delegate()
			{
				ServiceError serviceError = null;
				Item item = null;
				try
				{
					IdAndSession idAndSession = this.IdConverter.ConvertItemIdToIdAndSessionReadOnly(this.itemIds[this.CurrentStep], BasicTypes.Item, false, ref item);
					if (this.CurrentStep == 0 && this.Request.PrefetchItems)
					{
						this.PrefetchItems(idAndSession, this.Request.PrefetchItemStoreIds);
					}
					List<ItemType> list = new List<ItemType>(1);
					list.Add(this.GetItemObject(idAndSession, ref item, out serviceError));
					if (this.itemIds[this.CurrentStep] is RecurringMasterItemIdRanges && serviceError == null)
					{
						this.EnumerateAndGetOccurrences((RecurringMasterItemIdRanges)this.itemIds[this.CurrentStep], item, out serviceError, list);
					}
					this.objectsChanged++;
					ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(2969972029U, list[0].ItemId.Id);
					if (serviceError == null)
					{
						serviceResult = new ServiceResult<ItemType[]>(list.ToArray());
					}
					else
					{
						serviceResult = new ServiceResult<ItemType[]>(list.ToArray(), serviceError);
					}
				}
				finally
				{
					if (item != null)
					{
						item.Dispose();
					}
				}
			}, new GrayException.IsGrayExceptionDelegate(GrayException.IsSystemGrayException));
			return serviceResult;
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x00073004 File Offset: 0x00071204
		private static void FindMinMax(RecurringMasterItemIdRanges.OccurrencesRange[] ranges, out ExDateTime minStart, out ExDateTime maxEnd)
		{
			minStart = ExDateTime.MaxValue;
			maxEnd = ExDateTime.MinValue;
			if (ranges != null && ranges.Length > 0)
			{
				foreach (RecurringMasterItemIdRanges.OccurrencesRange occurrencesRange in ranges)
				{
					if (occurrencesRange.Count > 0)
					{
						if (minStart > occurrencesRange.StartExDateTime)
						{
							minStart = occurrencesRange.StartExDateTime;
						}
						if (maxEnd < occurrencesRange.EndExDateTime)
						{
							maxEnd = occurrencesRange.EndExDateTime;
						}
					}
				}
			}
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x0007308C File Offset: 0x0007128C
		private bool IsAtLeastOneValidRange(RecurringMasterItemIdRanges.OccurrencesRange[] ranges)
		{
			bool result = false;
			foreach (RecurringMasterItemIdRanges.OccurrencesRange occurrencesRange in ranges)
			{
				if (occurrencesRange.StartExDateTime.CompareTo(occurrencesRange.EndExDateTime) <= 0)
				{
					result = true;
				}
				else
				{
					occurrencesRange.Count = 0;
				}
			}
			return result;
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x000730D4 File Offset: 0x000712D4
		private bool IsOccurrenceInRanges(OccurrenceInfo occurrence, RecurringMasterItemIdRanges.OccurrencesRange[] ranges)
		{
			bool flag = false;
			foreach (RecurringMasterItemIdRanges.OccurrencesRange occurrencesRange in ranges)
			{
				if (occurrencesRange.Count > 0)
				{
					bool flag2;
					if (occurrencesRange.CompareOriginalStartTime)
					{
						flag2 = (occurrencesRange.StartExDateTime.CompareTo(occurrence.OriginalStartTime) <= 0 && occurrencesRange.EndExDateTime.CompareTo(occurrence.OriginalStartTime) >= 0);
					}
					else
					{
						flag2 = (occurrencesRange.StartExDateTime.CompareTo(occurrence.EndTime) <= 0 && occurrencesRange.EndExDateTime.CompareTo(occurrence.StartTime) >= 0);
					}
					if (flag2)
					{
						occurrencesRange.Count--;
					}
					flag = (flag || flag2);
				}
			}
			return flag;
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x00073228 File Offset: 0x00071428
		private void EnumerateAndGetOccurrences(RecurringMasterItemIdRanges rangesInfo, Item recurringMaster, out ServiceError warning, List<ItemType> itemsList)
		{
			warning = null;
			CalendarItem calendarItem = recurringMaster as CalendarItem;
			if (calendarItem == null || calendarItem.Recurrence == null)
			{
				return;
			}
			RecurringMasterItemIdRanges.OccurrencesRange[] ranges;
			Func<IList<OccurrenceInfo>> getOccurrences;
			if (rangesInfo.Ranges != null)
			{
				ranges = rangesInfo.Ranges;
				getOccurrences = delegate()
				{
					ExDateTime startView;
					ExDateTime endView;
					GetItem.FindMinMax(ranges, out startView, out endView);
					return calendarItem.Recurrence.GetOccurrenceInfoList(startView, endView, 732);
				};
			}
			else
			{
				if (rangesInfo.ExpandAroundDateOccurrenceRange == null)
				{
					return;
				}
				ranges = new RecurringMasterItemIdRanges.OccurrencesRange[]
				{
					rangesInfo.ExpandAroundDateOccurrenceRange
				};
				getOccurrences = delegate()
				{
					RecurringMasterItemIdRanges.ExpandAroundDateOccurrenceRangeType expandAroundDateOccurrenceRange = rangesInfo.ExpandAroundDateOccurrenceRange;
					int count = Math.Min(expandAroundDateOccurrenceRange.Count, 732);
					return calendarItem.Recurrence.GetRecentOccurrencesInfoList(expandAroundDateOccurrenceRange.ExpandOccurrencesAroundExDateTime.Value, count);
				};
			}
			if (ranges == null || ranges.Length == 0 || !this.IsAtLeastOneValidRange(ranges))
			{
				return;
			}
			warning = this.AddFilteredOccurrences(itemsList, ranges, getOccurrences, calendarItem);
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x00073318 File Offset: 0x00071518
		private ServiceError AddFilteredOccurrences(List<ItemType> itemsList, RecurringMasterItemIdRanges.OccurrencesRange[] ranges, Func<IList<OccurrenceInfo>> getOccurrences, CalendarItem calendarItem)
		{
			ServiceError serviceError = null;
			IList<OccurrenceInfo> list = getOccurrences();
			foreach (OccurrenceInfo occurrenceInfo in list)
			{
				if (this.IsOccurrenceInRanges(occurrenceInfo, ranges))
				{
					Item item = null;
					try
					{
						item = calendarItem.OpenOccurrence(occurrenceInfo.VersionedId.ObjectId, null);
						IdAndSession idAndSession = IdAndSession.CreateFromItem(item);
						itemsList.Add(this.GetItemObject(idAndSession, ref item, out serviceError));
						if (serviceError != null)
						{
							return serviceError;
						}
					}
					finally
					{
						if (item != null)
						{
							item.Dispose();
						}
					}
				}
			}
			return null;
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x000733C8 File Offset: 0x000715C8
		internal override int StepCount
		{
			get
			{
				return this.itemIds.Count;
			}
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x000733D8 File Offset: 0x000715D8
		internal override IExchangeWebMethodResponse GetResponse()
		{
			GetItemResponse getItemResponse = new GetItemResponse();
			getItemResponse.BuildForResults<ItemType[]>(base.Results);
			return getItemResponse;
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x000733F8 File Offset: 0x000715F8
		private ItemType GetItemObject(IdAndSession idAndSession, ref Item xsoItem, out ServiceError warning)
		{
			warning = null;
			base.CallContext.AuthZBehavior.OnGetItem(idAndSession.GetAsStoreObjectId());
			ItemResponseShape itemResponseShape = null;
			if (base.CallContext.IsExternalUser)
			{
				warning = ExternalUserHandler.CheckAndGetResponseShape(base.GetType(), idAndSession as ExternalUserIdAndSession, this.responseShape, out itemResponseShape);
			}
			ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(3238407485U, this.itemIds[base.CurrentStep].GetId());
			ToServiceObjectPropertyList toServiceObjectPropertyList = XsoDataConverter.GetToServiceObjectPropertyList(idAndSession.Id, idAndSession.Session, (itemResponseShape == null) ? this.responseShape : itemResponseShape, base.ParticipantResolver);
			PropertyDefinition[] array = toServiceObjectPropertyList.GetPropertyDefinitions();
			if (!((IList)array).Contains(ItemSchema.Size))
			{
				PropertyDefinition[] array2 = new PropertyDefinition[array.Length + 1];
				array2[0] = ItemSchema.Size;
				array.CopyTo(array2, 1);
				array = array2;
			}
			if (xsoItem == null)
			{
				xsoItem = idAndSession.GetRootXsoItem(array);
			}
			else
			{
				xsoItem.Load(array);
			}
			if (base.CallContext.IsExternalUser)
			{
				List<PropertyPath> allowedProperties = ExternalUserHandler.GetAllowedProperties(idAndSession as ExternalUserIdAndSession, xsoItem);
				if (allowedProperties != null)
				{
					toServiceObjectPropertyList.FilterProperties(allowedProperties);
				}
			}
			else if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2012) && xsoItem is CalendarItemBase && xsoItem.Session is MailboxSession && ((CalendarItemBase)xsoItem).Sensitivity != Sensitivity.Normal && ((MailboxSession)xsoItem.Session).ShouldFilterPrivateItems)
			{
				toServiceObjectPropertyList.FilterProperties(ExternalUserCalendarResponseShape.CalendarPropertiesPrivateItemWithSubject);
			}
			this.SuperSizeCheck(xsoItem);
			toServiceObjectPropertyList.CharBuffer = this.charBuffer;
			StoreObjectId storeObjectId = StoreId.GetStoreObjectId(idAndSession.Id);
			ItemType itemType = ItemType.CreateFromStoreObjectType(storeObjectId.ObjectType);
			if (IrmUtils.IsIrmEnabled(this.responseShape.ClientSupportsIrm, idAndSession.Session))
			{
				IrmUtils.DecodeIrmMessage(idAndSession.Session, xsoItem, true);
			}
			if (idAndSession.Session is PublicFolderSession && ClientInfo.OWA.IsMatch(idAndSession.Session.ClientInfoString))
			{
				toServiceObjectPropertyList.CommandOptions |= CommandOptions.ConvertParentFolderIdToPublicFolderId;
			}
			ServiceCommandBase.LoadServiceObject(itemType, xsoItem, idAndSession, this.responseShape, toServiceObjectPropertyList);
			bool flag = !string.IsNullOrWhiteSpace(itemType.Preview);
			if (flag && IrmUtils.IsMessageRestrictedAndDecoded(xsoItem))
			{
				itemType.Preview = IrmUtils.GetItemPreview(xsoItem);
			}
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2012))
			{
				EWSSettings.SetInlineAttachmentFlags(itemType);
			}
			return itemType;
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x00073638 File Offset: 0x00071838
		private void SuperSizeCheck(Item xsoItem)
		{
			if (!this.responseShape.IncludeMimeContent && !this.ResponseShapeIncludes(PropertyUriEnum.MimeContent) && !this.ResponseShapeIncludes(PropertyUriEnum.MimeContentUTF8))
			{
				return;
			}
			uint num = 0U;
			try
			{
				object obj = xsoItem[ItemSchema.Size];
				if (obj is PropertyError)
				{
					ExTraceGlobals.GetItemCallTracer.TraceDebug<int>((long)this.GetHashCode(), "[GetItem:SuperSizeCheck] Hashcode: {0}. Item size not found", this.GetHashCode());
				}
				else
				{
					num = (uint)((int)obj);
				}
			}
			catch (NotInBagPropertyErrorException)
			{
				ExTraceGlobals.GetItemCallTracer.TraceDebug<int>((long)this.GetHashCode(), "[GetItem:SuperSizeCheck] Hashcode: {0}. Item size not in property bag", this.GetHashCode());
			}
			if (this.ResponseShapeIncludes(PropertyUriEnum.MimeContent) && this.ResponseShapeIncludes(PropertyUriEnum.MimeContentUTF8))
			{
				num *= 2U;
			}
			if ((ulong)num > (ulong)((long)Global.GetAttachmentSizeLimit))
			{
				throw new MessageTooBigException(CoreResources.ErrorMessageSizeExceeded, null);
			}
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x00073708 File Offset: 0x00071908
		private bool ResponseShapeIncludes(PropertyUriEnum propertyUri)
		{
			if (this.responseShape.AdditionalProperties == null)
			{
				return false;
			}
			foreach (PropertyPath propertyPath in this.responseShape.AdditionalProperties)
			{
				PropertyUri propertyUri2 = propertyPath as PropertyUri;
				if (propertyUri2 != null && propertyUri2.Uri == propertyUri)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x00073764 File Offset: 0x00071964
		private void PrefetchItems(IdAndSession idAndSession, List<StoreId> itemIds)
		{
			MailboxSession mailboxSession = idAndSession.Session as MailboxSession;
			if (mailboxSession != null)
			{
				mailboxSession.PrereadMessages(itemIds.ToArray());
			}
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x0007378C File Offset: 0x0007198C
		private void Dispose(bool isDisposing)
		{
			ExTraceGlobals.GetItemCallTracer.TraceDebug<int, bool, bool>((long)this.GetHashCode(), "[GetItem:Dispose(bool)] Hashcode: {0}. IsDisposing: {1}, Already Disposed: {2}", this.GetHashCode(), isDisposing, this.isDisposed);
			if (!this.isDisposed)
			{
				if (isDisposing)
				{
					this.charBuffer = null;
				}
				this.isDisposed = true;
			}
		}

		// Token: 0x04000EE1 RID: 3809
		internal const int BufferSize = 32768;

		// Token: 0x04000EE2 RID: 3810
		private IList<BaseItemId> itemIds;

		// Token: 0x04000EE3 RID: 3811
		private ItemResponseShape responseShape;

		// Token: 0x04000EE4 RID: 3812
		private bool isDisposed;

		// Token: 0x04000EE5 RID: 3813
		private char[] charBuffer = new char[32768];
	}
}
