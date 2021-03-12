using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200049A RID: 1178
	[XmlType(TypeName = "UpdateMailboxAssociationType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class UpdateMailboxAssociationRequest : BaseRequest
	{
		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x0600235E RID: 9054 RVA: 0x000A3DF7 File Offset: 0x000A1FF7
		// (set) Token: 0x0600235F RID: 9055 RVA: 0x000A3DFF File Offset: 0x000A1FFF
		[XmlElement("Association")]
		public MailboxAssociationType Association { get; set; }

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06002360 RID: 9056 RVA: 0x000A3E08 File Offset: 0x000A2008
		// (set) Token: 0x06002361 RID: 9057 RVA: 0x000A3E10 File Offset: 0x000A2010
		[XmlElement("Master")]
		public MasterMailboxType Master { get; set; }

		// Token: 0x06002362 RID: 9058 RVA: 0x000A3E19 File Offset: 0x000A2019
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new UpdateMailboxAssociation(callContext, this);
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x000A3E22 File Offset: 0x000A2022
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return IdConverter.GetServerInfoForCallContext(callContext);
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000A3E2A File Offset: 0x000A202A
		internal override ResourceKey[] GetResources(CallContext callContext, int currentStep)
		{
			return base.GetResourceKeysFromProxyInfo(true, callContext);
		}
	}
}
