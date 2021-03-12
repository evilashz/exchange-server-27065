using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200044F RID: 1103
	[XmlType(TypeName = "GetSharingFolderType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetSharingFolderRequest : BaseRequest
	{
		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x0600205F RID: 8287 RVA: 0x000A1AEB File Offset: 0x0009FCEB
		// (set) Token: 0x06002060 RID: 8288 RVA: 0x000A1AF3 File Offset: 0x0009FCF3
		[XmlElement("SmtpAddress", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string SmtpAddress { get; set; }

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06002061 RID: 8289 RVA: 0x000A1AFC File Offset: 0x0009FCFC
		// (set) Token: 0x06002062 RID: 8290 RVA: 0x000A1B04 File Offset: 0x0009FD04
		[XmlElement("DataType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string DataType { get; set; }

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06002063 RID: 8291 RVA: 0x000A1B0D File Offset: 0x0009FD0D
		// (set) Token: 0x06002064 RID: 8292 RVA: 0x000A1B15 File Offset: 0x0009FD15
		[XmlElement("SharedFolderId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string SharedFolderId { get; set; }

		// Token: 0x06002065 RID: 8293 RVA: 0x000A1B1E File Offset: 0x0009FD1E
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetSharingFolder(callContext, this);
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x000A1B27 File Offset: 0x0009FD27
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return callContext.GetServerInfoForEffectiveCaller();
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x000A1B2F File Offset: 0x0009FD2F
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}
	}
}
