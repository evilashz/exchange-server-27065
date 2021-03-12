using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200043B RID: 1083
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetItemType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetItemRequest : BaseRequest, IRemoteArchiveRequest
	{
		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001FBC RID: 8124 RVA: 0x000A117E File Offset: 0x0009F37E
		// (set) Token: 0x06001FBD RID: 8125 RVA: 0x000A1186 File Offset: 0x0009F386
		[XmlElement(ElementName = "ItemShape")]
		[DataMember(Name = "ItemShape", IsRequired = true, Order = 1)]
		public ItemResponseShape ItemShape { get; set; }

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001FBE RID: 8126 RVA: 0x000A118F File Offset: 0x0009F38F
		// (set) Token: 0x06001FBF RID: 8127 RVA: 0x000A1197 File Offset: 0x0009F397
		[XmlIgnore]
		[DataMember(Name = "ShapeName", IsRequired = false)]
		public string ShapeName { get; set; }

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001FC0 RID: 8128 RVA: 0x000A11A0 File Offset: 0x0009F3A0
		// (set) Token: 0x06001FC1 RID: 8129 RVA: 0x000A11A8 File Offset: 0x0009F3A8
		[DataMember(Name = "ItemIds", IsRequired = true, Order = 2)]
		[XmlArrayItem("OccurrenceItemId", typeof(OccurrenceItemId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemIdRanges", typeof(RecurringMasterItemIdRanges), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("ItemId", typeof(ItemId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemId", typeof(RecurringMasterItemId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArray("ItemIds")]
		public BaseItemId[] Ids { get; set; }

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001FC2 RID: 8130 RVA: 0x000A11B1 File Offset: 0x0009F3B1
		// (set) Token: 0x06001FC3 RID: 8131 RVA: 0x000A11B9 File Offset: 0x0009F3B9
		[IgnoreDataMember]
		[XmlIgnore]
		internal List<StoreId> PrefetchItemStoreIds { get; private set; }

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001FC4 RID: 8132 RVA: 0x000A11C2 File Offset: 0x0009F3C2
		// (set) Token: 0x06001FC5 RID: 8133 RVA: 0x000A11CA File Offset: 0x0009F3CA
		[IgnoreDataMember]
		[XmlIgnore]
		internal bool PrefetchItems { get; private set; }

		// Token: 0x06001FC6 RID: 8134 RVA: 0x000A11D3 File Offset: 0x0009F3D3
		protected override List<ServiceObjectId> GetAllIds()
		{
			return new List<ServiceObjectId>(this.Ids);
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x000A11E0 File Offset: 0x0009F3E0
		private bool CanPrefetchItems(CallContext callContext)
		{
			if (!base.AllowCommandOptimization("getitem"))
			{
				return false;
			}
			if (ExchangeVersion.Current == ExchangeVersion.Exchange2007)
			{
				return false;
			}
			if (this.Ids == null || this.Ids.Length == 1)
			{
				return false;
			}
			IdConverter idConverter = new IdConverter(callContext);
			List<StoreId> list = new List<StoreId>();
			Guid? guid = null;
			int num = 0;
			foreach (BaseItemId baseItemId in this.Ids)
			{
				StoreId item = null;
				Guid empty = Guid.Empty;
				bool flag = false;
				if (!string.IsNullOrEmpty(baseItemId.GetChangeKey()))
				{
					flag = idConverter.TryGetStoreIdAndMailboxGuidFromItemId(baseItemId, out item, out empty);
				}
				if (guid == null)
				{
					guid = new Guid?(empty);
				}
				if (!flag || !empty.Equals(guid.Value))
				{
					return false;
				}
				list.Add(item);
				if (num++ >= GetItemRequest.PrefetchItemSizeLimit)
				{
					break;
				}
			}
			this.PrefetchItemStoreIds = list;
			return true;
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x000A12D4 File Offset: 0x0009F4D4
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			if (this != null && ((IRemoteArchiveRequest)this).IsRemoteArchiveRequest(callContext))
			{
				return ((IRemoteArchiveRequest)this).GetRemoteArchiveServiceCommand(callContext);
			}
			this.PrefetchItems = this.CanPrefetchItems(callContext);
			return new GetItem(callContext, this);
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x000A130B File Offset: 0x0009F50B
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.Ids == null)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForItemIdList(callContext, this.Ids);
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x000A1324 File Offset: 0x0009F524
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.Ids == null || this.Ids.Length < taskStep)
			{
				return null;
			}
			if (this != null && ((IRemoteArchiveRequest)this).IsRemoteArchiveRequest(callContext))
			{
				return null;
			}
			BaseServerIdInfo serverInfoForItemId = BaseRequest.GetServerInfoForItemId(callContext, this.Ids[taskStep]);
			return BaseRequest.ServerInfoToResourceKeys(false, serverInfoForItemId);
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x000A136C File Offset: 0x0009F56C
		internal override void Validate()
		{
			base.Validate();
			if (this.Ids == null || this.Ids.Length == 0)
			{
				throw FaultExceptionUtilities.CreateFault(new ServiceInvalidOperationException(ResponseCodeType.ErrorInvalidIdMalformed), FaultParty.Sender);
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001FCC RID: 8140 RVA: 0x000A139C File Offset: 0x0009F59C
		// (set) Token: 0x06001FCD RID: 8141 RVA: 0x000A13A4 File Offset: 0x0009F5A4
		ExchangeServiceBinding IRemoteArchiveRequest.ArchiveService { get; set; }

		// Token: 0x06001FCE RID: 8142 RVA: 0x000A13D8 File Offset: 0x0009F5D8
		bool IRemoteArchiveRequest.IsRemoteArchiveRequest(CallContext callContext)
		{
			return ComplianceUtil.TryCreateArchiveService(callContext, this, this.Ids != null, delegate
			{
				((IRemoteArchiveRequest)this).ArchiveService = ComplianceUtil.GetArchiveServiceForItemIdList(callContext, this.Ids);
			});
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x000A141D File Offset: 0x0009F61D
		ServiceCommandBase IRemoteArchiveRequest.GetRemoteArchiveServiceCommand(CallContext callContext)
		{
			return new GetRemoteArchiveItem(callContext, this);
		}

		// Token: 0x040013FA RID: 5114
		private const string PrefetchItemSizeLimitKeyName = "GetItemPrefetchSizeLimit";

		// Token: 0x040013FB RID: 5115
		internal const string ItemIdsElementName = "ItemIds";

		// Token: 0x040013FC RID: 5116
		private static readonly int PrefetchItemSizeLimit = Global.GetAppSettingAsInt("GetItemPrefetchSizeLimit", 250);
	}
}
