using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000447 RID: 1095
	[XmlType("GetPhoneCallInformationRequest", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetPhoneCallInformationRequest : BaseRequest
	{
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x000A18E4 File Offset: 0x0009FAE4
		// (set) Token: 0x06002023 RID: 8227 RVA: 0x000A18EC File Offset: 0x0009FAEC
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

		// Token: 0x06002024 RID: 8228 RVA: 0x000A18F5 File Offset: 0x0009FAF5
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetPhoneCallInformation(callContext, this);
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x000A18FE File Offset: 0x0009FAFE
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x000A1901 File Offset: 0x0009FB01
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}

		// Token: 0x0400142C RID: 5164
		private PhoneCallId callId;
	}
}
