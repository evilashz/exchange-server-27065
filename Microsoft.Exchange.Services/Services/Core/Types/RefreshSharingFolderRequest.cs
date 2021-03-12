using System;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200046C RID: 1132
	[XmlType(TypeName = "RefreshSharingFolderType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class RefreshSharingFolderRequest : BaseRequest
	{
		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x0600216C RID: 8556 RVA: 0x000A25A6 File Offset: 0x000A07A6
		// (set) Token: 0x0600216D RID: 8557 RVA: 0x000A25AE File Offset: 0x000A07AE
		[XmlAnyElement(Name = "SharingFolderId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public XmlElement SharingFolderId
		{
			get
			{
				return this.sharingFolderId;
			}
			set
			{
				this.sharingFolderId = value;
			}
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x000A25B7 File Offset: 0x000A07B7
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new RefreshSharingFolder(callContext, this);
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x000A25C0 File Offset: 0x000A07C0
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return BaseRequest.GetServerInfoForSingleId(callContext, this.SharingFolderId);
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x000A25CE File Offset: 0x000A07CE
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x04001495 RID: 5269
		private XmlElement sharingFolderId;
	}
}
