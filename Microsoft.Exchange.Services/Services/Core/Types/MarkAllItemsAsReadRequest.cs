using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000464 RID: 1124
	[XmlType("MarkAllItemsAsReadRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MarkAllItemsAsReadRequest : BaseRequest
	{
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06002116 RID: 8470 RVA: 0x000A2232 File Offset: 0x000A0432
		// (set) Token: 0x06002117 RID: 8471 RVA: 0x000A223A File Offset: 0x000A043A
		[XmlArray("FolderIds", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[DataMember(Name = "FolderIds", IsRequired = true, Order = 1)]
		[XmlArrayItem("FolderId", typeof(FolderId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseFolderId[] FolderIds { get; set; }

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06002118 RID: 8472 RVA: 0x000A2243 File Offset: 0x000A0443
		// (set) Token: 0x06002119 RID: 8473 RVA: 0x000A224B File Offset: 0x000A044B
		[DataMember(Name = "ReadFlag", IsRequired = true, Order = 2)]
		[XmlElement("ReadFlag", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public bool ReadFlag { get; set; }

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x0600211A RID: 8474 RVA: 0x000A2254 File Offset: 0x000A0454
		// (set) Token: 0x0600211B RID: 8475 RVA: 0x000A225C File Offset: 0x000A045C
		[DataMember(Name = "SuppressReadReceipts", IsRequired = true, Order = 3)]
		[XmlElement("SuppressReadReceipts", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public bool SuppressReadReceipts { get; set; }

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x0600211C RID: 8476 RVA: 0x000A2265 File Offset: 0x000A0465
		// (set) Token: 0x0600211D RID: 8477 RVA: 0x000A226D File Offset: 0x000A046D
		[DataMember(Name = "FromFilter", IsRequired = false)]
		[XmlIgnore]
		public string FromFilter { get; set; }

		// Token: 0x0600211E RID: 8478 RVA: 0x000A2276 File Offset: 0x000A0476
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			if (string.IsNullOrEmpty(this.FromFilter))
			{
				return new MarkAllItemsAsRead(callContext, this);
			}
			return new PeopleIKnowMarkAllAsReadCommand(callContext, this);
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x000A2294 File Offset: 0x000A0494
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.FolderIds == null)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForFolderIdList(callContext, this.FolderIds);
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x000A22AC File Offset: 0x000A04AC
		internal override void Validate()
		{
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x000A22AE File Offset: 0x000A04AE
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.FolderIds == null || this.FolderIds.Length < taskStep)
			{
				return null;
			}
			return base.GetResourceKeysForFolderId(true, callContext, this.FolderIds[taskStep]);
		}
	}
}
