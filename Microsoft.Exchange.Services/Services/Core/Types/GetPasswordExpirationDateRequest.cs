using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000442 RID: 1090
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "GetPasswordExpirationDateType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetPasswordExpirationDateRequest : BaseRequest
	{
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001FFE RID: 8190 RVA: 0x000A17CD File Offset: 0x0009F9CD
		// (set) Token: 0x06001FFF RID: 8191 RVA: 0x000A17D5 File Offset: 0x0009F9D5
		[DataMember]
		public string MailboxSmtpAddress { get; set; }

		// Token: 0x06002000 RID: 8192 RVA: 0x000A17DE File Offset: 0x0009F9DE
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetPasswordExpirationDate(callContext, this);
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x000A17E7 File Offset: 0x0009F9E7
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x000A17EA File Offset: 0x0009F9EA
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
