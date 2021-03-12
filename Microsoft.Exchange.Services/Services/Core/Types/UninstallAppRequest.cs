using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000490 RID: 1168
	[XmlType("UninstallAppRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class UninstallAppRequest : BaseRequest
	{
		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060022DE RID: 8926 RVA: 0x000A35ED File Offset: 0x000A17ED
		// (set) Token: 0x060022DF RID: 8927 RVA: 0x000A35F5 File Offset: 0x000A17F5
		[XmlElement]
		public string ID { get; set; }

		// Token: 0x060022E0 RID: 8928 RVA: 0x000A35FE File Offset: 0x000A17FE
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new UninstallApp(callContext, this);
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x000A3607 File Offset: 0x000A1807
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x000A360A File Offset: 0x000A180A
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
