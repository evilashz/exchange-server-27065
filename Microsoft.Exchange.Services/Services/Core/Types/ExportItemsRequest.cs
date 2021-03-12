using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200041D RID: 1053
	[XmlType("ExportItemsType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class ExportItemsRequest : BaseRequest, IRemoteArchiveRequest
	{
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001E3D RID: 7741 RVA: 0x0009FD1B File Offset: 0x0009DF1B
		// (set) Token: 0x06001E3E RID: 7742 RVA: 0x0009FD23 File Offset: 0x0009DF23
		[XmlElement("ItemIds", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public XmlNodeArray Ids
		{
			get
			{
				return this.ids;
			}
			set
			{
				this.ids = value;
				if (this.ids != null)
				{
					this.ids.Normalize();
				}
			}
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x0009FD40 File Offset: 0x0009DF40
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			if (this != null && ((IRemoteArchiveRequest)this).IsRemoteArchiveRequest(callContext))
			{
				return ((IRemoteArchiveRequest)this).GetRemoteArchiveServiceCommand(callContext);
			}
			return new ExportItems(callContext, this);
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x0009FD6A File Offset: 0x0009DF6A
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.ids == null)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForIdList(callContext, this.ids.Nodes);
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x0009FD88 File Offset: 0x0009DF88
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.ids == null || this.ids.Nodes.Count < taskStep)
			{
				return null;
			}
			if (this != null && ((IRemoteArchiveRequest)this).IsRemoteArchiveRequest(callContext))
			{
				return null;
			}
			return base.GetResourceKeysForXmlNode(false, callContext, this.ids.Nodes[taskStep]);
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001E42 RID: 7746 RVA: 0x0009FDDB File Offset: 0x0009DFDB
		// (set) Token: 0x06001E43 RID: 7747 RVA: 0x0009FDE3 File Offset: 0x0009DFE3
		ExchangeServiceBinding IRemoteArchiveRequest.ArchiveService { get; set; }

		// Token: 0x06001E44 RID: 7748 RVA: 0x0009FE1C File Offset: 0x0009E01C
		bool IRemoteArchiveRequest.IsRemoteArchiveRequest(CallContext callContext)
		{
			return ComplianceUtil.TryCreateArchiveService(callContext, this, this.ids != null, delegate
			{
				((IRemoteArchiveRequest)this).ArchiveService = ComplianceUtil.GetArchiveServiceForItemIdList(callContext, this.ids.Nodes);
			});
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x0009FE61 File Offset: 0x0009E061
		ServiceCommandBase IRemoteArchiveRequest.GetRemoteArchiveServiceCommand(CallContext callContext)
		{
			return new ExportRemoteArchiveItems(callContext, this);
		}

		// Token: 0x0400136B RID: 4971
		internal const string ElementName = "ExportItems";

		// Token: 0x0400136C RID: 4972
		private const string ItemIdsElementName = "ItemIds";

		// Token: 0x0400136D RID: 4973
		private XmlNodeArray ids;
	}
}
