using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200045F RID: 1119
	[XmlType("IsOffice365DomainRequest", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class IsOffice365DomainRequest : BaseRequest
	{
		// Token: 0x060020F4 RID: 8436 RVA: 0x000A20D9 File Offset: 0x000A02D9
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x000A20DC File Offset: 0x000A02DC
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060020F6 RID: 8438 RVA: 0x000A20DF File Offset: 0x000A02DF
		// (set) Token: 0x060020F7 RID: 8439 RVA: 0x000A20E7 File Offset: 0x000A02E7
		[XmlElement]
		public string EmailAddress { get; set; }

		// Token: 0x060020F8 RID: 8440 RVA: 0x000A20F0 File Offset: 0x000A02F0
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new IsOffice365Domain(callContext, this);
		}
	}
}
