using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200049E RID: 1182
	[XmlType(TypeName = "UploadItemsType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class UploadItemsRequest : BaseRequest
	{
		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x0600236F RID: 9071 RVA: 0x000A3ECF File Offset: 0x000A20CF
		// (set) Token: 0x06002370 RID: 9072 RVA: 0x000A3ED7 File Offset: 0x000A20D7
		[XmlElement("Items", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public XmlNodeArray Items
		{
			get
			{
				return this.items;
			}
			set
			{
				this.items = value;
			}
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x000A3EE0 File Offset: 0x000A20E0
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new UploadItems(callContext, this);
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x000A3EE9 File Offset: 0x000A20E9
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.items == null)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForIdList(callContext, this.items.Nodes);
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x000A3F08 File Offset: 0x000A2108
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			BaseServerIdInfo serverInfoForSingleId = BaseRequest.GetServerInfoForSingleId(callContext, this.items.Nodes[taskStep]);
			return BaseRequest.ServerInfoToResourceKeys(true, serverInfoForSingleId);
		}

		// Token: 0x0400154D RID: 5453
		private XmlNodeArray items;
	}
}
