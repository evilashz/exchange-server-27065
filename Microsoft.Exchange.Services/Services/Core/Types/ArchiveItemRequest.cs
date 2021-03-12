using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003F9 RID: 1017
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("ArchiveItemType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class ArchiveItemRequest : BaseRequest
	{
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001CB0 RID: 7344 RVA: 0x0009E6A8 File Offset: 0x0009C8A8
		// (set) Token: 0x06001CB1 RID: 7345 RVA: 0x0009E6B0 File Offset: 0x0009C8B0
		[XmlElement("ArchiveSourceFolderId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "ArchiveSourceFolderId", IsRequired = true, Order = 1)]
		public TargetFolderId SourceFolderId { get; set; }

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001CB2 RID: 7346 RVA: 0x0009E6B9 File Offset: 0x0009C8B9
		// (set) Token: 0x06001CB3 RID: 7347 RVA: 0x0009E6C1 File Offset: 0x0009C8C1
		[XmlArrayItem("ItemId", typeof(ItemId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemId", typeof(RecurringMasterItemId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArray("ItemIds", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[XmlArrayItem("OccurrenceItemId", typeof(OccurrenceItemId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[DataMember(Name = "ItemIds", IsRequired = true, Order = 2)]
		public BaseItemId[] Ids { get; set; }

		// Token: 0x06001CB4 RID: 7348 RVA: 0x0009E6CA File Offset: 0x0009C8CA
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new ArchiveItem(callContext, this);
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x0009E6D3 File Offset: 0x0009C8D3
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return this.GetServerInfoForStep(callContext, 0);
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x0009E6DD File Offset: 0x0009C8DD
		internal BaseServerIdInfo GetServerInfoForStep(CallContext callContext, int step)
		{
			return BaseRequest.GetServerInfoForFolderId(callContext, this.SourceFolderId.BaseFolderId);
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x0009E6F0 File Offset: 0x0009C8F0
		internal override ResourceKey[] GetResources(CallContext callContext, int currentStep)
		{
			return base.GetResourceKeysForFolderId(true, callContext, this.SourceFolderId.BaseFolderId);
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x0009E708 File Offset: 0x0009C908
		internal override void Validate()
		{
			base.Validate();
			if (this.Ids == null || this.Ids.Length == 0)
			{
				throw FaultExceptionUtilities.CreateFault(new ServiceInvalidOperationException(ResponseCodeType.ErrorInvalidIdMalformed), FaultParty.Sender);
			}
			if (this.SourceFolderId == null)
			{
				throw FaultExceptionUtilities.CreateFault(new ServiceInvalidOperationException(ResponseCodeType.ErrorInvalidFolderTypeForOperation), FaultParty.Sender);
			}
		}

		// Token: 0x040012DD RID: 4829
		internal const string SourceFolderIdElementName = "ArchiveSourceFolderId";

		// Token: 0x040012DE RID: 4830
		internal const string ItemsElementName = "ItemIds";
	}
}
