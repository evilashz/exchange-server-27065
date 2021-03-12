using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003F2 RID: 1010
	[XmlType("AddDistributionGroupToImListRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddDistributionGroupToImListRequest : BaseRequest
	{
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001C55 RID: 7253 RVA: 0x0009E2D2 File Offset: 0x0009C4D2
		// (set) Token: 0x06001C56 RID: 7254 RVA: 0x0009E2DA File Offset: 0x0009C4DA
		[XmlElement(ElementName = "SmtpAddress")]
		[DataMember(Name = "SmtpAddress", IsRequired = true, Order = 1)]
		public string SmtpAddress { get; set; }

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001C57 RID: 7255 RVA: 0x0009E2E3 File Offset: 0x0009C4E3
		// (set) Token: 0x06001C58 RID: 7256 RVA: 0x0009E2EB File Offset: 0x0009C4EB
		[XmlElement(ElementName = "DisplayName")]
		[DataMember(Name = "DisplayName", IsRequired = true, Order = 2)]
		public string DisplayName { get; set; }

		// Token: 0x06001C59 RID: 7257 RVA: 0x0009E2F4 File Offset: 0x0009C4F4
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new AddDistributionGroupToImListCommand(callContext, this);
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x0009E2FD File Offset: 0x0009C4FD
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return IdConverter.GetServerInfoForCallContext(callContext);
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x0009E305 File Offset: 0x0009C505
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(true, callContext);
		}
	}
}
