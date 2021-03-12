using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003FC RID: 1020
	[XmlInclude(typeof(CopyItemRequest))]
	[XmlType("BaseMoveCopyItemType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[KnownType(typeof(CopyItemRequest))]
	[KnownType(typeof(MoveItemRequest))]
	[XmlInclude(typeof(MoveItemRequest))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public abstract class BaseMoveCopyItemRequest : BaseRequest
	{
		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x0009E97B File Offset: 0x0009CB7B
		// (set) Token: 0x06001CE3 RID: 7395 RVA: 0x0009E983 File Offset: 0x0009CB83
		[XmlElement("ToFolderId")]
		[DataMember(IsRequired = true, Order = 1)]
		public TargetFolderId ToFolderId { get; set; }

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x0009E98C File Offset: 0x0009CB8C
		// (set) Token: 0x06001CE5 RID: 7397 RVA: 0x0009E994 File Offset: 0x0009CB94
		[XmlArrayItem("ItemId", typeof(ItemId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("OccurrenceItemId", typeof(OccurrenceItemId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[DataMember(Name = "ItemIds", IsRequired = true, Order = 2)]
		[XmlArray("ItemIds", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[XmlArrayItem("RecurringMasterItemId", typeof(RecurringMasterItemId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseItemId[] Ids { get; set; }

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001CE6 RID: 7398 RVA: 0x0009E99D File Offset: 0x0009CB9D
		// (set) Token: 0x06001CE7 RID: 7399 RVA: 0x0009E9A5 File Offset: 0x0009CBA5
		[DataMember(IsRequired = false, Order = 3)]
		[XmlElement("ReturnNewItemIds", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public bool ReturnNewItemIds
		{
			get
			{
				return this.returnNewItemIds;
			}
			set
			{
				this.returnNewItemIds = value;
			}
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x0009E9B0 File Offset: 0x0009CBB0
		protected override List<ServiceObjectId> GetAllIds()
		{
			return new List<ServiceObjectId>(this.Ids)
			{
				this.ToFolderId.BaseFolderId
			};
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001CE9 RID: 7401 RVA: 0x0009E9DB File Offset: 0x0009CBDB
		// (set) Token: 0x06001CEA RID: 7402 RVA: 0x0009E9E3 File Offset: 0x0009CBE3
		[IgnoreDataMember]
		[XmlIgnore]
		internal List<StoreId> ItemStoreIds { get; private set; }

		// Token: 0x06001CEB RID: 7403 RVA: 0x0009EA00 File Offset: 0x0009CC00
		internal override bool CanOptimizeCommandExecution(CallContext callContext)
		{
			if (!base.AllowCommandOptimization(this.CommandName) || !callContext.AuthZBehavior.IsAllowedToOptimizeMoveCopyCommand())
			{
				return false;
			}
			if (ExchangeVersion.Current == ExchangeVersion.Exchange2007)
			{
				return false;
			}
			if (this.Ids == null || this.Ids.Length == 0)
			{
				return false;
			}
			IdConverter idConverter = new IdConverter(callContext);
			List<StoreId> list = new List<StoreId>();
			Guid? guid = null;
			foreach (BaseItemId itemId in this.Ids)
			{
				StoreId item;
				Guid value;
				bool flag = idConverter.TryGetStoreIdAndMailboxGuidFromItemId(itemId, out item, out value);
				if (guid == null)
				{
					guid = new Guid?(value);
				}
				if (!flag || !value.Equals(guid.Value))
				{
					ExTraceGlobals.CopyItemCallTracer.TraceDebug((long)this.GetHashCode(), "Move/CopyItem operation cannot be optimized. Not all items are in the same mailbox.");
					return false;
				}
				list.Add(item);
			}
			if (list.Count((StoreId id) => IdConverter.GetAsStoreObjectId(id).ObjectType == StoreObjectType.CalendarItemOccurrence) > 0)
			{
				ExTraceGlobals.CopyItemCallTracer.TraceDebug((long)this.GetHashCode(), "Move/CopyItem operation cannot be optimized. List of items contains one or more calendar occurrences.");
				return false;
			}
			this.ItemStoreIds = list;
			return true;
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001CEC RID: 7404
		protected abstract string CommandName { get; }

		// Token: 0x06001CED RID: 7405 RVA: 0x0009EB27 File Offset: 0x0009CD27
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return BaseRequest.GetServerInfoForFolderId(callContext, this.ToFolderId.BaseFolderId);
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x0009EB3C File Offset: 0x0009CD3C
		internal override ResourceKey[] GetResources(CallContext callContext, int currentStep)
		{
			if (this.toFolderResourceKey == null)
			{
				BaseServerIdInfo serverInfoForFolderId = BaseRequest.GetServerInfoForFolderId(callContext, this.ToFolderId.BaseFolderId);
				if (serverInfoForFolderId != null)
				{
					this.toFolderResourceKey = serverInfoForFolderId.ToResourceKey(true);
				}
			}
			ResourceKey[] array = null;
			BaseServerIdInfo serverInfoForItemId = BaseRequest.GetServerInfoForItemId(callContext, this.Ids[currentStep]);
			if (serverInfoForItemId != null)
			{
				array = serverInfoForItemId.ToResourceKey(false);
			}
			List<ResourceKey> list = new List<ResourceKey>();
			if (this.toFolderResourceKey != null)
			{
				list.AddRange(this.toFolderResourceKey);
			}
			if (array != null)
			{
				list.AddRange(array);
			}
			if (list.Count != 0)
			{
				return list.ToArray();
			}
			return null;
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x0009EBC3 File Offset: 0x0009CDC3
		internal override void Validate()
		{
			base.Validate();
			if (this.Ids == null || this.Ids.Length == 0)
			{
				throw FaultExceptionUtilities.CreateFault(new ServiceInvalidOperationException(ResponseCodeType.ErrorInvalidIdMalformed), FaultParty.Sender);
			}
		}

		// Token: 0x040012F5 RID: 4853
		private ResourceKey[] toFolderResourceKey;

		// Token: 0x040012F6 RID: 4854
		private bool returnNewItemIds = true;
	}
}
