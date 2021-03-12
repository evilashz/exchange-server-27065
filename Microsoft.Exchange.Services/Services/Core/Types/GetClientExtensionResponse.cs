using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004ED RID: 1261
	[XmlType(TypeName = "GetClientExtensionResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetClientExtensionResponse : ResponseMessage
	{
		// Token: 0x060024B3 RID: 9395 RVA: 0x000A5183 File Offset: 0x000A3383
		public GetClientExtensionResponse()
		{
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x000A518B File Offset: 0x000A338B
		internal GetClientExtensionResponse(ServiceResultCode code, ServiceError error, GetClientExtensionResponse getClientExtensionResponse) : base(code, error)
		{
			if (getClientExtensionResponse != null)
			{
				this.ClientExtensions = getClientExtensionResponse.ClientExtensions;
				if (!string.IsNullOrEmpty(getClientExtensionResponse.RawMasterTableXml))
				{
					this.RawMasterTableXml = getClientExtensionResponse.RawMasterTableXml;
				}
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x060024B5 RID: 9397 RVA: 0x000A51BD File Offset: 0x000A33BD
		// (set) Token: 0x060024B6 RID: 9398 RVA: 0x000A51C5 File Offset: 0x000A33C5
		[XmlArrayItem("ClientExtension", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ClientExtension[] ClientExtensions { get; set; }

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x060024B7 RID: 9399 RVA: 0x000A51CE File Offset: 0x000A33CE
		// (set) Token: 0x060024B8 RID: 9400 RVA: 0x000A51D6 File Offset: 0x000A33D6
		[DataMember(Name = "RawMasterTableXml", IsRequired = false)]
		[XmlElement("RawMasterTableXml")]
		public string RawMasterTableXml { get; set; }

		// Token: 0x060024B9 RID: 9401 RVA: 0x000A51DF File Offset: 0x000A33DF
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetClientExtensionResponseMessage;
		}
	}
}
