using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000419 RID: 1049
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "EndInstantSearchSessionRequest", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class EndInstantSearchSessionRequest : BaseRequest
	{
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001E22 RID: 7714 RVA: 0x0009FC4F File Offset: 0x0009DE4F
		// (set) Token: 0x06001E23 RID: 7715 RVA: 0x0009FC57 File Offset: 0x0009DE57
		[DataMember(IsRequired = true)]
		public string SessionId { get; set; }

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001E24 RID: 7716 RVA: 0x0009FC60 File Offset: 0x0009DE60
		// (set) Token: 0x06001E25 RID: 7717 RVA: 0x0009FC68 File Offset: 0x0009DE68
		[DataMember]
		public string DeviceId { get; set; }

		// Token: 0x06001E26 RID: 7718 RVA: 0x0009FC71 File Offset: 0x0009DE71
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new EndInstantSearchSession(callContext, this);
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x0009FC7A File Offset: 0x0009DE7A
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x0009FC7D File Offset: 0x0009DE7D
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x0009FC80 File Offset: 0x0009DE80
		internal override void Validate()
		{
			base.Validate();
		}
	}
}
