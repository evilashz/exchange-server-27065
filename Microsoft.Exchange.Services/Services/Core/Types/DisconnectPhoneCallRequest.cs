using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000417 RID: 1047
	[XmlType("DisconnectPhoneCallRequest", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DisconnectPhoneCallRequest : BaseRequest
	{
		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001E09 RID: 7689 RVA: 0x0009FB3A File Offset: 0x0009DD3A
		// (set) Token: 0x06001E0A RID: 7690 RVA: 0x0009FB42 File Offset: 0x0009DD42
		[DataMember(Name = "PhoneCallId", IsRequired = true)]
		[XmlElement("PhoneCallId")]
		public PhoneCallId CallId
		{
			get
			{
				return this.callId;
			}
			set
			{
				this.callId = value;
			}
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x0009FB4B File Offset: 0x0009DD4B
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new DisconnectPhoneCall(callContext, this);
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x0009FB54 File Offset: 0x0009DD54
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x0009FB57 File Offset: 0x0009DD57
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}

		// Token: 0x0400135F RID: 4959
		private PhoneCallId callId;
	}
}
