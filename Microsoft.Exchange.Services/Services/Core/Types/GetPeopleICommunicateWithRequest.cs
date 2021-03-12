using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000445 RID: 1093
	[XmlType("GetPeopleICommunicateWithType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class GetPeopleICommunicateWithRequest : BaseRequest
	{
		// Token: 0x06002010 RID: 8208 RVA: 0x000A1853 File Offset: 0x0009FA53
		public GetPeopleICommunicateWithRequest(IOutgoingWebResponseContext outgoingResponse)
		{
			this.OutgoingResponse = outgoingResponse;
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06002011 RID: 8209 RVA: 0x000A1862 File Offset: 0x0009FA62
		// (set) Token: 0x06002012 RID: 8210 RVA: 0x000A186A File Offset: 0x0009FA6A
		internal ADObjectId AdObjectId { get; set; }

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06002013 RID: 8211 RVA: 0x000A1873 File Offset: 0x0009FA73
		// (set) Token: 0x06002014 RID: 8212 RVA: 0x000A187B File Offset: 0x0009FA7B
		internal IOutgoingWebResponseContext OutgoingResponse { get; set; }

		// Token: 0x06002015 RID: 8213 RVA: 0x000A1884 File Offset: 0x0009FA84
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetPeopleICommunicateWith(callContext, this);
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x000A188D File Offset: 0x0009FA8D
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x000A1890 File Offset: 0x0009FA90
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
