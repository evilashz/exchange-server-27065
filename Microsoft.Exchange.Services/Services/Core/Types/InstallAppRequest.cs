using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200045E RID: 1118
	[XmlType("InstallAppRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class InstallAppRequest : BaseRequest
	{
		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060020EF RID: 8431 RVA: 0x000A20B9 File Offset: 0x000A02B9
		// (set) Token: 0x060020F0 RID: 8432 RVA: 0x000A20C1 File Offset: 0x000A02C1
		[XmlElement]
		public string Manifest { get; set; }

		// Token: 0x060020F1 RID: 8433 RVA: 0x000A20CA File Offset: 0x000A02CA
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new InstallApp(callContext, this);
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x000A20D3 File Offset: 0x000A02D3
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x000A20D6 File Offset: 0x000A02D6
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
