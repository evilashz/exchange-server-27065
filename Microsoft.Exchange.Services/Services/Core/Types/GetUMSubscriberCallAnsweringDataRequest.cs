using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000458 RID: 1112
	[XmlType("GetUMSubscriberCallAnsweringDataType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetUMSubscriberCallAnsweringDataRequest : BaseRequest
	{
		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060020B7 RID: 8375 RVA: 0x000A1DF1 File Offset: 0x0009FFF1
		// (set) Token: 0x060020B8 RID: 8376 RVA: 0x000A1DF9 File Offset: 0x0009FFF9
		[XmlElement("Timeout")]
		[DataMember(Name = "Timeout")]
		public string Timeout { get; set; }

		// Token: 0x060020B9 RID: 8377 RVA: 0x000A1E02 File Offset: 0x000A0002
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetUMSubscriberCallAnsweringData(callContext, this);
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x000A1E0B File Offset: 0x000A000B
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x000A1E0E File Offset: 0x000A000E
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
