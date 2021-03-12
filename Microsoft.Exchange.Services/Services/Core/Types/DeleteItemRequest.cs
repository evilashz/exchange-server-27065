using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000411 RID: 1041
	[KnownType(typeof(OccurrenceItemId))]
	[XmlType("DeleteItemType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[KnownType(typeof(ItemId))]
	[KnownType(typeof(RecurringMasterItemId))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeleteItemRequest : BaseRequest
	{
		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001DBE RID: 7614 RVA: 0x0009F704 File Offset: 0x0009D904
		// (set) Token: 0x06001DBF RID: 7615 RVA: 0x0009F70C File Offset: 0x0009D90C
		[XmlArray("ItemIds", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[XmlArrayItem("RecurringMasterItemId", typeof(RecurringMasterItemId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("OccurrenceItemId", typeof(OccurrenceItemId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("ItemId", typeof(ItemId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[DataMember(Name = "ItemIds", IsRequired = true, Order = 1)]
		public BaseItemId[] Ids { get; set; }

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001DC0 RID: 7616 RVA: 0x0009F715 File Offset: 0x0009D915
		// (set) Token: 0x06001DC1 RID: 7617 RVA: 0x0009F71D File Offset: 0x0009D91D
		[IgnoreDataMember]
		[XmlAttribute("DeleteType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public DisposalType DeleteType { get; set; }

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001DC2 RID: 7618 RVA: 0x0009F726 File Offset: 0x0009D926
		// (set) Token: 0x06001DC3 RID: 7619 RVA: 0x0009F733 File Offset: 0x0009D933
		[XmlIgnore]
		[DataMember(Name = "DeleteType", IsRequired = true, Order = 2)]
		public string DeleteTypeString
		{
			get
			{
				return EnumUtilities.ToString<DisposalType>(this.DeleteType);
			}
			set
			{
				this.DeleteType = EnumUtilities.Parse<DisposalType>(value);
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x0009F741 File Offset: 0x0009D941
		// (set) Token: 0x06001DC5 RID: 7621 RVA: 0x0009F749 File Offset: 0x0009D949
		[DataMember(IsRequired = false, Order = 3)]
		[XmlAttribute("SendMeetingCancellations", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string SendMeetingCancellations { get; set; }

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001DC6 RID: 7622 RVA: 0x0009F752 File Offset: 0x0009D952
		// (set) Token: 0x06001DC7 RID: 7623 RVA: 0x0009F75A File Offset: 0x0009D95A
		[DataMember(IsRequired = false, Order = 4)]
		[XmlAttribute("AffectedTaskOccurrences", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string AffectedTaskOccurrences { get; set; }

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001DC8 RID: 7624 RVA: 0x0009F763 File Offset: 0x0009D963
		// (set) Token: 0x06001DC9 RID: 7625 RVA: 0x0009F76B File Offset: 0x0009D96B
		[DataMember(IsRequired = false, Order = 5)]
		[XmlAttribute(AttributeName = "SuppressReadReceipts")]
		public bool SuppressReadReceipts { get; set; }

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001DCA RID: 7626 RVA: 0x0009F774 File Offset: 0x0009D974
		// (set) Token: 0x06001DCB RID: 7627 RVA: 0x0009F77C File Offset: 0x0009D97C
		[DataMember(IsRequired = false, Order = 6)]
		[XmlIgnore]
		public bool ReturnMovedItemIds { get; set; }

		// Token: 0x06001DCC RID: 7628 RVA: 0x0009F785 File Offset: 0x0009D985
		protected override List<ServiceObjectId> GetAllIds()
		{
			return new List<ServiceObjectId>(this.Ids);
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001DCD RID: 7629 RVA: 0x0009F792 File Offset: 0x0009D992
		// (set) Token: 0x06001DCE RID: 7630 RVA: 0x0009F79A File Offset: 0x0009D99A
		[IgnoreDataMember]
		[XmlIgnore]
		internal List<StoreId> ItemStoreIds { get; private set; }

		// Token: 0x06001DCF RID: 7631 RVA: 0x0009F7A3 File Offset: 0x0009D9A3
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			if (this.CanOptimizeCommandExecution(callContext))
			{
				return new DeleteItemBatch(callContext, this);
			}
			return new DeleteItem(callContext, this);
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x0009F7C0 File Offset: 0x0009D9C0
		internal override bool CanOptimizeCommandExecution(CallContext callContext)
		{
			if (!base.AllowCommandOptimization("deleteitem"))
			{
				return false;
			}
			if (ExchangeVersion.Current == ExchangeVersion.Exchange2007)
			{
				return false;
			}
			if (this.Ids == null || this.Ids.Length < 2)
			{
				return false;
			}
			IdConverter idConverter = new IdConverter(callContext);
			List<StoreId> list = new List<StoreId>();
			Guid? guid = null;
			foreach (BaseItemId itemId in this.Ids)
			{
				StoreId storeId;
				Guid value;
				bool flag = idConverter.TryGetStoreIdAndMailboxGuidFromItemId(itemId, out storeId, out value) && this.ItemCanBeBulkDeleted(storeId);
				if (guid == null)
				{
					guid = new Guid?(value);
				}
				if (!flag || !value.Equals(guid.Value))
				{
					return false;
				}
				list.Add(storeId);
			}
			this.ItemStoreIds = list;
			return true;
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x0009F894 File Offset: 0x0009DA94
		private bool ItemCanBeBulkDeleted(StoreId storeId)
		{
			StoreObjectId storeObjectId = StoreId.GetStoreObjectId(storeId);
			StoreObjectType objectType = storeObjectId.ObjectType;
			if (objectType != StoreObjectType.Unknown)
			{
				switch (objectType)
				{
				case StoreObjectType.CalendarItem:
				case StoreObjectType.CalendarItemOccurrence:
				case StoreObjectType.Task:
					return false;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x0009F8D5 File Offset: 0x0009DAD5
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.Ids == null)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForItemIdList(callContext, this.Ids);
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x0009F8ED File Offset: 0x0009DAED
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.Ids == null || this.Ids.Length < taskStep)
			{
				return null;
			}
			return base.GetResourceKeysForItemId(true, callContext, this.Ids[taskStep]);
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x0009F914 File Offset: 0x0009DB14
		internal override void Validate()
		{
			base.Validate();
			if (this.Ids == null || this.Ids.Length == 0)
			{
				throw FaultExceptionUtilities.CreateFault(new ServiceInvalidOperationException(ResponseCodeType.ErrorInvalidIdMalformed), FaultParty.Sender);
			}
		}
	}
}
