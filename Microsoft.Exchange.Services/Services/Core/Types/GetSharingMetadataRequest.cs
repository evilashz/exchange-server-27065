using System;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000450 RID: 1104
	[XmlType(TypeName = "GetSharingMetadataType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetSharingMetadataRequest : BaseRequest
	{
		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06002069 RID: 8297 RVA: 0x000A1B41 File Offset: 0x0009FD41
		// (set) Token: 0x0600206A RID: 8298 RVA: 0x000A1B49 File Offset: 0x0009FD49
		[XmlAnyElement(Name = "IdOfFolderToShare")]
		public XmlElement IdOfFolderToShare
		{
			get
			{
				return this.idOfFolderToShare;
			}
			set
			{
				this.idOfFolderToShare = value;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600206B RID: 8299 RVA: 0x000A1B52 File Offset: 0x0009FD52
		// (set) Token: 0x0600206C RID: 8300 RVA: 0x000A1B5A File Offset: 0x0009FD5A
		[XmlElement("SenderSmtpAddress", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string SenderSmtpAddress
		{
			get
			{
				return this.senderSmtpAddress;
			}
			set
			{
				this.senderSmtpAddress = value;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x0600206D RID: 8301 RVA: 0x000A1B63 File Offset: 0x0009FD63
		// (set) Token: 0x0600206E RID: 8302 RVA: 0x000A1B6B File Offset: 0x0009FD6B
		[XmlArrayItem("SmtpAddress", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
		[XmlArray("Recipients", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string[] Recipients
		{
			get
			{
				return this.recipients;
			}
			set
			{
				this.recipients = value;
			}
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x000A1B74 File Offset: 0x0009FD74
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetSharingMetadata(callContext, this);
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x000A1B7D File Offset: 0x0009FD7D
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return BaseRequest.GetServerInfoForSingleId(callContext, this.IdOfFolderToShare);
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x000A1B8B File Offset: 0x0009FD8B
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x04001449 RID: 5193
		private XmlElement idOfFolderToShare;

		// Token: 0x0400144A RID: 5194
		private string senderSmtpAddress;

		// Token: 0x0400144B RID: 5195
		private string[] recipients;
	}
}
