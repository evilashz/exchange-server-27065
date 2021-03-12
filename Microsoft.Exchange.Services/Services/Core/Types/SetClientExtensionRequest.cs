using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200047A RID: 1146
	[XmlType("SetClientExtensionRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class SetClientExtensionRequest : BaseRequest
	{
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060021F0 RID: 8688 RVA: 0x000A2B02 File Offset: 0x000A0D02
		// (set) Token: 0x060021F1 RID: 8689 RVA: 0x000A2B0A File Offset: 0x000A0D0A
		[XmlArrayItem("Action", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public SetClientExtensionAction[] Actions { get; set; }

		// Token: 0x060021F2 RID: 8690 RVA: 0x000A2B13 File Offset: 0x000A0D13
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new SetClientExtension(callContext, this);
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x000A2B1C File Offset: 0x000A0D1C
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x000A2B1F File Offset: 0x000A0D1F
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
